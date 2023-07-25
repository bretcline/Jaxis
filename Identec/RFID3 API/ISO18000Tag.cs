using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using IDENTEC.UdbElements;
using System.Diagnostics;
using IDENTEC.Protocol;

namespace IDENTEC
{
    namespace Tags
    {
        public class ISO18000Tag : IComparable
        {

            private const int REFERENCE_TABLE_ID = 0xD000; //directory table
            private const int REFERENCE_TABLE_RECORD_SIZE = 35;
            private const int REFERENCE_TABLE_TABLE_ID_INDEX = 0;
            private const int REFERENCE_TABLE_TABLE_ID_SIZE = 2;
            private const int REFERENCE_TABLE_COLUMN_STRUCTURE_INDEX = 3;
            private const int REFERENCE_TABLE_COLUMN_STRUCTURE_SIZE = 32;
            private const int REFERENCE_TABLE_COLUMN_COUNT_INDEX = 2;
            private const int REFERENCE_TABLE_COLUMN_COUNT_SIZE = 1;

            private static byte sequenceIDAddRecords = 0x00;
            private int MAX_WRITE_BYTES = 210;
            private int MAX_READ_BYTES = 46;
            private System.UInt32 m_dwTagID;
            private System.UInt16 m_ManufacturerID;
            public byte[] m_RoutingCode;
            public byte m_RoutingCodeType;
            public byte[] m_UserID;
            public byte m_UserIDType;
            internal System.DateTime _lastTimeSeenAwake = DateTime.Now; // used to keep the last time tag was woken up
            private System.Collections.Generic.Dictionary<int, int> m_SignalStrength = new Dictionary<int, int>();

            /// <summary>
            /// The invalid signal strength value. If tag communications fails, the tag signal strength will be this value.
            /// </summary>
            public const int InvalidSignal = -128;

            public UInt32 ID
            {
                get
                {
                    return m_dwTagID;
                }
                set
                {
                    if (value != 0)
                        m_dwTagID = value;
                    else
                        throw new ArgumentOutOfRangeException("value");
                }
            }
            public void setRSSI(int antenna, int level)
            {
                if (m_SignalStrength.ContainsKey(antenna))
                {
                    m_SignalStrength[antenna] = level;
                }
                else
                    m_SignalStrength.Add(antenna, level);
            }

            public UInt16 ManufacturerID
            {
                get
                {
                    return m_ManufacturerID;
                }
                set
                {
                    if (value != 0)
                        m_ManufacturerID = value;
                    else
                        throw new ArgumentOutOfRangeException("value");
                }
            }
            public System.DateTime LastTimeAwake
            {
                get
                {
                    return _lastTimeSeenAwake;
                }
                set
                {
                    _lastTimeSeenAwake = value;
                }
            }

            #region IComparable Members

            /// <summary>
            /// 
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public int CompareTo(object obj)
            {
                ISO18000Tag tag = obj as ISO18000Tag;
                if (null != tag)
                {
                    return m_dwTagID.CompareTo(tag.ID);
                }
                else
                    throw new ArgumentException("The tag can only be compared to a tag object");

                //return 0;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override bool Equals(object obj)
            {
                ISO18000Tag tag = obj as ISO18000Tag;
                if (null != tag)
                {
                    return m_dwTagID.Equals(tag.ID);
                }
                else
                    throw new ArgumentException("The tag can only be compared to a tag object");
            }

            // Omitting getHashCode violates rule: OverrideGetHashCodeOnOverridingEquals.
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                return this.m_dwTagID.GetHashCode();
            }


            #endregion

