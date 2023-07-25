using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Jaxis.Inventory.Data.IBLDataItems;
using Jaxis.Util.Log4Net;

namespace Jaxis.Inventory.Data.DataAccess
{
    public class OnlineUPCLookup
    {
        [Serializable()]
        public class feed
        {
            [System.Xml.Serialization.XmlElementAttribute("status")]
            public Status OnlineStatus { get; set; }
            [System.Xml.Serialization.XmlElementAttribute("product")]
            public Products OnlineProduct { get; set; }
            [System.Xml.Serialization.XmlElementAttribute("company")]
            public Company OnlineCompany { get; set; }
        }

        [Serializable()]
        public class Status
        {
            [System.Xml.Serialization.XmlElementAttribute("version")]
            public int Version { get; set; }
            [System.Xml.Serialization.XmlElementAttribute("find")]
            public string Find { get; set; }
            [System.Xml.Serialization.XmlElementAttribute("code")]
            public int Code { get; set; }
            [System.Xml.Serialization.XmlElementAttribute("message")]
            public string Message { get; set; }
        }

        [Serializable()]
        public class Products
        {
            [System.Xml.Serialization.XmlElementAttribute("modified")]
            public string Modified { get; set; }
            [System.Xml.Serialization.XmlElementAttribute("ean13")]
            public string EAN13 { get; set; }
            [System.Xml.Serialization.XmlElementAttribute("upca")]
            public string UPCA { get; set; }
            [System.Xml.Serialization.XmlElementAttribute("upce")]
            public string UPCE { get; set; }
            [System.Xml.Serialization.XmlElementAttribute("product")]
            public string Product { get; set; }
            [System.Xml.Serialization.XmlElementAttribute("description")]
            public string Description { get; set; }
            [System.Xml.Serialization.XmlElementAttribute("category_no")]
            public string Category { get; set; }
            [System.Xml.Serialization.XmlElementAttribute("category_text")]
            public string CategoryText { get; set; }
            [System.Xml.Serialization.XmlElementAttribute("url")]
            public string URL { get; set; }
            [System.Xml.Serialization.XmlElementAttribute("has_long_desc")]
            public bool HasLongDesc { get; set; }
            [System.Xml.Serialization.XmlElementAttribute("image")]
            public string Image { get; set; }
            [System.Xml.Serialization.XmlElementAttribute("barcode")]
            public string Barcode { get; set; }
            [System.Xml.Serialization.XmlElementAttribute("locked")]
            public bool Locked { get; set; }
        }

        [Serializable()]
        public class Company
        {
            [System.Xml.Serialization.XmlElementAttribute("name")]
            public string Name { get; set; }
            [System.Xml.Serialization.XmlElementAttribute("logo")]
            public string Logo { get; set; }
            [System.Xml.Serialization.XmlElementAttribute("url")]
            public string URL { get; set; }
            [System.Xml.Serialization.XmlElementAttribute("address")]
            public string Address { get; set; }
            [System.Xml.Serialization.XmlElementAttribute("phone")]
            public string Phone { get; set; }
            [System.Xml.Serialization.XmlElementAttribute("locked")]
            public bool Locked { get; set; }
        }

        public IBLUPCItem GetOnlineUPCData( string _upcNumber )
        {
            var feed = new feed();
            feed.OnlineCompany = new Company();
            feed.OnlineProduct = new Products();
            feed.OnlineStatus = new Status();

            using( var stream = new StreamWriter( "feed.xml" ))
            {
                var s = new XmlSerializer(typeof(feed));
                s.Serialize(stream, feed);
            }

            return LookupUPC( _upcNumber );
        }


        protected IBLUPCItem LookupUPC(string _upcValue)
        {
            IBLUPCItem rc = BLManagerFactory.Get().ManageUPCs().Create();
            rc.ItemNumber = _upcValue;
            try
            {
                var url = string.Format("http://upcdata.info/feed.php?keycode=6A16DF454F3EBD61&mode=xml&find={0}", _upcValue);

                WebRequest request = WebRequest.Create(url);
                request.Timeout = 5000;
                request.Method = "POST";
                var postData = "UPC Query";
                var byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                var dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse response = request.GetResponse();

                dataStream = response.GetResponseStream();
                using (var reader = new StreamReader(dataStream))
                {
                    string responseFromServer = reader.ReadToEnd();

                    ParseResults(rc, responseFromServer.Replace("\\n", "").Replace("\n", ""), _upcValue);
                }
                response.Close();
            }
            catch (Exception)
            {
            }

            return rc;
        }

        private void ParseResults(IBLUPCItem _item, string _responseFromServer, string _upcValue)
        {
            try
            {
                var feed = Deserialize(_responseFromServer);
                if (feed.OnlineStatus.Code == 200)
                {
                    _item.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(feed.OnlineProduct.Product.ToLower()); 
                    
                    ///TODO: Update Manufacturer table...not sure we want to do this just yet...
                    //string manName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(feed.OnlineCompany.Name.ToLower());
                    //var man = BLManagerFactory.Get().ManageManufacturers().GetAll().Where( m => m.Name == manName ).FirstOrDefault();
                    //if (null != man)
                    //{
                    //    _item.ManufacturerID = man.ManufacturerID;
                    //}
                    //else
                    //{
                    //    man = BLManagerFactory.Get().ManageManufacturers().Create();
                    //    man.Name = manName;
                    //    BLManagerFactory.Get().ManageManufacturers().Save(man);
                    //}

                    const string pattern = @"(\d+)\s*(ml|oz)";

                    var match = Regex.Match(feed.OnlineProduct.Description, pattern,
                                            System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    if (match.Success)
                    {
                        _item.Size = int.Parse(match.Groups[1].Value);
                        var sizeTypeAbb = match.Groups[2].Value.ToUpper();

                        var sizeType =
                            BLManagerFactory.Get().ManageSizeTypes().GetAll().Where(s => s.Abbreviation == sizeTypeAbb).
                                FirstOrDefault();
                        if (null != sizeType)
                        {
                            _item.SizeType = sizeType;
                        }
                    }
                }
            }
            catch( Exception err)
            {
                Log.WriteException( "Online UPC Pull", err );
            }
        }

        protected feed Deserialize( string _data )
        {
            feed rc = null;
            var serializer = new XmlSerializer(typeof(feed));

            using ( var reader = new StringReader( _data ) )
            {
                rc = (feed)serializer.Deserialize(reader);
            }
            return rc;
        }
    }
}
