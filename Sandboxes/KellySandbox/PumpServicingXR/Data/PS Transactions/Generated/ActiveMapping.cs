


using LFI.Sync.Shared;

namespace WFT.PSService.Data
{
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the Assembly table in the PumpServicing Database.
    /// </summary>
    public sealed partial class AssemblyMap : TableMap
    {
		public const string TABLE_NAME = "Assembly";
		
		public static string ID = "aclPrimaryKey_GUID";
		public static string aclPrimaryKey = "aclPrimaryKey";
		public static string LastModified = "aclLstChgDT";
		public static string aclLstChgUser = "aclLstChgUser";
		public static string aclRefCaseDefined = "aclRefCaseDefined";
		public static string aclFK_r_AssemblyType_GUID = "aclFK_r_AssemblyType_GUID";
		public static string aclFK_r_AssemblyType = "aclFK_r_AssemblyType";
		public static string aclAssemblyName = "aclAssemblyName";
		public static string aclTemplate = "aclTemplate";
		
		public sealed partial class Prefixed
		{
			public static string ID = "Assembly.aclPrimaryKey_GUID";
			public static string aclPrimaryKey = "Assembly.aclPrimaryKey";
			public static string LastModified = "Assembly.aclLstChgDT";
			public static string aclLstChgUser = "Assembly.aclLstChgUser";
			public static string aclRefCaseDefined = "Assembly.aclRefCaseDefined";
			public static string aclFK_r_AssemblyType_GUID = "Assembly.aclFK_r_AssemblyType_GUID";
			public static string aclFK_r_AssemblyType = "Assembly.aclFK_r_AssemblyType";
			public static string aclAssemblyName = "Assembly.aclAssemblyName";
			public static string aclTemplate = "Assembly.aclTemplate";
		}

		public sealed partial class Aliased
		{
			public static string ID = "AssemblyaclPrimaryKey_GUID";
			public static string aclPrimaryKey = "AssemblyaclPrimaryKey";
			public static string LastModified = "AssemblyaclLstChgDT";
			public static string aclLstChgUser = "AssemblyaclLstChgUser";
			public static string aclRefCaseDefined = "AssemblyaclRefCaseDefined";
			public static string aclFK_r_AssemblyType_GUID = "AssemblyaclFK_r_AssemblyType_GUID";
			public static string aclFK_r_AssemblyType = "AssemblyaclFK_r_AssemblyType";
			public static string aclAssemblyName = "AssemblyaclAssemblyName";
			public static string aclTemplate = "AssemblyaclTemplate";
		}

		public sealed partial class Param
		{
			public static string ID = "@aclPrimaryKey_GUID";
			public static string aclPrimaryKey = "@aclPrimaryKey";
			public static string LastModified = "@aclLstChgDT";
			public static string aclLstChgUser = "@aclLstChgUser";
			public static string aclRefCaseDefined = "@aclRefCaseDefined";
			public static string aclFK_r_AssemblyType_GUID = "@aclFK_r_AssemblyType_GUID";
			public static string aclFK_r_AssemblyType = "@aclFK_r_AssemblyType";
			public static string aclAssemblyName = "@aclAssemblyName";
			public static string aclTemplate = "@aclTemplate";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the AssemblyComponent table in the PumpServicing Database.
    /// </summary>
    public sealed partial class AssemblyComponentMap : TableMap
    {
		public const string TABLE_NAME = "AssemblyComponent";
		
		public static string ID = "ascPrimaryKey_GUID";
		public static string ascPrimaryKey = "ascPrimaryKey";
		public static string LastModified = "ascLstChgDT";
		public static string ascLstChgUser = "ascLstChgUser";
		public static string ascFK_Assembly_GUID = "ascFK_Assembly_GUID";
		public static string ascFK_Assembly = "ascFK_Assembly";
		public static string ascFK_Component_GUID = "ascFK_Component_GUID";
		public static string ascFK_Component = "ascFK_Component";
		public static string ascFK_Event_Beginning_GUID = "ascFK_Event_Beginning_GUID";
		public static string ascFK_Event_Beginning = "ascFK_Event_Beginning";
		public static string ascBegEventDT = "ascBegEventDT";
		public static string ascFK_Event_Ending_GUID = "ascFK_Event_Ending_GUID";
		public static string ascFK_Event_Ending = "ascFK_Event_Ending";
		public static string ascEndEventDT = "ascEndEventDT";
		public static string ascAssemblyOrder = "ascAssemblyOrder";
		public static string ascFK_r_ComponentGrouping_GUID = "ascFK_r_ComponentGrouping_GUID";
		public static string ascFK_r_ComponentGrouping = "ascFK_r_ComponentGrouping";
		public static string ascQuantity = "ascQuantity";
		public static string ascLength = "ascLength";
		public static string ascTopDepth = "ascTopDepth";
		public static string ascBottomDepth = "ascBottomDepth";
		public static string ascTrueVerticalDepth = "ascTrueVerticalDepth";
		public static string ascTrueVerticalDepthBottom = "ascTrueVerticalDepthBottom";
		public static string ascPreviousRunDays = "ascPreviousRunDays";
		public static string ascPreviousRunDaysDT = "ascPreviousRunDaysDT";
		public static string ascRemark = "ascRemark";
		public static string ascFK_r_ComponentCategory_GUID = "ascFK_r_ComponentCategory_GUID";
		public static string ascFK_r_ComponentCategory = "ascFK_r_ComponentCategory";
		
		public sealed partial class Prefixed
		{
			public static string ID = "AssemblyComponent.ascPrimaryKey_GUID";
			public static string ascPrimaryKey = "AssemblyComponent.ascPrimaryKey";
			public static string LastModified = "AssemblyComponent.ascLstChgDT";
			public static string ascLstChgUser = "AssemblyComponent.ascLstChgUser";
			public static string ascFK_Assembly_GUID = "AssemblyComponent.ascFK_Assembly_GUID";
			public static string ascFK_Assembly = "AssemblyComponent.ascFK_Assembly";
			public static string ascFK_Component_GUID = "AssemblyComponent.ascFK_Component_GUID";
			public static string ascFK_Component = "AssemblyComponent.ascFK_Component";
			public static string ascFK_Event_Beginning_GUID = "AssemblyComponent.ascFK_Event_Beginning_GUID";
			public static string ascFK_Event_Beginning = "AssemblyComponent.ascFK_Event_Beginning";
			public static string ascBegEventDT = "AssemblyComponent.ascBegEventDT";
			public static string ascFK_Event_Ending_GUID = "AssemblyComponent.ascFK_Event_Ending_GUID";
			public static string ascFK_Event_Ending = "AssemblyComponent.ascFK_Event_Ending";
			public static string ascEndEventDT = "AssemblyComponent.ascEndEventDT";
			public static string ascAssemblyOrder = "AssemblyComponent.ascAssemblyOrder";
			public static string ascFK_r_ComponentGrouping_GUID = "AssemblyComponent.ascFK_r_ComponentGrouping_GUID";
			public static string ascFK_r_ComponentGrouping = "AssemblyComponent.ascFK_r_ComponentGrouping";
			public static string ascQuantity = "AssemblyComponent.ascQuantity";
			public static string ascLength = "AssemblyComponent.ascLength";
			public static string ascTopDepth = "AssemblyComponent.ascTopDepth";
			public static string ascBottomDepth = "AssemblyComponent.ascBottomDepth";
			public static string ascTrueVerticalDepth = "AssemblyComponent.ascTrueVerticalDepth";
			public static string ascTrueVerticalDepthBottom = "AssemblyComponent.ascTrueVerticalDepthBottom";
			public static string ascPreviousRunDays = "AssemblyComponent.ascPreviousRunDays";
			public static string ascPreviousRunDaysDT = "AssemblyComponent.ascPreviousRunDaysDT";
			public static string ascRemark = "AssemblyComponent.ascRemark";
			public static string ascFK_r_ComponentCategory_GUID = "AssemblyComponent.ascFK_r_ComponentCategory_GUID";
			public static string ascFK_r_ComponentCategory = "AssemblyComponent.ascFK_r_ComponentCategory";
		}

		public sealed partial class Aliased
		{
			public static string ID = "AssemblyComponentascPrimaryKey_GUID";
			public static string ascPrimaryKey = "AssemblyComponentascPrimaryKey";
			public static string LastModified = "AssemblyComponentascLstChgDT";
			public static string ascLstChgUser = "AssemblyComponentascLstChgUser";
			public static string ascFK_Assembly_GUID = "AssemblyComponentascFK_Assembly_GUID";
			public static string ascFK_Assembly = "AssemblyComponentascFK_Assembly";
			public static string ascFK_Component_GUID = "AssemblyComponentascFK_Component_GUID";
			public static string ascFK_Component = "AssemblyComponentascFK_Component";
			public static string ascFK_Event_Beginning_GUID = "AssemblyComponentascFK_Event_Beginning_GUID";
			public static string ascFK_Event_Beginning = "AssemblyComponentascFK_Event_Beginning";
			public static string ascBegEventDT = "AssemblyComponentascBegEventDT";
			public static string ascFK_Event_Ending_GUID = "AssemblyComponentascFK_Event_Ending_GUID";
			public static string ascFK_Event_Ending = "AssemblyComponentascFK_Event_Ending";
			public static string ascEndEventDT = "AssemblyComponentascEndEventDT";
			public static string ascAssemblyOrder = "AssemblyComponentascAssemblyOrder";
			public static string ascFK_r_ComponentGrouping_GUID = "AssemblyComponentascFK_r_ComponentGrouping_GUID";
			public static string ascFK_r_ComponentGrouping = "AssemblyComponentascFK_r_ComponentGrouping";
			public static string ascQuantity = "AssemblyComponentascQuantity";
			public static string ascLength = "AssemblyComponentascLength";
			public static string ascTopDepth = "AssemblyComponentascTopDepth";
			public static string ascBottomDepth = "AssemblyComponentascBottomDepth";
			public static string ascTrueVerticalDepth = "AssemblyComponentascTrueVerticalDepth";
			public static string ascTrueVerticalDepthBottom = "AssemblyComponentascTrueVerticalDepthBottom";
			public static string ascPreviousRunDays = "AssemblyComponentascPreviousRunDays";
			public static string ascPreviousRunDaysDT = "AssemblyComponentascPreviousRunDaysDT";
			public static string ascRemark = "AssemblyComponentascRemark";
			public static string ascFK_r_ComponentCategory_GUID = "AssemblyComponentascFK_r_ComponentCategory_GUID";
			public static string ascFK_r_ComponentCategory = "AssemblyComponentascFK_r_ComponentCategory";
		}

		public sealed partial class Param
		{
			public static string ID = "@ascPrimaryKey_GUID";
			public static string ascPrimaryKey = "@ascPrimaryKey";
			public static string LastModified = "@ascLstChgDT";
			public static string ascLstChgUser = "@ascLstChgUser";
			public static string ascFK_Assembly_GUID = "@ascFK_Assembly_GUID";
			public static string ascFK_Assembly = "@ascFK_Assembly";
			public static string ascFK_Component_GUID = "@ascFK_Component_GUID";
			public static string ascFK_Component = "@ascFK_Component";
			public static string ascFK_Event_Beginning_GUID = "@ascFK_Event_Beginning_GUID";
			public static string ascFK_Event_Beginning = "@ascFK_Event_Beginning";
			public static string ascBegEventDT = "@ascBegEventDT";
			public static string ascFK_Event_Ending_GUID = "@ascFK_Event_Ending_GUID";
			public static string ascFK_Event_Ending = "@ascFK_Event_Ending";
			public static string ascEndEventDT = "@ascEndEventDT";
			public static string ascAssemblyOrder = "@ascAssemblyOrder";
			public static string ascFK_r_ComponentGrouping_GUID = "@ascFK_r_ComponentGrouping_GUID";
			public static string ascFK_r_ComponentGrouping = "@ascFK_r_ComponentGrouping";
			public static string ascQuantity = "@ascQuantity";
			public static string ascLength = "@ascLength";
			public static string ascTopDepth = "@ascTopDepth";
			public static string ascBottomDepth = "@ascBottomDepth";
			public static string ascTrueVerticalDepth = "@ascTrueVerticalDepth";
			public static string ascTrueVerticalDepthBottom = "@ascTrueVerticalDepthBottom";
			public static string ascPreviousRunDays = "@ascPreviousRunDays";
			public static string ascPreviousRunDaysDT = "@ascPreviousRunDaysDT";
			public static string ascRemark = "@ascRemark";
			public static string ascFK_r_ComponentCategory_GUID = "@ascFK_r_ComponentCategory_GUID";
			public static string ascFK_r_ComponentCategory = "@ascFK_r_ComponentCategory";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the AssemblyComponentBblPlgMeasure table in the PumpServicing Database.
    /// </summary>
    public sealed partial class AssemblyComponentBblPlgMeasureMap : TableMap
    {
		public const string TABLE_NAME = "AssemblyComponentBblPlgMeasure";
		
		public static string ID = "acmPrimaryKey_GUID";
		public static string acmPrimaryKey = "acmPrimaryKey";
		public static string LastModified = "acmLstChgDT";
		public static string acmLstChgUser = "acmLstChgUser";
		public static string acmFK_AssemblyComponent_GUID = "acmFK_AssemblyComponent_GUID";
		public static string acmFK_AssemblyComponent = "acmFK_AssemblyComponent";
		public static string acmFK_Event_GUID = "acmFK_Event_GUID";
		public static string acmFK_Event = "acmFK_Event";
		public static string acmPosition = "acmPosition";
		public static string acmMeasurement = "acmMeasurement";
		
		public sealed partial class Prefixed
		{
			public static string ID = "AssemblyComponentBblPlgMeasure.acmPrimaryKey_GUID";
			public static string acmPrimaryKey = "AssemblyComponentBblPlgMeasure.acmPrimaryKey";
			public static string LastModified = "AssemblyComponentBblPlgMeasure.acmLstChgDT";
			public static string acmLstChgUser = "AssemblyComponentBblPlgMeasure.acmLstChgUser";
			public static string acmFK_AssemblyComponent_GUID = "AssemblyComponentBblPlgMeasure.acmFK_AssemblyComponent_GUID";
			public static string acmFK_AssemblyComponent = "AssemblyComponentBblPlgMeasure.acmFK_AssemblyComponent";
			public static string acmFK_Event_GUID = "AssemblyComponentBblPlgMeasure.acmFK_Event_GUID";
			public static string acmFK_Event = "AssemblyComponentBblPlgMeasure.acmFK_Event";
			public static string acmPosition = "AssemblyComponentBblPlgMeasure.acmPosition";
			public static string acmMeasurement = "AssemblyComponentBblPlgMeasure.acmMeasurement";
		}

		public sealed partial class Aliased
		{
			public static string ID = "AssemblyComponentBblPlgMeasureacmPrimaryKey_GUID";
			public static string acmPrimaryKey = "AssemblyComponentBblPlgMeasureacmPrimaryKey";
			public static string LastModified = "AssemblyComponentBblPlgMeasureacmLstChgDT";
			public static string acmLstChgUser = "AssemblyComponentBblPlgMeasureacmLstChgUser";
			public static string acmFK_AssemblyComponent_GUID = "AssemblyComponentBblPlgMeasureacmFK_AssemblyComponent_GUID";
			public static string acmFK_AssemblyComponent = "AssemblyComponentBblPlgMeasureacmFK_AssemblyComponent";
			public static string acmFK_Event_GUID = "AssemblyComponentBblPlgMeasureacmFK_Event_GUID";
			public static string acmFK_Event = "AssemblyComponentBblPlgMeasureacmFK_Event";
			public static string acmPosition = "AssemblyComponentBblPlgMeasureacmPosition";
			public static string acmMeasurement = "AssemblyComponentBblPlgMeasureacmMeasurement";
		}

		public sealed partial class Param
		{
			public static string ID = "@acmPrimaryKey_GUID";
			public static string acmPrimaryKey = "@acmPrimaryKey";
			public static string LastModified = "@acmLstChgDT";
			public static string acmLstChgUser = "@acmLstChgUser";
			public static string acmFK_AssemblyComponent_GUID = "@acmFK_AssemblyComponent_GUID";
			public static string acmFK_AssemblyComponent = "@acmFK_AssemblyComponent";
			public static string acmFK_Event_GUID = "@acmFK_Event_GUID";
			public static string acmFK_Event = "@acmFK_Event";
			public static string acmPosition = "@acmPosition";
			public static string acmMeasurement = "@acmMeasurement";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the AssemblyComponentSRPump table in the PumpServicing Database.
    /// </summary>
    public sealed partial class AssemblyComponentSRPumpMap : TableMap
    {
		public const string TABLE_NAME = "AssemblyComponentSRPump";
		
		public static string ID = "arpPrimaryKey_GUID";
		public static string arpPrimaryKey = "arpPrimaryKey";
		public static string LastModified = "arpLstChgDT";
		public static string arpLstChgUser = "arpLstChgUser";
		public static string arpFK_AssemblyComponent_GUID = "arpFK_AssemblyComponent_GUID";
		public static string arpFK_AssemblyComponent = "arpFK_AssemblyComponent";
		public static string arpAPIDescription = "arpAPIDescription";
		public static string arpAPIExtraDescriptionText = "arpAPIExtraDescriptionText";
		public static string arpFK_r_APIPumpGraphics_GUID = "arpFK_r_APIPumpGraphics_GUID";
		public static string arpFK_r_APIPumpGraphics = "arpFK_r_APIPumpGraphics";
		public static string arpFK_r_APISRPTubingSize_GUID = "arpFK_r_APISRPTubingSize_GUID";
		public static string arpFK_r_APISRPTubingSize = "arpFK_r_APISRPTubingSize";
		public static string arpFK_r_APISRPPumpBore_GUID = "arpFK_r_APISRPPumpBore_GUID";
		public static string arpFK_r_APISRPPumpBore = "arpFK_r_APISRPPumpBore";
		public static string arpFK_r_APISRPPumpType_GUID = "arpFK_r_APISRPPumpType_GUID";
		public static string arpFK_r_APISRPPumpType = "arpFK_r_APISRPPumpType";
		public static string arpFK_r_APISRPBarrelType_GUID = "arpFK_r_APISRPBarrelType_GUID";
		public static string arpFK_r_APISRPBarrelType = "arpFK_r_APISRPBarrelType";
		public static string arpFK_r_APISRPSeatAssyLocation_GUID = "arpFK_r_APISRPSeatAssyLocation_GUID";
		public static string arpFK_r_APISRPSeatAssyLocation = "arpFK_r_APISRPSeatAssyLocation";
		public static string arpFK_r_APISRPSeatAssyType_GUID = "arpFK_r_APISRPSeatAssyType_GUID";
		public static string arpFK_r_APISRPSeatAssyType = "arpFK_r_APISRPSeatAssyType";
		public static string arpAPIBarrelLength = "arpAPIBarrelLength";
		public static string arpAPIPlungerLength = "arpAPIPlungerLength";
		public static string arpAPIExtensionCouplingUpperLength = "arpAPIExtensionCouplingUpperLength";
		public static string arpAPIExtensionCouplingLowerLength = "arpAPIExtensionCouplingLowerLength";
		public static string arpFK_r_APISRPExtPumpType_GUID = "arpFK_r_APISRPExtPumpType_GUID";
		public static string arpFK_r_APISRPExtPumpType = "arpFK_r_APISRPExtPumpType";
		public static string arpFK_r_APISRPExtBarrelType_GUID = "arpFK_r_APISRPExtBarrelType_GUID";
		public static string arpFK_r_APISRPExtBarrelType = "arpFK_r_APISRPExtBarrelType";
		public static string arpFK_r_APISRPExtSeatAssyLocation_GUID = "arpFK_r_APISRPExtSeatAssyLocation_GUID";
		public static string arpFK_r_APISRPExtSeatAssyLocation = "arpFK_r_APISRPExtSeatAssyLocation";
		public static string arpFK_r_APISRPExtSeatAssyType_GUID = "arpFK_r_APISRPExtSeatAssyType_GUID";
		public static string arpFK_r_APISRPExtSeatAssyType = "arpFK_r_APISRPExtSeatAssyType";
		public static string arpFK_r_APISRPExtSand_GUID = "arpFK_r_APISRPExtSand_GUID";
		public static string arpFK_r_APISRPExtSand = "arpFK_r_APISRPExtSand";
		public static string arpFK_r_APISRPExtBblAcc_GUID = "arpFK_r_APISRPExtBblAcc_GUID";
		public static string arpFK_r_APISRPExtBblAcc = "arpFK_r_APISRPExtBblAcc";
		public static string arpFK_r_APISRPExtPlgAcc_GUID = "arpFK_r_APISRPExtPlgAcc_GUID";
		public static string arpFK_r_APISRPExtPlgAcc = "arpFK_r_APISRPExtPlgAcc";
		public static string arpFK_r_APISRPExtPlgType_GUID = "arpFK_r_APISRPExtPlgType_GUID";
		public static string arpFK_r_APISRPExtPlgType = "arpFK_r_APISRPExtPlgType";
		public static string arpFK_r_APISRPExtPlgPin_GUID = "arpFK_r_APISRPExtPlgPin_GUID";
		public static string arpFK_r_APISRPExtPlgPin = "arpFK_r_APISRPExtPlgPin";
		public static string arpFK_r_APISRPExtSV_GUID = "arpFK_r_APISRPExtSV_GUID";
		public static string arpFK_r_APISRPExtSV = "arpFK_r_APISRPExtSV";
		public static string arpFK_r_APISRPExtSVCage_GUID = "arpFK_r_APISRPExtSVCage_GUID";
		public static string arpFK_r_APISRPExtSVCage = "arpFK_r_APISRPExtSVCage";
		public static string arpFK_r_APISRPExtTV_GUID = "arpFK_r_APISRPExtTV_GUID";
		public static string arpFK_r_APISRPExtTV = "arpFK_r_APISRPExtTV";
		public static string arpFK_r_APISRPExtTVCage_GUID = "arpFK_r_APISRPExtTVCage_GUID";
		public static string arpFK_r_APISRPExtTVCage = "arpFK_r_APISRPExtTVCage";
		public static string arpFK_r_APISRPExtTVStPlg_GUID = "arpFK_r_APISRPExtTVStPlg_GUID";
		public static string arpFK_r_APISRPExtTVStPlg = "arpFK_r_APISRPExtTVStPlg";
		public static string arpFK_r_APISRPExtVRod_GUID = "arpFK_r_APISRPExtVRod_GUID";
		public static string arpFK_r_APISRPExtVRod = "arpFK_r_APISRPExtVRod";
		public static string arpFK_r_APISRPExtWiper_GUID = "arpFK_r_APISRPExtWiper_GUID";
		public static string arpFK_r_APISRPExtWiper = "arpFK_r_APISRPExtWiper";
		public static string arpBblPlgAvgClearance = "arpBblPlgAvgClearance";
		public static string arpMaxSL = "arpMaxSL";
		
		public sealed partial class Prefixed
		{
			public static string ID = "AssemblyComponentSRPump.arpPrimaryKey_GUID";
			public static string arpPrimaryKey = "AssemblyComponentSRPump.arpPrimaryKey";
			public static string LastModified = "AssemblyComponentSRPump.arpLstChgDT";
			public static string arpLstChgUser = "AssemblyComponentSRPump.arpLstChgUser";
			public static string arpFK_AssemblyComponent_GUID = "AssemblyComponentSRPump.arpFK_AssemblyComponent_GUID";
			public static string arpFK_AssemblyComponent = "AssemblyComponentSRPump.arpFK_AssemblyComponent";
			public static string arpAPIDescription = "AssemblyComponentSRPump.arpAPIDescription";
			public static string arpAPIExtraDescriptionText = "AssemblyComponentSRPump.arpAPIExtraDescriptionText";
			public static string arpFK_r_APIPumpGraphics_GUID = "AssemblyComponentSRPump.arpFK_r_APIPumpGraphics_GUID";
			public static string arpFK_r_APIPumpGraphics = "AssemblyComponentSRPump.arpFK_r_APIPumpGraphics";
			public static string arpFK_r_APISRPTubingSize_GUID = "AssemblyComponentSRPump.arpFK_r_APISRPTubingSize_GUID";
			public static string arpFK_r_APISRPTubingSize = "AssemblyComponentSRPump.arpFK_r_APISRPTubingSize";
			public static string arpFK_r_APISRPPumpBore_GUID = "AssemblyComponentSRPump.arpFK_r_APISRPPumpBore_GUID";
			public static string arpFK_r_APISRPPumpBore = "AssemblyComponentSRPump.arpFK_r_APISRPPumpBore";
			public static string arpFK_r_APISRPPumpType_GUID = "AssemblyComponentSRPump.arpFK_r_APISRPPumpType_GUID";
			public static string arpFK_r_APISRPPumpType = "AssemblyComponentSRPump.arpFK_r_APISRPPumpType";
			public static string arpFK_r_APISRPBarrelType_GUID = "AssemblyComponentSRPump.arpFK_r_APISRPBarrelType_GUID";
			public static string arpFK_r_APISRPBarrelType = "AssemblyComponentSRPump.arpFK_r_APISRPBarrelType";
			public static string arpFK_r_APISRPSeatAssyLocation_GUID = "AssemblyComponentSRPump.arpFK_r_APISRPSeatAssyLocation_GUID";
			public static string arpFK_r_APISRPSeatAssyLocation = "AssemblyComponentSRPump.arpFK_r_APISRPSeatAssyLocation";
			public static string arpFK_r_APISRPSeatAssyType_GUID = "AssemblyComponentSRPump.arpFK_r_APISRPSeatAssyType_GUID";
			public static string arpFK_r_APISRPSeatAssyType = "AssemblyComponentSRPump.arpFK_r_APISRPSeatAssyType";
			public static string arpAPIBarrelLength = "AssemblyComponentSRPump.arpAPIBarrelLength";
			public static string arpAPIPlungerLength = "AssemblyComponentSRPump.arpAPIPlungerLength";
			public static string arpAPIExtensionCouplingUpperLength = "AssemblyComponentSRPump.arpAPIExtensionCouplingUpperLength";
			public static string arpAPIExtensionCouplingLowerLength = "AssemblyComponentSRPump.arpAPIExtensionCouplingLowerLength";
			public static string arpFK_r_APISRPExtPumpType_GUID = "AssemblyComponentSRPump.arpFK_r_APISRPExtPumpType_GUID";
			public static string arpFK_r_APISRPExtPumpType = "AssemblyComponentSRPump.arpFK_r_APISRPExtPumpType";
			public static string arpFK_r_APISRPExtBarrelType_GUID = "AssemblyComponentSRPump.arpFK_r_APISRPExtBarrelType_GUID";
			public static string arpFK_r_APISRPExtBarrelType = "AssemblyComponentSRPump.arpFK_r_APISRPExtBarrelType";
			public static string arpFK_r_APISRPExtSeatAssyLocation_GUID = "AssemblyComponentSRPump.arpFK_r_APISRPExtSeatAssyLocation_GUID";
			public static string arpFK_r_APISRPExtSeatAssyLocation = "AssemblyComponentSRPump.arpFK_r_APISRPExtSeatAssyLocation";
			public static string arpFK_r_APISRPExtSeatAssyType_GUID = "AssemblyComponentSRPump.arpFK_r_APISRPExtSeatAssyType_GUID";
			public static string arpFK_r_APISRPExtSeatAssyType = "AssemblyComponentSRPump.arpFK_r_APISRPExtSeatAssyType";
			public static string arpFK_r_APISRPExtSand_GUID = "AssemblyComponentSRPump.arpFK_r_APISRPExtSand_GUID";
			public static string arpFK_r_APISRPExtSand = "AssemblyComponentSRPump.arpFK_r_APISRPExtSand";
			public static string arpFK_r_APISRPExtBblAcc_GUID = "AssemblyComponentSRPump.arpFK_r_APISRPExtBblAcc_GUID";
			public static string arpFK_r_APISRPExtBblAcc = "AssemblyComponentSRPump.arpFK_r_APISRPExtBblAcc";
			public static string arpFK_r_APISRPExtPlgAcc_GUID = "AssemblyComponentSRPump.arpFK_r_APISRPExtPlgAcc_GUID";
			public static string arpFK_r_APISRPExtPlgAcc = "AssemblyComponentSRPump.arpFK_r_APISRPExtPlgAcc";
			public static string arpFK_r_APISRPExtPlgType_GUID = "AssemblyComponentSRPump.arpFK_r_APISRPExtPlgType_GUID";
			public static string arpFK_r_APISRPExtPlgType = "AssemblyComponentSRPump.arpFK_r_APISRPExtPlgType";
			public static string arpFK_r_APISRPExtPlgPin_GUID = "AssemblyComponentSRPump.arpFK_r_APISRPExtPlgPin_GUID";
			public static string arpFK_r_APISRPExtPlgPin = "AssemblyComponentSRPump.arpFK_r_APISRPExtPlgPin";
			public static string arpFK_r_APISRPExtSV_GUID = "AssemblyComponentSRPump.arpFK_r_APISRPExtSV_GUID";
			public static string arpFK_r_APISRPExtSV = "AssemblyComponentSRPump.arpFK_r_APISRPExtSV";
			public static string arpFK_r_APISRPExtSVCage_GUID = "AssemblyComponentSRPump.arpFK_r_APISRPExtSVCage_GUID";
			public static string arpFK_r_APISRPExtSVCage = "AssemblyComponentSRPump.arpFK_r_APISRPExtSVCage";
			public static string arpFK_r_APISRPExtTV_GUID = "AssemblyComponentSRPump.arpFK_r_APISRPExtTV_GUID";
			public static string arpFK_r_APISRPExtTV = "AssemblyComponentSRPump.arpFK_r_APISRPExtTV";
			public static string arpFK_r_APISRPExtTVCage_GUID = "AssemblyComponentSRPump.arpFK_r_APISRPExtTVCage_GUID";
			public static string arpFK_r_APISRPExtTVCage = "AssemblyComponentSRPump.arpFK_r_APISRPExtTVCage";
			public static string arpFK_r_APISRPExtTVStPlg_GUID = "AssemblyComponentSRPump.arpFK_r_APISRPExtTVStPlg_GUID";
			public static string arpFK_r_APISRPExtTVStPlg = "AssemblyComponentSRPump.arpFK_r_APISRPExtTVStPlg";
			public static string arpFK_r_APISRPExtVRod_GUID = "AssemblyComponentSRPump.arpFK_r_APISRPExtVRod_GUID";
			public static string arpFK_r_APISRPExtVRod = "AssemblyComponentSRPump.arpFK_r_APISRPExtVRod";
			public static string arpFK_r_APISRPExtWiper_GUID = "AssemblyComponentSRPump.arpFK_r_APISRPExtWiper_GUID";
			public static string arpFK_r_APISRPExtWiper = "AssemblyComponentSRPump.arpFK_r_APISRPExtWiper";
			public static string arpBblPlgAvgClearance = "AssemblyComponentSRPump.arpBblPlgAvgClearance";
			public static string arpMaxSL = "AssemblyComponentSRPump.arpMaxSL";
		}