            /// <summary>
            /// A method to get the signal strength from the specified antenna.
            /// </summary>
            /// <param name="antenna">The antenna number for the specified signal.</param>
            /// <returns>The relative signal strength as calculated by the reader.</returns>
            /// <remarks>This method is only valid for tags detected on readers with multiple antennas.</remarks>
            public int GetSignalStrength(int antenna)
            {
                int value;
                //Check to see if the tag was detected on a reader with multiple antennas:
                if (null == m_SignalStrength)
                    return InvalidSignal;

                if (!m_SignalStrength.TryGetValue(antenna, out value))
                    throw new ArgumentOutOfRangeException("antenna");
                return value;
            }

            public Boolean Sleep(IDENTEC.Readers.iPortMCI reader)
            {

                return reader.Sleep(this);
            }

            public Boolean ReadRoutingCode(IDENTEC.Readers.iPortMCI reader, out byte[] routingCode)
            {
                this.LastTimeAwake = DateTime.Now;
                return reader.ReadRoutingCode(this, out routingCode);
            }

            public Boolean WriteRoutingCode(IDENTEC.Readers.iPortMCI reader, byte code_length, ref byte[] routingCode)
            {
                this.LastTimeAwake = DateTime.Now;
                return reader.WriteRoutingCode(this, code_length, ref routingCode);
            }

            public Boolean ReadUDB(IDENTEC.Readers.iPortMCI reader, out byte[] UDB)
            {
                return reader.ReadUDB(this, out UDB);
            }

            public Boolean ReadUDB(IDENTEC.Readers.iPortMCI reader, byte udbType, byte maxPackageLength, out ArrayList udbElements)
            {
                byte[] UDB = null;
                int totalUdbLength;
                int udbBytes = 0;
                ArrayList UDBArray = new ArrayList();
                Boolean done = false;

                while (!done)
                {
                    Boolean result = reader.ReadUDB(this, udbType, maxPackageLength, udbBytes, out UDB, out totalUdbLength);
                    Debug.WriteLine("udbBytes: " + udbBytes);
                    if (result) this.LastTimeAwake = DateTime.Now;

                    if (UDB != null && result)
                    {
                        udbBytes += UDB.Length;
                        UDBArray.Add(UDB);
                    }

                    if (!result || (totalUdbLength == udbBytes) || (UDB == null) || (UDB.Length == 0)) done = true;
                    //else System.Threading.Thread.Sleep(1000);
         
                }
                Debug.WriteLine("udbLength: " + UDBArray.Count);
                udbElements = parseUDB(UDBArray);

                return true;

            }


