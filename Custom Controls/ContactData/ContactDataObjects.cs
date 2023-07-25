using System;
using DevExpress.Xpo;
namespace SecureRisk
{

    public class ContactXAddresses : XPLiteObject
    {
        Guid fContactXAddressID;
        [Key( true )]
        public Guid ContactXAddressID
        {
            get { return fContactXAddressID; }
            set { SetPropertyValue( "ContactXAddressID", ref fContactXAddressID, value ); }
        }
        Contacts fContactID;
        public Contacts ContactID
        {
            get { return fContactID; }
            set
            {
                Contacts oldValue = fContactID;
                if( oldValue == value ) return;
                fContactID = value;
                OnChanged( "ContactID", oldValue, value );
            }
        }
        Addresses fAddressID;
        public Addresses AddressID
        {
            get { return fAddressID; }
            set
            {
                Addresses oldValue = fAddressID;
                if( oldValue == value ) return;
                fAddressID = value;
                OnChanged( "AddressID", oldValue, value );
            }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public ContactXAddresses( Session session ) : base( session ) { }
        public ContactXAddresses( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class sysdiagrams : XPLiteObject
    {
        string fname;
        public string name
        {
            get { return fname; }
            set { SetPropertyValue( "name", ref fname, value ); }
        }
        int fprincipal_id;
        public int principal_id
        {
            get { return fprincipal_id; }
            set { SetPropertyValue( "principal_id", ref fprincipal_id, value ); }
        }
        int fdiagram_id;
        [Key( true )]
        public int diagram_id
        {
            get { return fdiagram_id; }
            set { SetPropertyValue( "diagram_id", ref fdiagram_id, value ); }
        }
        int fversion;
        public int version
        {
            get { return fversion; }
            set { SetPropertyValue( "version", ref fversion, value ); }
        }
        byte[] fdefinition;
        public byte[] definition
        {
            get { return fdefinition; }
            set { SetPropertyValue( "definition", ref fdefinition, value ); }
        }
        public sysdiagrams( Session session ) : base( session ) { }
        public sysdiagrams( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class JobAssignments : XPLiteObject
    {
        Guid fJobAssignmentID;
        [Key( true )]
        public Guid JobAssignmentID
        {
            get { return fJobAssignmentID; }
            set { SetPropertyValue( "JobAssignmentID", ref fJobAssignmentID, value ); }
        }
        Jobs fJobID;
        public Jobs JobID
        {
            get { return fJobID; }
            set
            {
                Jobs oldValue = fJobID;
                if( oldValue == value ) return;
                fJobID = value;
                OnChanged( "JobID", oldValue, value );
            }
        }
        DateTime fAssignedDate;
        public DateTime AssignedDate
        {
            get { return fAssignedDate; }
            set { SetPropertyValue( "AssignedDate", ref fAssignedDate, value ); }
        }
        DateTime fDueDate;
        public DateTime DueDate
        {
            get { return fDueDate; }
            set { SetPropertyValue( "DueDate", ref fDueDate, value ); }
        }
        DateTime fCompleteDate;
        public DateTime CompleteDate
        {
            get { return fCompleteDate; }
            set { SetPropertyValue( "CompleteDate", ref fCompleteDate, value ); }
        }
        AssignedTypes fAssignedTypeID;
        public AssignedTypes AssignedTypeID
        {
            get { return fAssignedTypeID; }
            set
            {
                AssignedTypes oldValue = fAssignedTypeID;
                if( oldValue == value ) return;
                fAssignedTypeID = value;
                OnChanged( "AssignedTypeID", oldValue, value );
            }
        }
        Contacts fAssignedID;
        public Contacts AssignedID
        {
            get { return fAssignedID; }
            set
            {
                Contacts oldValue = fAssignedID;
                if( oldValue == value ) return;
                fAssignedID = value;
                OnChanged( "AssignedID", oldValue, value );
            }
        }
        string fRates;
        public string Rates
        {
            get { return fRates; }
            set { SetPropertyValue( "Rates", ref fRates, value ); }
        }
        string fHours;
        public string Hours
        {
            get { return fHours; }
            set { SetPropertyValue( "Hours", ref fHours, value ); }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public JobAssignments( Session session ) : base( session ) { }
        public JobAssignments( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class ContactTypes : XPLiteObject
    {
        Guid fContactTypeID;
        [Key( true )]
        public Guid ContactTypeID
        {
            get { return fContactTypeID; }
            set { SetPropertyValue( "ContactTypeID", ref fContactTypeID, value ); }
        }
        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue( "Name", ref fName, value ); }
        }
        string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue( "Description", ref fDescription, value ); }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public ContactTypes( Session session ) : base( session ) { }
        public ContactTypes( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class Policies : XPLiteObject
    {
        Guid fPolicyID;
        [Key( true )]
        public Guid PolicyID
        {
            get { return fPolicyID; }
            set { SetPropertyValue( "PolicyID", ref fPolicyID, value ); }
        }
        string fPolicyNumber;
        public string PolicyNumber
        {
            get { return fPolicyNumber; }
            set { SetPropertyValue( "PolicyNumber", ref fPolicyNumber, value ); }
        }
        string fPolicyAmount;
        public string PolicyAmount
        {
            get { return fPolicyAmount; }
            set { SetPropertyValue( "PolicyAmount", ref fPolicyAmount, value ); }
        }
        string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue( "Description", ref fDescription, value ); }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public Policies( Session session ) : base( session ) { }
        public Policies( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class AddressTypes : XPLiteObject
    {
        Guid fAddressTypeID;
        [Key( true )]
        public Guid AddressTypeID
        {
            get { return fAddressTypeID; }
            set { SetPropertyValue( "AddressTypeID", ref fAddressTypeID, value ); }
        }
        string fAddressTypeName;
        public string AddressTypeName
        {
            get { return fAddressTypeName; }
            set { SetPropertyValue( "AddressTypeName", ref fAddressTypeName, value ); }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public AddressTypes( Session session ) : base( session ) { }
        public AddressTypes( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class JobLocations : XPLiteObject
    {
        Guid fJobLocationID;
        [Key( true )]
        public Guid JobLocationID
        {
            get { return fJobLocationID; }
            set { SetPropertyValue( "JobLocationID", ref fJobLocationID, value ); }
        }
        Jobs fJobID;
        public Jobs JobID
        {
            get { return fJobID; }
            set
            {
                Jobs oldValue = fJobID;
                if( oldValue == value ) return;
                fJobID = value;
                OnChanged( "JobID", oldValue, value );
            }
        }
        Addresses fAddressID;
        public Addresses AddressID
        {
            get { return fAddressID; }
            set
            {
                Addresses oldValue = fAddressID;
                if( oldValue == value ) return;
                fAddressID = value;
                OnChanged( "AddressID", oldValue, value );
            }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public JobLocations( Session session ) : base( session ) { }
        public JobLocations( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class PhoneNumberTypes : XPLiteObject
    {
        Guid fPhoneNumberTypeID;
        [Key( true )]
        public Guid PhoneNumberTypeID
        {
            get { return fPhoneNumberTypeID; }
            set { SetPropertyValue( "PhoneNumberTypeID", ref fPhoneNumberTypeID, value ); }
        }
        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue( "Name", ref fName, value ); }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public PhoneNumberTypes( Session session ) : base( session ) { }
        public PhoneNumberTypes( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class Jobs : XPLiteObject
    {
        Guid fJobID;
        [Key( true )]
        public Guid JobID
        {
            get { return fJobID; }
            set { SetPropertyValue( "JobID", ref fJobID, value ); }
        }
        DateTime fRequestDate;
        public DateTime RequestDate
        {
            get { return fRequestDate; }
            set { SetPropertyValue( "RequestDate", ref fRequestDate, value ); }
        }
        DateTime fDueDate;
        public DateTime DueDate
        {
            get { return fDueDate; }
            set { SetPropertyValue( "DueDate", ref fDueDate, value ); }
        }
        DateTime fCompletionDate;
        public DateTime CompletionDate
        {
            get { return fCompletionDate; }
            set { SetPropertyValue( "CompletionDate", ref fCompletionDate, value ); }
        }
        string fInsuranceName;
        public string InsuranceName
        {
            get { return fInsuranceName; }
            set { SetPropertyValue( "InsuranceName", ref fInsuranceName, value ); }
        }
        Companies fInsuranceCompanyID;
        public Companies InsuranceCompanyID
        {
            get { return fInsuranceCompanyID; }
            set
            {
                Companies oldValue = fInsuranceCompanyID;
                if( oldValue == value ) return;
                fInsuranceCompanyID = value;
                OnChanged( "InsuranceCompanyID", oldValue, value );
            }
        }
        string fWorkItemNote;
        public string WorkItemNote
        {
            get { return fWorkItemNote; }
            set { SetPropertyValue( "WorkItemNote", ref fWorkItemNote, value ); }
        }
        JobStatuses fJobStatusID;
        public JobStatuses JobStatusID
        {
            get { return fJobStatusID; }
            set
            {
                JobStatuses oldValue = fJobStatusID;
                if( oldValue == value ) return;
                fJobStatusID = value;
                OnChanged( "JobStatusID", oldValue, value );
            }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public Jobs( Session session ) : base( session ) { }
        public Jobs( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class ParentTypes : XPLiteObject
    {
        Guid fParentTypeID;
        [Key( true )]
        public Guid ParentTypeID
        {
            get { return fParentTypeID; }
            set { SetPropertyValue( "ParentTypeID", ref fParentTypeID, value ); }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public ParentTypes( Session session ) : base( session ) { }
        public ParentTypes( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class Notes : XPLiteObject
    {
        Guid fNoteID;
        [Key( true )]
        public Guid NoteID
        {
            get { return fNoteID; }
            set { SetPropertyValue( "NoteID", ref fNoteID, value ); }
        }
        Parents fParentID;
        public Parents ParentID
        {
            get { return fParentID; }
            set
            {
                Parents oldValue = fParentID;
                if( oldValue == value ) return;
                fParentID = value;
                OnChanged( "ParentID", oldValue, value );
            }
        }
        ParentTypes fParentTypeID;
        public ParentTypes ParentTypeID
        {
            get { return fParentTypeID; }
            set
            {
                ParentTypes oldValue = fParentTypeID;
                if( oldValue == value ) return;
                fParentTypeID = value;
                OnChanged( "ParentTypeID", oldValue, value );
            }
        }
        string fNoteTitle;
        public string NoteTitle
        {
            get { return fNoteTitle; }
            set { SetPropertyValue( "NoteTitle", ref fNoteTitle, value ); }
        }
        string fNote;
        public string Note
        {
            get { return fNote; }
            set { SetPropertyValue( "Note", ref fNote, value ); }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public Notes( Session session ) : base( session ) { }
        public Notes( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class Companies : XPLiteObject
    {
        Guid fCompanyID;
        [Key( true )]
        public Guid CompanyID
        {
            get { return fCompanyID; }
            set { SetPropertyValue( "CompanyID", ref fCompanyID, value ); }
        }
        string fCompanyName;
        public string CompanyName
        {
            get { return fCompanyName; }
            set { SetPropertyValue( "CompanyName", ref fCompanyName, value ); }
        }
        Contacts fPrimaryContactID;
        public Contacts PrimaryContactID
        {
            get { return fPrimaryContactID; }
            set
            {
                Contacts oldValue = fPrimaryContactID;
                if( oldValue == value ) return;
                fPrimaryContactID = value;
                OnChanged( "PrimaryContactID", oldValue, value );
            }
        }
        Guid fPrimaryAddressID;
        public Guid PrimaryAddressID
        {
            get { return fPrimaryAddressID; }
            set { SetPropertyValue( "PrimaryAddressID", ref fPrimaryAddressID, value ); }
        }
        PhoneNumbers fPrimaryPhoneNumberID;
        public PhoneNumbers PrimaryPhoneNumberID
        {
            get { return fPrimaryPhoneNumberID; }
            set
            {
                PhoneNumbers oldValue = fPrimaryPhoneNumberID;
                if( oldValue == value ) return;
                fPrimaryPhoneNumberID = value;
                OnChanged( "PrimaryPhoneNumberID", oldValue, value );
            }
        }
        RelationshipTypes fRelationshipTypeID;
        public RelationshipTypes RelationshipTypeID
        {
            get { return fRelationshipTypeID; }
            set
            {
                RelationshipTypes oldValue = fRelationshipTypeID;
                if( oldValue == value ) return;
                fRelationshipTypeID = value;
                OnChanged( "RelationshipTypeID", oldValue, value );
            }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public Companies( Session session ) : base( session ) { }
        public Companies( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class WorkItems : XPLiteObject
    {
        Guid fWorkItemID;
        [Key( true )]
        public Guid WorkItemID
        {
            get { return fWorkItemID; }
            set { SetPropertyValue( "WorkItemID", ref fWorkItemID, value ); }
        }
        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue( "Name", ref fName, value ); }
        }
        string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue( "Description", ref fDescription, value ); }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public WorkItems( Session session ) : base( session ) { }
        public WorkItems( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class CompanyXAddresses : XPLiteObject
    {
        Guid fCompanyXAddressID;
        [Key( true )]
        public Guid CompanyXAddressID
        {
            get { return fCompanyXAddressID; }
            set { SetPropertyValue( "CompanyXAddressID", ref fCompanyXAddressID, value ); }
        }
        Companies fCompanyID;
        public Companies CompanyID
        {
            get { return fCompanyID; }
            set
            {
                Companies oldValue = fCompanyID;
                if( oldValue == value ) return;
                fCompanyID = value;
                OnChanged( "CompanyID", oldValue, value );
            }
        }
        Addresses fAddressID;
        public Addresses AddressID
        {
            get { return fAddressID; }
            set
            {
                Addresses oldValue = fAddressID;
                if( oldValue == value ) return;
                fAddressID = value;
                OnChanged( "AddressID", oldValue, value );
            }
        }
        Relationships fRelationshipID;
        public Relationships RelationshipID
        {
            get { return fRelationshipID; }
            set
            {
                Relationships oldValue = fRelationshipID;
                if( oldValue == value ) return;
                fRelationshipID = value;
                OnChanged( "RelationshipID", oldValue, value );
            }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public CompanyXAddresses( Session session ) : base( session ) { }
        public CompanyXAddresses( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class Groups : XPLiteObject
    {
        Guid fGroupID;
        [Key( true )]
        public Guid GroupID
        {
            get { return fGroupID; }
            set { SetPropertyValue( "GroupID", ref fGroupID, value ); }
        }
        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue( "Name", ref fName, value ); }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public Groups( Session session ) : base( session ) { }
        public Groups( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class PhoneNumbers : XPLiteObject
    {
        Guid fPhoneNumberID;
        [Key( true )]
        public Guid PhoneNumberID
        {
            get { return fPhoneNumberID; }
            set { SetPropertyValue( "PhoneNumberID", ref fPhoneNumberID, value ); }
        }
        PhoneNumberTypes fPhoneNumberTypeID;
        public PhoneNumberTypes PhoneNumberTypeID
        {
            get { return fPhoneNumberTypeID; }
            set
            {
                PhoneNumberTypes oldValue = fPhoneNumberTypeID;
                if( oldValue == value ) return;
                fPhoneNumberTypeID = value;
                OnChanged( "PhoneNumberTypeID", oldValue, value );
            }
        }
        string fAreaCode;
        public string AreaCode
        {
            get { return fAreaCode; }
            set { SetPropertyValue( "AreaCode", ref fAreaCode, value ); }
        }
        string fPrefix;
        public string Prefix
        {
            get { return fPrefix; }
            set { SetPropertyValue( "Prefix", ref fPrefix, value ); }
        }
        string fSuffix;
        public string Suffix
        {
            get { return fSuffix; }
            set { SetPropertyValue( "Suffix", ref fSuffix, value ); }
        }
        string fExtension;
        public string Extension
        {
            get { return fExtension; }
            set { SetPropertyValue( "Extension", ref fExtension, value ); }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public PhoneNumbers( Session session ) : base( session ) { }
        public PhoneNumbers( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class Relationships : XPLiteObject
    {
        Guid fRelationshipID;
        [Key( true )]
        public Guid RelationshipID
        {
            get { return fRelationshipID; }
            set { SetPropertyValue( "RelationshipID", ref fRelationshipID, value ); }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public Relationships( Session session ) : base( session ) { }
        public Relationships( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class Addresses : XPLiteObject
    {
        Guid fAddressID;
        [Key( true )]
        public Guid AddressID
        {
            get { return fAddressID; }
            set { SetPropertyValue( "AddressID", ref fAddressID, value ); }
        }
        AddressTypes fAddressTypeID;
        public AddressTypes AddressTypeID
        {
            get { return fAddressTypeID; }
            set
            {
                AddressTypes oldValue = fAddressTypeID;
                if( oldValue == value ) return;
                fAddressTypeID = value;
                OnChanged( "AddressTypeID", oldValue, value );
            }
        }
        string fStreet;
        public string Street
        {
            get { return fStreet; }
            set { SetPropertyValue( "Street", ref fStreet, value ); }
        }
        string fCity;
        public string City
        {
            get { return fCity; }
            set { SetPropertyValue( "City", ref fCity, value ); }
        }
        string fState;
        public string State
        {
            get { return fState; }
            set { SetPropertyValue( "State", ref fState, value ); }
        }
        string fZip;
        public string Zip
        {
            get { return fZip; }
            set { SetPropertyValue( "Zip", ref fZip, value ); }
        }
        string fCountry;
        public string Country
        {
            get { return fCountry; }
            set { SetPropertyValue( "Country", ref fCountry, value ); }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public Addresses( Session session ) : base( session ) { }
        public Addresses( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class RelationshipTypes : XPLiteObject
    {
        Guid fRelationshipTypeID;
        [Key( true )]
        public Guid RelationshipTypeID
        {
            get { return fRelationshipTypeID; }
            set { SetPropertyValue( "RelationshipTypeID", ref fRelationshipTypeID, value ); }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public RelationshipTypes( Session session ) : base( session ) { }
        public RelationshipTypes( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class Parents : XPLiteObject
    {
        Guid fParentID;
        [Key( true )]
        public Guid ParentID
        {
            get { return fParentID; }
            set { SetPropertyValue( "ParentID", ref fParentID, value ); }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public Parents( Session session ) : base( session ) { }
        public Parents( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class AssignedTypes : XPLiteObject
    {
        Guid fAssignedTypeID;
        [Key( true )]
        public Guid AssignedTypeID
        {
            get { return fAssignedTypeID; }
            set { SetPropertyValue( "AssignedTypeID", ref fAssignedTypeID, value ); }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public AssignedTypes( Session session ) : base( session ) { }
        public AssignedTypes( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class Contacts : XPLiteObject
    {
        Guid fContactID;
        [Key( true )]
        public Guid ContactID
        {
            get { return fContactID; }
            set { SetPropertyValue( "ContactID", ref fContactID, value ); }
        }
        ContactTypes fContactTypeID;
        public ContactTypes ContactTypeID
        {
            get { return fContactTypeID; }
            set
            {
                ContactTypes oldValue = fContactTypeID;
                if( oldValue == value ) return;
                fContactTypeID = value;
                OnChanged( "ContactTypeID", oldValue, value );
            }
        }
        string fFirstName;
        public string FirstName
        {
            get { return fFirstName; }
            set { SetPropertyValue( "FirstName", ref fFirstName, value ); }
        }
        string fMiddleName;
        public string MiddleName
        {
            get { return fMiddleName; }
            set { SetPropertyValue( "MiddleName", ref fMiddleName, value ); }
        }
        string fLastName;
        public string LastName
        {
            get { return fLastName; }
            set { SetPropertyValue( "LastName", ref fLastName, value ); }
        }
        Guid fPrimaryAddress;
        public Guid PrimaryAddress
        {
            get { return fPrimaryAddress; }
            set { SetPropertyValue( "PrimaryAddress", ref fPrimaryAddress, value ); }
        }
        Guid fCompanyID;
        public Guid CompanyID
        {
            get { return fCompanyID; }
            set { SetPropertyValue( "CompanyID", ref fCompanyID, value ); }
        }
        string fCompanyName;
        public string CompanyName
        {
            get { return fCompanyName; }
            set { SetPropertyValue( "CompanyName", ref fCompanyName, value ); }
        }
        PhoneNumbers fPrimaryPhoneNumberID;
        public PhoneNumbers PrimaryPhoneNumberID
        {
            get { return fPrimaryPhoneNumberID; }
            set
            {
                PhoneNumbers oldValue = fPrimaryPhoneNumberID;
                if( oldValue == value ) return;
                fPrimaryPhoneNumberID = value;
                OnChanged( "PrimaryPhoneNumberID", oldValue, value );
            }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public Contacts( Session session ) : base( session ) { }
        public Contacts( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class JobStatuses : XPLiteObject
    {
        Guid fJobStatusID;
        [Key( true )]
        public Guid JobStatusID
        {
            get { return fJobStatusID; }
            set { SetPropertyValue( "JobStatusID", ref fJobStatusID, value ); }
        }
        string fName;
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue( "Name", ref fName, value ); }
        }
        string fDescription;
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue( "Description", ref fDescription, value ); }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public JobStatuses( Session session ) : base( session ) { }
        public JobStatuses( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }

    public class WorkItemsXJobLocations : XPLiteObject
    {
        Guid fWorkItemXJobLocation;
        [Key( true )]
        public Guid WorkItemXJobLocation
        {
            get { return fWorkItemXJobLocation; }
            set { SetPropertyValue( "WorkItemXJobLocation", ref fWorkItemXJobLocation, value ); }
        }
        JobLocations fJobLocationID;
        public JobLocations JobLocationID
        {
            get { return fJobLocationID; }
            set
            {
                JobLocations oldValue = fJobLocationID;
                if( oldValue == value ) return;
                fJobLocationID = value;
                OnChanged( "JobLocationID", oldValue, value );
            }
        }
        WorkItems fWorkItemID;
        public WorkItems WorkItemID
        {
            get { return fWorkItemID; }
            set
            {
                WorkItems oldValue = fWorkItemID;
                if( oldValue == value ) return;
                fWorkItemID = value;
                OnChanged( "WorkItemID", oldValue, value );
            }
        }
        Guid fNoteID;
        public Guid NoteID
        {
            get { return fNoteID; }
            set { SetPropertyValue( "NoteID", ref fNoteID, value ); }
        }
        DateTime fCreateDate;
        public DateTime CreateDate
        {
            get { return fCreateDate; }
            set { SetPropertyValue( "CreateDate", ref fCreateDate, value ); }
        }
        DateTime fModifyDate;
        public DateTime ModifyDate
        {
            get { return fModifyDate; }
            set { SetPropertyValue( "ModifyDate", ref fModifyDate, value ); }
        }
        public WorkItemsXJobLocations( Session session ) : base( session ) { }
        public WorkItemsXJobLocations( ) : base( Session.DefaultSession ) { }
        public override void AfterConstruction( ) { base.AfterConstruction( ); }
    }
}
