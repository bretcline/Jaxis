using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Objects;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Jaxis.JaxisSystem;
using JaxisExtensions;

namespace AuditImport
{
    public class BusinessLogic
    {
        class ExpectedFile : KeyValue<string, Action<string>>
        {
            public int Sequence { get; set; }
            public ExpectedFile( int _sequence, string _fileName, Action<string> _action )
                : base( _fileName, _action )
            {
                Sequence = _sequence;
            }
        }

        List<ExpectedFile> m_ExpectedFiles;

        public BindingList<string> Messages { get; private set; }

        public BusinessLogic( )
        {
            Messages = new BindingList<string>( );
            SetUpExpectedFiles( );
        }

        private void SetUpExpectedFiles( )
        {
            // For foreign key considerations, tables with foreign keys must be loaded after their related table(s).
            m_ExpectedFiles = new List<ExpectedFile>{ 
                new ExpectedFile( 0, "R_D_Dept.txt", ProcessRDDept ),  // Sales Department
                new ExpectedFile( 1, "R_L_Loc.txt", ProcessRLLoc ),    // Serving Location
                new ExpectedFile( 2, "R_V_Intv.txt", ProcessRVIntv ),  // Sales Interval
                
                new ExpectedFile( 3, "R_C_Cat.txt", ProcessRCCat ),    // Sales Category
                new ExpectedFile( 4, "R_S_Subc.txt", ProcessRSSubc ),  // Sales Subcategory
                new ExpectedFile( 5, "R_K_Item_SKU.txt", ProcessRKItemSKU ),   // Sales Items Configuration
                new ExpectedFile( 6, "R_I_Item.txt", ProcessRIItem ),  // Sales Items

                new ExpectedFile( 7, "R_E_Sale.txt", ProcessRESale ),  // Sales Mix With Check Type
                new ExpectedFile( 8, "R_M_Sale.txt", ProcessRMSale ),  // Sales Mix
                new ExpectedFile( 9, "R_P_Sale.txt", ProcessRPSale ),  // Sales Mix Other
                };
        }
    
        public void OnDirectorySelected( string _path )
        {
            var files = Directory.GetFiles( _path );
            var orderedFiles = from e in m_ExpectedFiles
                               join f in files on e.Key equals f.Substring( f.LastIndexOf( '\\' ) + 1 )
                               orderby e.Sequence
                               select new ExpectedFile( e.Sequence, f, e.Value );
            ProcessFiles( orderedFiles.ToList( ) );
        }
        
        private void ProcessFiles( List<ExpectedFile> _orderedFiles )
        {
            foreach ( var f in _orderedFiles )
            {
                f.Value( f.Key );
            }
            AddMessage( "Finished." );
        }

        private void ProcessRCCat( string _fileName )
        {
            var results = CommonProcess<SalesCategory>( _fileName, @"""(C)"",([\d]+),""([^""]+)"",([\d]+)", ( g ) => new SalesCategory
                { 
                    RecordType = g[ 1 ].Value,
                    RevenueCategoryId = Int32.Parse( g[ 2 ].Value ),
                    RevenueCategoryName = g[ 3 ].Value,
                    RevenueClassId = Int32.Parse( g[ 4 ].Value ),
                } );
            
            UpdateHelper<SalesCategory>( results, true );
        }

        private void ProcessRDDept( string _fileName )
        {
            var results = CommonProcess<SalesDepartment>( _fileName, @"""(D)"",([\d]+),""([^""]+)""", ( g ) => new SalesDepartment
            {
                RecordType = g[ 1 ].Value,
                RevenueClassId = Int32.Parse( g[ 2 ].Value ),
                RevenueClassName = g[ 3 ].Value
            } );

            UpdateHelper( results, true );
        }

        private void ProcessRESale( string _fileName )
        {
            var results = CommonProcess<SalesMixesWithCheckType>( _fileName, @"""(E)"",""([^""]+)"",([\d]+),([\d]+),([\d]+),([\d]+),([^,]+),([\d]+),([^,]+)", ( g ) => new SalesMixesWithCheckType
            {
                SalesMixWithCheckTypeId = Guid.NewGuid( ),
                RecordType = g[ 1 ].Value,
                Date = new DateTime( Int32.Parse( g[ 2 ].Value.Substring( 0, 4 ) ), Int32.Parse( g[ 2 ].Value.Substring( 4, 2 ) ), Int32.Parse( g[ 2 ].Value.Substring( 6, 2 ) ) ),
                ProfitCenterId = Int32.Parse( g[ 3 ].Value ),
                CheckTypeId = Int32.Parse( g[ 4 ].Value ),
                SalesIntervalId = Int32.Parse( g[ 5 ].Value ),
                MenuItemId = Int32.Parse( g[ 6 ].Value ),
                Price = Convert.ToDecimal( double.Parse( g[ 7 ].Value, System.Globalization.NumberStyles.Currency ) ),
                Count = Int32.Parse( g[ 8 ].Value ),
                ExtendedAmount = Convert.ToDecimal( double.Parse( g[ 9 ].Value, System.Globalization.NumberStyles.Currency ) )
            } );

            UpdateHelper<SalesMixesWithCheckType>( results, false );
        }