		public sealed partial class Aliased
		{
			public static string ID = "AssemblyComponentSRPumparpPrimaryKey_GUID";
			public static string arpPrimaryKey = "AssemblyComponentSRPumparpPrimaryKey";
			public static string LastModified = "AssemblyComponentSRPumparpLstChgDT";
			public static string arpLstChgUser = "AssemblyComponentSRPumparpLstChgUser";
			public static string arpFK_AssemblyComponent_GUID = "AssemblyComponentSRPumparpFK_AssemblyComponent_GUID";
			public static string arpFK_AssemblyComponent = "AssemblyComponentSRPumparpFK_AssemblyComponent";
			public static string arpAPIDescription = "AssemblyComponentSRPumparpAPIDescription";
			public static string arpAPIExtraDescriptionText = "AssemblyComponentSRPumparpAPIExtraDescriptionText";
			public static string arpFK_r_APIPumpGraphics_GUID = "AssemblyComponentSRPumparpFK_r_APIPumpGraphics_GUID";
			public static string arpFK_r_APIPumpGraphics = "AssemblyComponentSRPumparpFK_r_APIPumpGraphics";
			public static string arpFK_r_APISRPTubingSize_GUID = "AssemblyComponentSRPumparpFK_r_APISRPTubingSize_GUID";
			public static string arpFK_r_APISRPTubingSize = "AssemblyComponentSRPumparpFK_r_APISRPTubingSize";
			public static string arpFK_r_APISRPPumpBore_GUID = "AssemblyComponentSRPumparpFK_r_APISRPPumpBore_GUID";
			public static string arpFK_r_APISRPPumpBore = "AssemblyComponentSRPumparpFK_r_APISRPPumpBore";
			public static string arpFK_r_APISRPPumpType_GUID = "AssemblyComponentSRPumparpFK_r_APISRPPumpType_GUID";
			public static string arpFK_r_APISRPPumpType = "AssemblyComponentSRPumparpFK_r_APISRPPumpType";
			public static string arpFK_r_APISRPBarrelType_GUID = "AssemblyComponentSRPumparpFK_r_APISRPBarrelType_GUID";
			public static string arpFK_r_APISRPBarrelType = "AssemblyComponentSRPumparpFK_r_APISRPBarrelType";
			public static string arpFK_r_APISRPSeatAssyLocation_GUID = "AssemblyComponentSRPumparpFK_r_APISRPSeatAssyLocation_GUID";
			public static string arpFK_r_APISRPSeatAssyLocation = "AssemblyComponentSRPumparpFK_r_APISRPSeatAssyLocation";
			public static string arpFK_r_APISRPSeatAssyType_GUID = "AssemblyComponentSRPumparpFK_r_APISRPSeatAssyType_GUID";
			public static string arpFK_r_APISRPSeatAssyType = "AssemblyComponentSRPumparpFK_r_APISRPSeatAssyType";
			public static string arpAPIBarrelLength = "AssemblyComponentSRPumparpAPIBarrelLength";
			public static string arpAPIPlungerLength = "AssemblyComponentSRPumparpAPIPlungerLength";
			public static string arpAPIExtensionCouplingUpperLength = "AssemblyComponentSRPumparpAPIExtensionCouplingUpperLength";
			public static string arpAPIExtensionCouplingLowerLength = "AssemblyComponentSRPumparpAPIExtensionCouplingLowerLength";
			public static string arpFK_r_APISRPExtPumpType_GUID = "AssemblyComponentSRPumparpFK_r_APISRPExtPumpType_GUID";
			public static string arpFK_r_APISRPExtPumpType = "AssemblyComponentSRPumparpFK_r_APISRPExtPumpType";
			public static string arpFK_r_APISRPExtBarrelType_GUID = "AssemblyComponentSRPumparpFK_r_APISRPExtBarrelType_GUID";
			public static string arpFK_r_APISRPExtBarrelType = "AssemblyComponentSRPumparpFK_r_APISRPExtBarrelType";
			public static string arpFK_r_APISRPExtSeatAssyLocation_GUID = "AssemblyComponentSRPumparpFK_r_APISRPExtSeatAssyLocation_GUID";
			public static string arpFK_r_APISRPExtSeatAssyLocation = "AssemblyComponentSRPumparpFK_r_APISRPExtSeatAssyLocation";
			public static string arpFK_r_APISRPExtSeatAssyType_GUID = "AssemblyComponentSRPumparpFK_r_APISRPExtSeatAssyType_GUID";
			public static string arpFK_r_APISRPExtSeatAssyType = "AssemblyComponentSRPumparpFK_r_APISRPExtSeatAssyType";
			public static string arpFK_r_APISRPExtSand_GUID = "AssemblyComponentSRPumparpFK_r_APISRPExtSand_GUID";
			public static string arpFK_r_APISRPExtSand = "AssemblyComponentSRPumparpFK_r_APISRPExtSand";
			public static string arpFK_r_APISRPExtBblAcc_GUID = "AssemblyComponentSRPumparpFK_r_APISRPExtBblAcc_GUID";
			public static string arpFK_r_APISRPExtBblAcc = "AssemblyComponentSRPumparpFK_r_APISRPExtBblAcc";
			public static string arpFK_r_APISRPExtPlgAcc_GUID = "AssemblyComponentSRPumparpFK_r_APISRPExtPlgAcc_GUID";
			public static string arpFK_r_APISRPExtPlgAcc = "AssemblyComponentSRPumparpFK_r_APISRPExtPlgAcc";
			public static string arpFK_r_APISRPExtPlgType_GUID = "AssemblyComponentSRPumparpFK_r_APISRPExtPlgType_GUID";
			public static string arpFK_r_APISRPExtPlgType = "AssemblyComponentSRPumparpFK_r_APISRPExtPlgType";
			public static string arpFK_r_APISRPExtPlgPin_GUID = "AssemblyComponentSRPumparpFK_r_APISRPExtPlgPin_GUID";
			public static string arpFK_r_APISRPExtPlgPin = "AssemblyComponentSRPumparpFK_r_APISRPExtPlgPin";
			public static string arpFK_r_APISRPExtSV_GUID = "AssemblyComponentSRPumparpFK_r_APISRPExtSV_GUID";
			public static string arpFK_r_APISRPExtSV = "AssemblyComponentSRPumparpFK_r_APISRPExtSV";
			public static string arpFK_r_APISRPExtSVCage_GUID = "AssemblyComponentSRPumparpFK_r_APISRPExtSVCage_GUID";
			public static string arpFK_r_APISRPExtSVCage = "AssemblyComponentSRPumparpFK_r_APISRPExtSVCage";
			public static string arpFK_r_APISRPExtTV_GUID = "AssemblyComponentSRPumparpFK_r_APISRPExtTV_GUID";
			public static string arpFK_r_APISRPExtTV = "AssemblyComponentSRPumparpFK_r_APISRPExtTV";
			public static string arpFK_r_APISRPExtTVCage_GUID = "AssemblyComponentSRPumparpFK_r_APISRPExtTVCage_GUID";
			public static string arpFK_r_APISRPExtTVCage = "AssemblyComponentSRPumparpFK_r_APISRPExtTVCage";
			public static string arpFK_r_APISRPExtTVStPlg_GUID = "AssemblyComponentSRPumparpFK_r_APISRPExtTVStPlg_GUID";
			public static string arpFK_r_APISRPExtTVStPlg = "AssemblyComponentSRPumparpFK_r_APISRPExtTVStPlg";
			public static string arpFK_r_APISRPExtVRod_GUID = "AssemblyComponentSRPumparpFK_r_APISRPExtVRod_GUID";
			public static string arpFK_r_APISRPExtVRod = "AssemblyComponentSRPumparpFK_r_APISRPExtVRod";
			public static string arpFK_r_APISRPExtWiper_GUID = "AssemblyComponentSRPumparpFK_r_APISRPExtWiper_GUID";
			public static string arpFK_r_APISRPExtWiper = "AssemblyComponentSRPumparpFK_r_APISRPExtWiper";
			public static string arpBblPlgAvgClearance = "AssemblyComponentSRPumparpBblPlgAvgClearance";
			public static string arpMaxSL = "AssemblyComponentSRPumparpMaxSL";
		}

		public sealed partial class Param
		{
			public static string ID = "@arpPrimaryKey_GUID";
			public static string arpPrimaryKey = "@arpPrimaryKey";
			public static string LastModified = "@arpLstChgDT";
			public static string arpLstChgUser = "@arpLstChgUser";
			public static string arpFK_AssemblyComponent_GUID = "@arpFK_AssemblyComponent_GUID";
			public static string arpFK_AssemblyComponent = "@arpFK_AssemblyComponent";
			public static string arpAPIDescription = "@arpAPIDescription";
			public static string arpAPIExtraDescriptionText = "@arpAPIExtraDescriptionText";
			public static string arpFK_r_APIPumpGraphics_GUID = "@arpFK_r_APIPumpGraphics_GUID";
			public static string arpFK_r_APIPumpGraphics = "@arpFK_r_APIPumpGraphics";
			public static string arpFK_r_APISRPTubingSize_GUID = "@arpFK_r_APISRPTubingSize_GUID";
			public static string arpFK_r_APISRPTubingSize = "@arpFK_r_APISRPTubingSize";
			public static string arpFK_r_APISRPPumpBore_GUID = "@arpFK_r_APISRPPumpBore_GUID";
			public static string arpFK_r_APISRPPumpBore = "@arpFK_r_APISRPPumpBore";
			public static string arpFK_r_APISRPPumpType_GUID = "@arpFK_r_APISRPPumpType_GUID";
			public static string arpFK_r_APISRPPumpType = "@arpFK_r_APISRPPumpType";
			public static string arpFK_r_APISRPBarrelType_GUID = "@arpFK_r_APISRPBarrelType_GUID";
			public static string arpFK_r_APISRPBarrelType = "@arpFK_r_APISRPBarrelType";
			public static string arpFK_r_APISRPSeatAssyLocation_GUID = "@arpFK_r_APISRPSeatAssyLocation_GUID";
			public static string arpFK_r_APISRPSeatAssyLocation = "@arpFK_r_APISRPSeatAssyLocation";
			public static string arpFK_r_APISRPSeatAssyType_GUID = "@arpFK_r_APISRPSeatAssyType_GUID";
			public static string arpFK_r_APISRPSeatAssyType = "@arpFK_r_APISRPSeatAssyType";
			public static string arpAPIBarrelLength = "@arpAPIBarrelLength";
			public static string arpAPIPlungerLength = "@arpAPIPlungerLength";
			public static string arpAPIExtensionCouplingUpperLength = "@arpAPIExtensionCouplingUpperLength";
			public static string arpAPIExtensionCouplingLowerLength = "@arpAPIExtensionCouplingLowerLength";
			public static string arpFK_r_APISRPExtPumpType_GUID = "@arpFK_r_APISRPExtPumpType_GUID";
			public static string arpFK_r_APISRPExtPumpType = "@arpFK_r_APISRPExtPumpType";
			public static string arpFK_r_APISRPExtBarrelType_GUID = "@arpFK_r_APISRPExtBarrelType_GUID";
			public static string arpFK_r_APISRPExtBarrelType = "@arpFK_r_APISRPExtBarrelType";
			public static string arpFK_r_APISRPExtSeatAssyLocation_GUID = "@arpFK_r_APISRPExtSeatAssyLocation_GUID";
			public static string arpFK_r_APISRPExtSeatAssyLocation = "@arpFK_r_APISRPExtSeatAssyLocation";
			public static string arpFK_r_APISRPExtSeatAssyType_GUID = "@arpFK_r_APISRPExtSeatAssyType_GUID";
			public static string arpFK_r_APISRPExtSeatAssyType = "@arpFK_r_APISRPExtSeatAssyType";
			public static string arpFK_r_APISRPExtSand_GUID = "@arpFK_r_APISRPExtSand_GUID";
			public static string arpFK_r_APISRPExtSand = "@arpFK_r_APISRPExtSand";
			public static string arpFK_r_APISRPExtBblAcc_GUID = "@arpFK_r_APISRPExtBblAcc_GUID";
			public static string arpFK_r_APISRPExtBblAcc = "@arpFK_r_APISRPExtBblAcc";
			public static string arpFK_r_APISRPExtPlgAcc_GUID = "@arpFK_r_APISRPExtPlgAcc_GUID";
			public static string arpFK_r_APISRPExtPlgAcc = "@arpFK_r_APISRPExtPlgAcc";
			public static string arpFK_r_APISRPExtPlgType_GUID = "@arpFK_r_APISRPExtPlgType_GUID";
			public static string arpFK_r_APISRPExtPlgType = "@arpFK_r_APISRPExtPlgType";
			public static string arpFK_r_APISRPExtPlgPin_GUID = "@arpFK_r_APISRPExtPlgPin_GUID";
			public static string arpFK_r_APISRPExtPlgPin = "@arpFK_r_APISRPExtPlgPin";
			public static string arpFK_r_APISRPExtSV_GUID = "@arpFK_r_APISRPExtSV_GUID";
			public static string arpFK_r_APISRPExtSV = "@arpFK_r_APISRPExtSV";
			public static string arpFK_r_APISRPExtSVCage_GUID = "@arpFK_r_APISRPExtSVCage_GUID";
			public static string arpFK_r_APISRPExtSVCage = "@arpFK_r_APISRPExtSVCage";
			public static string arpFK_r_APISRPExtTV_GUID = "@arpFK_r_APISRPExtTV_GUID";
			public static string arpFK_r_APISRPExtTV = "@arpFK_r_APISRPExtTV";
			public static string arpFK_r_APISRPExtTVCage_GUID = "@arpFK_r_APISRPExtTVCage_GUID";
			public static string arpFK_r_APISRPExtTVCage = "@arpFK_r_APISRPExtTVCage";
			public static string arpFK_r_APISRPExtTVStPlg_GUID = "@arpFK_r_APISRPExtTVStPlg_GUID";
			public static string arpFK_r_APISRPExtTVStPlg = "@arpFK_r_APISRPExtTVStPlg";
			public static string arpFK_r_APISRPExtVRod_GUID = "@arpFK_r_APISRPExtVRod_GUID";
			public static string arpFK_r_APISRPExtVRod = "@arpFK_r_APISRPExtVRod";
			public static string arpFK_r_APISRPExtWiper_GUID = "@arpFK_r_APISRPExtWiper_GUID";
			public static string arpFK_r_APISRPExtWiper = "@arpFK_r_APISRPExtWiper";
			public static string arpBblPlgAvgClearance = "@arpBblPlgAvgClearance";
			public static string arpMaxSL = "@arpMaxSL";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the BusinessOrganization table in the PumpServicing Database.
    /// </summary>
    public sealed partial class BusinessOrganizationMap : TableMap
    {
		public const string TABLE_NAME = "BusinessOrganization";
		
		public static string ID = "venPrimaryKey_GUID";
		public static string venPrimaryKey = "venPrimaryKey";
		public static string venLanguageCd = "venLanguageCd";
		public static string LastModified = "venLstChgDT";
		public static string venLstChgUser = "venLstChgUser";
		public static string venRefUserDeleted = "venRefUserDeleted";
		public static string venRefCaseDefined = "venRefCaseDefined";
		public static string venFK_r_BusinessOrganizationType_GUID = "venFK_r_BusinessOrganizationType_GUID";
		public static string venFK_r_BusinessOrganizationType = "venFK_r_BusinessOrganizationType";
		public static string venBusinessOrganizationAbrev = "venBusinessOrganizationAbrev";
		public static string venBusinessOrganizationName = "venBusinessOrganizationName";
		public static string venPT_TaxableStatus = "venPT_TaxableStatus";
		public static string venPT_TaxRate = "venPT_TaxRate";
		public static string venPT_InvoiceComments = "venPT_InvoiceComments";
		public static string venPT_DiscountRateRepair = "venPT_DiscountRateRepair";
		public static string venPT_DiscountRateNew = "venPT_DiscountRateNew";
		public static string venPT_SalesRep = "venPT_SalesRep";
		public static string venAccountingID = "venAccountingID";
		public static string venInactive = "venInactive";
		
		public sealed partial class Prefixed
		{
			public static string ID = "BusinessOrganization.venPrimaryKey_GUID";
			public static string venPrimaryKey = "BusinessOrganization.venPrimaryKey";
			public static string venLanguageCd = "BusinessOrganization.venLanguageCd";
			public static string LastModified = "BusinessOrganization.venLstChgDT";
			public static string venLstChgUser = "BusinessOrganization.venLstChgUser";
			public static string venRefUserDeleted = "BusinessOrganization.venRefUserDeleted";
			public static string venRefCaseDefined = "BusinessOrganization.venRefCaseDefined";
			public static string venFK_r_BusinessOrganizationType_GUID = "BusinessOrganization.venFK_r_BusinessOrganizationType_GUID";
			public static string venFK_r_BusinessOrganizationType = "BusinessOrganization.venFK_r_BusinessOrganizationType";
			public static string venBusinessOrganizationAbrev = "BusinessOrganization.venBusinessOrganizationAbrev";
			public static string venBusinessOrganizationName = "BusinessOrganization.venBusinessOrganizationName";
			public static string venPT_TaxableStatus = "BusinessOrganization.venPT_TaxableStatus";
			public static string venPT_TaxRate = "BusinessOrganization.venPT_TaxRate";
			public static string venPT_InvoiceComments = "BusinessOrganization.venPT_InvoiceComments";
			public static string venPT_DiscountRateRepair = "BusinessOrganization.venPT_DiscountRateRepair";
			public static string venPT_DiscountRateNew = "BusinessOrganization.venPT_DiscountRateNew";
			public static string venPT_SalesRep = "BusinessOrganization.venPT_SalesRep";
			public static string venAccountingID = "BusinessOrganization.venAccountingID";
			public static string venInactive = "BusinessOrganization.venInactive";
		}

		public sealed partial class Aliased
		{
			public static string ID = "BusinessOrganizationvenPrimaryKey_GUID";
			public static string venPrimaryKey = "BusinessOrganizationvenPrimaryKey";
			public static string venLanguageCd = "BusinessOrganizationvenLanguageCd";
			public static string LastModified = "BusinessOrganizationvenLstChgDT";
			public static string venLstChgUser = "BusinessOrganizationvenLstChgUser";
			public static string venRefUserDeleted = "BusinessOrganizationvenRefUserDeleted";
			public static string venRefCaseDefined = "BusinessOrganizationvenRefCaseDefined";
			public static string venFK_r_BusinessOrganizationType_GUID = "BusinessOrganizationvenFK_r_BusinessOrganizationType_GUID";
			public static string venFK_r_BusinessOrganizationType = "BusinessOrganizationvenFK_r_BusinessOrganizationType";
			public static string venBusinessOrganizationAbrev = "BusinessOrganizationvenBusinessOrganizationAbrev";
			public static string venBusinessOrganizationName = "BusinessOrganizationvenBusinessOrganizationName";
			public static string venPT_TaxableStatus = "BusinessOrganizationvenPT_TaxableStatus";
			public static string venPT_TaxRate = "BusinessOrganizationvenPT_TaxRate";
			public static string venPT_InvoiceComments = "BusinessOrganizationvenPT_InvoiceComments";
			public static string venPT_DiscountRateRepair = "BusinessOrganizationvenPT_DiscountRateRepair";
			public static string venPT_DiscountRateNew = "BusinessOrganizationvenPT_DiscountRateNew";
			public static string venPT_SalesRep = "BusinessOrganizationvenPT_SalesRep";
			public static string venAccountingID = "BusinessOrganizationvenAccountingID";
			public static string venInactive = "BusinessOrganizationvenInactive";
		}

		public sealed partial class Param
		{
			public static string ID = "@venPrimaryKey_GUID";
			public static string venPrimaryKey = "@venPrimaryKey";
			public static string venLanguageCd = "@venLanguageCd";
			public static string LastModified = "@venLstChgDT";
			public static string venLstChgUser = "@venLstChgUser";
			public static string venRefUserDeleted = "@venRefUserDeleted";
			public static string venRefCaseDefined = "@venRefCaseDefined";
			public static string venFK_r_BusinessOrganizationType_GUID = "@venFK_r_BusinessOrganizationType_GUID";
			public static string venFK_r_BusinessOrganizationType = "@venFK_r_BusinessOrganizationType";
			public static string venBusinessOrganizationAbrev = "@venBusinessOrganizationAbrev";
			public static string venBusinessOrganizationName = "@venBusinessOrganizationName";
			public static string venPT_TaxableStatus = "@venPT_TaxableStatus";
			public static string venPT_TaxRate = "@venPT_TaxRate";
			public static string venPT_InvoiceComments = "@venPT_InvoiceComments";
			public static string venPT_DiscountRateRepair = "@venPT_DiscountRateRepair";
			public static string venPT_DiscountRateNew = "@venPT_DiscountRateNew";
			public static string venPT_SalesRep = "@venPT_SalesRep";
			public static string venAccountingID = "@venAccountingID";
			public static string venInactive = "@venInactive";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the Component table in the PumpServicing Database.
    /// </summary>
    public sealed partial class ComponentMap : TableMap
    {
		public const string TABLE_NAME = "Component";
		
		public static string ID = "cmcPrimaryKey_GUID";
		public static string cmcPrimaryKey = "cmcPrimaryKey";
		public static string LastModified = "cmcLstChgDT";
		public static string cmcLstChgUser = "cmcLstChgUser";
		public static string cmcRefCaseDefined = "cmcRefCaseDefined";
		public static string cmcFK_Assembly_GUID = "cmcFK_Assembly_GUID";
		public static string cmcFK_Assembly = "cmcFK_Assembly";
		public static string cmcFK_r_CatalogItem_GUID = "cmcFK_r_CatalogItem_GUID";
		public static string cmcFK_r_CatalogItem = "cmcFK_r_CatalogItem";
		public static string cmcFK_r_MfgCatalogItem_GUID = "cmcFK_r_MfgCatalogItem_GUID";
		public static string cmcFK_r_MfgCatalogItem = "cmcFK_r_MfgCatalogItem";
		public static string cmcSerialNo = "cmcSerialNo";
		public static string cmcFK_r_ComponentOrigin_GUID = "cmcFK_r_ComponentOrigin_GUID";
		public static string cmcFK_r_ComponentOrigin = "cmcFK_r_ComponentOrigin";
		public static string cmcPreviousRunDays = "cmcPreviousRunDays";
		public static string cmcFK_BusinessOrganization_GUID = "cmcFK_BusinessOrganization_GUID";
		public static string cmcFK_BusinessOrganization = "cmcFK_BusinessOrganization";
		public static string cmcOriginKey = "cmcOriginKey";
		public static string cmcDiscontinueUse = "cmcDiscontinueUse";
		
		public sealed partial class Prefixed
		{
			public static string ID = "Component.cmcPrimaryKey_GUID";
			public static string cmcPrimaryKey = "Component.cmcPrimaryKey";
			public static string LastModified = "Component.cmcLstChgDT";
			public static string cmcLstChgUser = "Component.cmcLstChgUser";
			public static string cmcRefCaseDefined = "Component.cmcRefCaseDefined";
			public static string cmcFK_Assembly_GUID = "Component.cmcFK_Assembly_GUID";
			public static string cmcFK_Assembly = "Component.cmcFK_Assembly";
			public static string cmcFK_r_CatalogItem_GUID = "Component.cmcFK_r_CatalogItem_GUID";
			public static string cmcFK_r_CatalogItem = "Component.cmcFK_r_CatalogItem";
			public static string cmcFK_r_MfgCatalogItem_GUID = "Component.cmcFK_r_MfgCatalogItem_GUID";
			public static string cmcFK_r_MfgCatalogItem = "Component.cmcFK_r_MfgCatalogItem";
			public static string cmcSerialNo = "Component.cmcSerialNo";
			public static string cmcFK_r_ComponentOrigin_GUID = "Component.cmcFK_r_ComponentOrigin_GUID";
			public static string cmcFK_r_ComponentOrigin = "Component.cmcFK_r_ComponentOrigin";
			public static string cmcPreviousRunDays = "Component.cmcPreviousRunDays";
			public static string cmcFK_BusinessOrganization_GUID = "Component.cmcFK_BusinessOrganization_GUID";
			public static string cmcFK_BusinessOrganization = "Component.cmcFK_BusinessOrganization";
			public static string cmcOriginKey = "Component.cmcOriginKey";
			public static string cmcDiscontinueUse = "Component.cmcDiscontinueUse";
		}

		public sealed partial class Aliased
		{
			public static string ID = "ComponentcmcPrimaryKey_GUID";
			public static string cmcPrimaryKey = "ComponentcmcPrimaryKey";
			public static string LastModified = "ComponentcmcLstChgDT";
			public static string cmcLstChgUser = "ComponentcmcLstChgUser";
			public static string cmcRefCaseDefined = "ComponentcmcRefCaseDefined";
			public static string cmcFK_Assembly_GUID = "ComponentcmcFK_Assembly_GUID";
			public static string cmcFK_Assembly = "ComponentcmcFK_Assembly";
			public static string cmcFK_r_CatalogItem_GUID = "ComponentcmcFK_r_CatalogItem_GUID";
			public static string cmcFK_r_CatalogItem = "ComponentcmcFK_r_CatalogItem";
			public static string cmcFK_r_MfgCatalogItem_GUID = "ComponentcmcFK_r_MfgCatalogItem_GUID";
			public static string cmcFK_r_MfgCatalogItem = "ComponentcmcFK_r_MfgCatalogItem";
			public static string cmcSerialNo = "ComponentcmcSerialNo";
			public static string cmcFK_r_ComponentOrigin_GUID = "ComponentcmcFK_r_ComponentOrigin_GUID";
			public static string cmcFK_r_ComponentOrigin = "ComponentcmcFK_r_ComponentOrigin";
			public static string cmcPreviousRunDays = "ComponentcmcPreviousRunDays";
			public static string cmcFK_BusinessOrganization_GUID = "ComponentcmcFK_BusinessOrganization_GUID";
			public static string cmcFK_BusinessOrganization = "ComponentcmcFK_BusinessOrganization";
			public static string cmcOriginKey = "ComponentcmcOriginKey";
			public static string cmcDiscontinueUse = "ComponentcmcDiscontinueUse";
		}

		public sealed partial class Param
		{
			public static string ID = "@cmcPrimaryKey_GUID";
			public static string cmcPrimaryKey = "@cmcPrimaryKey";
			public static string LastModified = "@cmcLstChgDT";
			public static string cmcLstChgUser = "@cmcLstChgUser";
			public static string cmcRefCaseDefined = "@cmcRefCaseDefined";
			public static string cmcFK_Assembly_GUID = "@cmcFK_Assembly_GUID";
			public static string cmcFK_Assembly = "@cmcFK_Assembly";
			public static string cmcFK_r_CatalogItem_GUID = "@cmcFK_r_CatalogItem_GUID";
			public static string cmcFK_r_CatalogItem = "@cmcFK_r_CatalogItem";
			public static string cmcFK_r_MfgCatalogItem_GUID = "@cmcFK_r_MfgCatalogItem_GUID";
			public static string cmcFK_r_MfgCatalogItem = "@cmcFK_r_MfgCatalogItem";
			public static string cmcSerialNo = "@cmcSerialNo";
			public static string cmcFK_r_ComponentOrigin_GUID = "@cmcFK_r_ComponentOrigin_GUID";
			public static string cmcFK_r_ComponentOrigin = "@cmcFK_r_ComponentOrigin";
			public static string cmcPreviousRunDays = "@cmcPreviousRunDays";
			public static string cmcFK_BusinessOrganization_GUID = "@cmcFK_BusinessOrganization_GUID";
			public static string cmcFK_BusinessOrganization = "@cmcFK_BusinessOrganization";
			public static string cmcOriginKey = "@cmcOriginKey";
			public static string cmcDiscontinueUse = "@cmcDiscontinueUse";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the ComponentSRPump table in the PumpServicing Database.
    /// </summary>
    public sealed partial class ComponentSRPumpMap : TableMap
    {
		public const string TABLE_NAME = "ComponentSRPump";
		
		public static string ID = "cspPrimaryKey_GUID";
		public static string cspPrimaryKey = "cspPrimaryKey";
		public static string LastModified = "cspLstChgDT";
		public static string cspLstChgUser = "cspLstChgUser";
		public static string cspFK_Component_GUID = "cspFK_Component_GUID";
		public static string cspFK_Component = "cspFK_Component";
		public static string cspFK_BusinessOrganization_GUID = "cspFK_BusinessOrganization_GUID";
		public static string cspFK_BusinessOrganization = "cspFK_BusinessOrganization";
		
		public sealed partial class Prefixed
		{
			public static string ID = "ComponentSRPump.cspPrimaryKey_GUID";
			public static string cspPrimaryKey = "ComponentSRPump.cspPrimaryKey";
			public static string LastModified = "ComponentSRPump.cspLstChgDT";
			public static string cspLstChgUser = "ComponentSRPump.cspLstChgUser";
			public static string cspFK_Component_GUID = "ComponentSRPump.cspFK_Component_GUID";
			public static string cspFK_Component = "ComponentSRPump.cspFK_Component";
			public static string cspFK_BusinessOrganization_GUID = "ComponentSRPump.cspFK_BusinessOrganization_GUID";
			public static string cspFK_BusinessOrganization = "ComponentSRPump.cspFK_BusinessOrganization";
		}

		public sealed partial class Aliased
		{
			public static string ID = "ComponentSRPumpcspPrimaryKey_GUID";
			public static string cspPrimaryKey = "ComponentSRPumpcspPrimaryKey";
			public static string LastModified = "ComponentSRPumpcspLstChgDT";
			public static string cspLstChgUser = "ComponentSRPumpcspLstChgUser";
			public static string cspFK_Component_GUID = "ComponentSRPumpcspFK_Component_GUID";
			public static string cspFK_Component = "ComponentSRPumpcspFK_Component";
			public static string cspFK_BusinessOrganization_GUID = "ComponentSRPumpcspFK_BusinessOrganization_GUID";
			public static string cspFK_BusinessOrganization = "ComponentSRPumpcspFK_BusinessOrganization";
		}

		public sealed partial class Param
		{
			public static string ID = "@cspPrimaryKey_GUID";
			public static string cspPrimaryKey = "@cspPrimaryKey";
			public static string LastModified = "@cspLstChgDT";
			public static string cspLstChgUser = "@cspLstChgUser";
			public static string cspFK_Component_GUID = "@cspFK_Component_GUID";
			public static string cspFK_Component = "@cspFK_Component";
			public static string cspFK_BusinessOrganization_GUID = "@cspFK_BusinessOrganization_GUID";
			public static string cspFK_BusinessOrganization = "@cspFK_BusinessOrganization";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the DatabaseConfiguration table in the PumpServicing Database.
    /// </summary>
    public sealed partial class DatabaseConfigurationMap : TableMap
    {
		public const string TABLE_NAME = "DatabaseConfiguration";
		
		public static string ID = "dbcPrimaryKey_GUID";
		public static string dbcPrimaryKey = "dbcPrimaryKey";
		public static string LastModified = "dbcLstChgDT";
		public static string dbcLstChgUser = "dbcLstChgUser";
		public static string dbcDescription = "dbcDescription";
		public static string dbcValue = "dbcValue";
		
		public sealed partial class Prefixed
		{
			public static string ID = "DatabaseConfiguration.dbcPrimaryKey_GUID";
			public static string dbcPrimaryKey = "DatabaseConfiguration.dbcPrimaryKey";
			public static string LastModified = "DatabaseConfiguration.dbcLstChgDT";
			public static string dbcLstChgUser = "DatabaseConfiguration.dbcLstChgUser";
			public static string dbcDescription = "DatabaseConfiguration.dbcDescription";
			public static string dbcValue = "DatabaseConfiguration.dbcValue";
		}

		public sealed partial class Aliased
		{
			public static string ID = "DatabaseConfigurationdbcPrimaryKey_GUID";
			public static string dbcPrimaryKey = "DatabaseConfigurationdbcPrimaryKey";
			public static string LastModified = "DatabaseConfigurationdbcLstChgDT";
			public static string dbcLstChgUser = "DatabaseConfigurationdbcLstChgUser";
			public static string dbcDescription = "DatabaseConfigurationdbcDescription";
			public static string dbcValue = "DatabaseConfigurationdbcValue";
		}

		public sealed partial class Param
		{
			public static string ID = "@dbcPrimaryKey_GUID";
			public static string dbcPrimaryKey = "@dbcPrimaryKey";
			public static string LastModified = "@dbcLstChgDT";
			public static string dbcLstChgUser = "@dbcLstChgUser";
			public static string dbcDescription = "@dbcDescription";
			public static string dbcValue = "@dbcValue";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the DeletedLog table in the PumpServicing Database.
    /// </summary>
    public sealed partial class DeletedLogMap : TableMap
    {
		public const string TABLE_NAME = "DeletedLog";
		
