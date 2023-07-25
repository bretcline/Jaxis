namespace ReceiverApp
{
    using PureRF;
    using System;
    using System.Data.OleDb;

    public class TagsOleDB
    {
        private OleDbConnection mConn = null;
        private string mDBName = "";
        private DB_Type mDBType = DB_Type.DB_INVALID;
        private string mTable = "Tags";

        public bool AddTag(ReceiversManager.ManagedReceiver receiver, ReceiverRetVal ret, Receiver.Tag tag)
        {
            string format = "INSERT INTO {0} (Receiver, Loop, UnitID, RetVal, TagID, TransmissionIndex, TagMsg, ActivatorNum, RSSI, ReceiverTS) VALUES ('{1}', '{2}', {3}, '{4}', {5}, {6}, '{7}', {8}, {9}, '{10}')";
            format = string.Format(format, new object[] { this.mTable, receiver.Name, receiver.Loop.Name, receiver.UnitID, ret.ToString(), tag.tagID.GetPureRFTagID(), tag.transmissionIndex, tag.tagMsg.ToString(), tag.activatorNum, tag.RSSI, tag.ts.ToString() });
            if (!this.IsOpened)
            {
                return false;
            }
            OleDbCommand command = new OleDbCommand(format, this.mConn);
            try
            {
                command.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void Close()
        {
            if (this.IsOpened)
            {
                this.mConn.Close();
                this.mConn = null;
                this.mDBName = "";
            }
        }

        public bool Open(string ConnStr, DB_Type ConnType)
        {
            this.Close();
            this.mConn = new OleDbConnection(ConnStr);
            try
            {
                this.mConn.Open();
            }
            catch
            {
                this.mConn = null;
                this.mDBType = DB_Type.DB_INVALID;
                return false;
            }
            this.mDBType = ConnType;
            return true;
        }

        public bool OpenAccess(string Filename)
        {
            if (!this.Open("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Filename, DB_Type.DB_ACCESS))
            {
                return false;
            }
            this.mDBName = Filename;
            this.mTable = "Tags";
            return true;
        }

        public bool OpenSQL(string ServerName, string DB, string Table)
        {
            string connStr = string.Format("Provider=SQLOLEDB;Data Source={0};Initial Catalog={1};Integrated Security=SSPI;", ServerName, DB);
            if (!this.Open(connStr, DB_Type.DB_SQL))
            {
                return false;
            }
            this.mDBName = Table + ":" + DB + "@" + ServerName;
            this.mTable = Table;
            return true;
        }

        public string DBName
        {
            get
            {
                return this.mDBName;
            }
        }

        public string DBType
        {
            get
            {
                switch (this.mDBType)
                {
                    case DB_Type.DB_ACCESS:
                        return "ACCESS";

                    case DB_Type.DB_SQL:
                        return "SQL";
                }
                return "INVALID";
            }
        }

        public bool IsOpened
        {
            get
            {
                return (this.mConn != null);
            }
        }

        public int NumRecords
        {
            get
            {
                if (!this.IsOpened)
                {
                    return 0;
                }
                OleDbCommand command = new OleDbCommand("SELECT COUNT(*) FROM " + this.mTable, this.mConn);
                return (int) command.ExecuteScalar();
            }
        }

        public enum DB_Type
        {
            DB_INVALID,
            DB_ACCESS,
            DB_SQL
        }
    }
}