        private void ProcessRIItem( string _fileName )
        {
            var results = CommonProcess<SalesItem>( _fileName, @"""(I)"",([\d]+),""([^""]+)"",([\d]+)", ( g ) => new SalesItem
            {
                RecordType = g[ 1 ].Value,
                MenuItemId = Int32.Parse( g[ 2 ].Value ),
                MenuItemName = g[ 3 ].Value,
                ProductClassId = Int32.Parse( g[ 4 ].Value )
            } );

            UpdateHelper( results, true );
        }

        private void ProcessRKItemSKU( string _fileName )
        {
            // Note the *? in the expression:  the SKUIdentifier field is not required, and is sometimes not in the data stream.  When it is missing, however, the
            // preceding value (ProductClassId) is still followed by a comma.
            var results = CommonProcess<SalesItemsConfiguration>( _fileName, @"""(K)"",([\d]+),""([^""]+)"",([\d]+),""([^""]*?)""", ( g ) => new SalesItemsConfiguration
            {
                RecordType = g[ 1 ].Value,
                MenuItemId = Int32.Parse( g[ 2 ].Value ),
                MenuItemName = g[ 3 ].Value,
                ProductClassId = Int32.Parse( g[ 4 ].Value ),
                SKUIdentifier = g[ 5 ].Value
            } );

            UpdateHelper<SalesItemsConfiguration>( results, true );
        }

        private void ProcessRLLoc( string _fileName )
        {
            var results = CommonProcess<ServingLocation>( _fileName, @"""(L)"",([\d]+),""([^""]+)""", ( g ) => new ServingLocation
            {
                RecordType = g[ 1 ].Value,
                ProfitCenterId = Int32.Parse( g[ 2 ].Value ),
                ProfitCenterName = g[ 3 ].Value
            } );

            UpdateHelper<ServingLocation>( results, true );
        }

        private void ProcessRMSale( string _fileName )
        {
            var results = CommonProcess<SalesMix>( _fileName, @"""(M)"",""([^""]+)"",([\d]+),([\d]+),([\d]+),([^,]+),([\d]+),([^,]+)", ( g ) => new SalesMix
            {
                SalesMixId = Guid.NewGuid( ),
                RecordType = g[ 1 ].Value,
                Date = new DateTime( Int32.Parse( g[ 2 ].Value.Substring( 0, 4 ) ), Int32.Parse( g[ 2 ].Value.Substring( 4, 2 ) ), Int32.Parse( g[ 2 ].Value.Substring( 6, 2 ) ) ),
                ProfitCenterId = Int32.Parse( g[ 3 ].Value ),
                SalesIntervalId = Int32.Parse( g[ 4 ].Value ),
                MenuItemId = Int32.Parse( g[ 5 ].Value ),
                Price = Convert.ToDecimal( double.Parse( g[ 6 ].Value, System.Globalization.NumberStyles.Currency ) ),
                Count = Int32.Parse( g[ 7 ].Value ),
                ExtendedAmount = Convert.ToDecimal( double.Parse( g[ 8 ].Value, System.Globalization.NumberStyles.Currency ) )
            } );

            UpdateHelper<SalesMix>( results, false );
        }

        private void ProcessRPSale( string _fileName )
        {
            var results = CommonProcess<SalesMixesOther>( _fileName, @"""(P)"",""([^""]+)"",([\d]+),([\d]+),([\d]+),([\d]+),([^,]+),([\d]+),([^,]+)", ( g ) => new SalesMixesOther
            {
                SalesMixesOtherId = Guid.NewGuid( ),
                RecordType = g[ 1 ].Value,
                Date = new DateTime( Int32.Parse( g[ 2 ].Value.Substring( 0, 4 ) ), Int32.Parse( g[ 2 ].Value.Substring( 4, 2 ) ), Int32.Parse( g[ 2 ].Value.Substring( 6, 2 ) ) ),
                ProfitCenterId = Int32.Parse( g[ 3 ].Value ),
                CheckTypeId = Int32.Parse( g[ 4 ].Value ),
                SalesIntervalId = Int32.Parse( g[ 5 ].Value ),
                MenuItemId = Int32.Parse( g[ 6 ].Value ),
                Price = Convert.ToDecimal( double.Parse( g[ 7 ].Value, System.Globalization.NumberStyles.Currency ) ),
                Count = Int32.Parse( g[ 8 ].Value ),
                ExtendedAmount = Convert.ToDecimal( double.Parse( g[ 9 ].Value, System.Globalization.NumberStyles.Currency ) )
            } );

            UpdateHelper<SalesMixesOther>( results, false );
        }

        private void ProcessRSSubc( string _fileName )
        {
            var results = CommonProcess<SalesSubcategory>( _fileName, @"""(S)"",([\d]+),""([^""]+)"",([\d]+)", ( g ) => new SalesSubcategory
            {
                RecordType = g[ 1 ].Value,
                ProductClassId = Int32.Parse( g[ 2 ].Value ),
                ProductClassName = g[ 3 ].Value,
                RevenueCategoryId = Int32.Parse( g[ 4 ].Value )
            } );

            UpdateHelper<SalesSubcategory>( results, true );
        }