		public static string ID = "delPrimaryKey_GUID";
		public static string delPrimaryKey = "delPrimaryKey";
		public static string LastModified = "delLstChgDT";
		public static string delLstChgUser = "delLstChgUser";
		public static string delFK_csDBEntity = "delFK_csDBEntity";
		public static string delDeletedKey = "delDeletedKey";
		public static string delDataXML = "delDataXML";
		public static string delRemark = "delRemark";
		
		public sealed partial class Prefixed
		{
			public static string ID = "DeletedLog.delPrimaryKey_GUID";
			public static string delPrimaryKey = "DeletedLog.delPrimaryKey";
			public static string LastModified = "DeletedLog.delLstChgDT";
			public static string delLstChgUser = "DeletedLog.delLstChgUser";
			public static string delFK_csDBEntity = "DeletedLog.delFK_csDBEntity";
			public static string delDeletedKey = "DeletedLog.delDeletedKey";
			public static string delDataXML = "DeletedLog.delDataXML";
			public static string delRemark = "DeletedLog.delRemark";
		}

		public sealed partial class Aliased
		{
			public static string ID = "DeletedLogdelPrimaryKey_GUID";
			public static string delPrimaryKey = "DeletedLogdelPrimaryKey";
			public static string LastModified = "DeletedLogdelLstChgDT";
			public static string delLstChgUser = "DeletedLogdelLstChgUser";
			public static string delFK_csDBEntity = "DeletedLogdelFK_csDBEntity";
			public static string delDeletedKey = "DeletedLogdelDeletedKey";
			public static string delDataXML = "DeletedLogdelDataXML";
			public static string delRemark = "DeletedLogdelRemark";
		}

		public sealed partial class Param
		{
			public static string ID = "@delPrimaryKey_GUID";
			public static string delPrimaryKey = "@delPrimaryKey";
			public static string LastModified = "@delLstChgDT";
			public static string delLstChgUser = "@delLstChgUser";
			public static string delFK_csDBEntity = "@delFK_csDBEntity";
			public static string delDeletedKey = "@delDeletedKey";
			public static string delDataXML = "@delDataXML";
			public static string delRemark = "@delRemark";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the Document table in the PumpServicing Database.
    /// </summary>
    public sealed partial class DocumentMap : TableMap
    {
		public const string TABLE_NAME = "Document";
		
		public static string ID = "docPrimaryKey_GUID";
		public static string docPrimaryKey = "docPrimaryKey";
		public static string LastModified = "docLstChgDT";
		public static string docLstChgUser = "docLstChgUser";
		public static string docRefCaseDefined = "docRefCaseDefined";
		public static string docFK_r_DocumentType = "docFK_r_DocumentType";
		public static string docDocID = "docDocID";
		public static string docBriefDescription = "docBriefDescription";
		public static string docDocServerPath = "docDocServerPath";
		public static string docDocServerFileName = "docDocServerFileName";
		public static string docOriginalFileName = "docOriginalFileName";
		public static string docFolderName = "docFolderName";
		public static string docCurrentVersion = "docCurrentVersion";
		public static string docInUse = "docInUse";
		public static string docCheckedOut = "docCheckedOut";
		public static string docCheckoutUser = "docCheckoutUser";
		public static string docCheckoutDT = "docCheckoutDT";
		public static string docAPI10 = "docAPI10";
		public static string docAPI12 = "docAPI12";
		public static string docAPI14 = "docAPI14";
		public static string docProducer = "docProducer";
		public static string docField = "docField";
		public static string docFacility = "docFacility";
		public static string docFK_EventCategory = "docFK_EventCategory";
		public static string docFK_Event = "docFK_Event";
		public static string docFK_Component = "docFK_Component";
		public static string docUserDef01 = "docUserDef01";
		public static string docUserDef02 = "docUserDef02";
		public static string docUserDef03 = "docUserDef03";
		
		public sealed partial class Prefixed
		{
			public static string ID = "Document.docPrimaryKey_GUID";
			public static string docPrimaryKey = "Document.docPrimaryKey";
			public static string LastModified = "Document.docLstChgDT";
			public static string docLstChgUser = "Document.docLstChgUser";
			public static string docRefCaseDefined = "Document.docRefCaseDefined";
			public static string docFK_r_DocumentType = "Document.docFK_r_DocumentType";
			public static string docDocID = "Document.docDocID";
			public static string docBriefDescription = "Document.docBriefDescription";
			public static string docDocServerPath = "Document.docDocServerPath";
			public static string docDocServerFileName = "Document.docDocServerFileName";
			public static string docOriginalFileName = "Document.docOriginalFileName";
			public static string docFolderName = "Document.docFolderName";
			public static string docCurrentVersion = "Document.docCurrentVersion";
			public static string docInUse = "Document.docInUse";
			public static string docCheckedOut = "Document.docCheckedOut";
			public static string docCheckoutUser = "Document.docCheckoutUser";
			public static string docCheckoutDT = "Document.docCheckoutDT";
			public static string docAPI10 = "Document.docAPI10";
			public static string docAPI12 = "Document.docAPI12";
			public static string docAPI14 = "Document.docAPI14";
			public static string docProducer = "Document.docProducer";
			public static string docField = "Document.docField";
			public static string docFacility = "Document.docFacility";
			public static string docFK_EventCategory = "Document.docFK_EventCategory";
			public static string docFK_Event = "Document.docFK_Event";
			public static string docFK_Component = "Document.docFK_Component";
			public static string docUserDef01 = "Document.docUserDef01";
			public static string docUserDef02 = "Document.docUserDef02";
			public static string docUserDef03 = "Document.docUserDef03";
		}

		public sealed partial class Aliased
		{
			public static string ID = "DocumentdocPrimaryKey_GUID";
			public static string docPrimaryKey = "DocumentdocPrimaryKey";
			public static string LastModified = "DocumentdocLstChgDT";
			public static string docLstChgUser = "DocumentdocLstChgUser";
			public static string docRefCaseDefined = "DocumentdocRefCaseDefined";
			public static string docFK_r_DocumentType = "DocumentdocFK_r_DocumentType";
			public static string docDocID = "DocumentdocDocID";
			public static string docBriefDescription = "DocumentdocBriefDescription";
			public static string docDocServerPath = "DocumentdocDocServerPath";
			public static string docDocServerFileName = "DocumentdocDocServerFileName";
			public static string docOriginalFileName = "DocumentdocOriginalFileName";
			public static string docFolderName = "DocumentdocFolderName";
			public static string docCurrentVersion = "DocumentdocCurrentVersion";
			public static string docInUse = "DocumentdocInUse";
			public static string docCheckedOut = "DocumentdocCheckedOut";
			public static string docCheckoutUser = "DocumentdocCheckoutUser";
			public static string docCheckoutDT = "DocumentdocCheckoutDT";
			public static string docAPI10 = "DocumentdocAPI10";
			public static string docAPI12 = "DocumentdocAPI12";
			public static string docAPI14 = "DocumentdocAPI14";
			public static string docProducer = "DocumentdocProducer";
			public static string docField = "DocumentdocField";
			public static string docFacility = "DocumentdocFacility";
			public static string docFK_EventCategory = "DocumentdocFK_EventCategory";
			public static string docFK_Event = "DocumentdocFK_Event";
			public static string docFK_Component = "DocumentdocFK_Component";
			public static string docUserDef01 = "DocumentdocUserDef01";
			public static string docUserDef02 = "DocumentdocUserDef02";
			public static string docUserDef03 = "DocumentdocUserDef03";
		}

		public sealed partial class Param
		{
			public static string ID = "@docPrimaryKey_GUID";
			public static string docPrimaryKey = "@docPrimaryKey";
			public static string LastModified = "@docLstChgDT";
			public static string docLstChgUser = "@docLstChgUser";
			public static string docRefCaseDefined = "@docRefCaseDefined";
			public static string docFK_r_DocumentType = "@docFK_r_DocumentType";
			public static string docDocID = "@docDocID";
			public static string docBriefDescription = "@docBriefDescription";
			public static string docDocServerPath = "@docDocServerPath";
			public static string docDocServerFileName = "@docDocServerFileName";
			public static string docOriginalFileName = "@docOriginalFileName";
			public static string docFolderName = "@docFolderName";
			public static string docCurrentVersion = "@docCurrentVersion";
			public static string docInUse = "@docInUse";
			public static string docCheckedOut = "@docCheckedOut";
			public static string docCheckoutUser = "@docCheckoutUser";
			public static string docCheckoutDT = "@docCheckoutDT";
			public static string docAPI10 = "@docAPI10";
			public static string docAPI12 = "@docAPI12";
			public static string docAPI14 = "@docAPI14";
			public static string docProducer = "@docProducer";
			public static string docField = "@docField";
			public static string docFacility = "@docFacility";
			public static string docFK_EventCategory = "@docFK_EventCategory";
			public static string docFK_Event = "@docFK_Event";
			public static string docFK_Component = "@docFK_Component";
			public static string docUserDef01 = "@docUserDef01";
			public static string docUserDef02 = "@docUserDef02";
			public static string docUserDef03 = "@docUserDef03";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the Event table in the PumpServicing Database.
    /// </summary>
    public sealed partial class EventMap : TableMap
    {
		public const string TABLE_NAME = "Event";
		
		public static string ID = "evcPrimaryKey_GUID";
		public static string evcPrimaryKey = "evcPrimaryKey";
		public static string LastModified = "evcLstChgDT";
		public static string evcLstChgUser = "evcLstChgUser";
		public static string evcRefCaseDefined = "evcRefCaseDefined";
		public static string evcFK_Job_GUID = "evcFK_Job_GUID";
		public static string evcFK_EventCategory = "evcFK_EventCategory";
		public static string evcFK_Workorder_GUID = "evcFK_Workorder_GUID";
		public static string evcFK_Workorder = "evcFK_Workorder";
		public static string evcFK_r_EventType_GUID = "evcFK_r_EventType_GUID";
		public static string evcFK_r_EventType = "evcFK_r_EventType";
		public static string evcEventBegDtTm = "evcEventBegDtTm";
		public static string evcEventEndDtTm = "evcEventEndDtTm";
		public static string evcDurationHours = "evcDurationHours";
		public static string evcEventOrder = "evcEventOrder";
		public static string evcFK_Assembly_GUID = "evcFK_Assembly_GUID";
		public static string evcFK_Assembly = "evcFK_Assembly";
		public static string evcResponsiblePerson = "evcResponsiblePerson";
		public static string evcFK_BusinessOrganization_GUID = "evcFK_BusinessOrganization_GUID";
		public static string evcFK_BusinessOrganization = "evcFK_BusinessOrganization";
		public static string evcPersonPerformingTask = "evcPersonPerformingTask";
		public static string evcOriginKey = "evcOriginKey";
		public static string evcRemarks = "evcRemarks";
		
		public sealed partial class Prefixed
		{
			public static string ID = "Event.evcPrimaryKey_GUID";
			public static string evcPrimaryKey = "Event.evcPrimaryKey";
			public static string LastModified = "Event.evcLstChgDT";
			public static string evcLstChgUser = "Event.evcLstChgUser";
			public static string evcRefCaseDefined = "Event.evcRefCaseDefined";
			public static string evcFK_Job_GUID = "Event.evcFK_Job_GUID";
			public static string evcFK_EventCategory = "Event.evcFK_EventCategory";
			public static string evcFK_Workorder_GUID = "Event.evcFK_Workorder_GUID";
			public static string evcFK_Workorder = "Event.evcFK_Workorder";
			public static string evcFK_r_EventType_GUID = "Event.evcFK_r_EventType_GUID";
			public static string evcFK_r_EventType = "Event.evcFK_r_EventType";
			public static string evcEventBegDtTm = "Event.evcEventBegDtTm";
			public static string evcEventEndDtTm = "Event.evcEventEndDtTm";
			public static string evcDurationHours = "Event.evcDurationHours";
			public static string evcEventOrder = "Event.evcEventOrder";
			public static string evcFK_Assembly_GUID = "Event.evcFK_Assembly_GUID";
			public static string evcFK_Assembly = "Event.evcFK_Assembly";
			public static string evcResponsiblePerson = "Event.evcResponsiblePerson";
			public static string evcFK_BusinessOrganization_GUID = "Event.evcFK_BusinessOrganization_GUID";
			public static string evcFK_BusinessOrganization = "Event.evcFK_BusinessOrganization";
			public static string evcPersonPerformingTask = "Event.evcPersonPerformingTask";
			public static string evcOriginKey = "Event.evcOriginKey";
			public static string evcRemarks = "Event.evcRemarks";
		}

		public sealed partial class Aliased
		{
			public static string ID = "EventevcPrimaryKey_GUID";
			public static string evcPrimaryKey = "EventevcPrimaryKey";
			public static string LastModified = "EventevcLstChgDT";
			public static string evcLstChgUser = "EventevcLstChgUser";
			public static string evcRefCaseDefined = "EventevcRefCaseDefined";
			public static string evcFK_Job_GUID = "EventevcFK_Job_GUID";
			public static string evcFK_EventCategory = "EventevcFK_EventCategory";
			public static string evcFK_Workorder_GUID = "EventevcFK_Workorder_GUID";
			public static string evcFK_Workorder = "EventevcFK_Workorder";
			public static string evcFK_r_EventType_GUID = "EventevcFK_r_EventType_GUID";
			public static string evcFK_r_EventType = "EventevcFK_r_EventType";
			public static string evcEventBegDtTm = "EventevcEventBegDtTm";
			public static string evcEventEndDtTm = "EventevcEventEndDtTm";
			public static string evcDurationHours = "EventevcDurationHours";
			public static string evcEventOrder = "EventevcEventOrder";
			public static string evcFK_Assembly_GUID = "EventevcFK_Assembly_GUID";
			public static string evcFK_Assembly = "EventevcFK_Assembly";
			public static string evcResponsiblePerson = "EventevcResponsiblePerson";
			public static string evcFK_BusinessOrganization_GUID = "EventevcFK_BusinessOrganization_GUID";
			public static string evcFK_BusinessOrganization = "EventevcFK_BusinessOrganization";
			public static string evcPersonPerformingTask = "EventevcPersonPerformingTask";
			public static string evcOriginKey = "EventevcOriginKey";
			public static string evcRemarks = "EventevcRemarks";
		}

		public sealed partial class Param
		{
			public static string ID = "@evcPrimaryKey_GUID";
			public static string evcPrimaryKey = "@evcPrimaryKey";
			public static string LastModified = "@evcLstChgDT";
			public static string evcLstChgUser = "@evcLstChgUser";
			public static string evcRefCaseDefined = "@evcRefCaseDefined";
			public static string evcFK_Job_GUID = "@evcFK_Job_GUID";
			public static string evcFK_EventCategory = "@evcFK_EventCategory";
			public static string evcFK_Workorder_GUID = "@evcFK_Workorder_GUID";
			public static string evcFK_Workorder = "@evcFK_Workorder";
			public static string evcFK_r_EventType_GUID = "@evcFK_r_EventType_GUID";
			public static string evcFK_r_EventType = "@evcFK_r_EventType";
			public static string evcEventBegDtTm = "@evcEventBegDtTm";
			public static string evcEventEndDtTm = "@evcEventEndDtTm";
			public static string evcDurationHours = "@evcDurationHours";
			public static string evcEventOrder = "@evcEventOrder";
			public static string evcFK_Assembly_GUID = "@evcFK_Assembly_GUID";
			public static string evcFK_Assembly = "@evcFK_Assembly";
			public static string evcResponsiblePerson = "@evcResponsiblePerson";
			public static string evcFK_BusinessOrganization_GUID = "@evcFK_BusinessOrganization_GUID";
			public static string evcFK_BusinessOrganization = "@evcFK_BusinessOrganization";
			public static string evcPersonPerformingTask = "@evcPersonPerformingTask";
			public static string evcOriginKey = "@evcOriginKey";
			public static string evcRemarks = "@evcRemarks";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the EventAssembleSRPump table in the PumpServicing Database.
    /// </summary>
    public sealed partial class EventAssembleSRPumpMap : TableMap
    {
		public const string TABLE_NAME = "EventAssembleSRPump";
		
		public static string ID = "psrPrimaryKey_GUID";
		public static string psrPrimaryKey = "psrPrimaryKey";
		public static string LastModified = "psrLstChgDT";
		public static string psrLstChgUser = "psrLstChgUser";
		public static string psrFK_Event_GUID = "psrFK_Event_GUID";
		public static string psrFK_Event = "psrFK_Event";
		public static string psrDeliveryTicketNo = "psrDeliveryTicketNo";
		public static string psrVacuumPressureTest = "psrVacuumPressureTest";
		public static string psrTotalPumpCharges = "psrTotalPumpCharges";
		public static string psrFK_Assembly_WellChargeTo_GUID = "psrFK_Assembly_WellChargeTo_GUID";
		public static string psrFK_Assembly_WellChargeTo = "psrFK_Assembly_WellChargeTo";
		public static string psrFK_WellCompletionXRef_WellChargeTo_GUID = "psrFK_WellCompletionXRef_WellChargeTo_GUID";
		public static string psrFK_WellCompletionXRef_WellChargeTo = "psrFK_WellCompletionXRef_WellChargeTo";
		
		public sealed partial class Prefixed
		{
			public static string ID = "EventAssembleSRPump.psrPrimaryKey_GUID";
			public static string psrPrimaryKey = "EventAssembleSRPump.psrPrimaryKey";
			public static string LastModified = "EventAssembleSRPump.psrLstChgDT";
			public static string psrLstChgUser = "EventAssembleSRPump.psrLstChgUser";
			public static string psrFK_Event_GUID = "EventAssembleSRPump.psrFK_Event_GUID";
			public static string psrFK_Event = "EventAssembleSRPump.psrFK_Event";
			public static string psrDeliveryTicketNo = "EventAssembleSRPump.psrDeliveryTicketNo";
			public static string psrVacuumPressureTest = "EventAssembleSRPump.psrVacuumPressureTest";
			public static string psrTotalPumpCharges = "EventAssembleSRPump.psrTotalPumpCharges";
			public static string psrFK_Assembly_WellChargeTo_GUID = "EventAssembleSRPump.psrFK_Assembly_WellChargeTo_GUID";
			public static string psrFK_Assembly_WellChargeTo = "EventAssembleSRPump.psrFK_Assembly_WellChargeTo";
			public static string psrFK_WellCompletionXRef_WellChargeTo_GUID = "EventAssembleSRPump.psrFK_WellCompletionXRef_WellChargeTo_GUID";
			public static string psrFK_WellCompletionXRef_WellChargeTo = "EventAssembleSRPump.psrFK_WellCompletionXRef_WellChargeTo";
		}

		public sealed partial class Aliased
		{
			public static string ID = "EventAssembleSRPumppsrPrimaryKey_GUID";
			public static string psrPrimaryKey = "EventAssembleSRPumppsrPrimaryKey";
			public static string LastModified = "EventAssembleSRPumppsrLstChgDT";
			public static string psrLstChgUser = "EventAssembleSRPumppsrLstChgUser";
			public static string psrFK_Event_GUID = "EventAssembleSRPumppsrFK_Event_GUID";
			public static string psrFK_Event = "EventAssembleSRPumppsrFK_Event";
			public static string psrDeliveryTicketNo = "EventAssembleSRPumppsrDeliveryTicketNo";
			public static string psrVacuumPressureTest = "EventAssembleSRPumppsrVacuumPressureTest";
			public static string psrTotalPumpCharges = "EventAssembleSRPumppsrTotalPumpCharges";
			public static string psrFK_Assembly_WellChargeTo_GUID = "EventAssembleSRPumppsrFK_Assembly_WellChargeTo_GUID";
			public static string psrFK_Assembly_WellChargeTo = "EventAssembleSRPumppsrFK_Assembly_WellChargeTo";
			public static string psrFK_WellCompletionXRef_WellChargeTo_GUID = "EventAssembleSRPumppsrFK_WellCompletionXRef_WellChargeTo_GUID";
			public static string psrFK_WellCompletionXRef_WellChargeTo = "EventAssembleSRPumppsrFK_WellCompletionXRef_WellChargeTo";
		}

		public sealed partial class Param
		{
			public static string ID = "@psrPrimaryKey_GUID";
			public static string psrPrimaryKey = "@psrPrimaryKey";
			public static string LastModified = "@psrLstChgDT";
			public static string psrLstChgUser = "@psrLstChgUser";
			public static string psrFK_Event_GUID = "@psrFK_Event_GUID";
			public static string psrFK_Event = "@psrFK_Event";
			public static string psrDeliveryTicketNo = "@psrDeliveryTicketNo";
			public static string psrVacuumPressureTest = "@psrVacuumPressureTest";
			public static string psrTotalPumpCharges = "@psrTotalPumpCharges";
			public static string psrFK_Assembly_WellChargeTo_GUID = "@psrFK_Assembly_WellChargeTo_GUID";
			public static string psrFK_Assembly_WellChargeTo = "@psrFK_Assembly_WellChargeTo";
			public static string psrFK_WellCompletionXRef_WellChargeTo_GUID = "@psrFK_WellCompletionXRef_WellChargeTo_GUID";
			public static string psrFK_WellCompletionXRef_WellChargeTo = "@psrFK_WellCompletionXRef_WellChargeTo";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the EventComponentFailure table in the PumpServicing Database.
    /// </summary>
    public sealed partial class EventComponentFailureMap : TableMap
    {
		public const string TABLE_NAME = "EventComponentFailure";
		
		public static string ID = "acfPrimaryKey_GUID";
		public static string acfPrimaryKey = "acfPrimaryKey";
		public static string LastModified = "acfLstChgDT";
		public static string acfLstChgUser = "acfLstChgUser";
		public static string acfFK_Event_GUID = "acfFK_Event_GUID";
		public static string acfFK_Event = "acfFK_Event";
		public static string acfFK_Component_GUID = "acfFK_Component_GUID";
		public static string acfFK_Component = "acfFK_Component";
		public static string acfPrimaryFailureObservation = "acfPrimaryFailureObservation";
		public static string acfPrimaryCauseOfFailure = "acfPrimaryCauseOfFailure";
		public static string acfPreviousRunDays = "acfPreviousRunDays";
		public static string acfFK_r_ComponentCondition_Current_GUID = "acfFK_r_ComponentCondition_Current_GUID";
		public static string acfFK_r_ComponentCondition_Current = "acfFK_r_ComponentCondition_Current";
		public static string acfFK_r_FailureObservation_GUID = "acfFK_r_FailureObservation_GUID";
		public static string acfFK_r_FailureObservation = "acfFK_r_FailureObservation";
		public static string acfFK_r_FailureLocation_GUID = "acfFK_r_FailureLocation_GUID";
		public static string acfFK_r_FailureLocation = "acfFK_r_FailureLocation";
		public static string acfFK_r_FailureInternalExternal_GUID = "acfFK_r_FailureInternalExternal_GUID";
		public static string acfFK_r_FailureInternalExternal = "acfFK_r_FailureInternalExternal";
		public static string acfFailedDepth = "acfFailedDepth";
		public static string acfFK_r_CorrosionLocation_GUID = "acfFK_r_CorrosionLocation_GUID";
		public static string acfFK_r_CorrosionLocation = "acfFK_r_CorrosionLocation";
		public static string acfFK_r_CorrosionAmount_GUID = "acfFK_r_CorrosionAmount_GUID";
		public static string acfFK_r_CorrosionAmount = "acfFK_r_CorrosionAmount";
		public static string acfFK_r_CorrosionType_GUID = "acfFK_r_CorrosionType_GUID";
		public static string acfFK_r_CorrosionType = "acfFK_r_CorrosionType";
		public static string acfFK_r_ComponentDisposition_GUID = "acfFK_r_ComponentDisposition_GUID";
		public static string acfFK_r_ComponentDisposition = "acfFK_r_ComponentDisposition";
		public static string acfRemarks = "acfRemarks";
		
		public sealed partial class Prefixed
		{
			public static string ID = "EventComponentFailure.acfPrimaryKey_GUID";
			public static string acfPrimaryKey = "EventComponentFailure.acfPrimaryKey";
			public static string LastModified = "EventComponentFailure.acfLstChgDT";
			public static string acfLstChgUser = "EventComponentFailure.acfLstChgUser";
			public static string acfFK_Event_GUID = "EventComponentFailure.acfFK_Event_GUID";
			public static string acfFK_Event = "EventComponentFailure.acfFK_Event";
			public static string acfFK_Component_GUID = "EventComponentFailure.acfFK_Component_GUID";
			public static string acfFK_Component = "EventComponentFailure.acfFK_Component";
			public static string acfPrimaryFailureObservation = "EventComponentFailure.acfPrimaryFailureObservation";
			public static string acfPrimaryCauseOfFailure = "EventComponentFailure.acfPrimaryCauseOfFailure";
			public static string acfPreviousRunDays = "EventComponentFailure.acfPreviousRunDays";
			public static string acfFK_r_ComponentCondition_Current_GUID = "EventComponentFailure.acfFK_r_ComponentCondition_Current_GUID";
			public static string acfFK_r_ComponentCondition_Current = "EventComponentFailure.acfFK_r_ComponentCondition_Current";
			public static string acfFK_r_FailureObservation_GUID = "EventComponentFailure.acfFK_r_FailureObservation_GUID";
			public static string acfFK_r_FailureObservation = "EventComponentFailure.acfFK_r_FailureObservation";
			public static string acfFK_r_FailureLocation_GUID = "EventComponentFailure.acfFK_r_FailureLocation_GUID";
			public static string acfFK_r_FailureLocation = "EventComponentFailure.acfFK_r_FailureLocation";
			public static string acfFK_r_FailureInternalExternal_GUID = "EventComponentFailure.acfFK_r_FailureInternalExternal_GUID";
			public static string acfFK_r_FailureInternalExternal = "EventComponentFailure.acfFK_r_FailureInternalExternal";
			public static string acfFailedDepth = "EventComponentFailure.acfFailedDepth";
			public static string acfFK_r_CorrosionLocation_GUID = "EventComponentFailure.acfFK_r_CorrosionLocation_GUID";
			public static string acfFK_r_CorrosionLocation = "EventComponentFailure.acfFK_r_CorrosionLocation";
			public static string acfFK_r_CorrosionAmount_GUID = "EventComponentFailure.acfFK_r_CorrosionAmount_GUID";
			public static string acfFK_r_CorrosionAmount = "EventComponentFailure.acfFK_r_CorrosionAmount";
			public static string acfFK_r_CorrosionType_GUID = "EventComponentFailure.acfFK_r_CorrosionType_GUID";
			public static string acfFK_r_CorrosionType = "EventComponentFailure.acfFK_r_CorrosionType";
			public static string acfFK_r_ComponentDisposition_GUID = "EventComponentFailure.acfFK_r_ComponentDisposition_GUID";
			public static string acfFK_r_ComponentDisposition = "EventComponentFailure.acfFK_r_ComponentDisposition";
			public static string acfRemarks = "EventComponentFailure.acfRemarks";
		}

		public sealed partial class Aliased
		{
			public static string ID = "EventComponentFailureacfPrimaryKey_GUID";
			public static string acfPrimaryKey = "EventComponentFailureacfPrimaryKey";
			public static string LastModified = "EventComponentFailureacfLstChgDT";
			public static string acfLstChgUser = "EventComponentFailureacfLstChgUser";
			public static string acfFK_Event_GUID = "EventComponentFailureacfFK_Event_GUID";
			public static string acfFK_Event = "EventComponentFailureacfFK_Event";
			public static string acfFK_Component_GUID = "EventComponentFailureacfFK_Component_GUID";
			public static string acfFK_Component = "EventComponentFailureacfFK_Component";
			public static string acfPrimaryFailureObservation = "EventComponentFailureacfPrimaryFailureObservation";
			public static string acfPrimaryCauseOfFailure = "EventComponentFailureacfPrimaryCauseOfFailure";
			public static string acfPreviousRunDays = "EventComponentFailureacfPreviousRunDays";
			public static string acfFK_r_ComponentCondition_Current_GUID = "EventComponentFailureacfFK_r_ComponentCondition_Current_GUID";
			public static string acfFK_r_ComponentCondition_Current = "EventComponentFailureacfFK_r_ComponentCondition_Current";
			public static string acfFK_r_FailureObservation_GUID = "EventComponentFailureacfFK_r_FailureObservation_GUID";
			public static string acfFK_r_FailureObservation = "EventComponentFailureacfFK_r_FailureObservation";
			public static string acfFK_r_FailureLocation_GUID = "EventComponentFailureacfFK_r_FailureLocation_GUID";
			public static string acfFK_r_FailureLocation = "EventComponentFailureacfFK_r_FailureLocation";
			public static string acfFK_r_FailureInternalExternal_GUID = "EventComponentFailureacfFK_r_FailureInternalExternal_GUID";
			public static string acfFK_r_FailureInternalExternal = "EventComponentFailureacfFK_r_FailureInternalExternal";
			public static string acfFailedDepth = "EventComponentFailureacfFailedDepth";
			public static string acfFK_r_CorrosionLocation_GUID = "EventComponentFailureacfFK_r_CorrosionLocation_GUID";
			public static string acfFK_r_CorrosionLocation = "EventComponentFailureacfFK_r_CorrosionLocation";
			public static string acfFK_r_CorrosionAmount_GUID = "EventComponentFailureacfFK_r_CorrosionAmount_GUID";
			public static string acfFK_r_CorrosionAmount = "EventComponentFailureacfFK_r_CorrosionAmount";
			public static string acfFK_r_CorrosionType_GUID = "EventComponentFailureacfFK_r_CorrosionType_GUID";
			public static string acfFK_r_CorrosionType = "EventComponentFailureacfFK_r_CorrosionType";
			public static string acfFK_r_ComponentDisposition_GUID = "EventComponentFailureacfFK_r_ComponentDisposition_GUID";
			public static string acfFK_r_ComponentDisposition = "EventComponentFailureacfFK_r_ComponentDisposition";
			public static string acfRemarks = "EventComponentFailureacfRemarks";
		}

		public sealed partial class Param
		{
			public static string ID = "@acfPrimaryKey_GUID";
			public static string acfPrimaryKey = "@acfPrimaryKey";
			public static string LastModified = "@acfLstChgDT";
			public static string acfLstChgUser = "@acfLstChgUser";
			public static string acfFK_Event_GUID = "@acfFK_Event_GUID";
			public static string acfFK_Event = "@acfFK_Event";
			public static string acfFK_Component_GUID = "@acfFK_Component_GUID";
			public static string acfFK_Component = "@acfFK_Component";
			public static string acfPrimaryFailureObservation = "@acfPrimaryFailureObservation";
			public static string acfPrimaryCauseOfFailure = "@acfPrimaryCauseOfFailure";
			public static string acfPreviousRunDays = "@acfPreviousRunDays";
			public static string acfFK_r_ComponentCondition_Current_GUID = "@acfFK_r_ComponentCondition_Current_GUID";
			public static string acfFK_r_ComponentCondition_Current = "@acfFK_r_ComponentCondition_Current";
			public static string acfFK_r_FailureObservation_GUID = "@acfFK_r_FailureObservation_GUID";
			public static string acfFK_r_FailureObservation = "@acfFK_r_FailureObservation";
			public static string acfFK_r_FailureLocation_GUID = "@acfFK_r_FailureLocation_GUID";
			public static string acfFK_r_FailureLocation = "@acfFK_r_FailureLocation";
			public static string acfFK_r_FailureInternalExternal_GUID = "@acfFK_r_FailureInternalExternal_GUID";
			public static string acfFK_r_FailureInternalExternal = "@acfFK_r_FailureInternalExternal";
			public static string acfFailedDepth = "@acfFailedDepth";
			public static string acfFK_r_CorrosionLocation_GUID = "@acfFK_r_CorrosionLocation_GUID";
			public static string acfFK_r_CorrosionLocation = "@acfFK_r_CorrosionLocation";
			public static string acfFK_r_CorrosionAmount_GUID = "@acfFK_r_CorrosionAmount_GUID";
			public static string acfFK_r_CorrosionAmount = "@acfFK_r_CorrosionAmount";
			public static string acfFK_r_CorrosionType_GUID = "@acfFK_r_CorrosionType_GUID";
			public static string acfFK_r_CorrosionType = "@acfFK_r_CorrosionType";
			public static string acfFK_r_ComponentDisposition_GUID = "@acfFK_r_ComponentDisposition_GUID";
			public static string acfFK_r_ComponentDisposition = "@acfFK_r_ComponentDisposition";
			public static string acfRemarks = "@acfRemarks";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the EventDetailCosts table in the PumpServicing Database.
    /// </summary>
    public sealed partial class EventDetailCostsMap : TableMap
    {
		public const string TABLE_NAME = "EventDetailCosts";
		