            public virtual ArrayList parseUDB(ArrayList uMessages)
            {
                 ArrayList udbElements = new System.Collections.ArrayList();
                /** @todo Need to read the default max packet size from a config file
                *   Setting it to 40 bytes for now... */
                /** @todo Also need to ask Mike and Bruce what the type is all about */
                // here we need to send the command to retrieve the UDB from the tag
                
                
                // collect udb array
                    //this.mTagDockProtocol.readTagUDB(tag, udbType, 0, 50);
                
                if (uMessages != null && uMessages.Count > 0)
                {
                    // need to parse out each of the messages i.e. get payloads and then parse the payloads out and get the UDB info
                    /** @todo Check with Bruce to make sure this is returning the right stuff */
                    byte[] tagMessagesData = new byte[0];
                    for (int i = 0; i < uMessages.Count; i++)
                    {
                        byte[] tagMsgPayload = (byte[])uMessages[i]; //put payload here
                        if (tagMsgPayload != null)
                        {
                            byte[] tmpMsgData = new byte[tagMessagesData.Length + tagMsgPayload.Length];
                            Array.Copy(tagMessagesData, tmpMsgData, tagMessagesData.Length);
                            Array.Copy(tagMsgPayload, 0, tmpMsgData, tagMessagesData.Length, tagMsgPayload.Length);
                            tagMessagesData = tmpMsgData;
                        }
                    }
                    byte[] byteData = tagMessagesData;
                    Debug.WriteLine("UDB Lenght: " + byteData.Length);
                    //System.out.println("Data Length: " + Integer.toString(byteData.length));
                    bool keepProcessing = true;
                    int currentIndex = 0;
                    System.String udbString = new System.String(SupportClass.ToCharArray(byteData));
                    while (keepProcessing)
                    {
                        //System.out.println(currentIndex);
                        int curElementType = (int)(byteData[currentIndex++] & 0xFF);
                        //System.out.println(curElementType);
                        int curElementLength = (int)(byteData[currentIndex++] & 0xFF);
                        //System.out.println(curElementLength);
                        //System.out.println("next");
                        /** @todo these numeric values should be constants */
                        if (curElementType == 16)
                        {
                            // this is the routing code
                            System.String routingCode = udbString.Substring(currentIndex, (currentIndex + curElementLength) - (currentIndex));
                            // now create a routing code element
                            DoDRoutingCodeElement rce = new DoDRoutingCodeElement(routingCode);
                            // add the element to the list
                            udbElements.Add(rce);
                            currentIndex = currentIndex + curElementLength;
                        }
                        else if (curElementType == 17)
                        {
                            // this is the user id
                            System.String userID = udbString.Substring(currentIndex, (currentIndex + curElementLength) - (currentIndex));
                            // now create a user ID element
                            DoDUserIDElement uide = new DoDUserIDElement(userID);
                            // add the element to the list
                            udbElements.Add(uide);
                            currentIndex = currentIndex + curElementLength;
                        }
                        else if (curElementType == 18)
                        {
                            // this is the optional command list
                            System.String optionalCommand = udbString.Substring(currentIndex, (currentIndex + curElementLength) - (currentIndex));
                            // now create a optional command list element
                            DoDOptionalCommandListElement ocle = new DoDOptionalCommandListElement(optionalCommand);
                            // add the element to the list
                            udbElements.Add(ocle);
                            currentIndex = currentIndex + curElementLength;
                        }
                        else if (curElementType == 19)
                        {
                            // this is the memory size element
                            System.String memorySize = udbString.Substring(currentIndex, (currentIndex + curElementLength) - (currentIndex));
                            // now create a memory size element
                            DoDMemorySizeElement mse = new DoDMemorySizeElement(memorySize);
                            // add the element to the list
                            udbElements.Add(mse);
                            currentIndex = currentIndex + curElementLength;
                        }
                        else if (curElementType == 20)
                        {
                            // this is the table query element
                            System.String tableQuerySize = udbString.Substring(currentIndex, (currentIndex + curElementLength) - (currentIndex));
                            // now create a table query element
                            DoDTableQuerySizeElement tqse = new DoDTableQuerySizeElement(tableQuerySize);
                            // add the element to the list
                            udbElements.Add(tqse);
                            currentIndex = currentIndex + curElementLength;
                        }
                        else if (curElementType == 21)
                        {
                            // this is the table query results
                            System.String tableQueryResults = udbString.Substring(currentIndex, (currentIndex + curElementLength) - (currentIndex));
                            // now create a table query results
                            DoDTableQueryResultsElement tqre = new DoDTableQueryResultsElement(tableQueryResults);
                            // add the element to the list
                            udbElements.Add(tqre);
                            currentIndex = currentIndex + curElementLength;
                        }
                        else if (curElementType == 22)
                        {
                            // this is the hardware fault
                            System.String hardwareFaultStatus = udbString.Substring(currentIndex, (currentIndex + curElementLength) - (currentIndex));
                            // now create a hardware fault
                            DoDHardwareFaultStatusElement hfse = new DoDHardwareFaultStatusElement(hardwareFaultStatus);
                            // add the element to the list
                            udbElements.Add(hfse);
                            currentIndex = currentIndex + curElementLength;
                        }
                        else if (curElementType == 0xFF)
                        {
                            // this is the Application Extension Element
                            System.String applicationExtension = udbString.Substring(currentIndex, (currentIndex + curElementLength - 2) - (currentIndex));
                            // now create a Application Extension Element
                            DoDApplicationExtensionElement aee = new DoDApplicationExtensionElement(applicationExtension);
                            // add the element to the list
                            udbElements.Add(aee);
                            currentIndex = currentIndex + curElementLength;
                        }

                        if (currentIndex >= (udbString.Length - 1))
                        {
                            keepProcessing = false;
                        }
                    }
                }
                return udbElements;
            }
         