        private void ProcessRVIntv( string _fileName )
        {
            // Note the two *? occurances in the expression:  the Military Start and End Time values are not required, and are sometimes not in the data stream.
            // The documentation indicated that these would be fields with a fixed length of 4, but what we have seen in the test data is 0,0...
            var results = CommonProcess<SalesIntervalConfiguration>( _fileName, @"""(V)"",([\d]+),""([^""]+)"",([\d]*?),([\d]*?)", ( g ) => new SalesIntervalConfiguration
            {
                RecordType = g[ 1 ].Value,
                SalesIntervalId = Int32.Parse( g[ 2 ].Value ),
                IntervalName = g[ 3 ].Value,
                MilitaryStartTime = g[ 4 ].Value.Length == 4 ? new TimeSpan( Int32.Parse( g[ 4 ].Value.Substring( 0, 2 ) ), Int32.Parse( g[ 4 ].Value.Substring( 2, 2 ) ), 0 ) : ( TimeSpan? ) null,
                MilitaryEndTime = g[ 5 ].Value.Length == 4 ? new TimeSpan( Int32.Parse( g[ 5 ].Value.Substring( 0, 2 ) ), Int32.Parse( g[ 5 ].Value.Substring( 2, 2 ) ), 0 ) : ( TimeSpan? ) null
            } );

            UpdateHelper<SalesIntervalConfiguration>( results, true );
        }

        // The load process is very similar for each file.  This method does the bulk of the work.
        // _fileName is self-explanatory.
        // _regex is the regular expression that parses the records into fields.
        // _initDataObject is a function that takes the fields from Regex.Matches and returns a DataObject of the proper type.
        private List<T> CommonProcess<T>( string _fileName, string _regex, Func<GroupCollection, T> _initDataObject ) where T : class
        {
            AddMessage( string.Format( "Processing {0} ({1})...", _fileName, typeof( T ).Name ) );
            var results = new List<T>( );
            using ( var reader = new StreamReader( _fileName ) )
            {
                string buffer;
                while ( ( buffer = reader.ReadLine( ) ) != null )
                {
                    var matches = Regex.Matches( buffer, _regex );
                    foreach ( Match match in matches )
                    {
                        results.Add( _initDataObject( match.Groups ) );
                    }
                }
                reader.Close( );
            }
            return results;
        }

        // The database update process is very similar for each type.  The bulk of the work is done here.
        // Reference Types might be either inserts or updates, but non-reference types are always just inserts.
        private void UpdateHelper<T>( List<T> _items, bool _isReference ) where T : class
        {
            using ( var context = new AuditEntities( ) )
            {
                // This bit of reflection enables us to derive the ObjectSetName from T; otherwise, we are passing the name around as a string, easily misspelled.
                var objectSetInfo = context.GetType( ).GetProperties( )
                    .Where( p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericArguments( )[ 0 ].Name == typeof( T ).Name )
                    .Select( p => new { Name = p.Name, Property = p } )
                    .First( );
                var entitySetName = objectSetInfo.Name;
                if ( _isReference )
                {
                    // For reference types, load all the existing rows, to check for updates.
                    ( objectSetInfo.Property.GetValue( context, null ) as ObjectSet<T> ).ToList( );
                }
                foreach ( T item in _items )
                {
                    if ( _isReference )
                    {
                        var key = context.CreateEntityKey( entitySetName, item );
                        ObjectStateEntry entry = null;
                        if ( context.ObjectStateManager.TryGetObjectStateEntry( key, out entry ) )
                        {
                            context.ApplyCurrentValues( entitySetName, item );
                        }
                        else
                        {
                            context.AddObject( entitySetName, item );
                        }
                    }
                    else
                    {
                        context.AddObject( entitySetName, item );
                    }
                }
                context.SaveChanges( );
            }
        }

        private void AddMessage( string _message )
        {
            Messages.Add( _message );
            Application.DoEvents( );
        }
    }
}
