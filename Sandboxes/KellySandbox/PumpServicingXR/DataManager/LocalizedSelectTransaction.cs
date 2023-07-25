using System;
using System.Collections.Generic;

#if PocketPC

#endif

namespace LFI.Sync.DataManager
{
    /// <summary>
    /// Transaction that will localized 'strings' added as columns using the AddTranslatedColumn methods.
    /// </summary>
    /// <typeparam name="T">
    /// Object type returned by the transaction.
    /// </typeparam>
    public class LocalizedSelectTransaction : LocalizedTransaction<Dictionary<string, object>>
    {
        private string where = String.Empty;

        //----------------------------------------------------------------------
        public LocalizedSelectTransaction( string tableName )
            : base( tableName, String.Empty )
        {
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedSelectTransaction"/> class.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="selectColumns">The select columns.</param>
        public LocalizedSelectTransaction( string tableName, List<string> selectColumns )
            : base( tableName, String.Empty )
        {
            AddSelectColumns( selectColumns, IDColumn );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedSelectTransaction"/> class.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="selectColumns">The select columns.</param>
        /// <param name="idColumn">The id column.</param>
        public LocalizedSelectTransaction( string tableName, List<string> selectColumns, string idColumn )
            : base( tableName, idColumn )
        {
            AddSelectColumns( selectColumns, idColumn );
        }


        //----------------------------------------------------------------------
        public void AddSelectColumn( string colName )
        {
            AddColumn( colName );
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Adds the select columns.
        /// </summary>
        /// <param name="columns">The columns.</param>
        /// <param name="idColumn">The id column.</param>
        public void AddSelectColumns( List<string> columns, string idColumn )
        {
            foreach( string column in columns )
            {
                if( string.Compare( column, "Name", true ) == 0 )
                {
                    AddTranslatedColumn( column, idColumn );
                }
                else
                {
                    AddColumn( column );
                }
            }
        }

        //----------------------------------------------------------------------
        public override Dictionary<string, object> BuildFromReader( TransactionReader reader )
        {
            Dictionary<string, object> outList = new Dictionary<string, object>( );

            int fieldCount = reader.GetColumnCount( );
            for( int i = 0; i < fieldCount; ++i )
            {
                if( !outList.ContainsKey( reader.GetColumnName( i ) ) )
                {
                    outList.Add( reader.GetColumnName( i ), reader.GetColumnValue( i ) );
                }
            }

            return outList;
        }

        //----------------------------------------------------------------------
        public void SetWhereSQL( string where )
        {
            this.where = where;
        }

        //----------------------------------------------------------------------
        protected override string BuildWhere( )
        {
            if( where == String.Empty )
                return base.BuildWhere( );

            return where;
        }
    }
}