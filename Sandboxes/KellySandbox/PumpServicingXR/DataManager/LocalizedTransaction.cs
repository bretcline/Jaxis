using System;
using System.Collections.Generic;
using System.Globalization;

namespace LFI.Sync.DataManager
{
    /// <summary>
    /// Transaction that will localized 'strings' added as columns using the AddTranslatedColumn methods.
    /// </summary>
    /// <typeparam name="T">
    /// Object type returned by the transaction.
    /// </typeparam>
    public abstract class LocalizedTransaction<T> : BaseTransaction<T>
    {
        #region Data Members

        /// <summary>
        /// Statement used when creating the localization query parameters
        /// </summary>
        private const string TranslationCASE =
            @",
            CASE 
                WHEN {4}.[DataRefID] = {0} THEN
                    CASE 
                        WHEN {4}.Translation = '' THEN {2}
                        WHEN {4}.Property = '{1}' THEN {4}.Translation
                    END
                        ELSE {2}
                END AS [{3}] ";

        private int _cultureLCID = 1033;

        private int _columnIndex = 0;
        //----------------------------------------------------------------------
        /// <summary>
        /// Dictionary indexed by a column alias and the corresponding localized group
        /// </summary>
        private readonly Dictionary<string, LocalizedColumnGroup> _translatedColGroups = new Dictionary<string, LocalizedColumnGroup>( 5 );

        #endregion

        #region Constructors

