


using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

namespace WFT.PSService.ServiceLibrary
{
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the Assembly table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class Assembly : XRData<Assembly>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return aclPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid aclPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string aclPrimaryKey{ get; set; }
		[DataMember]
		public DateTime aclLstChgDT{ get; set; }
		[DataMember]
		public string aclLstChgUser{ get; set; }
		[DataMember]
		public bool aclRefCaseDefined{ get; set; }
		[DataMember]
		public Guid aclFK_r_AssemblyType_GUID{ get; set; }
		[DataMember]
		public string aclFK_r_AssemblyType{ get; set; }
		[DataMember]
		public string aclAssemblyName{ get; set; }
		[DataMember]
		public bool aclTemplate{ get; set; }
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the AssemblyComponent table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class AssemblyComponent : XRData<AssemblyComponent>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return ascPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid ascPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string ascPrimaryKey{ get; set; }
		[DataMember]
		public DateTime ascLstChgDT{ get; set; }
		[DataMember]
		public string ascLstChgUser{ get; set; }
		[DataMember]
		public Guid ascFK_Assembly_GUID{ get; set; }
		[DataMember]
		public string ascFK_Assembly{ get; set; }
		[DataMember]
		public Guid ascFK_Component_GUID{ get; set; }
		[DataMember]
		public string ascFK_Component{ get; set; }
		[DataMember]
		public Guid ascFK_Event_Beginning_GUID{ get; set; }
		[DataMember]
		public string ascFK_Event_Beginning{ get; set; }
		[DataMember]
		public DateTime ascBegEventDT{ get; set; }
		[DataMember]
		public Guid ascFK_Event_Ending_GUID{ get; set; }
		[DataMember]
		public string ascFK_Event_Ending{ get; set; }
		[DataMember]
		public DateTime ascEndEventDT{ get; set; }
		[DataMember]
		public decimal ascAssemblyOrder{ get; set; }
		[DataMember]
		public Guid ascFK_r_ComponentGrouping_GUID{ get; set; }
		[DataMember]
		public string ascFK_r_ComponentGrouping{ get; set; }
		[DataMember]
		public decimal ascQuantity{ get; set; }
		[DataMember]
		public decimal? ascLength{ get; set; }
		[DataMember]
		public decimal? ascTopDepth{ get; set; }
		[DataMember]
		public decimal? ascBottomDepth{ get; set; }
		[DataMember]
		public decimal? ascTrueVerticalDepth{ get; set; }
		[DataMember]
		public decimal? ascTrueVerticalDepthBottom{ get; set; }
		[DataMember]
		public decimal ascPreviousRunDays{ get; set; }
		[DataMember]
		public DateTime? ascPreviousRunDaysDT{ get; set; }
		[DataMember]
		public string ascRemark{ get; set; }
		[DataMember]
		public Guid? ascFK_r_ComponentCategory_GUID{ get; set; }
		[DataMember]
		public string ascFK_r_ComponentCategory{ get; set; }
		override public void AdjustDates( TimeSpan _offset )
		{
			base.AdjustDates( _offset );
			ascBegEventDT += _offset;			
			ascEndEventDT += _offset;			
			ascPreviousRunDaysDT += _offset;			
		}
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the AssemblyComponentBblPlgMeasure table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class AssemblyComponentBblPlgMeasure : XRData<AssemblyComponentBblPlgMeasure>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return acmPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid acmPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string acmPrimaryKey{ get; set; }
		[DataMember]
		public DateTime acmLstChgDT{ get; set; }
		[DataMember]
		public string acmLstChgUser{ get; set; }
		[DataMember]
		public Guid acmFK_AssemblyComponent_GUID{ get; set; }
		[DataMember]
		public string acmFK_AssemblyComponent{ get; set; }
		[DataMember]
		public Guid acmFK_Event_GUID{ get; set; }
		[DataMember]
		public string acmFK_Event{ get; set; }
		[DataMember]
		public decimal acmPosition{ get; set; }
		[DataMember]
		public decimal acmMeasurement{ get; set; }
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the AssemblyComponentSRPump table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class AssemblyComponentSRPump : XRData<AssemblyComponentSRPump>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return arpPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid arpPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string arpPrimaryKey{ get; set; }
		[DataMember]
		public DateTime arpLstChgDT{ get; set; }
		[DataMember]
		public string arpLstChgUser{ get; set; }
		[DataMember]
		public Guid arpFK_AssemblyComponent_GUID{ get; set; }
		[DataMember]
		public string arpFK_AssemblyComponent{ get; set; }
		[DataMember]
		public string arpAPIDescription{ get; set; }
		[DataMember]
		public string arpAPIExtraDescriptionText{ get; set; }
		[DataMember]
		public Guid arpFK_r_APIPumpGraphics_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APIPumpGraphics{ get; set; }
		[DataMember]
		public Guid arpFK_r_APISRPTubingSize_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APISRPTubingSize{ get; set; }
		[DataMember]
		public Guid arpFK_r_APISRPPumpBore_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APISRPPumpBore{ get; set; }
		[DataMember]
		public Guid arpFK_r_APISRPPumpType_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APISRPPumpType{ get; set; }
		[DataMember]
		public Guid arpFK_r_APISRPBarrelType_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APISRPBarrelType{ get; set; }
		[DataMember]
		public Guid arpFK_r_APISRPSeatAssyLocation_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APISRPSeatAssyLocation{ get; set; }
		[DataMember]
		public Guid arpFK_r_APISRPSeatAssyType_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APISRPSeatAssyType{ get; set; }
		[DataMember]
		public decimal arpAPIBarrelLength{ get; set; }
		[DataMember]
		public decimal arpAPIPlungerLength{ get; set; }
		[DataMember]
		public decimal arpAPIExtensionCouplingUpperLength{ get; set; }
		[DataMember]
		public decimal arpAPIExtensionCouplingLowerLength{ get; set; }
		[DataMember]
		public Guid arpFK_r_APISRPExtPumpType_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APISRPExtPumpType{ get; set; }
		[DataMember]
		public Guid arpFK_r_APISRPExtBarrelType_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APISRPExtBarrelType{ get; set; }
		[DataMember]
		public Guid arpFK_r_APISRPExtSeatAssyLocation_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APISRPExtSeatAssyLocation{ get; set; }
		[DataMember]
		public Guid arpFK_r_APISRPExtSeatAssyType_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APISRPExtSeatAssyType{ get; set; }
		[DataMember]
		public Guid arpFK_r_APISRPExtSand_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APISRPExtSand{ get; set; }
		[DataMember]
		public Guid arpFK_r_APISRPExtBblAcc_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APISRPExtBblAcc{ get; set; }
		[DataMember]
		public Guid arpFK_r_APISRPExtPlgAcc_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APISRPExtPlgAcc{ get; set; }
		[DataMember]
		public Guid arpFK_r_APISRPExtPlgType_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APISRPExtPlgType{ get; set; }
		[DataMember]
		public Guid arpFK_r_APISRPExtPlgPin_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APISRPExtPlgPin{ get; set; }
		[DataMember]
		public Guid arpFK_r_APISRPExtSV_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APISRPExtSV{ get; set; }
		[DataMember]
		public Guid arpFK_r_APISRPExtSVCage_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APISRPExtSVCage{ get; set; }
		[DataMember]
		public Guid arpFK_r_APISRPExtTV_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APISRPExtTV{ get; set; }
		[DataMember]
		public Guid arpFK_r_APISRPExtTVCage_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APISRPExtTVCage{ get; set; }
		[DataMember]
		public Guid arpFK_r_APISRPExtTVStPlg_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APISRPExtTVStPlg{ get; set; }
		[DataMember]
		public Guid arpFK_r_APISRPExtVRod_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APISRPExtVRod{ get; set; }
		[DataMember]
		public Guid arpFK_r_APISRPExtWiper_GUID{ get; set; }
		[DataMember]
		public string arpFK_r_APISRPExtWiper{ get; set; }
		[DataMember]
		public decimal arpBblPlgAvgClearance{ get; set; }
		[DataMember]
		public decimal arpMaxSL{ get; set; }
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the BusinessOrganization table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class BusinessOrganization : XRData<BusinessOrganization>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return venPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid venPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string venPrimaryKey{ get; set; }
		[DataMember]
		public string venLanguageCd{ get; set; }
		[DataMember]
		public DateTime venLstChgDT{ get; set; }
		[DataMember]
		public string venLstChgUser{ get; set; }
		[DataMember]
		public bool? venRefUserDeleted{ get; set; }
		[DataMember]
		public bool? venRefCaseDefined{ get; set; }
		[DataMember]
		public Guid venFK_r_BusinessOrganizationType_GUID{ get; set; }
		[DataMember]
		public string venFK_r_BusinessOrganizationType{ get; set; }
		[DataMember]
		public string venBusinessOrganizationAbrev{ get; set; }
		[DataMember]
		public string venBusinessOrganizationName{ get; set; }
		[DataMember]
		public bool? venPT_TaxableStatus{ get; set; }
		[DataMember]
		public decimal? venPT_TaxRate{ get; set; }
		[DataMember]
		public string venPT_InvoiceComments{ get; set; }
		[DataMember]
		public decimal? venPT_DiscountRateRepair{ get; set; }
		[DataMember]
		public decimal? venPT_DiscountRateNew{ get; set; }
		[DataMember]
		public string venPT_SalesRep{ get; set; }
		[DataMember]
		public string venAccountingID{ get; set; }
		[DataMember]
		public bool? venInactive{ get; set; }
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the Component table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class Component : XRData<Component>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return cmcPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid cmcPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string cmcPrimaryKey{ get; set; }
		[DataMember]
		public DateTime cmcLstChgDT{ get; set; }
		[DataMember]
		public string cmcLstChgUser{ get; set; }
		[DataMember]
		public bool cmcRefCaseDefined{ get; set; }
		[DataMember]
		public Guid cmcFK_Assembly_GUID{ get; set; }
		[DataMember]
		public string cmcFK_Assembly{ get; set; }
		[DataMember]
		public Guid cmcFK_r_CatalogItem_GUID{ get; set; }
		[DataMember]
		public string cmcFK_r_CatalogItem{ get; set; }
		[DataMember]
		public Guid cmcFK_r_MfgCatalogItem_GUID{ get; set; }
		[DataMember]
		public string cmcFK_r_MfgCatalogItem{ get; set; }
		[DataMember]
		public string cmcSerialNo{ get; set; }
		[DataMember]
		public Guid cmcFK_r_ComponentOrigin_GUID{ get; set; }
		[DataMember]
		public string cmcFK_r_ComponentOrigin{ get; set; }
		[DataMember]
		public decimal cmcPreviousRunDays{ get; set; }
		[DataMember]
		public Guid cmcFK_BusinessOrganization_GUID{ get; set; }
		[DataMember]
		public string cmcFK_BusinessOrganization{ get; set; }
		[DataMember]
		public string cmcOriginKey{ get; set; }
		[DataMember]
		public bool cmcDiscontinueUse{ get; set; }
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the ComponentSRPump table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class ComponentSRPump : XRData<ComponentSRPump>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return cspPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid cspPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string cspPrimaryKey{ get; set; }
		[DataMember]
		public DateTime cspLstChgDT{ get; set; }
		[DataMember]
		public string cspLstChgUser{ get; set; }
		[DataMember]
		public Guid cspFK_Component_GUID{ get; set; }
		[DataMember]
		public string cspFK_Component{ get; set; }
		[DataMember]
		public Guid cspFK_BusinessOrganization_GUID{ get; set; }
		[DataMember]
		public string cspFK_BusinessOrganization{ get; set; }
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the DatabaseConfiguration table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class DatabaseConfiguration : XRData<DatabaseConfiguration>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return dbcPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid dbcPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string dbcPrimaryKey{ get; set; }
		[DataMember]
		public DateTime dbcLstChgDT{ get; set; }
		[DataMember]
		public string dbcLstChgUser{ get; set; }
		[DataMember]
		public string dbcDescription{ get; set; }
		[DataMember]
		public string dbcValue{ get; set; }
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the DeletedLog table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class DeletedLog : XRData<DeletedLog>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return delPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid delPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string delPrimaryKey{ get; set; }
		[DataMember]
		public DateTime delLstChgDT{ get; set; }
		[DataMember]
		public string delLstChgUser{ get; set; }
		[DataMember]
		public string delFK_csDBEntity{ get; set; }
		[DataMember]
		public string delDeletedKey{ get; set; }
		[DataMember]
		public byte[] delDataXML{ get; set; }
		[DataMember]
		public string delRemark{ get; set; }
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the Document table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class Document : XRData<Document>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return docPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid docPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string docPrimaryKey{ get; set; }
		[DataMember]
		public DateTime docLstChgDT{ get; set; }
		[DataMember]
		public string docLstChgUser{ get; set; }
		[DataMember]
		public bool docRefCaseDefined{ get; set; }
		[DataMember]
		public string docFK_r_DocumentType{ get; set; }
		[DataMember]
		public int docDocID{ get; set; }
		[DataMember]
		public string docBriefDescription{ get; set; }
		[DataMember]
		public string docDocServerPath{ get; set; }
		[DataMember]
		public string docDocServerFileName{ get; set; }
		[DataMember]
		public string docOriginalFileName{ get; set; }
		[DataMember]
		public string docFolderName{ get; set; }
		[DataMember]
		public int? docCurrentVersion{ get; set; }
		[DataMember]
		public bool? docInUse{ get; set; }
		[DataMember]
		public bool? docCheckedOut{ get; set; }
		[DataMember]
		public string docCheckoutUser{ get; set; }
		[DataMember]
		public DateTime? docCheckoutDT{ get; set; }
		[DataMember]
		public string docAPI10{ get; set; }
		[DataMember]
		public string docAPI12{ get; set; }
		[DataMember]
		public string docAPI14{ get; set; }
		[DataMember]
		public string docProducer{ get; set; }
		[DataMember]
		public string docField{ get; set; }
		[DataMember]
		public string docFacility{ get; set; }
		[DataMember]
		public string docFK_EventCategory{ get; set; }
		[DataMember]
		public string docFK_Event{ get; set; }
		[DataMember]
		public string docFK_Component{ get; set; }
		[DataMember]
		public string docUserDef01{ get; set; }
		[DataMember]
		public string docUserDef02{ get; set; }
		[DataMember]
		public string docUserDef03{ get; set; }
		override public void AdjustDates( TimeSpan _offset )
		{
			base.AdjustDates( _offset );
			docCheckoutDT += _offset;			
		}
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the Event table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class Event : XRData<Event>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return evcPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid evcPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string evcPrimaryKey{ get; set; }
		[DataMember]
		public DateTime evcLstChgDT{ get; set; }
		[DataMember]
		public string evcLstChgUser{ get; set; }
		[DataMember]
		public bool evcRefCaseDefined{ get; set; }
		[DataMember]
		public Guid evcFK_Job_GUID{ get; set; }
		[DataMember]
		public string evcFK_EventCategory{ get; set; }
		[DataMember]
		public Guid evcFK_Workorder_GUID{ get; set; }
		[DataMember]
		public string evcFK_Workorder{ get; set; }
		[DataMember]
		public Guid evcFK_r_EventType_GUID{ get; set; }
		[DataMember]
		public string evcFK_r_EventType{ get; set; }
		[DataMember]
		public DateTime evcEventBegDtTm{ get; set; }
		[DataMember]
		public DateTime evcEventEndDtTm{ get; set; }
		[DataMember]
		public decimal evcDurationHours{ get; set; }
		[DataMember]
		public decimal evcEventOrder{ get; set; }
		[DataMember]
		public Guid evcFK_Assembly_GUID{ get; set; }
		[DataMember]
		public string evcFK_Assembly{ get; set; }
		[DataMember]
		public string evcResponsiblePerson{ get; set; }
		[DataMember]
		public Guid evcFK_BusinessOrganization_GUID{ get; set; }
		[DataMember]
		public string evcFK_BusinessOrganization{ get; set; }
		[DataMember]
		public string evcPersonPerformingTask{ get; set; }
		[DataMember]
		public string evcOriginKey{ get; set; }
		[DataMember]
		public string evcRemarks{ get; set; }
		override public void AdjustDates( TimeSpan _offset )
		{
			base.AdjustDates( _offset );
			evcEventBegDtTm += _offset;			
			evcEventEndDtTm += _offset;			
		}
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the EventAssembleSRPump table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class EventAssembleSRPump : XRData<EventAssembleSRPump>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return psrPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid psrPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string psrPrimaryKey{ get; set; }
		[DataMember]
		public DateTime psrLstChgDT{ get; set; }
		[DataMember]
		public string psrLstChgUser{ get; set; }
		[DataMember]
		public Guid psrFK_Event_GUID{ get; set; }
		[DataMember]
		public string psrFK_Event{ get; set; }
		[DataMember]
		public string psrDeliveryTicketNo{ get; set; }
		[DataMember]
		public bool? psrVacuumPressureTest{ get; set; }
		[DataMember]
		public decimal? psrTotalPumpCharges{ get; set; }
		[DataMember]
		public Guid psrFK_Assembly_WellChargeTo_GUID{ get; set; }
		[DataMember]
		public string psrFK_Assembly_WellChargeTo{ get; set; }
		[DataMember]
		public Guid psrFK_WellCompletionXRef_WellChargeTo_GUID{ get; set; }
		[DataMember]
		public string psrFK_WellCompletionXRef_WellChargeTo{ get; set; }
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the EventComponentFailure table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class EventComponentFailure : XRData<EventComponentFailure>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return acfPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid acfPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string acfPrimaryKey{ get; set; }
		[DataMember]
		public DateTime acfLstChgDT{ get; set; }
		[DataMember]
		public string acfLstChgUser{ get; set; }
		[DataMember]
		public Guid acfFK_Event_GUID{ get; set; }
		[DataMember]
		public string acfFK_Event{ get; set; }
		[DataMember]
		public Guid acfFK_Component_GUID{ get; set; }
		[DataMember]
		public string acfFK_Component{ get; set; }
		[DataMember]
		public bool acfPrimaryFailureObservation{ get; set; }
		[DataMember]
		public int acfPrimaryCauseOfFailure{ get; set; }
		[DataMember]
		public decimal acfPreviousRunDays{ get; set; }
		[DataMember]
		public Guid acfFK_r_ComponentCondition_Current_GUID{ get; set; }
		[DataMember]
		public string acfFK_r_ComponentCondition_Current{ get; set; }
		[DataMember]
		public Guid acfFK_r_FailureObservation_GUID{ get; set; }
		[DataMember]
		public string acfFK_r_FailureObservation{ get; set; }
		[DataMember]
		public Guid acfFK_r_FailureLocation_GUID{ get; set; }
		[DataMember]
		public string acfFK_r_FailureLocation{ get; set; }
		[DataMember]
		public Guid acfFK_r_FailureInternalExternal_GUID{ get; set; }
		[DataMember]
		public string acfFK_r_FailureInternalExternal{ get; set; }
		[DataMember]
		public decimal? acfFailedDepth{ get; set; }
		[DataMember]
		public Guid acfFK_r_CorrosionLocation_GUID{ get; set; }
		[DataMember]
		public string acfFK_r_CorrosionLocation{ get; set; }
		[DataMember]
		public Guid acfFK_r_CorrosionAmount_GUID{ get; set; }
		[DataMember]
		public string acfFK_r_CorrosionAmount{ get; set; }
		[DataMember]
		public Guid acfFK_r_CorrosionType_GUID{ get; set; }
		[DataMember]
		public string acfFK_r_CorrosionType{ get; set; }
		[DataMember]
		public Guid acfFK_r_ComponentDisposition_GUID{ get; set; }
		[DataMember]
		public string acfFK_r_ComponentDisposition{ get; set; }
		[DataMember]
		public string acfRemarks{ get; set; }
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the EventDetailCosts table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class EventDetailCosts : XRData<EventDetailCosts>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return ecsPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid ecsPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string ecsPrimaryKey{ get; set; }
		[DataMember]
		public DateTime ecsLstChgDT{ get; set; }
		[DataMember]
		public string ecsLstChgUser{ get; set; }
		[DataMember]
		public Guid ecsFK_Event_GUID{ get; set; }
		[DataMember]
		public string ecsFK_Event{ get; set; }
		[DataMember]
		public Guid ecsFK_BusinessOrganization_GUID{ get; set; }
		[DataMember]
		public string ecsFK_BusinessOrganization{ get; set; }
		[DataMember]
		public Guid ecsFK_Component_GUID{ get; set; }
		[DataMember]
		public string ecsFK_Component{ get; set; }
		[DataMember]
		public Guid ecsFK_r_CatalogItem_GUID{ get; set; }
		[DataMember]
		public string ecsFK_r_CatalogItem{ get; set; }
		[DataMember]
		public decimal ecsQuantity{ get; set; }
		[DataMember]
		public decimal ecsUnitPrice{ get; set; }
		[DataMember]
		public decimal ecsDiscountAmount{ get; set; }
		[DataMember]
		public decimal ecsExtendedPrice{ get; set; }
		[DataMember]
		public Guid ecsFK_r_UOMUnit_GUID{ get; set; }
		[DataMember]
		public string ecsFK_r_UOMUnit{ get; set; }
		[DataMember]
		public bool ecsTaxableItem{ get; set; }
		[DataMember]
		public string ecsFK_Invoice{ get; set; }
		[DataMember]
		public Guid ecsFK_Invoice_GUID{ get; set; }
		[DataMember]
		public string ecsRemarks{ get; set; }
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the EventDisassembleSRPump table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class EventDisassembleSRPump : XRData<EventDisassembleSRPump>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return ptdPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid ptdPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string ptdPrimaryKey{ get; set; }
		[DataMember]
		public DateTime ptdLstChgDT{ get; set; }
		[DataMember]
		public string ptdLstChgUser{ get; set; }
		[DataMember]
		public Guid ptdFK_Event_GUID{ get; set; }
		[DataMember]
		public string ptdFK_Event{ get; set; }
		[DataMember]
		public bool? ptdVacuumPressureTest{ get; set; }
		[DataMember]
		public bool? ptdJunkPump{ get; set; }
		[DataMember]
		public bool? ptdRecommendation{ get; set; }
		[DataMember]
		public bool? ptdForMatlAsphaltene{ get; set; }
		[DataMember]
		public bool? ptdForMatlSand{ get; set; }
		[DataMember]
		public bool? ptdForMatlRubber{ get; set; }
		[DataMember]
		public bool? ptdForMatlSteelFilings{ get; set; }
		[DataMember]
		public bool? ptdForMatlShale{ get; set; }
		[DataMember]
		public bool? ptdForMatlParaffin{ get; set; }
		[DataMember]
		public bool? ptdForMatlIronSulfide{ get; set; }
		[DataMember]
		public bool? ptdForMatlMud{ get; set; }
		[DataMember]
		public bool? ptdForMatlScale{ get; set; }
		[DataMember]
		public bool? ptdForMatlChalk{ get; set; }
		[DataMember]
		public bool? ptdForMatlCoal{ get; set; }
		[DataMember]
		public bool? ptdForMatlOther{ get; set; }
		[DataMember]
		public Guid ptdFK_r_CorrosionAmount_GUID{ get; set; }
		[DataMember]
		public string ptdFK_r_CorrosionAmount{ get; set; }
		[DataMember]
		public Guid ptdFK_r_CorrosionLocation_GUID{ get; set; }
		[DataMember]
		public string ptdFK_r_CorrosionLocation{ get; set; }
		[DataMember]
		public Guid ptdFK_r_SRPumpFailureReason_GUID{ get; set; }
		[DataMember]
		public string ptdFK_r_SRPumpFailureReason{ get; set; }
		[DataMember]
		public decimal? ptdApparentDownholeStroke{ get; set; }
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the EventInstallPump table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class EventInstallPump : XRData<EventInstallPump>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return eipPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid eipPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string eipPrimaryKey{ get; set; }
		[DataMember]
		public DateTime eipLstChgDT{ get; set; }
		[DataMember]
		public string eipLstChgUser{ get; set; }
		[DataMember]
		public Guid eipFK_Event_GUID{ get; set; }
		[DataMember]
		public string eipFK_Event{ get; set; }
		[DataMember]
		public Guid eipFK_Assembly_WellSurfaceLocation_GUID{ get; set; }
		[DataMember]
		public string eipFK_Assembly_WellSurfaceLocation{ get; set; }
		[DataMember]
		public Guid eipFK_WellCompletionXRef_GUID{ get; set; }
		[DataMember]
		public string eipFK_WellCompletionXRef{ get; set; }
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the EventPullPump table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class EventPullPump : XRData<EventPullPump>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return eppPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid eppPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string eppPrimaryKey{ get; set; }
		[DataMember]
		public DateTime eppLstChgDT{ get; set; }
		[DataMember]
		public string eppLstChgUser{ get; set; }
		[DataMember]
		public Guid eppFK_Event_GUID{ get; set; }
		[DataMember]
		public string eppFK_Event{ get; set; }
		[DataMember]
		public Guid eppFK_Assembly_WellSurfaceLocation_GUID{ get; set; }
		[DataMember]
		public string eppFK_Assembly_WellSurfaceLocation{ get; set; }
		[DataMember]
		public Guid eppFK_WellCompletionXRef_GUID{ get; set; }
		[DataMember]
		public string eppFK_WellCompletionXRef{ get; set; }
		[DataMember]
		public DateTime? eppFailedDate{ get; set; }
		[DataMember]
		public decimal? eppRunDays{ get; set; }
		override public void AdjustDates( TimeSpan _offset )
		{
			base.AdjustDates( _offset );
			eppFailedDate += _offset;			
		}
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the Facility table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class Facility : XRData<Facility>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return facPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid facPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string facPrimaryKey{ get; set; }
		[DataMember]
		public DateTime facLstChgDT{ get; set; }
		[DataMember]
		public string facLstChgUser{ get; set; }
		[DataMember]
		public bool facRefCaseDefined{ get; set; }
		[DataMember]
		public bool facRefUserDeleted{ get; set; }
		[DataMember]
		public Guid facFK_Assembly_GUID{ get; set; }
		[DataMember]
		public string facFK_Assembly{ get; set; }
		[DataMember]
		public Guid facFK_r_FacilityType_GUID{ get; set; }
		[DataMember]
		public string facFK_r_FacilityType{ get; set; }
		[DataMember]
		public string facFacilityID{ get; set; }
		[DataMember]
		public string facFacilityDescription{ get; set; }
		[DataMember]
		public string facFK_Owner{ get; set; }
		[DataMember]
		public Guid facFK_BusinessOrganization_GUID{ get; set; }
		[DataMember]
		public string facFK_BusinessOrganization{ get; set; }
		[DataMember]
		public string facStorageID{ get; set; }
		[DataMember]
		public bool? facInactive{ get; set; }
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the Invoice table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class Invoice : XRData<Invoice>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return xh5PrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid xh5PrimaryKey_GUID{ get; set; }
		[DataMember]
		public string xh5PrimaryKey{ get; set; }
		[DataMember]
		public bool xh5RefCaseDefined{ get; set; }
		[DataMember]
		public DateTime xh5LstChgDT{ get; set; }
		[DataMember]
		public string xh5LstChgUser{ get; set; }
		[DataMember]
		public Guid xh5FK_Assembly_PumpShop_GUID{ get; set; }
		[DataMember]
		public string xh5FK_Assembly_PumpShop{ get; set; }
		[DataMember]
		public string xh5InvoiceID{ get; set; }
		[DataMember]
		public string xh5AccountingReferenceID{ get; set; }
		[DataMember]
		public bool xh5Invoiced{ get; set; }
		[DataMember]
		public DateTime xh5InvoiceDate{ get; set; }
		[DataMember]
		public Guid xh5FK_BusinessOrganization_Producer_GUID{ get; set; }
		[DataMember]
		public string xh5FK_BusinessOrganization_Producer{ get; set; }
		[DataMember]
		public Guid xh5FK_Assembly_Well_GUID{ get; set; }
		[DataMember]
		public string xh5FK_Assembly_Well{ get; set; }
		[DataMember]
		public Guid xh5FK_WellCompletionXRef_GUID{ get; set; }
		[DataMember]
		public string xh5FK_WellCompletionXRef{ get; set; }
		[DataMember]
		public string xh5ISO4217CurrencyCode{ get; set; }
		[DataMember]
		public string xh5ProductLineID{ get; set; }
		[DataMember]
		public bool? xh5ExternalTransactionComplete{ get; set; }
		[DataMember]
		public string xh5ReturnTransactionMessages{ get; set; }
		[DataMember]
		public string xh5Remarks{ get; set; }
		[DataMember]
		public string xh5FK_Job{ get; set; }
		[DataMember]
		public Guid? xh5FK_Job_GUID{ get; set; }
		override public void AdjustDates( TimeSpan _offset )
		{
			base.AdjustDates( _offset );
			xh5InvoiceDate += _offset;			
		}
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the Job table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class Job : XRData<Job>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return ecgPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid ecgPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string ecgPrimaryKey{ get; set; }
		[DataMember]
		public DateTime ecgLstChgDT{ get; set; }
		[DataMember]
		public string ecgLstChgUser{ get; set; }
		[DataMember]
		public bool ecgRefCaseDefined{ get; set; }
		[DataMember]
		public Guid ecgFK_Assembly_GUID{ get; set; }
		[DataMember]
		public string ecgFK_Assembly{ get; set; }
		[DataMember]
		public Guid ecgFK_BusinessOrganization_GUID{ get; set; }
		[DataMember]
		public string ecgFK_BusinessOrganization{ get; set; }
		[DataMember]
		public Guid ecgFK_r_EventCategoryType_GUID{ get; set; }
		[DataMember]
		public string ecgFK_r_EventCategoryType{ get; set; }
		[DataMember]
		public Guid ecgFK_r_EventCategoryReason_GUID{ get; set; }
		[DataMember]
		public string ecgFK_r_EventCategoryReason{ get; set; }
		[DataMember]
		public DateTime ecgEventBegDtTm{ get; set; }
		[DataMember]
		public DateTime ecgEventEndDtTm{ get; set; }
		[DataMember]
		public Guid ecgFK_r_JobStatus_GUID{ get; set; }
		[DataMember]
		public string ecgFK_r_JobStatus{ get; set; }
		[DataMember]
		public DateTime ecgStatusDt{ get; set; }
		[DataMember]
		public string ecgStChgUser{ get; set; }
		[DataMember]
		public string ecgJobID{ get; set; }
		[DataMember]
		public string ecgOriginKey{ get; set; }
		[DataMember]
		public string ecgAcctRef{ get; set; }
		[DataMember]
		public Guid ecgFK_WellCompletionXRef_GUID{ get; set; }
		[DataMember]
		public string ecgFK_WellCompletionXRef{ get; set; }
		[DataMember]
		public Guid ecgFK_r_PumpJobType_GUID{ get; set; }
		[DataMember]
		public string ecgFK_r_PumpJobType{ get; set; }
		[DataMember]
		public string ecgRemarks{ get; set; }
		[DataMember]
		public DateTime? ecgPrevRunDT{ get; set; }
		[DataMember]
		public DateTime? ecgWellFailedDate{ get; set; }
		[DataMember]
		public DateTime? ecgWellPullDT{ get; set; }
		override public void AdjustDates( TimeSpan _offset )
		{
			base.AdjustDates( _offset );
			ecgEventBegDtTm += _offset;			
			ecgEventEndDtTm += _offset;			
			ecgStatusDt += _offset;			
			ecgPrevRunDT += _offset;			
			ecgWellFailedDate += _offset;			
			ecgWellPullDT += _offset;			
		}
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the JobStatusChangeLog table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class JobStatusChangeLog : XRData<JobStatusChangeLog>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return jscPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid jscPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string jscPrimaryKey{ get; set; }
		[DataMember]
		public DateTime jscLstChgDT{ get; set; }
		[DataMember]
		public string jscLstChgUser{ get; set; }
		[DataMember]
		public Guid jscFK_Job_GUID{ get; set; }
		[DataMember]
		public string jscFK_Job{ get; set; }
		[DataMember]
		public Guid jscFK_r_JobStatus_GUID{ get; set; }
		[DataMember]
		public string jscFK_r_JobStatus{ get; set; }
		[DataMember]
		public DateTime jscStatusDt{ get; set; }
		[DataMember]
		public string jscStChgUser{ get; set; }
		override public void AdjustDates( TimeSpan _offset )
		{
			base.AdjustDates( _offset );
			jscStatusDt += _offset;			
		}
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the Lease table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class Lease : XRData<Lease>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return lsePrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid lsePrimaryKey_GUID{ get; set; }
		[DataMember]
		public string lsePrimaryKey{ get; set; }
		[DataMember]
		public DateTime lseLstChgDT{ get; set; }
		[DataMember]
		public string lseLstChgUser{ get; set; }
		[DataMember]
		public bool lseRefCaseDefined{ get; set; }
		[DataMember]
		public bool lseRefUserDeleted{ get; set; }
		[DataMember]
		public Guid lseFK_BusinessOrganization_GUID{ get; set; }
		[DataMember]
		public string lseFK_BusinessOrganization{ get; set; }
		[DataMember]
		public string lseLeaseID{ get; set; }
		[DataMember]
		public string lseLeaseName{ get; set; }
		[DataMember]
		public bool? lsePMTaxableStatus{ get; set; }
		[DataMember]
		public decimal? lsePMTaxRate{ get; set; }
		[DataMember]
		public bool? lseCHTaxableStatus{ get; set; }
		[DataMember]
		public decimal? lseCHTaxRate{ get; set; }
		[DataMember]
		public bool? lseWSTaxableStatus{ get; set; }
		[DataMember]
		public decimal? lseWSTaxRate{ get; set; }
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the Owner table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class Owner : XRData<Owner>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return ownPrimaryKey.ToString( );
			}
		}
		[DataMember]
		public string ownPrimaryKey{ get; set; }
		[DataMember]
		public DateTime ownLstChgDT{ get; set; }
		[DataMember]
		public string ownLstChgUser{ get; set; }
		[DataMember]
		public bool ownRefCaseDefined{ get; set; }
		[DataMember]
		public bool ownRefUserDeleted{ get; set; }
		[DataMember]
		public string ownOwnerName{ get; set; }
		[DataMember]
		public string ownFK_BusinessOrganization{ get; set; }
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the StickyNotes table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class StickyNotes : XRData<StickyNotes>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return styPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid styPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string styPrimaryKey{ get; set; }
		[DataMember]
		public DateTime styLstChgDT{ get; set; }
		[DataMember]
		public string styLstChgUser{ get; set; }
		[DataMember]
		public Guid styFK_r_MessageActivityType_GUID{ get; set; }
		[DataMember]
		public string styFK_r_MessageActivityType{ get; set; }
		[DataMember]
		public DateTime styMsgOriginDT{ get; set; }
		[DataMember]
		public Guid styFK_Assembly_GUID{ get; set; }
		[DataMember]
		public string styFK_Assembly{ get; set; }
		[DataMember]
		public Guid styFK_Event_GUID{ get; set; }
		[DataMember]
		public string styFK_Event{ get; set; }
		[DataMember]
		public Guid styFK_r_StickyNoteStatus_GUID{ get; set; }
		[DataMember]
		public string styFK_r_StickyNoteStatus{ get; set; }
		[DataMember]
		public Guid styFK_r_MessageActivityPriorityCd_GUID{ get; set; }
		[DataMember]
		public string styFK_r_MessageActivityPriorityCd{ get; set; }
		[DataMember]
		public DateTime styMsgCompletionDT{ get; set; }
		[DataMember]
		public string stySender{ get; set; }
		[DataMember]
		public string styRecipient{ get; set; }
		[DataMember]
		public string styBriefDescription{ get; set; }
		[DataMember]
		public string styCompletionComments{ get; set; }
		[DataMember]
		public string styDocumentFileName{ get; set; }
		[DataMember]
		public string styMessage{ get; set; }
		override public void AdjustDates( TimeSpan _offset )
		{
			base.AdjustDates( _offset );
			styMsgOriginDT += _offset;			
			styMsgCompletionDT += _offset;			
		}
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the TemplatePump table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class TemplatePump : XRData<TemplatePump>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return tphPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid tphPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string tphPrimaryKey{ get; set; }
		[DataMember]
		public string tphLanguageCd{ get; set; }
		[DataMember]
		public DateTime tphLstChgDT{ get; set; }
		[DataMember]
		public string tphLstChgUser{ get; set; }
		[DataMember]
		public bool tphRefCaseDefined{ get; set; }
		[DataMember]
		public bool tphRefUserDeleted{ get; set; }
		[DataMember]
		public string tphTemplateDescription{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APIPumpGraphics_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APIPumpGraphics{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APISRPTubingSize_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APISRPTubingSize{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APISRPPumpBore_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APISRPPumpBore{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APISRPPumpType_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APISRPPumpType{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APISRPBarrelType_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APISRPBarrelType{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APISRPSeatAssyLocation_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APISRPSeatAssyLocation{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APISRPSeatAssyType_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APISRPSeatAssyType{ get; set; }
		[DataMember]
		public decimal? tphAPIBarrelLength{ get; set; }
		[DataMember]
		public decimal? tphAPIPlungerLength{ get; set; }
		[DataMember]
		public decimal? tphAPIExtensionCouplingUpperLength{ get; set; }
		[DataMember]
		public decimal? tphAPIExtensionCouplingLowerLength{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APISRPExtPumpType_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APISRPExtPumpType{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APISRPExtBarrelType_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APISRPExtBarrelType{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APISRPExtSeatAssyLocation_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APISRPExtSeatAssyLocation{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APISRPExtSeatAssyType_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APISRPExtSeatAssyType{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APISRPExtSand_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APISRPExtSand{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APISRPExtBblAcc_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APISRPExtBblAcc{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APISRPExtPlgAcc_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APISRPExtPlgAcc{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APISRPExtPlgType_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APISRPExtPlgType{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APISRPExtPlgPin_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APISRPExtPlgPin{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APISRPExtSV_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APISRPExtSV{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APISRPExtSVCage_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APISRPExtSVCage{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APISRPExtTV_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APISRPExtTV{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APISRPExtTVCage_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APISRPExtTVCage{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APISRPExtTVStPlg_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APISRPExtTVStPlg{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APISRPExtVRod_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APISRPExtVRod{ get; set; }
		[DataMember]
		public Guid? tphFK_r_APISRPExtWiper_GUID{ get; set; }
		[DataMember]
		public string tphFK_r_APISRPExtWiper{ get; set; }
		[DataMember]
		public decimal? tphlBblPlgAvgClearance{ get; set; }
		[DataMember]
		public decimal? tphMaxSL{ get; set; }
		[DataMember]
		public string tphHelpText{ get; set; }
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the TemplatePumpDetail table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class TemplatePumpDetail : XRData<TemplatePumpDetail>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return tpdPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid tpdPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string tpdPrimaryKey{ get; set; }
		[DataMember]
		public DateTime tpdLstChgDT{ get; set; }
		[DataMember]
		public string tpdLstChgUser{ get; set; }
		[DataMember]
		public bool tpdRefCaseDefined{ get; set; }
		[DataMember]
		public bool tpdRefUserDeleted{ get; set; }
		[DataMember]
		public Guid tpdFK_TemplatePump_GUID{ get; set; }
		[DataMember]
		public string tpdFK_TemplatePump{ get; set; }
		[DataMember]
		public Guid tpdFK_TemplateSubAssembly_GUID{ get; set; }
		[DataMember]
		public string tpdFK_TemplateSubAssembly{ get; set; }
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the TemplateSubAssembly table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class TemplateSubAssembly : XRData<TemplateSubAssembly>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return tahPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid tahPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string tahPrimaryKey{ get; set; }
		[DataMember]
		public string tahLanguageCd{ get; set; }
		[DataMember]
		public DateTime tahLstChgDT{ get; set; }
		[DataMember]
		public string tahLstChgUser{ get; set; }
		[DataMember]
		public bool tahRefCaseDefined{ get; set; }
		[DataMember]
		public bool tahRefUserDeleted{ get; set; }
		[DataMember]
		public string tahTemplateAssemblyDescription{ get; set; }
		[DataMember]
		public Guid tahFK_r_APIPumpGraphic_GUID{ get; set; }
		[DataMember]
		public string tahFK_r_APIPumpGraphic{ get; set; }
		[DataMember]
		public Guid tahFK_r_ComponentCategory_GUID{ get; set; }
		[DataMember]
		public string tahFK_r_ComponentCategory{ get; set; }
		[DataMember]
		public string tahHelpText{ get; set; }
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the TemplateSubAssemblyDetail table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class TemplateSubAssemblyDetail : XRData<TemplateSubAssemblyDetail>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return tadPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid tadPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string tadPrimaryKey{ get; set; }
		[DataMember]
		public DateTime tadLstChgDT{ get; set; }
		[DataMember]
		public string tadLstChgUser{ get; set; }
		[DataMember]
		public bool tadRefCaseDefined{ get; set; }
		[DataMember]
		public bool tadRefUserDeleted{ get; set; }
		[DataMember]
		public Guid tadFK_TemplateSubAssembly_GUID{ get; set; }
		[DataMember]
		public string tadFK_TemplateSubAssembly{ get; set; }
		[DataMember]
		public decimal tadAssemblyOrder{ get; set; }
		[DataMember]
		public Guid tadFK_r_PartType_GUID{ get; set; }
		[DataMember]
		public string tadFK_r_PartType{ get; set; }
		[DataMember]
		public Guid tadFK_r_CatalogItem_GUID{ get; set; }
		[DataMember]
		public string tadFK_r_CatalogItem{ get; set; }
		[DataMember]
		public Guid tadFK_r_ComponentGrouping_GUID{ get; set; }
		[DataMember]
		public string tadFK_r_ComponentGrouping{ get; set; }
		[DataMember]
		public decimal tadQuantity{ get; set; }
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the UserMaster table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class UserMaster : XRData<UserMaster>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return usrPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid usrPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string usrPrimaryKey{ get; set; }
		[DataMember]
		public DateTime usrLstChgDT{ get; set; }
		[DataMember]
		public string usrLstChgUser{ get; set; }
		[DataMember]
		public string usrUserName{ get; set; }
		[DataMember]
		public string usrPwd{ get; set; }
		[DataMember]
		public DateTime? usrPwdExpiry{ get; set; }
		[DataMember]
		public string usrFullName{ get; set; }
		[DataMember]
		public string usrSUserName{ get; set; }
		[DataMember]
		public string usrLevel{ get; set; }
		[DataMember]
		public DateTime? usrCreateDT{ get; set; }
		[DataMember]
		public bool? usrPumpService{ get; set; }
		[DataMember]
		public bool usrInActive{ get; set; }
		[DataMember]
		public long usrColor{ get; set; }
		[DataMember]
		public Guid usrFK_Assembly_GUID{ get; set; }
		[DataMember]
		public string usrFK_Assembly{ get; set; }
		override public void AdjustDates( TimeSpan _offset )
		{
			base.AdjustDates( _offset );
			usrPwdExpiry += _offset;			
			usrCreateDT += _offset;			
		}
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the Well table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class Well : XRData<Well>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return welPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid welPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string welPrimaryKey{ get; set; }
		[DataMember]
		public DateTime welLstChgDT{ get; set; }
		[DataMember]
		public string welLstChgUser{ get; set; }
		[DataMember]
		public bool welRefCaseDefined{ get; set; }
		[DataMember]
		public Guid welFK_Assembly_GUID{ get; set; }
		[DataMember]
		public string welFK_Assembly{ get; set; }
		[DataMember]
		public string welUWBID{ get; set; }
		[DataMember]
		public string welFK_r_WellType{ get; set; }
		[DataMember]
		public string welFK_r_WellProfile{ get; set; }
		[DataMember]
		public string welFK_Owner{ get; set; }
		[DataMember]
		public Guid welFK_BusinessOrganization_Producer_GUID{ get; set; }
		[DataMember]
		public string welFK_BusinessOrganization_Producer{ get; set; }
		[DataMember]
		public Guid welFK_Lease_GUID{ get; set; }
		[DataMember]
		public string welFK_Lease{ get; set; }
		[DataMember]
		public string welWellName{ get; set; }
		[DataMember]
		public string welLongWellName{ get; set; }
		[DataMember]
		public bool? welActive{ get; set; }
		[DataMember]
		public decimal? welSurfaceLatitude{ get; set; }
		[DataMember]
		public decimal? welSurfaceLongitude{ get; set; }
		[DataMember]
		public Guid welFK_r_Country_GUID{ get; set; }
		[DataMember]
		public string welFK_r_Country{ get; set; }
		[DataMember]
		public Guid welFK_r_StateProvince_GUID{ get; set; }
		[DataMember]
		public string welFK_r_StateProvince{ get; set; }
		[DataMember]
		public DateTime? welSpudDate{ get; set; }
		[DataMember]
		public DateTime? welCompletionDate{ get; set; }
		[DataMember]
		public DateTime? welAbandonmentDate{ get; set; }
		[DataMember]
		public Guid welFK_r_Field_GUID{ get; set; }
		[DataMember]
		public string welFK_r_Field{ get; set; }
		[DataMember]
		public string welLegalDesc{ get; set; }
		[DataMember]
		public string welCCLTownshipDirection{ get; set; }
		[DataMember]
		public string welCCLTownshipNumber{ get; set; }
		[DataMember]
		public string welCCLRangeDirection{ get; set; }
		[DataMember]
		public string welCCLRangeNumber{ get; set; }
		[DataMember]
		public string welCCLSectionIndicator{ get; set; }
		[DataMember]
		public string welCCLSectionNumber{ get; set; }
		[DataMember]
		public string welCCLUnit{ get; set; }
		[DataMember]
		public string welCCLMeridianCode{ get; set; }
		[DataMember]
		public string welCCLMeridianName{ get; set; }
		[DataMember]
		public Guid welFK_r_Foreman_GUID{ get; set; }
		[DataMember]
		public string welFK_r_Foreman{ get; set; }
		[DataMember]
		public Guid welFK_r_Engineer_GUID{ get; set; }
		[DataMember]
		public string welFK_r_Engineer{ get; set; }
		[DataMember]
		public bool? welPOCinService{ get; set; }
		[DataMember]
		public Guid welFK_BusinessOrganization_PumpService_GUID{ get; set; }
		[DataMember]
		public string welFK_BusinessOrganization_PumpService{ get; set; }
		[DataMember]
		public bool? welPTTaxableStatus{ get; set; }
		[DataMember]
		public decimal? welPTTaxRate{ get; set; }
		[DataMember]
		public string welUserDef10{ get; set; }
		[DataMember]
		public string welRemarks{ get; set; }
		override public void AdjustDates( TimeSpan _offset )
		{
			base.AdjustDates( _offset );
			welSpudDate += _offset;			
			welCompletionDate += _offset;			
			welAbandonmentDate += _offset;			
		}
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the WellCompletionReservoirs table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class WellCompletionReservoirs : XRData<WellCompletionReservoirs>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return wcrPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid wcrPrimaryKey_GUID{ get; set; }
		[DataMember]
		public DateTime wcrLstChgDT{ get; set; }
		[DataMember]
		public string wcrLstChgUser{ get; set; }
		[DataMember]
		public Guid wcrFK_WellCompletion_GUID{ get; set; }
		[DataMember]
		public Guid wcrFK_r_Reservoir_GUID{ get; set; }
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the WellCompletionXRef table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class WellCompletionXRef : XRData<WellCompletionXRef>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return wxrPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid wxrPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string wxrPrimaryKey{ get; set; }
		[DataMember]
		public DateTime wxrLstChgDT{ get; set; }
		[DataMember]
		public string wxrLstChgUser{ get; set; }
		[DataMember]
		public bool wxrRefCaseDefined{ get; set; }
		[DataMember]
		public Guid wxrFK_Well_GUID{ get; set; }
		[DataMember]
		public string wxrFK_Well{ get; set; }
		[DataMember]
		public string wxrAPI12{ get; set; }
		[DataMember]
		public string wxrAPI14{ get; set; }
		[DataMember]
		public string wxrShortWellCompName{ get; set; }
		[DataMember]
		public string wxrLongWellCompName{ get; set; }
		[DataMember]
		public DateTime? wxrCurrentTestDateTime{ get; set; }
		[DataMember]
		public decimal? wxrCurrentTestOil{ get; set; }
		[DataMember]
		public decimal? wxrCurrentTestGas{ get; set; }
		[DataMember]
		public decimal? wxrCurrentTestWater{ get; set; }
		[DataMember]
		public DateTime? wxrCompletionDate{ get; set; }
		[DataMember]
		public DateTime? wxrAbandonmentDate{ get; set; }
		override public void AdjustDates( TimeSpan _offset )
		{
			base.AdjustDates( _offset );
			wxrCurrentTestDateTime += _offset;			
			wxrCompletionDate += _offset;			
			wxrAbandonmentDate += _offset;			
		}
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the Workorder table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class Workorder : XRData<Workorder>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return pswPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid pswPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string pswPrimaryKey{ get; set; }
		[DataMember]
		public DateTime pswLstChgDT{ get; set; }
		[DataMember]
		public string pswLstChgUser{ get; set; }
		[DataMember]
		public Guid pswFK_Job_GUID{ get; set; }
		[DataMember]
		public string pswFK_Job{ get; set; }
		[DataMember]
		public Guid pswFK_r_WorkorderType_GUID{ get; set; }
		[DataMember]
		public string pswFK_r_WorkorderType{ get; set; }
		[DataMember]
		public string pswWorkorderID{ get; set; }
		[DataMember]
		public Guid pswFK_Component_GUID{ get; set; }
		[DataMember]
		public string pswFK_Component{ get; set; }
		[DataMember]
		public string pswWorkorderGrouping{ get; set; }
		[DataMember]
		public DateTime? pswScheduledDateTime{ get; set; }
		[DataMember]
		public Guid pswFK_r_WorkorderStatus_GUID{ get; set; }
		[DataMember]
		public string pswFK_r_WorkorderStatus{ get; set; }
		[DataMember]
		public Guid? pswFK_Invoice_GUID{ get; set; }
		[DataMember]
		public string pswFK_Invoice{ get; set; }
		[DataMember]
		public string pswFK_r_WorkorderTypeTaskType{ get; set; }
		[DataMember]
		public Guid? pswFK_r_WorkorderTypeTaskType_GUID{ get; set; }
		override public void AdjustDates( TimeSpan _offset )
		{
			base.AdjustDates( _offset );
			pswScheduledDateTime += _offset;			
		}
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the WorkorderStatusHistory table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class WorkorderStatusHistory : XRData<WorkorderStatusHistory>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return xhdPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid xhdPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string xhdPrimaryKey{ get; set; }
		[DataMember]
		public DateTime xhdLstChgDT{ get; set; }
		[DataMember]
		public string xhdLstChgUser{ get; set; }
		[DataMember]
		public Guid xhdFK_Workorder_GUID{ get; set; }
		[DataMember]
		public string xhdFK_Workorder{ get; set; }
		[DataMember]
		public Guid xhdFK_r_WorkorderStatus_GUID{ get; set; }
		[DataMember]
		public string xhdFK_r_WorkorderStatus{ get; set; }
		[DataMember]
		public DateTime xhdStatusChangeDT{ get; set; }
		[DataMember]
		public string xhdFK_r_WorkorderTypeTaskType{ get; set; }
		[DataMember]
		public Guid? xhdFK_r_WorkorderTypeTaskType_GUID{ get; set; }
		override public void AdjustDates( TimeSpan _offset )
		{
			base.AdjustDates( _offset );
			xhdStatusChangeDT += _offset;			
		}
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the WorkorderSubAssemblies table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class WorkorderSubAssemblies : XRData<WorkorderSubAssemblies>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return psiPrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid psiPrimaryKey_GUID{ get; set; }
		[DataMember]
		public string psiPrimaryKey{ get; set; }
		[DataMember]
		public DateTime psiLstChgDT{ get; set; }
		[DataMember]
		public string psiLstChgUser{ get; set; }
		[DataMember]
		public Guid psiFK_Workorder_GUID{ get; set; }
		[DataMember]
		public string psiFK_Workorder{ get; set; }
		[DataMember]
		public Guid psiFK_r_ComponentCategory_GUID{ get; set; }
		[DataMember]
		public string psiFK_r_ComponentCategory{ get; set; }
		[DataMember]
		public DateTime psiStartedDT{ get; set; }
		[DataMember]
		public Guid psiFK_Assembly_GUID{ get; set; }
		[DataMember]
		public string psiFK_Assembly{ get; set; }
		[DataMember]
		public Guid psiFK_UserMaster_GUID{ get; set; }
		[DataMember]
		public string psiFK_UserMaster{ get; set; }
		[DataMember]
		public Guid psiFK_r_PSRSubAssemblyStatus_GUID{ get; set; }
		[DataMember]
		public string psiFK_r_PSRSubAssemblyStatus{ get; set; }
		[DataMember]
		public DateTime psiStatusDT{ get; set; }
		[DataMember]
		public string psiCurrentGUIPhase{ get; set; }
		override public void AdjustDates( TimeSpan _offset )
		{
			base.AdjustDates( _offset );
			psiStartedDT += _offset;			
			psiStatusDT += _offset;			
		}
			
    } 
    /// <summary>
    /// A partial class to work with the WCF Service Library which represents the WorkorderSubAssembliesStatusHistory table in the PumpServicing Database.
    /// </summary>
    [DataContract]
    public partial class WorkorderSubAssembliesStatusHistory : XRData<WorkorderSubAssembliesStatusHistory>
    {
		override public string TablePrimaryKey
		{
			get
			{
				return xh4PrimaryKey_GUID.ToString( );
			}
		}
		[DataMember]
		public Guid xh4PrimaryKey_GUID{ get; set; }
		[DataMember]
		public string xh4PrimaryKey{ get; set; }
		[DataMember]
		public DateTime xh4LstChgDT{ get; set; }
		[DataMember]
		public string xh4LstChgUser{ get; set; }
		[DataMember]
		public Guid xh4FK_WorkorderSubAssemblies_GUID{ get; set; }
		[DataMember]
		public string xh4FK_WorkorderSubAssemblies{ get; set; }
		[DataMember]
		public Guid xh4FK_r_PSRSubAssemblyStatus_GUID{ get; set; }
		[DataMember]
		public string xh4FK_r_PSRSubAssemblyStatus{ get; set; }
			
    } 
}
