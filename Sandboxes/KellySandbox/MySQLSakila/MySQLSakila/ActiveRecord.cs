


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SubSonic.DataProviders;
using SubSonic.Extensions;
using System.Linq.Expressions;
using SubSonic.Schema;
using System.Collections;
using SubSonic;
using SubSonic.Repository;
using System.ComponentModel;
using System.Data.Common;

namespace Sakila.Data
{
	public interface ICallOnCreated
	{
		void CallOnCreated( bool _CallOnCreated);
	}

    public static class ActiveRecordExtensions
    {
        public static T SingleOrDefault<T>( this IRepository<T> _repo, Expression<Func<T, bool>> expression ) where T : IActiveRecord
        {
            var results = _repo.Find( expression );
            T single = default( T );
            foreach ( T i in results )
            {
                single = i;
                single.SetIsLoaded( true );
                single.SetIsNew( false );
                break;
            }
            return single;
        }
    }
    /// <summary>
    /// A class which represents the actor table in the Sakila Database.
    /// </summary>
    public partial class actor: IActiveRecord, ICallOnCreated
    {
    
        #region Built-in testing
        static TestRepository<actor> _testRepo;
       
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<actor>(new Sakila.Data.SakilaDB());
        }

        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }

        public static void Setup(List<actor> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }

        public static void Setup(actor item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }

        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                actor item=new actor();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;
        #endregion

        IRepository<actor> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }

        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Sakila.Data.SakilaDB _db;
        
        public actor(){
             _db=new Sakila.Data.SakilaDB();
            Init();            
        }

		///<summary>
		///Set bool to true to assign NewGuid to PrimaryKey (if appropriate) and to call OnCreated
		///</summary>
        public actor( bool _CallOnCreated ){
             _db=new Sakila.Data.SakilaDB();
            Init();  
            CallOnCreated( _CallOnCreated );
        }

        public actor(string connectionString, string providerName) {
            _db=new Sakila.Data.SakilaDB(connectionString, providerName);
            Init();            
        }

        public actor( actor _Item )
        {
			Copy( _Item );
             _db=new Sakila.Data.SakilaDB();
            Init();            
		}         
         
        public void Copy( actor _Item )
        {
			_first_name = _Item.first_name;			
			_last_name = _Item.last_name;			
			_last_update = _Item.last_update;			
		}         

        public void CallOnCreated( bool _CallOnCreated )
        {
  			if( _CallOnCreated )
			{
	
				OnCreated( );
			}
        }
         
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                actor.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<actor>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
        }
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaving();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public actor(Expression<Func<actor, bool>> expression):this() {
            SetIsLoaded(_repo.Load(this,expression));
        }
        
        internal static IRepository<actor> GetRepo(string connectionString, string providerName){
            Sakila.Data.SakilaDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Sakila.Data.SakilaDB();
            }else{
                db=new Sakila.Data.SakilaDB(connectionString, providerName);
            }
            IRepository<actor> _repo;
            
            if(db.TestMode){
                actor.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<actor>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<actor> GetRepo(){
            return GetRepo("","");
        }
        
        public static actor SingleOrDefault(Expression<Func<actor, bool>> expression) {
			return SingleOrDefault( expression, String.Empty, String.Empty );
        }      
        
        public static actor SingleOrDefault(Expression<Func<actor, bool>> expression,string connectionString, string providerName) {
            IRepository<actor> repo = GetRepo(connectionString,providerName);
            actor single = repo.SingleOrDefault<actor>( expression );
            if( null != single )
            {
				single.OnLoaded( );
            }
            return single;
        }
        
        public static bool Exists(Expression<Func<actor, bool>> expression,string connectionString, string providerName) {
            return All(connectionString,providerName).Any(expression);
        }        
        
        public static bool Exists(Expression<Func<actor, bool>> expression) {
            return All().Any(expression);
        }        

		
        public static actor GetByID(short value) 
        {
			return actor.Find( L => L.actor_id.Equals( value ) ).FirstOrDefault( );
        }

        public static IList<actor> Find(Expression<Func<actor, bool>> expression) {
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<actor> Find(Expression<Func<actor, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();
        }

        public static IQueryable<actor> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }

        public static IQueryable<actor> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<actor> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<actor> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<actor> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
        }

        public static PagedList<actor> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
        }

        public string KeyName()
        {
            return "actor_id";
        }

        public object KeyValue()
        {
            return this.actor_id;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<short>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
//        public override string ToString()
//        {
//			string rc = string.Empty;
//			if( null != this.first_name )
//			{
//				rc = this.first_name.ToString();
//			}
//            return rc;
//        }

        public override bool Equals(object obj){
			if(obj == null)
				return false;
            if(obj is actor){
                actor compare=(actor)obj;
                return compare.KeyValue().Equals( this.KeyValue() );
                //return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }



        public string DescriptorValue()
        {
            return this.first_name.ToString();
        }

        public string DescriptorColumn() {
            return "first_name";
        }

        public static string GetKeyColumn()
        {
            return "actor_id";
        }        

        public static string GetDescriptorColumn()
        {
            return "first_name";
        }
        
        #region ' Foreign Keys '
        public IQueryable<filmactor> filmactors
        {
            get
            {
                  var repo=Sakila.Data.filmactor.GetRepo();
                  return from items in repo.GetAll()
                       where items.actor_id == _actor_id
                       select items;
            }
        }
        #endregion

        short _actor_id;
		[LocalData]
        public short actor_id
        {
            get { return _actor_id; }
            set
            {
                if(_actor_id!=value){
                    _actor_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="actor_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _first_name;
		[LocalData]
        public string first_name
        {
            get { return _first_name; }
            set
            {
                if(_first_name!=value){
                    _first_name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="first_name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _last_name;
		[LocalData]
        public string last_name
        {
            get { return _last_name; }
            set
            {
                if(_last_name!=value){
                    _last_name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="last_name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _last_update;
		[LocalData]
        public DateTime last_update
        {
            get { return _last_update; }
            set
            {
                if(_last_update!=value){
                    _last_update=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="last_update");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }


        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }

        public DbCommand GetInsertCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
			OnSaving( );
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        public void Add(IDataProvider provider){
			OnSaving( );

            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
        public void Save() {
            Save(_db.DataProvider);
        }      

        public void Save(IDataProvider provider) {
            if (_isNew) {
                Add(provider);
            } else {
                Update(provider);
            }
        }

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
                    }

        public void Delete() {
            Delete(_db.DataProvider);
        }

        public static void Delete(Expression<Func<actor, bool>> expression) {
            var repo = GetRepo();
            
       
            repo.DeleteMany(expression);
        }

        
        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }

        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {
                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
    } 
    /// <summary>
    /// A class which represents the address table in the Sakila Database.
    /// </summary>
    public partial class address: IActiveRecord, ICallOnCreated
    {
    
        #region Built-in testing
        static TestRepository<address> _testRepo;
       
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<address>(new Sakila.Data.SakilaDB());
        }

        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }

        public static void Setup(List<address> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }

        public static void Setup(address item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }

        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                address item=new address();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;
        #endregion

        IRepository<address> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }

        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Sakila.Data.SakilaDB _db;
        
        public address(){
             _db=new Sakila.Data.SakilaDB();
            Init();            
        }

		///<summary>
		///Set bool to true to assign NewGuid to PrimaryKey (if appropriate) and to call OnCreated
		///</summary>
        public address( bool _CallOnCreated ){
             _db=new Sakila.Data.SakilaDB();
            Init();  
            CallOnCreated( _CallOnCreated );
        }

        public address(string connectionString, string providerName) {
            _db=new Sakila.Data.SakilaDB(connectionString, providerName);
            Init();            
        }

        public address( address _Item )
        {
			Copy( _Item );
             _db=new Sakila.Data.SakilaDB();
            Init();            
		}         
         
        public void Copy( address _Item )
        {
			_address1 = _Item.address1;			
			_address2 = _Item.address2;			
			_district = _Item.district;			
			_city_id = _Item.city_id;			
			_postal_code = _Item.postal_code;			
			_phone = _Item.phone;			
			_last_update = _Item.last_update;			
		}         

        public void CallOnCreated( bool _CallOnCreated )
        {
  			if( _CallOnCreated )
			{
	
				OnCreated( );
			}
        }
         
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                address.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<address>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
        }
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaving();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public address(Expression<Func<address, bool>> expression):this() {
            SetIsLoaded(_repo.Load(this,expression));
        }
        
        internal static IRepository<address> GetRepo(string connectionString, string providerName){
            Sakila.Data.SakilaDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Sakila.Data.SakilaDB();
            }else{
                db=new Sakila.Data.SakilaDB(connectionString, providerName);
            }
            IRepository<address> _repo;
            
            if(db.TestMode){
                address.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<address>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<address> GetRepo(){
            return GetRepo("","");
        }
        
        public static address SingleOrDefault(Expression<Func<address, bool>> expression) {
			return SingleOrDefault( expression, String.Empty, String.Empty );
        }      
        
        public static address SingleOrDefault(Expression<Func<address, bool>> expression,string connectionString, string providerName) {
            IRepository<address> repo = GetRepo(connectionString,providerName);
            address single = repo.SingleOrDefault<address>( expression );
            if( null != single )
            {
				single.OnLoaded( );
            }
            return single;
        }
        
        public static bool Exists(Expression<Func<address, bool>> expression,string connectionString, string providerName) {
            return All(connectionString,providerName).Any(expression);
        }        
        
        public static bool Exists(Expression<Func<address, bool>> expression) {
            return All().Any(expression);
        }        

		
        public static address GetByID(short value) 
        {
			return address.Find( L => L.address_id.Equals( value ) ).FirstOrDefault( );
        }

        public static IList<address> Find(Expression<Func<address, bool>> expression) {
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<address> Find(Expression<Func<address, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();
        }

        public static IQueryable<address> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }

        public static IQueryable<address> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<address> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<address> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<address> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
        }

        public static PagedList<address> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
        }

        public string KeyName()
        {
            return "address_id";
        }

        public object KeyValue()
        {
            return this.address_id;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<short>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
//        public override string ToString()
//        {
//			string rc = string.Empty;
//			if( null != this.address1 )
//			{
//				rc = this.address1.ToString();
//			}
//            return rc;
//        }

        public override bool Equals(object obj){
			if(obj == null)
				return false;
            if(obj is address){
                address compare=(address)obj;
                return compare.KeyValue().Equals( this.KeyValue() );
                //return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }



        public string DescriptorValue()
        {
            return this.address1.ToString();
        }

        public string DescriptorColumn() {
            return "address1";
        }

        public static string GetKeyColumn()
        {
            return "address_id";
        }        

        public static string GetDescriptorColumn()
        {
            return "address1";
        }
        
        #region ' Foreign Keys '
        public IQueryable<city> addresscity
        {
            get
            {
                  var repo=Sakila.Data.city.GetRepo();
                  return from items in repo.GetAll()
                       where items.city_id == _city_id
                       select items;
            }
        }
        public IQueryable<customer> customers
        {
            get
            {
                  var repo=Sakila.Data.customer.GetRepo();
                  return from items in repo.GetAll()
                       where items.address_id == _address_id
                       select items;
            }
        }
        public IQueryable<staff> staffs
        {
            get
            {
                  var repo=Sakila.Data.staff.GetRepo();
                  return from items in repo.GetAll()
                       where items.address_id == _address_id
                       select items;
            }
        }
        public IQueryable<store> stores
        {
            get
            {
                  var repo=Sakila.Data.store.GetRepo();
                  return from items in repo.GetAll()
                       where items.address_id == _address_id
                       select items;
            }
        }
        #endregion

        short _address_id;
		[LocalData]
        public short address_id
        {
            get { return _address_id; }
            set
            {
                if(_address_id!=value){
                    _address_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="address_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _address1;
		[LocalData]
        public string address1
        {
            get { return _address1; }
            set
            {
                if(_address1!=value){
                    _address1=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="address1");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _address2;
		[LocalData]
        public string address2
        {
            get { return _address2; }
            set
            {
                if(_address2!=value){
                    _address2=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="address2");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _district;
		[LocalData]
        public string district
        {
            get { return _district; }
            set
            {
                if(_district!=value){
                    _district=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="district");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        short _city_id;
		[LocalData]
        public short city_id
        {
            get { return _city_id; }
            set
            {
                if(_city_id!=value){
                    _city_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="city_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _postal_code;
		[LocalData]
        public string postal_code
        {
            get { return _postal_code; }
            set
            {
                if(_postal_code!=value){
                    _postal_code=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="postal_code");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _phone;
		[LocalData]
        public string phone
        {
            get { return _phone; }
            set
            {
                if(_phone!=value){
                    _phone=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="phone");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _last_update;
		[LocalData]
        public DateTime last_update
        {
            get { return _last_update; }
            set
            {
                if(_last_update!=value){
                    _last_update=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="last_update");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }


        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }

        public DbCommand GetInsertCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
			OnSaving( );
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        public void Add(IDataProvider provider){
			OnSaving( );

            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
        public void Save() {
            Save(_db.DataProvider);
        }      

        public void Save(IDataProvider provider) {
            if (_isNew) {
                Add(provider);
            } else {
                Update(provider);
            }
        }

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
                    }

        public void Delete() {
            Delete(_db.DataProvider);
        }

        public static void Delete(Expression<Func<address, bool>> expression) {
            var repo = GetRepo();
            
       
            repo.DeleteMany(expression);
        }

        
        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }

        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {
                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
    } 
    /// <summary>
    /// A class which represents the category table in the Sakila Database.
    /// </summary>
    public partial class category: IActiveRecord, ICallOnCreated
    {
    
        #region Built-in testing
        static TestRepository<category> _testRepo;
       
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<category>(new Sakila.Data.SakilaDB());
        }

        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }

        public static void Setup(List<category> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }

        public static void Setup(category item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }

        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                category item=new category();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;
        #endregion

        IRepository<category> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }

        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Sakila.Data.SakilaDB _db;
        
        public category(){
             _db=new Sakila.Data.SakilaDB();
            Init();            
        }

		///<summary>
		///Set bool to true to assign NewGuid to PrimaryKey (if appropriate) and to call OnCreated
		///</summary>
        public category( bool _CallOnCreated ){
             _db=new Sakila.Data.SakilaDB();
            Init();  
            CallOnCreated( _CallOnCreated );
        }

        public category(string connectionString, string providerName) {
            _db=new Sakila.Data.SakilaDB(connectionString, providerName);
            Init();            
        }

        public category( category _Item )
        {
			Copy( _Item );
             _db=new Sakila.Data.SakilaDB();
            Init();            
		}         
         
        public void Copy( category _Item )
        {
			_name = _Item.name;			
			_last_update = _Item.last_update;			
		}         

        public void CallOnCreated( bool _CallOnCreated )
        {
  			if( _CallOnCreated )
			{
	
				OnCreated( );
			}
        }
         
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                category.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<category>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
        }
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaving();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public category(Expression<Func<category, bool>> expression):this() {
            SetIsLoaded(_repo.Load(this,expression));
        }
        
        internal static IRepository<category> GetRepo(string connectionString, string providerName){
            Sakila.Data.SakilaDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Sakila.Data.SakilaDB();
            }else{
                db=new Sakila.Data.SakilaDB(connectionString, providerName);
            }
            IRepository<category> _repo;
            
            if(db.TestMode){
                category.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<category>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<category> GetRepo(){
            return GetRepo("","");
        }
        
        public static category SingleOrDefault(Expression<Func<category, bool>> expression) {
			return SingleOrDefault( expression, String.Empty, String.Empty );
        }      
        
        public static category SingleOrDefault(Expression<Func<category, bool>> expression,string connectionString, string providerName) {
            IRepository<category> repo = GetRepo(connectionString,providerName);
            category single = repo.SingleOrDefault<category>( expression );
            if( null != single )
            {
				single.OnLoaded( );
            }
            return single;
        }
        
        public static bool Exists(Expression<Func<category, bool>> expression,string connectionString, string providerName) {
            return All(connectionString,providerName).Any(expression);
        }        
        
        public static bool Exists(Expression<Func<category, bool>> expression) {
            return All().Any(expression);
        }        

		
        public static category GetByID(bool value) 
        {
			return category.Find( L => L.category_id.Equals( value ) ).FirstOrDefault( );
        }

        public static IList<category> Find(Expression<Func<category, bool>> expression) {
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<category> Find(Expression<Func<category, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();
        }

        public static IQueryable<category> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }

        public static IQueryable<category> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<category> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<category> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<category> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
        }

        public static PagedList<category> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
        }

        public string KeyName()
        {
            return "category_id";
        }

        public object KeyValue()
        {
            return this.category_id;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<bool>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
//        public override string ToString()
//        {
//			string rc = string.Empty;
//			if( null != this.name )
//			{
//				rc = this.name.ToString();
//			}
//            return rc;
//        }

        public override bool Equals(object obj){
			if(obj == null)
				return false;
            if(obj is category){
                category compare=(category)obj;
                return compare.KeyValue().Equals( this.KeyValue() );
                //return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }



        public string DescriptorValue()
        {
            return this.name.ToString();
        }

        public string DescriptorColumn() {
            return "name";
        }

        public static string GetKeyColumn()
        {
            return "category_id";
        }        

        public static string GetDescriptorColumn()
        {
            return "name";
        }
        
        #region ' Foreign Keys '
        public IQueryable<filmcategory> filmcategories
        {
            get
            {
                  var repo=Sakila.Data.filmcategory.GetRepo();
                  return from items in repo.GetAll()
                       where items.category_id == _category_id
                       select items;
            }
        }
        #endregion

        bool _category_id;
		[LocalData]
        public bool category_id
        {
            get { return _category_id; }
            set
            {
                if(_category_id!=value){
                    _category_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="category_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _name;
		[LocalData]
        public string name
        {
            get { return _name; }
            set
            {
                if(_name!=value){
                    _name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _last_update;
		[LocalData]
        public DateTime last_update
        {
            get { return _last_update; }
            set
            {
                if(_last_update!=value){
                    _last_update=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="last_update");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }


        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }

        public DbCommand GetInsertCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
			OnSaving( );
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        public void Add(IDataProvider provider){
			OnSaving( );

            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
        public void Save() {
            Save(_db.DataProvider);
        }      

        public void Save(IDataProvider provider) {
            if (_isNew) {
                Add(provider);
            } else {
                Update(provider);
            }
        }

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
                    }

        public void Delete() {
            Delete(_db.DataProvider);
        }

        public static void Delete(Expression<Func<category, bool>> expression) {
            var repo = GetRepo();
            
       
            repo.DeleteMany(expression);
        }

        
        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }

        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {
                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
    } 
    /// <summary>
    /// A class which represents the city table in the Sakila Database.
    /// </summary>
    public partial class city: IActiveRecord, ICallOnCreated
    {
    
        #region Built-in testing
        static TestRepository<city> _testRepo;
       
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<city>(new Sakila.Data.SakilaDB());
        }

        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }

        public static void Setup(List<city> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }

        public static void Setup(city item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }

        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                city item=new city();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;
        #endregion

        IRepository<city> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }

        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Sakila.Data.SakilaDB _db;
        
        public city(){
             _db=new Sakila.Data.SakilaDB();
            Init();            
        }

		///<summary>
		///Set bool to true to assign NewGuid to PrimaryKey (if appropriate) and to call OnCreated
		///</summary>
        public city( bool _CallOnCreated ){
             _db=new Sakila.Data.SakilaDB();
            Init();  
            CallOnCreated( _CallOnCreated );
        }

        public city(string connectionString, string providerName) {
            _db=new Sakila.Data.SakilaDB(connectionString, providerName);
            Init();            
        }

        public city( city _Item )
        {
			Copy( _Item );
             _db=new Sakila.Data.SakilaDB();
            Init();            
		}         
         
        public void Copy( city _Item )
        {
			_city_name = _Item.city_name;			
			_country_id = _Item.country_id;			
			_last_update = _Item.last_update;			
		}         

        public void CallOnCreated( bool _CallOnCreated )
        {
  			if( _CallOnCreated )
			{
	
				OnCreated( );
			}
        }
         
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                city.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<city>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
        }
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaving();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public city(Expression<Func<city, bool>> expression):this() {
            SetIsLoaded(_repo.Load(this,expression));
        }
        
        internal static IRepository<city> GetRepo(string connectionString, string providerName){
            Sakila.Data.SakilaDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Sakila.Data.SakilaDB();
            }else{
                db=new Sakila.Data.SakilaDB(connectionString, providerName);
            }
            IRepository<city> _repo;
            
            if(db.TestMode){
                city.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<city>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<city> GetRepo(){
            return GetRepo("","");
        }
        
        public static city SingleOrDefault(Expression<Func<city, bool>> expression) {
			return SingleOrDefault( expression, String.Empty, String.Empty );
        }      
        
        public static city SingleOrDefault(Expression<Func<city, bool>> expression,string connectionString, string providerName) {
            IRepository<city> repo = GetRepo(connectionString,providerName);
            city single = repo.SingleOrDefault<city>( expression );
            if( null != single )
            {
				single.OnLoaded( );
            }
            return single;
        }
        
        public static bool Exists(Expression<Func<city, bool>> expression,string connectionString, string providerName) {
            return All(connectionString,providerName).Any(expression);
        }        
        
        public static bool Exists(Expression<Func<city, bool>> expression) {
            return All().Any(expression);
        }        

		
        public static city GetByID(short value) 
        {
			return city.Find( L => L.city_id.Equals( value ) ).FirstOrDefault( );
        }

        public static IList<city> Find(Expression<Func<city, bool>> expression) {
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<city> Find(Expression<Func<city, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();
        }

        public static IQueryable<city> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }

        public static IQueryable<city> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<city> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<city> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<city> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
        }

        public static PagedList<city> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
        }

        public string KeyName()
        {
            return "city_id";
        }

        public object KeyValue()
        {
            return this.city_id;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<short>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
//        public override string ToString()
//        {
//			string rc = string.Empty;
//			if( null != this.city_name )
//			{
//				rc = this.city_name.ToString();
//			}
//            return rc;
//        }

        public override bool Equals(object obj){
			if(obj == null)
				return false;
            if(obj is city){
                city compare=(city)obj;
                return compare.KeyValue().Equals( this.KeyValue() );
                //return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }



        public string DescriptorValue()
        {
            return this.city_name.ToString();
        }

        public string DescriptorColumn() {
            return "city_name";
        }

        public static string GetKeyColumn()
        {
            return "city_id";
        }        

        public static string GetDescriptorColumn()
        {
            return "city_name";
        }
        
        #region ' Foreign Keys '
        public IQueryable<address> addresses
        {
            get
            {
                  var repo=Sakila.Data.address.GetRepo();
                  return from items in repo.GetAll()
                       where items.city_id == _city_id
                       select items;
            }
        }
        public IQueryable<country> citycountry
        {
            get
            {
                  var repo=Sakila.Data.country.GetRepo();
                  return from items in repo.GetAll()
                       where items.country_id == _country_id
                       select items;
            }
        }
        #endregion

        short _city_id;
		[LocalData]
        public short city_id
        {
            get { return _city_id; }
            set
            {
                if(_city_id!=value){
                    _city_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="city_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _city_name;
		[LocalData]
        public string city_name
        {
            get { return _city_name; }
            set
            {
                if(_city_name!=value){
                    _city_name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="city_name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        short _country_id;
		[LocalData]
        public short country_id
        {
            get { return _country_id; }
            set
            {
                if(_country_id!=value){
                    _country_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="country_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _last_update;
		[LocalData]
        public DateTime last_update
        {
            get { return _last_update; }
            set
            {
                if(_last_update!=value){
                    _last_update=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="last_update");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }


        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }

        public DbCommand GetInsertCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
			OnSaving( );
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        public void Add(IDataProvider provider){
			OnSaving( );

            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
        public void Save() {
            Save(_db.DataProvider);
        }      

        public void Save(IDataProvider provider) {
            if (_isNew) {
                Add(provider);
            } else {
                Update(provider);
            }
        }

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
                    }

        public void Delete() {
            Delete(_db.DataProvider);
        }

        public static void Delete(Expression<Func<city, bool>> expression) {
            var repo = GetRepo();
            
       
            repo.DeleteMany(expression);
        }

        
        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }

        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {
                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
    } 
    /// <summary>
    /// A class which represents the country table in the Sakila Database.
    /// </summary>
    public partial class country: IActiveRecord, ICallOnCreated
    {
    
        #region Built-in testing
        static TestRepository<country> _testRepo;
       
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<country>(new Sakila.Data.SakilaDB());
        }

        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }

        public static void Setup(List<country> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }

        public static void Setup(country item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }

        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                country item=new country();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;
        #endregion

        IRepository<country> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }

        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Sakila.Data.SakilaDB _db;
        
        public country(){
             _db=new Sakila.Data.SakilaDB();
            Init();            
        }

		///<summary>
		///Set bool to true to assign NewGuid to PrimaryKey (if appropriate) and to call OnCreated
		///</summary>
        public country( bool _CallOnCreated ){
             _db=new Sakila.Data.SakilaDB();
            Init();  
            CallOnCreated( _CallOnCreated );
        }

        public country(string connectionString, string providerName) {
            _db=new Sakila.Data.SakilaDB(connectionString, providerName);
            Init();            
        }

        public country( country _Item )
        {
			Copy( _Item );
             _db=new Sakila.Data.SakilaDB();
            Init();            
		}         
         
        public void Copy( country _Item )
        {
			_country_name = _Item.country_name;			
			_last_update = _Item.last_update;			
		}         

        public void CallOnCreated( bool _CallOnCreated )
        {
  			if( _CallOnCreated )
			{
	
				OnCreated( );
			}
        }
         
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                country.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<country>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
        }
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaving();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public country(Expression<Func<country, bool>> expression):this() {
            SetIsLoaded(_repo.Load(this,expression));
        }
        
        internal static IRepository<country> GetRepo(string connectionString, string providerName){
            Sakila.Data.SakilaDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Sakila.Data.SakilaDB();
            }else{
                db=new Sakila.Data.SakilaDB(connectionString, providerName);
            }
            IRepository<country> _repo;
            
            if(db.TestMode){
                country.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<country>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<country> GetRepo(){
            return GetRepo("","");
        }
        
        public static country SingleOrDefault(Expression<Func<country, bool>> expression) {
			return SingleOrDefault( expression, String.Empty, String.Empty );
        }      
        
        public static country SingleOrDefault(Expression<Func<country, bool>> expression,string connectionString, string providerName) {
            IRepository<country> repo = GetRepo(connectionString,providerName);
            country single = repo.SingleOrDefault<country>( expression );
            if( null != single )
            {
				single.OnLoaded( );
            }
            return single;
        }
        
        public static bool Exists(Expression<Func<country, bool>> expression,string connectionString, string providerName) {
            return All(connectionString,providerName).Any(expression);
        }        
        
        public static bool Exists(Expression<Func<country, bool>> expression) {
            return All().Any(expression);
        }        

		
        public static country GetByID(short value) 
        {
			return country.Find( L => L.country_id.Equals( value ) ).FirstOrDefault( );
        }

        public static IList<country> Find(Expression<Func<country, bool>> expression) {
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<country> Find(Expression<Func<country, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();
        }

        public static IQueryable<country> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }

        public static IQueryable<country> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<country> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<country> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<country> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
        }

        public static PagedList<country> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
        }

        public string KeyName()
        {
            return "country_id";
        }

        public object KeyValue()
        {
            return this.country_id;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<short>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
//        public override string ToString()
//        {
//			string rc = string.Empty;
//			if( null != this.country_name )
//			{
//				rc = this.country_name.ToString();
//			}
//            return rc;
//        }

        public override bool Equals(object obj){
			if(obj == null)
				return false;
            if(obj is country){
                country compare=(country)obj;
                return compare.KeyValue().Equals( this.KeyValue() );
                //return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }



        public string DescriptorValue()
        {
            return this.country_name.ToString();
        }

        public string DescriptorColumn() {
            return "country_name";
        }

        public static string GetKeyColumn()
        {
            return "country_id";
        }        

        public static string GetDescriptorColumn()
        {
            return "country_name";
        }
        
        #region ' Foreign Keys '
        public IQueryable<city> cities
        {
            get
            {
                  var repo=Sakila.Data.city.GetRepo();
                  return from items in repo.GetAll()
                       where items.country_id == _country_id
                       select items;
            }
        }
        #endregion

        short _country_id;
		[LocalData]
        public short country_id
        {
            get { return _country_id; }
            set
            {
                if(_country_id!=value){
                    _country_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="country_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _country_name;
		[LocalData]
        public string country_name
        {
            get { return _country_name; }
            set
            {
                if(_country_name!=value){
                    _country_name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="country_name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _last_update;
		[LocalData]
        public DateTime last_update
        {
            get { return _last_update; }
            set
            {
                if(_last_update!=value){
                    _last_update=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="last_update");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }


        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }

        public DbCommand GetInsertCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
			OnSaving( );
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        public void Add(IDataProvider provider){
			OnSaving( );

            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
        public void Save() {
            Save(_db.DataProvider);
        }      

        public void Save(IDataProvider provider) {
            if (_isNew) {
                Add(provider);
            } else {
                Update(provider);
            }
        }

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
                    }

        public void Delete() {
            Delete(_db.DataProvider);
        }

        public static void Delete(Expression<Func<country, bool>> expression) {
            var repo = GetRepo();
            
       
            repo.DeleteMany(expression);
        }

        
        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }

        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {
                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
    } 
    /// <summary>
    /// A class which represents the customer table in the Sakila Database.
    /// </summary>
    public partial class customer: IActiveRecord, ICallOnCreated
    {
    
        #region Built-in testing
        static TestRepository<customer> _testRepo;
       
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<customer>(new Sakila.Data.SakilaDB());
        }

        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }

        public static void Setup(List<customer> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }

        public static void Setup(customer item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }

        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                customer item=new customer();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;
        #endregion

        IRepository<customer> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }

        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Sakila.Data.SakilaDB _db;
        
        public customer(){
             _db=new Sakila.Data.SakilaDB();
            Init();            
        }

		///<summary>
		///Set bool to true to assign NewGuid to PrimaryKey (if appropriate) and to call OnCreated
		///</summary>
        public customer( bool _CallOnCreated ){
             _db=new Sakila.Data.SakilaDB();
            Init();  
            CallOnCreated( _CallOnCreated );
        }

        public customer(string connectionString, string providerName) {
            _db=new Sakila.Data.SakilaDB(connectionString, providerName);
            Init();            
        }

        public customer( customer _Item )
        {
			Copy( _Item );
             _db=new Sakila.Data.SakilaDB();
            Init();            
		}         
         
        public void Copy( customer _Item )
        {
			_store_id = _Item.store_id;			
			_first_name = _Item.first_name;			
			_last_name = _Item.last_name;			
			_email = _Item.email;			
			_address_id = _Item.address_id;			
			_active = _Item.active;			
			_create_date = _Item.create_date;			
			_last_update = _Item.last_update;			
		}         

        public void CallOnCreated( bool _CallOnCreated )
        {
  			if( _CallOnCreated )
			{
	
				OnCreated( );
			}
        }
         
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                customer.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<customer>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
        }
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaving();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public customer(Expression<Func<customer, bool>> expression):this() {
            SetIsLoaded(_repo.Load(this,expression));
        }
        
        internal static IRepository<customer> GetRepo(string connectionString, string providerName){
            Sakila.Data.SakilaDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Sakila.Data.SakilaDB();
            }else{
                db=new Sakila.Data.SakilaDB(connectionString, providerName);
            }
            IRepository<customer> _repo;
            
            if(db.TestMode){
                customer.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<customer>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<customer> GetRepo(){
            return GetRepo("","");
        }
        
        public static customer SingleOrDefault(Expression<Func<customer, bool>> expression) {
			return SingleOrDefault( expression, String.Empty, String.Empty );
        }      
        
        public static customer SingleOrDefault(Expression<Func<customer, bool>> expression,string connectionString, string providerName) {
            IRepository<customer> repo = GetRepo(connectionString,providerName);
            customer single = repo.SingleOrDefault<customer>( expression );
            if( null != single )
            {
				single.OnLoaded( );
            }
            return single;
        }
        
        public static bool Exists(Expression<Func<customer, bool>> expression,string connectionString, string providerName) {
            return All(connectionString,providerName).Any(expression);
        }        
        
        public static bool Exists(Expression<Func<customer, bool>> expression) {
            return All().Any(expression);
        }        

		
        public static customer GetByID(short value) 
        {
			return customer.Find( L => L.customer_id.Equals( value ) ).FirstOrDefault( );
        }

        public static IList<customer> Find(Expression<Func<customer, bool>> expression) {
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<customer> Find(Expression<Func<customer, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();
        }

        public static IQueryable<customer> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }

        public static IQueryable<customer> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<customer> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<customer> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<customer> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
        }

        public static PagedList<customer> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
        }

        public string KeyName()
        {
            return "customer_id";
        }

        public object KeyValue()
        {
            return this.customer_id;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<short>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
//        public override string ToString()
//        {
//			string rc = string.Empty;
//			if( null != this.first_name )
//			{
//				rc = this.first_name.ToString();
//			}
//            return rc;
//        }

        public override bool Equals(object obj){
			if(obj == null)
				return false;
            if(obj is customer){
                customer compare=(customer)obj;
                return compare.KeyValue().Equals( this.KeyValue() );
                //return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }



        public string DescriptorValue()
        {
            return this.first_name.ToString();
        }

        public string DescriptorColumn() {
            return "first_name";
        }

        public static string GetKeyColumn()
        {
            return "customer_id";
        }        

        public static string GetDescriptorColumn()
        {
            return "first_name";
        }
        
        #region ' Foreign Keys '
        public IQueryable<address> customeraddress
        {
            get
            {
                  var repo=Sakila.Data.address.GetRepo();
                  return from items in repo.GetAll()
                       where items.address_id == _address_id
                       select items;
            }
        }
        public IQueryable<store> customerstore
        {
            get
            {
                  var repo=Sakila.Data.store.GetRepo();
                  return from items in repo.GetAll()
                       where items.store_id == _store_id
                       select items;
            }
        }
        public IQueryable<payment> payments
        {
            get
            {
                  var repo=Sakila.Data.payment.GetRepo();
                  return from items in repo.GetAll()
                       where items.customer_id == _customer_id
                       select items;
            }
        }
        public IQueryable<rental> rentals
        {
            get
            {
                  var repo=Sakila.Data.rental.GetRepo();
                  return from items in repo.GetAll()
                       where items.customer_id == _customer_id
                       select items;
            }
        }
        #endregion

        short _customer_id;
		[LocalData]
        public short customer_id
        {
            get { return _customer_id; }
            set
            {
                if(_customer_id!=value){
                    _customer_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="customer_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        bool _store_id;
		[LocalData]
        public bool store_id
        {
            get { return _store_id; }
            set
            {
                if(_store_id!=value){
                    _store_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="store_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _first_name;
		[LocalData]
        public string first_name
        {
            get { return _first_name; }
            set
            {
                if(_first_name!=value){
                    _first_name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="first_name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _last_name;
		[LocalData]
        public string last_name
        {
            get { return _last_name; }
            set
            {
                if(_last_name!=value){
                    _last_name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="last_name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _email;
		[LocalData]
        public string email
        {
            get { return _email; }
            set
            {
                if(_email!=value){
                    _email=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="email");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        short _address_id;
		[LocalData]
        public short address_id
        {
            get { return _address_id; }
            set
            {
                if(_address_id!=value){
                    _address_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="address_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        bool _active;
		[LocalData]
        public bool active
        {
            get { return _active; }
            set
            {
                if(_active!=value){
                    _active=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="active");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _create_date;
		[LocalData]
        public DateTime create_date
        {
            get { return _create_date; }
            set
            {
                if(_create_date!=value){
                    _create_date=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="create_date");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _last_update;
		[LocalData]
        public DateTime last_update
        {
            get { return _last_update; }
            set
            {
                if(_last_update!=value){
                    _last_update=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="last_update");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }


        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }

        public DbCommand GetInsertCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
			OnSaving( );
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        public void Add(IDataProvider provider){
			OnSaving( );

            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
        public void Save() {
            Save(_db.DataProvider);
        }      

        public void Save(IDataProvider provider) {
            if (_isNew) {
                Add(provider);
            } else {
                Update(provider);
            }
        }

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
                    }

        public void Delete() {
            Delete(_db.DataProvider);
        }

        public static void Delete(Expression<Func<customer, bool>> expression) {
            var repo = GetRepo();
            
       
            repo.DeleteMany(expression);
        }

        
        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }

        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {
                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
    } 
    /// <summary>
    /// A class which represents the film table in the Sakila Database.
    /// </summary>
    public partial class film: IActiveRecord, ICallOnCreated
    {
    
        #region Built-in testing
        static TestRepository<film> _testRepo;
       
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<film>(new Sakila.Data.SakilaDB());
        }

        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }

        public static void Setup(List<film> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }

        public static void Setup(film item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }

        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                film item=new film();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;
        #endregion

        IRepository<film> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }

        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Sakila.Data.SakilaDB _db;
        
        public film(){
             _db=new Sakila.Data.SakilaDB();
            Init();            
        }

		///<summary>
		///Set bool to true to assign NewGuid to PrimaryKey (if appropriate) and to call OnCreated
		///</summary>
        public film( bool _CallOnCreated ){
             _db=new Sakila.Data.SakilaDB();
            Init();  
            CallOnCreated( _CallOnCreated );
        }

        public film(string connectionString, string providerName) {
            _db=new Sakila.Data.SakilaDB(connectionString, providerName);
            Init();            
        }

        public film( film _Item )
        {
			Copy( _Item );
             _db=new Sakila.Data.SakilaDB();
            Init();            
		}         
         
        public void Copy( film _Item )
        {
			_title = _Item.title;			
			_description = _Item.description;			
			_release_year = _Item.release_year;			
			_language_id = _Item.language_id;			
			_original_language_id = _Item.original_language_id;			
			_rental_duration = _Item.rental_duration;			
			_rental_rate = _Item.rental_rate;			
			_length = _Item.length;			
			_replacement_cost = _Item.replacement_cost;			
			_rating = _Item.rating;			
			_special_features = _Item.special_features;			
			_last_update = _Item.last_update;			
		}         

        public void CallOnCreated( bool _CallOnCreated )
        {
  			if( _CallOnCreated )
			{
	
				OnCreated( );
			}
        }
         
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                film.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<film>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
        }
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaving();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public film(Expression<Func<film, bool>> expression):this() {
            SetIsLoaded(_repo.Load(this,expression));
        }
        
        internal static IRepository<film> GetRepo(string connectionString, string providerName){
            Sakila.Data.SakilaDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Sakila.Data.SakilaDB();
            }else{
                db=new Sakila.Data.SakilaDB(connectionString, providerName);
            }
            IRepository<film> _repo;
            
            if(db.TestMode){
                film.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<film>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<film> GetRepo(){
            return GetRepo("","");
        }
        
        public static film SingleOrDefault(Expression<Func<film, bool>> expression) {
			return SingleOrDefault( expression, String.Empty, String.Empty );
        }      
        
        public static film SingleOrDefault(Expression<Func<film, bool>> expression,string connectionString, string providerName) {
            IRepository<film> repo = GetRepo(connectionString,providerName);
            film single = repo.SingleOrDefault<film>( expression );
            if( null != single )
            {
				single.OnLoaded( );
            }
            return single;
        }
        
        public static bool Exists(Expression<Func<film, bool>> expression,string connectionString, string providerName) {
            return All(connectionString,providerName).Any(expression);
        }        
        
        public static bool Exists(Expression<Func<film, bool>> expression) {
            return All().Any(expression);
        }        

		
        public static film GetByID(short value) 
        {
			return film.Find( L => L.film_id.Equals( value ) ).FirstOrDefault( );
        }

        public static IList<film> Find(Expression<Func<film, bool>> expression) {
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<film> Find(Expression<Func<film, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();
        }

        public static IQueryable<film> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }

        public static IQueryable<film> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<film> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<film> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<film> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
        }

        public static PagedList<film> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
        }

        public string KeyName()
        {
            return "film_id";
        }

        public object KeyValue()
        {
            return this.film_id;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<short>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
//        public override string ToString()
//        {
//			string rc = string.Empty;
//			if( null != this.title )
//			{
//				rc = this.title.ToString();
//			}
//            return rc;
//        }

        public override bool Equals(object obj){
			if(obj == null)
				return false;
            if(obj is film){
                film compare=(film)obj;
                return compare.KeyValue().Equals( this.KeyValue() );
                //return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }



        public string DescriptorValue()
        {
            return this.title.ToString();
        }

        public string DescriptorColumn() {
            return "title";
        }

        public static string GetKeyColumn()
        {
            return "film_id";
        }        

        public static string GetDescriptorColumn()
        {
            return "title";
        }
        
        #region ' Foreign Keys '
        public IQueryable<language> filmlanguage
        {
            get
            {
                  var repo=Sakila.Data.language.GetRepo();
                  return from items in repo.GetAll()
                       where items.language_id == _language_id
                       select items;
            }
        }
        public IQueryable<language> filmlanguageoriginal
        {
            get
            {
                  var repo=Sakila.Data.language.GetRepo();
                  return from items in repo.GetAll()
                       where items.language_id == _original_language_id
                       select items;
            }
        }
        public IQueryable<filmactor> filmactors
        {
            get
            {
                  var repo=Sakila.Data.filmactor.GetRepo();
                  return from items in repo.GetAll()
                       where items.film_id == _film_id
                       select items;
            }
        }
        public IQueryable<filmcategory> filmcategories
        {
            get
            {
                  var repo=Sakila.Data.filmcategory.GetRepo();
                  return from items in repo.GetAll()
                       where items.film_id == _film_id
                       select items;
            }
        }
        public IQueryable<inventory> inventories
        {
            get
            {
                  var repo=Sakila.Data.inventory.GetRepo();
                  return from items in repo.GetAll()
                       where items.film_id == _film_id
                       select items;
            }
        }
        #endregion

        short _film_id;
		[LocalData]
        public short film_id
        {
            get { return _film_id; }
            set
            {
                if(_film_id!=value){
                    _film_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="film_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _title;
		[LocalData]
        public string title
        {
            get { return _title; }
            set
            {
                if(_title!=value){
                    _title=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="title");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _description;
		[LocalData]
        public string description
        {
            get { return _description; }
            set
            {
                if(_description!=value){
                    _description=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="description");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _release_year;
		[LocalData]
        public string release_year
        {
            get { return _release_year; }
            set
            {
                if(_release_year!=value){
                    _release_year=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="release_year");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        bool _language_id;
		[LocalData]
        public bool language_id
        {
            get { return _language_id; }
            set
            {
                if(_language_id!=value){
                    _language_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="language_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        bool? _original_language_id;
		[LocalData]
        public bool? original_language_id
        {
            get { return _original_language_id; }
            set
            {
                if(_original_language_id!=value){
                    _original_language_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="original_language_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        bool _rental_duration;
		[LocalData]
        public bool rental_duration
        {
            get { return _rental_duration; }
            set
            {
                if(_rental_duration!=value){
                    _rental_duration=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="rental_duration");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        decimal _rental_rate;
		[LocalData]
        public decimal rental_rate
        {
            get { return _rental_rate; }
            set
            {
                if(_rental_rate!=value){
                    _rental_rate=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="rental_rate");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        short? _length;
		[LocalData]
        public short? length
        {
            get { return _length; }
            set
            {
                if(_length!=value){
                    _length=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="length");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        decimal _replacement_cost;
		[LocalData]
        public decimal replacement_cost
        {
            get { return _replacement_cost; }
            set
            {
                if(_replacement_cost!=value){
                    _replacement_cost=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="replacement_cost");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _rating;
		[LocalData]
        public string rating
        {
            get { return _rating; }
            set
            {
                if(_rating!=value){
                    _rating=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="rating");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _special_features;
		[LocalData]
        public string special_features
        {
            get { return _special_features; }
            set
            {
                if(_special_features!=value){
                    _special_features=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="special_features");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _last_update;
		[LocalData]
        public DateTime last_update
        {
            get { return _last_update; }
            set
            {
                if(_last_update!=value){
                    _last_update=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="last_update");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }


        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }

        public DbCommand GetInsertCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
			OnSaving( );
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        public void Add(IDataProvider provider){
			OnSaving( );

            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
        public void Save() {
            Save(_db.DataProvider);
        }      

        public void Save(IDataProvider provider) {
            if (_isNew) {
                Add(provider);
            } else {
                Update(provider);
            }
        }

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
                    }

        public void Delete() {
            Delete(_db.DataProvider);
        }

        public static void Delete(Expression<Func<film, bool>> expression) {
            var repo = GetRepo();
            
       
            repo.DeleteMany(expression);
        }

        
        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }

        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {
                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
    } 
    /// <summary>
    /// A class which represents the filmactor table in the Sakila Database.
    /// </summary>
    public partial class filmactor: IActiveRecord, ICallOnCreated
    {
    
        #region Built-in testing
        static TestRepository<filmactor> _testRepo;
       
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<filmactor>(new Sakila.Data.SakilaDB());
        }

        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }

        public static void Setup(List<filmactor> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }

        public static void Setup(filmactor item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }

        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                filmactor item=new filmactor();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;
        #endregion

        IRepository<filmactor> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }

        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Sakila.Data.SakilaDB _db;
        
        public filmactor(){
             _db=new Sakila.Data.SakilaDB();
            Init();            
        }

		///<summary>
		///Set bool to true to assign NewGuid to PrimaryKey (if appropriate) and to call OnCreated
		///</summary>
        public filmactor( bool _CallOnCreated ){
             _db=new Sakila.Data.SakilaDB();
            Init();  
            CallOnCreated( _CallOnCreated );
        }

        public filmactor(string connectionString, string providerName) {
            _db=new Sakila.Data.SakilaDB(connectionString, providerName);
            Init();            
        }

        public filmactor( filmactor _Item )
        {
			Copy( _Item );
             _db=new Sakila.Data.SakilaDB();
            Init();            
		}         
         
        public void Copy( filmactor _Item )
        {
			_film_id = _Item.film_id;			
			_last_update = _Item.last_update;			
		}         

        public void CallOnCreated( bool _CallOnCreated )
        {
  			if( _CallOnCreated )
			{
	
				OnCreated( );
			}
        }
         
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                filmactor.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<filmactor>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
        }
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaving();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public filmactor(Expression<Func<filmactor, bool>> expression):this() {
            SetIsLoaded(_repo.Load(this,expression));
        }
        
        internal static IRepository<filmactor> GetRepo(string connectionString, string providerName){
            Sakila.Data.SakilaDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Sakila.Data.SakilaDB();
            }else{
                db=new Sakila.Data.SakilaDB(connectionString, providerName);
            }
            IRepository<filmactor> _repo;
            
            if(db.TestMode){
                filmactor.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<filmactor>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<filmactor> GetRepo(){
            return GetRepo("","");
        }
        
        public static filmactor SingleOrDefault(Expression<Func<filmactor, bool>> expression) {
			return SingleOrDefault( expression, String.Empty, String.Empty );
        }      
        
        public static filmactor SingleOrDefault(Expression<Func<filmactor, bool>> expression,string connectionString, string providerName) {
            IRepository<filmactor> repo = GetRepo(connectionString,providerName);
            filmactor single = repo.SingleOrDefault<filmactor>( expression );
            if( null != single )
            {
				single.OnLoaded( );
            }
            return single;
        }
        
        public static bool Exists(Expression<Func<filmactor, bool>> expression,string connectionString, string providerName) {
            return All(connectionString,providerName).Any(expression);
        }        
        
        public static bool Exists(Expression<Func<filmactor, bool>> expression) {
            return All().Any(expression);
        }        

		
        public static filmactor GetByID(short value) 
        {
			return filmactor.Find( L => L.actor_id.Equals( value ) ).FirstOrDefault( );
        }

        public static IList<filmactor> Find(Expression<Func<filmactor, bool>> expression) {
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<filmactor> Find(Expression<Func<filmactor, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();
        }

        public static IQueryable<filmactor> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }

        public static IQueryable<filmactor> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<filmactor> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<filmactor> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<filmactor> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
        }

        public static PagedList<filmactor> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
        }

        public string KeyName()
        {
            return "actor_id";
        }

        public object KeyValue()
        {
            return this.actor_id;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<short>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
//        public override string ToString()
//        {
//			string rc = string.Empty;
//			if( null != this.film_id )
//			{
//				rc = this.film_id.ToString();
//			}
//            return rc;
//        }

        public override bool Equals(object obj){
			if(obj == null)
				return false;
            if(obj is filmactor){
                filmactor compare=(filmactor)obj;
                return compare.KeyValue().Equals( this.KeyValue() );
                //return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }



        public string DescriptorValue()
        {
            return this.film_id.ToString();
        }

        public string DescriptorColumn() {
            return "film_id";
        }

        public static string GetKeyColumn()
        {
            return "actor_id";
        }        

        public static string GetDescriptorColumn()
        {
            return "film_id";
        }
        
        #region ' Foreign Keys '
        public IQueryable<actor> filmactoractor
        {
            get
            {
                  var repo=Sakila.Data.actor.GetRepo();
                  return from items in repo.GetAll()
                       where items.actor_id == _actor_id
                       select items;
            }
        }
        public IQueryable<film> filmactorfilm
        {
            get
            {
                  var repo=Sakila.Data.film.GetRepo();
                  return from items in repo.GetAll()
                       where items.film_id == _film_id
                       select items;
            }
        }
        #endregion

        short _actor_id;
		[LocalData]
        public short actor_id
        {
            get { return _actor_id; }
            set
            {
                if(_actor_id!=value){
                    _actor_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="actor_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        short _film_id;
		[LocalData]
        public short film_id
        {
            get { return _film_id; }
            set
            {
                if(_film_id!=value){
                    _film_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="film_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _last_update;
		[LocalData]
        public DateTime last_update
        {
            get { return _last_update; }
            set
            {
                if(_last_update!=value){
                    _last_update=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="last_update");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }


        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }

        public DbCommand GetInsertCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
			OnSaving( );
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        public void Add(IDataProvider provider){
			OnSaving( );

            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
        public void Save() {
            Save(_db.DataProvider);
        }      

        public void Save(IDataProvider provider) {
            if (_isNew) {
                Add(provider);
            } else {
                Update(provider);
            }
        }

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
                    }

        public void Delete() {
            Delete(_db.DataProvider);
        }

        public static void Delete(Expression<Func<filmactor, bool>> expression) {
            var repo = GetRepo();
            
       
            repo.DeleteMany(expression);
        }

        
        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }

        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {
                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
    } 
    /// <summary>
    /// A class which represents the filmcategory table in the Sakila Database.
    /// </summary>
    public partial class filmcategory: IActiveRecord, ICallOnCreated
    {
    
        #region Built-in testing
        static TestRepository<filmcategory> _testRepo;
       
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<filmcategory>(new Sakila.Data.SakilaDB());
        }

        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }

        public static void Setup(List<filmcategory> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }

        public static void Setup(filmcategory item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }

        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                filmcategory item=new filmcategory();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;
        #endregion

        IRepository<filmcategory> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }

        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Sakila.Data.SakilaDB _db;
        
        public filmcategory(){
             _db=new Sakila.Data.SakilaDB();
            Init();            
        }

		///<summary>
		///Set bool to true to assign NewGuid to PrimaryKey (if appropriate) and to call OnCreated
		///</summary>
        public filmcategory( bool _CallOnCreated ){
             _db=new Sakila.Data.SakilaDB();
            Init();  
            CallOnCreated( _CallOnCreated );
        }

        public filmcategory(string connectionString, string providerName) {
            _db=new Sakila.Data.SakilaDB(connectionString, providerName);
            Init();            
        }

        public filmcategory( filmcategory _Item )
        {
			Copy( _Item );
             _db=new Sakila.Data.SakilaDB();
            Init();            
		}         
         
        public void Copy( filmcategory _Item )
        {
			_category_id = _Item.category_id;			
			_last_update = _Item.last_update;			
		}         

        public void CallOnCreated( bool _CallOnCreated )
        {
  			if( _CallOnCreated )
			{
	
				OnCreated( );
			}
        }
         
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                filmcategory.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<filmcategory>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
        }
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaving();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public filmcategory(Expression<Func<filmcategory, bool>> expression):this() {
            SetIsLoaded(_repo.Load(this,expression));
        }
        
        internal static IRepository<filmcategory> GetRepo(string connectionString, string providerName){
            Sakila.Data.SakilaDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Sakila.Data.SakilaDB();
            }else{
                db=new Sakila.Data.SakilaDB(connectionString, providerName);
            }
            IRepository<filmcategory> _repo;
            
            if(db.TestMode){
                filmcategory.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<filmcategory>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<filmcategory> GetRepo(){
            return GetRepo("","");
        }
        
        public static filmcategory SingleOrDefault(Expression<Func<filmcategory, bool>> expression) {
			return SingleOrDefault( expression, String.Empty, String.Empty );
        }      
        
        public static filmcategory SingleOrDefault(Expression<Func<filmcategory, bool>> expression,string connectionString, string providerName) {
            IRepository<filmcategory> repo = GetRepo(connectionString,providerName);
            filmcategory single = repo.SingleOrDefault<filmcategory>( expression );
            if( null != single )
            {
				single.OnLoaded( );
            }
            return single;
        }
        
        public static bool Exists(Expression<Func<filmcategory, bool>> expression,string connectionString, string providerName) {
            return All(connectionString,providerName).Any(expression);
        }        
        
        public static bool Exists(Expression<Func<filmcategory, bool>> expression) {
            return All().Any(expression);
        }        

		
        public static filmcategory GetByID(short value) 
        {
			return filmcategory.Find( L => L.film_id.Equals( value ) ).FirstOrDefault( );
        }

        public static IList<filmcategory> Find(Expression<Func<filmcategory, bool>> expression) {
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<filmcategory> Find(Expression<Func<filmcategory, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();
        }

        public static IQueryable<filmcategory> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }

        public static IQueryable<filmcategory> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<filmcategory> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<filmcategory> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<filmcategory> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
        }

        public static PagedList<filmcategory> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
        }

        public string KeyName()
        {
            return "film_id";
        }

        public object KeyValue()
        {
            return this.film_id;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<short>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
//        public override string ToString()
//        {
//			string rc = string.Empty;
//			if( null != this.category_id )
//			{
//				rc = this.category_id.ToString();
//			}
//            return rc;
//        }

        public override bool Equals(object obj){
			if(obj == null)
				return false;
            if(obj is filmcategory){
                filmcategory compare=(filmcategory)obj;
                return compare.KeyValue().Equals( this.KeyValue() );
                //return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }



        public string DescriptorValue()
        {
            return this.category_id.ToString();
        }

        public string DescriptorColumn() {
            return "category_id";
        }

        public static string GetKeyColumn()
        {
            return "film_id";
        }        

        public static string GetDescriptorColumn()
        {
            return "category_id";
        }
        
        #region ' Foreign Keys '
        public IQueryable<category> filmcategorycategory
        {
            get
            {
                  var repo=Sakila.Data.category.GetRepo();
                  return from items in repo.GetAll()
                       where items.category_id == _category_id
                       select items;
            }
        }
        public IQueryable<film> filmcategoryfilm
        {
            get
            {
                  var repo=Sakila.Data.film.GetRepo();
                  return from items in repo.GetAll()
                       where items.film_id == _film_id
                       select items;
            }
        }
        #endregion

        short _film_id;
		[LocalData]
        public short film_id
        {
            get { return _film_id; }
            set
            {
                if(_film_id!=value){
                    _film_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="film_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        bool _category_id;
		[LocalData]
        public bool category_id
        {
            get { return _category_id; }
            set
            {
                if(_category_id!=value){
                    _category_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="category_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _last_update;
		[LocalData]
        public DateTime last_update
        {
            get { return _last_update; }
            set
            {
                if(_last_update!=value){
                    _last_update=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="last_update");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }


        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }

        public DbCommand GetInsertCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
			OnSaving( );
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        public void Add(IDataProvider provider){
			OnSaving( );

            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
        public void Save() {
            Save(_db.DataProvider);
        }      

        public void Save(IDataProvider provider) {
            if (_isNew) {
                Add(provider);
            } else {
                Update(provider);
            }
        }

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
                    }

        public void Delete() {
            Delete(_db.DataProvider);
        }

        public static void Delete(Expression<Func<filmcategory, bool>> expression) {
            var repo = GetRepo();
            
       
            repo.DeleteMany(expression);
        }

        
        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }

        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {
                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
    } 
    /// <summary>
    /// A class which represents the filmlistview table in the Sakila Database.
    /// </summary>
    public partial class filmlistview: IActiveRecord, ICallOnCreated
    {
    
        #region Built-in testing
        static TestRepository<filmlistview> _testRepo;
       
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<filmlistview>(new Sakila.Data.SakilaDB());
        }

        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }

        public static void Setup(List<filmlistview> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }

        public static void Setup(filmlistview item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }

        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                filmlistview item=new filmlistview();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;
        #endregion

        IRepository<filmlistview> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }

        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Sakila.Data.SakilaDB _db;
        
        public filmlistview(){
             _db=new Sakila.Data.SakilaDB();
            Init();            
        }

		///<summary>
		///Set bool to true to assign NewGuid to PrimaryKey (if appropriate) and to call OnCreated
		///</summary>
        public filmlistview( bool _CallOnCreated ){
             _db=new Sakila.Data.SakilaDB();
            Init();  
            CallOnCreated( _CallOnCreated );
        }

        public filmlistview(string connectionString, string providerName) {
            _db=new Sakila.Data.SakilaDB(connectionString, providerName);
            Init();            
        }

        public filmlistview( filmlistview _Item )
        {
			Copy( _Item );
             _db=new Sakila.Data.SakilaDB();
            Init();            
		}         
         
        public void Copy( filmlistview _Item )
        {
			_title = _Item.title;			
			_description = _Item.description;			
			_category = _Item.category;			
			_price = _Item.price;			
			_length = _Item.length;			
			_rating = _Item.rating;			
			_actors = _Item.actors;			
		}         

        public void CallOnCreated( bool _CallOnCreated )
        {
  			if( _CallOnCreated )
			{
	
				OnCreated( );
			}
        }
         
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                filmlistview.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<filmlistview>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
        }
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaving();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public filmlistview(Expression<Func<filmlistview, bool>> expression):this() {
            SetIsLoaded(_repo.Load(this,expression));
        }
        
        internal static IRepository<filmlistview> GetRepo(string connectionString, string providerName){
            Sakila.Data.SakilaDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Sakila.Data.SakilaDB();
            }else{
                db=new Sakila.Data.SakilaDB(connectionString, providerName);
            }
            IRepository<filmlistview> _repo;
            
            if(db.TestMode){
                filmlistview.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<filmlistview>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<filmlistview> GetRepo(){
            return GetRepo("","");
        }
        
        public static filmlistview SingleOrDefault(Expression<Func<filmlistview, bool>> expression) {
			return SingleOrDefault( expression, String.Empty, String.Empty );
        }      
        
        public static filmlistview SingleOrDefault(Expression<Func<filmlistview, bool>> expression,string connectionString, string providerName) {
            IRepository<filmlistview> repo = GetRepo(connectionString,providerName);
            filmlistview single = repo.SingleOrDefault<filmlistview>( expression );
            if( null != single )
            {
				single.OnLoaded( );
            }
            return single;
        }
        
        public static bool Exists(Expression<Func<filmlistview, bool>> expression,string connectionString, string providerName) {
            return All(connectionString,providerName).Any(expression);
        }        
        
        public static bool Exists(Expression<Func<filmlistview, bool>> expression) {
            return All().Any(expression);
        }        

		
        public static filmlistview GetByID(short value) 
        {
			return filmlistview.Find( L => L.FID.Equals( value ) ).FirstOrDefault( );
        }

        public static IList<filmlistview> Find(Expression<Func<filmlistview, bool>> expression) {
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<filmlistview> Find(Expression<Func<filmlistview, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();
        }

        public static IQueryable<filmlistview> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }

        public static IQueryable<filmlistview> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<filmlistview> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<filmlistview> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<filmlistview> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
        }

        public static PagedList<filmlistview> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
        }

        public string KeyName()
        {
            return "FID";
        }

        public object KeyValue()
        {
            return this.FID;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<short>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
//        public override string ToString()
//        {
//			string rc = string.Empty;
//			if( null != this.title )
//			{
//				rc = this.title.ToString();
//			}
//            return rc;
//        }

        public override bool Equals(object obj){
			if(obj == null)
				return false;
            if(obj is filmlistview){
                filmlistview compare=(filmlistview)obj;
                return compare.KeyValue().Equals( this.KeyValue() );
                //return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }



        public string DescriptorValue()
        {
            return this.title.ToString();
        }

        public string DescriptorColumn() {
            return "title";
        }

        public static string GetKeyColumn()
        {
            return "FID";
        }        

        public static string GetDescriptorColumn()
        {
            return "title";
        }
        
        #region ' Foreign Keys '
        #endregion

        short? _FID;
		[LocalData]
        public short? FID
        {
            get { return _FID; }
            set
            {
                if(_FID!=value){
                    _FID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="FID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _title;
		[LocalData]
        public string title
        {
            get { return _title; }
            set
            {
                if(_title!=value){
                    _title=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="title");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _description;
		[LocalData]
        public string description
        {
            get { return _description; }
            set
            {
                if(_description!=value){
                    _description=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="description");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _category;
		[LocalData]
        public string category
        {
            get { return _category; }
            set
            {
                if(_category!=value){
                    _category=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="category");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        decimal? _price;
		[LocalData]
        public decimal? price
        {
            get { return _price; }
            set
            {
                if(_price!=value){
                    _price=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="price");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        short? _length;
		[LocalData]
        public short? length
        {
            get { return _length; }
            set
            {
                if(_length!=value){
                    _length=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="length");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _rating;
		[LocalData]
        public string rating
        {
            get { return _rating; }
            set
            {
                if(_rating!=value){
                    _rating=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="rating");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _actors;
		[LocalData]
        public string actors
        {
            get { return _actors; }
            set
            {
                if(_actors!=value){
                    _actors=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="actors");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }


        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }

        public DbCommand GetInsertCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
			OnSaving( );
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        public void Add(IDataProvider provider){
			OnSaving( );

            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
        public void Save() {
            Save(_db.DataProvider);
        }      

        public void Save(IDataProvider provider) {
            if (_isNew) {
                Add(provider);
            } else {
                Update(provider);
            }
        }

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
                    }

        public void Delete() {
            Delete(_db.DataProvider);
        }

        public static void Delete(Expression<Func<filmlistview, bool>> expression) {
            var repo = GetRepo();
            
       
            repo.DeleteMany(expression);
        }

        
        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }

        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {
                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
    } 
    /// <summary>
    /// A class which represents the filmtext table in the Sakila Database.
    /// </summary>
    public partial class filmtext: IActiveRecord, ICallOnCreated
    {
    
        #region Built-in testing
        static TestRepository<filmtext> _testRepo;
       
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<filmtext>(new Sakila.Data.SakilaDB());
        }

        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }

        public static void Setup(List<filmtext> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }

        public static void Setup(filmtext item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }

        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                filmtext item=new filmtext();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;
        #endregion

        IRepository<filmtext> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }

        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Sakila.Data.SakilaDB _db;
        
        public filmtext(){
             _db=new Sakila.Data.SakilaDB();
            Init();            
        }

		///<summary>
		///Set bool to true to assign NewGuid to PrimaryKey (if appropriate) and to call OnCreated
		///</summary>
        public filmtext( bool _CallOnCreated ){
             _db=new Sakila.Data.SakilaDB();
            Init();  
            CallOnCreated( _CallOnCreated );
        }

        public filmtext(string connectionString, string providerName) {
            _db=new Sakila.Data.SakilaDB(connectionString, providerName);
            Init();            
        }

        public filmtext( filmtext _Item )
        {
			Copy( _Item );
             _db=new Sakila.Data.SakilaDB();
            Init();            
		}         
         
        public void Copy( filmtext _Item )
        {
			_title = _Item.title;			
			_description = _Item.description;			
		}         

        public void CallOnCreated( bool _CallOnCreated )
        {
  			if( _CallOnCreated )
			{
	
				OnCreated( );
			}
        }
         
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                filmtext.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<filmtext>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
        }
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaving();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public filmtext(Expression<Func<filmtext, bool>> expression):this() {
            SetIsLoaded(_repo.Load(this,expression));
        }
        
        internal static IRepository<filmtext> GetRepo(string connectionString, string providerName){
            Sakila.Data.SakilaDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Sakila.Data.SakilaDB();
            }else{
                db=new Sakila.Data.SakilaDB(connectionString, providerName);
            }
            IRepository<filmtext> _repo;
            
            if(db.TestMode){
                filmtext.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<filmtext>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<filmtext> GetRepo(){
            return GetRepo("","");
        }
        
        public static filmtext SingleOrDefault(Expression<Func<filmtext, bool>> expression) {
			return SingleOrDefault( expression, String.Empty, String.Empty );
        }      
        
        public static filmtext SingleOrDefault(Expression<Func<filmtext, bool>> expression,string connectionString, string providerName) {
            IRepository<filmtext> repo = GetRepo(connectionString,providerName);
            filmtext single = repo.SingleOrDefault<filmtext>( expression );
            if( null != single )
            {
				single.OnLoaded( );
            }
            return single;
        }
        
        public static bool Exists(Expression<Func<filmtext, bool>> expression,string connectionString, string providerName) {
            return All(connectionString,providerName).Any(expression);
        }        
        
        public static bool Exists(Expression<Func<filmtext, bool>> expression) {
            return All().Any(expression);
        }        

		
        public static filmtext GetByID(short value) 
        {
			return filmtext.Find( L => L.film_id.Equals( value ) ).FirstOrDefault( );
        }

        public static IList<filmtext> Find(Expression<Func<filmtext, bool>> expression) {
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<filmtext> Find(Expression<Func<filmtext, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();
        }

        public static IQueryable<filmtext> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }

        public static IQueryable<filmtext> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<filmtext> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<filmtext> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<filmtext> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
        }

        public static PagedList<filmtext> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
        }

        public string KeyName()
        {
            return "film_id";
        }

        public object KeyValue()
        {
            return this.film_id;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<short>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
//        public override string ToString()
//        {
//			string rc = string.Empty;
//			if( null != this.title )
//			{
//				rc = this.title.ToString();
//			}
//            return rc;
//        }

        public override bool Equals(object obj){
			if(obj == null)
				return false;
            if(obj is filmtext){
                filmtext compare=(filmtext)obj;
                return compare.KeyValue().Equals( this.KeyValue() );
                //return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }



        public string DescriptorValue()
        {
            return this.title.ToString();
        }

        public string DescriptorColumn() {
            return "title";
        }

        public static string GetKeyColumn()
        {
            return "film_id";
        }        

        public static string GetDescriptorColumn()
        {
            return "title";
        }
        
        #region ' Foreign Keys '
        #endregion

        short _film_id;
		[LocalData]
        public short film_id
        {
            get { return _film_id; }
            set
            {
                if(_film_id!=value){
                    _film_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="film_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _title;
		[LocalData]
        public string title
        {
            get { return _title; }
            set
            {
                if(_title!=value){
                    _title=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="title");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _description;
		[LocalData]
        public string description
        {
            get { return _description; }
            set
            {
                if(_description!=value){
                    _description=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="description");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }


        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }

        public DbCommand GetInsertCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
			OnSaving( );
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        public void Add(IDataProvider provider){
			OnSaving( );

            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
        public void Save() {
            Save(_db.DataProvider);
        }      

        public void Save(IDataProvider provider) {
            if (_isNew) {
                Add(provider);
            } else {
                Update(provider);
            }
        }

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
                    }

        public void Delete() {
            Delete(_db.DataProvider);
        }

        public static void Delete(Expression<Func<filmtext, bool>> expression) {
            var repo = GetRepo();
            
       
            repo.DeleteMany(expression);
        }

        
        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }

        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {
                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
    } 
    /// <summary>
    /// A class which represents the inventory table in the Sakila Database.
    /// </summary>
    public partial class inventory: IActiveRecord, ICallOnCreated
    {
    
        #region Built-in testing
        static TestRepository<inventory> _testRepo;
       
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<inventory>(new Sakila.Data.SakilaDB());
        }

        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }

        public static void Setup(List<inventory> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }

        public static void Setup(inventory item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }

        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                inventory item=new inventory();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;
        #endregion

        IRepository<inventory> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }

        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Sakila.Data.SakilaDB _db;
        
        public inventory(){
             _db=new Sakila.Data.SakilaDB();
            Init();            
        }

		///<summary>
		///Set bool to true to assign NewGuid to PrimaryKey (if appropriate) and to call OnCreated
		///</summary>
        public inventory( bool _CallOnCreated ){
             _db=new Sakila.Data.SakilaDB();
            Init();  
            CallOnCreated( _CallOnCreated );
        }

        public inventory(string connectionString, string providerName) {
            _db=new Sakila.Data.SakilaDB(connectionString, providerName);
            Init();            
        }

        public inventory( inventory _Item )
        {
			Copy( _Item );
             _db=new Sakila.Data.SakilaDB();
            Init();            
		}         
         
        public void Copy( inventory _Item )
        {
			_film_id = _Item.film_id;			
			_store_id = _Item.store_id;			
			_last_update = _Item.last_update;			
		}         

        public void CallOnCreated( bool _CallOnCreated )
        {
  			if( _CallOnCreated )
			{
	
				OnCreated( );
			}
        }
         
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                inventory.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<inventory>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
        }
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaving();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public inventory(Expression<Func<inventory, bool>> expression):this() {
            SetIsLoaded(_repo.Load(this,expression));
        }
        
        internal static IRepository<inventory> GetRepo(string connectionString, string providerName){
            Sakila.Data.SakilaDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Sakila.Data.SakilaDB();
            }else{
                db=new Sakila.Data.SakilaDB(connectionString, providerName);
            }
            IRepository<inventory> _repo;
            
            if(db.TestMode){
                inventory.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<inventory>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<inventory> GetRepo(){
            return GetRepo("","");
        }
        
        public static inventory SingleOrDefault(Expression<Func<inventory, bool>> expression) {
			return SingleOrDefault( expression, String.Empty, String.Empty );
        }      
        
        public static inventory SingleOrDefault(Expression<Func<inventory, bool>> expression,string connectionString, string providerName) {
            IRepository<inventory> repo = GetRepo(connectionString,providerName);
            inventory single = repo.SingleOrDefault<inventory>( expression );
            if( null != single )
            {
				single.OnLoaded( );
            }
            return single;
        }
        
        public static bool Exists(Expression<Func<inventory, bool>> expression,string connectionString, string providerName) {
            return All(connectionString,providerName).Any(expression);
        }        
        
        public static bool Exists(Expression<Func<inventory, bool>> expression) {
            return All().Any(expression);
        }        

		
        public static inventory GetByID(string value) 
        {
			return inventory.Find( L => L.inventory_id.Equals( value ) ).FirstOrDefault( );
        }

        public static IList<inventory> Find(Expression<Func<inventory, bool>> expression) {
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<inventory> Find(Expression<Func<inventory, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();
        }

        public static IQueryable<inventory> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }

        public static IQueryable<inventory> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<inventory> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<inventory> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<inventory> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
        }

        public static PagedList<inventory> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
        }

        public string KeyName()
        {
            return "inventory_id";
        }

        public object KeyValue()
        {
            return this.inventory_id;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<string>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
//        public override string ToString()
//        {
//			string rc = string.Empty;
//			if( null != this.inventory_id )
//			{
//				rc = this.inventory_id.ToString();
//			}
//            return rc;
//        }

        public override bool Equals(object obj){
			if(obj == null)
				return false;
            if(obj is inventory){
                inventory compare=(inventory)obj;
                return compare.KeyValue().Equals( this.KeyValue() );
                //return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }



        public string DescriptorValue()
        {
            return this.inventory_id.ToString();
        }

        public string DescriptorColumn() {
            return "inventory_id";
        }

        public static string GetKeyColumn()
        {
            return "inventory_id";
        }        

        public static string GetDescriptorColumn()
        {
            return "inventory_id";
        }
        
        #region ' Foreign Keys '
        public IQueryable<film> inventoryfilm
        {
            get
            {
                  var repo=Sakila.Data.film.GetRepo();
                  return from items in repo.GetAll()
                       where items.film_id == _film_id
                       select items;
            }
        }
        public IQueryable<store> inventorystore
        {
            get
            {
                  var repo=Sakila.Data.store.GetRepo();
                  return from items in repo.GetAll()
                       where items.store_id == _store_id
                       select items;
            }
        }
        public IQueryable<rental> rentals
        {
            get
            {
                  var repo=Sakila.Data.rental.GetRepo();
                  return from items in repo.GetAll()
                       where items.inventory_id == _inventory_id
                       select items;
            }
        }
        #endregion

        string _inventory_id;
		[LocalData]
        public string inventory_id
        {
            get { return _inventory_id; }
            set
            {
                if(_inventory_id!=value){
                    _inventory_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="inventory_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        short _film_id;
		[LocalData]
        public short film_id
        {
            get { return _film_id; }
            set
            {
                if(_film_id!=value){
                    _film_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="film_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        bool _store_id;
		[LocalData]
        public bool store_id
        {
            get { return _store_id; }
            set
            {
                if(_store_id!=value){
                    _store_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="store_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _last_update;
		[LocalData]
        public DateTime last_update
        {
            get { return _last_update; }
            set
            {
                if(_last_update!=value){
                    _last_update=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="last_update");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }


        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }

        public DbCommand GetInsertCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
			OnSaving( );
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        public void Add(IDataProvider provider){
			OnSaving( );

            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
        public void Save() {
            Save(_db.DataProvider);
        }      

        public void Save(IDataProvider provider) {
            if (_isNew) {
                Add(provider);
            } else {
                Update(provider);
            }
        }

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
                    }

        public void Delete() {
            Delete(_db.DataProvider);
        }

        public static void Delete(Expression<Func<inventory, bool>> expression) {
            var repo = GetRepo();
            
       
            repo.DeleteMany(expression);
        }

        
        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }

        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {
                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
    } 
    /// <summary>
    /// A class which represents the language table in the Sakila Database.
    /// </summary>
    public partial class language: IActiveRecord, ICallOnCreated
    {
    
        #region Built-in testing
        static TestRepository<language> _testRepo;
       
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<language>(new Sakila.Data.SakilaDB());
        }

        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }

        public static void Setup(List<language> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }

        public static void Setup(language item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }

        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                language item=new language();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;
        #endregion

        IRepository<language> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }

        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Sakila.Data.SakilaDB _db;
        
        public language(){
             _db=new Sakila.Data.SakilaDB();
            Init();            
        }

		///<summary>
		///Set bool to true to assign NewGuid to PrimaryKey (if appropriate) and to call OnCreated
		///</summary>
        public language( bool _CallOnCreated ){
             _db=new Sakila.Data.SakilaDB();
            Init();  
            CallOnCreated( _CallOnCreated );
        }

        public language(string connectionString, string providerName) {
            _db=new Sakila.Data.SakilaDB(connectionString, providerName);
            Init();            
        }

        public language( language _Item )
        {
			Copy( _Item );
             _db=new Sakila.Data.SakilaDB();
            Init();            
		}         
         
        public void Copy( language _Item )
        {
			_name = _Item.name;			
			_last_update = _Item.last_update;			
		}         

        public void CallOnCreated( bool _CallOnCreated )
        {
  			if( _CallOnCreated )
			{
	
				OnCreated( );
			}
        }
         
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                language.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<language>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
        }
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaving();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public language(Expression<Func<language, bool>> expression):this() {
            SetIsLoaded(_repo.Load(this,expression));
        }
        
        internal static IRepository<language> GetRepo(string connectionString, string providerName){
            Sakila.Data.SakilaDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Sakila.Data.SakilaDB();
            }else{
                db=new Sakila.Data.SakilaDB(connectionString, providerName);
            }
            IRepository<language> _repo;
            
            if(db.TestMode){
                language.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<language>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<language> GetRepo(){
            return GetRepo("","");
        }
        
        public static language SingleOrDefault(Expression<Func<language, bool>> expression) {
			return SingleOrDefault( expression, String.Empty, String.Empty );
        }      
        
        public static language SingleOrDefault(Expression<Func<language, bool>> expression,string connectionString, string providerName) {
            IRepository<language> repo = GetRepo(connectionString,providerName);
            language single = repo.SingleOrDefault<language>( expression );
            if( null != single )
            {
				single.OnLoaded( );
            }
            return single;
        }
        
        public static bool Exists(Expression<Func<language, bool>> expression,string connectionString, string providerName) {
            return All(connectionString,providerName).Any(expression);
        }        
        
        public static bool Exists(Expression<Func<language, bool>> expression) {
            return All().Any(expression);
        }        

		
        public static language GetByID(bool value) 
        {
			return language.Find( L => L.language_id.Equals( value ) ).FirstOrDefault( );
        }

        public static IList<language> Find(Expression<Func<language, bool>> expression) {
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<language> Find(Expression<Func<language, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();
        }

        public static IQueryable<language> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }

        public static IQueryable<language> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<language> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<language> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<language> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
        }

        public static PagedList<language> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
        }

        public string KeyName()
        {
            return "language_id";
        }

        public object KeyValue()
        {
            return this.language_id;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<bool>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
//        public override string ToString()
//        {
//			string rc = string.Empty;
//			if( null != this.name )
//			{
//				rc = this.name.ToString();
//			}
//            return rc;
//        }

        public override bool Equals(object obj){
			if(obj == null)
				return false;
            if(obj is language){
                language compare=(language)obj;
                return compare.KeyValue().Equals( this.KeyValue() );
                //return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }



        public string DescriptorValue()
        {
            return this.name.ToString();
        }

        public string DescriptorColumn() {
            return "name";
        }

        public static string GetKeyColumn()
        {
            return "language_id";
        }        

        public static string GetDescriptorColumn()
        {
            return "name";
        }
        
        #region ' Foreign Keys '
        public IQueryable<film> films
        {
            get
            {
                  var repo=Sakila.Data.film.GetRepo();
                  return from items in repo.GetAll()
                       where items.language_id == _language_id
                       select items;
            }
        }
        public IQueryable<film> films1
        {
            get
            {
                  var repo=Sakila.Data.film.GetRepo();
                  return from items in repo.GetAll()
                       where items.original_language_id == _language_id
                       select items;
            }
        }
        #endregion

        bool _language_id;
		[LocalData]
        public bool language_id
        {
            get { return _language_id; }
            set
            {
                if(_language_id!=value){
                    _language_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="language_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _name;
		[LocalData]
        public string name
        {
            get { return _name; }
            set
            {
                if(_name!=value){
                    _name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _last_update;
		[LocalData]
        public DateTime last_update
        {
            get { return _last_update; }
            set
            {
                if(_last_update!=value){
                    _last_update=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="last_update");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }


        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }

        public DbCommand GetInsertCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
			OnSaving( );
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        public void Add(IDataProvider provider){
			OnSaving( );

            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
        public void Save() {
            Save(_db.DataProvider);
        }      

        public void Save(IDataProvider provider) {
            if (_isNew) {
                Add(provider);
            } else {
                Update(provider);
            }
        }

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
                    }

        public void Delete() {
            Delete(_db.DataProvider);
        }

        public static void Delete(Expression<Func<language, bool>> expression) {
            var repo = GetRepo();
            
       
            repo.DeleteMany(expression);
        }

        
        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }

        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {
                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
    } 
    /// <summary>
    /// A class which represents the payment table in the Sakila Database.
    /// </summary>
    public partial class payment: IActiveRecord, ICallOnCreated
    {
    
        #region Built-in testing
        static TestRepository<payment> _testRepo;
       
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<payment>(new Sakila.Data.SakilaDB());
        }

        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }

        public static void Setup(List<payment> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }

        public static void Setup(payment item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }

        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                payment item=new payment();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;
        #endregion

        IRepository<payment> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }

        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Sakila.Data.SakilaDB _db;
        
        public payment(){
             _db=new Sakila.Data.SakilaDB();
            Init();            
        }

		///<summary>
		///Set bool to true to assign NewGuid to PrimaryKey (if appropriate) and to call OnCreated
		///</summary>
        public payment( bool _CallOnCreated ){
             _db=new Sakila.Data.SakilaDB();
            Init();  
            CallOnCreated( _CallOnCreated );
        }

        public payment(string connectionString, string providerName) {
            _db=new Sakila.Data.SakilaDB(connectionString, providerName);
            Init();            
        }

        public payment( payment _Item )
        {
			Copy( _Item );
             _db=new Sakila.Data.SakilaDB();
            Init();            
		}         
         
        public void Copy( payment _Item )
        {
			_customer_id = _Item.customer_id;			
			_staff_id = _Item.staff_id;			
			_rental_id = _Item.rental_id;			
			_amount = _Item.amount;			
			_payment_date = _Item.payment_date;			
			_last_update = _Item.last_update;			
		}         

        public void CallOnCreated( bool _CallOnCreated )
        {
  			if( _CallOnCreated )
			{
	
				OnCreated( );
			}
        }
         
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                payment.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<payment>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
        }
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaving();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public payment(Expression<Func<payment, bool>> expression):this() {
            SetIsLoaded(_repo.Load(this,expression));
        }
        
        internal static IRepository<payment> GetRepo(string connectionString, string providerName){
            Sakila.Data.SakilaDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Sakila.Data.SakilaDB();
            }else{
                db=new Sakila.Data.SakilaDB(connectionString, providerName);
            }
            IRepository<payment> _repo;
            
            if(db.TestMode){
                payment.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<payment>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<payment> GetRepo(){
            return GetRepo("","");
        }
        
        public static payment SingleOrDefault(Expression<Func<payment, bool>> expression) {
			return SingleOrDefault( expression, String.Empty, String.Empty );
        }      
        
        public static payment SingleOrDefault(Expression<Func<payment, bool>> expression,string connectionString, string providerName) {
            IRepository<payment> repo = GetRepo(connectionString,providerName);
            payment single = repo.SingleOrDefault<payment>( expression );
            if( null != single )
            {
				single.OnLoaded( );
            }
            return single;
        }
        
        public static bool Exists(Expression<Func<payment, bool>> expression,string connectionString, string providerName) {
            return All(connectionString,providerName).Any(expression);
        }        
        
        public static bool Exists(Expression<Func<payment, bool>> expression) {
            return All().Any(expression);
        }        

		
        public static payment GetByID(short value) 
        {
			return payment.Find( L => L.payment_id.Equals( value ) ).FirstOrDefault( );
        }

        public static IList<payment> Find(Expression<Func<payment, bool>> expression) {
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<payment> Find(Expression<Func<payment, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();
        }

        public static IQueryable<payment> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }

        public static IQueryable<payment> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<payment> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<payment> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<payment> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
        }

        public static PagedList<payment> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
        }

        public string KeyName()
        {
            return "payment_id";
        }

        public object KeyValue()
        {
            return this.payment_id;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<short>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
//        public override string ToString()
//        {
//			string rc = string.Empty;
//			if( null != this.customer_id )
//			{
//				rc = this.customer_id.ToString();
//			}
//            return rc;
//        }

        public override bool Equals(object obj){
			if(obj == null)
				return false;
            if(obj is payment){
                payment compare=(payment)obj;
                return compare.KeyValue().Equals( this.KeyValue() );
                //return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }



        public string DescriptorValue()
        {
            return this.customer_id.ToString();
        }

        public string DescriptorColumn() {
            return "customer_id";
        }

        public static string GetKeyColumn()
        {
            return "payment_id";
        }        

        public static string GetDescriptorColumn()
        {
            return "customer_id";
        }
        
        #region ' Foreign Keys '
        public IQueryable<customer> paymentcustomer
        {
            get
            {
                  var repo=Sakila.Data.customer.GetRepo();
                  return from items in repo.GetAll()
                       where items.customer_id == _customer_id
                       select items;
            }
        }
        public IQueryable<rental> paymentrental
        {
            get
            {
                  var repo=Sakila.Data.rental.GetRepo();
                  return from items in repo.GetAll()
                       where items.rental_id == _rental_id
                       select items;
            }
        }
        public IQueryable<staff> paymentstaff
        {
            get
            {
                  var repo=Sakila.Data.staff.GetRepo();
                  return from items in repo.GetAll()
                       where items.staff_id == _staff_id
                       select items;
            }
        }
        #endregion

        short _payment_id;
		[LocalData]
        public short payment_id
        {
            get { return _payment_id; }
            set
            {
                if(_payment_id!=value){
                    _payment_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="payment_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        short _customer_id;
		[LocalData]
        public short customer_id
        {
            get { return _customer_id; }
            set
            {
                if(_customer_id!=value){
                    _customer_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="customer_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        bool _staff_id;
		[LocalData]
        public bool staff_id
        {
            get { return _staff_id; }
            set
            {
                if(_staff_id!=value){
                    _staff_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="staff_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int? _rental_id;
		[LocalData]
        public int? rental_id
        {
            get { return _rental_id; }
            set
            {
                if(_rental_id!=value){
                    _rental_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="rental_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        decimal _amount;
		[LocalData]
        public decimal amount
        {
            get { return _amount; }
            set
            {
                if(_amount!=value){
                    _amount=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="amount");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _payment_date;
		[LocalData]
        public DateTime payment_date
        {
            get { return _payment_date; }
            set
            {
                if(_payment_date!=value){
                    _payment_date=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="payment_date");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _last_update;
		[LocalData]
        public DateTime last_update
        {
            get { return _last_update; }
            set
            {
                if(_last_update!=value){
                    _last_update=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="last_update");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }


        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }

        public DbCommand GetInsertCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
			OnSaving( );
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        public void Add(IDataProvider provider){
			OnSaving( );

            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
        public void Save() {
            Save(_db.DataProvider);
        }      

        public void Save(IDataProvider provider) {
            if (_isNew) {
                Add(provider);
            } else {
                Update(provider);
            }
        }

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
                    }

        public void Delete() {
            Delete(_db.DataProvider);
        }

        public static void Delete(Expression<Func<payment, bool>> expression) {
            var repo = GetRepo();
            
       
            repo.DeleteMany(expression);
        }

        
        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }

        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {
                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
    } 
    /// <summary>
    /// A class which represents the rental table in the Sakila Database.
    /// </summary>
    public partial class rental: IActiveRecord, ICallOnCreated
    {
    
        #region Built-in testing
        static TestRepository<rental> _testRepo;
       
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<rental>(new Sakila.Data.SakilaDB());
        }

        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }

        public static void Setup(List<rental> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }

        public static void Setup(rental item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }

        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                rental item=new rental();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;
        #endregion

        IRepository<rental> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }

        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Sakila.Data.SakilaDB _db;
        
        public rental(){
             _db=new Sakila.Data.SakilaDB();
            Init();            
        }

		///<summary>
		///Set bool to true to assign NewGuid to PrimaryKey (if appropriate) and to call OnCreated
		///</summary>
        public rental( bool _CallOnCreated ){
             _db=new Sakila.Data.SakilaDB();
            Init();  
            CallOnCreated( _CallOnCreated );
        }

        public rental(string connectionString, string providerName) {
            _db=new Sakila.Data.SakilaDB(connectionString, providerName);
            Init();            
        }

        public rental( rental _Item )
        {
			Copy( _Item );
             _db=new Sakila.Data.SakilaDB();
            Init();            
		}         
         
        public void Copy( rental _Item )
        {
			_rental_date = _Item.rental_date;			
			_inventory_id = _Item.inventory_id;			
			_customer_id = _Item.customer_id;			
			_return_date = _Item.return_date;			
			_staff_id = _Item.staff_id;			
			_last_update = _Item.last_update;			
		}         

        public void CallOnCreated( bool _CallOnCreated )
        {
  			if( _CallOnCreated )
			{
	
				OnCreated( );
			}
        }
         
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                rental.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<rental>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
        }
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaving();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public rental(Expression<Func<rental, bool>> expression):this() {
            SetIsLoaded(_repo.Load(this,expression));
        }
        
        internal static IRepository<rental> GetRepo(string connectionString, string providerName){
            Sakila.Data.SakilaDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Sakila.Data.SakilaDB();
            }else{
                db=new Sakila.Data.SakilaDB(connectionString, providerName);
            }
            IRepository<rental> _repo;
            
            if(db.TestMode){
                rental.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<rental>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<rental> GetRepo(){
            return GetRepo("","");
        }
        
        public static rental SingleOrDefault(Expression<Func<rental, bool>> expression) {
			return SingleOrDefault( expression, String.Empty, String.Empty );
        }      
        
        public static rental SingleOrDefault(Expression<Func<rental, bool>> expression,string connectionString, string providerName) {
            IRepository<rental> repo = GetRepo(connectionString,providerName);
            rental single = repo.SingleOrDefault<rental>( expression );
            if( null != single )
            {
				single.OnLoaded( );
            }
            return single;
        }
        
        public static bool Exists(Expression<Func<rental, bool>> expression,string connectionString, string providerName) {
            return All(connectionString,providerName).Any(expression);
        }        
        
        public static bool Exists(Expression<Func<rental, bool>> expression) {
            return All().Any(expression);
        }        

		
        public static rental GetByID(int value) 
        {
			return rental.Find( L => L.rental_id.Equals( value ) ).FirstOrDefault( );
        }

        public static IList<rental> Find(Expression<Func<rental, bool>> expression) {
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<rental> Find(Expression<Func<rental, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();
        }

        public static IQueryable<rental> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }

        public static IQueryable<rental> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<rental> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<rental> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<rental> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
        }

        public static PagedList<rental> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
        }

        public string KeyName()
        {
            return "rental_id";
        }

        public object KeyValue()
        {
            return this.rental_id;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
//        public override string ToString()
//        {
//			string rc = string.Empty;
//			if( null != this.inventory_id )
//			{
//				rc = this.inventory_id.ToString();
//			}
//            return rc;
//        }

        public override bool Equals(object obj){
			if(obj == null)
				return false;
            if(obj is rental){
                rental compare=(rental)obj;
                return compare.KeyValue().Equals( this.KeyValue() );
                //return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        public override int GetHashCode() {
            return this.rental_id;
        }


        public string DescriptorValue()
        {
            return this.inventory_id.ToString();
        }

        public string DescriptorColumn() {
            return "inventory_id";
        }

        public static string GetKeyColumn()
        {
            return "rental_id";
        }        

        public static string GetDescriptorColumn()
        {
            return "inventory_id";
        }
        
        #region ' Foreign Keys '
        public IQueryable<payment> payments
        {
            get
            {
                  var repo=Sakila.Data.payment.GetRepo();
                  return from items in repo.GetAll()
                       where items.rental_id == _rental_id
                       select items;
            }
        }
        public IQueryable<customer> rentalcustomer
        {
            get
            {
                  var repo=Sakila.Data.customer.GetRepo();
                  return from items in repo.GetAll()
                       where items.customer_id == _customer_id
                       select items;
            }
        }
        public IQueryable<inventory> rentalinventory
        {
            get
            {
                  var repo=Sakila.Data.inventory.GetRepo();
                  return from items in repo.GetAll()
                       where items.inventory_id == _inventory_id
                       select items;
            }
        }
        public IQueryable<staff> rentalstaff
        {
            get
            {
                  var repo=Sakila.Data.staff.GetRepo();
                  return from items in repo.GetAll()
                       where items.staff_id == _staff_id
                       select items;
            }
        }
        #endregion

        int _rental_id;
		[LocalData]
        public int rental_id
        {
            get { return _rental_id; }
            set
            {
                if(_rental_id!=value){
                    _rental_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="rental_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _rental_date;
		[LocalData]
        public DateTime rental_date
        {
            get { return _rental_date; }
            set
            {
                if(_rental_date!=value){
                    _rental_date=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="rental_date");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _inventory_id;
		[LocalData]
        public string inventory_id
        {
            get { return _inventory_id; }
            set
            {
                if(_inventory_id!=value){
                    _inventory_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="inventory_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        short _customer_id;
		[LocalData]
        public short customer_id
        {
            get { return _customer_id; }
            set
            {
                if(_customer_id!=value){
                    _customer_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="customer_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime? _return_date;
		[LocalData]
        public DateTime? return_date
        {
            get { return _return_date; }
            set
            {
                if(_return_date!=value){
                    _return_date=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="return_date");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        bool _staff_id;
		[LocalData]
        public bool staff_id
        {
            get { return _staff_id; }
            set
            {
                if(_staff_id!=value){
                    _staff_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="staff_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _last_update;
		[LocalData]
        public DateTime last_update
        {
            get { return _last_update; }
            set
            {
                if(_last_update!=value){
                    _last_update=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="last_update");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }


        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }

        public DbCommand GetInsertCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
			OnSaving( );
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        public void Add(IDataProvider provider){
			OnSaving( );

            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
        public void Save() {
            Save(_db.DataProvider);
        }      

        public void Save(IDataProvider provider) {
            if (_isNew) {
                Add(provider);
            } else {
                Update(provider);
            }
        }

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
                    }

        public void Delete() {
            Delete(_db.DataProvider);
        }

        public static void Delete(Expression<Func<rental, bool>> expression) {
            var repo = GetRepo();
            
       
            repo.DeleteMany(expression);
        }

        
        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }

        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {
                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
    } 
    /// <summary>
    /// A class which represents the staff table in the Sakila Database.
    /// </summary>
    public partial class staff: IActiveRecord, ICallOnCreated
    {
    
        #region Built-in testing
        static TestRepository<staff> _testRepo;
       
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<staff>(new Sakila.Data.SakilaDB());
        }

        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }

        public static void Setup(List<staff> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }

        public static void Setup(staff item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }

        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                staff item=new staff();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;
        #endregion

        IRepository<staff> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }

        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Sakila.Data.SakilaDB _db;
        
        public staff(){
             _db=new Sakila.Data.SakilaDB();
            Init();            
        }

		///<summary>
		///Set bool to true to assign NewGuid to PrimaryKey (if appropriate) and to call OnCreated
		///</summary>
        public staff( bool _CallOnCreated ){
             _db=new Sakila.Data.SakilaDB();
            Init();  
            CallOnCreated( _CallOnCreated );
        }

        public staff(string connectionString, string providerName) {
            _db=new Sakila.Data.SakilaDB(connectionString, providerName);
            Init();            
        }

        public staff( staff _Item )
        {
			Copy( _Item );
             _db=new Sakila.Data.SakilaDB();
            Init();            
		}         
         
        public void Copy( staff _Item )
        {
			_first_name = _Item.first_name;			
			_last_name = _Item.last_name;			
			_address_id = _Item.address_id;			
			_picture = _Item.picture;			
			_email = _Item.email;			
			_store_id = _Item.store_id;			
			_active = _Item.active;			
			_username = _Item.username;			
			_password = _Item.password;			
			_last_update = _Item.last_update;			
		}         

        public void CallOnCreated( bool _CallOnCreated )
        {
  			if( _CallOnCreated )
			{
	
				OnCreated( );
			}
        }
         
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                staff.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<staff>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
        }
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaving();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public staff(Expression<Func<staff, bool>> expression):this() {
            SetIsLoaded(_repo.Load(this,expression));
        }
        
        internal static IRepository<staff> GetRepo(string connectionString, string providerName){
            Sakila.Data.SakilaDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Sakila.Data.SakilaDB();
            }else{
                db=new Sakila.Data.SakilaDB(connectionString, providerName);
            }
            IRepository<staff> _repo;
            
            if(db.TestMode){
                staff.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<staff>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<staff> GetRepo(){
            return GetRepo("","");
        }
        
        public static staff SingleOrDefault(Expression<Func<staff, bool>> expression) {
			return SingleOrDefault( expression, String.Empty, String.Empty );
        }      
        
        public static staff SingleOrDefault(Expression<Func<staff, bool>> expression,string connectionString, string providerName) {
            IRepository<staff> repo = GetRepo(connectionString,providerName);
            staff single = repo.SingleOrDefault<staff>( expression );
            if( null != single )
            {
				single.OnLoaded( );
            }
            return single;
        }
        
        public static bool Exists(Expression<Func<staff, bool>> expression,string connectionString, string providerName) {
            return All(connectionString,providerName).Any(expression);
        }        
        
        public static bool Exists(Expression<Func<staff, bool>> expression) {
            return All().Any(expression);
        }        

		
        public static staff GetByID(bool value) 
        {
			return staff.Find( L => L.staff_id.Equals( value ) ).FirstOrDefault( );
        }

        public static IList<staff> Find(Expression<Func<staff, bool>> expression) {
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<staff> Find(Expression<Func<staff, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();
        }

        public static IQueryable<staff> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }

        public static IQueryable<staff> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<staff> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<staff> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<staff> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
        }

        public static PagedList<staff> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
        }

        public string KeyName()
        {
            return "staff_id";
        }

        public object KeyValue()
        {
            return this.staff_id;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<bool>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
//        public override string ToString()
//        {
//			string rc = string.Empty;
//			if( null != this.first_name )
//			{
//				rc = this.first_name.ToString();
//			}
//            return rc;
//        }

        public override bool Equals(object obj){
			if(obj == null)
				return false;
            if(obj is staff){
                staff compare=(staff)obj;
                return compare.KeyValue().Equals( this.KeyValue() );
                //return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }



        public string DescriptorValue()
        {
            return this.first_name.ToString();
        }

        public string DescriptorColumn() {
            return "first_name";
        }

        public static string GetKeyColumn()
        {
            return "staff_id";
        }        

        public static string GetDescriptorColumn()
        {
            return "first_name";
        }
        
        #region ' Foreign Keys '
        public IQueryable<payment> payments
        {
            get
            {
                  var repo=Sakila.Data.payment.GetRepo();
                  return from items in repo.GetAll()
                       where items.staff_id == _staff_id
                       select items;
            }
        }
        public IQueryable<rental> rentals
        {
            get
            {
                  var repo=Sakila.Data.rental.GetRepo();
                  return from items in repo.GetAll()
                       where items.staff_id == _staff_id
                       select items;
            }
        }
        public IQueryable<address> staffaddress
        {
            get
            {
                  var repo=Sakila.Data.address.GetRepo();
                  return from items in repo.GetAll()
                       where items.address_id == _address_id
                       select items;
            }
        }
        public IQueryable<store> staffstore
        {
            get
            {
                  var repo=Sakila.Data.store.GetRepo();
                  return from items in repo.GetAll()
                       where items.store_id == _store_id
                       select items;
            }
        }
        public IQueryable<store> stores
        {
            get
            {
                  var repo=Sakila.Data.store.GetRepo();
                  return from items in repo.GetAll()
                       where items.manager_staff_id == _staff_id
                       select items;
            }
        }
        #endregion

        bool _staff_id;
		[LocalData]
        public bool staff_id
        {
            get { return _staff_id; }
            set
            {
                if(_staff_id!=value){
                    _staff_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="staff_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _first_name;
		[LocalData]
        public string first_name
        {
            get { return _first_name; }
            set
            {
                if(_first_name!=value){
                    _first_name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="first_name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _last_name;
		[LocalData]
        public string last_name
        {
            get { return _last_name; }
            set
            {
                if(_last_name!=value){
                    _last_name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="last_name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        short _address_id;
		[LocalData]
        public short address_id
        {
            get { return _address_id; }
            set
            {
                if(_address_id!=value){
                    _address_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="address_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _picture;
		[LocalData]
        public string picture
        {
            get { return _picture; }
            set
            {
                if(_picture!=value){
                    _picture=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="picture");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _email;
		[LocalData]
        public string email
        {
            get { return _email; }
            set
            {
                if(_email!=value){
                    _email=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="email");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        bool _store_id;
		[LocalData]
        public bool store_id
        {
            get { return _store_id; }
            set
            {
                if(_store_id!=value){
                    _store_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="store_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        bool _active;
		[LocalData]
        public bool active
        {
            get { return _active; }
            set
            {
                if(_active!=value){
                    _active=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="active");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _username;
		[LocalData]
        public string username
        {
            get { return _username; }
            set
            {
                if(_username!=value){
                    _username=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="username");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _password;
		[LocalData]
        public string password
        {
            get { return _password; }
            set
            {
                if(_password!=value){
                    _password=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="password");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _last_update;
		[LocalData]
        public DateTime last_update
        {
            get { return _last_update; }
            set
            {
                if(_last_update!=value){
                    _last_update=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="last_update");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }


        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }

        public DbCommand GetInsertCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
			OnSaving( );
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        public void Add(IDataProvider provider){
			OnSaving( );

            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
        public void Save() {
            Save(_db.DataProvider);
        }      

        public void Save(IDataProvider provider) {
            if (_isNew) {
                Add(provider);
            } else {
                Update(provider);
            }
        }

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
                    }

        public void Delete() {
            Delete(_db.DataProvider);
        }

        public static void Delete(Expression<Func<staff, bool>> expression) {
            var repo = GetRepo();
            
       
            repo.DeleteMany(expression);
        }

        
        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }

        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {
                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
    } 
    /// <summary>
    /// A class which represents the store table in the Sakila Database.
    /// </summary>
    public partial class store: IActiveRecord, ICallOnCreated
    {
    
        #region Built-in testing
        static TestRepository<store> _testRepo;
       
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<store>(new Sakila.Data.SakilaDB());
        }

        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }

        public static void Setup(List<store> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }

        public static void Setup(store item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }

        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                store item=new store();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;
        #endregion

        IRepository<store> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }

        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        Sakila.Data.SakilaDB _db;
        
        public store(){
             _db=new Sakila.Data.SakilaDB();
            Init();            
        }

		///<summary>
		///Set bool to true to assign NewGuid to PrimaryKey (if appropriate) and to call OnCreated
		///</summary>
        public store( bool _CallOnCreated ){
             _db=new Sakila.Data.SakilaDB();
            Init();  
            CallOnCreated( _CallOnCreated );
        }

        public store(string connectionString, string providerName) {
            _db=new Sakila.Data.SakilaDB(connectionString, providerName);
            Init();            
        }

        public store( store _Item )
        {
			Copy( _Item );
             _db=new Sakila.Data.SakilaDB();
            Init();            
		}         
         
        public void Copy( store _Item )
        {
			_manager_staff_id = _Item.manager_staff_id;			
			_address_id = _Item.address_id;			
			_last_update = _Item.last_update;			
		}         

        public void CallOnCreated( bool _CallOnCreated )
        {
  			if( _CallOnCreated )
			{
	
				OnCreated( );
			}
        }
         
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                store.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<store>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
        }
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaving();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public store(Expression<Func<store, bool>> expression):this() {
            SetIsLoaded(_repo.Load(this,expression));
        }
        
        internal static IRepository<store> GetRepo(string connectionString, string providerName){
            Sakila.Data.SakilaDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new Sakila.Data.SakilaDB();
            }else{
                db=new Sakila.Data.SakilaDB(connectionString, providerName);
            }
            IRepository<store> _repo;
            
            if(db.TestMode){
                store.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<store>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<store> GetRepo(){
            return GetRepo("","");
        }
        
        public static store SingleOrDefault(Expression<Func<store, bool>> expression) {
			return SingleOrDefault( expression, String.Empty, String.Empty );
        }      
        
        public static store SingleOrDefault(Expression<Func<store, bool>> expression,string connectionString, string providerName) {
            IRepository<store> repo = GetRepo(connectionString,providerName);
            store single = repo.SingleOrDefault<store>( expression );
            if( null != single )
            {
				single.OnLoaded( );
            }
            return single;
        }
        
        public static bool Exists(Expression<Func<store, bool>> expression,string connectionString, string providerName) {
            return All(connectionString,providerName).Any(expression);
        }        
        
        public static bool Exists(Expression<Func<store, bool>> expression) {
            return All().Any(expression);
        }        

		
        public static store GetByID(bool value) 
        {
			return store.Find( L => L.store_id.Equals( value ) ).FirstOrDefault( );
        }

        public static IList<store> Find(Expression<Func<store, bool>> expression) {
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<store> Find(Expression<Func<store, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();
        }

        public static IQueryable<store> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }

        public static IQueryable<store> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<store> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<store> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<store> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
        }

        public static PagedList<store> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
        }

        public string KeyName()
        {
            return "store_id";
        }

        public object KeyValue()
        {
            return this.store_id;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<bool>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
//        public override string ToString()
//        {
//			string rc = string.Empty;
//			if( null != this.manager_staff_id )
//			{
//				rc = this.manager_staff_id.ToString();
//			}
//            return rc;
//        }

        public override bool Equals(object obj){
			if(obj == null)
				return false;
            if(obj is store){
                store compare=(store)obj;
                return compare.KeyValue().Equals( this.KeyValue() );
                //return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }



        public string DescriptorValue()
        {
            return this.manager_staff_id.ToString();
        }

        public string DescriptorColumn() {
            return "manager_staff_id";
        }

        public static string GetKeyColumn()
        {
            return "store_id";
        }        

        public static string GetDescriptorColumn()
        {
            return "manager_staff_id";
        }
        
        #region ' Foreign Keys '
        public IQueryable<customer> customers
        {
            get
            {
                  var repo=Sakila.Data.customer.GetRepo();
                  return from items in repo.GetAll()
                       where items.store_id == _store_id
                       select items;
            }
        }
        public IQueryable<inventory> inventories
        {
            get
            {
                  var repo=Sakila.Data.inventory.GetRepo();
                  return from items in repo.GetAll()
                       where items.store_id == _store_id
                       select items;
            }
        }
        public IQueryable<staff> staffs
        {
            get
            {
                  var repo=Sakila.Data.staff.GetRepo();
                  return from items in repo.GetAll()
                       where items.store_id == _store_id
                       select items;
            }
        }
        public IQueryable<address> storeaddress
        {
            get
            {
                  var repo=Sakila.Data.address.GetRepo();
                  return from items in repo.GetAll()
                       where items.address_id == _address_id
                       select items;
            }
        }
        public IQueryable<staff> storestaff
        {
            get
            {
                  var repo=Sakila.Data.staff.GetRepo();
                  return from items in repo.GetAll()
                       where items.staff_id == _manager_staff_id
                       select items;
            }
        }
        #endregion

        bool _store_id;
		[LocalData]
        public bool store_id
        {
            get { return _store_id; }
            set
            {
                if(_store_id!=value){
                    _store_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="store_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        bool _manager_staff_id;
		[LocalData]
        public bool manager_staff_id
        {
            get { return _manager_staff_id; }
            set
            {
                if(_manager_staff_id!=value){
                    _manager_staff_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="manager_staff_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        short _address_id;
		[LocalData]
        public short address_id
        {
            get { return _address_id; }
            set
            {
                if(_address_id!=value){
                    _address_id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="address_id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _last_update;
		[LocalData]
        public DateTime last_update
        {
            get { return _last_update; }
            set
            {
                if(_last_update!=value){
                    _last_update=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="last_update");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }


        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }

        public DbCommand GetInsertCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
			OnSaving( );
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        public void Add(IDataProvider provider){
			OnSaving( );

            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
        public void Save() {
            Save(_db.DataProvider);
        }      

        public void Save(IDataProvider provider) {
            if (_isNew) {
                Add(provider);
            } else {
                Update(provider);
            }
        }

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
                    }

        public void Delete() {
            Delete(_db.DataProvider);
        }

        public static void Delete(Expression<Func<store, bool>> expression) {
            var repo = GetRepo();
            
       
            repo.DeleteMany(expression);
        }

        
        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }

        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {
                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
    } 
}