        //----------------------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizedTransaction{T}"/> class.
        /// </summary>
        /// <param name="culture">The culture.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="idColumn">The id column.</param>
        protected LocalizedTransaction( CultureInfo culture, string tableName, string idColumn )
            : base( tableName, idColumn )
        {
            if( culture == null ) throw new ArgumentNullException( "culture" );
            if( tableName == null ) throw new ArgumentNullException( "tableName" );
            if( idColumn == null ) throw new ArgumentNullException( "idColumn" );

            _cultureLCID = culture.LCID;
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Runs the provided stored procedure.
        /// </summary>
        /// <param name="storedProcedureName">The name of the stored procedure to execute.</param>
        protected LocalizedTransaction( string storedProcedureName )
            : base( storedProcedureName )
        {
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// INSERT/UPDATE constructor. INSERT/UPDATE data are taken from the provided IBaseData object.
        /// </summary>
        /// <param name="transactionObj">The object to be inserted/updated.</param>
        /// <param name="tableName">The name of the target table in the database.</param>
        /// <param name="idColumn">The name of the primary key column in the table. If the table does not have a primary key, any column with unique data will work.</param>
        protected LocalizedTransaction( IBaseData transactionObj, string tableName, string idColumn )
            : base( transactionObj, tableName, idColumn )
        {
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Basic SELECT query constructor.
        /// </summary>
        /// <param name="tableName">The name of the target table in the database.</param>
        /// <param name="idColumn">The name of the primary key column in the table. If the table does not have a primary key, any column with data will work.</param>
        protected LocalizedTransaction( string tableName, string idColumn )
            : base( tableName, idColumn )
        {
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Constuctor for SELECTing all results after a given date.
        /// </summary>
        /// <param name="tableName">The name of the target table in the database.</param>
        /// <param name="idColumn">The name of the primary key column in the table. If the table does not have a primary key, any column with data will work.</param>
        /// <param name="lastUpdated">All results after (but not including) this date will be returned.</param>
        /// <param name="updateColumn">Name of the column that holds the last modified date in the table.</param>
        protected LocalizedTransaction( string tableName, string idColumn, DateTime lastUpdated, string updateColumn )
            : base( tableName, idColumn, lastUpdated, updateColumn )
        {
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Constructor for SELECTing a single row by its primary key.
        /// </summary>
        /// <param name="tableName">The name of the target table in the database.</param>
        /// <param name="idColumn">The name of the primary key column in the table. If the table does not have a primary key, any column with data will work.</param>
        /// <param name="primaryKey">Primary key of the row to return.</param>
        protected LocalizedTransaction( string tableName, string idColumn, string primaryKey )
            : base( tableName, idColumn, primaryKey )
        {
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Constructor for SELECTing results with pagination. EX: Returns results 1-499 for (PageIndex 0, PageSize 500) and results 500-999 for (PageIndex 1, PageSize 500).
        /// </summary>
        /// <param name="tableName">The name of the target table in the database.</param>
        /// <param name="idColumn">The name of the primary key column in the table. If the table does not have a primary key, any column with data will work.</param>
        /// <param name="pageIndex">The index of the page to return.</param>
        /// <param name="pageSize">The number of results in the given page.</param>
        protected LocalizedTransaction( string tableName, string idColumn, int pageIndex, int pageSize )
            : base( tableName, idColumn, pageIndex, pageSize )
        {
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Constructor for SELECTing all results after the provided date with pagination. EX: Returns results 1-499 for (PageIndex 0, PageSize 500) and results 500-999 for (PageIndex 1, PageSize 500).
        /// </summary>
        /// <param name="tableName">The name of the target table in the database.</param>
        /// <param name="idColumn">The name of the primary key column in the table. If the table does not have a primary key, any column with data will work.</param>
        /// <param name="pageIndex">The index of the page to return.</param>
        /// <param name="pageSize">The number of results in the given page.</param>
        /// <param name="lastUpdated">All results after (but not including) this date will be returned.</param>
        /// <param name="updateColumn">Name of the column that holds the last modified date in the table.</param>
        protected LocalizedTransaction( string tableName, string idColumn, int pageIndex, int pageSize, DateTime lastUpdated, string updateColumn )
            : base( tableName,
                   idColumn, pageIndex, pageSize, lastUpdated, updateColumn )
        {
        }

        #endregion

        //----------------------------------------------------------------------
        /// <summary>
        /// Creates the translation CASE.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <returns></returns>
        private string TranslationColumn( LocalizedColumnGroup group )
        {
            return string.Format( TranslationCASE,
                                 GetColumnFormat( group.ForeignColumnID, false ),
                                 group.PropertyName,
                                 GetColumnFormat( group.ForeignColumnName, false ),
                                 group.SourceColumnName,
                                 GetLocalizedAlias( group.UniqueColumnID ).Replace( '.', '_' ) );
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Gets the localized alias.
        /// </summary>
        /// <param name="columnID">The column ID.</param>
        /// <returns></returns>
        private static string GetLocalizedAlias( string columnID )
        {
            return string.Format( "{0}Localization", columnID );
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Adds the translated column (this method should only be used if the translated item, belongs to the table selected from)
        /// </summary>
        /// <param name="sourceColumnName">The name.</param>
        /// <param name="propertyName">Name of the property.</param>
        protected void AddTranslatedColumn( string sourceColumnName, string propertyName )
        {
            AddTranslatedColumn( sourceColumnName, IDColumn, sourceColumnName, IDColumn, Table, propertyName );
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Adds a column to be selected in the SQL. Columns will be prefixed in the SQL with their table name and a unique number to allow for multiple joins to the same table.
        /// This method can be used for a single table query or for joining to other Transactions (using the AddTransaction function).
        /// </summary>
        /// <param name="sourceColumnName">Name of the source column.</param>
        /// <param name="sourceColumnID">The source column ID.</param>
        /// <param name="foreignColumnName">Name of the foreign column.</param>
        /// <param name="foreignColumnID">The foreign column ID.</param>
        /// <param name="foreignTableName">Name of the foreign table.</param>
        /// <param name="propertyName">Name of the property.</param>
        protected void AddTranslatedColumn( string sourceColumnName, string sourceColumnID, string foreignColumnName, string foreignColumnID, string foreignTableName, string propertyName )
        {
            LocalizedColumnGroup simpleGroup = new LocalizedColumnGroup(
                sourceColumnName,
                sourceColumnID,
                foreignTableName,
                foreignColumnName,
                foreignColumnID,
                propertyName,
                _columnIndex++ );

            SelectColumns += TranslationColumn( simpleGroup );

            // store column for addition of inner joins
            _translatedColGroups.Add( sourceColumnName, simpleGroup );

            return;
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Override this function to MANUALLY build a join string.
        /// </summary>
        /// <returns>
        /// Nothing by default. If overridden, returns manually built SQL join string.
        /// </returns>
        public override string BuildJoins( )
        {
            return BuildJoins( IDColumn );
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Override this function to MANUALLY build a join string.
        /// </summary>
        /// <param name="primaryID">The primary ID.</param>
        /// <returns>
        /// Nothing by default. If overridden, returns manually built SQL join string.
        /// </returns>
        public string BuildJoins( string primaryID )
        {
            // Add the localization lookup to the join statement of query
            string innerJoin = string.Empty;// "\n LEFT OUTER JOIN Localization on Localization.DataRefID = " + Table + "." + primaryID;

            foreach( LocalizedColumnGroup group in _translatedColGroups.Values )
            {
                string[ ] aliasSplit = group.ForeignColumnID.Split( new[ ] { '.' } );
                string fColumnAlias = ( aliasSplit.Length > 1 ) ? aliasSplit[ 1 ] : aliasSplit[ 0 ];

                innerJoin += string.Format( "\n LEFT OUTER JOIN Localization as {0} ON {0}.DataRefID = {1}.{2} ",
                    GetLocalizedAlias( group.UniqueColumnID ).Replace( '.', '_' ),
                    group.ForeignTableName, fColumnAlias );
                innerJoin += string.Format( "\n AND ({0}.LCID = {1}) AND {0}.Property = '{2}' ",
                    GetLocalizedAlias( group.UniqueColumnID ).Replace( '.', '_' ),
                    _cultureLCID,
                    group.PropertyName );
            }

            return innerJoin;
        }

        //----------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        /// <value>The culture.</value>
        public CultureInfo Culture
        {
            get { return new CultureInfo( _cultureLCID ); }
            set { _cultureLCID = value.LCID; }
        }
    }
}