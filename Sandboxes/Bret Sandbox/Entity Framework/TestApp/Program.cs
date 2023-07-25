using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using BeverageMonitor.Entities;
using Jaxis.Util.Log4Net;
using Jaxis.Utilities.Database;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var data = new BeverageMonitorEntities())
            {
                CleanupManufacturers(data);
                CleanupUPCs(data);

                CleanupAliases(data);
            }
        }

        private static void CleanupAliases(BeverageMonitorEntities _data)
        {
            Log.Time("Entity Framework", LogType.Debug, false, () =>
            {
                var count = _data.TicketItemAliases.Count();


                var aliases = (from m in _data.TicketItemAliases
                                     select m.Description).Distinct();

                Console.WriteLine(string.Format("{1} Ticket Item Aliases Count: {0}", aliases.Count(), count));
                foreach (var alias in aliases)
                {
                    var copies = from m in _data.TicketItemAliases
                                 where m.Description == alias
                                 select m;

                    var first = copies.First();

                    foreach (var copy in copies)
                    {
                        if (copy.TicketItemAliasID != first.TicketItemAliasID)
                        {
                            _data.DeleteObject(copy);
                        }
                    }

                    var pours = from m in _data.Pours
                                 select m;

//                    Console.WriteLine(String.Format("Alias = {0}", alias));
                }
                _data.SaveChanges();
            });
        }

        private static void CleanupManufacturers(BeverageMonitorEntities _data)
        {
            Log.Time("Entity Framework", LogType.Debug, false, () =>
            {
                var count = _data.Manufacturers.Count();

                var manufacturers = (from m in _data.Manufacturers
                                     select m.Name).Distinct();

                Console.WriteLine(string.Format("{1} Manufacturer Count: {0}", manufacturers.Count(), count));
                foreach (var manufacturer in manufacturers)
                {
                    var copies = from m in _data.Manufacturers
                                 where m.Name == manufacturer
                                 select m;

                    var first = copies.First();

                    foreach (var copy in copies)
                    {
                        if (copy.ManufacturerID != first.ManufacturerID)
                        {
                            var upcs = from u in _data.UPCs
                                       where u.ManufacturerID == copy.ManufacturerID
                                       select u;
                            foreach (var upc in upcs)
                            {
                                upc.ManufacturerID = first.ManufacturerID;
                            }
                            _data.DeleteObject(copy);
                        }
                    }

//                    Console.WriteLine(String.Format("Manufacturer = {0}", manufacturer));
                }
                _data.SaveChanges();
            });
        }


        private static void CleanupUPCs(BeverageMonitorEntities _data)
        {
            Log.Time("Cleanup UPCs", LogType.Debug, false, () =>
            {
                var parLevels = new List<Guid>();

                var count = _data.UPCs.Count();

                var upcs = (from m in _data.UPCs
                                     select m.ItemNumber).Distinct();

                Console.WriteLine(string.Format("{1} UPC Count: {0}", upcs.Count(), count));
                foreach (var upc in upcs)
                {
                    var copies = from m in _data.UPCs
                                 where m.ItemNumber == upc
                                 select m;

                    var first = copies.First();

                    foreach (var copy in copies)
                    {
                        if (copy.UPCID != first.UPCID )
                        {
                            var inventories = from i in _data.Inventories
                                              where i.UPCID == copy.UPCID
                                              select i;
                            foreach (var inventory in inventories)
                            {
                                inventory.UPCID = first.UPCID;
                            }

                            var pours = from i in _data.Pours
                                              where i.UPCID == copy.UPCID
                                              select i;
                            foreach (var pour in pours)
                            {
                                pour.UPCID = first.UPCID;
                            }

                            var pars = from i in _data.ParLevels
                                        where i.UPCID == copy.UPCID
                                        select i;
                            foreach (var par in pars)
                            {
                                parLevels.Add( par.ParLevelID );
                            }
                            _data.DeleteObject(copy);
                        }
                    }
//                    Console.WriteLine(String.Format("UPCs = {0}", upc));

                }
                if (0 < parLevels.Count)
                {
                    var connstring = ConfigurationManager.ConnectionStrings["BeverageMonitor"].ConnectionString;
                    using (var db = new SqlTool(connstring))
                    {
                        StringBuilder sql = new StringBuilder("DELETE FROM ParLevels WHERE ParLevelID IN (");
                        foreach (var parLevel in parLevels)
                        {
                            sql.Append(string.Format("'{0}',", parLevel));
                        }
                        sql.Remove(sql.Length - 1, 1);
                        sql.Append(")");

                        db.Execute(sql.ToString());
                    }
                }
                _data.SaveChanges();

            });
        }
    }
}