            public Boolean ReadMemory(IDENTEC.Readers.iPortMCI reader, int address, byte nb_bytes, out byte[] dataRead)
            {
                return reader.ReadMemory(this, address, nb_bytes, out dataRead);
            }

            public Boolean WriteMemory(IDENTEC.Readers.iPortMCI reader, int address, byte nb_bytes, ref byte[] data)
            {
                return reader.WriteMemory(this, address, nb_bytes, ref data);
            }

            public Boolean BeeperControl(IDENTEC.Readers.iPortMCI reader, Boolean BeeperON)
            {
                return reader.BeeperControl(this, BeeperON);
            }

            public Boolean DeleteWriteableMemory(IDENTEC.Readers.iPortMCI reader)
            {
                return reader.DeleteWriteableMemory(this);
            }


            public Boolean TableCreate(IDENTEC.Readers.iPortMCI reader, int tableID, int maxRecords, int nbFields, byte[] fieldLengths)
            {
                if ((nbFields > 32) || (nbFields < 1))
                    throw new ArgumentOutOfRangeException("nbFields", "should be >1 and <32");
                if (fieldLengths.Length < nbFields)
                    throw new ArgumentOutOfRangeException("fieldLengths is less than nbFields");
                byte[] data;
                data = new byte[5 + nbFields];
                data[0] = (byte)(tableID & 0xFF);
                data[1] = (byte)((tableID >> 8) & 0xFF);
                data[2] = (byte)(maxRecords & 0xFF);
                data[3] = (byte)((maxRecords >> 8) & 0xFF);
                data[4] = (byte) (nbFields & 0xFF);
                Array.Copy(fieldLengths, 0, data, 5, nbFields);
                this.LastTimeAwake = DateTime.Now;
                return reader.DataBase(this, IDENTEC.Readers.iPortMCI.DatabaseSubCmd.CreateTable, data);

            }

            public Boolean TableGetProperties(IDENTEC.Readers.iPortMCI reader, int tableID, out int nbRecords, out int MaxRecords)
            {
                byte[] response = null;
                byte[] data = new byte[2];
                data[0] = (byte)(tableID & 0xFF);
                data[1] = (byte)((tableID >> 8) & 0xFF);
                nbRecords = 0;
                MaxRecords = 0;
                Boolean ret = reader.DataBase(this, IDENTEC.Readers.iPortMCI.DatabaseSubCmd.GetProperty, data, out response);
                if (ret) this.LastTimeAwake = DateTime.Now;
                if (ret)
                {
                    nbRecords = response[3] + (response[4] << 8);
                    MaxRecords = response[5] + (response[6] << 8);
                }
                return ret;
            }