		public static string ID = "ecsPrimaryKey_GUID";
		public static string ecsPrimaryKey = "ecsPrimaryKey";
		public static string LastModified = "ecsLstChgDT";
		public static string ecsLstChgUser = "ecsLstChgUser";
		public static string ecsFK_Event_GUID = "ecsFK_Event_GUID";
		public static string ecsFK_Event = "ecsFK_Event";
		public static string ecsFK_BusinessOrganization_GUID = "ecsFK_BusinessOrganization_GUID";
		public static string ecsFK_BusinessOrganization = "ecsFK_BusinessOrganization";
		public static string ecsFK_Component_GUID = "ecsFK_Component_GUID";
		public static string ecsFK_Component = "ecsFK_Component";
		public static string ecsFK_r_CatalogItem_GUID = "ecsFK_r_CatalogItem_GUID";
		public static string ecsFK_r_CatalogItem = "ecsFK_r_CatalogItem";
		public static string ecsQuantity = "ecsQuantity";
		public static string ecsUnitPrice = "ecsUnitPrice";
		public static string ecsDiscountAmount = "ecsDiscountAmount";
		public static string ecsExtendedPrice = "ecsExtendedPrice";
		public static string ecsFK_r_UOMUnit_GUID = "ecsFK_r_UOMUnit_GUID";
		public static string ecsFK_r_UOMUnit = "ecsFK_r_UOMUnit";
		public static string ecsTaxableItem = "ecsTaxableItem";
		public static string ecsFK_Invoice = "ecsFK_Invoice";
		public static string ecsFK_Invoice_GUID = "ecsFK_Invoice_GUID";
		public static string ecsRemarks = "ecsRemarks";
		
		public sealed partial class Prefixed
		{
			public static string ID = "EventDetailCosts.ecsPrimaryKey_GUID";
			public static string ecsPrimaryKey = "EventDetailCosts.ecsPrimaryKey";
			public static string LastModified = "EventDetailCosts.ecsLstChgDT";
			public static string ecsLstChgUser = "EventDetailCosts.ecsLstChgUser";
			public static string ecsFK_Event_GUID = "EventDetailCosts.ecsFK_Event_GUID";
			public static string ecsFK_Event = "EventDetailCosts.ecsFK_Event";
			public static string ecsFK_BusinessOrganization_GUID = "EventDetailCosts.ecsFK_BusinessOrganization_GUID";
			public static string ecsFK_BusinessOrganization = "EventDetailCosts.ecsFK_BusinessOrganization";
			public static string ecsFK_Component_GUID = "EventDetailCosts.ecsFK_Component_GUID";
			public static string ecsFK_Component = "EventDetailCosts.ecsFK_Component";
			public static string ecsFK_r_CatalogItem_GUID = "EventDetailCosts.ecsFK_r_CatalogItem_GUID";
			public static string ecsFK_r_CatalogItem = "EventDetailCosts.ecsFK_r_CatalogItem";
			public static string ecsQuantity = "EventDetailCosts.ecsQuantity";
			public static string ecsUnitPrice = "EventDetailCosts.ecsUnitPrice";
			public static string ecsDiscountAmount = "EventDetailCosts.ecsDiscountAmount";
			public static string ecsExtendedPrice = "EventDetailCosts.ecsExtendedPrice";
			public static string ecsFK_r_UOMUnit_GUID = "EventDetailCosts.ecsFK_r_UOMUnit_GUID";
			public static string ecsFK_r_UOMUnit = "EventDetailCosts.ecsFK_r_UOMUnit";
			public static string ecsTaxableItem = "EventDetailCosts.ecsTaxableItem";
			public static string ecsFK_Invoice = "EventDetailCosts.ecsFK_Invoice";
			public static string ecsFK_Invoice_GUID = "EventDetailCosts.ecsFK_Invoice_GUID";
			public static string ecsRemarks = "EventDetailCosts.ecsRemarks";
		}

		public sealed partial class Aliased
		{
			public static string ID = "EventDetailCostsecsPrimaryKey_GUID";
			public static string ecsPrimaryKey = "EventDetailCostsecsPrimaryKey";
			public static string LastModified = "EventDetailCostsecsLstChgDT";
			public static string ecsLstChgUser = "EventDetailCostsecsLstChgUser";
			public static string ecsFK_Event_GUID = "EventDetailCostsecsFK_Event_GUID";
			public static string ecsFK_Event = "EventDetailCostsecsFK_Event";
			public static string ecsFK_BusinessOrganization_GUID = "EventDetailCostsecsFK_BusinessOrganization_GUID";
			public static string ecsFK_BusinessOrganization = "EventDetailCostsecsFK_BusinessOrganization";
			public static string ecsFK_Component_GUID = "EventDetailCostsecsFK_Component_GUID";
			public static string ecsFK_Component = "EventDetailCostsecsFK_Component";
			public static string ecsFK_r_CatalogItem_GUID = "EventDetailCostsecsFK_r_CatalogItem_GUID";
			public static string ecsFK_r_CatalogItem = "EventDetailCostsecsFK_r_CatalogItem";
			public static string ecsQuantity = "EventDetailCostsecsQuantity";
			public static string ecsUnitPrice = "EventDetailCostsecsUnitPrice";
			public static string ecsDiscountAmount = "EventDetailCostsecsDiscountAmount";
			public static string ecsExtendedPrice = "EventDetailCostsecsExtendedPrice";
			public static string ecsFK_r_UOMUnit_GUID = "EventDetailCostsecsFK_r_UOMUnit_GUID";
			public static string ecsFK_r_UOMUnit = "EventDetailCostsecsFK_r_UOMUnit";
			public static string ecsTaxableItem = "EventDetailCostsecsTaxableItem";
			public static string ecsFK_Invoice = "EventDetailCostsecsFK_Invoice";
			public static string ecsFK_Invoice_GUID = "EventDetailCostsecsFK_Invoice_GUID";
			public static string ecsRemarks = "EventDetailCostsecsRemarks";
		}

		public sealed partial class Param
		{
			public static string ID = "@ecsPrimaryKey_GUID";
			public static string ecsPrimaryKey = "@ecsPrimaryKey";
			public static string LastModified = "@ecsLstChgDT";
			public static string ecsLstChgUser = "@ecsLstChgUser";
			public static string ecsFK_Event_GUID = "@ecsFK_Event_GUID";
			public static string ecsFK_Event = "@ecsFK_Event";
			public static string ecsFK_BusinessOrganization_GUID = "@ecsFK_BusinessOrganization_GUID";
			public static string ecsFK_BusinessOrganization = "@ecsFK_BusinessOrganization";
			public static string ecsFK_Component_GUID = "@ecsFK_Component_GUID";
			public static string ecsFK_Component = "@ecsFK_Component";
			public static string ecsFK_r_CatalogItem_GUID = "@ecsFK_r_CatalogItem_GUID";
			public static string ecsFK_r_CatalogItem = "@ecsFK_r_CatalogItem";
			public static string ecsQuantity = "@ecsQuantity";
			public static string ecsUnitPrice = "@ecsUnitPrice";
			public static string ecsDiscountAmount = "@ecsDiscountAmount";
			public static string ecsExtendedPrice = "@ecsExtendedPrice";
			public static string ecsFK_r_UOMUnit_GUID = "@ecsFK_r_UOMUnit_GUID";
			public static string ecsFK_r_UOMUnit = "@ecsFK_r_UOMUnit";
			public static string ecsTaxableItem = "@ecsTaxableItem";
			public static string ecsFK_Invoice = "@ecsFK_Invoice";
			public static string ecsFK_Invoice_GUID = "@ecsFK_Invoice_GUID";
			public static string ecsRemarks = "@ecsRemarks";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the EventDisassembleSRPump table in the PumpServicing Database.
    /// </summary>
    public sealed partial class EventDisassembleSRPumpMap : TableMap
    {
		public const string TABLE_NAME = "EventDisassembleSRPump";
		
		public static string ID = "ptdPrimaryKey_GUID";
		public static string ptdPrimaryKey = "ptdPrimaryKey";
		public static string LastModified = "ptdLstChgDT";
		public static string ptdLstChgUser = "ptdLstChgUser";
		public static string ptdFK_Event_GUID = "ptdFK_Event_GUID";
		public static string ptdFK_Event = "ptdFK_Event";
		public static string ptdVacuumPressureTest = "ptdVacuumPressureTest";
		public static string ptdJunkPump = "ptdJunkPump";
		public static string ptdRecommendation = "ptdRecommendation";
		public static string ptdForMatlAsphaltene = "ptdForMatlAsphaltene";
		public static string ptdForMatlSand = "ptdForMatlSand";
		public static string ptdForMatlRubber = "ptdForMatlRubber";
		public static string ptdForMatlSteelFilings = "ptdForMatlSteelFilings";
		public static string ptdForMatlShale = "ptdForMatlShale";
		public static string ptdForMatlParaffin = "ptdForMatlParaffin";
		public static string ptdForMatlIronSulfide = "ptdForMatlIronSulfide";
		public static string ptdForMatlMud = "ptdForMatlMud";
		public static string ptdForMatlScale = "ptdForMatlScale";
		public static string ptdForMatlChalk = "ptdForMatlChalk";
		public static string ptdForMatlCoal = "ptdForMatlCoal";
		public static string ptdForMatlOther = "ptdForMatlOther";
		public static string ptdFK_r_CorrosionAmount_GUID = "ptdFK_r_CorrosionAmount_GUID";
		public static string ptdFK_r_CorrosionAmount = "ptdFK_r_CorrosionAmount";
		public static string ptdFK_r_CorrosionLocation_GUID = "ptdFK_r_CorrosionLocation_GUID";
		public static string ptdFK_r_CorrosionLocation = "ptdFK_r_CorrosionLocation";
		public static string ptdFK_r_SRPumpFailureReason_GUID = "ptdFK_r_SRPumpFailureReason_GUID";
		public static string ptdFK_r_SRPumpFailureReason = "ptdFK_r_SRPumpFailureReason";
		public static string ptdApparentDownholeStroke = "ptdApparentDownholeStroke";
		
		public sealed partial class Prefixed
		{
			public static string ID = "EventDisassembleSRPump.ptdPrimaryKey_GUID";
			public static string ptdPrimaryKey = "EventDisassembleSRPump.ptdPrimaryKey";
			public static string LastModified = "EventDisassembleSRPump.ptdLstChgDT";
			public static string ptdLstChgUser = "EventDisassembleSRPump.ptdLstChgUser";
			public static string ptdFK_Event_GUID = "EventDisassembleSRPump.ptdFK_Event_GUID";
			public static string ptdFK_Event = "EventDisassembleSRPump.ptdFK_Event";
			public static string ptdVacuumPressureTest = "EventDisassembleSRPump.ptdVacuumPressureTest";
			public static string ptdJunkPump = "EventDisassembleSRPump.ptdJunkPump";
			public static string ptdRecommendation = "EventDisassembleSRPump.ptdRecommendation";
			public static string ptdForMatlAsphaltene = "EventDisassembleSRPump.ptdForMatlAsphaltene";
			public static string ptdForMatlSand = "EventDisassembleSRPump.ptdForMatlSand";
			public static string ptdForMatlRubber = "EventDisassembleSRPump.ptdForMatlRubber";
			public static string ptdForMatlSteelFilings = "EventDisassembleSRPump.ptdForMatlSteelFilings";
			public static string ptdForMatlShale = "EventDisassembleSRPump.ptdForMatlShale";
			public static string ptdForMatlParaffin = "EventDisassembleSRPump.ptdForMatlParaffin";
			public static string ptdForMatlIronSulfide = "EventDisassembleSRPump.ptdForMatlIronSulfide";
			public static string ptdForMatlMud = "EventDisassembleSRPump.ptdForMatlMud";
			public static string ptdForMatlScale = "EventDisassembleSRPump.ptdForMatlScale";
			public static string ptdForMatlChalk = "EventDisassembleSRPump.ptdForMatlChalk";
			public static string ptdForMatlCoal = "EventDisassembleSRPump.ptdForMatlCoal";
			public static string ptdForMatlOther = "EventDisassembleSRPump.ptdForMatlOther";
			public static string ptdFK_r_CorrosionAmount_GUID = "EventDisassembleSRPump.ptdFK_r_CorrosionAmount_GUID";
			public static string ptdFK_r_CorrosionAmount = "EventDisassembleSRPump.ptdFK_r_CorrosionAmount";
			public static string ptdFK_r_CorrosionLocation_GUID = "EventDisassembleSRPump.ptdFK_r_CorrosionLocation_GUID";
			public static string ptdFK_r_CorrosionLocation = "EventDisassembleSRPump.ptdFK_r_CorrosionLocation";
			public static string ptdFK_r_SRPumpFailureReason_GUID = "EventDisassembleSRPump.ptdFK_r_SRPumpFailureReason_GUID";
			public static string ptdFK_r_SRPumpFailureReason = "EventDisassembleSRPump.ptdFK_r_SRPumpFailureReason";
			public static string ptdApparentDownholeStroke = "EventDisassembleSRPump.ptdApparentDownholeStroke";
		}

		public sealed partial class Aliased
		{
			public static string ID = "EventDisassembleSRPumpptdPrimaryKey_GUID";
			public static string ptdPrimaryKey = "EventDisassembleSRPumpptdPrimaryKey";
			public static string LastModified = "EventDisassembleSRPumpptdLstChgDT";
			public static string ptdLstChgUser = "EventDisassembleSRPumpptdLstChgUser";
			public static string ptdFK_Event_GUID = "EventDisassembleSRPumpptdFK_Event_GUID";
			public static string ptdFK_Event = "EventDisassembleSRPumpptdFK_Event";
			public static string ptdVacuumPressureTest = "EventDisassembleSRPumpptdVacuumPressureTest";
			public static string ptdJunkPump = "EventDisassembleSRPumpptdJunkPump";
			public static string ptdRecommendation = "EventDisassembleSRPumpptdRecommendation";
			public static string ptdForMatlAsphaltene = "EventDisassembleSRPumpptdForMatlAsphaltene";
			public static string ptdForMatlSand = "EventDisassembleSRPumpptdForMatlSand";
			public static string ptdForMatlRubber = "EventDisassembleSRPumpptdForMatlRubber";
			public static string ptdForMatlSteelFilings = "EventDisassembleSRPumpptdForMatlSteelFilings";
			public static string ptdForMatlShale = "EventDisassembleSRPumpptdForMatlShale";
			public static string ptdForMatlParaffin = "EventDisassembleSRPumpptdForMatlParaffin";
			public static string ptdForMatlIronSulfide = "EventDisassembleSRPumpptdForMatlIronSulfide";
			public static string ptdForMatlMud = "EventDisassembleSRPumpptdForMatlMud";
			public static string ptdForMatlScale = "EventDisassembleSRPumpptdForMatlScale";
			public static string ptdForMatlChalk = "EventDisassembleSRPumpptdForMatlChalk";
			public static string ptdForMatlCoal = "EventDisassembleSRPumpptdForMatlCoal";
			public static string ptdForMatlOther = "EventDisassembleSRPumpptdForMatlOther";
			public static string ptdFK_r_CorrosionAmount_GUID = "EventDisassembleSRPumpptdFK_r_CorrosionAmount_GUID";
			public static string ptdFK_r_CorrosionAmount = "EventDisassembleSRPumpptdFK_r_CorrosionAmount";
			public static string ptdFK_r_CorrosionLocation_GUID = "EventDisassembleSRPumpptdFK_r_CorrosionLocation_GUID";
			public static string ptdFK_r_CorrosionLocation = "EventDisassembleSRPumpptdFK_r_CorrosionLocation";
			public static string ptdFK_r_SRPumpFailureReason_GUID = "EventDisassembleSRPumpptdFK_r_SRPumpFailureReason_GUID";
			public static string ptdFK_r_SRPumpFailureReason = "EventDisassembleSRPumpptdFK_r_SRPumpFailureReason";
			public static string ptdApparentDownholeStroke = "EventDisassembleSRPumpptdApparentDownholeStroke";
		}

		public sealed partial class Param
		{
			public static string ID = "@ptdPrimaryKey_GUID";
			public static string ptdPrimaryKey = "@ptdPrimaryKey";
			public static string LastModified = "@ptdLstChgDT";
			public static string ptdLstChgUser = "@ptdLstChgUser";
			public static string ptdFK_Event_GUID = "@ptdFK_Event_GUID";
			public static string ptdFK_Event = "@ptdFK_Event";
			public static string ptdVacuumPressureTest = "@ptdVacuumPressureTest";
			public static string ptdJunkPump = "@ptdJunkPump";
			public static string ptdRecommendation = "@ptdRecommendation";
			public static string ptdForMatlAsphaltene = "@ptdForMatlAsphaltene";
			public static string ptdForMatlSand = "@ptdForMatlSand";
			public static string ptdForMatlRubber = "@ptdForMatlRubber";
			public static string ptdForMatlSteelFilings = "@ptdForMatlSteelFilings";
			public static string ptdForMatlShale = "@ptdForMatlShale";
			public static string ptdForMatlParaffin = "@ptdForMatlParaffin";
			public static string ptdForMatlIronSulfide = "@ptdForMatlIronSulfide";
			public static string ptdForMatlMud = "@ptdForMatlMud";
			public static string ptdForMatlScale = "@ptdForMatlScale";
			public static string ptdForMatlChalk = "@ptdForMatlChalk";
			public static string ptdForMatlCoal = "@ptdForMatlCoal";
			public static string ptdForMatlOther = "@ptdForMatlOther";
			public static string ptdFK_r_CorrosionAmount_GUID = "@ptdFK_r_CorrosionAmount_GUID";
			public static string ptdFK_r_CorrosionAmount = "@ptdFK_r_CorrosionAmount";
			public static string ptdFK_r_CorrosionLocation_GUID = "@ptdFK_r_CorrosionLocation_GUID";
			public static string ptdFK_r_CorrosionLocation = "@ptdFK_r_CorrosionLocation";
			public static string ptdFK_r_SRPumpFailureReason_GUID = "@ptdFK_r_SRPumpFailureReason_GUID";
			public static string ptdFK_r_SRPumpFailureReason = "@ptdFK_r_SRPumpFailureReason";
			public static string ptdApparentDownholeStroke = "@ptdApparentDownholeStroke";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the EventInstallPump table in the PumpServicing Database.
    /// </summary>
    public sealed partial class EventInstallPumpMap : TableMap
    {
		public const string TABLE_NAME = "EventInstallPump";
		
		public static string ID = "eipPrimaryKey_GUID";
		public static string eipPrimaryKey = "eipPrimaryKey";
		public static string LastModified = "eipLstChgDT";
		public static string eipLstChgUser = "eipLstChgUser";
		public static string eipFK_Event_GUID = "eipFK_Event_GUID";
		public static string eipFK_Event = "eipFK_Event";
		public static string eipFK_Assembly_WellSurfaceLocation_GUID = "eipFK_Assembly_WellSurfaceLocation_GUID";
		public static string eipFK_Assembly_WellSurfaceLocation = "eipFK_Assembly_WellSurfaceLocation";
		public static string eipFK_WellCompletionXRef_GUID = "eipFK_WellCompletionXRef_GUID";
		public static string eipFK_WellCompletionXRef = "eipFK_WellCompletionXRef";
		
		public sealed partial class Prefixed
		{
			public static string ID = "EventInstallPump.eipPrimaryKey_GUID";
			public static string eipPrimaryKey = "EventInstallPump.eipPrimaryKey";
			public static string LastModified = "EventInstallPump.eipLstChgDT";
			public static string eipLstChgUser = "EventInstallPump.eipLstChgUser";
			public static string eipFK_Event_GUID = "EventInstallPump.eipFK_Event_GUID";
			public static string eipFK_Event = "EventInstallPump.eipFK_Event";
			public static string eipFK_Assembly_WellSurfaceLocation_GUID = "EventInstallPump.eipFK_Assembly_WellSurfaceLocation_GUID";
			public static string eipFK_Assembly_WellSurfaceLocation = "EventInstallPump.eipFK_Assembly_WellSurfaceLocation";
			public static string eipFK_WellCompletionXRef_GUID = "EventInstallPump.eipFK_WellCompletionXRef_GUID";
			public static string eipFK_WellCompletionXRef = "EventInstallPump.eipFK_WellCompletionXRef";
		}

		public sealed partial class Aliased
		{
			public static string ID = "EventInstallPumpeipPrimaryKey_GUID";
			public static string eipPrimaryKey = "EventInstallPumpeipPrimaryKey";
			public static string LastModified = "EventInstallPumpeipLstChgDT";
			public static string eipLstChgUser = "EventInstallPumpeipLstChgUser";
			public static string eipFK_Event_GUID = "EventInstallPumpeipFK_Event_GUID";
			public static string eipFK_Event = "EventInstallPumpeipFK_Event";
			public static string eipFK_Assembly_WellSurfaceLocation_GUID = "EventInstallPumpeipFK_Assembly_WellSurfaceLocation_GUID";
			public static string eipFK_Assembly_WellSurfaceLocation = "EventInstallPumpeipFK_Assembly_WellSurfaceLocation";
			public static string eipFK_WellCompletionXRef_GUID = "EventInstallPumpeipFK_WellCompletionXRef_GUID";
			public static string eipFK_WellCompletionXRef = "EventInstallPumpeipFK_WellCompletionXRef";
		}

		public sealed partial class Param
		{
			public static string ID = "@eipPrimaryKey_GUID";
			public static string eipPrimaryKey = "@eipPrimaryKey";
			public static string LastModified = "@eipLstChgDT";
			public static string eipLstChgUser = "@eipLstChgUser";
			public static string eipFK_Event_GUID = "@eipFK_Event_GUID";
			public static string eipFK_Event = "@eipFK_Event";
			public static string eipFK_Assembly_WellSurfaceLocation_GUID = "@eipFK_Assembly_WellSurfaceLocation_GUID";
			public static string eipFK_Assembly_WellSurfaceLocation = "@eipFK_Assembly_WellSurfaceLocation";
			public static string eipFK_WellCompletionXRef_GUID = "@eipFK_WellCompletionXRef_GUID";
			public static string eipFK_WellCompletionXRef = "@eipFK_WellCompletionXRef";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the EventPullPump table in the PumpServicing Database.
    /// </summary>
    public sealed partial class EventPullPumpMap : TableMap
    {
		public const string TABLE_NAME = "EventPullPump";
		
		public static string ID = "eppPrimaryKey_GUID";
		public static string eppPrimaryKey = "eppPrimaryKey";
		public static string LastModified = "eppLstChgDT";
		public static string eppLstChgUser = "eppLstChgUser";
		public static string eppFK_Event_GUID = "eppFK_Event_GUID";
		public static string eppFK_Event = "eppFK_Event";
		public static string eppFK_Assembly_WellSurfaceLocation_GUID = "eppFK_Assembly_WellSurfaceLocation_GUID";
		public static string eppFK_Assembly_WellSurfaceLocation = "eppFK_Assembly_WellSurfaceLocation";
		public static string eppFK_WellCompletionXRef_GUID = "eppFK_WellCompletionXRef_GUID";
		public static string eppFK_WellCompletionXRef = "eppFK_WellCompletionXRef";
		public static string eppFailedDate = "eppFailedDate";
		public static string eppRunDays = "eppRunDays";
		
		public sealed partial class Prefixed
		{
			public static string ID = "EventPullPump.eppPrimaryKey_GUID";
			public static string eppPrimaryKey = "EventPullPump.eppPrimaryKey";
			public static string LastModified = "EventPullPump.eppLstChgDT";
			public static string eppLstChgUser = "EventPullPump.eppLstChgUser";
			public static string eppFK_Event_GUID = "EventPullPump.eppFK_Event_GUID";
			public static string eppFK_Event = "EventPullPump.eppFK_Event";
			public static string eppFK_Assembly_WellSurfaceLocation_GUID = "EventPullPump.eppFK_Assembly_WellSurfaceLocation_GUID";
			public static string eppFK_Assembly_WellSurfaceLocation = "EventPullPump.eppFK_Assembly_WellSurfaceLocation";
			public static string eppFK_WellCompletionXRef_GUID = "EventPullPump.eppFK_WellCompletionXRef_GUID";
			public static string eppFK_WellCompletionXRef = "EventPullPump.eppFK_WellCompletionXRef";
			public static string eppFailedDate = "EventPullPump.eppFailedDate";
			public static string eppRunDays = "EventPullPump.eppRunDays";
		}

		public sealed partial class Aliased
		{
			public static string ID = "EventPullPumpeppPrimaryKey_GUID";
			public static string eppPrimaryKey = "EventPullPumpeppPrimaryKey";
			public static string LastModified = "EventPullPumpeppLstChgDT";
			public static string eppLstChgUser = "EventPullPumpeppLstChgUser";
			public static string eppFK_Event_GUID = "EventPullPumpeppFK_Event_GUID";
			public static string eppFK_Event = "EventPullPumpeppFK_Event";
			public static string eppFK_Assembly_WellSurfaceLocation_GUID = "EventPullPumpeppFK_Assembly_WellSurfaceLocation_GUID";
			public static string eppFK_Assembly_WellSurfaceLocation = "EventPullPumpeppFK_Assembly_WellSurfaceLocation";
			public static string eppFK_WellCompletionXRef_GUID = "EventPullPumpeppFK_WellCompletionXRef_GUID";
			public static string eppFK_WellCompletionXRef = "EventPullPumpeppFK_WellCompletionXRef";
			public static string eppFailedDate = "EventPullPumpeppFailedDate";
			public static string eppRunDays = "EventPullPumpeppRunDays";
		}

		public sealed partial class Param
		{
			public static string ID = "@eppPrimaryKey_GUID";
			public static string eppPrimaryKey = "@eppPrimaryKey";
			public static string LastModified = "@eppLstChgDT";
			public static string eppLstChgUser = "@eppLstChgUser";
			public static string eppFK_Event_GUID = "@eppFK_Event_GUID";
			public static string eppFK_Event = "@eppFK_Event";
			public static string eppFK_Assembly_WellSurfaceLocation_GUID = "@eppFK_Assembly_WellSurfaceLocation_GUID";
			public static string eppFK_Assembly_WellSurfaceLocation = "@eppFK_Assembly_WellSurfaceLocation";
			public static string eppFK_WellCompletionXRef_GUID = "@eppFK_WellCompletionXRef_GUID";
			public static string eppFK_WellCompletionXRef = "@eppFK_WellCompletionXRef";
			public static string eppFailedDate = "@eppFailedDate";
			public static string eppRunDays = "@eppRunDays";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the Facility table in the PumpServicing Database.
    /// </summary>
    public sealed partial class FacilityMap : TableMap
    {
		public const string TABLE_NAME = "Facility";
		
		public static string ID = "facPrimaryKey_GUID";
		public static string facPrimaryKey = "facPrimaryKey";
		public static string LastModified = "facLstChgDT";
		public static string facLstChgUser = "facLstChgUser";
		public static string facRefCaseDefined = "facRefCaseDefined";
		public static string facRefUserDeleted = "facRefUserDeleted";
		public static string facFK_Assembly_GUID = "facFK_Assembly_GUID";
		public static string facFK_Assembly = "facFK_Assembly";
		public static string facFK_r_FacilityType_GUID = "facFK_r_FacilityType_GUID";
		public static string facFK_r_FacilityType = "facFK_r_FacilityType";
		public static string facFacilityID = "facFacilityID";
		public static string facFacilityDescription = "facFacilityDescription";
		public static string facFK_Owner = "facFK_Owner";
		public static string facFK_BusinessOrganization_GUID = "facFK_BusinessOrganization_GUID";
		public static string facFK_BusinessOrganization = "facFK_BusinessOrganization";
		public static string facStorageID = "facStorageID";
		public static string facInactive = "facInactive";
		
		public sealed partial class Prefixed
		{
			public static string ID = "Facility.facPrimaryKey_GUID";
			public static string facPrimaryKey = "Facility.facPrimaryKey";
			public static string LastModified = "Facility.facLstChgDT";
			public static string facLstChgUser = "Facility.facLstChgUser";
			public static string facRefCaseDefined = "Facility.facRefCaseDefined";
			public static string facRefUserDeleted = "Facility.facRefUserDeleted";
			public static string facFK_Assembly_GUID = "Facility.facFK_Assembly_GUID";
			public static string facFK_Assembly = "Facility.facFK_Assembly";
			public static string facFK_r_FacilityType_GUID = "Facility.facFK_r_FacilityType_GUID";
			public static string facFK_r_FacilityType = "Facility.facFK_r_FacilityType";
			public static string facFacilityID = "Facility.facFacilityID";
			public static string facFacilityDescription = "Facility.facFacilityDescription";
			public static string facFK_Owner = "Facility.facFK_Owner";
			public static string facFK_BusinessOrganization_GUID = "Facility.facFK_BusinessOrganization_GUID";
			public static string facFK_BusinessOrganization = "Facility.facFK_BusinessOrganization";
			public static string facStorageID = "Facility.facStorageID";
			public static string facInactive = "Facility.facInactive";
		}

		public sealed partial class Aliased
		{
			public static string ID = "FacilityfacPrimaryKey_GUID";
			public static string facPrimaryKey = "FacilityfacPrimaryKey";
			public static string LastModified = "FacilityfacLstChgDT";
			public static string facLstChgUser = "FacilityfacLstChgUser";
			public static string facRefCaseDefined = "FacilityfacRefCaseDefined";
			public static string facRefUserDeleted = "FacilityfacRefUserDeleted";
			public static string facFK_Assembly_GUID = "FacilityfacFK_Assembly_GUID";
			public static string facFK_Assembly = "FacilityfacFK_Assembly";
			public static string facFK_r_FacilityType_GUID = "FacilityfacFK_r_FacilityType_GUID";
			public static string facFK_r_FacilityType = "FacilityfacFK_r_FacilityType";
			public static string facFacilityID = "FacilityfacFacilityID";
			public static string facFacilityDescription = "FacilityfacFacilityDescription";
			public static string facFK_Owner = "FacilityfacFK_Owner";
			public static string facFK_BusinessOrganization_GUID = "FacilityfacFK_BusinessOrganization_GUID";
			public static string facFK_BusinessOrganization = "FacilityfacFK_BusinessOrganization";
			public static string facStorageID = "FacilityfacStorageID";
			public static string facInactive = "FacilityfacInactive";
		}

		public sealed partial class Param
		{
			public static string ID = "@facPrimaryKey_GUID";
			public static string facPrimaryKey = "@facPrimaryKey";
			public static string LastModified = "@facLstChgDT";
			public static string facLstChgUser = "@facLstChgUser";
			public static string facRefCaseDefined = "@facRefCaseDefined";
			public static string facRefUserDeleted = "@facRefUserDeleted";
			public static string facFK_Assembly_GUID = "@facFK_Assembly_GUID";
			public static string facFK_Assembly = "@facFK_Assembly";
			public static string facFK_r_FacilityType_GUID = "@facFK_r_FacilityType_GUID";
			public static string facFK_r_FacilityType = "@facFK_r_FacilityType";
			public static string facFacilityID = "@facFacilityID";
			public static string facFacilityDescription = "@facFacilityDescription";
			public static string facFK_Owner = "@facFK_Owner";
			public static string facFK_BusinessOrganization_GUID = "@facFK_BusinessOrganization_GUID";
			public static string facFK_BusinessOrganization = "@facFK_BusinessOrganization";
			public static string facStorageID = "@facStorageID";
			public static string facInactive = "@facInactive";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the Invoice table in the PumpServicing Database.
    /// </summary>
    public sealed partial class InvoiceMap : TableMap
    {
		public const string TABLE_NAME = "Invoice";
		