            public Boolean TableAddRecords(IDENTEC.Readers.iPortMCI reader, int tableID, int nbRecords, byte[] recordData)
            {
                byte[] data;
                data = new byte[5];
                data[0] = (byte)(tableID & 0xFF);
                data[1] = (byte)((tableID >> 8) & 0xFF);
                data[2] = sequenceIDAddRecords++; // sequence ID
                data[3] = (byte)(nbRecords & 0xFF);
                data[4] = (byte)((nbRecords >> 8) & 0xFF);
                this.LastTimeAwake = DateTime.Now;

                byte[] response = null;
                MessageToken token = null;
                Boolean status = reader.DataBase(this, IDENTEC.Readers.iPortMCI.DatabaseSubCmd.AddRecords, data, out response);
                if (status)
                {
                    int messageTokenLength = response[3];
                    byte[] messageToken = new byte[messageTokenLength + 1];
                    Array.Copy(response, 3, messageToken, 0, messageToken.Length);
                    token = new MessageToken(messageToken);
                    for (int i = 0; i < recordData.Length; i += MAX_WRITE_BYTES)
                    {
                        byte[] writeData;
                        if (recordData.Length - i > MAX_WRITE_BYTES)
                        {
                            writeData = new byte[MAX_WRITE_BYTES];
                            Array.Copy(recordData, i, writeData,0, MAX_WRITE_BYTES);
                        }
                        else
                        {
                            writeData = new byte[recordData.Length - i];
                            Array.Copy(recordData, i, writeData, 0, recordData.Length - i);
                            
                        }
                        //System.out.println(writeData);
                        int index = 0;
                        data = new byte[2+token.TokenLength+writeData.Length];
                        Array.Copy(token.getToken(),0,data,0,token.TokenLength+1);
                        data[token.TokenLength+1] = (byte)((writeData.Length) & 0xFF);
                        Array.Copy(writeData,0,data,2+token.TokenLength,writeData.Length);

                        this.LastTimeAwake = DateTime.Now;
         
                        status = reader.DataBase(this, IDENTEC.Readers.iPortMCI.DatabaseSubCmd.WriteFragment, data, out response);
                        if(status)
                        token = new MessageToken(response);

                       
                    }


                }

                return status;

            }

            public Boolean TableGetData(IDENTEC.Readers.iPortMCI reader, int tableID, int recordNumber, int fieldNumber, out MessageToken readToken)
            {
                readToken = null;
                byte[] response = null;
                byte[] data = new byte[5];
                data[0] = (byte)(tableID & 0xFF);
                data[1] = (byte)((tableID >> 8) & 0xFF);
                data[2] = (byte)(recordNumber & 0xFF);
                data[3] = (byte)((recordNumber >> 8)& 0xFF);
                data[4] = (byte)(fieldNumber & 0xFF);
               
                Boolean ret = reader.DataBase(this, IDENTEC.Readers.iPortMCI.DatabaseSubCmd.GetData, data, out response);
                if (ret) this.LastTimeAwake = DateTime.Now;
                if (ret)
                {
                    int messageTokenLength = response[3];
                    byte[] messageToken = new byte[messageTokenLength + 1];
                    Array.Copy(response, 3, messageToken, 0, messageToken.Length);
                    readToken = new MessageToken(messageToken);                  
                }
                return ret;
            }


            public Boolean TableReadFragment(IDENTEC.Readers.iPortMCI reader, int readLength, out byte[] byteData, MessageToken inToken,  out MessageToken readToken)
            {
                byteData = null;
                readToken = null;
                byte[] response = null;
                byte[] data = new byte[2 + inToken.TokenLength];
                Array.Copy(inToken.getToken(), 0, data, 0, inToken.TokenLength + 1);
                data[data.Length-1] = (byte)(readLength & 0xFF);
                Boolean ret = reader.DataBase(this, IDENTEC.Readers.iPortMCI.DatabaseSubCmd.ReadFragment, data, out response);

                if (ret)
                {
                    int messageTokenLength = response[3];
                    byte[] messageToken = new byte[messageTokenLength + 1];
                    Array.Copy(response, 3, messageToken, 0, messageToken.Length);
                    readToken = new MessageToken(messageToken);

                    int tokenLength = messageToken.Length;
                    if (tokenLength == 0) tokenLength = 1;

                    int responseIndex = 3 + tokenLength;
                    int responseLength = response[responseIndex];

                    byteData = new byte[responseLength];
                    Array.Copy(response, responseIndex+1, byteData, 0, responseLength);


                }



                return ret;
            }