		public static string ID = "xh5PrimaryKey_GUID";
		public static string xh5PrimaryKey = "xh5PrimaryKey";
		public static string xh5RefCaseDefined = "xh5RefCaseDefined";
		public static string LastModified = "xh5LstChgDT";
		public static string xh5LstChgUser = "xh5LstChgUser";
		public static string xh5FK_Assembly_PumpShop_GUID = "xh5FK_Assembly_PumpShop_GUID";
		public static string xh5FK_Assembly_PumpShop = "xh5FK_Assembly_PumpShop";
		public static string xh5InvoiceID = "xh5InvoiceID";
		public static string xh5AccountingReferenceID = "xh5AccountingReferenceID";
		public static string xh5Invoiced = "xh5Invoiced";
		public static string xh5InvoiceDate = "xh5InvoiceDate";
		public static string xh5FK_BusinessOrganization_Producer_GUID = "xh5FK_BusinessOrganization_Producer_GUID";
		public static string xh5FK_BusinessOrganization_Producer = "xh5FK_BusinessOrganization_Producer";
		public static string xh5FK_Assembly_Well_GUID = "xh5FK_Assembly_Well_GUID";
		public static string xh5FK_Assembly_Well = "xh5FK_Assembly_Well";
		public static string xh5FK_WellCompletionXRef_GUID = "xh5FK_WellCompletionXRef_GUID";
		public static string xh5FK_WellCompletionXRef = "xh5FK_WellCompletionXRef";
		public static string xh5ISO4217CurrencyCode = "xh5ISO4217CurrencyCode";
		public static string xh5ProductLineID = "xh5ProductLineID";
		public static string xh5ExternalTransactionComplete = "xh5ExternalTransactionComplete";
		public static string xh5ReturnTransactionMessages = "xh5ReturnTransactionMessages";
		public static string xh5Remarks = "xh5Remarks";
		public static string xh5FK_Job = "xh5FK_Job";
		public static string xh5FK_Job_GUID = "xh5FK_Job_GUID";
		
		public sealed partial class Prefixed
		{
			public static string ID = "Invoice.xh5PrimaryKey_GUID";
			public static string xh5PrimaryKey = "Invoice.xh5PrimaryKey";
			public static string xh5RefCaseDefined = "Invoice.xh5RefCaseDefined";
			public static string LastModified = "Invoice.xh5LstChgDT";
			public static string xh5LstChgUser = "Invoice.xh5LstChgUser";
			public static string xh5FK_Assembly_PumpShop_GUID = "Invoice.xh5FK_Assembly_PumpShop_GUID";
			public static string xh5FK_Assembly_PumpShop = "Invoice.xh5FK_Assembly_PumpShop";
			public static string xh5InvoiceID = "Invoice.xh5InvoiceID";
			public static string xh5AccountingReferenceID = "Invoice.xh5AccountingReferenceID";
			public static string xh5Invoiced = "Invoice.xh5Invoiced";
			public static string xh5InvoiceDate = "Invoice.xh5InvoiceDate";
			public static string xh5FK_BusinessOrganization_Producer_GUID = "Invoice.xh5FK_BusinessOrganization_Producer_GUID";
			public static string xh5FK_BusinessOrganization_Producer = "Invoice.xh5FK_BusinessOrganization_Producer";
			public static string xh5FK_Assembly_Well_GUID = "Invoice.xh5FK_Assembly_Well_GUID";
			public static string xh5FK_Assembly_Well = "Invoice.xh5FK_Assembly_Well";
			public static string xh5FK_WellCompletionXRef_GUID = "Invoice.xh5FK_WellCompletionXRef_GUID";
			public static string xh5FK_WellCompletionXRef = "Invoice.xh5FK_WellCompletionXRef";
			public static string xh5ISO4217CurrencyCode = "Invoice.xh5ISO4217CurrencyCode";
			public static string xh5ProductLineID = "Invoice.xh5ProductLineID";
			public static string xh5ExternalTransactionComplete = "Invoice.xh5ExternalTransactionComplete";
			public static string xh5ReturnTransactionMessages = "Invoice.xh5ReturnTransactionMessages";
			public static string xh5Remarks = "Invoice.xh5Remarks";
			public static string xh5FK_Job = "Invoice.xh5FK_Job";
			public static string xh5FK_Job_GUID = "Invoice.xh5FK_Job_GUID";
		}

		public sealed partial class Aliased
		{
			public static string ID = "Invoicexh5PrimaryKey_GUID";
			public static string xh5PrimaryKey = "Invoicexh5PrimaryKey";
			public static string xh5RefCaseDefined = "Invoicexh5RefCaseDefined";
			public static string LastModified = "Invoicexh5LstChgDT";
			public static string xh5LstChgUser = "Invoicexh5LstChgUser";
			public static string xh5FK_Assembly_PumpShop_GUID = "Invoicexh5FK_Assembly_PumpShop_GUID";
			public static string xh5FK_Assembly_PumpShop = "Invoicexh5FK_Assembly_PumpShop";
			public static string xh5InvoiceID = "Invoicexh5InvoiceID";
			public static string xh5AccountingReferenceID = "Invoicexh5AccountingReferenceID";
			public static string xh5Invoiced = "Invoicexh5Invoiced";
			public static string xh5InvoiceDate = "Invoicexh5InvoiceDate";
			public static string xh5FK_BusinessOrganization_Producer_GUID = "Invoicexh5FK_BusinessOrganization_Producer_GUID";
			public static string xh5FK_BusinessOrganization_Producer = "Invoicexh5FK_BusinessOrganization_Producer";
			public static string xh5FK_Assembly_Well_GUID = "Invoicexh5FK_Assembly_Well_GUID";
			public static string xh5FK_Assembly_Well = "Invoicexh5FK_Assembly_Well";
			public static string xh5FK_WellCompletionXRef_GUID = "Invoicexh5FK_WellCompletionXRef_GUID";
			public static string xh5FK_WellCompletionXRef = "Invoicexh5FK_WellCompletionXRef";
			public static string xh5ISO4217CurrencyCode = "Invoicexh5ISO4217CurrencyCode";
			public static string xh5ProductLineID = "Invoicexh5ProductLineID";
			public static string xh5ExternalTransactionComplete = "Invoicexh5ExternalTransactionComplete";
			public static string xh5ReturnTransactionMessages = "Invoicexh5ReturnTransactionMessages";
			public static string xh5Remarks = "Invoicexh5Remarks";
			public static string xh5FK_Job = "Invoicexh5FK_Job";
			public static string xh5FK_Job_GUID = "Invoicexh5FK_Job_GUID";
		}

		public sealed partial class Param
		{
			public static string ID = "@xh5PrimaryKey_GUID";
			public static string xh5PrimaryKey = "@xh5PrimaryKey";
			public static string xh5RefCaseDefined = "@xh5RefCaseDefined";
			public static string LastModified = "@xh5LstChgDT";
			public static string xh5LstChgUser = "@xh5LstChgUser";
			public static string xh5FK_Assembly_PumpShop_GUID = "@xh5FK_Assembly_PumpShop_GUID";
			public static string xh5FK_Assembly_PumpShop = "@xh5FK_Assembly_PumpShop";
			public static string xh5InvoiceID = "@xh5InvoiceID";
			public static string xh5AccountingReferenceID = "@xh5AccountingReferenceID";
			public static string xh5Invoiced = "@xh5Invoiced";
			public static string xh5InvoiceDate = "@xh5InvoiceDate";
			public static string xh5FK_BusinessOrganization_Producer_GUID = "@xh5FK_BusinessOrganization_Producer_GUID";
			public static string xh5FK_BusinessOrganization_Producer = "@xh5FK_BusinessOrganization_Producer";
			public static string xh5FK_Assembly_Well_GUID = "@xh5FK_Assembly_Well_GUID";
			public static string xh5FK_Assembly_Well = "@xh5FK_Assembly_Well";
			public static string xh5FK_WellCompletionXRef_GUID = "@xh5FK_WellCompletionXRef_GUID";
			public static string xh5FK_WellCompletionXRef = "@xh5FK_WellCompletionXRef";
			public static string xh5ISO4217CurrencyCode = "@xh5ISO4217CurrencyCode";
			public static string xh5ProductLineID = "@xh5ProductLineID";
			public static string xh5ExternalTransactionComplete = "@xh5ExternalTransactionComplete";
			public static string xh5ReturnTransactionMessages = "@xh5ReturnTransactionMessages";
			public static string xh5Remarks = "@xh5Remarks";
			public static string xh5FK_Job = "@xh5FK_Job";
			public static string xh5FK_Job_GUID = "@xh5FK_Job_GUID";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the Job table in the PumpServicing Database.
    /// </summary>
    public sealed partial class JobMap : TableMap
    {
		public const string TABLE_NAME = "Job";
		
		public static string ID = "ecgPrimaryKey_GUID";
		public static string ecgPrimaryKey = "ecgPrimaryKey";
		public static string LastModified = "ecgLstChgDT";
		public static string ecgLstChgUser = "ecgLstChgUser";
		public static string ecgRefCaseDefined = "ecgRefCaseDefined";
		public static string ecgFK_Assembly_GUID = "ecgFK_Assembly_GUID";
		public static string ecgFK_Assembly = "ecgFK_Assembly";
		public static string ecgFK_BusinessOrganization_GUID = "ecgFK_BusinessOrganization_GUID";
		public static string ecgFK_BusinessOrganization = "ecgFK_BusinessOrganization";
		public static string ecgFK_r_EventCategoryType_GUID = "ecgFK_r_EventCategoryType_GUID";
		public static string ecgFK_r_EventCategoryType = "ecgFK_r_EventCategoryType";
		public static string ecgFK_r_EventCategoryReason_GUID = "ecgFK_r_EventCategoryReason_GUID";
		public static string ecgFK_r_EventCategoryReason = "ecgFK_r_EventCategoryReason";
		public static string ecgEventBegDtTm = "ecgEventBegDtTm";
		public static string ecgEventEndDtTm = "ecgEventEndDtTm";
		public static string ecgFK_r_JobStatus_GUID = "ecgFK_r_JobStatus_GUID";
		public static string ecgFK_r_JobStatus = "ecgFK_r_JobStatus";
		public static string ecgStatusDt = "ecgStatusDt";
		public static string ecgStChgUser = "ecgStChgUser";
		public static string ecgJobID = "ecgJobID";
		public static string ecgOriginKey = "ecgOriginKey";
		public static string ecgAcctRef = "ecgAcctRef";
		public static string ecgFK_WellCompletionXRef_GUID = "ecgFK_WellCompletionXRef_GUID";
		public static string ecgFK_WellCompletionXRef = "ecgFK_WellCompletionXRef";
		public static string ecgFK_r_PumpJobType_GUID = "ecgFK_r_PumpJobType_GUID";
		public static string ecgFK_r_PumpJobType = "ecgFK_r_PumpJobType";
		public static string ecgRemarks = "ecgRemarks";
		public static string ecgPrevRunDT = "ecgPrevRunDT";
		public static string ecgWellFailedDate = "ecgWellFailedDate";
		public static string ecgWellPullDT = "ecgWellPullDT";
		
		public sealed partial class Prefixed
		{
			public static string ID = "Job.ecgPrimaryKey_GUID";
			public static string ecgPrimaryKey = "Job.ecgPrimaryKey";
			public static string LastModified = "Job.ecgLstChgDT";
			public static string ecgLstChgUser = "Job.ecgLstChgUser";
			public static string ecgRefCaseDefined = "Job.ecgRefCaseDefined";
			public static string ecgFK_Assembly_GUID = "Job.ecgFK_Assembly_GUID";
			public static string ecgFK_Assembly = "Job.ecgFK_Assembly";
			public static string ecgFK_BusinessOrganization_GUID = "Job.ecgFK_BusinessOrganization_GUID";
			public static string ecgFK_BusinessOrganization = "Job.ecgFK_BusinessOrganization";
			public static string ecgFK_r_EventCategoryType_GUID = "Job.ecgFK_r_EventCategoryType_GUID";
			public static string ecgFK_r_EventCategoryType = "Job.ecgFK_r_EventCategoryType";
			public static string ecgFK_r_EventCategoryReason_GUID = "Job.ecgFK_r_EventCategoryReason_GUID";
			public static string ecgFK_r_EventCategoryReason = "Job.ecgFK_r_EventCategoryReason";
			public static string ecgEventBegDtTm = "Job.ecgEventBegDtTm";
			public static string ecgEventEndDtTm = "Job.ecgEventEndDtTm";
			public static string ecgFK_r_JobStatus_GUID = "Job.ecgFK_r_JobStatus_GUID";
			public static string ecgFK_r_JobStatus = "Job.ecgFK_r_JobStatus";
			public static string ecgStatusDt = "Job.ecgStatusDt";
			public static string ecgStChgUser = "Job.ecgStChgUser";
			public static string ecgJobID = "Job.ecgJobID";
			public static string ecgOriginKey = "Job.ecgOriginKey";
			public static string ecgAcctRef = "Job.ecgAcctRef";
			public static string ecgFK_WellCompletionXRef_GUID = "Job.ecgFK_WellCompletionXRef_GUID";
			public static string ecgFK_WellCompletionXRef = "Job.ecgFK_WellCompletionXRef";
			public static string ecgFK_r_PumpJobType_GUID = "Job.ecgFK_r_PumpJobType_GUID";
			public static string ecgFK_r_PumpJobType = "Job.ecgFK_r_PumpJobType";
			public static string ecgRemarks = "Job.ecgRemarks";
			public static string ecgPrevRunDT = "Job.ecgPrevRunDT";
			public static string ecgWellFailedDate = "Job.ecgWellFailedDate";
			public static string ecgWellPullDT = "Job.ecgWellPullDT";
		}

		public sealed partial class Aliased
		{
			public static string ID = "JobecgPrimaryKey_GUID";
			public static string ecgPrimaryKey = "JobecgPrimaryKey";
			public static string LastModified = "JobecgLstChgDT";
			public static string ecgLstChgUser = "JobecgLstChgUser";
			public static string ecgRefCaseDefined = "JobecgRefCaseDefined";
			public static string ecgFK_Assembly_GUID = "JobecgFK_Assembly_GUID";
			public static string ecgFK_Assembly = "JobecgFK_Assembly";
			public static string ecgFK_BusinessOrganization_GUID = "JobecgFK_BusinessOrganization_GUID";
			public static string ecgFK_BusinessOrganization = "JobecgFK_BusinessOrganization";
			public static string ecgFK_r_EventCategoryType_GUID = "JobecgFK_r_EventCategoryType_GUID";
			public static string ecgFK_r_EventCategoryType = "JobecgFK_r_EventCategoryType";
			public static string ecgFK_r_EventCategoryReason_GUID = "JobecgFK_r_EventCategoryReason_GUID";
			public static string ecgFK_r_EventCategoryReason = "JobecgFK_r_EventCategoryReason";
			public static string ecgEventBegDtTm = "JobecgEventBegDtTm";
			public static string ecgEventEndDtTm = "JobecgEventEndDtTm";
			public static string ecgFK_r_JobStatus_GUID = "JobecgFK_r_JobStatus_GUID";
			public static string ecgFK_r_JobStatus = "JobecgFK_r_JobStatus";
			public static string ecgStatusDt = "JobecgStatusDt";
			public static string ecgStChgUser = "JobecgStChgUser";
			public static string ecgJobID = "JobecgJobID";
			public static string ecgOriginKey = "JobecgOriginKey";
			public static string ecgAcctRef = "JobecgAcctRef";
			public static string ecgFK_WellCompletionXRef_GUID = "JobecgFK_WellCompletionXRef_GUID";
			public static string ecgFK_WellCompletionXRef = "JobecgFK_WellCompletionXRef";
			public static string ecgFK_r_PumpJobType_GUID = "JobecgFK_r_PumpJobType_GUID";
			public static string ecgFK_r_PumpJobType = "JobecgFK_r_PumpJobType";
			public static string ecgRemarks = "JobecgRemarks";
			public static string ecgPrevRunDT = "JobecgPrevRunDT";
			public static string ecgWellFailedDate = "JobecgWellFailedDate";
			public static string ecgWellPullDT = "JobecgWellPullDT";
		}

		public sealed partial class Param
		{
			public static string ID = "@ecgPrimaryKey_GUID";
			public static string ecgPrimaryKey = "@ecgPrimaryKey";
			public static string LastModified = "@ecgLstChgDT";
			public static string ecgLstChgUser = "@ecgLstChgUser";
			public static string ecgRefCaseDefined = "@ecgRefCaseDefined";
			public static string ecgFK_Assembly_GUID = "@ecgFK_Assembly_GUID";
			public static string ecgFK_Assembly = "@ecgFK_Assembly";
			public static string ecgFK_BusinessOrganization_GUID = "@ecgFK_BusinessOrganization_GUID";
			public static string ecgFK_BusinessOrganization = "@ecgFK_BusinessOrganization";
			public static string ecgFK_r_EventCategoryType_GUID = "@ecgFK_r_EventCategoryType_GUID";
			public static string ecgFK_r_EventCategoryType = "@ecgFK_r_EventCategoryType";
			public static string ecgFK_r_EventCategoryReason_GUID = "@ecgFK_r_EventCategoryReason_GUID";
			public static string ecgFK_r_EventCategoryReason = "@ecgFK_r_EventCategoryReason";
			public static string ecgEventBegDtTm = "@ecgEventBegDtTm";
			public static string ecgEventEndDtTm = "@ecgEventEndDtTm";
			public static string ecgFK_r_JobStatus_GUID = "@ecgFK_r_JobStatus_GUID";
			public static string ecgFK_r_JobStatus = "@ecgFK_r_JobStatus";
			public static string ecgStatusDt = "@ecgStatusDt";
			public static string ecgStChgUser = "@ecgStChgUser";
			public static string ecgJobID = "@ecgJobID";
			public static string ecgOriginKey = "@ecgOriginKey";
			public static string ecgAcctRef = "@ecgAcctRef";
			public static string ecgFK_WellCompletionXRef_GUID = "@ecgFK_WellCompletionXRef_GUID";
			public static string ecgFK_WellCompletionXRef = "@ecgFK_WellCompletionXRef";
			public static string ecgFK_r_PumpJobType_GUID = "@ecgFK_r_PumpJobType_GUID";
			public static string ecgFK_r_PumpJobType = "@ecgFK_r_PumpJobType";
			public static string ecgRemarks = "@ecgRemarks";
			public static string ecgPrevRunDT = "@ecgPrevRunDT";
			public static string ecgWellFailedDate = "@ecgWellFailedDate";
			public static string ecgWellPullDT = "@ecgWellPullDT";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the JobStatusChangeLog table in the PumpServicing Database.
    /// </summary>
    public sealed partial class JobStatusChangeLogMap : TableMap
    {
		public const string TABLE_NAME = "JobStatusChangeLog";
		
		public static string ID = "jscPrimaryKey_GUID";
		public static string jscPrimaryKey = "jscPrimaryKey";
		public static string LastModified = "jscLstChgDT";
		public static string jscLstChgUser = "jscLstChgUser";
		public static string jscFK_Job_GUID = "jscFK_Job_GUID";
		public static string jscFK_Job = "jscFK_Job";
		public static string jscFK_r_JobStatus_GUID = "jscFK_r_JobStatus_GUID";
		public static string jscFK_r_JobStatus = "jscFK_r_JobStatus";
		public static string jscStatusDt = "jscStatusDt";
		public static string jscStChgUser = "jscStChgUser";
		
		public sealed partial class Prefixed
		{
			public static string ID = "JobStatusChangeLog.jscPrimaryKey_GUID";
			public static string jscPrimaryKey = "JobStatusChangeLog.jscPrimaryKey";
			public static string LastModified = "JobStatusChangeLog.jscLstChgDT";
			public static string jscLstChgUser = "JobStatusChangeLog.jscLstChgUser";
			public static string jscFK_Job_GUID = "JobStatusChangeLog.jscFK_Job_GUID";
			public static string jscFK_Job = "JobStatusChangeLog.jscFK_Job";
			public static string jscFK_r_JobStatus_GUID = "JobStatusChangeLog.jscFK_r_JobStatus_GUID";
			public static string jscFK_r_JobStatus = "JobStatusChangeLog.jscFK_r_JobStatus";
			public static string jscStatusDt = "JobStatusChangeLog.jscStatusDt";
			public static string jscStChgUser = "JobStatusChangeLog.jscStChgUser";
		}

		public sealed partial class Aliased
		{
			public static string ID = "JobStatusChangeLogjscPrimaryKey_GUID";
			public static string jscPrimaryKey = "JobStatusChangeLogjscPrimaryKey";
			public static string LastModified = "JobStatusChangeLogjscLstChgDT";
			public static string jscLstChgUser = "JobStatusChangeLogjscLstChgUser";
			public static string jscFK_Job_GUID = "JobStatusChangeLogjscFK_Job_GUID";
			public static string jscFK_Job = "JobStatusChangeLogjscFK_Job";
			public static string jscFK_r_JobStatus_GUID = "JobStatusChangeLogjscFK_r_JobStatus_GUID";
			public static string jscFK_r_JobStatus = "JobStatusChangeLogjscFK_r_JobStatus";
			public static string jscStatusDt = "JobStatusChangeLogjscStatusDt";
			public static string jscStChgUser = "JobStatusChangeLogjscStChgUser";
		}

		public sealed partial class Param
		{
			public static string ID = "@jscPrimaryKey_GUID";
			public static string jscPrimaryKey = "@jscPrimaryKey";
			public static string LastModified = "@jscLstChgDT";
			public static string jscLstChgUser = "@jscLstChgUser";
			public static string jscFK_Job_GUID = "@jscFK_Job_GUID";
			public static string jscFK_Job = "@jscFK_Job";
			public static string jscFK_r_JobStatus_GUID = "@jscFK_r_JobStatus_GUID";
			public static string jscFK_r_JobStatus = "@jscFK_r_JobStatus";
			public static string jscStatusDt = "@jscStatusDt";
			public static string jscStChgUser = "@jscStChgUser";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the Lease table in the PumpServicing Database.
    /// </summary>
    public sealed partial class LeaseMap : TableMap
    {
		public const string TABLE_NAME = "Lease";
		
		public static string ID = "lsePrimaryKey_GUID";
		public static string lsePrimaryKey = "lsePrimaryKey";
		public static string LastModified = "lseLstChgDT";
		public static string lseLstChgUser = "lseLstChgUser";
		public static string lseRefCaseDefined = "lseRefCaseDefined";
		public static string lseRefUserDeleted = "lseRefUserDeleted";
		public static string lseFK_BusinessOrganization_GUID = "lseFK_BusinessOrganization_GUID";
		public static string lseFK_BusinessOrganization = "lseFK_BusinessOrganization";
		public static string lseLeaseID = "lseLeaseID";
		public static string lseLeaseName = "lseLeaseName";
		public static string lsePMTaxableStatus = "lsePMTaxableStatus";
		public static string lsePMTaxRate = "lsePMTaxRate";
		public static string lseCHTaxableStatus = "lseCHTaxableStatus";
		public static string lseCHTaxRate = "lseCHTaxRate";
		public static string lseWSTaxableStatus = "lseWSTaxableStatus";
		public static string lseWSTaxRate = "lseWSTaxRate";
		
		public sealed partial class Prefixed
		{
			public static string ID = "Lease.lsePrimaryKey_GUID";
			public static string lsePrimaryKey = "Lease.lsePrimaryKey";
			public static string LastModified = "Lease.lseLstChgDT";
			public static string lseLstChgUser = "Lease.lseLstChgUser";
			public static string lseRefCaseDefined = "Lease.lseRefCaseDefined";
			public static string lseRefUserDeleted = "Lease.lseRefUserDeleted";
			public static string lseFK_BusinessOrganization_GUID = "Lease.lseFK_BusinessOrganization_GUID";
			public static string lseFK_BusinessOrganization = "Lease.lseFK_BusinessOrganization";
			public static string lseLeaseID = "Lease.lseLeaseID";
			public static string lseLeaseName = "Lease.lseLeaseName";
			public static string lsePMTaxableStatus = "Lease.lsePMTaxableStatus";
			public static string lsePMTaxRate = "Lease.lsePMTaxRate";
			public static string lseCHTaxableStatus = "Lease.lseCHTaxableStatus";
			public static string lseCHTaxRate = "Lease.lseCHTaxRate";
			public static string lseWSTaxableStatus = "Lease.lseWSTaxableStatus";
			public static string lseWSTaxRate = "Lease.lseWSTaxRate";
		}

		public sealed partial class Aliased
		{
			public static string ID = "LeaselsePrimaryKey_GUID";
			public static string lsePrimaryKey = "LeaselsePrimaryKey";
			public static string LastModified = "LeaselseLstChgDT";
			public static string lseLstChgUser = "LeaselseLstChgUser";
			public static string lseRefCaseDefined = "LeaselseRefCaseDefined";
			public static string lseRefUserDeleted = "LeaselseRefUserDeleted";
			public static string lseFK_BusinessOrganization_GUID = "LeaselseFK_BusinessOrganization_GUID";
			public static string lseFK_BusinessOrganization = "LeaselseFK_BusinessOrganization";
			public static string lseLeaseID = "LeaselseLeaseID";
			public static string lseLeaseName = "LeaselseLeaseName";
			public static string lsePMTaxableStatus = "LeaselsePMTaxableStatus";
			public static string lsePMTaxRate = "LeaselsePMTaxRate";
			public static string lseCHTaxableStatus = "LeaselseCHTaxableStatus";
			public static string lseCHTaxRate = "LeaselseCHTaxRate";
			public static string lseWSTaxableStatus = "LeaselseWSTaxableStatus";
			public static string lseWSTaxRate = "LeaselseWSTaxRate";
		}

		public sealed partial class Param
		{
			public static string ID = "@lsePrimaryKey_GUID";
			public static string lsePrimaryKey = "@lsePrimaryKey";
			public static string LastModified = "@lseLstChgDT";
			public static string lseLstChgUser = "@lseLstChgUser";
			public static string lseRefCaseDefined = "@lseRefCaseDefined";
			public static string lseRefUserDeleted = "@lseRefUserDeleted";
			public static string lseFK_BusinessOrganization_GUID = "@lseFK_BusinessOrganization_GUID";
			public static string lseFK_BusinessOrganization = "@lseFK_BusinessOrganization";
			public static string lseLeaseID = "@lseLeaseID";
			public static string lseLeaseName = "@lseLeaseName";
			public static string lsePMTaxableStatus = "@lsePMTaxableStatus";
			public static string lsePMTaxRate = "@lsePMTaxRate";
			public static string lseCHTaxableStatus = "@lseCHTaxableStatus";
			public static string lseCHTaxRate = "@lseCHTaxRate";
			public static string lseWSTaxableStatus = "@lseWSTaxableStatus";
			public static string lseWSTaxRate = "@lseWSTaxRate";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the Owner table in the PumpServicing Database.
    /// </summary>
    public sealed partial class OwnerMap : TableMap
    {
		public const string TABLE_NAME = "Owner";
		
		public static string ID = "ownPrimaryKey";
		public static string LastModified = "ownLstChgDT";
		public static string ownLstChgUser = "ownLstChgUser";
		public static string ownRefCaseDefined = "ownRefCaseDefined";
		public static string ownRefUserDeleted = "ownRefUserDeleted";
		public static string ownOwnerName = "ownOwnerName";
		public static string ownFK_BusinessOrganization = "ownFK_BusinessOrganization";
		
		public sealed partial class Prefixed
		{
			public static string ID = "Owner.ownPrimaryKey";
			public static string LastModified = "Owner.ownLstChgDT";
			public static string ownLstChgUser = "Owner.ownLstChgUser";
			public static string ownRefCaseDefined = "Owner.ownRefCaseDefined";
			public static string ownRefUserDeleted = "Owner.ownRefUserDeleted";
			public static string ownOwnerName = "Owner.ownOwnerName";
			public static string ownFK_BusinessOrganization = "Owner.ownFK_BusinessOrganization";
		}

		public sealed partial class Aliased
		{
			public static string ID = "OwnerownPrimaryKey";
			public static string LastModified = "OwnerownLstChgDT";
			public static string ownLstChgUser = "OwnerownLstChgUser";
			public static string ownRefCaseDefined = "OwnerownRefCaseDefined";
			public static string ownRefUserDeleted = "OwnerownRefUserDeleted";
			public static string ownOwnerName = "OwnerownOwnerName";
			public static string ownFK_BusinessOrganization = "OwnerownFK_BusinessOrganization";
		}

		public sealed partial class Param
		{
			public static string ID = "@ownPrimaryKey";
			public static string LastModified = "@ownLstChgDT";
			public static string ownLstChgUser = "@ownLstChgUser";
			public static string ownRefCaseDefined = "@ownRefCaseDefined";
			public static string ownRefUserDeleted = "@ownRefUserDeleted";
			public static string ownOwnerName = "@ownOwnerName";
			public static string ownFK_BusinessOrganization = "@ownFK_BusinessOrganization";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the StickyNotes table in the PumpServicing Database.
    /// </summary>
    public sealed partial class StickyNotesMap : TableMap
    {
		public const string TABLE_NAME = "StickyNotes";
		
		public static string ID = "styPrimaryKey_GUID";
		public static string styPrimaryKey = "styPrimaryKey";
		public static string LastModified = "styLstChgDT";
		public static string styLstChgUser = "styLstChgUser";
		public static string styFK_r_MessageActivityType_GUID = "styFK_r_MessageActivityType_GUID";
		public static string styFK_r_MessageActivityType = "styFK_r_MessageActivityType";
		public static string styMsgOriginDT = "styMsgOriginDT";
		public static string styFK_Assembly_GUID = "styFK_Assembly_GUID";
		public static string styFK_Assembly = "styFK_Assembly";
		public static string styFK_Event_GUID = "styFK_Event_GUID";
		public static string styFK_Event = "styFK_Event";
		public static string styFK_r_StickyNoteStatus_GUID = "styFK_r_StickyNoteStatus_GUID";
		public static string styFK_r_StickyNoteStatus = "styFK_r_StickyNoteStatus";
		public static string styFK_r_MessageActivityPriorityCd_GUID = "styFK_r_MessageActivityPriorityCd_GUID";
		public static string styFK_r_MessageActivityPriorityCd = "styFK_r_MessageActivityPriorityCd";
		public static string styMsgCompletionDT = "styMsgCompletionDT";
		public static string stySender = "stySender";
		public static string styRecipient = "styRecipient";
		public static string styBriefDescription = "styBriefDescription";
		public static string styCompletionComments = "styCompletionComments";
		public static string styDocumentFileName = "styDocumentFileName";
		public static string styMessage = "styMessage";
		
		public sealed partial class Prefixed
		{
			public static string ID = "StickyNotes.styPrimaryKey_GUID";
			public static string styPrimaryKey = "StickyNotes.styPrimaryKey";
			public static string LastModified = "StickyNotes.styLstChgDT";
			public static string styLstChgUser = "StickyNotes.styLstChgUser";
			public static string styFK_r_MessageActivityType_GUID = "StickyNotes.styFK_r_MessageActivityType_GUID";
			public static string styFK_r_MessageActivityType = "StickyNotes.styFK_r_MessageActivityType";
			public static string styMsgOriginDT = "StickyNotes.styMsgOriginDT";
			public static string styFK_Assembly_GUID = "StickyNotes.styFK_Assembly_GUID";
			public static string styFK_Assembly = "StickyNotes.styFK_Assembly";
			public static string styFK_Event_GUID = "StickyNotes.styFK_Event_GUID";
			public static string styFK_Event = "StickyNotes.styFK_Event";
			public static string styFK_r_StickyNoteStatus_GUID = "StickyNotes.styFK_r_StickyNoteStatus_GUID";
			public static string styFK_r_StickyNoteStatus = "StickyNotes.styFK_r_StickyNoteStatus";
			public static string styFK_r_MessageActivityPriorityCd_GUID = "StickyNotes.styFK_r_MessageActivityPriorityCd_GUID";
			public static string styFK_r_MessageActivityPriorityCd = "StickyNotes.styFK_r_MessageActivityPriorityCd";
			public static string styMsgCompletionDT = "StickyNotes.styMsgCompletionDT";
			public static string stySender = "StickyNotes.stySender";
			public static string styRecipient = "StickyNotes.styRecipient";
			public static string styBriefDescription = "StickyNotes.styBriefDescription";
			public static string styCompletionComments = "StickyNotes.styCompletionComments";
			public static string styDocumentFileName = "StickyNotes.styDocumentFileName";
			public static string styMessage = "StickyNotes.styMessage";
		}

		public sealed partial class Aliased
		{
			public static string ID = "StickyNotesstyPrimaryKey_GUID";
			public static string styPrimaryKey = "StickyNotesstyPrimaryKey";
			public static string LastModified = "StickyNotesstyLstChgDT";
			public static string styLstChgUser = "StickyNotesstyLstChgUser";
			public static string styFK_r_MessageActivityType_GUID = "StickyNotesstyFK_r_MessageActivityType_GUID";
			public static string styFK_r_MessageActivityType = "StickyNotesstyFK_r_MessageActivityType";
			public static string styMsgOriginDT = "StickyNotesstyMsgOriginDT";
			public static string styFK_Assembly_GUID = "StickyNotesstyFK_Assembly_GUID";
			public static string styFK_Assembly = "StickyNotesstyFK_Assembly";
			public static string styFK_Event_GUID = "StickyNotesstyFK_Event_GUID";
			public static string styFK_Event = "StickyNotesstyFK_Event";
			public static string styFK_r_StickyNoteStatus_GUID = "StickyNotesstyFK_r_StickyNoteStatus_GUID";
			public static string styFK_r_StickyNoteStatus = "StickyNotesstyFK_r_StickyNoteStatus";
			public static string styFK_r_MessageActivityPriorityCd_GUID = "StickyNotesstyFK_r_MessageActivityPriorityCd_GUID";
			public static string styFK_r_MessageActivityPriorityCd = "StickyNotesstyFK_r_MessageActivityPriorityCd";
			public static string styMsgCompletionDT = "StickyNotesstyMsgCompletionDT";
			public static string stySender = "StickyNotesstySender";
			public static string styRecipient = "StickyNotesstyRecipient";
			public static string styBriefDescription = "StickyNotesstyBriefDescription";
			public static string styCompletionComments = "StickyNotesstyCompletionComments";
			public static string styDocumentFileName = "StickyNotesstyDocumentFileName";
			public static string styMessage = "StickyNotesstyMessage";
		}

		public sealed partial class Param
		{
			public static string ID = "@styPrimaryKey_GUID";
			public static string styPrimaryKey = "@styPrimaryKey";
			public static string LastModified = "@styLstChgDT";
			public static string styLstChgUser = "@styLstChgUser";
			public static string styFK_r_MessageActivityType_GUID = "@styFK_r_MessageActivityType_GUID";
			public static string styFK_r_MessageActivityType = "@styFK_r_MessageActivityType";
			public static string styMsgOriginDT = "@styMsgOriginDT";
			public static string styFK_Assembly_GUID = "@styFK_Assembly_GUID";
			public static string styFK_Assembly = "@styFK_Assembly";
			public static string styFK_Event_GUID = "@styFK_Event_GUID";
			public static string styFK_Event = "@styFK_Event";
			public static string styFK_r_StickyNoteStatus_GUID = "@styFK_r_StickyNoteStatus_GUID";
			public static string styFK_r_StickyNoteStatus = "@styFK_r_StickyNoteStatus";
			public static string styFK_r_MessageActivityPriorityCd_GUID = "@styFK_r_MessageActivityPriorityCd_GUID";
			public static string styFK_r_MessageActivityPriorityCd = "@styFK_r_MessageActivityPriorityCd";
			public static string styMsgCompletionDT = "@styMsgCompletionDT";
			public static string stySender = "@stySender";
			public static string styRecipient = "@styRecipient";
			public static string styBriefDescription = "@styBriefDescription";
			public static string styCompletionComments = "@styCompletionComments";
			public static string styDocumentFileName = "@styDocumentFileName";
			public static string styMessage = "@styMessage";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the TemplatePump table in the PumpServicing Database.
    /// </summary>
    public sealed partial class TemplatePumpMap : TableMap
    {
		public const string TABLE_NAME = "TemplatePump";
		
		public static string ID = "tphPrimaryKey_GUID";
		public static string tphPrimaryKey = "tphPrimaryKey";
		public static string tphLanguageCd = "tphLanguageCd";
		public static string LastModified = "tphLstChgDT";
		public static string tphLstChgUser = "tphLstChgUser";
		public static string tphRefCaseDefined = "tphRefCaseDefined";
		public static string tphRefUserDeleted = "tphRefUserDeleted";
		public static string tphTemplateDescription = "tphTemplateDescription";
		public static string tphFK_r_APIPumpGraphics_GUID = "tphFK_r_APIPumpGraphics_GUID";
		public static string tphFK_r_APIPumpGraphics = "tphFK_r_APIPumpGraphics";
		public static string tphFK_r_APISRPTubingSize_GUID = "tphFK_r_APISRPTubingSize_GUID";
		public static string tphFK_r_APISRPTubingSize = "tphFK_r_APISRPTubingSize";
		public static string tphFK_r_APISRPPumpBore_GUID = "tphFK_r_APISRPPumpBore_GUID";
		public static string tphFK_r_APISRPPumpBore = "tphFK_r_APISRPPumpBore";
		public static string tphFK_r_APISRPPumpType_GUID = "tphFK_r_APISRPPumpType_GUID";
		public static string tphFK_r_APISRPPumpType = "tphFK_r_APISRPPumpType";
		public static string tphFK_r_APISRPBarrelType_GUID = "tphFK_r_APISRPBarrelType_GUID";
		public static string tphFK_r_APISRPBarrelType = "tphFK_r_APISRPBarrelType";
		public static string tphFK_r_APISRPSeatAssyLocation_GUID = "tphFK_r_APISRPSeatAssyLocation_GUID";
		public static string tphFK_r_APISRPSeatAssyLocation = "tphFK_r_APISRPSeatAssyLocation";
		public static string tphFK_r_APISRPSeatAssyType_GUID = "tphFK_r_APISRPSeatAssyType_GUID";
		public static string tphFK_r_APISRPSeatAssyType = "tphFK_r_APISRPSeatAssyType";
		public static string tphAPIBarrelLength = "tphAPIBarrelLength";
		public static string tphAPIPlungerLength = "tphAPIPlungerLength";
		public static string tphAPIExtensionCouplingUpperLength = "tphAPIExtensionCouplingUpperLength";
		public static string tphAPIExtensionCouplingLowerLength = "tphAPIExtensionCouplingLowerLength";
		public static string tphFK_r_APISRPExtPumpType_GUID = "tphFK_r_APISRPExtPumpType_GUID";
		public static string tphFK_r_APISRPExtPumpType = "tphFK_r_APISRPExtPumpType";
		public static string tphFK_r_APISRPExtBarrelType_GUID = "tphFK_r_APISRPExtBarrelType_GUID";
		public static string tphFK_r_APISRPExtBarrelType = "tphFK_r_APISRPExtBarrelType";
		public static string tphFK_r_APISRPExtSeatAssyLocation_GUID = "tphFK_r_APISRPExtSeatAssyLocation_GUID";
		public static string tphFK_r_APISRPExtSeatAssyLocation = "tphFK_r_APISRPExtSeatAssyLocation";
		public static string tphFK_r_APISRPExtSeatAssyType_GUID = "tphFK_r_APISRPExtSeatAssyType_GUID";
		public static string tphFK_r_APISRPExtSeatAssyType = "tphFK_r_APISRPExtSeatAssyType";
		public static string tphFK_r_APISRPExtSand_GUID = "tphFK_r_APISRPExtSand_GUID";
		public static string tphFK_r_APISRPExtSand = "tphFK_r_APISRPExtSand";
		public static string tphFK_r_APISRPExtBblAcc_GUID = "tphFK_r_APISRPExtBblAcc_GUID";
		public static string tphFK_r_APISRPExtBblAcc = "tphFK_r_APISRPExtBblAcc";
		public static string tphFK_r_APISRPExtPlgAcc_GUID = "tphFK_r_APISRPExtPlgAcc_GUID";
		public static string tphFK_r_APISRPExtPlgAcc = "tphFK_r_APISRPExtPlgAcc";
		public static string tphFK_r_APISRPExtPlgType_GUID = "tphFK_r_APISRPExtPlgType_GUID";
		public static string tphFK_r_APISRPExtPlgType = "tphFK_r_APISRPExtPlgType";
		public static string tphFK_r_APISRPExtPlgPin_GUID = "tphFK_r_APISRPExtPlgPin_GUID";
		public static string tphFK_r_APISRPExtPlgPin = "tphFK_r_APISRPExtPlgPin";
		public static string tphFK_r_APISRPExtSV_GUID = "tphFK_r_APISRPExtSV_GUID";
		public static string tphFK_r_APISRPExtSV = "tphFK_r_APISRPExtSV";
		public static string tphFK_r_APISRPExtSVCage_GUID = "tphFK_r_APISRPExtSVCage_GUID";
		public static string tphFK_r_APISRPExtSVCage = "tphFK_r_APISRPExtSVCage";
		public static string tphFK_r_APISRPExtTV_GUID = "tphFK_r_APISRPExtTV_GUID";
		public static string tphFK_r_APISRPExtTV = "tphFK_r_APISRPExtTV";
		public static string tphFK_r_APISRPExtTVCage_GUID = "tphFK_r_APISRPExtTVCage_GUID";
		public static string tphFK_r_APISRPExtTVCage = "tphFK_r_APISRPExtTVCage";
		public static string tphFK_r_APISRPExtTVStPlg_GUID = "tphFK_r_APISRPExtTVStPlg_GUID";
		public static string tphFK_r_APISRPExtTVStPlg = "tphFK_r_APISRPExtTVStPlg";
		public static string tphFK_r_APISRPExtVRod_GUID = "tphFK_r_APISRPExtVRod_GUID";
		public static string tphFK_r_APISRPExtVRod = "tphFK_r_APISRPExtVRod";
		public static string tphFK_r_APISRPExtWiper_GUID = "tphFK_r_APISRPExtWiper_GUID";
		public static string tphFK_r_APISRPExtWiper = "tphFK_r_APISRPExtWiper";
		public static string tphlBblPlgAvgClearance = "tphlBblPlgAvgClearance";
		public static string tphMaxSL = "tphMaxSL";
		public static string tphHelpText = "tphHelpText";
		
		public sealed partial class Prefixed
		{
			public static string ID = "TemplatePump.tphPrimaryKey_GUID";
			public static string tphPrimaryKey = "TemplatePump.tphPrimaryKey";
			public static string tphLanguageCd = "TemplatePump.tphLanguageCd";
			public static string LastModified = "TemplatePump.tphLstChgDT";
			public static string tphLstChgUser = "TemplatePump.tphLstChgUser";
			public static string tphRefCaseDefined = "TemplatePump.tphRefCaseDefined";
			public static string tphRefUserDeleted = "TemplatePump.tphRefUserDeleted";
			public static string tphTemplateDescription = "TemplatePump.tphTemplateDescription";
			public static string tphFK_r_APIPumpGraphics_GUID = "TemplatePump.tphFK_r_APIPumpGraphics_GUID";
			public static string tphFK_r_APIPumpGraphics = "TemplatePump.tphFK_r_APIPumpGraphics";
			public static string tphFK_r_APISRPTubingSize_GUID = "TemplatePump.tphFK_r_APISRPTubingSize_GUID";
			public static string tphFK_r_APISRPTubingSize = "TemplatePump.tphFK_r_APISRPTubingSize";
			public static string tphFK_r_APISRPPumpBore_GUID = "TemplatePump.tphFK_r_APISRPPumpBore_GUID";
			public static string tphFK_r_APISRPPumpBore = "TemplatePump.tphFK_r_APISRPPumpBore";
			public static string tphFK_r_APISRPPumpType_GUID = "TemplatePump.tphFK_r_APISRPPumpType_GUID";
			public static string tphFK_r_APISRPPumpType = "TemplatePump.tphFK_r_APISRPPumpType";
			public static string tphFK_r_APISRPBarrelType_GUID = "TemplatePump.tphFK_r_APISRPBarrelType_GUID";
			public static string tphFK_r_APISRPBarrelType = "TemplatePump.tphFK_r_APISRPBarrelType";
			public static string tphFK_r_APISRPSeatAssyLocation_GUID = "TemplatePump.tphFK_r_APISRPSeatAssyLocation_GUID";
			public static string tphFK_r_APISRPSeatAssyLocation = "TemplatePump.tphFK_r_APISRPSeatAssyLocation";
			public static string tphFK_r_APISRPSeatAssyType_GUID = "TemplatePump.tphFK_r_APISRPSeatAssyType_GUID";
			public static string tphFK_r_APISRPSeatAssyType = "TemplatePump.tphFK_r_APISRPSeatAssyType";
			public static string tphAPIBarrelLength = "TemplatePump.tphAPIBarrelLength";
			public static string tphAPIPlungerLength = "TemplatePump.tphAPIPlungerLength";
			public static string tphAPIExtensionCouplingUpperLength = "TemplatePump.tphAPIExtensionCouplingUpperLength";
			public static string tphAPIExtensionCouplingLowerLength = "TemplatePump.tphAPIExtensionCouplingLowerLength";
			public static string tphFK_r_APISRPExtPumpType_GUID = "TemplatePump.tphFK_r_APISRPExtPumpType_GUID";
			public static string tphFK_r_APISRPExtPumpType = "TemplatePump.tphFK_r_APISRPExtPumpType";
			public static string tphFK_r_APISRPExtBarrelType_GUID = "TemplatePump.tphFK_r_APISRPExtBarrelType_GUID";
			public static string tphFK_r_APISRPExtBarrelType = "TemplatePump.tphFK_r_APISRPExtBarrelType";
			public static string tphFK_r_APISRPExtSeatAssyLocation_GUID = "TemplatePump.tphFK_r_APISRPExtSeatAssyLocation_GUID";
			public static string tphFK_r_APISRPExtSeatAssyLocation = "TemplatePump.tphFK_r_APISRPExtSeatAssyLocation";
			public static string tphFK_r_APISRPExtSeatAssyType_GUID = "TemplatePump.tphFK_r_APISRPExtSeatAssyType_GUID";
			public static string tphFK_r_APISRPExtSeatAssyType = "TemplatePump.tphFK_r_APISRPExtSeatAssyType";
			public static string tphFK_r_APISRPExtSand_GUID = "TemplatePump.tphFK_r_APISRPExtSand_GUID";
			public static string tphFK_r_APISRPExtSand = "TemplatePump.tphFK_r_APISRPExtSand";
			public static string tphFK_r_APISRPExtBblAcc_GUID = "TemplatePump.tphFK_r_APISRPExtBblAcc_GUID";
			public static string tphFK_r_APISRPExtBblAcc = "TemplatePump.tphFK_r_APISRPExtBblAcc";
			public static string tphFK_r_APISRPExtPlgAcc_GUID = "TemplatePump.tphFK_r_APISRPExtPlgAcc_GUID";
			public static string tphFK_r_APISRPExtPlgAcc = "TemplatePump.tphFK_r_APISRPExtPlgAcc";
			public static string tphFK_r_APISRPExtPlgType_GUID = "TemplatePump.tphFK_r_APISRPExtPlgType_GUID";
			public static string tphFK_r_APISRPExtPlgType = "TemplatePump.tphFK_r_APISRPExtPlgType";
			public static string tphFK_r_APISRPExtPlgPin_GUID = "TemplatePump.tphFK_r_APISRPExtPlgPin_GUID";
			public static string tphFK_r_APISRPExtPlgPin = "TemplatePump.tphFK_r_APISRPExtPlgPin";
			public static string tphFK_r_APISRPExtSV_GUID = "TemplatePump.tphFK_r_APISRPExtSV_GUID";
			public static string tphFK_r_APISRPExtSV = "TemplatePump.tphFK_r_APISRPExtSV";
			public static string tphFK_r_APISRPExtSVCage_GUID = "TemplatePump.tphFK_r_APISRPExtSVCage_GUID";
			public static string tphFK_r_APISRPExtSVCage = "TemplatePump.tphFK_r_APISRPExtSVCage";
			public static string tphFK_r_APISRPExtTV_GUID = "TemplatePump.tphFK_r_APISRPExtTV_GUID";
			public static string tphFK_r_APISRPExtTV = "TemplatePump.tphFK_r_APISRPExtTV";
			public static string tphFK_r_APISRPExtTVCage_GUID = "TemplatePump.tphFK_r_APISRPExtTVCage_GUID";
			public static string tphFK_r_APISRPExtTVCage = "TemplatePump.tphFK_r_APISRPExtTVCage";
			public static string tphFK_r_APISRPExtTVStPlg_GUID = "TemplatePump.tphFK_r_APISRPExtTVStPlg_GUID";
			public static string tphFK_r_APISRPExtTVStPlg = "TemplatePump.tphFK_r_APISRPExtTVStPlg";
			public static string tphFK_r_APISRPExtVRod_GUID = "TemplatePump.tphFK_r_APISRPExtVRod_GUID";
			public static string tphFK_r_APISRPExtVRod = "TemplatePump.tphFK_r_APISRPExtVRod";
			public static string tphFK_r_APISRPExtWiper_GUID = "TemplatePump.tphFK_r_APISRPExtWiper_GUID";
			public static string tphFK_r_APISRPExtWiper = "TemplatePump.tphFK_r_APISRPExtWiper";
			public static string tphlBblPlgAvgClearance = "TemplatePump.tphlBblPlgAvgClearance";
			public static string tphMaxSL = "TemplatePump.tphMaxSL";
			public static string tphHelpText = "TemplatePump.tphHelpText";
		}

		public sealed partial class Aliased
		{
			public static string ID = "TemplatePumptphPrimaryKey_GUID";
			public static string tphPrimaryKey = "TemplatePumptphPrimaryKey";
			public static string tphLanguageCd = "TemplatePumptphLanguageCd";
			public static string LastModified = "TemplatePumptphLstChgDT";
			public static string tphLstChgUser = "TemplatePumptphLstChgUser";
			public static string tphRefCaseDefined = "TemplatePumptphRefCaseDefined";
			public static string tphRefUserDeleted = "TemplatePumptphRefUserDeleted";
			public static string tphTemplateDescription = "TemplatePumptphTemplateDescription";
			public static string tphFK_r_APIPumpGraphics_GUID = "TemplatePumptphFK_r_APIPumpGraphics_GUID";
			public static string tphFK_r_APIPumpGraphics = "TemplatePumptphFK_r_APIPumpGraphics";
			public static string tphFK_r_APISRPTubingSize_GUID = "TemplatePumptphFK_r_APISRPTubingSize_GUID";
			public static string tphFK_r_APISRPTubingSize = "TemplatePumptphFK_r_APISRPTubingSize";
			public static string tphFK_r_APISRPPumpBore_GUID = "TemplatePumptphFK_r_APISRPPumpBore_GUID";
			public static string tphFK_r_APISRPPumpBore = "TemplatePumptphFK_r_APISRPPumpBore";
			public static string tphFK_r_APISRPPumpType_GUID = "TemplatePumptphFK_r_APISRPPumpType_GUID";
			public static string tphFK_r_APISRPPumpType = "TemplatePumptphFK_r_APISRPPumpType";
			public static string tphFK_r_APISRPBarrelType_GUID = "TemplatePumptphFK_r_APISRPBarrelType_GUID";
			public static string tphFK_r_APISRPBarrelType = "TemplatePumptphFK_r_APISRPBarrelType";
			public static string tphFK_r_APISRPSeatAssyLocation_GUID = "TemplatePumptphFK_r_APISRPSeatAssyLocation_GUID";
			public static string tphFK_r_APISRPSeatAssyLocation = "TemplatePumptphFK_r_APISRPSeatAssyLocation";
			public static string tphFK_r_APISRPSeatAssyType_GUID = "TemplatePumptphFK_r_APISRPSeatAssyType_GUID";
			public static string tphFK_r_APISRPSeatAssyType = "TemplatePumptphFK_r_APISRPSeatAssyType";
			public static string tphAPIBarrelLength = "TemplatePumptphAPIBarrelLength";
			public static string tphAPIPlungerLength = "TemplatePumptphAPIPlungerLength";
			public static string tphAPIExtensionCouplingUpperLength = "TemplatePumptphAPIExtensionCouplingUpperLength";
			public static string tphAPIExtensionCouplingLowerLength = "TemplatePumptphAPIExtensionCouplingLowerLength";
			public static string tphFK_r_APISRPExtPumpType_GUID = "TemplatePumptphFK_r_APISRPExtPumpType_GUID";
			public static string tphFK_r_APISRPExtPumpType = "TemplatePumptphFK_r_APISRPExtPumpType";
			public static string tphFK_r_APISRPExtBarrelType_GUID = "TemplatePumptphFK_r_APISRPExtBarrelType_GUID";
			public static string tphFK_r_APISRPExtBarrelType = "TemplatePumptphFK_r_APISRPExtBarrelType";
			public static string tphFK_r_APISRPExtSeatAssyLocation_GUID = "TemplatePumptphFK_r_APISRPExtSeatAssyLocation_GUID";
			public static string tphFK_r_APISRPExtSeatAssyLocation = "TemplatePumptphFK_r_APISRPExtSeatAssyLocation";
			public static string tphFK_r_APISRPExtSeatAssyType_GUID = "TemplatePumptphFK_r_APISRPExtSeatAssyType_GUID";
			public static string tphFK_r_APISRPExtSeatAssyType = "TemplatePumptphFK_r_APISRPExtSeatAssyType";
			public static string tphFK_r_APISRPExtSand_GUID = "TemplatePumptphFK_r_APISRPExtSand_GUID";
			public static string tphFK_r_APISRPExtSand = "TemplatePumptphFK_r_APISRPExtSand";
			public static string tphFK_r_APISRPExtBblAcc_GUID = "TemplatePumptphFK_r_APISRPExtBblAcc_GUID";
			public static string tphFK_r_APISRPExtBblAcc = "TemplatePumptphFK_r_APISRPExtBblAcc";
			public static string tphFK_r_APISRPExtPlgAcc_GUID = "TemplatePumptphFK_r_APISRPExtPlgAcc_GUID";
			public static string tphFK_r_APISRPExtPlgAcc = "TemplatePumptphFK_r_APISRPExtPlgAcc";
			public static string tphFK_r_APISRPExtPlgType_GUID = "TemplatePumptphFK_r_APISRPExtPlgType_GUID";
			public static string tphFK_r_APISRPExtPlgType = "TemplatePumptphFK_r_APISRPExtPlgType";
			public static string tphFK_r_APISRPExtPlgPin_GUID = "TemplatePumptphFK_r_APISRPExtPlgPin_GUID";
			public static string tphFK_r_APISRPExtPlgPin = "TemplatePumptphFK_r_APISRPExtPlgPin";
			public static string tphFK_r_APISRPExtSV_GUID = "TemplatePumptphFK_r_APISRPExtSV_GUID";
			public static string tphFK_r_APISRPExtSV = "TemplatePumptphFK_r_APISRPExtSV";
			public static string tphFK_r_APISRPExtSVCage_GUID = "TemplatePumptphFK_r_APISRPExtSVCage_GUID";
			public static string tphFK_r_APISRPExtSVCage = "TemplatePumptphFK_r_APISRPExtSVCage";
			public static string tphFK_r_APISRPExtTV_GUID = "TemplatePumptphFK_r_APISRPExtTV_GUID";
			public static string tphFK_r_APISRPExtTV = "TemplatePumptphFK_r_APISRPExtTV";
			public static string tphFK_r_APISRPExtTVCage_GUID = "TemplatePumptphFK_r_APISRPExtTVCage_GUID";
			public static string tphFK_r_APISRPExtTVCage = "TemplatePumptphFK_r_APISRPExtTVCage";
			public static string tphFK_r_APISRPExtTVStPlg_GUID = "TemplatePumptphFK_r_APISRPExtTVStPlg_GUID";
			public static string tphFK_r_APISRPExtTVStPlg = "TemplatePumptphFK_r_APISRPExtTVStPlg";
			public static string tphFK_r_APISRPExtVRod_GUID = "TemplatePumptphFK_r_APISRPExtVRod_GUID";
			public static string tphFK_r_APISRPExtVRod = "TemplatePumptphFK_r_APISRPExtVRod";
			public static string tphFK_r_APISRPExtWiper_GUID = "TemplatePumptphFK_r_APISRPExtWiper_GUID";
			public static string tphFK_r_APISRPExtWiper = "TemplatePumptphFK_r_APISRPExtWiper";
			public static string tphlBblPlgAvgClearance = "TemplatePumptphlBblPlgAvgClearance";
			public static string tphMaxSL = "TemplatePumptphMaxSL";
			public static string tphHelpText = "TemplatePumptphHelpText";
		}

		public sealed partial class Param
		{
			public static string ID = "@tphPrimaryKey_GUID";
			public static string tphPrimaryKey = "@tphPrimaryKey";
			public static string tphLanguageCd = "@tphLanguageCd";
			public static string LastModified = "@tphLstChgDT";
			public static string tphLstChgUser = "@tphLstChgUser";
			public static string tphRefCaseDefined = "@tphRefCaseDefined";
			public static string tphRefUserDeleted = "@tphRefUserDeleted";
			public static string tphTemplateDescription = "@tphTemplateDescription";
			public static string tphFK_r_APIPumpGraphics_GUID = "@tphFK_r_APIPumpGraphics_GUID";
			public static string tphFK_r_APIPumpGraphics = "@tphFK_r_APIPumpGraphics";
			public static string tphFK_r_APISRPTubingSize_GUID = "@tphFK_r_APISRPTubingSize_GUID";
			public static string tphFK_r_APISRPTubingSize = "@tphFK_r_APISRPTubingSize";
			public static string tphFK_r_APISRPPumpBore_GUID = "@tphFK_r_APISRPPumpBore_GUID";
			public static string tphFK_r_APISRPPumpBore = "@tphFK_r_APISRPPumpBore";
			public static string tphFK_r_APISRPPumpType_GUID = "@tphFK_r_APISRPPumpType_GUID";
			public static string tphFK_r_APISRPPumpType = "@tphFK_r_APISRPPumpType";
			public static string tphFK_r_APISRPBarrelType_GUID = "@tphFK_r_APISRPBarrelType_GUID";
			public static string tphFK_r_APISRPBarrelType = "@tphFK_r_APISRPBarrelType";
			public static string tphFK_r_APISRPSeatAssyLocation_GUID = "@tphFK_r_APISRPSeatAssyLocation_GUID";
			public static string tphFK_r_APISRPSeatAssyLocation = "@tphFK_r_APISRPSeatAssyLocation";
			public static string tphFK_r_APISRPSeatAssyType_GUID = "@tphFK_r_APISRPSeatAssyType_GUID";
			public static string tphFK_r_APISRPSeatAssyType = "@tphFK_r_APISRPSeatAssyType";
			public static string tphAPIBarrelLength = "@tphAPIBarrelLength";
			public static string tphAPIPlungerLength = "@tphAPIPlungerLength";
			public static string tphAPIExtensionCouplingUpperLength = "@tphAPIExtensionCouplingUpperLength";
			public static string tphAPIExtensionCouplingLowerLength = "@tphAPIExtensionCouplingLowerLength";
			public static string tphFK_r_APISRPExtPumpType_GUID = "@tphFK_r_APISRPExtPumpType_GUID";
			public static string tphFK_r_APISRPExtPumpType = "@tphFK_r_APISRPExtPumpType";
			public static string tphFK_r_APISRPExtBarrelType_GUID = "@tphFK_r_APISRPExtBarrelType_GUID";
			public static string tphFK_r_APISRPExtBarrelType = "@tphFK_r_APISRPExtBarrelType";
			public static string tphFK_r_APISRPExtSeatAssyLocation_GUID = "@tphFK_r_APISRPExtSeatAssyLocation_GUID";
			public static string tphFK_r_APISRPExtSeatAssyLocation = "@tphFK_r_APISRPExtSeatAssyLocation";
			public static string tphFK_r_APISRPExtSeatAssyType_GUID = "@tphFK_r_APISRPExtSeatAssyType_GUID";
			public static string tphFK_r_APISRPExtSeatAssyType = "@tphFK_r_APISRPExtSeatAssyType";
			public static string tphFK_r_APISRPExtSand_GUID = "@tphFK_r_APISRPExtSand_GUID";
			public static string tphFK_r_APISRPExtSand = "@tphFK_r_APISRPExtSand";
			public static string tphFK_r_APISRPExtBblAcc_GUID = "@tphFK_r_APISRPExtBblAcc_GUID";
			public static string tphFK_r_APISRPExtBblAcc = "@tphFK_r_APISRPExtBblAcc";
			public static string tphFK_r_APISRPExtPlgAcc_GUID = "@tphFK_r_APISRPExtPlgAcc_GUID";
			public static string tphFK_r_APISRPExtPlgAcc = "@tphFK_r_APISRPExtPlgAcc";
			public static string tphFK_r_APISRPExtPlgType_GUID = "@tphFK_r_APISRPExtPlgType_GUID";
			public static string tphFK_r_APISRPExtPlgType = "@tphFK_r_APISRPExtPlgType";
			public static string tphFK_r_APISRPExtPlgPin_GUID = "@tphFK_r_APISRPExtPlgPin_GUID";
			public static string tphFK_r_APISRPExtPlgPin = "@tphFK_r_APISRPExtPlgPin";
			public static string tphFK_r_APISRPExtSV_GUID = "@tphFK_r_APISRPExtSV_GUID";
			public static string tphFK_r_APISRPExtSV = "@tphFK_r_APISRPExtSV";
			public static string tphFK_r_APISRPExtSVCage_GUID = "@tphFK_r_APISRPExtSVCage_GUID";
			public static string tphFK_r_APISRPExtSVCage = "@tphFK_r_APISRPExtSVCage";
			public static string tphFK_r_APISRPExtTV_GUID = "@tphFK_r_APISRPExtTV_GUID";
			public static string tphFK_r_APISRPExtTV = "@tphFK_r_APISRPExtTV";
			public static string tphFK_r_APISRPExtTVCage_GUID = "@tphFK_r_APISRPExtTVCage_GUID";
			public static string tphFK_r_APISRPExtTVCage = "@tphFK_r_APISRPExtTVCage";
			public static string tphFK_r_APISRPExtTVStPlg_GUID = "@tphFK_r_APISRPExtTVStPlg_GUID";
			public static string tphFK_r_APISRPExtTVStPlg = "@tphFK_r_APISRPExtTVStPlg";
			public static string tphFK_r_APISRPExtVRod_GUID = "@tphFK_r_APISRPExtVRod_GUID";
			public static string tphFK_r_APISRPExtVRod = "@tphFK_r_APISRPExtVRod";
			public static string tphFK_r_APISRPExtWiper_GUID = "@tphFK_r_APISRPExtWiper_GUID";
			public static string tphFK_r_APISRPExtWiper = "@tphFK_r_APISRPExtWiper";
			public static string tphlBblPlgAvgClearance = "@tphlBblPlgAvgClearance";
			public static string tphMaxSL = "@tphMaxSL";
			public static string tphHelpText = "@tphHelpText";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the TemplatePumpDetail table in the PumpServicing Database.
    /// </summary>
    public sealed partial class TemplatePumpDetailMap : TableMap
    {
		public const string TABLE_NAME = "TemplatePumpDetail";
		