            public Boolean TableReadRecords(IDENTEC.Readers.iPortMCI reader, int tableID, int recordNumber,
                                            int nbBytes, out byte[] tableData)
            {
                MessageToken oldToken = null;
                MessageToken newToken = null;
                tableData = new byte[nbBytes];
                int index = 0;
                Boolean success = this.TableGetData(reader, tableID, recordNumber, 0, out oldToken);
                Boolean done = false;
                
                while (success && !done)
                {
                    int readByteCount = nbBytes - index ;
                    if (readByteCount > MAX_READ_BYTES) readByteCount = MAX_READ_BYTES;
                    byte[] byteFragement = null;
                    success = this.TableReadFragment(reader, readByteCount, out byteFragement, oldToken, out newToken);
                    if (success)
                    {
                        int dataLength = byteFragement.Length;
                        if (dataLength > tableData.Length - index) dataLength = tableData.Length - index;

                        Array.Copy(byteFragement, 0, tableData, index, dataLength);
                        index += dataLength;
                        if (newToken == null || newToken.TokenLength == 0 || index == nbBytes)
                            done = true;
                        else oldToken = newToken;
                    }

                    

                }

                return success;

            }



            public Boolean TableReadAll(IDENTEC.Readers.iPortMCI reader, int tableID, out byte[] tableData)
            {
                tableData = null; 
                // read the directory table

                int nbDirRecords = 0;
                int maxDirRecords = 0;
                Boolean ret = this.TableGetProperties(reader, REFERENCE_TABLE_ID, out nbDirRecords, out maxDirRecords);

                byte[] refTableData = null;
                ret = this.TableReadRecords(reader, REFERENCE_TABLE_ID, 0, REFERENCE_TABLE_RECORD_SIZE * nbDirRecords, out refTableData);

                // parse the directory table
                for (int i = 0; i < nbDirRecords; i++)
                {
                    byte[] rowRecord = new byte[REFERENCE_TABLE_RECORD_SIZE];
                    Array.Copy(refTableData, i * REFERENCE_TABLE_RECORD_SIZE, rowRecord, 0, REFERENCE_TABLE_RECORD_SIZE);


                    byte[] tableIDByte = new byte[REFERENCE_TABLE_TABLE_ID_SIZE];
                    Array.Copy(rowRecord, REFERENCE_TABLE_TABLE_ID_INDEX, tableIDByte, 0, REFERENCE_TABLE_TABLE_ID_SIZE);
                    int refTableID = (int) (tableIDByte[0] & 0xFF);
                    refTableID += 256 * (int)(tableIDByte[1] & 0xFF);
                    if (tableID == refTableID)
                    {
                        // we found the record we've been looking for!
                        int recordCount = (int)rowRecord[REFERENCE_TABLE_COLUMN_COUNT_INDEX];

                        int rowSize = 0;

                        for (int j = 0; j < recordCount; j++)
                        {
                            rowSize += rowRecord[REFERENCE_TABLE_COLUMN_STRUCTURE_INDEX + j];
                        }

                        int nbRecords = 0;
                        int maxRecords = 0;
                        ret = this.TableGetProperties(reader, tableID, out nbRecords, out maxRecords);
                        if(ret && nbRecords > 0)
                           ret = this.TableReadRecords(reader, tableID, 0, rowSize * nbRecords, out tableData);

                    }

                }




                return ret;
            }




            public Boolean TableUpdateRecords(IDENTEC.Readers.iPortMCI reader, int tableID, int startRecordID, int nbRecords, byte[] recordsData)
            {
                return false;
            }

            public Boolean TableDeleteRecord(IDENTEC.Readers.iPortMCI reader, int tableID, int RecordID)
            {
                return false;
            }

            public Boolean TableUpdateFields(IDENTEC.Readers.iPortMCI reader, int tableID, int recordID, byte StartFieldID, byte nbFields, byte[] recordsData)
            {
                return false;
            }

            public Boolean TableGetData(IDENTEC.Readers.iPortMCI reader, int tableID, int recordID, byte StartFieldID, int nbByte, out byte[] Data)
            {
                Data = null;
                return false;
            }

        }
    }
}