		public static string ID = "tpdPrimaryKey_GUID";
		public static string tpdPrimaryKey = "tpdPrimaryKey";
		public static string LastModified = "tpdLstChgDT";
		public static string tpdLstChgUser = "tpdLstChgUser";
		public static string tpdRefCaseDefined = "tpdRefCaseDefined";
		public static string tpdRefUserDeleted = "tpdRefUserDeleted";
		public static string tpdFK_TemplatePump_GUID = "tpdFK_TemplatePump_GUID";
		public static string tpdFK_TemplatePump = "tpdFK_TemplatePump";
		public static string tpdFK_TemplateSubAssembly_GUID = "tpdFK_TemplateSubAssembly_GUID";
		public static string tpdFK_TemplateSubAssembly = "tpdFK_TemplateSubAssembly";
		
		public sealed partial class Prefixed
		{
			public static string ID = "TemplatePumpDetail.tpdPrimaryKey_GUID";
			public static string tpdPrimaryKey = "TemplatePumpDetail.tpdPrimaryKey";
			public static string LastModified = "TemplatePumpDetail.tpdLstChgDT";
			public static string tpdLstChgUser = "TemplatePumpDetail.tpdLstChgUser";
			public static string tpdRefCaseDefined = "TemplatePumpDetail.tpdRefCaseDefined";
			public static string tpdRefUserDeleted = "TemplatePumpDetail.tpdRefUserDeleted";
			public static string tpdFK_TemplatePump_GUID = "TemplatePumpDetail.tpdFK_TemplatePump_GUID";
			public static string tpdFK_TemplatePump = "TemplatePumpDetail.tpdFK_TemplatePump";
			public static string tpdFK_TemplateSubAssembly_GUID = "TemplatePumpDetail.tpdFK_TemplateSubAssembly_GUID";
			public static string tpdFK_TemplateSubAssembly = "TemplatePumpDetail.tpdFK_TemplateSubAssembly";
		}

		public sealed partial class Aliased
		{
			public static string ID = "TemplatePumpDetailtpdPrimaryKey_GUID";
			public static string tpdPrimaryKey = "TemplatePumpDetailtpdPrimaryKey";
			public static string LastModified = "TemplatePumpDetailtpdLstChgDT";
			public static string tpdLstChgUser = "TemplatePumpDetailtpdLstChgUser";
			public static string tpdRefCaseDefined = "TemplatePumpDetailtpdRefCaseDefined";
			public static string tpdRefUserDeleted = "TemplatePumpDetailtpdRefUserDeleted";
			public static string tpdFK_TemplatePump_GUID = "TemplatePumpDetailtpdFK_TemplatePump_GUID";
			public static string tpdFK_TemplatePump = "TemplatePumpDetailtpdFK_TemplatePump";
			public static string tpdFK_TemplateSubAssembly_GUID = "TemplatePumpDetailtpdFK_TemplateSubAssembly_GUID";
			public static string tpdFK_TemplateSubAssembly = "TemplatePumpDetailtpdFK_TemplateSubAssembly";
		}

		public sealed partial class Param
		{
			public static string ID = "@tpdPrimaryKey_GUID";
			public static string tpdPrimaryKey = "@tpdPrimaryKey";
			public static string LastModified = "@tpdLstChgDT";
			public static string tpdLstChgUser = "@tpdLstChgUser";
			public static string tpdRefCaseDefined = "@tpdRefCaseDefined";
			public static string tpdRefUserDeleted = "@tpdRefUserDeleted";
			public static string tpdFK_TemplatePump_GUID = "@tpdFK_TemplatePump_GUID";
			public static string tpdFK_TemplatePump = "@tpdFK_TemplatePump";
			public static string tpdFK_TemplateSubAssembly_GUID = "@tpdFK_TemplateSubAssembly_GUID";
			public static string tpdFK_TemplateSubAssembly = "@tpdFK_TemplateSubAssembly";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the TemplateSubAssembly table in the PumpServicing Database.
    /// </summary>
    public sealed partial class TemplateSubAssemblyMap : TableMap
    {
		public const string TABLE_NAME = "TemplateSubAssembly";
		
		public static string ID = "tahPrimaryKey_GUID";
		public static string tahPrimaryKey = "tahPrimaryKey";
		public static string tahLanguageCd = "tahLanguageCd";
		public static string LastModified = "tahLstChgDT";
		public static string tahLstChgUser = "tahLstChgUser";
		public static string tahRefCaseDefined = "tahRefCaseDefined";
		public static string tahRefUserDeleted = "tahRefUserDeleted";
		public static string tahTemplateAssemblyDescription = "tahTemplateAssemblyDescription";
		public static string tahFK_r_APIPumpGraphic_GUID = "tahFK_r_APIPumpGraphic_GUID";
		public static string tahFK_r_APIPumpGraphic = "tahFK_r_APIPumpGraphic";
		public static string tahFK_r_ComponentCategory_GUID = "tahFK_r_ComponentCategory_GUID";
		public static string tahFK_r_ComponentCategory = "tahFK_r_ComponentCategory";
		public static string tahHelpText = "tahHelpText";
		
		public sealed partial class Prefixed
		{
			public static string ID = "TemplateSubAssembly.tahPrimaryKey_GUID";
			public static string tahPrimaryKey = "TemplateSubAssembly.tahPrimaryKey";
			public static string tahLanguageCd = "TemplateSubAssembly.tahLanguageCd";
			public static string LastModified = "TemplateSubAssembly.tahLstChgDT";
			public static string tahLstChgUser = "TemplateSubAssembly.tahLstChgUser";
			public static string tahRefCaseDefined = "TemplateSubAssembly.tahRefCaseDefined";
			public static string tahRefUserDeleted = "TemplateSubAssembly.tahRefUserDeleted";
			public static string tahTemplateAssemblyDescription = "TemplateSubAssembly.tahTemplateAssemblyDescription";
			public static string tahFK_r_APIPumpGraphic_GUID = "TemplateSubAssembly.tahFK_r_APIPumpGraphic_GUID";
			public static string tahFK_r_APIPumpGraphic = "TemplateSubAssembly.tahFK_r_APIPumpGraphic";
			public static string tahFK_r_ComponentCategory_GUID = "TemplateSubAssembly.tahFK_r_ComponentCategory_GUID";
			public static string tahFK_r_ComponentCategory = "TemplateSubAssembly.tahFK_r_ComponentCategory";
			public static string tahHelpText = "TemplateSubAssembly.tahHelpText";
		}

		public sealed partial class Aliased
		{
			public static string ID = "TemplateSubAssemblytahPrimaryKey_GUID";
			public static string tahPrimaryKey = "TemplateSubAssemblytahPrimaryKey";
			public static string tahLanguageCd = "TemplateSubAssemblytahLanguageCd";
			public static string LastModified = "TemplateSubAssemblytahLstChgDT";
			public static string tahLstChgUser = "TemplateSubAssemblytahLstChgUser";
			public static string tahRefCaseDefined = "TemplateSubAssemblytahRefCaseDefined";
			public static string tahRefUserDeleted = "TemplateSubAssemblytahRefUserDeleted";
			public static string tahTemplateAssemblyDescription = "TemplateSubAssemblytahTemplateAssemblyDescription";
			public static string tahFK_r_APIPumpGraphic_GUID = "TemplateSubAssemblytahFK_r_APIPumpGraphic_GUID";
			public static string tahFK_r_APIPumpGraphic = "TemplateSubAssemblytahFK_r_APIPumpGraphic";
			public static string tahFK_r_ComponentCategory_GUID = "TemplateSubAssemblytahFK_r_ComponentCategory_GUID";
			public static string tahFK_r_ComponentCategory = "TemplateSubAssemblytahFK_r_ComponentCategory";
			public static string tahHelpText = "TemplateSubAssemblytahHelpText";
		}

		public sealed partial class Param
		{
			public static string ID = "@tahPrimaryKey_GUID";
			public static string tahPrimaryKey = "@tahPrimaryKey";
			public static string tahLanguageCd = "@tahLanguageCd";
			public static string LastModified = "@tahLstChgDT";
			public static string tahLstChgUser = "@tahLstChgUser";
			public static string tahRefCaseDefined = "@tahRefCaseDefined";
			public static string tahRefUserDeleted = "@tahRefUserDeleted";
			public static string tahTemplateAssemblyDescription = "@tahTemplateAssemblyDescription";
			public static string tahFK_r_APIPumpGraphic_GUID = "@tahFK_r_APIPumpGraphic_GUID";
			public static string tahFK_r_APIPumpGraphic = "@tahFK_r_APIPumpGraphic";
			public static string tahFK_r_ComponentCategory_GUID = "@tahFK_r_ComponentCategory_GUID";
			public static string tahFK_r_ComponentCategory = "@tahFK_r_ComponentCategory";
			public static string tahHelpText = "@tahHelpText";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the TemplateSubAssemblyDetail table in the PumpServicing Database.
    /// </summary>
    public sealed partial class TemplateSubAssemblyDetailMap : TableMap
    {
		public const string TABLE_NAME = "TemplateSubAssemblyDetail";
		
		public static string ID = "tadPrimaryKey_GUID";
		public static string tadPrimaryKey = "tadPrimaryKey";
		public static string LastModified = "tadLstChgDT";
		public static string tadLstChgUser = "tadLstChgUser";
		public static string tadRefCaseDefined = "tadRefCaseDefined";
		public static string tadRefUserDeleted = "tadRefUserDeleted";
		public static string tadFK_TemplateSubAssembly_GUID = "tadFK_TemplateSubAssembly_GUID";
		public static string tadFK_TemplateSubAssembly = "tadFK_TemplateSubAssembly";
		public static string tadAssemblyOrder = "tadAssemblyOrder";
		public static string tadFK_r_PartType_GUID = "tadFK_r_PartType_GUID";
		public static string tadFK_r_PartType = "tadFK_r_PartType";
		public static string tadFK_r_CatalogItem_GUID = "tadFK_r_CatalogItem_GUID";
		public static string tadFK_r_CatalogItem = "tadFK_r_CatalogItem";
		public static string tadFK_r_ComponentGrouping_GUID = "tadFK_r_ComponentGrouping_GUID";
		public static string tadFK_r_ComponentGrouping = "tadFK_r_ComponentGrouping";
		public static string tadQuantity = "tadQuantity";
		
		public sealed partial class Prefixed
		{
			public static string ID = "TemplateSubAssemblyDetail.tadPrimaryKey_GUID";
			public static string tadPrimaryKey = "TemplateSubAssemblyDetail.tadPrimaryKey";
			public static string LastModified = "TemplateSubAssemblyDetail.tadLstChgDT";
			public static string tadLstChgUser = "TemplateSubAssemblyDetail.tadLstChgUser";
			public static string tadRefCaseDefined = "TemplateSubAssemblyDetail.tadRefCaseDefined";
			public static string tadRefUserDeleted = "TemplateSubAssemblyDetail.tadRefUserDeleted";
			public static string tadFK_TemplateSubAssembly_GUID = "TemplateSubAssemblyDetail.tadFK_TemplateSubAssembly_GUID";
			public static string tadFK_TemplateSubAssembly = "TemplateSubAssemblyDetail.tadFK_TemplateSubAssembly";
			public static string tadAssemblyOrder = "TemplateSubAssemblyDetail.tadAssemblyOrder";
			public static string tadFK_r_PartType_GUID = "TemplateSubAssemblyDetail.tadFK_r_PartType_GUID";
			public static string tadFK_r_PartType = "TemplateSubAssemblyDetail.tadFK_r_PartType";
			public static string tadFK_r_CatalogItem_GUID = "TemplateSubAssemblyDetail.tadFK_r_CatalogItem_GUID";
			public static string tadFK_r_CatalogItem = "TemplateSubAssemblyDetail.tadFK_r_CatalogItem";
			public static string tadFK_r_ComponentGrouping_GUID = "TemplateSubAssemblyDetail.tadFK_r_ComponentGrouping_GUID";
			public static string tadFK_r_ComponentGrouping = "TemplateSubAssemblyDetail.tadFK_r_ComponentGrouping";
			public static string tadQuantity = "TemplateSubAssemblyDetail.tadQuantity";
		}

		public sealed partial class Aliased
		{
			public static string ID = "TemplateSubAssemblyDetailtadPrimaryKey_GUID";
			public static string tadPrimaryKey = "TemplateSubAssemblyDetailtadPrimaryKey";
			public static string LastModified = "TemplateSubAssemblyDetailtadLstChgDT";
			public static string tadLstChgUser = "TemplateSubAssemblyDetailtadLstChgUser";
			public static string tadRefCaseDefined = "TemplateSubAssemblyDetailtadRefCaseDefined";
			public static string tadRefUserDeleted = "TemplateSubAssemblyDetailtadRefUserDeleted";
			public static string tadFK_TemplateSubAssembly_GUID = "TemplateSubAssemblyDetailtadFK_TemplateSubAssembly_GUID";
			public static string tadFK_TemplateSubAssembly = "TemplateSubAssemblyDetailtadFK_TemplateSubAssembly";
			public static string tadAssemblyOrder = "TemplateSubAssemblyDetailtadAssemblyOrder";
			public static string tadFK_r_PartType_GUID = "TemplateSubAssemblyDetailtadFK_r_PartType_GUID";
			public static string tadFK_r_PartType = "TemplateSubAssemblyDetailtadFK_r_PartType";
			public static string tadFK_r_CatalogItem_GUID = "TemplateSubAssemblyDetailtadFK_r_CatalogItem_GUID";
			public static string tadFK_r_CatalogItem = "TemplateSubAssemblyDetailtadFK_r_CatalogItem";
			public static string tadFK_r_ComponentGrouping_GUID = "TemplateSubAssemblyDetailtadFK_r_ComponentGrouping_GUID";
			public static string tadFK_r_ComponentGrouping = "TemplateSubAssemblyDetailtadFK_r_ComponentGrouping";
			public static string tadQuantity = "TemplateSubAssemblyDetailtadQuantity";
		}

		public sealed partial class Param
		{
			public static string ID = "@tadPrimaryKey_GUID";
			public static string tadPrimaryKey = "@tadPrimaryKey";
			public static string LastModified = "@tadLstChgDT";
			public static string tadLstChgUser = "@tadLstChgUser";
			public static string tadRefCaseDefined = "@tadRefCaseDefined";
			public static string tadRefUserDeleted = "@tadRefUserDeleted";
			public static string tadFK_TemplateSubAssembly_GUID = "@tadFK_TemplateSubAssembly_GUID";
			public static string tadFK_TemplateSubAssembly = "@tadFK_TemplateSubAssembly";
			public static string tadAssemblyOrder = "@tadAssemblyOrder";
			public static string tadFK_r_PartType_GUID = "@tadFK_r_PartType_GUID";
			public static string tadFK_r_PartType = "@tadFK_r_PartType";
			public static string tadFK_r_CatalogItem_GUID = "@tadFK_r_CatalogItem_GUID";
			public static string tadFK_r_CatalogItem = "@tadFK_r_CatalogItem";
			public static string tadFK_r_ComponentGrouping_GUID = "@tadFK_r_ComponentGrouping_GUID";
			public static string tadFK_r_ComponentGrouping = "@tadFK_r_ComponentGrouping";
			public static string tadQuantity = "@tadQuantity";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the UserMaster table in the PumpServicing Database.
    /// </summary>
    public sealed partial class UserMasterMap : TableMap
    {
		public const string TABLE_NAME = "UserMaster";
		
		public static string ID = "usrPrimaryKey_GUID";
		public static string usrPrimaryKey = "usrPrimaryKey";
		public static string LastModified = "usrLstChgDT";
		public static string usrLstChgUser = "usrLstChgUser";
		public static string usrUserName = "usrUserName";
		public static string usrPwd = "usrPwd";
		public static string usrPwdExpiry = "usrPwdExpiry";
		public static string usrFullName = "usrFullName";
		public static string usrSUserName = "usrSUserName";
		public static string usrLevel = "usrLevel";
		public static string usrCreateDT = "usrCreateDT";
		public static string usrPumpService = "usrPumpService";
		public static string usrInActive = "usrInActive";
		public static string usrColor = "usrColor";
		public static string usrFK_Assembly_GUID = "usrFK_Assembly_GUID";
		public static string usrFK_Assembly = "usrFK_Assembly";
		
		public sealed partial class Prefixed
		{
			public static string ID = "UserMaster.usrPrimaryKey_GUID";
			public static string usrPrimaryKey = "UserMaster.usrPrimaryKey";
			public static string LastModified = "UserMaster.usrLstChgDT";
			public static string usrLstChgUser = "UserMaster.usrLstChgUser";
			public static string usrUserName = "UserMaster.usrUserName";
			public static string usrPwd = "UserMaster.usrPwd";
			public static string usrPwdExpiry = "UserMaster.usrPwdExpiry";
			public static string usrFullName = "UserMaster.usrFullName";
			public static string usrSUserName = "UserMaster.usrSUserName";
			public static string usrLevel = "UserMaster.usrLevel";
			public static string usrCreateDT = "UserMaster.usrCreateDT";
			public static string usrPumpService = "UserMaster.usrPumpService";
			public static string usrInActive = "UserMaster.usrInActive";
			public static string usrColor = "UserMaster.usrColor";
			public static string usrFK_Assembly_GUID = "UserMaster.usrFK_Assembly_GUID";
			public static string usrFK_Assembly = "UserMaster.usrFK_Assembly";
		}

		public sealed partial class Aliased
		{
			public static string ID = "UserMasterusrPrimaryKey_GUID";
			public static string usrPrimaryKey = "UserMasterusrPrimaryKey";
			public static string LastModified = "UserMasterusrLstChgDT";
			public static string usrLstChgUser = "UserMasterusrLstChgUser";
			public static string usrUserName = "UserMasterusrUserName";
			public static string usrPwd = "UserMasterusrPwd";
			public static string usrPwdExpiry = "UserMasterusrPwdExpiry";
			public static string usrFullName = "UserMasterusrFullName";
			public static string usrSUserName = "UserMasterusrSUserName";
			public static string usrLevel = "UserMasterusrLevel";
			public static string usrCreateDT = "UserMasterusrCreateDT";
			public static string usrPumpService = "UserMasterusrPumpService";
			public static string usrInActive = "UserMasterusrInActive";
			public static string usrColor = "UserMasterusrColor";
			public static string usrFK_Assembly_GUID = "UserMasterusrFK_Assembly_GUID";
			public static string usrFK_Assembly = "UserMasterusrFK_Assembly";
		}

		public sealed partial class Param
		{
			public static string ID = "@usrPrimaryKey_GUID";
			public static string usrPrimaryKey = "@usrPrimaryKey";
			public static string LastModified = "@usrLstChgDT";
			public static string usrLstChgUser = "@usrLstChgUser";
			public static string usrUserName = "@usrUserName";
			public static string usrPwd = "@usrPwd";
			public static string usrPwdExpiry = "@usrPwdExpiry";
			public static string usrFullName = "@usrFullName";
			public static string usrSUserName = "@usrSUserName";
			public static string usrLevel = "@usrLevel";
			public static string usrCreateDT = "@usrCreateDT";
			public static string usrPumpService = "@usrPumpService";
			public static string usrInActive = "@usrInActive";
			public static string usrColor = "@usrColor";
			public static string usrFK_Assembly_GUID = "@usrFK_Assembly_GUID";
			public static string usrFK_Assembly = "@usrFK_Assembly";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the Well table in the PumpServicing Database.
    /// </summary>
    public sealed partial class WellMap : TableMap
    {
		public const string TABLE_NAME = "Well";
		
		public static string ID = "welPrimaryKey_GUID";
		public static string welPrimaryKey = "welPrimaryKey";
		public static string LastModified = "welLstChgDT";
		public static string welLstChgUser = "welLstChgUser";
		public static string welRefCaseDefined = "welRefCaseDefined";
		public static string welFK_Assembly_GUID = "welFK_Assembly_GUID";
		public static string welFK_Assembly = "welFK_Assembly";
		public static string welUWBID = "welUWBID";
		public static string welFK_r_WellType = "welFK_r_WellType";
		public static string welFK_r_WellProfile = "welFK_r_WellProfile";
		public static string welFK_Owner = "welFK_Owner";
		public static string welFK_BusinessOrganization_Producer_GUID = "welFK_BusinessOrganization_Producer_GUID";
		public static string welFK_BusinessOrganization_Producer = "welFK_BusinessOrganization_Producer";
		public static string welFK_Lease_GUID = "welFK_Lease_GUID";
		public static string welFK_Lease = "welFK_Lease";
		public static string welWellName = "welWellName";
		public static string welLongWellName = "welLongWellName";
		public static string welActive = "welActive";
		public static string welSurfaceLatitude = "welSurfaceLatitude";
		public static string welSurfaceLongitude = "welSurfaceLongitude";
		public static string welFK_r_Country_GUID = "welFK_r_Country_GUID";
		public static string welFK_r_Country = "welFK_r_Country";
		public static string welFK_r_StateProvince_GUID = "welFK_r_StateProvince_GUID";
		public static string welFK_r_StateProvince = "welFK_r_StateProvince";
		public static string welSpudDate = "welSpudDate";
		public static string welCompletionDate = "welCompletionDate";
		public static string welAbandonmentDate = "welAbandonmentDate";
		public static string welFK_r_Field_GUID = "welFK_r_Field_GUID";
		public static string welFK_r_Field = "welFK_r_Field";
		public static string welLegalDesc = "welLegalDesc";
		public static string welCCLTownshipDirection = "welCCLTownshipDirection";
		public static string welCCLTownshipNumber = "welCCLTownshipNumber";
		public static string welCCLRangeDirection = "welCCLRangeDirection";
		public static string welCCLRangeNumber = "welCCLRangeNumber";
		public static string welCCLSectionIndicator = "welCCLSectionIndicator";
		public static string welCCLSectionNumber = "welCCLSectionNumber";
		public static string welCCLUnit = "welCCLUnit";
		public static string welCCLMeridianCode = "welCCLMeridianCode";
		public static string welCCLMeridianName = "welCCLMeridianName";
		public static string welFK_r_Foreman_GUID = "welFK_r_Foreman_GUID";
		public static string welFK_r_Foreman = "welFK_r_Foreman";
		public static string welFK_r_Engineer_GUID = "welFK_r_Engineer_GUID";
		public static string welFK_r_Engineer = "welFK_r_Engineer";
		public static string welPOCinService = "welPOCinService";
		public static string welFK_BusinessOrganization_PumpService_GUID = "welFK_BusinessOrganization_PumpService_GUID";
		public static string welFK_BusinessOrganization_PumpService = "welFK_BusinessOrganization_PumpService";
		public static string welPTTaxableStatus = "welPTTaxableStatus";
		public static string welPTTaxRate = "welPTTaxRate";
		public static string welUserDef10 = "welUserDef10";
		public static string welRemarks = "welRemarks";
		
		public sealed partial class Prefixed
		{
			public static string ID = "Well.welPrimaryKey_GUID";
			public static string welPrimaryKey = "Well.welPrimaryKey";
			public static string LastModified = "Well.welLstChgDT";
			public static string welLstChgUser = "Well.welLstChgUser";
			public static string welRefCaseDefined = "Well.welRefCaseDefined";
			public static string welFK_Assembly_GUID = "Well.welFK_Assembly_GUID";
			public static string welFK_Assembly = "Well.welFK_Assembly";
			public static string welUWBID = "Well.welUWBID";
			public static string welFK_r_WellType = "Well.welFK_r_WellType";
			public static string welFK_r_WellProfile = "Well.welFK_r_WellProfile";
			public static string welFK_Owner = "Well.welFK_Owner";
			public static string welFK_BusinessOrganization_Producer_GUID = "Well.welFK_BusinessOrganization_Producer_GUID";
			public static string welFK_BusinessOrganization_Producer = "Well.welFK_BusinessOrganization_Producer";
			public static string welFK_Lease_GUID = "Well.welFK_Lease_GUID";
			public static string welFK_Lease = "Well.welFK_Lease";
			public static string welWellName = "Well.welWellName";
			public static string welLongWellName = "Well.welLongWellName";
			public static string welActive = "Well.welActive";
			public static string welSurfaceLatitude = "Well.welSurfaceLatitude";
			public static string welSurfaceLongitude = "Well.welSurfaceLongitude";
			public static string welFK_r_Country_GUID = "Well.welFK_r_Country_GUID";
			public static string welFK_r_Country = "Well.welFK_r_Country";
			public static string welFK_r_StateProvince_GUID = "Well.welFK_r_StateProvince_GUID";
			public static string welFK_r_StateProvince = "Well.welFK_r_StateProvince";
			public static string welSpudDate = "Well.welSpudDate";
			public static string welCompletionDate = "Well.welCompletionDate";
			public static string welAbandonmentDate = "Well.welAbandonmentDate";
			public static string welFK_r_Field_GUID = "Well.welFK_r_Field_GUID";
			public static string welFK_r_Field = "Well.welFK_r_Field";
			public static string welLegalDesc = "Well.welLegalDesc";
			public static string welCCLTownshipDirection = "Well.welCCLTownshipDirection";
			public static string welCCLTownshipNumber = "Well.welCCLTownshipNumber";
			public static string welCCLRangeDirection = "Well.welCCLRangeDirection";
			public static string welCCLRangeNumber = "Well.welCCLRangeNumber";
			public static string welCCLSectionIndicator = "Well.welCCLSectionIndicator";
			public static string welCCLSectionNumber = "Well.welCCLSectionNumber";
			public static string welCCLUnit = "Well.welCCLUnit";
			public static string welCCLMeridianCode = "Well.welCCLMeridianCode";
			public static string welCCLMeridianName = "Well.welCCLMeridianName";
			public static string welFK_r_Foreman_GUID = "Well.welFK_r_Foreman_GUID";
			public static string welFK_r_Foreman = "Well.welFK_r_Foreman";
			public static string welFK_r_Engineer_GUID = "Well.welFK_r_Engineer_GUID";
			public static string welFK_r_Engineer = "Well.welFK_r_Engineer";
			public static string welPOCinService = "Well.welPOCinService";
			public static string welFK_BusinessOrganization_PumpService_GUID = "Well.welFK_BusinessOrganization_PumpService_GUID";
			public static string welFK_BusinessOrganization_PumpService = "Well.welFK_BusinessOrganization_PumpService";
			public static string welPTTaxableStatus = "Well.welPTTaxableStatus";
			public static string welPTTaxRate = "Well.welPTTaxRate";
			public static string welUserDef10 = "Well.welUserDef10";
			public static string welRemarks = "Well.welRemarks";
		}

		public sealed partial class Aliased
		{
			public static string ID = "WellwelPrimaryKey_GUID";
			public static string welPrimaryKey = "WellwelPrimaryKey";
			public static string LastModified = "WellwelLstChgDT";
			public static string welLstChgUser = "WellwelLstChgUser";
			public static string welRefCaseDefined = "WellwelRefCaseDefined";
			public static string welFK_Assembly_GUID = "WellwelFK_Assembly_GUID";
			public static string welFK_Assembly = "WellwelFK_Assembly";
			public static string welUWBID = "WellwelUWBID";
			public static string welFK_r_WellType = "WellwelFK_r_WellType";
			public static string welFK_r_WellProfile = "WellwelFK_r_WellProfile";
			public static string welFK_Owner = "WellwelFK_Owner";
			public static string welFK_BusinessOrganization_Producer_GUID = "WellwelFK_BusinessOrganization_Producer_GUID";
			public static string welFK_BusinessOrganization_Producer = "WellwelFK_BusinessOrganization_Producer";
			public static string welFK_Lease_GUID = "WellwelFK_Lease_GUID";
			public static string welFK_Lease = "WellwelFK_Lease";
			public static string welWellName = "WellwelWellName";
			public static string welLongWellName = "WellwelLongWellName";
			public static string welActive = "WellwelActive";
			public static string welSurfaceLatitude = "WellwelSurfaceLatitude";
			public static string welSurfaceLongitude = "WellwelSurfaceLongitude";
			public static string welFK_r_Country_GUID = "WellwelFK_r_Country_GUID";
			public static string welFK_r_Country = "WellwelFK_r_Country";
			public static string welFK_r_StateProvince_GUID = "WellwelFK_r_StateProvince_GUID";
			public static string welFK_r_StateProvince = "WellwelFK_r_StateProvince";
			public static string welSpudDate = "WellwelSpudDate";
			public static string welCompletionDate = "WellwelCompletionDate";
			public static string welAbandonmentDate = "WellwelAbandonmentDate";
			public static string welFK_r_Field_GUID = "WellwelFK_r_Field_GUID";
			public static string welFK_r_Field = "WellwelFK_r_Field";
			public static string welLegalDesc = "WellwelLegalDesc";
			public static string welCCLTownshipDirection = "WellwelCCLTownshipDirection";
			public static string welCCLTownshipNumber = "WellwelCCLTownshipNumber";
			public static string welCCLRangeDirection = "WellwelCCLRangeDirection";
			public static string welCCLRangeNumber = "WellwelCCLRangeNumber";
			public static string welCCLSectionIndicator = "WellwelCCLSectionIndicator";
			public static string welCCLSectionNumber = "WellwelCCLSectionNumber";
			public static string welCCLUnit = "WellwelCCLUnit";
			public static string welCCLMeridianCode = "WellwelCCLMeridianCode";
			public static string welCCLMeridianName = "WellwelCCLMeridianName";
			public static string welFK_r_Foreman_GUID = "WellwelFK_r_Foreman_GUID";
			public static string welFK_r_Foreman = "WellwelFK_r_Foreman";
			public static string welFK_r_Engineer_GUID = "WellwelFK_r_Engineer_GUID";
			public static string welFK_r_Engineer = "WellwelFK_r_Engineer";
			public static string welPOCinService = "WellwelPOCinService";
			public static string welFK_BusinessOrganization_PumpService_GUID = "WellwelFK_BusinessOrganization_PumpService_GUID";
			public static string welFK_BusinessOrganization_PumpService = "WellwelFK_BusinessOrganization_PumpService";
			public static string welPTTaxableStatus = "WellwelPTTaxableStatus";
			public static string welPTTaxRate = "WellwelPTTaxRate";
			public static string welUserDef10 = "WellwelUserDef10";
			public static string welRemarks = "WellwelRemarks";
		}

		public sealed partial class Param
		{
			public static string ID = "@welPrimaryKey_GUID";
			public static string welPrimaryKey = "@welPrimaryKey";
			public static string LastModified = "@welLstChgDT";
			public static string welLstChgUser = "@welLstChgUser";
			public static string welRefCaseDefined = "@welRefCaseDefined";
			public static string welFK_Assembly_GUID = "@welFK_Assembly_GUID";
			public static string welFK_Assembly = "@welFK_Assembly";
			public static string welUWBID = "@welUWBID";
			public static string welFK_r_WellType = "@welFK_r_WellType";
			public static string welFK_r_WellProfile = "@welFK_r_WellProfile";
			public static string welFK_Owner = "@welFK_Owner";
			public static string welFK_BusinessOrganization_Producer_GUID = "@welFK_BusinessOrganization_Producer_GUID";
			public static string welFK_BusinessOrganization_Producer = "@welFK_BusinessOrganization_Producer";
			public static string welFK_Lease_GUID = "@welFK_Lease_GUID";
			public static string welFK_Lease = "@welFK_Lease";
			public static string welWellName = "@welWellName";
			public static string welLongWellName = "@welLongWellName";
			public static string welActive = "@welActive";
			public static string welSurfaceLatitude = "@welSurfaceLatitude";
			public static string welSurfaceLongitude = "@welSurfaceLongitude";
			public static string welFK_r_Country_GUID = "@welFK_r_Country_GUID";
			public static string welFK_r_Country = "@welFK_r_Country";
			public static string welFK_r_StateProvince_GUID = "@welFK_r_StateProvince_GUID";
			public static string welFK_r_StateProvince = "@welFK_r_StateProvince";
			public static string welSpudDate = "@welSpudDate";
			public static string welCompletionDate = "@welCompletionDate";
			public static string welAbandonmentDate = "@welAbandonmentDate";
			public static string welFK_r_Field_GUID = "@welFK_r_Field_GUID";
			public static string welFK_r_Field = "@welFK_r_Field";
			public static string welLegalDesc = "@welLegalDesc";
			public static string welCCLTownshipDirection = "@welCCLTownshipDirection";
			public static string welCCLTownshipNumber = "@welCCLTownshipNumber";
			public static string welCCLRangeDirection = "@welCCLRangeDirection";
			public static string welCCLRangeNumber = "@welCCLRangeNumber";
			public static string welCCLSectionIndicator = "@welCCLSectionIndicator";
			public static string welCCLSectionNumber = "@welCCLSectionNumber";
			public static string welCCLUnit = "@welCCLUnit";
			public static string welCCLMeridianCode = "@welCCLMeridianCode";
			public static string welCCLMeridianName = "@welCCLMeridianName";
			public static string welFK_r_Foreman_GUID = "@welFK_r_Foreman_GUID";
			public static string welFK_r_Foreman = "@welFK_r_Foreman";
			public static string welFK_r_Engineer_GUID = "@welFK_r_Engineer_GUID";
			public static string welFK_r_Engineer = "@welFK_r_Engineer";
			public static string welPOCinService = "@welPOCinService";
			public static string welFK_BusinessOrganization_PumpService_GUID = "@welFK_BusinessOrganization_PumpService_GUID";
			public static string welFK_BusinessOrganization_PumpService = "@welFK_BusinessOrganization_PumpService";
			public static string welPTTaxableStatus = "@welPTTaxableStatus";
			public static string welPTTaxRate = "@welPTTaxRate";
			public static string welUserDef10 = "@welUserDef10";
			public static string welRemarks = "@welRemarks";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the WellCompletionReservoirs table in the PumpServicing Database.
    /// </summary>
    public sealed partial class WellCompletionReservoirsMap : TableMap
    {
		public const string TABLE_NAME = "WellCompletionReservoirs";
		
		public static string ID = "wcrPrimaryKey_GUID";
		public static string LastModified = "wcrLstChgDT";
		public static string wcrLstChgUser = "wcrLstChgUser";
		public static string wcrFK_WellCompletion_GUID = "wcrFK_WellCompletion_GUID";
		public static string wcrFK_r_Reservoir_GUID = "wcrFK_r_Reservoir_GUID";
		
		public sealed partial class Prefixed
		{
			public static string ID = "WellCompletionReservoirs.wcrPrimaryKey_GUID";
			public static string LastModified = "WellCompletionReservoirs.wcrLstChgDT";
			public static string wcrLstChgUser = "WellCompletionReservoirs.wcrLstChgUser";
			public static string wcrFK_WellCompletion_GUID = "WellCompletionReservoirs.wcrFK_WellCompletion_GUID";
			public static string wcrFK_r_Reservoir_GUID = "WellCompletionReservoirs.wcrFK_r_Reservoir_GUID";
		}

		public sealed partial class Aliased
		{
			public static string ID = "WellCompletionReservoirswcrPrimaryKey_GUID";
			public static string LastModified = "WellCompletionReservoirswcrLstChgDT";
			public static string wcrLstChgUser = "WellCompletionReservoirswcrLstChgUser";
			public static string wcrFK_WellCompletion_GUID = "WellCompletionReservoirswcrFK_WellCompletion_GUID";
			public static string wcrFK_r_Reservoir_GUID = "WellCompletionReservoirswcrFK_r_Reservoir_GUID";
		}

		public sealed partial class Param
		{
			public static string ID = "@wcrPrimaryKey_GUID";
			public static string LastModified = "@wcrLstChgDT";
			public static string wcrLstChgUser = "@wcrLstChgUser";
			public static string wcrFK_WellCompletion_GUID = "@wcrFK_WellCompletion_GUID";
			public static string wcrFK_r_Reservoir_GUID = "@wcrFK_r_Reservoir_GUID";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the WellCompletionXRef table in the PumpServicing Database.
    /// </summary>
    public sealed partial class WellCompletionXRefMap : TableMap
    {
		public const string TABLE_NAME = "WellCompletionXRef";
		
		public static string ID = "wxrPrimaryKey_GUID";
		public static string wxrPrimaryKey = "wxrPrimaryKey";
		public static string LastModified = "wxrLstChgDT";
		public static string wxrLstChgUser = "wxrLstChgUser";
		public static string wxrRefCaseDefined = "wxrRefCaseDefined";
		public static string wxrFK_Well_GUID = "wxrFK_Well_GUID";
		public static string wxrFK_Well = "wxrFK_Well";
		public static string wxrAPI12 = "wxrAPI12";
		public static string wxrAPI14 = "wxrAPI14";
		public static string wxrShortWellCompName = "wxrShortWellCompName";
		public static string wxrLongWellCompName = "wxrLongWellCompName";
		public static string wxrCurrentTestDateTime = "wxrCurrentTestDateTime";
		public static string wxrCurrentTestOil = "wxrCurrentTestOil";
		public static string wxrCurrentTestGas = "wxrCurrentTestGas";
		public static string wxrCurrentTestWater = "wxrCurrentTestWater";
		public static string wxrCompletionDate = "wxrCompletionDate";
		public static string wxrAbandonmentDate = "wxrAbandonmentDate";
		
		public sealed partial class Prefixed
		{
			public static string ID = "WellCompletionXRef.wxrPrimaryKey_GUID";
			public static string wxrPrimaryKey = "WellCompletionXRef.wxrPrimaryKey";
			public static string LastModified = "WellCompletionXRef.wxrLstChgDT";
			public static string wxrLstChgUser = "WellCompletionXRef.wxrLstChgUser";
			public static string wxrRefCaseDefined = "WellCompletionXRef.wxrRefCaseDefined";
			public static string wxrFK_Well_GUID = "WellCompletionXRef.wxrFK_Well_GUID";
			public static string wxrFK_Well = "WellCompletionXRef.wxrFK_Well";
			public static string wxrAPI12 = "WellCompletionXRef.wxrAPI12";
			public static string wxrAPI14 = "WellCompletionXRef.wxrAPI14";
			public static string wxrShortWellCompName = "WellCompletionXRef.wxrShortWellCompName";
			public static string wxrLongWellCompName = "WellCompletionXRef.wxrLongWellCompName";
			public static string wxrCurrentTestDateTime = "WellCompletionXRef.wxrCurrentTestDateTime";
			public static string wxrCurrentTestOil = "WellCompletionXRef.wxrCurrentTestOil";
			public static string wxrCurrentTestGas = "WellCompletionXRef.wxrCurrentTestGas";
			public static string wxrCurrentTestWater = "WellCompletionXRef.wxrCurrentTestWater";
			public static string wxrCompletionDate = "WellCompletionXRef.wxrCompletionDate";
			public static string wxrAbandonmentDate = "WellCompletionXRef.wxrAbandonmentDate";
		}

		public sealed partial class Aliased
		{
			public static string ID = "WellCompletionXRefwxrPrimaryKey_GUID";
			public static string wxrPrimaryKey = "WellCompletionXRefwxrPrimaryKey";
			public static string LastModified = "WellCompletionXRefwxrLstChgDT";
			public static string wxrLstChgUser = "WellCompletionXRefwxrLstChgUser";
			public static string wxrRefCaseDefined = "WellCompletionXRefwxrRefCaseDefined";
			public static string wxrFK_Well_GUID = "WellCompletionXRefwxrFK_Well_GUID";
			public static string wxrFK_Well = "WellCompletionXRefwxrFK_Well";
			public static string wxrAPI12 = "WellCompletionXRefwxrAPI12";
			public static string wxrAPI14 = "WellCompletionXRefwxrAPI14";
			public static string wxrShortWellCompName = "WellCompletionXRefwxrShortWellCompName";
			public static string wxrLongWellCompName = "WellCompletionXRefwxrLongWellCompName";
			public static string wxrCurrentTestDateTime = "WellCompletionXRefwxrCurrentTestDateTime";
			public static string wxrCurrentTestOil = "WellCompletionXRefwxrCurrentTestOil";
			public static string wxrCurrentTestGas = "WellCompletionXRefwxrCurrentTestGas";
			public static string wxrCurrentTestWater = "WellCompletionXRefwxrCurrentTestWater";
			public static string wxrCompletionDate = "WellCompletionXRefwxrCompletionDate";
			public static string wxrAbandonmentDate = "WellCompletionXRefwxrAbandonmentDate";
		}

		public sealed partial class Param
		{
			public static string ID = "@wxrPrimaryKey_GUID";
			public static string wxrPrimaryKey = "@wxrPrimaryKey";
			public static string LastModified = "@wxrLstChgDT";
			public static string wxrLstChgUser = "@wxrLstChgUser";
			public static string wxrRefCaseDefined = "@wxrRefCaseDefined";
			public static string wxrFK_Well_GUID = "@wxrFK_Well_GUID";
			public static string wxrFK_Well = "@wxrFK_Well";
			public static string wxrAPI12 = "@wxrAPI12";
			public static string wxrAPI14 = "@wxrAPI14";
			public static string wxrShortWellCompName = "@wxrShortWellCompName";
			public static string wxrLongWellCompName = "@wxrLongWellCompName";
			public static string wxrCurrentTestDateTime = "@wxrCurrentTestDateTime";
			public static string wxrCurrentTestOil = "@wxrCurrentTestOil";
			public static string wxrCurrentTestGas = "@wxrCurrentTestGas";
			public static string wxrCurrentTestWater = "@wxrCurrentTestWater";
			public static string wxrCompletionDate = "@wxrCompletionDate";
			public static string wxrAbandonmentDate = "@wxrAbandonmentDate";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the Workorder table in the PumpServicing Database.
    /// </summary>
    public sealed partial class WorkorderMap : TableMap
    {
		public const string TABLE_NAME = "Workorder";
		
		public static string ID = "pswPrimaryKey_GUID";
		public static string pswPrimaryKey = "pswPrimaryKey";
		public static string LastModified = "pswLstChgDT";
		public static string pswLstChgUser = "pswLstChgUser";
		public static string pswFK_Job_GUID = "pswFK_Job_GUID";
		public static string pswFK_Job = "pswFK_Job";
		public static string pswFK_r_WorkorderType_GUID = "pswFK_r_WorkorderType_GUID";
		public static string pswFK_r_WorkorderType = "pswFK_r_WorkorderType";
		public static string pswWorkorderID = "pswWorkorderID";
		public static string pswFK_Component_GUID = "pswFK_Component_GUID";
		public static string pswFK_Component = "pswFK_Component";
		public static string pswWorkorderGrouping = "pswWorkorderGrouping";
		public static string pswScheduledDateTime = "pswScheduledDateTime";
		public static string pswFK_r_WorkorderStatus_GUID = "pswFK_r_WorkorderStatus_GUID";
		public static string pswFK_r_WorkorderStatus = "pswFK_r_WorkorderStatus";
		public static string pswFK_Invoice_GUID = "pswFK_Invoice_GUID";
		public static string pswFK_Invoice = "pswFK_Invoice";
		public static string pswFK_r_WorkorderTypeTaskType = "pswFK_r_WorkorderTypeTaskType";
		public static string pswFK_r_WorkorderTypeTaskType_GUID = "pswFK_r_WorkorderTypeTaskType_GUID";
		
		public sealed partial class Prefixed
		{
			public static string ID = "Workorder.pswPrimaryKey_GUID";
			public static string pswPrimaryKey = "Workorder.pswPrimaryKey";
			public static string LastModified = "Workorder.pswLstChgDT";
			public static string pswLstChgUser = "Workorder.pswLstChgUser";
			public static string pswFK_Job_GUID = "Workorder.pswFK_Job_GUID";
			public static string pswFK_Job = "Workorder.pswFK_Job";
			public static string pswFK_r_WorkorderType_GUID = "Workorder.pswFK_r_WorkorderType_GUID";
			public static string pswFK_r_WorkorderType = "Workorder.pswFK_r_WorkorderType";
			public static string pswWorkorderID = "Workorder.pswWorkorderID";
			public static string pswFK_Component_GUID = "Workorder.pswFK_Component_GUID";
			public static string pswFK_Component = "Workorder.pswFK_Component";
			public static string pswWorkorderGrouping = "Workorder.pswWorkorderGrouping";
			public static string pswScheduledDateTime = "Workorder.pswScheduledDateTime";
			public static string pswFK_r_WorkorderStatus_GUID = "Workorder.pswFK_r_WorkorderStatus_GUID";
			public static string pswFK_r_WorkorderStatus = "Workorder.pswFK_r_WorkorderStatus";
			public static string pswFK_Invoice_GUID = "Workorder.pswFK_Invoice_GUID";
			public static string pswFK_Invoice = "Workorder.pswFK_Invoice";
			public static string pswFK_r_WorkorderTypeTaskType = "Workorder.pswFK_r_WorkorderTypeTaskType";
			public static string pswFK_r_WorkorderTypeTaskType_GUID = "Workorder.pswFK_r_WorkorderTypeTaskType_GUID";
		}

		public sealed partial class Aliased
		{
			public static string ID = "WorkorderpswPrimaryKey_GUID";
			public static string pswPrimaryKey = "WorkorderpswPrimaryKey";
			public static string LastModified = "WorkorderpswLstChgDT";
			public static string pswLstChgUser = "WorkorderpswLstChgUser";
			public static string pswFK_Job_GUID = "WorkorderpswFK_Job_GUID";
			public static string pswFK_Job = "WorkorderpswFK_Job";
			public static string pswFK_r_WorkorderType_GUID = "WorkorderpswFK_r_WorkorderType_GUID";
			public static string pswFK_r_WorkorderType = "WorkorderpswFK_r_WorkorderType";
			public static string pswWorkorderID = "WorkorderpswWorkorderID";
			public static string pswFK_Component_GUID = "WorkorderpswFK_Component_GUID";
			public static string pswFK_Component = "WorkorderpswFK_Component";
			public static string pswWorkorderGrouping = "WorkorderpswWorkorderGrouping";
			public static string pswScheduledDateTime = "WorkorderpswScheduledDateTime";
			public static string pswFK_r_WorkorderStatus_GUID = "WorkorderpswFK_r_WorkorderStatus_GUID";
			public static string pswFK_r_WorkorderStatus = "WorkorderpswFK_r_WorkorderStatus";
			public static string pswFK_Invoice_GUID = "WorkorderpswFK_Invoice_GUID";
			public static string pswFK_Invoice = "WorkorderpswFK_Invoice";
			public static string pswFK_r_WorkorderTypeTaskType = "WorkorderpswFK_r_WorkorderTypeTaskType";
			public static string pswFK_r_WorkorderTypeTaskType_GUID = "WorkorderpswFK_r_WorkorderTypeTaskType_GUID";
		}

		public sealed partial class Param
		{
			public static string ID = "@pswPrimaryKey_GUID";
			public static string pswPrimaryKey = "@pswPrimaryKey";
			public static string LastModified = "@pswLstChgDT";
			public static string pswLstChgUser = "@pswLstChgUser";
			public static string pswFK_Job_GUID = "@pswFK_Job_GUID";
			public static string pswFK_Job = "@pswFK_Job";
			public static string pswFK_r_WorkorderType_GUID = "@pswFK_r_WorkorderType_GUID";
			public static string pswFK_r_WorkorderType = "@pswFK_r_WorkorderType";
			public static string pswWorkorderID = "@pswWorkorderID";
			public static string pswFK_Component_GUID = "@pswFK_Component_GUID";
			public static string pswFK_Component = "@pswFK_Component";
			public static string pswWorkorderGrouping = "@pswWorkorderGrouping";
			public static string pswScheduledDateTime = "@pswScheduledDateTime";
			public static string pswFK_r_WorkorderStatus_GUID = "@pswFK_r_WorkorderStatus_GUID";
			public static string pswFK_r_WorkorderStatus = "@pswFK_r_WorkorderStatus";
			public static string pswFK_Invoice_GUID = "@pswFK_Invoice_GUID";
			public static string pswFK_Invoice = "@pswFK_Invoice";
			public static string pswFK_r_WorkorderTypeTaskType = "@pswFK_r_WorkorderTypeTaskType";
			public static string pswFK_r_WorkorderTypeTaskType_GUID = "@pswFK_r_WorkorderTypeTaskType_GUID";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the WorkorderStatusHistory table in the PumpServicing Database.
    /// </summary>
    public sealed partial class WorkorderStatusHistoryMap : TableMap
    {
		public const string TABLE_NAME = "WorkorderStatusHistory";
		
		public static string ID = "xhdPrimaryKey_GUID";
		public static string xhdPrimaryKey = "xhdPrimaryKey";
		public static string LastModified = "xhdLstChgDT";
		public static string xhdLstChgUser = "xhdLstChgUser";
		public static string xhdFK_Workorder_GUID = "xhdFK_Workorder_GUID";
		public static string xhdFK_Workorder = "xhdFK_Workorder";
		public static string xhdFK_r_WorkorderStatus_GUID = "xhdFK_r_WorkorderStatus_GUID";
		public static string xhdFK_r_WorkorderStatus = "xhdFK_r_WorkorderStatus";
		public static string xhdStatusChangeDT = "xhdStatusChangeDT";
		public static string xhdFK_r_WorkorderTypeTaskType = "xhdFK_r_WorkorderTypeTaskType";
		public static string xhdFK_r_WorkorderTypeTaskType_GUID = "xhdFK_r_WorkorderTypeTaskType_GUID";
		
		public sealed partial class Prefixed
		{
			public static string ID = "WorkorderStatusHistory.xhdPrimaryKey_GUID";
			public static string xhdPrimaryKey = "WorkorderStatusHistory.xhdPrimaryKey";
			public static string LastModified = "WorkorderStatusHistory.xhdLstChgDT";
			public static string xhdLstChgUser = "WorkorderStatusHistory.xhdLstChgUser";
			public static string xhdFK_Workorder_GUID = "WorkorderStatusHistory.xhdFK_Workorder_GUID";
			public static string xhdFK_Workorder = "WorkorderStatusHistory.xhdFK_Workorder";
			public static string xhdFK_r_WorkorderStatus_GUID = "WorkorderStatusHistory.xhdFK_r_WorkorderStatus_GUID";
			public static string xhdFK_r_WorkorderStatus = "WorkorderStatusHistory.xhdFK_r_WorkorderStatus";
			public static string xhdStatusChangeDT = "WorkorderStatusHistory.xhdStatusChangeDT";
			public static string xhdFK_r_WorkorderTypeTaskType = "WorkorderStatusHistory.xhdFK_r_WorkorderTypeTaskType";
			public static string xhdFK_r_WorkorderTypeTaskType_GUID = "WorkorderStatusHistory.xhdFK_r_WorkorderTypeTaskType_GUID";
		}

		public sealed partial class Aliased
		{
			public static string ID = "WorkorderStatusHistoryxhdPrimaryKey_GUID";
			public static string xhdPrimaryKey = "WorkorderStatusHistoryxhdPrimaryKey";
			public static string LastModified = "WorkorderStatusHistoryxhdLstChgDT";
			public static string xhdLstChgUser = "WorkorderStatusHistoryxhdLstChgUser";
			public static string xhdFK_Workorder_GUID = "WorkorderStatusHistoryxhdFK_Workorder_GUID";
			public static string xhdFK_Workorder = "WorkorderStatusHistoryxhdFK_Workorder";
			public static string xhdFK_r_WorkorderStatus_GUID = "WorkorderStatusHistoryxhdFK_r_WorkorderStatus_GUID";
			public static string xhdFK_r_WorkorderStatus = "WorkorderStatusHistoryxhdFK_r_WorkorderStatus";
			public static string xhdStatusChangeDT = "WorkorderStatusHistoryxhdStatusChangeDT";
			public static string xhdFK_r_WorkorderTypeTaskType = "WorkorderStatusHistoryxhdFK_r_WorkorderTypeTaskType";
			public static string xhdFK_r_WorkorderTypeTaskType_GUID = "WorkorderStatusHistoryxhdFK_r_WorkorderTypeTaskType_GUID";
		}

		public sealed partial class Param
		{
			public static string ID = "@xhdPrimaryKey_GUID";
			public static string xhdPrimaryKey = "@xhdPrimaryKey";
			public static string LastModified = "@xhdLstChgDT";
			public static string xhdLstChgUser = "@xhdLstChgUser";
			public static string xhdFK_Workorder_GUID = "@xhdFK_Workorder_GUID";
			public static string xhdFK_Workorder = "@xhdFK_Workorder";
			public static string xhdFK_r_WorkorderStatus_GUID = "@xhdFK_r_WorkorderStatus_GUID";
			public static string xhdFK_r_WorkorderStatus = "@xhdFK_r_WorkorderStatus";
			public static string xhdStatusChangeDT = "@xhdStatusChangeDT";
			public static string xhdFK_r_WorkorderTypeTaskType = "@xhdFK_r_WorkorderTypeTaskType";
			public static string xhdFK_r_WorkorderTypeTaskType_GUID = "@xhdFK_r_WorkorderTypeTaskType_GUID";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the WorkorderSubAssemblies table in the PumpServicing Database.
    /// </summary>
    public sealed partial class WorkorderSubAssembliesMap : TableMap
    {
		public const string TABLE_NAME = "WorkorderSubAssemblies";
		
		public static string ID = "psiPrimaryKey_GUID";
		public static string psiPrimaryKey = "psiPrimaryKey";
		public static string LastModified = "psiLstChgDT";
		public static string psiLstChgUser = "psiLstChgUser";
		public static string psiFK_Workorder_GUID = "psiFK_Workorder_GUID";
		public static string psiFK_Workorder = "psiFK_Workorder";
		public static string psiFK_r_ComponentCategory_GUID = "psiFK_r_ComponentCategory_GUID";
		public static string psiFK_r_ComponentCategory = "psiFK_r_ComponentCategory";
		public static string psiStartedDT = "psiStartedDT";
		public static string psiFK_Assembly_GUID = "psiFK_Assembly_GUID";
		public static string psiFK_Assembly = "psiFK_Assembly";
		public static string psiFK_UserMaster_GUID = "psiFK_UserMaster_GUID";
		public static string psiFK_UserMaster = "psiFK_UserMaster";
		public static string psiFK_r_PSRSubAssemblyStatus_GUID = "psiFK_r_PSRSubAssemblyStatus_GUID";
		public static string psiFK_r_PSRSubAssemblyStatus = "psiFK_r_PSRSubAssemblyStatus";
		public static string psiStatusDT = "psiStatusDT";
		public static string psiCurrentGUIPhase = "psiCurrentGUIPhase";
		
		public sealed partial class Prefixed
		{
			public static string ID = "WorkorderSubAssemblies.psiPrimaryKey_GUID";
			public static string psiPrimaryKey = "WorkorderSubAssemblies.psiPrimaryKey";
			public static string LastModified = "WorkorderSubAssemblies.psiLstChgDT";
			public static string psiLstChgUser = "WorkorderSubAssemblies.psiLstChgUser";
			public static string psiFK_Workorder_GUID = "WorkorderSubAssemblies.psiFK_Workorder_GUID";
			public static string psiFK_Workorder = "WorkorderSubAssemblies.psiFK_Workorder";
			public static string psiFK_r_ComponentCategory_GUID = "WorkorderSubAssemblies.psiFK_r_ComponentCategory_GUID";
			public static string psiFK_r_ComponentCategory = "WorkorderSubAssemblies.psiFK_r_ComponentCategory";
			public static string psiStartedDT = "WorkorderSubAssemblies.psiStartedDT";
			public static string psiFK_Assembly_GUID = "WorkorderSubAssemblies.psiFK_Assembly_GUID";
			public static string psiFK_Assembly = "WorkorderSubAssemblies.psiFK_Assembly";
			public static string psiFK_UserMaster_GUID = "WorkorderSubAssemblies.psiFK_UserMaster_GUID";
			public static string psiFK_UserMaster = "WorkorderSubAssemblies.psiFK_UserMaster";
			public static string psiFK_r_PSRSubAssemblyStatus_GUID = "WorkorderSubAssemblies.psiFK_r_PSRSubAssemblyStatus_GUID";
			public static string psiFK_r_PSRSubAssemblyStatus = "WorkorderSubAssemblies.psiFK_r_PSRSubAssemblyStatus";
			public static string psiStatusDT = "WorkorderSubAssemblies.psiStatusDT";
			public static string psiCurrentGUIPhase = "WorkorderSubAssemblies.psiCurrentGUIPhase";
		}

		public sealed partial class Aliased
		{
			public static string ID = "WorkorderSubAssembliespsiPrimaryKey_GUID";
			public static string psiPrimaryKey = "WorkorderSubAssembliespsiPrimaryKey";
			public static string LastModified = "WorkorderSubAssembliespsiLstChgDT";
			public static string psiLstChgUser = "WorkorderSubAssembliespsiLstChgUser";
			public static string psiFK_Workorder_GUID = "WorkorderSubAssembliespsiFK_Workorder_GUID";
			public static string psiFK_Workorder = "WorkorderSubAssembliespsiFK_Workorder";
			public static string psiFK_r_ComponentCategory_GUID = "WorkorderSubAssembliespsiFK_r_ComponentCategory_GUID";
			public static string psiFK_r_ComponentCategory = "WorkorderSubAssembliespsiFK_r_ComponentCategory";
			public static string psiStartedDT = "WorkorderSubAssembliespsiStartedDT";
			public static string psiFK_Assembly_GUID = "WorkorderSubAssembliespsiFK_Assembly_GUID";
			public static string psiFK_Assembly = "WorkorderSubAssembliespsiFK_Assembly";
			public static string psiFK_UserMaster_GUID = "WorkorderSubAssembliespsiFK_UserMaster_GUID";
			public static string psiFK_UserMaster = "WorkorderSubAssembliespsiFK_UserMaster";
			public static string psiFK_r_PSRSubAssemblyStatus_GUID = "WorkorderSubAssembliespsiFK_r_PSRSubAssemblyStatus_GUID";
			public static string psiFK_r_PSRSubAssemblyStatus = "WorkorderSubAssembliespsiFK_r_PSRSubAssemblyStatus";
			public static string psiStatusDT = "WorkorderSubAssembliespsiStatusDT";
			public static string psiCurrentGUIPhase = "WorkorderSubAssembliespsiCurrentGUIPhase";
		}

		public sealed partial class Param
		{
			public static string ID = "@psiPrimaryKey_GUID";
			public static string psiPrimaryKey = "@psiPrimaryKey";
			public static string LastModified = "@psiLstChgDT";
			public static string psiLstChgUser = "@psiLstChgUser";
			public static string psiFK_Workorder_GUID = "@psiFK_Workorder_GUID";
			public static string psiFK_Workorder = "@psiFK_Workorder";
			public static string psiFK_r_ComponentCategory_GUID = "@psiFK_r_ComponentCategory_GUID";
			public static string psiFK_r_ComponentCategory = "@psiFK_r_ComponentCategory";
			public static string psiStartedDT = "@psiStartedDT";
			public static string psiFK_Assembly_GUID = "@psiFK_Assembly_GUID";
			public static string psiFK_Assembly = "@psiFK_Assembly";
			public static string psiFK_UserMaster_GUID = "@psiFK_UserMaster_GUID";
			public static string psiFK_UserMaster = "@psiFK_UserMaster";
			public static string psiFK_r_PSRSubAssemblyStatus_GUID = "@psiFK_r_PSRSubAssemblyStatus_GUID";
			public static string psiFK_r_PSRSubAssemblyStatus = "@psiFK_r_PSRSubAssemblyStatus";
			public static string psiStatusDT = "@psiStatusDT";
			public static string psiCurrentGUIPhase = "@psiCurrentGUIPhase";
		}
    } 
    /// <summary>
    /// A partial class to work with the XR Transaction Mappings which represents the WorkorderSubAssembliesStatusHistory table in the PumpServicing Database.
    /// </summary>
    public sealed partial class WorkorderSubAssembliesStatusHistoryMap : TableMap
    {
		public const string TABLE_NAME = "WorkorderSubAssembliesStatusHistory";
		
		public static string ID = "xh4PrimaryKey_GUID";
		public static string xh4PrimaryKey = "xh4PrimaryKey";
		public static string LastModified = "xh4LstChgDT";
		public static string xh4LstChgUser = "xh4LstChgUser";
		public static string xh4FK_WorkorderSubAssemblies_GUID = "xh4FK_WorkorderSubAssemblies_GUID";
		public static string xh4FK_WorkorderSubAssemblies = "xh4FK_WorkorderSubAssemblies";
		public static string xh4FK_r_PSRSubAssemblyStatus_GUID = "xh4FK_r_PSRSubAssemblyStatus_GUID";
		public static string xh4FK_r_PSRSubAssemblyStatus = "xh4FK_r_PSRSubAssemblyStatus";
		
		public sealed partial class Prefixed
		{
			public static string ID = "WorkorderSubAssembliesStatusHistory.xh4PrimaryKey_GUID";
			public static string xh4PrimaryKey = "WorkorderSubAssembliesStatusHistory.xh4PrimaryKey";
			public static string LastModified = "WorkorderSubAssembliesStatusHistory.xh4LstChgDT";
			public static string xh4LstChgUser = "WorkorderSubAssembliesStatusHistory.xh4LstChgUser";
			public static string xh4FK_WorkorderSubAssemblies_GUID = "WorkorderSubAssembliesStatusHistory.xh4FK_WorkorderSubAssemblies_GUID";
			public static string xh4FK_WorkorderSubAssemblies = "WorkorderSubAssembliesStatusHistory.xh4FK_WorkorderSubAssemblies";
			public static string xh4FK_r_PSRSubAssemblyStatus_GUID = "WorkorderSubAssembliesStatusHistory.xh4FK_r_PSRSubAssemblyStatus_GUID";
			public static string xh4FK_r_PSRSubAssemblyStatus = "WorkorderSubAssembliesStatusHistory.xh4FK_r_PSRSubAssemblyStatus";
		}

		public sealed partial class Aliased
		{
			public static string ID = "WorkorderSubAssembliesStatusHistoryxh4PrimaryKey_GUID";
			public static string xh4PrimaryKey = "WorkorderSubAssembliesStatusHistoryxh4PrimaryKey";
			public static string LastModified = "WorkorderSubAssembliesStatusHistoryxh4LstChgDT";
			public static string xh4LstChgUser = "WorkorderSubAssembliesStatusHistoryxh4LstChgUser";
			public static string xh4FK_WorkorderSubAssemblies_GUID = "WorkorderSubAssembliesStatusHistoryxh4FK_WorkorderSubAssemblies_GUID";
			public static string xh4FK_WorkorderSubAssemblies = "WorkorderSubAssembliesStatusHistoryxh4FK_WorkorderSubAssemblies";
			public static string xh4FK_r_PSRSubAssemblyStatus_GUID = "WorkorderSubAssembliesStatusHistoryxh4FK_r_PSRSubAssemblyStatus_GUID";
			public static string xh4FK_r_PSRSubAssemblyStatus = "WorkorderSubAssembliesStatusHistoryxh4FK_r_PSRSubAssemblyStatus";
		}

		public sealed partial class Param
		{
			public static string ID = "@xh4PrimaryKey_GUID";
			public static string xh4PrimaryKey = "@xh4PrimaryKey";
			public static string LastModified = "@xh4LstChgDT";
			public static string xh4LstChgUser = "@xh4LstChgUser";
			public static string xh4FK_WorkorderSubAssemblies_GUID = "@xh4FK_WorkorderSubAssemblies_GUID";
			public static string xh4FK_WorkorderSubAssemblies = "@xh4FK_WorkorderSubAssemblies";
			public static string xh4FK_r_PSRSubAssemblyStatus_GUID = "@xh4FK_r_PSRSubAssemblyStatus_GUID";
			public static string xh4FK_r_PSRSubAssemblyStatus = "@xh4FK_r_PSRSubAssemblyStatus";
		}
    } 
}
