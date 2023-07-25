


using System;
using WFT.PSService.ServiceLibrary;
using LFI.Sync.DataManager;

namespace WFT.PSService.Data
{
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the Assembly table in the PumpServicing Database.
    /// </summary>
    public partial class AssemblyTransaction : BaseTransaction<Assembly>
    {
        public AssemblyTransaction()
            : base(AssemblyMap.TABLE_NAME, AssemblyMap.ID)
        {
        }
        
        public AssemblyTransaction( IBaseData _type )
			: base( _type, AssemblyMap.TABLE_NAME, AssemblyMap.ID )
		{
        }

        public AssemblyTransaction( string _PrimaryKey )
			: base( AssemblyMap.TABLE_NAME, AssemblyMap.ID, _PrimaryKey )
		{
        }

        public AssemblyTransaction( DateTime _lastModified )
            : base( AssemblyMap.TABLE_NAME, AssemblyMap.ID, _lastModified, AssemblyMap.LastModified )
        {
        }				
    
        public override Assembly BuildFromReader(TransactionReader reader)
        {
            Assembly a = new Assembly( );
			a.aclPrimaryKey_GUID = reader.TryReadGuid( AssemblyMap.ID);
			a.aclPrimaryKey = reader.TryReadstring( AssemblyMap.aclPrimaryKey);
			a.aclLstChgDT = reader.TryReadDateTime( AssemblyMap.LastModified);
			a.aclLstChgUser = reader.TryReadstring( AssemblyMap.aclLstChgUser);
			a.aclRefCaseDefined = reader.TryReadbool( AssemblyMap.aclRefCaseDefined);
			a.aclFK_r_AssemblyType_GUID = reader.TryReadGuid( AssemblyMap.aclFK_r_AssemblyType_GUID);
			a.aclFK_r_AssemblyType = reader.TryReadstring( AssemblyMap.aclFK_r_AssemblyType);
			a.aclAssemblyName = reader.TryReadstring( AssemblyMap.aclAssemblyName);
			a.aclTemplate = reader.TryReadbool( AssemblyMap.aclTemplate);
    
            return a;
        }

   		public override void RegisterParams()
		{
			Assembly a = (Assembly)dataObj;
			this.AddParam( AssemblyMap.ID, AssemblyMap.Param.ID, a.ID );
			this.AddParam( AssemblyMap.aclPrimaryKey, AssemblyMap.Param.aclPrimaryKey, a.aclPrimaryKey );
			this.AddParam( AssemblyMap.LastModified, AssemblyMap.Param.LastModified, a.LastModified );
			this.AddParam( AssemblyMap.aclLstChgUser, AssemblyMap.Param.aclLstChgUser, a.aclLstChgUser );
			this.AddParam( AssemblyMap.aclRefCaseDefined, AssemblyMap.Param.aclRefCaseDefined, a.aclRefCaseDefined );
			this.AddParam( AssemblyMap.aclFK_r_AssemblyType_GUID, AssemblyMap.Param.aclFK_r_AssemblyType_GUID, a.aclFK_r_AssemblyType_GUID );
			this.AddParam( AssemblyMap.aclFK_r_AssemblyType, AssemblyMap.Param.aclFK_r_AssemblyType, a.aclFK_r_AssemblyType );
			this.AddParam( AssemblyMap.aclAssemblyName, AssemblyMap.Param.aclAssemblyName, a.aclAssemblyName );
			this.AddParam( AssemblyMap.aclTemplate, AssemblyMap.Param.aclTemplate, a.aclTemplate );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the AssemblyComponent table in the PumpServicing Database.
    /// </summary>
    public partial class AssemblyComponentTransaction : BaseTransaction<AssemblyComponent>
    {
        public AssemblyComponentTransaction()
            : base(AssemblyComponentMap.TABLE_NAME, AssemblyComponentMap.ID)
        {
        }
        
        public AssemblyComponentTransaction( IBaseData _type )
			: base( _type, AssemblyComponentMap.TABLE_NAME, AssemblyComponentMap.ID )
		{
        }

        public AssemblyComponentTransaction( string _PrimaryKey )
			: base( AssemblyComponentMap.TABLE_NAME, AssemblyComponentMap.ID, _PrimaryKey )
		{
        }

        public AssemblyComponentTransaction( DateTime _lastModified )
            : base( AssemblyComponentMap.TABLE_NAME, AssemblyComponentMap.ID, _lastModified, AssemblyComponentMap.LastModified )
        {
        }				
    
        public override AssemblyComponent BuildFromReader(TransactionReader reader)
        {
            AssemblyComponent a = new AssemblyComponent( );
			a.ascPrimaryKey_GUID = reader.TryReadGuid( AssemblyComponentMap.ID);
			a.ascPrimaryKey = reader.TryReadstring( AssemblyComponentMap.ascPrimaryKey);
			a.ascLstChgDT = reader.TryReadDateTime( AssemblyComponentMap.LastModified);
			a.ascLstChgUser = reader.TryReadstring( AssemblyComponentMap.ascLstChgUser);
			a.ascFK_Assembly_GUID = reader.TryReadGuid( AssemblyComponentMap.ascFK_Assembly_GUID);
			a.ascFK_Assembly = reader.TryReadstring( AssemblyComponentMap.ascFK_Assembly);
			a.ascFK_Component_GUID = reader.TryReadGuid( AssemblyComponentMap.ascFK_Component_GUID);
			a.ascFK_Component = reader.TryReadstring( AssemblyComponentMap.ascFK_Component);
			a.ascFK_Event_Beginning_GUID = reader.TryReadGuid( AssemblyComponentMap.ascFK_Event_Beginning_GUID);
			a.ascFK_Event_Beginning = reader.TryReadstring( AssemblyComponentMap.ascFK_Event_Beginning);
			a.ascBegEventDT = reader.TryReadDateTime( AssemblyComponentMap.ascBegEventDT);
			a.ascFK_Event_Ending_GUID = reader.TryReadGuid( AssemblyComponentMap.ascFK_Event_Ending_GUID);
			a.ascFK_Event_Ending = reader.TryReadstring( AssemblyComponentMap.ascFK_Event_Ending);
			a.ascEndEventDT = reader.TryReadDateTime( AssemblyComponentMap.ascEndEventDT);
			a.ascAssemblyOrder = reader.TryReaddecimal( AssemblyComponentMap.ascAssemblyOrder);
			a.ascFK_r_ComponentGrouping_GUID = reader.TryReadGuid( AssemblyComponentMap.ascFK_r_ComponentGrouping_GUID);
			a.ascFK_r_ComponentGrouping = reader.TryReadstring( AssemblyComponentMap.ascFK_r_ComponentGrouping);
			a.ascQuantity = reader.TryReaddecimal( AssemblyComponentMap.ascQuantity);
			a.ascLength = reader.TryReaddecimal( AssemblyComponentMap.ascLength);
			a.ascTopDepth = reader.TryReaddecimal( AssemblyComponentMap.ascTopDepth);
			a.ascBottomDepth = reader.TryReaddecimal( AssemblyComponentMap.ascBottomDepth);
			a.ascTrueVerticalDepth = reader.TryReaddecimal( AssemblyComponentMap.ascTrueVerticalDepth);
			a.ascTrueVerticalDepthBottom = reader.TryReaddecimal( AssemblyComponentMap.ascTrueVerticalDepthBottom);
			a.ascPreviousRunDays = reader.TryReaddecimal( AssemblyComponentMap.ascPreviousRunDays);
			a.ascPreviousRunDaysDT = reader.TryReadDateTime( AssemblyComponentMap.ascPreviousRunDaysDT);
			a.ascRemark = reader.TryReadstring( AssemblyComponentMap.ascRemark);
			a.ascFK_r_ComponentCategory_GUID = reader.TryReadGuid( AssemblyComponentMap.ascFK_r_ComponentCategory_GUID);
			a.ascFK_r_ComponentCategory = reader.TryReadstring( AssemblyComponentMap.ascFK_r_ComponentCategory);
    
            return a;
        }

   		public override void RegisterParams()
		{
			AssemblyComponent a = (AssemblyComponent)dataObj;
			this.AddParam( AssemblyComponentMap.ID, AssemblyComponentMap.Param.ID, a.ID );
			this.AddParam( AssemblyComponentMap.ascPrimaryKey, AssemblyComponentMap.Param.ascPrimaryKey, a.ascPrimaryKey );
			this.AddParam( AssemblyComponentMap.LastModified, AssemblyComponentMap.Param.LastModified, a.LastModified );
			this.AddParam( AssemblyComponentMap.ascLstChgUser, AssemblyComponentMap.Param.ascLstChgUser, a.ascLstChgUser );
			this.AddParam( AssemblyComponentMap.ascFK_Assembly_GUID, AssemblyComponentMap.Param.ascFK_Assembly_GUID, a.ascFK_Assembly_GUID );
			this.AddParam( AssemblyComponentMap.ascFK_Assembly, AssemblyComponentMap.Param.ascFK_Assembly, a.ascFK_Assembly );
			this.AddParam( AssemblyComponentMap.ascFK_Component_GUID, AssemblyComponentMap.Param.ascFK_Component_GUID, a.ascFK_Component_GUID );
			this.AddParam( AssemblyComponentMap.ascFK_Component, AssemblyComponentMap.Param.ascFK_Component, a.ascFK_Component );
			this.AddParam( AssemblyComponentMap.ascFK_Event_Beginning_GUID, AssemblyComponentMap.Param.ascFK_Event_Beginning_GUID, a.ascFK_Event_Beginning_GUID );
			this.AddParam( AssemblyComponentMap.ascFK_Event_Beginning, AssemblyComponentMap.Param.ascFK_Event_Beginning, a.ascFK_Event_Beginning );
			this.AddParam( AssemblyComponentMap.ascBegEventDT, AssemblyComponentMap.Param.ascBegEventDT, a.ascBegEventDT );
			this.AddParam( AssemblyComponentMap.ascFK_Event_Ending_GUID, AssemblyComponentMap.Param.ascFK_Event_Ending_GUID, a.ascFK_Event_Ending_GUID );
			this.AddParam( AssemblyComponentMap.ascFK_Event_Ending, AssemblyComponentMap.Param.ascFK_Event_Ending, a.ascFK_Event_Ending );
			this.AddParam( AssemblyComponentMap.ascEndEventDT, AssemblyComponentMap.Param.ascEndEventDT, a.ascEndEventDT );
			this.AddParam( AssemblyComponentMap.ascAssemblyOrder, AssemblyComponentMap.Param.ascAssemblyOrder, a.ascAssemblyOrder );
			this.AddParam( AssemblyComponentMap.ascFK_r_ComponentGrouping_GUID, AssemblyComponentMap.Param.ascFK_r_ComponentGrouping_GUID, a.ascFK_r_ComponentGrouping_GUID );
			this.AddParam( AssemblyComponentMap.ascFK_r_ComponentGrouping, AssemblyComponentMap.Param.ascFK_r_ComponentGrouping, a.ascFK_r_ComponentGrouping );
			this.AddParam( AssemblyComponentMap.ascQuantity, AssemblyComponentMap.Param.ascQuantity, a.ascQuantity );
			this.AddParam( AssemblyComponentMap.ascLength, AssemblyComponentMap.Param.ascLength, a.ascLength );
			this.AddParam( AssemblyComponentMap.ascTopDepth, AssemblyComponentMap.Param.ascTopDepth, a.ascTopDepth );
			this.AddParam( AssemblyComponentMap.ascBottomDepth, AssemblyComponentMap.Param.ascBottomDepth, a.ascBottomDepth );
			this.AddParam( AssemblyComponentMap.ascTrueVerticalDepth, AssemblyComponentMap.Param.ascTrueVerticalDepth, a.ascTrueVerticalDepth );
			this.AddParam( AssemblyComponentMap.ascTrueVerticalDepthBottom, AssemblyComponentMap.Param.ascTrueVerticalDepthBottom, a.ascTrueVerticalDepthBottom );
			this.AddParam( AssemblyComponentMap.ascPreviousRunDays, AssemblyComponentMap.Param.ascPreviousRunDays, a.ascPreviousRunDays );
			this.AddParam( AssemblyComponentMap.ascPreviousRunDaysDT, AssemblyComponentMap.Param.ascPreviousRunDaysDT, a.ascPreviousRunDaysDT );
			this.AddParam( AssemblyComponentMap.ascRemark, AssemblyComponentMap.Param.ascRemark, a.ascRemark );
			this.AddParam( AssemblyComponentMap.ascFK_r_ComponentCategory_GUID, AssemblyComponentMap.Param.ascFK_r_ComponentCategory_GUID, a.ascFK_r_ComponentCategory_GUID );
			this.AddParam( AssemblyComponentMap.ascFK_r_ComponentCategory, AssemblyComponentMap.Param.ascFK_r_ComponentCategory, a.ascFK_r_ComponentCategory );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the AssemblyComponentBblPlgMeasure table in the PumpServicing Database.
    /// </summary>
    public partial class AssemblyComponentBblPlgMeasureTransaction : BaseTransaction<AssemblyComponentBblPlgMeasure>
    {
        public AssemblyComponentBblPlgMeasureTransaction()
            : base(AssemblyComponentBblPlgMeasureMap.TABLE_NAME, AssemblyComponentBblPlgMeasureMap.ID)
        {
        }
        
        public AssemblyComponentBblPlgMeasureTransaction( IBaseData _type )
			: base( _type, AssemblyComponentBblPlgMeasureMap.TABLE_NAME, AssemblyComponentBblPlgMeasureMap.ID )
		{
        }

        public AssemblyComponentBblPlgMeasureTransaction( string _PrimaryKey )
			: base( AssemblyComponentBblPlgMeasureMap.TABLE_NAME, AssemblyComponentBblPlgMeasureMap.ID, _PrimaryKey )
		{
        }

        public AssemblyComponentBblPlgMeasureTransaction( DateTime _lastModified )
            : base( AssemblyComponentBblPlgMeasureMap.TABLE_NAME, AssemblyComponentBblPlgMeasureMap.ID, _lastModified, AssemblyComponentBblPlgMeasureMap.LastModified )
        {
        }				
    
        public override AssemblyComponentBblPlgMeasure BuildFromReader(TransactionReader reader)
        {
            AssemblyComponentBblPlgMeasure a = new AssemblyComponentBblPlgMeasure( );
			a.acmPrimaryKey_GUID = reader.TryReadGuid( AssemblyComponentBblPlgMeasureMap.ID);
			a.acmPrimaryKey = reader.TryReadstring( AssemblyComponentBblPlgMeasureMap.acmPrimaryKey);
			a.acmLstChgDT = reader.TryReadDateTime( AssemblyComponentBblPlgMeasureMap.LastModified);
			a.acmLstChgUser = reader.TryReadstring( AssemblyComponentBblPlgMeasureMap.acmLstChgUser);
			a.acmFK_AssemblyComponent_GUID = reader.TryReadGuid( AssemblyComponentBblPlgMeasureMap.acmFK_AssemblyComponent_GUID);
			a.acmFK_AssemblyComponent = reader.TryReadstring( AssemblyComponentBblPlgMeasureMap.acmFK_AssemblyComponent);
			a.acmFK_Event_GUID = reader.TryReadGuid( AssemblyComponentBblPlgMeasureMap.acmFK_Event_GUID);
			a.acmFK_Event = reader.TryReadstring( AssemblyComponentBblPlgMeasureMap.acmFK_Event);
			a.acmPosition = reader.TryReaddecimal( AssemblyComponentBblPlgMeasureMap.acmPosition);
			a.acmMeasurement = reader.TryReaddecimal( AssemblyComponentBblPlgMeasureMap.acmMeasurement);
    
            return a;
        }

   		public override void RegisterParams()
		{
			AssemblyComponentBblPlgMeasure a = (AssemblyComponentBblPlgMeasure)dataObj;
			this.AddParam( AssemblyComponentBblPlgMeasureMap.ID, AssemblyComponentBblPlgMeasureMap.Param.ID, a.ID );
			this.AddParam( AssemblyComponentBblPlgMeasureMap.acmPrimaryKey, AssemblyComponentBblPlgMeasureMap.Param.acmPrimaryKey, a.acmPrimaryKey );
			this.AddParam( AssemblyComponentBblPlgMeasureMap.LastModified, AssemblyComponentBblPlgMeasureMap.Param.LastModified, a.LastModified );
			this.AddParam( AssemblyComponentBblPlgMeasureMap.acmLstChgUser, AssemblyComponentBblPlgMeasureMap.Param.acmLstChgUser, a.acmLstChgUser );
			this.AddParam( AssemblyComponentBblPlgMeasureMap.acmFK_AssemblyComponent_GUID, AssemblyComponentBblPlgMeasureMap.Param.acmFK_AssemblyComponent_GUID, a.acmFK_AssemblyComponent_GUID );
			this.AddParam( AssemblyComponentBblPlgMeasureMap.acmFK_AssemblyComponent, AssemblyComponentBblPlgMeasureMap.Param.acmFK_AssemblyComponent, a.acmFK_AssemblyComponent );
			this.AddParam( AssemblyComponentBblPlgMeasureMap.acmFK_Event_GUID, AssemblyComponentBblPlgMeasureMap.Param.acmFK_Event_GUID, a.acmFK_Event_GUID );
			this.AddParam( AssemblyComponentBblPlgMeasureMap.acmFK_Event, AssemblyComponentBblPlgMeasureMap.Param.acmFK_Event, a.acmFK_Event );
			this.AddParam( AssemblyComponentBblPlgMeasureMap.acmPosition, AssemblyComponentBblPlgMeasureMap.Param.acmPosition, a.acmPosition );
			this.AddParam( AssemblyComponentBblPlgMeasureMap.acmMeasurement, AssemblyComponentBblPlgMeasureMap.Param.acmMeasurement, a.acmMeasurement );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the AssemblyComponentSRPump table in the PumpServicing Database.
    /// </summary>
    public partial class AssemblyComponentSRPumpTransaction : BaseTransaction<AssemblyComponentSRPump>
    {
        public AssemblyComponentSRPumpTransaction()
            : base(AssemblyComponentSRPumpMap.TABLE_NAME, AssemblyComponentSRPumpMap.ID)
        {
        }
        
        public AssemblyComponentSRPumpTransaction( IBaseData _type )
			: base( _type, AssemblyComponentSRPumpMap.TABLE_NAME, AssemblyComponentSRPumpMap.ID )
		{
        }

        public AssemblyComponentSRPumpTransaction( string _PrimaryKey )
			: base( AssemblyComponentSRPumpMap.TABLE_NAME, AssemblyComponentSRPumpMap.ID, _PrimaryKey )
		{
        }

        public AssemblyComponentSRPumpTransaction( DateTime _lastModified )
            : base( AssemblyComponentSRPumpMap.TABLE_NAME, AssemblyComponentSRPumpMap.ID, _lastModified, AssemblyComponentSRPumpMap.LastModified )
        {
        }				
    
        public override AssemblyComponentSRPump BuildFromReader(TransactionReader reader)
        {
            AssemblyComponentSRPump a = new AssemblyComponentSRPump( );
			a.arpPrimaryKey_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.ID);
			a.arpPrimaryKey = reader.TryReadstring( AssemblyComponentSRPumpMap.arpPrimaryKey);
			a.arpLstChgDT = reader.TryReadDateTime( AssemblyComponentSRPumpMap.LastModified);
			a.arpLstChgUser = reader.TryReadstring( AssemblyComponentSRPumpMap.arpLstChgUser);
			a.arpFK_AssemblyComponent_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_AssemblyComponent_GUID);
			a.arpFK_AssemblyComponent = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_AssemblyComponent);
			a.arpAPIDescription = reader.TryReadstring( AssemblyComponentSRPumpMap.arpAPIDescription);
			a.arpAPIExtraDescriptionText = reader.TryReadstring( AssemblyComponentSRPumpMap.arpAPIExtraDescriptionText);
			a.arpFK_r_APIPumpGraphics_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APIPumpGraphics_GUID);
			a.arpFK_r_APIPumpGraphics = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APIPumpGraphics);
			a.arpFK_r_APISRPTubingSize_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APISRPTubingSize_GUID);
			a.arpFK_r_APISRPTubingSize = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APISRPTubingSize);
			a.arpFK_r_APISRPPumpBore_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APISRPPumpBore_GUID);
			a.arpFK_r_APISRPPumpBore = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APISRPPumpBore);
			a.arpFK_r_APISRPPumpType_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APISRPPumpType_GUID);
			a.arpFK_r_APISRPPumpType = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APISRPPumpType);
			a.arpFK_r_APISRPBarrelType_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APISRPBarrelType_GUID);
			a.arpFK_r_APISRPBarrelType = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APISRPBarrelType);
			a.arpFK_r_APISRPSeatAssyLocation_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APISRPSeatAssyLocation_GUID);
			a.arpFK_r_APISRPSeatAssyLocation = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APISRPSeatAssyLocation);
			a.arpFK_r_APISRPSeatAssyType_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APISRPSeatAssyType_GUID);
			a.arpFK_r_APISRPSeatAssyType = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APISRPSeatAssyType);
			a.arpAPIBarrelLength = reader.TryReaddecimal( AssemblyComponentSRPumpMap.arpAPIBarrelLength);
			a.arpAPIPlungerLength = reader.TryReaddecimal( AssemblyComponentSRPumpMap.arpAPIPlungerLength);
			a.arpAPIExtensionCouplingUpperLength = reader.TryReaddecimal( AssemblyComponentSRPumpMap.arpAPIExtensionCouplingUpperLength);
			a.arpAPIExtensionCouplingLowerLength = reader.TryReaddecimal( AssemblyComponentSRPumpMap.arpAPIExtensionCouplingLowerLength);
			a.arpFK_r_APISRPExtPumpType_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtPumpType_GUID);
			a.arpFK_r_APISRPExtPumpType = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtPumpType);
			a.arpFK_r_APISRPExtBarrelType_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtBarrelType_GUID);
			a.arpFK_r_APISRPExtBarrelType = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtBarrelType);
			a.arpFK_r_APISRPExtSeatAssyLocation_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtSeatAssyLocation_GUID);
			a.arpFK_r_APISRPExtSeatAssyLocation = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtSeatAssyLocation);
			a.arpFK_r_APISRPExtSeatAssyType_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtSeatAssyType_GUID);
			a.arpFK_r_APISRPExtSeatAssyType = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtSeatAssyType);
			a.arpFK_r_APISRPExtSand_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtSand_GUID);
			a.arpFK_r_APISRPExtSand = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtSand);
			a.arpFK_r_APISRPExtBblAcc_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtBblAcc_GUID);
			a.arpFK_r_APISRPExtBblAcc = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtBblAcc);
			a.arpFK_r_APISRPExtPlgAcc_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtPlgAcc_GUID);
			a.arpFK_r_APISRPExtPlgAcc = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtPlgAcc);
			a.arpFK_r_APISRPExtPlgType_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtPlgType_GUID);
			a.arpFK_r_APISRPExtPlgType = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtPlgType);
			a.arpFK_r_APISRPExtPlgPin_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtPlgPin_GUID);
			a.arpFK_r_APISRPExtPlgPin = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtPlgPin);
			a.arpFK_r_APISRPExtSV_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtSV_GUID);
			a.arpFK_r_APISRPExtSV = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtSV);
			a.arpFK_r_APISRPExtSVCage_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtSVCage_GUID);
			a.arpFK_r_APISRPExtSVCage = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtSVCage);
			a.arpFK_r_APISRPExtTV_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtTV_GUID);
			a.arpFK_r_APISRPExtTV = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtTV);
			a.arpFK_r_APISRPExtTVCage_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtTVCage_GUID);
			a.arpFK_r_APISRPExtTVCage = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtTVCage);
			a.arpFK_r_APISRPExtTVStPlg_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtTVStPlg_GUID);
			a.arpFK_r_APISRPExtTVStPlg = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtTVStPlg);
			a.arpFK_r_APISRPExtVRod_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtVRod_GUID);
			a.arpFK_r_APISRPExtVRod = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtVRod);
			a.arpFK_r_APISRPExtWiper_GUID = reader.TryReadGuid( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtWiper_GUID);
			a.arpFK_r_APISRPExtWiper = reader.TryReadstring( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtWiper);
			a.arpBblPlgAvgClearance = reader.TryReaddecimal( AssemblyComponentSRPumpMap.arpBblPlgAvgClearance);
			a.arpMaxSL = reader.TryReaddecimal( AssemblyComponentSRPumpMap.arpMaxSL);
    
            return a;
        }

   		public override void RegisterParams()
		{
			AssemblyComponentSRPump a = (AssemblyComponentSRPump)dataObj;
			this.AddParam( AssemblyComponentSRPumpMap.ID, AssemblyComponentSRPumpMap.Param.ID, a.ID );
			this.AddParam( AssemblyComponentSRPumpMap.arpPrimaryKey, AssemblyComponentSRPumpMap.Param.arpPrimaryKey, a.arpPrimaryKey );
			this.AddParam( AssemblyComponentSRPumpMap.LastModified, AssemblyComponentSRPumpMap.Param.LastModified, a.LastModified );
			this.AddParam( AssemblyComponentSRPumpMap.arpLstChgUser, AssemblyComponentSRPumpMap.Param.arpLstChgUser, a.arpLstChgUser );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_AssemblyComponent_GUID, AssemblyComponentSRPumpMap.Param.arpFK_AssemblyComponent_GUID, a.arpFK_AssemblyComponent_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_AssemblyComponent, AssemblyComponentSRPumpMap.Param.arpFK_AssemblyComponent, a.arpFK_AssemblyComponent );
			this.AddParam( AssemblyComponentSRPumpMap.arpAPIDescription, AssemblyComponentSRPumpMap.Param.arpAPIDescription, a.arpAPIDescription );
			this.AddParam( AssemblyComponentSRPumpMap.arpAPIExtraDescriptionText, AssemblyComponentSRPumpMap.Param.arpAPIExtraDescriptionText, a.arpAPIExtraDescriptionText );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APIPumpGraphics_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APIPumpGraphics_GUID, a.arpFK_r_APIPumpGraphics_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APIPumpGraphics, AssemblyComponentSRPumpMap.Param.arpFK_r_APIPumpGraphics, a.arpFK_r_APIPumpGraphics );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPTubingSize_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPTubingSize_GUID, a.arpFK_r_APISRPTubingSize_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPTubingSize, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPTubingSize, a.arpFK_r_APISRPTubingSize );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPPumpBore_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPPumpBore_GUID, a.arpFK_r_APISRPPumpBore_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPPumpBore, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPPumpBore, a.arpFK_r_APISRPPumpBore );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPPumpType_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPPumpType_GUID, a.arpFK_r_APISRPPumpType_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPPumpType, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPPumpType, a.arpFK_r_APISRPPumpType );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPBarrelType_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPBarrelType_GUID, a.arpFK_r_APISRPBarrelType_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPBarrelType, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPBarrelType, a.arpFK_r_APISRPBarrelType );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPSeatAssyLocation_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPSeatAssyLocation_GUID, a.arpFK_r_APISRPSeatAssyLocation_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPSeatAssyLocation, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPSeatAssyLocation, a.arpFK_r_APISRPSeatAssyLocation );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPSeatAssyType_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPSeatAssyType_GUID, a.arpFK_r_APISRPSeatAssyType_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPSeatAssyType, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPSeatAssyType, a.arpFK_r_APISRPSeatAssyType );
			this.AddParam( AssemblyComponentSRPumpMap.arpAPIBarrelLength, AssemblyComponentSRPumpMap.Param.arpAPIBarrelLength, a.arpAPIBarrelLength );
			this.AddParam( AssemblyComponentSRPumpMap.arpAPIPlungerLength, AssemblyComponentSRPumpMap.Param.arpAPIPlungerLength, a.arpAPIPlungerLength );
			this.AddParam( AssemblyComponentSRPumpMap.arpAPIExtensionCouplingUpperLength, AssemblyComponentSRPumpMap.Param.arpAPIExtensionCouplingUpperLength, a.arpAPIExtensionCouplingUpperLength );
			this.AddParam( AssemblyComponentSRPumpMap.arpAPIExtensionCouplingLowerLength, AssemblyComponentSRPumpMap.Param.arpAPIExtensionCouplingLowerLength, a.arpAPIExtensionCouplingLowerLength );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtPumpType_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtPumpType_GUID, a.arpFK_r_APISRPExtPumpType_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtPumpType, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtPumpType, a.arpFK_r_APISRPExtPumpType );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtBarrelType_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtBarrelType_GUID, a.arpFK_r_APISRPExtBarrelType_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtBarrelType, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtBarrelType, a.arpFK_r_APISRPExtBarrelType );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtSeatAssyLocation_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtSeatAssyLocation_GUID, a.arpFK_r_APISRPExtSeatAssyLocation_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtSeatAssyLocation, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtSeatAssyLocation, a.arpFK_r_APISRPExtSeatAssyLocation );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtSeatAssyType_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtSeatAssyType_GUID, a.arpFK_r_APISRPExtSeatAssyType_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtSeatAssyType, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtSeatAssyType, a.arpFK_r_APISRPExtSeatAssyType );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtSand_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtSand_GUID, a.arpFK_r_APISRPExtSand_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtSand, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtSand, a.arpFK_r_APISRPExtSand );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtBblAcc_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtBblAcc_GUID, a.arpFK_r_APISRPExtBblAcc_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtBblAcc, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtBblAcc, a.arpFK_r_APISRPExtBblAcc );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtPlgAcc_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtPlgAcc_GUID, a.arpFK_r_APISRPExtPlgAcc_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtPlgAcc, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtPlgAcc, a.arpFK_r_APISRPExtPlgAcc );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtPlgType_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtPlgType_GUID, a.arpFK_r_APISRPExtPlgType_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtPlgType, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtPlgType, a.arpFK_r_APISRPExtPlgType );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtPlgPin_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtPlgPin_GUID, a.arpFK_r_APISRPExtPlgPin_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtPlgPin, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtPlgPin, a.arpFK_r_APISRPExtPlgPin );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtSV_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtSV_GUID, a.arpFK_r_APISRPExtSV_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtSV, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtSV, a.arpFK_r_APISRPExtSV );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtSVCage_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtSVCage_GUID, a.arpFK_r_APISRPExtSVCage_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtSVCage, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtSVCage, a.arpFK_r_APISRPExtSVCage );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtTV_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtTV_GUID, a.arpFK_r_APISRPExtTV_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtTV, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtTV, a.arpFK_r_APISRPExtTV );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtTVCage_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtTVCage_GUID, a.arpFK_r_APISRPExtTVCage_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtTVCage, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtTVCage, a.arpFK_r_APISRPExtTVCage );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtTVStPlg_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtTVStPlg_GUID, a.arpFK_r_APISRPExtTVStPlg_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtTVStPlg, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtTVStPlg, a.arpFK_r_APISRPExtTVStPlg );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtVRod_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtVRod_GUID, a.arpFK_r_APISRPExtVRod_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtVRod, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtVRod, a.arpFK_r_APISRPExtVRod );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtWiper_GUID, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtWiper_GUID, a.arpFK_r_APISRPExtWiper_GUID );
			this.AddParam( AssemblyComponentSRPumpMap.arpFK_r_APISRPExtWiper, AssemblyComponentSRPumpMap.Param.arpFK_r_APISRPExtWiper, a.arpFK_r_APISRPExtWiper );
			this.AddParam( AssemblyComponentSRPumpMap.arpBblPlgAvgClearance, AssemblyComponentSRPumpMap.Param.arpBblPlgAvgClearance, a.arpBblPlgAvgClearance );
			this.AddParam( AssemblyComponentSRPumpMap.arpMaxSL, AssemblyComponentSRPumpMap.Param.arpMaxSL, a.arpMaxSL );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the BusinessOrganization table in the PumpServicing Database.
    /// </summary>
    public partial class BusinessOrganizationTransaction : BaseTransaction<BusinessOrganization>
    {
        public BusinessOrganizationTransaction()
            : base(BusinessOrganizationMap.TABLE_NAME, BusinessOrganizationMap.ID)
        {
        }
        
        public BusinessOrganizationTransaction( IBaseData _type )
			: base( _type, BusinessOrganizationMap.TABLE_NAME, BusinessOrganizationMap.ID )
		{
        }

        public BusinessOrganizationTransaction( string _PrimaryKey )
			: base( BusinessOrganizationMap.TABLE_NAME, BusinessOrganizationMap.ID, _PrimaryKey )
		{
        }

        public BusinessOrganizationTransaction( DateTime _lastModified )
            : base( BusinessOrganizationMap.TABLE_NAME, BusinessOrganizationMap.ID, _lastModified, BusinessOrganizationMap.LastModified )
        {
        }				
    
        public override BusinessOrganization BuildFromReader(TransactionReader reader)
        {
            BusinessOrganization a = new BusinessOrganization( );
			a.venPrimaryKey_GUID = reader.TryReadGuid( BusinessOrganizationMap.ID);
			a.venPrimaryKey = reader.TryReadstring( BusinessOrganizationMap.venPrimaryKey);
			a.venLanguageCd = reader.TryReadstring( BusinessOrganizationMap.venLanguageCd);
			a.venLstChgDT = reader.TryReadDateTime( BusinessOrganizationMap.LastModified);
			a.venLstChgUser = reader.TryReadstring( BusinessOrganizationMap.venLstChgUser);
			a.venRefUserDeleted = reader.TryReadbool( BusinessOrganizationMap.venRefUserDeleted);
			a.venRefCaseDefined = reader.TryReadbool( BusinessOrganizationMap.venRefCaseDefined);
			a.venFK_r_BusinessOrganizationType_GUID = reader.TryReadGuid( BusinessOrganizationMap.venFK_r_BusinessOrganizationType_GUID);
			a.venFK_r_BusinessOrganizationType = reader.TryReadstring( BusinessOrganizationMap.venFK_r_BusinessOrganizationType);
			a.venBusinessOrganizationAbrev = reader.TryReadstring( BusinessOrganizationMap.venBusinessOrganizationAbrev);
			a.venBusinessOrganizationName = reader.TryReadstring( BusinessOrganizationMap.venBusinessOrganizationName);
			a.venPT_TaxableStatus = reader.TryReadbool( BusinessOrganizationMap.venPT_TaxableStatus);
			a.venPT_TaxRate = reader.TryReaddecimal( BusinessOrganizationMap.venPT_TaxRate);
			a.venPT_InvoiceComments = reader.TryReadstring( BusinessOrganizationMap.venPT_InvoiceComments);
			a.venPT_DiscountRateRepair = reader.TryReaddecimal( BusinessOrganizationMap.venPT_DiscountRateRepair);
			a.venPT_DiscountRateNew = reader.TryReaddecimal( BusinessOrganizationMap.venPT_DiscountRateNew);
			a.venPT_SalesRep = reader.TryReadstring( BusinessOrganizationMap.venPT_SalesRep);
			a.venAccountingID = reader.TryReadstring( BusinessOrganizationMap.venAccountingID);
			a.venInactive = reader.TryReadbool( BusinessOrganizationMap.venInactive);
    
            return a;
        }

   		public override void RegisterParams()
		{
			BusinessOrganization a = (BusinessOrganization)dataObj;
			this.AddParam( BusinessOrganizationMap.ID, BusinessOrganizationMap.Param.ID, a.ID );
			this.AddParam( BusinessOrganizationMap.venPrimaryKey, BusinessOrganizationMap.Param.venPrimaryKey, a.venPrimaryKey );
			this.AddParam( BusinessOrganizationMap.venLanguageCd, BusinessOrganizationMap.Param.venLanguageCd, a.venLanguageCd );
			this.AddParam( BusinessOrganizationMap.LastModified, BusinessOrganizationMap.Param.LastModified, a.LastModified );
			this.AddParam( BusinessOrganizationMap.venLstChgUser, BusinessOrganizationMap.Param.venLstChgUser, a.venLstChgUser );
			this.AddParam( BusinessOrganizationMap.venRefUserDeleted, BusinessOrganizationMap.Param.venRefUserDeleted, a.venRefUserDeleted );
			this.AddParam( BusinessOrganizationMap.venRefCaseDefined, BusinessOrganizationMap.Param.venRefCaseDefined, a.venRefCaseDefined );
			this.AddParam( BusinessOrganizationMap.venFK_r_BusinessOrganizationType_GUID, BusinessOrganizationMap.Param.venFK_r_BusinessOrganizationType_GUID, a.venFK_r_BusinessOrganizationType_GUID );
			this.AddParam( BusinessOrganizationMap.venFK_r_BusinessOrganizationType, BusinessOrganizationMap.Param.venFK_r_BusinessOrganizationType, a.venFK_r_BusinessOrganizationType );
			this.AddParam( BusinessOrganizationMap.venBusinessOrganizationAbrev, BusinessOrganizationMap.Param.venBusinessOrganizationAbrev, a.venBusinessOrganizationAbrev );
			this.AddParam( BusinessOrganizationMap.venBusinessOrganizationName, BusinessOrganizationMap.Param.venBusinessOrganizationName, a.venBusinessOrganizationName );
			this.AddParam( BusinessOrganizationMap.venPT_TaxableStatus, BusinessOrganizationMap.Param.venPT_TaxableStatus, a.venPT_TaxableStatus );
			this.AddParam( BusinessOrganizationMap.venPT_TaxRate, BusinessOrganizationMap.Param.venPT_TaxRate, a.venPT_TaxRate );
			this.AddParam( BusinessOrganizationMap.venPT_InvoiceComments, BusinessOrganizationMap.Param.venPT_InvoiceComments, a.venPT_InvoiceComments );
			this.AddParam( BusinessOrganizationMap.venPT_DiscountRateRepair, BusinessOrganizationMap.Param.venPT_DiscountRateRepair, a.venPT_DiscountRateRepair );
			this.AddParam( BusinessOrganizationMap.venPT_DiscountRateNew, BusinessOrganizationMap.Param.venPT_DiscountRateNew, a.venPT_DiscountRateNew );
			this.AddParam( BusinessOrganizationMap.venPT_SalesRep, BusinessOrganizationMap.Param.venPT_SalesRep, a.venPT_SalesRep );
			this.AddParam( BusinessOrganizationMap.venAccountingID, BusinessOrganizationMap.Param.venAccountingID, a.venAccountingID );
			this.AddParam( BusinessOrganizationMap.venInactive, BusinessOrganizationMap.Param.venInactive, a.venInactive );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the Component table in the PumpServicing Database.
    /// </summary>
    public partial class ComponentTransaction : BaseTransaction<Component>
    {
        public ComponentTransaction()
            : base(ComponentMap.TABLE_NAME, ComponentMap.ID)
        {
        }
        
        public ComponentTransaction( IBaseData _type )
			: base( _type, ComponentMap.TABLE_NAME, ComponentMap.ID )
		{
        }

        public ComponentTransaction( string _PrimaryKey )
			: base( ComponentMap.TABLE_NAME, ComponentMap.ID, _PrimaryKey )
		{
        }

        public ComponentTransaction( DateTime _lastModified )
            : base( ComponentMap.TABLE_NAME, ComponentMap.ID, _lastModified, ComponentMap.LastModified )
        {
        }				
    
        public override Component BuildFromReader(TransactionReader reader)
        {
            Component a = new Component( );
			a.cmcPrimaryKey_GUID = reader.TryReadGuid( ComponentMap.ID);
			a.cmcPrimaryKey = reader.TryReadstring( ComponentMap.cmcPrimaryKey);
			a.cmcLstChgDT = reader.TryReadDateTime( ComponentMap.LastModified);
			a.cmcLstChgUser = reader.TryReadstring( ComponentMap.cmcLstChgUser);
			a.cmcRefCaseDefined = reader.TryReadbool( ComponentMap.cmcRefCaseDefined);
			a.cmcFK_Assembly_GUID = reader.TryReadGuid( ComponentMap.cmcFK_Assembly_GUID);
			a.cmcFK_Assembly = reader.TryReadstring( ComponentMap.cmcFK_Assembly);
			a.cmcFK_r_CatalogItem_GUID = reader.TryReadGuid( ComponentMap.cmcFK_r_CatalogItem_GUID);
			a.cmcFK_r_CatalogItem = reader.TryReadstring( ComponentMap.cmcFK_r_CatalogItem);
			a.cmcFK_r_MfgCatalogItem_GUID = reader.TryReadGuid( ComponentMap.cmcFK_r_MfgCatalogItem_GUID);
			a.cmcFK_r_MfgCatalogItem = reader.TryReadstring( ComponentMap.cmcFK_r_MfgCatalogItem);
			a.cmcSerialNo = reader.TryReadstring( ComponentMap.cmcSerialNo);
			a.cmcFK_r_ComponentOrigin_GUID = reader.TryReadGuid( ComponentMap.cmcFK_r_ComponentOrigin_GUID);
			a.cmcFK_r_ComponentOrigin = reader.TryReadstring( ComponentMap.cmcFK_r_ComponentOrigin);
			a.cmcPreviousRunDays = reader.TryReaddecimal( ComponentMap.cmcPreviousRunDays);
			a.cmcFK_BusinessOrganization_GUID = reader.TryReadGuid( ComponentMap.cmcFK_BusinessOrganization_GUID);
			a.cmcFK_BusinessOrganization = reader.TryReadstring( ComponentMap.cmcFK_BusinessOrganization);
			a.cmcOriginKey = reader.TryReadstring( ComponentMap.cmcOriginKey);
			a.cmcDiscontinueUse = reader.TryReadbool( ComponentMap.cmcDiscontinueUse);
    
            return a;
        }

   		public override void RegisterParams()
		{
			Component a = (Component)dataObj;
			this.AddParam( ComponentMap.ID, ComponentMap.Param.ID, a.ID );
			this.AddParam( ComponentMap.cmcPrimaryKey, ComponentMap.Param.cmcPrimaryKey, a.cmcPrimaryKey );
			this.AddParam( ComponentMap.LastModified, ComponentMap.Param.LastModified, a.LastModified );
			this.AddParam( ComponentMap.cmcLstChgUser, ComponentMap.Param.cmcLstChgUser, a.cmcLstChgUser );
			this.AddParam( ComponentMap.cmcRefCaseDefined, ComponentMap.Param.cmcRefCaseDefined, a.cmcRefCaseDefined );
			this.AddParam( ComponentMap.cmcFK_Assembly_GUID, ComponentMap.Param.cmcFK_Assembly_GUID, a.cmcFK_Assembly_GUID );
			this.AddParam( ComponentMap.cmcFK_Assembly, ComponentMap.Param.cmcFK_Assembly, a.cmcFK_Assembly );
			this.AddParam( ComponentMap.cmcFK_r_CatalogItem_GUID, ComponentMap.Param.cmcFK_r_CatalogItem_GUID, a.cmcFK_r_CatalogItem_GUID );
			this.AddParam( ComponentMap.cmcFK_r_CatalogItem, ComponentMap.Param.cmcFK_r_CatalogItem, a.cmcFK_r_CatalogItem );
			this.AddParam( ComponentMap.cmcFK_r_MfgCatalogItem_GUID, ComponentMap.Param.cmcFK_r_MfgCatalogItem_GUID, a.cmcFK_r_MfgCatalogItem_GUID );
			this.AddParam( ComponentMap.cmcFK_r_MfgCatalogItem, ComponentMap.Param.cmcFK_r_MfgCatalogItem, a.cmcFK_r_MfgCatalogItem );
			this.AddParam( ComponentMap.cmcSerialNo, ComponentMap.Param.cmcSerialNo, a.cmcSerialNo );
			this.AddParam( ComponentMap.cmcFK_r_ComponentOrigin_GUID, ComponentMap.Param.cmcFK_r_ComponentOrigin_GUID, a.cmcFK_r_ComponentOrigin_GUID );
			this.AddParam( ComponentMap.cmcFK_r_ComponentOrigin, ComponentMap.Param.cmcFK_r_ComponentOrigin, a.cmcFK_r_ComponentOrigin );
			this.AddParam( ComponentMap.cmcPreviousRunDays, ComponentMap.Param.cmcPreviousRunDays, a.cmcPreviousRunDays );
			this.AddParam( ComponentMap.cmcFK_BusinessOrganization_GUID, ComponentMap.Param.cmcFK_BusinessOrganization_GUID, a.cmcFK_BusinessOrganization_GUID );
			this.AddParam( ComponentMap.cmcFK_BusinessOrganization, ComponentMap.Param.cmcFK_BusinessOrganization, a.cmcFK_BusinessOrganization );
			this.AddParam( ComponentMap.cmcOriginKey, ComponentMap.Param.cmcOriginKey, a.cmcOriginKey );
			this.AddParam( ComponentMap.cmcDiscontinueUse, ComponentMap.Param.cmcDiscontinueUse, a.cmcDiscontinueUse );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the ComponentSRPump table in the PumpServicing Database.
    /// </summary>
    public partial class ComponentSRPumpTransaction : BaseTransaction<ComponentSRPump>
    {
        public ComponentSRPumpTransaction()
            : base(ComponentSRPumpMap.TABLE_NAME, ComponentSRPumpMap.ID)
        {
        }
        
        public ComponentSRPumpTransaction( IBaseData _type )
			: base( _type, ComponentSRPumpMap.TABLE_NAME, ComponentSRPumpMap.ID )
		{
        }

        public ComponentSRPumpTransaction( string _PrimaryKey )
			: base( ComponentSRPumpMap.TABLE_NAME, ComponentSRPumpMap.ID, _PrimaryKey )
		{
        }

        public ComponentSRPumpTransaction( DateTime _lastModified )
            : base( ComponentSRPumpMap.TABLE_NAME, ComponentSRPumpMap.ID, _lastModified, ComponentSRPumpMap.LastModified )
        {
        }				
    
        public override ComponentSRPump BuildFromReader(TransactionReader reader)
        {
            ComponentSRPump a = new ComponentSRPump( );
			a.cspPrimaryKey_GUID = reader.TryReadGuid( ComponentSRPumpMap.ID);
			a.cspPrimaryKey = reader.TryReadstring( ComponentSRPumpMap.cspPrimaryKey);
			a.cspLstChgDT = reader.TryReadDateTime( ComponentSRPumpMap.LastModified);
			a.cspLstChgUser = reader.TryReadstring( ComponentSRPumpMap.cspLstChgUser);
			a.cspFK_Component_GUID = reader.TryReadGuid( ComponentSRPumpMap.cspFK_Component_GUID);
			a.cspFK_Component = reader.TryReadstring( ComponentSRPumpMap.cspFK_Component);
			a.cspFK_BusinessOrganization_GUID = reader.TryReadGuid( ComponentSRPumpMap.cspFK_BusinessOrganization_GUID);
			a.cspFK_BusinessOrganization = reader.TryReadstring( ComponentSRPumpMap.cspFK_BusinessOrganization);
    
            return a;
        }

   		public override void RegisterParams()
		{
			ComponentSRPump a = (ComponentSRPump)dataObj;
			this.AddParam( ComponentSRPumpMap.ID, ComponentSRPumpMap.Param.ID, a.ID );
			this.AddParam( ComponentSRPumpMap.cspPrimaryKey, ComponentSRPumpMap.Param.cspPrimaryKey, a.cspPrimaryKey );
			this.AddParam( ComponentSRPumpMap.LastModified, ComponentSRPumpMap.Param.LastModified, a.LastModified );
			this.AddParam( ComponentSRPumpMap.cspLstChgUser, ComponentSRPumpMap.Param.cspLstChgUser, a.cspLstChgUser );
			this.AddParam( ComponentSRPumpMap.cspFK_Component_GUID, ComponentSRPumpMap.Param.cspFK_Component_GUID, a.cspFK_Component_GUID );
			this.AddParam( ComponentSRPumpMap.cspFK_Component, ComponentSRPumpMap.Param.cspFK_Component, a.cspFK_Component );
			this.AddParam( ComponentSRPumpMap.cspFK_BusinessOrganization_GUID, ComponentSRPumpMap.Param.cspFK_BusinessOrganization_GUID, a.cspFK_BusinessOrganization_GUID );
			this.AddParam( ComponentSRPumpMap.cspFK_BusinessOrganization, ComponentSRPumpMap.Param.cspFK_BusinessOrganization, a.cspFK_BusinessOrganization );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the DatabaseConfiguration table in the PumpServicing Database.
    /// </summary>
    public partial class DatabaseConfigurationTransaction : BaseTransaction<DatabaseConfiguration>
    {
        public DatabaseConfigurationTransaction()
            : base(DatabaseConfigurationMap.TABLE_NAME, DatabaseConfigurationMap.ID)
        {
        }
        
        public DatabaseConfigurationTransaction( IBaseData _type )
			: base( _type, DatabaseConfigurationMap.TABLE_NAME, DatabaseConfigurationMap.ID )
		{
        }

        public DatabaseConfigurationTransaction( string _PrimaryKey )
			: base( DatabaseConfigurationMap.TABLE_NAME, DatabaseConfigurationMap.ID, _PrimaryKey )
		{
        }

        public DatabaseConfigurationTransaction( DateTime _lastModified )
            : base( DatabaseConfigurationMap.TABLE_NAME, DatabaseConfigurationMap.ID, _lastModified, DatabaseConfigurationMap.LastModified )
        {
        }				
    
        public override DatabaseConfiguration BuildFromReader(TransactionReader reader)
        {
            DatabaseConfiguration a = new DatabaseConfiguration( );
			a.dbcPrimaryKey_GUID = reader.TryReadGuid( DatabaseConfigurationMap.ID);
			a.dbcPrimaryKey = reader.TryReadstring( DatabaseConfigurationMap.dbcPrimaryKey);
			a.dbcLstChgDT = reader.TryReadDateTime( DatabaseConfigurationMap.LastModified);
			a.dbcLstChgUser = reader.TryReadstring( DatabaseConfigurationMap.dbcLstChgUser);
			a.dbcDescription = reader.TryReadstring( DatabaseConfigurationMap.dbcDescription);
			a.dbcValue = reader.TryReadstring( DatabaseConfigurationMap.dbcValue);
    
            return a;
        }

   		public override void RegisterParams()
		{
			DatabaseConfiguration a = (DatabaseConfiguration)dataObj;
			this.AddParam( DatabaseConfigurationMap.ID, DatabaseConfigurationMap.Param.ID, a.ID );
			this.AddParam( DatabaseConfigurationMap.dbcPrimaryKey, DatabaseConfigurationMap.Param.dbcPrimaryKey, a.dbcPrimaryKey );
			this.AddParam( DatabaseConfigurationMap.LastModified, DatabaseConfigurationMap.Param.LastModified, a.LastModified );
			this.AddParam( DatabaseConfigurationMap.dbcLstChgUser, DatabaseConfigurationMap.Param.dbcLstChgUser, a.dbcLstChgUser );
			this.AddParam( DatabaseConfigurationMap.dbcDescription, DatabaseConfigurationMap.Param.dbcDescription, a.dbcDescription );
			this.AddParam( DatabaseConfigurationMap.dbcValue, DatabaseConfigurationMap.Param.dbcValue, a.dbcValue );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the DeletedLog table in the PumpServicing Database.
    /// </summary>
    public partial class DeletedLogTransaction : BaseTransaction<DeletedLog>
    {
        public DeletedLogTransaction()
            : base(DeletedLogMap.TABLE_NAME, DeletedLogMap.ID)
        {
        }
        
        public DeletedLogTransaction( IBaseData _type )
			: base( _type, DeletedLogMap.TABLE_NAME, DeletedLogMap.ID )
		{
        }

        public DeletedLogTransaction( string _PrimaryKey )
			: base( DeletedLogMap.TABLE_NAME, DeletedLogMap.ID, _PrimaryKey )
		{
        }

        public DeletedLogTransaction( DateTime _lastModified )
            : base( DeletedLogMap.TABLE_NAME, DeletedLogMap.ID, _lastModified, DeletedLogMap.LastModified )
        {
        }				
    
        public override DeletedLog BuildFromReader(TransactionReader reader)
        {
            DeletedLog a = new DeletedLog( );
			a.delPrimaryKey_GUID = reader.TryReadGuid( DeletedLogMap.ID);
			a.delPrimaryKey = reader.TryReadstring( DeletedLogMap.delPrimaryKey);
			a.delLstChgDT = reader.TryReadDateTime( DeletedLogMap.LastModified);
			a.delLstChgUser = reader.TryReadstring( DeletedLogMap.delLstChgUser);
			a.delFK_csDBEntity = reader.TryReadstring( DeletedLogMap.delFK_csDBEntity);
			a.delDeletedKey = reader.TryReadstring( DeletedLogMap.delDeletedKey);
			a.delDataXML = reader.TryReadbyteArray( DeletedLogMap.delDataXML);
			a.delRemark = reader.TryReadstring( DeletedLogMap.delRemark);
    
            return a;
        }

   		public override void RegisterParams()
		{
			DeletedLog a = (DeletedLog)dataObj;
			this.AddParam( DeletedLogMap.ID, DeletedLogMap.Param.ID, a.ID );
			this.AddParam( DeletedLogMap.delPrimaryKey, DeletedLogMap.Param.delPrimaryKey, a.delPrimaryKey );
			this.AddParam( DeletedLogMap.LastModified, DeletedLogMap.Param.LastModified, a.LastModified );
			this.AddParam( DeletedLogMap.delLstChgUser, DeletedLogMap.Param.delLstChgUser, a.delLstChgUser );
			this.AddParam( DeletedLogMap.delFK_csDBEntity, DeletedLogMap.Param.delFK_csDBEntity, a.delFK_csDBEntity );
			this.AddParam( DeletedLogMap.delDeletedKey, DeletedLogMap.Param.delDeletedKey, a.delDeletedKey );
			this.AddParam( DeletedLogMap.delDataXML, DeletedLogMap.Param.delDataXML, a.delDataXML );
			this.AddParam( DeletedLogMap.delRemark, DeletedLogMap.Param.delRemark, a.delRemark );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the Document table in the PumpServicing Database.
    /// </summary>
    public partial class DocumentTransaction : BaseTransaction<Document>
    {
        public DocumentTransaction()
            : base(DocumentMap.TABLE_NAME, DocumentMap.ID)
        {
        }
        
        public DocumentTransaction( IBaseData _type )
			: base( _type, DocumentMap.TABLE_NAME, DocumentMap.ID )
		{
        }

        public DocumentTransaction( string _PrimaryKey )
			: base( DocumentMap.TABLE_NAME, DocumentMap.ID, _PrimaryKey )
		{
        }

        public DocumentTransaction( DateTime _lastModified )
            : base( DocumentMap.TABLE_NAME, DocumentMap.ID, _lastModified, DocumentMap.LastModified )
        {
        }				
    
        public override Document BuildFromReader(TransactionReader reader)
        {
            Document a = new Document( );
			a.docPrimaryKey_GUID = reader.TryReadGuid( DocumentMap.ID);
			a.docPrimaryKey = reader.TryReadstring( DocumentMap.docPrimaryKey);
			a.docLstChgDT = reader.TryReadDateTime( DocumentMap.LastModified);
			a.docLstChgUser = reader.TryReadstring( DocumentMap.docLstChgUser);
			a.docRefCaseDefined = reader.TryReadbool( DocumentMap.docRefCaseDefined);
			a.docFK_r_DocumentType = reader.TryReadstring( DocumentMap.docFK_r_DocumentType);
			a.docDocID = reader.TryReadint( DocumentMap.docDocID);
			a.docBriefDescription = reader.TryReadstring( DocumentMap.docBriefDescription);
			a.docDocServerPath = reader.TryReadstring( DocumentMap.docDocServerPath);
			a.docDocServerFileName = reader.TryReadstring( DocumentMap.docDocServerFileName);
			a.docOriginalFileName = reader.TryReadstring( DocumentMap.docOriginalFileName);
			a.docFolderName = reader.TryReadstring( DocumentMap.docFolderName);
			a.docCurrentVersion = reader.TryReadint( DocumentMap.docCurrentVersion);
			a.docInUse = reader.TryReadbool( DocumentMap.docInUse);
			a.docCheckedOut = reader.TryReadbool( DocumentMap.docCheckedOut);
			a.docCheckoutUser = reader.TryReadstring( DocumentMap.docCheckoutUser);
			a.docCheckoutDT = reader.TryReadDateTime( DocumentMap.docCheckoutDT);
			a.docAPI10 = reader.TryReadstring( DocumentMap.docAPI10);
			a.docAPI12 = reader.TryReadstring( DocumentMap.docAPI12);
			a.docAPI14 = reader.TryReadstring( DocumentMap.docAPI14);
			a.docProducer = reader.TryReadstring( DocumentMap.docProducer);
			a.docField = reader.TryReadstring( DocumentMap.docField);
			a.docFacility = reader.TryReadstring( DocumentMap.docFacility);
			a.docFK_EventCategory = reader.TryReadstring( DocumentMap.docFK_EventCategory);
			a.docFK_Event = reader.TryReadstring( DocumentMap.docFK_Event);
			a.docFK_Component = reader.TryReadstring( DocumentMap.docFK_Component);
			a.docUserDef01 = reader.TryReadstring( DocumentMap.docUserDef01);
			a.docUserDef02 = reader.TryReadstring( DocumentMap.docUserDef02);
			a.docUserDef03 = reader.TryReadstring( DocumentMap.docUserDef03);
    
            return a;
        }

   		public override void RegisterParams()
		{
			Document a = (Document)dataObj;
			this.AddParam( DocumentMap.ID, DocumentMap.Param.ID, a.ID );
			this.AddParam( DocumentMap.docPrimaryKey, DocumentMap.Param.docPrimaryKey, a.docPrimaryKey );
			this.AddParam( DocumentMap.LastModified, DocumentMap.Param.LastModified, a.LastModified );
			this.AddParam( DocumentMap.docLstChgUser, DocumentMap.Param.docLstChgUser, a.docLstChgUser );
			this.AddParam( DocumentMap.docRefCaseDefined, DocumentMap.Param.docRefCaseDefined, a.docRefCaseDefined );
			this.AddParam( DocumentMap.docFK_r_DocumentType, DocumentMap.Param.docFK_r_DocumentType, a.docFK_r_DocumentType );
			this.AddParam( DocumentMap.docDocID, DocumentMap.Param.docDocID, a.docDocID );
			this.AddParam( DocumentMap.docBriefDescription, DocumentMap.Param.docBriefDescription, a.docBriefDescription );
			this.AddParam( DocumentMap.docDocServerPath, DocumentMap.Param.docDocServerPath, a.docDocServerPath );
			this.AddParam( DocumentMap.docDocServerFileName, DocumentMap.Param.docDocServerFileName, a.docDocServerFileName );
			this.AddParam( DocumentMap.docOriginalFileName, DocumentMap.Param.docOriginalFileName, a.docOriginalFileName );
			this.AddParam( DocumentMap.docFolderName, DocumentMap.Param.docFolderName, a.docFolderName );
			this.AddParam( DocumentMap.docCurrentVersion, DocumentMap.Param.docCurrentVersion, a.docCurrentVersion );
			this.AddParam( DocumentMap.docInUse, DocumentMap.Param.docInUse, a.docInUse );
			this.AddParam( DocumentMap.docCheckedOut, DocumentMap.Param.docCheckedOut, a.docCheckedOut );
			this.AddParam( DocumentMap.docCheckoutUser, DocumentMap.Param.docCheckoutUser, a.docCheckoutUser );
			this.AddParam( DocumentMap.docCheckoutDT, DocumentMap.Param.docCheckoutDT, a.docCheckoutDT );
			this.AddParam( DocumentMap.docAPI10, DocumentMap.Param.docAPI10, a.docAPI10 );
			this.AddParam( DocumentMap.docAPI12, DocumentMap.Param.docAPI12, a.docAPI12 );
			this.AddParam( DocumentMap.docAPI14, DocumentMap.Param.docAPI14, a.docAPI14 );
			this.AddParam( DocumentMap.docProducer, DocumentMap.Param.docProducer, a.docProducer );
			this.AddParam( DocumentMap.docField, DocumentMap.Param.docField, a.docField );
			this.AddParam( DocumentMap.docFacility, DocumentMap.Param.docFacility, a.docFacility );
			this.AddParam( DocumentMap.docFK_EventCategory, DocumentMap.Param.docFK_EventCategory, a.docFK_EventCategory );
			this.AddParam( DocumentMap.docFK_Event, DocumentMap.Param.docFK_Event, a.docFK_Event );
			this.AddParam( DocumentMap.docFK_Component, DocumentMap.Param.docFK_Component, a.docFK_Component );
			this.AddParam( DocumentMap.docUserDef01, DocumentMap.Param.docUserDef01, a.docUserDef01 );
			this.AddParam( DocumentMap.docUserDef02, DocumentMap.Param.docUserDef02, a.docUserDef02 );
			this.AddParam( DocumentMap.docUserDef03, DocumentMap.Param.docUserDef03, a.docUserDef03 );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the Event table in the PumpServicing Database.
    /// </summary>
    public partial class EventTransaction : BaseTransaction<Event>
    {
        public EventTransaction()
            : base(EventMap.TABLE_NAME, EventMap.ID)
        {
        }
        
        public EventTransaction( IBaseData _type )
			: base( _type, EventMap.TABLE_NAME, EventMap.ID )
		{
        }

        public EventTransaction( string _PrimaryKey )
			: base( EventMap.TABLE_NAME, EventMap.ID, _PrimaryKey )
		{
        }

        public EventTransaction( DateTime _lastModified )
            : base( EventMap.TABLE_NAME, EventMap.ID, _lastModified, EventMap.LastModified )
        {
        }				
    
        public override Event BuildFromReader(TransactionReader reader)
        {
            Event a = new Event( );
			a.evcPrimaryKey_GUID = reader.TryReadGuid( EventMap.ID);
			a.evcPrimaryKey = reader.TryReadstring( EventMap.evcPrimaryKey);
			a.evcLstChgDT = reader.TryReadDateTime( EventMap.LastModified);
			a.evcLstChgUser = reader.TryReadstring( EventMap.evcLstChgUser);
			a.evcRefCaseDefined = reader.TryReadbool( EventMap.evcRefCaseDefined);
			a.evcFK_Job_GUID = reader.TryReadGuid( EventMap.evcFK_Job_GUID);
			a.evcFK_EventCategory = reader.TryReadstring( EventMap.evcFK_EventCategory);
			a.evcFK_Workorder_GUID = reader.TryReadGuid( EventMap.evcFK_Workorder_GUID);
			a.evcFK_Workorder = reader.TryReadstring( EventMap.evcFK_Workorder);
			a.evcFK_r_EventType_GUID = reader.TryReadGuid( EventMap.evcFK_r_EventType_GUID);
			a.evcFK_r_EventType = reader.TryReadstring( EventMap.evcFK_r_EventType);
			a.evcEventBegDtTm = reader.TryReadDateTime( EventMap.evcEventBegDtTm);
			a.evcEventEndDtTm = reader.TryReadDateTime( EventMap.evcEventEndDtTm);
			a.evcDurationHours = reader.TryReaddecimal( EventMap.evcDurationHours);
			a.evcEventOrder = reader.TryReaddecimal( EventMap.evcEventOrder);
			a.evcFK_Assembly_GUID = reader.TryReadGuid( EventMap.evcFK_Assembly_GUID);
			a.evcFK_Assembly = reader.TryReadstring( EventMap.evcFK_Assembly);
			a.evcResponsiblePerson = reader.TryReadstring( EventMap.evcResponsiblePerson);
			a.evcFK_BusinessOrganization_GUID = reader.TryReadGuid( EventMap.evcFK_BusinessOrganization_GUID);
			a.evcFK_BusinessOrganization = reader.TryReadstring( EventMap.evcFK_BusinessOrganization);
			a.evcPersonPerformingTask = reader.TryReadstring( EventMap.evcPersonPerformingTask);
			a.evcOriginKey = reader.TryReadstring( EventMap.evcOriginKey);
			a.evcRemarks = reader.TryReadstring( EventMap.evcRemarks);
    
            return a;
        }

   		public override void RegisterParams()
		{
			Event a = (Event)dataObj;
			this.AddParam( EventMap.ID, EventMap.Param.ID, a.ID );
			this.AddParam( EventMap.evcPrimaryKey, EventMap.Param.evcPrimaryKey, a.evcPrimaryKey );
			this.AddParam( EventMap.LastModified, EventMap.Param.LastModified, a.LastModified );
			this.AddParam( EventMap.evcLstChgUser, EventMap.Param.evcLstChgUser, a.evcLstChgUser );
			this.AddParam( EventMap.evcRefCaseDefined, EventMap.Param.evcRefCaseDefined, a.evcRefCaseDefined );
			this.AddParam( EventMap.evcFK_Job_GUID, EventMap.Param.evcFK_Job_GUID, a.evcFK_Job_GUID );
			this.AddParam( EventMap.evcFK_EventCategory, EventMap.Param.evcFK_EventCategory, a.evcFK_EventCategory );
			this.AddParam( EventMap.evcFK_Workorder_GUID, EventMap.Param.evcFK_Workorder_GUID, a.evcFK_Workorder_GUID );
			this.AddParam( EventMap.evcFK_Workorder, EventMap.Param.evcFK_Workorder, a.evcFK_Workorder );
			this.AddParam( EventMap.evcFK_r_EventType_GUID, EventMap.Param.evcFK_r_EventType_GUID, a.evcFK_r_EventType_GUID );
			this.AddParam( EventMap.evcFK_r_EventType, EventMap.Param.evcFK_r_EventType, a.evcFK_r_EventType );
			this.AddParam( EventMap.evcEventBegDtTm, EventMap.Param.evcEventBegDtTm, a.evcEventBegDtTm );
			this.AddParam( EventMap.evcEventEndDtTm, EventMap.Param.evcEventEndDtTm, a.evcEventEndDtTm );
			this.AddParam( EventMap.evcDurationHours, EventMap.Param.evcDurationHours, a.evcDurationHours );
			this.AddParam( EventMap.evcEventOrder, EventMap.Param.evcEventOrder, a.evcEventOrder );
			this.AddParam( EventMap.evcFK_Assembly_GUID, EventMap.Param.evcFK_Assembly_GUID, a.evcFK_Assembly_GUID );
			this.AddParam( EventMap.evcFK_Assembly, EventMap.Param.evcFK_Assembly, a.evcFK_Assembly );
			this.AddParam( EventMap.evcResponsiblePerson, EventMap.Param.evcResponsiblePerson, a.evcResponsiblePerson );
			this.AddParam( EventMap.evcFK_BusinessOrganization_GUID, EventMap.Param.evcFK_BusinessOrganization_GUID, a.evcFK_BusinessOrganization_GUID );
			this.AddParam( EventMap.evcFK_BusinessOrganization, EventMap.Param.evcFK_BusinessOrganization, a.evcFK_BusinessOrganization );
			this.AddParam( EventMap.evcPersonPerformingTask, EventMap.Param.evcPersonPerformingTask, a.evcPersonPerformingTask );
			this.AddParam( EventMap.evcOriginKey, EventMap.Param.evcOriginKey, a.evcOriginKey );
			this.AddParam( EventMap.evcRemarks, EventMap.Param.evcRemarks, a.evcRemarks );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the EventAssembleSRPump table in the PumpServicing Database.
    /// </summary>
    public partial class EventAssembleSRPumpTransaction : BaseTransaction<EventAssembleSRPump>
    {
        public EventAssembleSRPumpTransaction()
            : base(EventAssembleSRPumpMap.TABLE_NAME, EventAssembleSRPumpMap.ID)
        {
        }
        
        public EventAssembleSRPumpTransaction( IBaseData _type )
			: base( _type, EventAssembleSRPumpMap.TABLE_NAME, EventAssembleSRPumpMap.ID )
		{
        }

        public EventAssembleSRPumpTransaction( string _PrimaryKey )
			: base( EventAssembleSRPumpMap.TABLE_NAME, EventAssembleSRPumpMap.ID, _PrimaryKey )
		{
        }

        public EventAssembleSRPumpTransaction( DateTime _lastModified )
            : base( EventAssembleSRPumpMap.TABLE_NAME, EventAssembleSRPumpMap.ID, _lastModified, EventAssembleSRPumpMap.LastModified )
        {
        }				
    
        public override EventAssembleSRPump BuildFromReader(TransactionReader reader)
        {
            EventAssembleSRPump a = new EventAssembleSRPump( );
			a.psrPrimaryKey_GUID = reader.TryReadGuid( EventAssembleSRPumpMap.ID);
			a.psrPrimaryKey = reader.TryReadstring( EventAssembleSRPumpMap.psrPrimaryKey);
			a.psrLstChgDT = reader.TryReadDateTime( EventAssembleSRPumpMap.LastModified);
			a.psrLstChgUser = reader.TryReadstring( EventAssembleSRPumpMap.psrLstChgUser);
			a.psrFK_Event_GUID = reader.TryReadGuid( EventAssembleSRPumpMap.psrFK_Event_GUID);
			a.psrFK_Event = reader.TryReadstring( EventAssembleSRPumpMap.psrFK_Event);
			a.psrDeliveryTicketNo = reader.TryReadstring( EventAssembleSRPumpMap.psrDeliveryTicketNo);
			a.psrVacuumPressureTest = reader.TryReadbool( EventAssembleSRPumpMap.psrVacuumPressureTest);
			a.psrTotalPumpCharges = reader.TryReaddecimal( EventAssembleSRPumpMap.psrTotalPumpCharges);
			a.psrFK_Assembly_WellChargeTo_GUID = reader.TryReadGuid( EventAssembleSRPumpMap.psrFK_Assembly_WellChargeTo_GUID);
			a.psrFK_Assembly_WellChargeTo = reader.TryReadstring( EventAssembleSRPumpMap.psrFK_Assembly_WellChargeTo);
			a.psrFK_WellCompletionXRef_WellChargeTo_GUID = reader.TryReadGuid( EventAssembleSRPumpMap.psrFK_WellCompletionXRef_WellChargeTo_GUID);
			a.psrFK_WellCompletionXRef_WellChargeTo = reader.TryReadstring( EventAssembleSRPumpMap.psrFK_WellCompletionXRef_WellChargeTo);
    
            return a;
        }

   		public override void RegisterParams()
		{
			EventAssembleSRPump a = (EventAssembleSRPump)dataObj;
			this.AddParam( EventAssembleSRPumpMap.ID, EventAssembleSRPumpMap.Param.ID, a.ID );
			this.AddParam( EventAssembleSRPumpMap.psrPrimaryKey, EventAssembleSRPumpMap.Param.psrPrimaryKey, a.psrPrimaryKey );
			this.AddParam( EventAssembleSRPumpMap.LastModified, EventAssembleSRPumpMap.Param.LastModified, a.LastModified );
			this.AddParam( EventAssembleSRPumpMap.psrLstChgUser, EventAssembleSRPumpMap.Param.psrLstChgUser, a.psrLstChgUser );
			this.AddParam( EventAssembleSRPumpMap.psrFK_Event_GUID, EventAssembleSRPumpMap.Param.psrFK_Event_GUID, a.psrFK_Event_GUID );
			this.AddParam( EventAssembleSRPumpMap.psrFK_Event, EventAssembleSRPumpMap.Param.psrFK_Event, a.psrFK_Event );
			this.AddParam( EventAssembleSRPumpMap.psrDeliveryTicketNo, EventAssembleSRPumpMap.Param.psrDeliveryTicketNo, a.psrDeliveryTicketNo );
			this.AddParam( EventAssembleSRPumpMap.psrVacuumPressureTest, EventAssembleSRPumpMap.Param.psrVacuumPressureTest, a.psrVacuumPressureTest );
			this.AddParam( EventAssembleSRPumpMap.psrTotalPumpCharges, EventAssembleSRPumpMap.Param.psrTotalPumpCharges, a.psrTotalPumpCharges );
			this.AddParam( EventAssembleSRPumpMap.psrFK_Assembly_WellChargeTo_GUID, EventAssembleSRPumpMap.Param.psrFK_Assembly_WellChargeTo_GUID, a.psrFK_Assembly_WellChargeTo_GUID );
			this.AddParam( EventAssembleSRPumpMap.psrFK_Assembly_WellChargeTo, EventAssembleSRPumpMap.Param.psrFK_Assembly_WellChargeTo, a.psrFK_Assembly_WellChargeTo );
			this.AddParam( EventAssembleSRPumpMap.psrFK_WellCompletionXRef_WellChargeTo_GUID, EventAssembleSRPumpMap.Param.psrFK_WellCompletionXRef_WellChargeTo_GUID, a.psrFK_WellCompletionXRef_WellChargeTo_GUID );
			this.AddParam( EventAssembleSRPumpMap.psrFK_WellCompletionXRef_WellChargeTo, EventAssembleSRPumpMap.Param.psrFK_WellCompletionXRef_WellChargeTo, a.psrFK_WellCompletionXRef_WellChargeTo );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the EventComponentFailure table in the PumpServicing Database.
    /// </summary>
    public partial class EventComponentFailureTransaction : BaseTransaction<EventComponentFailure>
    {
        public EventComponentFailureTransaction()
            : base(EventComponentFailureMap.TABLE_NAME, EventComponentFailureMap.ID)
        {
        }
        
        public EventComponentFailureTransaction( IBaseData _type )
			: base( _type, EventComponentFailureMap.TABLE_NAME, EventComponentFailureMap.ID )
		{
        }

        public EventComponentFailureTransaction( string _PrimaryKey )
			: base( EventComponentFailureMap.TABLE_NAME, EventComponentFailureMap.ID, _PrimaryKey )
		{
        }

        public EventComponentFailureTransaction( DateTime _lastModified )
            : base( EventComponentFailureMap.TABLE_NAME, EventComponentFailureMap.ID, _lastModified, EventComponentFailureMap.LastModified )
        {
        }				
    
        public override EventComponentFailure BuildFromReader(TransactionReader reader)
        {
            EventComponentFailure a = new EventComponentFailure( );
			a.acfPrimaryKey_GUID = reader.TryReadGuid( EventComponentFailureMap.ID);
			a.acfPrimaryKey = reader.TryReadstring( EventComponentFailureMap.acfPrimaryKey);
			a.acfLstChgDT = reader.TryReadDateTime( EventComponentFailureMap.LastModified);
			a.acfLstChgUser = reader.TryReadstring( EventComponentFailureMap.acfLstChgUser);
			a.acfFK_Event_GUID = reader.TryReadGuid( EventComponentFailureMap.acfFK_Event_GUID);
			a.acfFK_Event = reader.TryReadstring( EventComponentFailureMap.acfFK_Event);
			a.acfFK_Component_GUID = reader.TryReadGuid( EventComponentFailureMap.acfFK_Component_GUID);
			a.acfFK_Component = reader.TryReadstring( EventComponentFailureMap.acfFK_Component);
			a.acfPrimaryFailureObservation = reader.TryReadbool( EventComponentFailureMap.acfPrimaryFailureObservation);
			a.acfPrimaryCauseOfFailure = reader.TryReadint( EventComponentFailureMap.acfPrimaryCauseOfFailure);
			a.acfPreviousRunDays = reader.TryReaddecimal( EventComponentFailureMap.acfPreviousRunDays);
			a.acfFK_r_ComponentCondition_Current_GUID = reader.TryReadGuid( EventComponentFailureMap.acfFK_r_ComponentCondition_Current_GUID);
			a.acfFK_r_ComponentCondition_Current = reader.TryReadstring( EventComponentFailureMap.acfFK_r_ComponentCondition_Current);
			a.acfFK_r_FailureObservation_GUID = reader.TryReadGuid( EventComponentFailureMap.acfFK_r_FailureObservation_GUID);
			a.acfFK_r_FailureObservation = reader.TryReadstring( EventComponentFailureMap.acfFK_r_FailureObservation);
			a.acfFK_r_FailureLocation_GUID = reader.TryReadGuid( EventComponentFailureMap.acfFK_r_FailureLocation_GUID);
			a.acfFK_r_FailureLocation = reader.TryReadstring( EventComponentFailureMap.acfFK_r_FailureLocation);
			a.acfFK_r_FailureInternalExternal_GUID = reader.TryReadGuid( EventComponentFailureMap.acfFK_r_FailureInternalExternal_GUID);
			a.acfFK_r_FailureInternalExternal = reader.TryReadstring( EventComponentFailureMap.acfFK_r_FailureInternalExternal);
			a.acfFailedDepth = reader.TryReaddecimal( EventComponentFailureMap.acfFailedDepth);
			a.acfFK_r_CorrosionLocation_GUID = reader.TryReadGuid( EventComponentFailureMap.acfFK_r_CorrosionLocation_GUID);
			a.acfFK_r_CorrosionLocation = reader.TryReadstring( EventComponentFailureMap.acfFK_r_CorrosionLocation);
			a.acfFK_r_CorrosionAmount_GUID = reader.TryReadGuid( EventComponentFailureMap.acfFK_r_CorrosionAmount_GUID);
			a.acfFK_r_CorrosionAmount = reader.TryReadstring( EventComponentFailureMap.acfFK_r_CorrosionAmount);
			a.acfFK_r_CorrosionType_GUID = reader.TryReadGuid( EventComponentFailureMap.acfFK_r_CorrosionType_GUID);
			a.acfFK_r_CorrosionType = reader.TryReadstring( EventComponentFailureMap.acfFK_r_CorrosionType);
			a.acfFK_r_ComponentDisposition_GUID = reader.TryReadGuid( EventComponentFailureMap.acfFK_r_ComponentDisposition_GUID);
			a.acfFK_r_ComponentDisposition = reader.TryReadstring( EventComponentFailureMap.acfFK_r_ComponentDisposition);
			a.acfRemarks = reader.TryReadstring( EventComponentFailureMap.acfRemarks);
    
            return a;
        }

   		public override void RegisterParams()
		{
			EventComponentFailure a = (EventComponentFailure)dataObj;
			this.AddParam( EventComponentFailureMap.ID, EventComponentFailureMap.Param.ID, a.ID );
			this.AddParam( EventComponentFailureMap.acfPrimaryKey, EventComponentFailureMap.Param.acfPrimaryKey, a.acfPrimaryKey );
			this.AddParam( EventComponentFailureMap.LastModified, EventComponentFailureMap.Param.LastModified, a.LastModified );
			this.AddParam( EventComponentFailureMap.acfLstChgUser, EventComponentFailureMap.Param.acfLstChgUser, a.acfLstChgUser );
			this.AddParam( EventComponentFailureMap.acfFK_Event_GUID, EventComponentFailureMap.Param.acfFK_Event_GUID, a.acfFK_Event_GUID );
			this.AddParam( EventComponentFailureMap.acfFK_Event, EventComponentFailureMap.Param.acfFK_Event, a.acfFK_Event );
			this.AddParam( EventComponentFailureMap.acfFK_Component_GUID, EventComponentFailureMap.Param.acfFK_Component_GUID, a.acfFK_Component_GUID );
			this.AddParam( EventComponentFailureMap.acfFK_Component, EventComponentFailureMap.Param.acfFK_Component, a.acfFK_Component );
			this.AddParam( EventComponentFailureMap.acfPrimaryFailureObservation, EventComponentFailureMap.Param.acfPrimaryFailureObservation, a.acfPrimaryFailureObservation );
			this.AddParam( EventComponentFailureMap.acfPrimaryCauseOfFailure, EventComponentFailureMap.Param.acfPrimaryCauseOfFailure, a.acfPrimaryCauseOfFailure );
			this.AddParam( EventComponentFailureMap.acfPreviousRunDays, EventComponentFailureMap.Param.acfPreviousRunDays, a.acfPreviousRunDays );
			this.AddParam( EventComponentFailureMap.acfFK_r_ComponentCondition_Current_GUID, EventComponentFailureMap.Param.acfFK_r_ComponentCondition_Current_GUID, a.acfFK_r_ComponentCondition_Current_GUID );
			this.AddParam( EventComponentFailureMap.acfFK_r_ComponentCondition_Current, EventComponentFailureMap.Param.acfFK_r_ComponentCondition_Current, a.acfFK_r_ComponentCondition_Current );
			this.AddParam( EventComponentFailureMap.acfFK_r_FailureObservation_GUID, EventComponentFailureMap.Param.acfFK_r_FailureObservation_GUID, a.acfFK_r_FailureObservation_GUID );
			this.AddParam( EventComponentFailureMap.acfFK_r_FailureObservation, EventComponentFailureMap.Param.acfFK_r_FailureObservation, a.acfFK_r_FailureObservation );
			this.AddParam( EventComponentFailureMap.acfFK_r_FailureLocation_GUID, EventComponentFailureMap.Param.acfFK_r_FailureLocation_GUID, a.acfFK_r_FailureLocation_GUID );
			this.AddParam( EventComponentFailureMap.acfFK_r_FailureLocation, EventComponentFailureMap.Param.acfFK_r_FailureLocation, a.acfFK_r_FailureLocation );
			this.AddParam( EventComponentFailureMap.acfFK_r_FailureInternalExternal_GUID, EventComponentFailureMap.Param.acfFK_r_FailureInternalExternal_GUID, a.acfFK_r_FailureInternalExternal_GUID );
			this.AddParam( EventComponentFailureMap.acfFK_r_FailureInternalExternal, EventComponentFailureMap.Param.acfFK_r_FailureInternalExternal, a.acfFK_r_FailureInternalExternal );
			this.AddParam( EventComponentFailureMap.acfFailedDepth, EventComponentFailureMap.Param.acfFailedDepth, a.acfFailedDepth );
			this.AddParam( EventComponentFailureMap.acfFK_r_CorrosionLocation_GUID, EventComponentFailureMap.Param.acfFK_r_CorrosionLocation_GUID, a.acfFK_r_CorrosionLocation_GUID );
			this.AddParam( EventComponentFailureMap.acfFK_r_CorrosionLocation, EventComponentFailureMap.Param.acfFK_r_CorrosionLocation, a.acfFK_r_CorrosionLocation );
			this.AddParam( EventComponentFailureMap.acfFK_r_CorrosionAmount_GUID, EventComponentFailureMap.Param.acfFK_r_CorrosionAmount_GUID, a.acfFK_r_CorrosionAmount_GUID );
			this.AddParam( EventComponentFailureMap.acfFK_r_CorrosionAmount, EventComponentFailureMap.Param.acfFK_r_CorrosionAmount, a.acfFK_r_CorrosionAmount );
			this.AddParam( EventComponentFailureMap.acfFK_r_CorrosionType_GUID, EventComponentFailureMap.Param.acfFK_r_CorrosionType_GUID, a.acfFK_r_CorrosionType_GUID );
			this.AddParam( EventComponentFailureMap.acfFK_r_CorrosionType, EventComponentFailureMap.Param.acfFK_r_CorrosionType, a.acfFK_r_CorrosionType );
			this.AddParam( EventComponentFailureMap.acfFK_r_ComponentDisposition_GUID, EventComponentFailureMap.Param.acfFK_r_ComponentDisposition_GUID, a.acfFK_r_ComponentDisposition_GUID );
			this.AddParam( EventComponentFailureMap.acfFK_r_ComponentDisposition, EventComponentFailureMap.Param.acfFK_r_ComponentDisposition, a.acfFK_r_ComponentDisposition );
			this.AddParam( EventComponentFailureMap.acfRemarks, EventComponentFailureMap.Param.acfRemarks, a.acfRemarks );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the EventDetailCosts table in the PumpServicing Database.
    /// </summary>
    public partial class EventDetailCostsTransaction : BaseTransaction<EventDetailCosts>
    {
        public EventDetailCostsTransaction()
            : base(EventDetailCostsMap.TABLE_NAME, EventDetailCostsMap.ID)
        {
        }
        
        public EventDetailCostsTransaction( IBaseData _type )
			: base( _type, EventDetailCostsMap.TABLE_NAME, EventDetailCostsMap.ID )
		{
        }

        public EventDetailCostsTransaction( string _PrimaryKey )
			: base( EventDetailCostsMap.TABLE_NAME, EventDetailCostsMap.ID, _PrimaryKey )
		{
        }

        public EventDetailCostsTransaction( DateTime _lastModified )
            : base( EventDetailCostsMap.TABLE_NAME, EventDetailCostsMap.ID, _lastModified, EventDetailCostsMap.LastModified )
        {
        }				
    
        public override EventDetailCosts BuildFromReader(TransactionReader reader)
        {
            EventDetailCosts a = new EventDetailCosts( );
			a.ecsPrimaryKey_GUID = reader.TryReadGuid( EventDetailCostsMap.ID);
			a.ecsPrimaryKey = reader.TryReadstring( EventDetailCostsMap.ecsPrimaryKey);
			a.ecsLstChgDT = reader.TryReadDateTime( EventDetailCostsMap.LastModified);
			a.ecsLstChgUser = reader.TryReadstring( EventDetailCostsMap.ecsLstChgUser);
			a.ecsFK_Event_GUID = reader.TryReadGuid( EventDetailCostsMap.ecsFK_Event_GUID);
			a.ecsFK_Event = reader.TryReadstring( EventDetailCostsMap.ecsFK_Event);
			a.ecsFK_BusinessOrganization_GUID = reader.TryReadGuid( EventDetailCostsMap.ecsFK_BusinessOrganization_GUID);
			a.ecsFK_BusinessOrganization = reader.TryReadstring( EventDetailCostsMap.ecsFK_BusinessOrganization);
			a.ecsFK_Component_GUID = reader.TryReadGuid( EventDetailCostsMap.ecsFK_Component_GUID);
			a.ecsFK_Component = reader.TryReadstring( EventDetailCostsMap.ecsFK_Component);
			a.ecsFK_r_CatalogItem_GUID = reader.TryReadGuid( EventDetailCostsMap.ecsFK_r_CatalogItem_GUID);
			a.ecsFK_r_CatalogItem = reader.TryReadstring( EventDetailCostsMap.ecsFK_r_CatalogItem);
			a.ecsQuantity = reader.TryReaddecimal( EventDetailCostsMap.ecsQuantity);
			a.ecsUnitPrice = reader.TryReaddecimal( EventDetailCostsMap.ecsUnitPrice);
			a.ecsDiscountAmount = reader.TryReaddecimal( EventDetailCostsMap.ecsDiscountAmount);
			a.ecsExtendedPrice = reader.TryReaddecimal( EventDetailCostsMap.ecsExtendedPrice);
			a.ecsFK_r_UOMUnit_GUID = reader.TryReadGuid( EventDetailCostsMap.ecsFK_r_UOMUnit_GUID);
			a.ecsFK_r_UOMUnit = reader.TryReadstring( EventDetailCostsMap.ecsFK_r_UOMUnit);
			a.ecsTaxableItem = reader.TryReadbool( EventDetailCostsMap.ecsTaxableItem);
			a.ecsFK_Invoice = reader.TryReadstring( EventDetailCostsMap.ecsFK_Invoice);
			a.ecsFK_Invoice_GUID = reader.TryReadGuid( EventDetailCostsMap.ecsFK_Invoice_GUID);
			a.ecsRemarks = reader.TryReadstring( EventDetailCostsMap.ecsRemarks);
    
            return a;
        }

   		public override void RegisterParams()
		{
			EventDetailCosts a = (EventDetailCosts)dataObj;
			this.AddParam( EventDetailCostsMap.ID, EventDetailCostsMap.Param.ID, a.ID );
			this.AddParam( EventDetailCostsMap.ecsPrimaryKey, EventDetailCostsMap.Param.ecsPrimaryKey, a.ecsPrimaryKey );
			this.AddParam( EventDetailCostsMap.LastModified, EventDetailCostsMap.Param.LastModified, a.LastModified );
			this.AddParam( EventDetailCostsMap.ecsLstChgUser, EventDetailCostsMap.Param.ecsLstChgUser, a.ecsLstChgUser );
			this.AddParam( EventDetailCostsMap.ecsFK_Event_GUID, EventDetailCostsMap.Param.ecsFK_Event_GUID, a.ecsFK_Event_GUID );
			this.AddParam( EventDetailCostsMap.ecsFK_Event, EventDetailCostsMap.Param.ecsFK_Event, a.ecsFK_Event );
			this.AddParam( EventDetailCostsMap.ecsFK_BusinessOrganization_GUID, EventDetailCostsMap.Param.ecsFK_BusinessOrganization_GUID, a.ecsFK_BusinessOrganization_GUID );
			this.AddParam( EventDetailCostsMap.ecsFK_BusinessOrganization, EventDetailCostsMap.Param.ecsFK_BusinessOrganization, a.ecsFK_BusinessOrganization );
			this.AddParam( EventDetailCostsMap.ecsFK_Component_GUID, EventDetailCostsMap.Param.ecsFK_Component_GUID, a.ecsFK_Component_GUID );
			this.AddParam( EventDetailCostsMap.ecsFK_Component, EventDetailCostsMap.Param.ecsFK_Component, a.ecsFK_Component );
			this.AddParam( EventDetailCostsMap.ecsFK_r_CatalogItem_GUID, EventDetailCostsMap.Param.ecsFK_r_CatalogItem_GUID, a.ecsFK_r_CatalogItem_GUID );
			this.AddParam( EventDetailCostsMap.ecsFK_r_CatalogItem, EventDetailCostsMap.Param.ecsFK_r_CatalogItem, a.ecsFK_r_CatalogItem );
			this.AddParam( EventDetailCostsMap.ecsQuantity, EventDetailCostsMap.Param.ecsQuantity, a.ecsQuantity );
			this.AddParam( EventDetailCostsMap.ecsUnitPrice, EventDetailCostsMap.Param.ecsUnitPrice, a.ecsUnitPrice );
			this.AddParam( EventDetailCostsMap.ecsDiscountAmount, EventDetailCostsMap.Param.ecsDiscountAmount, a.ecsDiscountAmount );
			this.AddParam( EventDetailCostsMap.ecsExtendedPrice, EventDetailCostsMap.Param.ecsExtendedPrice, a.ecsExtendedPrice );
			this.AddParam( EventDetailCostsMap.ecsFK_r_UOMUnit_GUID, EventDetailCostsMap.Param.ecsFK_r_UOMUnit_GUID, a.ecsFK_r_UOMUnit_GUID );
			this.AddParam( EventDetailCostsMap.ecsFK_r_UOMUnit, EventDetailCostsMap.Param.ecsFK_r_UOMUnit, a.ecsFK_r_UOMUnit );
			this.AddParam( EventDetailCostsMap.ecsTaxableItem, EventDetailCostsMap.Param.ecsTaxableItem, a.ecsTaxableItem );
			this.AddParam( EventDetailCostsMap.ecsFK_Invoice, EventDetailCostsMap.Param.ecsFK_Invoice, a.ecsFK_Invoice );
			this.AddParam( EventDetailCostsMap.ecsFK_Invoice_GUID, EventDetailCostsMap.Param.ecsFK_Invoice_GUID, a.ecsFK_Invoice_GUID );
			this.AddParam( EventDetailCostsMap.ecsRemarks, EventDetailCostsMap.Param.ecsRemarks, a.ecsRemarks );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the EventDisassembleSRPump table in the PumpServicing Database.
    /// </summary>
    public partial class EventDisassembleSRPumpTransaction : BaseTransaction<EventDisassembleSRPump>
    {
        public EventDisassembleSRPumpTransaction()
            : base(EventDisassembleSRPumpMap.TABLE_NAME, EventDisassembleSRPumpMap.ID)
        {
        }
        
        public EventDisassembleSRPumpTransaction( IBaseData _type )
			: base( _type, EventDisassembleSRPumpMap.TABLE_NAME, EventDisassembleSRPumpMap.ID )
		{
        }

        public EventDisassembleSRPumpTransaction( string _PrimaryKey )
			: base( EventDisassembleSRPumpMap.TABLE_NAME, EventDisassembleSRPumpMap.ID, _PrimaryKey )
		{
        }

        public EventDisassembleSRPumpTransaction( DateTime _lastModified )
            : base( EventDisassembleSRPumpMap.TABLE_NAME, EventDisassembleSRPumpMap.ID, _lastModified, EventDisassembleSRPumpMap.LastModified )
        {
        }				
    
        public override EventDisassembleSRPump BuildFromReader(TransactionReader reader)
        {
            EventDisassembleSRPump a = new EventDisassembleSRPump( );
			a.ptdPrimaryKey_GUID = reader.TryReadGuid( EventDisassembleSRPumpMap.ID);
			a.ptdPrimaryKey = reader.TryReadstring( EventDisassembleSRPumpMap.ptdPrimaryKey);
			a.ptdLstChgDT = reader.TryReadDateTime( EventDisassembleSRPumpMap.LastModified);
			a.ptdLstChgUser = reader.TryReadstring( EventDisassembleSRPumpMap.ptdLstChgUser);
			a.ptdFK_Event_GUID = reader.TryReadGuid( EventDisassembleSRPumpMap.ptdFK_Event_GUID);
			a.ptdFK_Event = reader.TryReadstring( EventDisassembleSRPumpMap.ptdFK_Event);
			a.ptdVacuumPressureTest = reader.TryReadbool( EventDisassembleSRPumpMap.ptdVacuumPressureTest);
			a.ptdJunkPump = reader.TryReadbool( EventDisassembleSRPumpMap.ptdJunkPump);
			a.ptdRecommendation = reader.TryReadbool( EventDisassembleSRPumpMap.ptdRecommendation);
			a.ptdForMatlAsphaltene = reader.TryReadbool( EventDisassembleSRPumpMap.ptdForMatlAsphaltene);
			a.ptdForMatlSand = reader.TryReadbool( EventDisassembleSRPumpMap.ptdForMatlSand);
			a.ptdForMatlRubber = reader.TryReadbool( EventDisassembleSRPumpMap.ptdForMatlRubber);
			a.ptdForMatlSteelFilings = reader.TryReadbool( EventDisassembleSRPumpMap.ptdForMatlSteelFilings);
			a.ptdForMatlShale = reader.TryReadbool( EventDisassembleSRPumpMap.ptdForMatlShale);
			a.ptdForMatlParaffin = reader.TryReadbool( EventDisassembleSRPumpMap.ptdForMatlParaffin);
			a.ptdForMatlIronSulfide = reader.TryReadbool( EventDisassembleSRPumpMap.ptdForMatlIronSulfide);
			a.ptdForMatlMud = reader.TryReadbool( EventDisassembleSRPumpMap.ptdForMatlMud);
			a.ptdForMatlScale = reader.TryReadbool( EventDisassembleSRPumpMap.ptdForMatlScale);
			a.ptdForMatlChalk = reader.TryReadbool( EventDisassembleSRPumpMap.ptdForMatlChalk);
			a.ptdForMatlCoal = reader.TryReadbool( EventDisassembleSRPumpMap.ptdForMatlCoal);
			a.ptdForMatlOther = reader.TryReadbool( EventDisassembleSRPumpMap.ptdForMatlOther);
			a.ptdFK_r_CorrosionAmount_GUID = reader.TryReadGuid( EventDisassembleSRPumpMap.ptdFK_r_CorrosionAmount_GUID);
			a.ptdFK_r_CorrosionAmount = reader.TryReadstring( EventDisassembleSRPumpMap.ptdFK_r_CorrosionAmount);
			a.ptdFK_r_CorrosionLocation_GUID = reader.TryReadGuid( EventDisassembleSRPumpMap.ptdFK_r_CorrosionLocation_GUID);
			a.ptdFK_r_CorrosionLocation = reader.TryReadstring( EventDisassembleSRPumpMap.ptdFK_r_CorrosionLocation);
			a.ptdFK_r_SRPumpFailureReason_GUID = reader.TryReadGuid( EventDisassembleSRPumpMap.ptdFK_r_SRPumpFailureReason_GUID);
			a.ptdFK_r_SRPumpFailureReason = reader.TryReadstring( EventDisassembleSRPumpMap.ptdFK_r_SRPumpFailureReason);
			a.ptdApparentDownholeStroke = reader.TryReaddecimal( EventDisassembleSRPumpMap.ptdApparentDownholeStroke);
    
            return a;
        }

   		public override void RegisterParams()
		{
			EventDisassembleSRPump a = (EventDisassembleSRPump)dataObj;
			this.AddParam( EventDisassembleSRPumpMap.ID, EventDisassembleSRPumpMap.Param.ID, a.ID );
			this.AddParam( EventDisassembleSRPumpMap.ptdPrimaryKey, EventDisassembleSRPumpMap.Param.ptdPrimaryKey, a.ptdPrimaryKey );
			this.AddParam( EventDisassembleSRPumpMap.LastModified, EventDisassembleSRPumpMap.Param.LastModified, a.LastModified );
			this.AddParam( EventDisassembleSRPumpMap.ptdLstChgUser, EventDisassembleSRPumpMap.Param.ptdLstChgUser, a.ptdLstChgUser );
			this.AddParam( EventDisassembleSRPumpMap.ptdFK_Event_GUID, EventDisassembleSRPumpMap.Param.ptdFK_Event_GUID, a.ptdFK_Event_GUID );
			this.AddParam( EventDisassembleSRPumpMap.ptdFK_Event, EventDisassembleSRPumpMap.Param.ptdFK_Event, a.ptdFK_Event );
			this.AddParam( EventDisassembleSRPumpMap.ptdVacuumPressureTest, EventDisassembleSRPumpMap.Param.ptdVacuumPressureTest, a.ptdVacuumPressureTest );
			this.AddParam( EventDisassembleSRPumpMap.ptdJunkPump, EventDisassembleSRPumpMap.Param.ptdJunkPump, a.ptdJunkPump );
			this.AddParam( EventDisassembleSRPumpMap.ptdRecommendation, EventDisassembleSRPumpMap.Param.ptdRecommendation, a.ptdRecommendation );
			this.AddParam( EventDisassembleSRPumpMap.ptdForMatlAsphaltene, EventDisassembleSRPumpMap.Param.ptdForMatlAsphaltene, a.ptdForMatlAsphaltene );
			this.AddParam( EventDisassembleSRPumpMap.ptdForMatlSand, EventDisassembleSRPumpMap.Param.ptdForMatlSand, a.ptdForMatlSand );
			this.AddParam( EventDisassembleSRPumpMap.ptdForMatlRubber, EventDisassembleSRPumpMap.Param.ptdForMatlRubber, a.ptdForMatlRubber );
			this.AddParam( EventDisassembleSRPumpMap.ptdForMatlSteelFilings, EventDisassembleSRPumpMap.Param.ptdForMatlSteelFilings, a.ptdForMatlSteelFilings );
			this.AddParam( EventDisassembleSRPumpMap.ptdForMatlShale, EventDisassembleSRPumpMap.Param.ptdForMatlShale, a.ptdForMatlShale );
			this.AddParam( EventDisassembleSRPumpMap.ptdForMatlParaffin, EventDisassembleSRPumpMap.Param.ptdForMatlParaffin, a.ptdForMatlParaffin );
			this.AddParam( EventDisassembleSRPumpMap.ptdForMatlIronSulfide, EventDisassembleSRPumpMap.Param.ptdForMatlIronSulfide, a.ptdForMatlIronSulfide );
			this.AddParam( EventDisassembleSRPumpMap.ptdForMatlMud, EventDisassembleSRPumpMap.Param.ptdForMatlMud, a.ptdForMatlMud );
			this.AddParam( EventDisassembleSRPumpMap.ptdForMatlScale, EventDisassembleSRPumpMap.Param.ptdForMatlScale, a.ptdForMatlScale );
			this.AddParam( EventDisassembleSRPumpMap.ptdForMatlChalk, EventDisassembleSRPumpMap.Param.ptdForMatlChalk, a.ptdForMatlChalk );
			this.AddParam( EventDisassembleSRPumpMap.ptdForMatlCoal, EventDisassembleSRPumpMap.Param.ptdForMatlCoal, a.ptdForMatlCoal );
			this.AddParam( EventDisassembleSRPumpMap.ptdForMatlOther, EventDisassembleSRPumpMap.Param.ptdForMatlOther, a.ptdForMatlOther );
			this.AddParam( EventDisassembleSRPumpMap.ptdFK_r_CorrosionAmount_GUID, EventDisassembleSRPumpMap.Param.ptdFK_r_CorrosionAmount_GUID, a.ptdFK_r_CorrosionAmount_GUID );
			this.AddParam( EventDisassembleSRPumpMap.ptdFK_r_CorrosionAmount, EventDisassembleSRPumpMap.Param.ptdFK_r_CorrosionAmount, a.ptdFK_r_CorrosionAmount );
			this.AddParam( EventDisassembleSRPumpMap.ptdFK_r_CorrosionLocation_GUID, EventDisassembleSRPumpMap.Param.ptdFK_r_CorrosionLocation_GUID, a.ptdFK_r_CorrosionLocation_GUID );
			this.AddParam( EventDisassembleSRPumpMap.ptdFK_r_CorrosionLocation, EventDisassembleSRPumpMap.Param.ptdFK_r_CorrosionLocation, a.ptdFK_r_CorrosionLocation );
			this.AddParam( EventDisassembleSRPumpMap.ptdFK_r_SRPumpFailureReason_GUID, EventDisassembleSRPumpMap.Param.ptdFK_r_SRPumpFailureReason_GUID, a.ptdFK_r_SRPumpFailureReason_GUID );
			this.AddParam( EventDisassembleSRPumpMap.ptdFK_r_SRPumpFailureReason, EventDisassembleSRPumpMap.Param.ptdFK_r_SRPumpFailureReason, a.ptdFK_r_SRPumpFailureReason );
			this.AddParam( EventDisassembleSRPumpMap.ptdApparentDownholeStroke, EventDisassembleSRPumpMap.Param.ptdApparentDownholeStroke, a.ptdApparentDownholeStroke );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the EventInstallPump table in the PumpServicing Database.
    /// </summary>
    public partial class EventInstallPumpTransaction : BaseTransaction<EventInstallPump>
    {
        public EventInstallPumpTransaction()
            : base(EventInstallPumpMap.TABLE_NAME, EventInstallPumpMap.ID)
        {
        }
        
        public EventInstallPumpTransaction( IBaseData _type )
			: base( _type, EventInstallPumpMap.TABLE_NAME, EventInstallPumpMap.ID )
		{
        }

        public EventInstallPumpTransaction( string _PrimaryKey )
			: base( EventInstallPumpMap.TABLE_NAME, EventInstallPumpMap.ID, _PrimaryKey )
		{
        }

        public EventInstallPumpTransaction( DateTime _lastModified )
            : base( EventInstallPumpMap.TABLE_NAME, EventInstallPumpMap.ID, _lastModified, EventInstallPumpMap.LastModified )
        {
        }				
    
        public override EventInstallPump BuildFromReader(TransactionReader reader)
        {
            EventInstallPump a = new EventInstallPump( );
			a.eipPrimaryKey_GUID = reader.TryReadGuid( EventInstallPumpMap.ID);
			a.eipPrimaryKey = reader.TryReadstring( EventInstallPumpMap.eipPrimaryKey);
			a.eipLstChgDT = reader.TryReadDateTime( EventInstallPumpMap.LastModified);
			a.eipLstChgUser = reader.TryReadstring( EventInstallPumpMap.eipLstChgUser);
			a.eipFK_Event_GUID = reader.TryReadGuid( EventInstallPumpMap.eipFK_Event_GUID);
			a.eipFK_Event = reader.TryReadstring( EventInstallPumpMap.eipFK_Event);
			a.eipFK_Assembly_WellSurfaceLocation_GUID = reader.TryReadGuid( EventInstallPumpMap.eipFK_Assembly_WellSurfaceLocation_GUID);
			a.eipFK_Assembly_WellSurfaceLocation = reader.TryReadstring( EventInstallPumpMap.eipFK_Assembly_WellSurfaceLocation);
			a.eipFK_WellCompletionXRef_GUID = reader.TryReadGuid( EventInstallPumpMap.eipFK_WellCompletionXRef_GUID);
			a.eipFK_WellCompletionXRef = reader.TryReadstring( EventInstallPumpMap.eipFK_WellCompletionXRef);
    
            return a;
        }

   		public override void RegisterParams()
		{
			EventInstallPump a = (EventInstallPump)dataObj;
			this.AddParam( EventInstallPumpMap.ID, EventInstallPumpMap.Param.ID, a.ID );
			this.AddParam( EventInstallPumpMap.eipPrimaryKey, EventInstallPumpMap.Param.eipPrimaryKey, a.eipPrimaryKey );
			this.AddParam( EventInstallPumpMap.LastModified, EventInstallPumpMap.Param.LastModified, a.LastModified );
			this.AddParam( EventInstallPumpMap.eipLstChgUser, EventInstallPumpMap.Param.eipLstChgUser, a.eipLstChgUser );
			this.AddParam( EventInstallPumpMap.eipFK_Event_GUID, EventInstallPumpMap.Param.eipFK_Event_GUID, a.eipFK_Event_GUID );
			this.AddParam( EventInstallPumpMap.eipFK_Event, EventInstallPumpMap.Param.eipFK_Event, a.eipFK_Event );
			this.AddParam( EventInstallPumpMap.eipFK_Assembly_WellSurfaceLocation_GUID, EventInstallPumpMap.Param.eipFK_Assembly_WellSurfaceLocation_GUID, a.eipFK_Assembly_WellSurfaceLocation_GUID );
			this.AddParam( EventInstallPumpMap.eipFK_Assembly_WellSurfaceLocation, EventInstallPumpMap.Param.eipFK_Assembly_WellSurfaceLocation, a.eipFK_Assembly_WellSurfaceLocation );
			this.AddParam( EventInstallPumpMap.eipFK_WellCompletionXRef_GUID, EventInstallPumpMap.Param.eipFK_WellCompletionXRef_GUID, a.eipFK_WellCompletionXRef_GUID );
			this.AddParam( EventInstallPumpMap.eipFK_WellCompletionXRef, EventInstallPumpMap.Param.eipFK_WellCompletionXRef, a.eipFK_WellCompletionXRef );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the EventPullPump table in the PumpServicing Database.
    /// </summary>
    public partial class EventPullPumpTransaction : BaseTransaction<EventPullPump>
    {
        public EventPullPumpTransaction()
            : base(EventPullPumpMap.TABLE_NAME, EventPullPumpMap.ID)
        {
        }
        
        public EventPullPumpTransaction( IBaseData _type )
			: base( _type, EventPullPumpMap.TABLE_NAME, EventPullPumpMap.ID )
		{
        }

        public EventPullPumpTransaction( string _PrimaryKey )
			: base( EventPullPumpMap.TABLE_NAME, EventPullPumpMap.ID, _PrimaryKey )
		{
        }

        public EventPullPumpTransaction( DateTime _lastModified )
            : base( EventPullPumpMap.TABLE_NAME, EventPullPumpMap.ID, _lastModified, EventPullPumpMap.LastModified )
        {
        }				
    
        public override EventPullPump BuildFromReader(TransactionReader reader)
        {
            EventPullPump a = new EventPullPump( );
			a.eppPrimaryKey_GUID = reader.TryReadGuid( EventPullPumpMap.ID);
			a.eppPrimaryKey = reader.TryReadstring( EventPullPumpMap.eppPrimaryKey);
			a.eppLstChgDT = reader.TryReadDateTime( EventPullPumpMap.LastModified);
			a.eppLstChgUser = reader.TryReadstring( EventPullPumpMap.eppLstChgUser);
			a.eppFK_Event_GUID = reader.TryReadGuid( EventPullPumpMap.eppFK_Event_GUID);
			a.eppFK_Event = reader.TryReadstring( EventPullPumpMap.eppFK_Event);
			a.eppFK_Assembly_WellSurfaceLocation_GUID = reader.TryReadGuid( EventPullPumpMap.eppFK_Assembly_WellSurfaceLocation_GUID);
			a.eppFK_Assembly_WellSurfaceLocation = reader.TryReadstring( EventPullPumpMap.eppFK_Assembly_WellSurfaceLocation);
			a.eppFK_WellCompletionXRef_GUID = reader.TryReadGuid( EventPullPumpMap.eppFK_WellCompletionXRef_GUID);
			a.eppFK_WellCompletionXRef = reader.TryReadstring( EventPullPumpMap.eppFK_WellCompletionXRef);
			a.eppFailedDate = reader.TryReadDateTime( EventPullPumpMap.eppFailedDate);
			a.eppRunDays = reader.TryReaddecimal( EventPullPumpMap.eppRunDays);
    
            return a;
        }

   		public override void RegisterParams()
		{
			EventPullPump a = (EventPullPump)dataObj;
			this.AddParam( EventPullPumpMap.ID, EventPullPumpMap.Param.ID, a.ID );
			this.AddParam( EventPullPumpMap.eppPrimaryKey, EventPullPumpMap.Param.eppPrimaryKey, a.eppPrimaryKey );
			this.AddParam( EventPullPumpMap.LastModified, EventPullPumpMap.Param.LastModified, a.LastModified );
			this.AddParam( EventPullPumpMap.eppLstChgUser, EventPullPumpMap.Param.eppLstChgUser, a.eppLstChgUser );
			this.AddParam( EventPullPumpMap.eppFK_Event_GUID, EventPullPumpMap.Param.eppFK_Event_GUID, a.eppFK_Event_GUID );
			this.AddParam( EventPullPumpMap.eppFK_Event, EventPullPumpMap.Param.eppFK_Event, a.eppFK_Event );
			this.AddParam( EventPullPumpMap.eppFK_Assembly_WellSurfaceLocation_GUID, EventPullPumpMap.Param.eppFK_Assembly_WellSurfaceLocation_GUID, a.eppFK_Assembly_WellSurfaceLocation_GUID );
			this.AddParam( EventPullPumpMap.eppFK_Assembly_WellSurfaceLocation, EventPullPumpMap.Param.eppFK_Assembly_WellSurfaceLocation, a.eppFK_Assembly_WellSurfaceLocation );
			this.AddParam( EventPullPumpMap.eppFK_WellCompletionXRef_GUID, EventPullPumpMap.Param.eppFK_WellCompletionXRef_GUID, a.eppFK_WellCompletionXRef_GUID );
			this.AddParam( EventPullPumpMap.eppFK_WellCompletionXRef, EventPullPumpMap.Param.eppFK_WellCompletionXRef, a.eppFK_WellCompletionXRef );
			this.AddParam( EventPullPumpMap.eppFailedDate, EventPullPumpMap.Param.eppFailedDate, a.eppFailedDate );
			this.AddParam( EventPullPumpMap.eppRunDays, EventPullPumpMap.Param.eppRunDays, a.eppRunDays );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the Facility table in the PumpServicing Database.
    /// </summary>
    public partial class FacilityTransaction : BaseTransaction<Facility>
    {
        public FacilityTransaction()
            : base(FacilityMap.TABLE_NAME, FacilityMap.ID)
        {
        }
        
        public FacilityTransaction( IBaseData _type )
			: base( _type, FacilityMap.TABLE_NAME, FacilityMap.ID )
		{
        }

        public FacilityTransaction( string _PrimaryKey )
			: base( FacilityMap.TABLE_NAME, FacilityMap.ID, _PrimaryKey )
		{
        }

        public FacilityTransaction( DateTime _lastModified )
            : base( FacilityMap.TABLE_NAME, FacilityMap.ID, _lastModified, FacilityMap.LastModified )
        {
        }				
    
        public override Facility BuildFromReader(TransactionReader reader)
        {
            Facility a = new Facility( );
			a.facPrimaryKey_GUID = reader.TryReadGuid( FacilityMap.ID);
			a.facPrimaryKey = reader.TryReadstring( FacilityMap.facPrimaryKey);
			a.facLstChgDT = reader.TryReadDateTime( FacilityMap.LastModified);
			a.facLstChgUser = reader.TryReadstring( FacilityMap.facLstChgUser);
			a.facRefCaseDefined = reader.TryReadbool( FacilityMap.facRefCaseDefined);
			a.facRefUserDeleted = reader.TryReadbool( FacilityMap.facRefUserDeleted);
			a.facFK_Assembly_GUID = reader.TryReadGuid( FacilityMap.facFK_Assembly_GUID);
			a.facFK_Assembly = reader.TryReadstring( FacilityMap.facFK_Assembly);
			a.facFK_r_FacilityType_GUID = reader.TryReadGuid( FacilityMap.facFK_r_FacilityType_GUID);
			a.facFK_r_FacilityType = reader.TryReadstring( FacilityMap.facFK_r_FacilityType);
			a.facFacilityID = reader.TryReadstring( FacilityMap.facFacilityID);
			a.facFacilityDescription = reader.TryReadstring( FacilityMap.facFacilityDescription);
			a.facFK_Owner = reader.TryReadstring( FacilityMap.facFK_Owner);
			a.facFK_BusinessOrganization_GUID = reader.TryReadGuid( FacilityMap.facFK_BusinessOrganization_GUID);
			a.facFK_BusinessOrganization = reader.TryReadstring( FacilityMap.facFK_BusinessOrganization);
			a.facStorageID = reader.TryReadstring( FacilityMap.facStorageID);
			a.facInactive = reader.TryReadbool( FacilityMap.facInactive);
    
            return a;
        }

   		public override void RegisterParams()
		{
			Facility a = (Facility)dataObj;
			this.AddParam( FacilityMap.ID, FacilityMap.Param.ID, a.ID );
			this.AddParam( FacilityMap.facPrimaryKey, FacilityMap.Param.facPrimaryKey, a.facPrimaryKey );
			this.AddParam( FacilityMap.LastModified, FacilityMap.Param.LastModified, a.LastModified );
			this.AddParam( FacilityMap.facLstChgUser, FacilityMap.Param.facLstChgUser, a.facLstChgUser );
			this.AddParam( FacilityMap.facRefCaseDefined, FacilityMap.Param.facRefCaseDefined, a.facRefCaseDefined );
			this.AddParam( FacilityMap.facRefUserDeleted, FacilityMap.Param.facRefUserDeleted, a.facRefUserDeleted );
			this.AddParam( FacilityMap.facFK_Assembly_GUID, FacilityMap.Param.facFK_Assembly_GUID, a.facFK_Assembly_GUID );
			this.AddParam( FacilityMap.facFK_Assembly, FacilityMap.Param.facFK_Assembly, a.facFK_Assembly );
			this.AddParam( FacilityMap.facFK_r_FacilityType_GUID, FacilityMap.Param.facFK_r_FacilityType_GUID, a.facFK_r_FacilityType_GUID );
			this.AddParam( FacilityMap.facFK_r_FacilityType, FacilityMap.Param.facFK_r_FacilityType, a.facFK_r_FacilityType );
			this.AddParam( FacilityMap.facFacilityID, FacilityMap.Param.facFacilityID, a.facFacilityID );
			this.AddParam( FacilityMap.facFacilityDescription, FacilityMap.Param.facFacilityDescription, a.facFacilityDescription );
			this.AddParam( FacilityMap.facFK_Owner, FacilityMap.Param.facFK_Owner, a.facFK_Owner );
			this.AddParam( FacilityMap.facFK_BusinessOrganization_GUID, FacilityMap.Param.facFK_BusinessOrganization_GUID, a.facFK_BusinessOrganization_GUID );
			this.AddParam( FacilityMap.facFK_BusinessOrganization, FacilityMap.Param.facFK_BusinessOrganization, a.facFK_BusinessOrganization );
			this.AddParam( FacilityMap.facStorageID, FacilityMap.Param.facStorageID, a.facStorageID );
			this.AddParam( FacilityMap.facInactive, FacilityMap.Param.facInactive, a.facInactive );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the Invoice table in the PumpServicing Database.
    /// </summary>
    public partial class InvoiceTransaction : BaseTransaction<Invoice>
    {
        public InvoiceTransaction()
            : base(InvoiceMap.TABLE_NAME, InvoiceMap.ID)
        {
        }
        
        public InvoiceTransaction( IBaseData _type )
			: base( _type, InvoiceMap.TABLE_NAME, InvoiceMap.ID )
		{
        }

        public InvoiceTransaction( string _PrimaryKey )
			: base( InvoiceMap.TABLE_NAME, InvoiceMap.ID, _PrimaryKey )
		{
        }

        public InvoiceTransaction( DateTime _lastModified )
            : base( InvoiceMap.TABLE_NAME, InvoiceMap.ID, _lastModified, InvoiceMap.LastModified )
        {
        }				
    
        public override Invoice BuildFromReader(TransactionReader reader)
        {
            Invoice a = new Invoice( );
			a.xh5PrimaryKey_GUID = reader.TryReadGuid( InvoiceMap.ID);
			a.xh5PrimaryKey = reader.TryReadstring( InvoiceMap.xh5PrimaryKey);
			a.xh5RefCaseDefined = reader.TryReadbool( InvoiceMap.xh5RefCaseDefined);
			a.xh5LstChgDT = reader.TryReadDateTime( InvoiceMap.LastModified);
			a.xh5LstChgUser = reader.TryReadstring( InvoiceMap.xh5LstChgUser);
			a.xh5FK_Assembly_PumpShop_GUID = reader.TryReadGuid( InvoiceMap.xh5FK_Assembly_PumpShop_GUID);
			a.xh5FK_Assembly_PumpShop = reader.TryReadstring( InvoiceMap.xh5FK_Assembly_PumpShop);
			a.xh5InvoiceID = reader.TryReadstring( InvoiceMap.xh5InvoiceID);
			a.xh5AccountingReferenceID = reader.TryReadstring( InvoiceMap.xh5AccountingReferenceID);
			a.xh5Invoiced = reader.TryReadbool( InvoiceMap.xh5Invoiced);
			a.xh5InvoiceDate = reader.TryReadDateTime( InvoiceMap.xh5InvoiceDate);
			a.xh5FK_BusinessOrganization_Producer_GUID = reader.TryReadGuid( InvoiceMap.xh5FK_BusinessOrganization_Producer_GUID);
			a.xh5FK_BusinessOrganization_Producer = reader.TryReadstring( InvoiceMap.xh5FK_BusinessOrganization_Producer);
			a.xh5FK_Assembly_Well_GUID = reader.TryReadGuid( InvoiceMap.xh5FK_Assembly_Well_GUID);
			a.xh5FK_Assembly_Well = reader.TryReadstring( InvoiceMap.xh5FK_Assembly_Well);
			a.xh5FK_WellCompletionXRef_GUID = reader.TryReadGuid( InvoiceMap.xh5FK_WellCompletionXRef_GUID);
			a.xh5FK_WellCompletionXRef = reader.TryReadstring( InvoiceMap.xh5FK_WellCompletionXRef);
			a.xh5ISO4217CurrencyCode = reader.TryReadstring( InvoiceMap.xh5ISO4217CurrencyCode);
			a.xh5ProductLineID = reader.TryReadstring( InvoiceMap.xh5ProductLineID);
			a.xh5ExternalTransactionComplete = reader.TryReadbool( InvoiceMap.xh5ExternalTransactionComplete);
			a.xh5ReturnTransactionMessages = reader.TryReadstring( InvoiceMap.xh5ReturnTransactionMessages);
			a.xh5Remarks = reader.TryReadstring( InvoiceMap.xh5Remarks);
			a.xh5FK_Job = reader.TryReadstring( InvoiceMap.xh5FK_Job);
			a.xh5FK_Job_GUID = reader.TryReadGuid( InvoiceMap.xh5FK_Job_GUID);
    
            return a;
        }

   		public override void RegisterParams()
		{
			Invoice a = (Invoice)dataObj;
			this.AddParam( InvoiceMap.ID, InvoiceMap.Param.ID, a.ID );
			this.AddParam( InvoiceMap.xh5PrimaryKey, InvoiceMap.Param.xh5PrimaryKey, a.xh5PrimaryKey );
			this.AddParam( InvoiceMap.xh5RefCaseDefined, InvoiceMap.Param.xh5RefCaseDefined, a.xh5RefCaseDefined );
			this.AddParam( InvoiceMap.LastModified, InvoiceMap.Param.LastModified, a.LastModified );
			this.AddParam( InvoiceMap.xh5LstChgUser, InvoiceMap.Param.xh5LstChgUser, a.xh5LstChgUser );
			this.AddParam( InvoiceMap.xh5FK_Assembly_PumpShop_GUID, InvoiceMap.Param.xh5FK_Assembly_PumpShop_GUID, a.xh5FK_Assembly_PumpShop_GUID );
			this.AddParam( InvoiceMap.xh5FK_Assembly_PumpShop, InvoiceMap.Param.xh5FK_Assembly_PumpShop, a.xh5FK_Assembly_PumpShop );
			this.AddParam( InvoiceMap.xh5InvoiceID, InvoiceMap.Param.xh5InvoiceID, a.xh5InvoiceID );
			this.AddParam( InvoiceMap.xh5AccountingReferenceID, InvoiceMap.Param.xh5AccountingReferenceID, a.xh5AccountingReferenceID );
			this.AddParam( InvoiceMap.xh5Invoiced, InvoiceMap.Param.xh5Invoiced, a.xh5Invoiced );
			this.AddParam( InvoiceMap.xh5InvoiceDate, InvoiceMap.Param.xh5InvoiceDate, a.xh5InvoiceDate );
			this.AddParam( InvoiceMap.xh5FK_BusinessOrganization_Producer_GUID, InvoiceMap.Param.xh5FK_BusinessOrganization_Producer_GUID, a.xh5FK_BusinessOrganization_Producer_GUID );
			this.AddParam( InvoiceMap.xh5FK_BusinessOrganization_Producer, InvoiceMap.Param.xh5FK_BusinessOrganization_Producer, a.xh5FK_BusinessOrganization_Producer );
			this.AddParam( InvoiceMap.xh5FK_Assembly_Well_GUID, InvoiceMap.Param.xh5FK_Assembly_Well_GUID, a.xh5FK_Assembly_Well_GUID );
			this.AddParam( InvoiceMap.xh5FK_Assembly_Well, InvoiceMap.Param.xh5FK_Assembly_Well, a.xh5FK_Assembly_Well );
			this.AddParam( InvoiceMap.xh5FK_WellCompletionXRef_GUID, InvoiceMap.Param.xh5FK_WellCompletionXRef_GUID, a.xh5FK_WellCompletionXRef_GUID );
			this.AddParam( InvoiceMap.xh5FK_WellCompletionXRef, InvoiceMap.Param.xh5FK_WellCompletionXRef, a.xh5FK_WellCompletionXRef );
			this.AddParam( InvoiceMap.xh5ISO4217CurrencyCode, InvoiceMap.Param.xh5ISO4217CurrencyCode, a.xh5ISO4217CurrencyCode );
			this.AddParam( InvoiceMap.xh5ProductLineID, InvoiceMap.Param.xh5ProductLineID, a.xh5ProductLineID );
			this.AddParam( InvoiceMap.xh5ExternalTransactionComplete, InvoiceMap.Param.xh5ExternalTransactionComplete, a.xh5ExternalTransactionComplete );
			this.AddParam( InvoiceMap.xh5ReturnTransactionMessages, InvoiceMap.Param.xh5ReturnTransactionMessages, a.xh5ReturnTransactionMessages );
			this.AddParam( InvoiceMap.xh5Remarks, InvoiceMap.Param.xh5Remarks, a.xh5Remarks );
			this.AddParam( InvoiceMap.xh5FK_Job, InvoiceMap.Param.xh5FK_Job, a.xh5FK_Job );
			this.AddParam( InvoiceMap.xh5FK_Job_GUID, InvoiceMap.Param.xh5FK_Job_GUID, a.xh5FK_Job_GUID );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the Job table in the PumpServicing Database.
    /// </summary>
    public partial class JobTransaction : BaseTransaction<Job>
    {
        public JobTransaction()
            : base(JobMap.TABLE_NAME, JobMap.ID)
        {
        }
        
        public JobTransaction( IBaseData _type )
			: base( _type, JobMap.TABLE_NAME, JobMap.ID )
		{
        }

        public JobTransaction( string _PrimaryKey )
			: base( JobMap.TABLE_NAME, JobMap.ID, _PrimaryKey )
		{
        }

        public JobTransaction( DateTime _lastModified )
            : base( JobMap.TABLE_NAME, JobMap.ID, _lastModified, JobMap.LastModified )
        {
        }				
    
        public override Job BuildFromReader(TransactionReader reader)
        {
            Job a = new Job( );
			a.ecgPrimaryKey_GUID = reader.TryReadGuid( JobMap.ID);
			a.ecgPrimaryKey = reader.TryReadstring( JobMap.ecgPrimaryKey);
			a.ecgLstChgDT = reader.TryReadDateTime( JobMap.LastModified);
			a.ecgLstChgUser = reader.TryReadstring( JobMap.ecgLstChgUser);
			a.ecgRefCaseDefined = reader.TryReadbool( JobMap.ecgRefCaseDefined);
			a.ecgFK_Assembly_GUID = reader.TryReadGuid( JobMap.ecgFK_Assembly_GUID);
			a.ecgFK_Assembly = reader.TryReadstring( JobMap.ecgFK_Assembly);
			a.ecgFK_BusinessOrganization_GUID = reader.TryReadGuid( JobMap.ecgFK_BusinessOrganization_GUID);
			a.ecgFK_BusinessOrganization = reader.TryReadstring( JobMap.ecgFK_BusinessOrganization);
			a.ecgFK_r_EventCategoryType_GUID = reader.TryReadGuid( JobMap.ecgFK_r_EventCategoryType_GUID);
			a.ecgFK_r_EventCategoryType = reader.TryReadstring( JobMap.ecgFK_r_EventCategoryType);
			a.ecgFK_r_EventCategoryReason_GUID = reader.TryReadGuid( JobMap.ecgFK_r_EventCategoryReason_GUID);
			a.ecgFK_r_EventCategoryReason = reader.TryReadstring( JobMap.ecgFK_r_EventCategoryReason);
			a.ecgEventBegDtTm = reader.TryReadDateTime( JobMap.ecgEventBegDtTm);
			a.ecgEventEndDtTm = reader.TryReadDateTime( JobMap.ecgEventEndDtTm);
			a.ecgFK_r_JobStatus_GUID = reader.TryReadGuid( JobMap.ecgFK_r_JobStatus_GUID);
			a.ecgFK_r_JobStatus = reader.TryReadstring( JobMap.ecgFK_r_JobStatus);
			a.ecgStatusDt = reader.TryReadDateTime( JobMap.ecgStatusDt);
			a.ecgStChgUser = reader.TryReadstring( JobMap.ecgStChgUser);
			a.ecgJobID = reader.TryReadstring( JobMap.ecgJobID);
			a.ecgOriginKey = reader.TryReadstring( JobMap.ecgOriginKey);
			a.ecgAcctRef = reader.TryReadstring( JobMap.ecgAcctRef);
			a.ecgFK_WellCompletionXRef_GUID = reader.TryReadGuid( JobMap.ecgFK_WellCompletionXRef_GUID);
			a.ecgFK_WellCompletionXRef = reader.TryReadstring( JobMap.ecgFK_WellCompletionXRef);
			a.ecgFK_r_PumpJobType_GUID = reader.TryReadGuid( JobMap.ecgFK_r_PumpJobType_GUID);
			a.ecgFK_r_PumpJobType = reader.TryReadstring( JobMap.ecgFK_r_PumpJobType);
			a.ecgRemarks = reader.TryReadstring( JobMap.ecgRemarks);
			a.ecgPrevRunDT = reader.TryReadDateTime( JobMap.ecgPrevRunDT);
			a.ecgWellFailedDate = reader.TryReadDateTime( JobMap.ecgWellFailedDate);
			a.ecgWellPullDT = reader.TryReadDateTime( JobMap.ecgWellPullDT);
    
            return a;
        }

   		public override void RegisterParams()
		{
			Job a = (Job)dataObj;
			this.AddParam( JobMap.ID, JobMap.Param.ID, a.ID );
			this.AddParam( JobMap.ecgPrimaryKey, JobMap.Param.ecgPrimaryKey, a.ecgPrimaryKey );
			this.AddParam( JobMap.LastModified, JobMap.Param.LastModified, a.LastModified );
			this.AddParam( JobMap.ecgLstChgUser, JobMap.Param.ecgLstChgUser, a.ecgLstChgUser );
			this.AddParam( JobMap.ecgRefCaseDefined, JobMap.Param.ecgRefCaseDefined, a.ecgRefCaseDefined );
			this.AddParam( JobMap.ecgFK_Assembly_GUID, JobMap.Param.ecgFK_Assembly_GUID, a.ecgFK_Assembly_GUID );
			this.AddParam( JobMap.ecgFK_Assembly, JobMap.Param.ecgFK_Assembly, a.ecgFK_Assembly );
			this.AddParam( JobMap.ecgFK_BusinessOrganization_GUID, JobMap.Param.ecgFK_BusinessOrganization_GUID, a.ecgFK_BusinessOrganization_GUID );
			this.AddParam( JobMap.ecgFK_BusinessOrganization, JobMap.Param.ecgFK_BusinessOrganization, a.ecgFK_BusinessOrganization );
			this.AddParam( JobMap.ecgFK_r_EventCategoryType_GUID, JobMap.Param.ecgFK_r_EventCategoryType_GUID, a.ecgFK_r_EventCategoryType_GUID );
			this.AddParam( JobMap.ecgFK_r_EventCategoryType, JobMap.Param.ecgFK_r_EventCategoryType, a.ecgFK_r_EventCategoryType );
			this.AddParam( JobMap.ecgFK_r_EventCategoryReason_GUID, JobMap.Param.ecgFK_r_EventCategoryReason_GUID, a.ecgFK_r_EventCategoryReason_GUID );
			this.AddParam( JobMap.ecgFK_r_EventCategoryReason, JobMap.Param.ecgFK_r_EventCategoryReason, a.ecgFK_r_EventCategoryReason );
			this.AddParam( JobMap.ecgEventBegDtTm, JobMap.Param.ecgEventBegDtTm, a.ecgEventBegDtTm );
			this.AddParam( JobMap.ecgEventEndDtTm, JobMap.Param.ecgEventEndDtTm, a.ecgEventEndDtTm );
			this.AddParam( JobMap.ecgFK_r_JobStatus_GUID, JobMap.Param.ecgFK_r_JobStatus_GUID, a.ecgFK_r_JobStatus_GUID );
			this.AddParam( JobMap.ecgFK_r_JobStatus, JobMap.Param.ecgFK_r_JobStatus, a.ecgFK_r_JobStatus );
			this.AddParam( JobMap.ecgStatusDt, JobMap.Param.ecgStatusDt, a.ecgStatusDt );
			this.AddParam( JobMap.ecgStChgUser, JobMap.Param.ecgStChgUser, a.ecgStChgUser );
			this.AddParam( JobMap.ecgJobID, JobMap.Param.ecgJobID, a.ecgJobID );
			this.AddParam( JobMap.ecgOriginKey, JobMap.Param.ecgOriginKey, a.ecgOriginKey );
			this.AddParam( JobMap.ecgAcctRef, JobMap.Param.ecgAcctRef, a.ecgAcctRef );
			this.AddParam( JobMap.ecgFK_WellCompletionXRef_GUID, JobMap.Param.ecgFK_WellCompletionXRef_GUID, a.ecgFK_WellCompletionXRef_GUID );
			this.AddParam( JobMap.ecgFK_WellCompletionXRef, JobMap.Param.ecgFK_WellCompletionXRef, a.ecgFK_WellCompletionXRef );
			this.AddParam( JobMap.ecgFK_r_PumpJobType_GUID, JobMap.Param.ecgFK_r_PumpJobType_GUID, a.ecgFK_r_PumpJobType_GUID );
			this.AddParam( JobMap.ecgFK_r_PumpJobType, JobMap.Param.ecgFK_r_PumpJobType, a.ecgFK_r_PumpJobType );
			this.AddParam( JobMap.ecgRemarks, JobMap.Param.ecgRemarks, a.ecgRemarks );
			this.AddParam( JobMap.ecgPrevRunDT, JobMap.Param.ecgPrevRunDT, a.ecgPrevRunDT );
			this.AddParam( JobMap.ecgWellFailedDate, JobMap.Param.ecgWellFailedDate, a.ecgWellFailedDate );
			this.AddParam( JobMap.ecgWellPullDT, JobMap.Param.ecgWellPullDT, a.ecgWellPullDT );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the JobStatusChangeLog table in the PumpServicing Database.
    /// </summary>
    public partial class JobStatusChangeLogTransaction : BaseTransaction<JobStatusChangeLog>
    {
        public JobStatusChangeLogTransaction()
            : base(JobStatusChangeLogMap.TABLE_NAME, JobStatusChangeLogMap.ID)
        {
        }
        
        public JobStatusChangeLogTransaction( IBaseData _type )
			: base( _type, JobStatusChangeLogMap.TABLE_NAME, JobStatusChangeLogMap.ID )
		{
        }

        public JobStatusChangeLogTransaction( string _PrimaryKey )
			: base( JobStatusChangeLogMap.TABLE_NAME, JobStatusChangeLogMap.ID, _PrimaryKey )
		{
        }

        public JobStatusChangeLogTransaction( DateTime _lastModified )
            : base( JobStatusChangeLogMap.TABLE_NAME, JobStatusChangeLogMap.ID, _lastModified, JobStatusChangeLogMap.LastModified )
        {
        }				
    
        public override JobStatusChangeLog BuildFromReader(TransactionReader reader)
        {
            JobStatusChangeLog a = new JobStatusChangeLog( );
			a.jscPrimaryKey_GUID = reader.TryReadGuid( JobStatusChangeLogMap.ID);
			a.jscPrimaryKey = reader.TryReadstring( JobStatusChangeLogMap.jscPrimaryKey);
			a.jscLstChgDT = reader.TryReadDateTime( JobStatusChangeLogMap.LastModified);
			a.jscLstChgUser = reader.TryReadstring( JobStatusChangeLogMap.jscLstChgUser);
			a.jscFK_Job_GUID = reader.TryReadGuid( JobStatusChangeLogMap.jscFK_Job_GUID);
			a.jscFK_Job = reader.TryReadstring( JobStatusChangeLogMap.jscFK_Job);
			a.jscFK_r_JobStatus_GUID = reader.TryReadGuid( JobStatusChangeLogMap.jscFK_r_JobStatus_GUID);
			a.jscFK_r_JobStatus = reader.TryReadstring( JobStatusChangeLogMap.jscFK_r_JobStatus);
			a.jscStatusDt = reader.TryReadDateTime( JobStatusChangeLogMap.jscStatusDt);
			a.jscStChgUser = reader.TryReadstring( JobStatusChangeLogMap.jscStChgUser);
    
            return a;
        }

   		public override void RegisterParams()
		{
			JobStatusChangeLog a = (JobStatusChangeLog)dataObj;
			this.AddParam( JobStatusChangeLogMap.ID, JobStatusChangeLogMap.Param.ID, a.ID );
			this.AddParam( JobStatusChangeLogMap.jscPrimaryKey, JobStatusChangeLogMap.Param.jscPrimaryKey, a.jscPrimaryKey );
			this.AddParam( JobStatusChangeLogMap.LastModified, JobStatusChangeLogMap.Param.LastModified, a.LastModified );
			this.AddParam( JobStatusChangeLogMap.jscLstChgUser, JobStatusChangeLogMap.Param.jscLstChgUser, a.jscLstChgUser );
			this.AddParam( JobStatusChangeLogMap.jscFK_Job_GUID, JobStatusChangeLogMap.Param.jscFK_Job_GUID, a.jscFK_Job_GUID );
			this.AddParam( JobStatusChangeLogMap.jscFK_Job, JobStatusChangeLogMap.Param.jscFK_Job, a.jscFK_Job );
			this.AddParam( JobStatusChangeLogMap.jscFK_r_JobStatus_GUID, JobStatusChangeLogMap.Param.jscFK_r_JobStatus_GUID, a.jscFK_r_JobStatus_GUID );
			this.AddParam( JobStatusChangeLogMap.jscFK_r_JobStatus, JobStatusChangeLogMap.Param.jscFK_r_JobStatus, a.jscFK_r_JobStatus );
			this.AddParam( JobStatusChangeLogMap.jscStatusDt, JobStatusChangeLogMap.Param.jscStatusDt, a.jscStatusDt );
			this.AddParam( JobStatusChangeLogMap.jscStChgUser, JobStatusChangeLogMap.Param.jscStChgUser, a.jscStChgUser );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the Lease table in the PumpServicing Database.
    /// </summary>
    public partial class LeaseTransaction : BaseTransaction<Lease>
    {
        public LeaseTransaction()
            : base(LeaseMap.TABLE_NAME, LeaseMap.ID)
        {
        }
        
        public LeaseTransaction( IBaseData _type )
			: base( _type, LeaseMap.TABLE_NAME, LeaseMap.ID )
		{
        }

        public LeaseTransaction( string _PrimaryKey )
			: base( LeaseMap.TABLE_NAME, LeaseMap.ID, _PrimaryKey )
		{
        }

        public LeaseTransaction( DateTime _lastModified )
            : base( LeaseMap.TABLE_NAME, LeaseMap.ID, _lastModified, LeaseMap.LastModified )
        {
        }				
    
        public override Lease BuildFromReader(TransactionReader reader)
        {
            Lease a = new Lease( );
			a.lsePrimaryKey_GUID = reader.TryReadGuid( LeaseMap.ID);
			a.lsePrimaryKey = reader.TryReadstring( LeaseMap.lsePrimaryKey);
			a.lseLstChgDT = reader.TryReadDateTime( LeaseMap.LastModified);
			a.lseLstChgUser = reader.TryReadstring( LeaseMap.lseLstChgUser);
			a.lseRefCaseDefined = reader.TryReadbool( LeaseMap.lseRefCaseDefined);
			a.lseRefUserDeleted = reader.TryReadbool( LeaseMap.lseRefUserDeleted);
			a.lseFK_BusinessOrganization_GUID = reader.TryReadGuid( LeaseMap.lseFK_BusinessOrganization_GUID);
			a.lseFK_BusinessOrganization = reader.TryReadstring( LeaseMap.lseFK_BusinessOrganization);
			a.lseLeaseID = reader.TryReadstring( LeaseMap.lseLeaseID);
			a.lseLeaseName = reader.TryReadstring( LeaseMap.lseLeaseName);
			a.lsePMTaxableStatus = reader.TryReadbool( LeaseMap.lsePMTaxableStatus);
			a.lsePMTaxRate = reader.TryReaddecimal( LeaseMap.lsePMTaxRate);
			a.lseCHTaxableStatus = reader.TryReadbool( LeaseMap.lseCHTaxableStatus);
			a.lseCHTaxRate = reader.TryReaddecimal( LeaseMap.lseCHTaxRate);
			a.lseWSTaxableStatus = reader.TryReadbool( LeaseMap.lseWSTaxableStatus);
			a.lseWSTaxRate = reader.TryReaddecimal( LeaseMap.lseWSTaxRate);
    
            return a;
        }

   		public override void RegisterParams()
		{
			Lease a = (Lease)dataObj;
			this.AddParam( LeaseMap.ID, LeaseMap.Param.ID, a.ID );
			this.AddParam( LeaseMap.lsePrimaryKey, LeaseMap.Param.lsePrimaryKey, a.lsePrimaryKey );
			this.AddParam( LeaseMap.LastModified, LeaseMap.Param.LastModified, a.LastModified );
			this.AddParam( LeaseMap.lseLstChgUser, LeaseMap.Param.lseLstChgUser, a.lseLstChgUser );
			this.AddParam( LeaseMap.lseRefCaseDefined, LeaseMap.Param.lseRefCaseDefined, a.lseRefCaseDefined );
			this.AddParam( LeaseMap.lseRefUserDeleted, LeaseMap.Param.lseRefUserDeleted, a.lseRefUserDeleted );
			this.AddParam( LeaseMap.lseFK_BusinessOrganization_GUID, LeaseMap.Param.lseFK_BusinessOrganization_GUID, a.lseFK_BusinessOrganization_GUID );
			this.AddParam( LeaseMap.lseFK_BusinessOrganization, LeaseMap.Param.lseFK_BusinessOrganization, a.lseFK_BusinessOrganization );
			this.AddParam( LeaseMap.lseLeaseID, LeaseMap.Param.lseLeaseID, a.lseLeaseID );
			this.AddParam( LeaseMap.lseLeaseName, LeaseMap.Param.lseLeaseName, a.lseLeaseName );
			this.AddParam( LeaseMap.lsePMTaxableStatus, LeaseMap.Param.lsePMTaxableStatus, a.lsePMTaxableStatus );
			this.AddParam( LeaseMap.lsePMTaxRate, LeaseMap.Param.lsePMTaxRate, a.lsePMTaxRate );
			this.AddParam( LeaseMap.lseCHTaxableStatus, LeaseMap.Param.lseCHTaxableStatus, a.lseCHTaxableStatus );
			this.AddParam( LeaseMap.lseCHTaxRate, LeaseMap.Param.lseCHTaxRate, a.lseCHTaxRate );
			this.AddParam( LeaseMap.lseWSTaxableStatus, LeaseMap.Param.lseWSTaxableStatus, a.lseWSTaxableStatus );
			this.AddParam( LeaseMap.lseWSTaxRate, LeaseMap.Param.lseWSTaxRate, a.lseWSTaxRate );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the Owner table in the PumpServicing Database.
    /// </summary>
    public partial class OwnerTransaction : BaseTransaction<Owner>
    {
        public OwnerTransaction()
            : base(OwnerMap.TABLE_NAME, OwnerMap.ID)
        {
        }
        
        public OwnerTransaction( IBaseData _type )
			: base( _type, OwnerMap.TABLE_NAME, OwnerMap.ID )
		{
        }

        public OwnerTransaction( string _PrimaryKey )
			: base( OwnerMap.TABLE_NAME, OwnerMap.ID, _PrimaryKey )
		{
        }

        public OwnerTransaction( DateTime _lastModified )
            : base( OwnerMap.TABLE_NAME, OwnerMap.ID, _lastModified, OwnerMap.LastModified )
        {
        }				
    
        public override Owner BuildFromReader(TransactionReader reader)
        {
            Owner a = new Owner( );
			a.ownPrimaryKey = reader.TryReadstring( OwnerMap.ID);
			a.ownLstChgDT = reader.TryReadDateTime( OwnerMap.LastModified);
			a.ownLstChgUser = reader.TryReadstring( OwnerMap.ownLstChgUser);
			a.ownRefCaseDefined = reader.TryReadbool( OwnerMap.ownRefCaseDefined);
			a.ownRefUserDeleted = reader.TryReadbool( OwnerMap.ownRefUserDeleted);
			a.ownOwnerName = reader.TryReadstring( OwnerMap.ownOwnerName);
			a.ownFK_BusinessOrganization = reader.TryReadstring( OwnerMap.ownFK_BusinessOrganization);
    
            return a;
        }

   		public override void RegisterParams()
		{
			Owner a = (Owner)dataObj;
			this.AddParam( OwnerMap.ID, OwnerMap.Param.ID, a.ID );
			this.AddParam( OwnerMap.LastModified, OwnerMap.Param.LastModified, a.LastModified );
			this.AddParam( OwnerMap.ownLstChgUser, OwnerMap.Param.ownLstChgUser, a.ownLstChgUser );
			this.AddParam( OwnerMap.ownRefCaseDefined, OwnerMap.Param.ownRefCaseDefined, a.ownRefCaseDefined );
			this.AddParam( OwnerMap.ownRefUserDeleted, OwnerMap.Param.ownRefUserDeleted, a.ownRefUserDeleted );
			this.AddParam( OwnerMap.ownOwnerName, OwnerMap.Param.ownOwnerName, a.ownOwnerName );
			this.AddParam( OwnerMap.ownFK_BusinessOrganization, OwnerMap.Param.ownFK_BusinessOrganization, a.ownFK_BusinessOrganization );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the StickyNotes table in the PumpServicing Database.
    /// </summary>
    public partial class StickyNotesTransaction : BaseTransaction<StickyNotes>
    {
        public StickyNotesTransaction()
            : base(StickyNotesMap.TABLE_NAME, StickyNotesMap.ID)
        {
        }
        
        public StickyNotesTransaction( IBaseData _type )
			: base( _type, StickyNotesMap.TABLE_NAME, StickyNotesMap.ID )
		{
        }

        public StickyNotesTransaction( string _PrimaryKey )
			: base( StickyNotesMap.TABLE_NAME, StickyNotesMap.ID, _PrimaryKey )
		{
        }

        public StickyNotesTransaction( DateTime _lastModified )
            : base( StickyNotesMap.TABLE_NAME, StickyNotesMap.ID, _lastModified, StickyNotesMap.LastModified )
        {
        }				
    
        public override StickyNotes BuildFromReader(TransactionReader reader)
        {
            StickyNotes a = new StickyNotes( );
			a.styPrimaryKey_GUID = reader.TryReadGuid( StickyNotesMap.ID);
			a.styPrimaryKey = reader.TryReadstring( StickyNotesMap.styPrimaryKey);
			a.styLstChgDT = reader.TryReadDateTime( StickyNotesMap.LastModified);
			a.styLstChgUser = reader.TryReadstring( StickyNotesMap.styLstChgUser);
			a.styFK_r_MessageActivityType_GUID = reader.TryReadGuid( StickyNotesMap.styFK_r_MessageActivityType_GUID);
			a.styFK_r_MessageActivityType = reader.TryReadstring( StickyNotesMap.styFK_r_MessageActivityType);
			a.styMsgOriginDT = reader.TryReadDateTime( StickyNotesMap.styMsgOriginDT);
			a.styFK_Assembly_GUID = reader.TryReadGuid( StickyNotesMap.styFK_Assembly_GUID);
			a.styFK_Assembly = reader.TryReadstring( StickyNotesMap.styFK_Assembly);
			a.styFK_Event_GUID = reader.TryReadGuid( StickyNotesMap.styFK_Event_GUID);
			a.styFK_Event = reader.TryReadstring( StickyNotesMap.styFK_Event);
			a.styFK_r_StickyNoteStatus_GUID = reader.TryReadGuid( StickyNotesMap.styFK_r_StickyNoteStatus_GUID);
			a.styFK_r_StickyNoteStatus = reader.TryReadstring( StickyNotesMap.styFK_r_StickyNoteStatus);
			a.styFK_r_MessageActivityPriorityCd_GUID = reader.TryReadGuid( StickyNotesMap.styFK_r_MessageActivityPriorityCd_GUID);
			a.styFK_r_MessageActivityPriorityCd = reader.TryReadstring( StickyNotesMap.styFK_r_MessageActivityPriorityCd);
			a.styMsgCompletionDT = reader.TryReadDateTime( StickyNotesMap.styMsgCompletionDT);
			a.stySender = reader.TryReadstring( StickyNotesMap.stySender);
			a.styRecipient = reader.TryReadstring( StickyNotesMap.styRecipient);
			a.styBriefDescription = reader.TryReadstring( StickyNotesMap.styBriefDescription);
			a.styCompletionComments = reader.TryReadstring( StickyNotesMap.styCompletionComments);
			a.styDocumentFileName = reader.TryReadstring( StickyNotesMap.styDocumentFileName);
			a.styMessage = reader.TryReadstring( StickyNotesMap.styMessage);
    
            return a;
        }

   		public override void RegisterParams()
		{
			StickyNotes a = (StickyNotes)dataObj;
			this.AddParam( StickyNotesMap.ID, StickyNotesMap.Param.ID, a.ID );
			this.AddParam( StickyNotesMap.styPrimaryKey, StickyNotesMap.Param.styPrimaryKey, a.styPrimaryKey );
			this.AddParam( StickyNotesMap.LastModified, StickyNotesMap.Param.LastModified, a.LastModified );
			this.AddParam( StickyNotesMap.styLstChgUser, StickyNotesMap.Param.styLstChgUser, a.styLstChgUser );
			this.AddParam( StickyNotesMap.styFK_r_MessageActivityType_GUID, StickyNotesMap.Param.styFK_r_MessageActivityType_GUID, a.styFK_r_MessageActivityType_GUID );
			this.AddParam( StickyNotesMap.styFK_r_MessageActivityType, StickyNotesMap.Param.styFK_r_MessageActivityType, a.styFK_r_MessageActivityType );
			this.AddParam( StickyNotesMap.styMsgOriginDT, StickyNotesMap.Param.styMsgOriginDT, a.styMsgOriginDT );
			this.AddParam( StickyNotesMap.styFK_Assembly_GUID, StickyNotesMap.Param.styFK_Assembly_GUID, a.styFK_Assembly_GUID );
			this.AddParam( StickyNotesMap.styFK_Assembly, StickyNotesMap.Param.styFK_Assembly, a.styFK_Assembly );
			this.AddParam( StickyNotesMap.styFK_Event_GUID, StickyNotesMap.Param.styFK_Event_GUID, a.styFK_Event_GUID );
			this.AddParam( StickyNotesMap.styFK_Event, StickyNotesMap.Param.styFK_Event, a.styFK_Event );
			this.AddParam( StickyNotesMap.styFK_r_StickyNoteStatus_GUID, StickyNotesMap.Param.styFK_r_StickyNoteStatus_GUID, a.styFK_r_StickyNoteStatus_GUID );
			this.AddParam( StickyNotesMap.styFK_r_StickyNoteStatus, StickyNotesMap.Param.styFK_r_StickyNoteStatus, a.styFK_r_StickyNoteStatus );
			this.AddParam( StickyNotesMap.styFK_r_MessageActivityPriorityCd_GUID, StickyNotesMap.Param.styFK_r_MessageActivityPriorityCd_GUID, a.styFK_r_MessageActivityPriorityCd_GUID );
			this.AddParam( StickyNotesMap.styFK_r_MessageActivityPriorityCd, StickyNotesMap.Param.styFK_r_MessageActivityPriorityCd, a.styFK_r_MessageActivityPriorityCd );
			this.AddParam( StickyNotesMap.styMsgCompletionDT, StickyNotesMap.Param.styMsgCompletionDT, a.styMsgCompletionDT );
			this.AddParam( StickyNotesMap.stySender, StickyNotesMap.Param.stySender, a.stySender );
			this.AddParam( StickyNotesMap.styRecipient, StickyNotesMap.Param.styRecipient, a.styRecipient );
			this.AddParam( StickyNotesMap.styBriefDescription, StickyNotesMap.Param.styBriefDescription, a.styBriefDescription );
			this.AddParam( StickyNotesMap.styCompletionComments, StickyNotesMap.Param.styCompletionComments, a.styCompletionComments );
			this.AddParam( StickyNotesMap.styDocumentFileName, StickyNotesMap.Param.styDocumentFileName, a.styDocumentFileName );
			this.AddParam( StickyNotesMap.styMessage, StickyNotesMap.Param.styMessage, a.styMessage );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the TemplatePump table in the PumpServicing Database.
    /// </summary>
    public partial class TemplatePumpTransaction : BaseTransaction<TemplatePump>
    {
        public TemplatePumpTransaction()
            : base(TemplatePumpMap.TABLE_NAME, TemplatePumpMap.ID)
        {
        }
        
        public TemplatePumpTransaction( IBaseData _type )
			: base( _type, TemplatePumpMap.TABLE_NAME, TemplatePumpMap.ID )
		{
        }

        public TemplatePumpTransaction( string _PrimaryKey )
			: base( TemplatePumpMap.TABLE_NAME, TemplatePumpMap.ID, _PrimaryKey )
		{
        }

        public TemplatePumpTransaction( DateTime _lastModified )
            : base( TemplatePumpMap.TABLE_NAME, TemplatePumpMap.ID, _lastModified, TemplatePumpMap.LastModified )
        {
        }				
    
        public override TemplatePump BuildFromReader(TransactionReader reader)
        {
            TemplatePump a = new TemplatePump( );
			a.tphPrimaryKey_GUID = reader.TryReadGuid( TemplatePumpMap.ID);
			a.tphPrimaryKey = reader.TryReadstring( TemplatePumpMap.tphPrimaryKey);
			a.tphLanguageCd = reader.TryReadstring( TemplatePumpMap.tphLanguageCd);
			a.tphLstChgDT = reader.TryReadDateTime( TemplatePumpMap.LastModified);
			a.tphLstChgUser = reader.TryReadstring( TemplatePumpMap.tphLstChgUser);
			a.tphRefCaseDefined = reader.TryReadbool( TemplatePumpMap.tphRefCaseDefined);
			a.tphRefUserDeleted = reader.TryReadbool( TemplatePumpMap.tphRefUserDeleted);
			a.tphTemplateDescription = reader.TryReadstring( TemplatePumpMap.tphTemplateDescription);
			a.tphFK_r_APIPumpGraphics_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APIPumpGraphics_GUID);
			a.tphFK_r_APIPumpGraphics = reader.TryReadstring( TemplatePumpMap.tphFK_r_APIPumpGraphics);
			a.tphFK_r_APISRPTubingSize_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APISRPTubingSize_GUID);
			a.tphFK_r_APISRPTubingSize = reader.TryReadstring( TemplatePumpMap.tphFK_r_APISRPTubingSize);
			a.tphFK_r_APISRPPumpBore_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APISRPPumpBore_GUID);
			a.tphFK_r_APISRPPumpBore = reader.TryReadstring( TemplatePumpMap.tphFK_r_APISRPPumpBore);
			a.tphFK_r_APISRPPumpType_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APISRPPumpType_GUID);
			a.tphFK_r_APISRPPumpType = reader.TryReadstring( TemplatePumpMap.tphFK_r_APISRPPumpType);
			a.tphFK_r_APISRPBarrelType_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APISRPBarrelType_GUID);
			a.tphFK_r_APISRPBarrelType = reader.TryReadstring( TemplatePumpMap.tphFK_r_APISRPBarrelType);
			a.tphFK_r_APISRPSeatAssyLocation_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APISRPSeatAssyLocation_GUID);
			a.tphFK_r_APISRPSeatAssyLocation = reader.TryReadstring( TemplatePumpMap.tphFK_r_APISRPSeatAssyLocation);
			a.tphFK_r_APISRPSeatAssyType_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APISRPSeatAssyType_GUID);
			a.tphFK_r_APISRPSeatAssyType = reader.TryReadstring( TemplatePumpMap.tphFK_r_APISRPSeatAssyType);
			a.tphAPIBarrelLength = reader.TryReaddecimal( TemplatePumpMap.tphAPIBarrelLength);
			a.tphAPIPlungerLength = reader.TryReaddecimal( TemplatePumpMap.tphAPIPlungerLength);
			a.tphAPIExtensionCouplingUpperLength = reader.TryReaddecimal( TemplatePumpMap.tphAPIExtensionCouplingUpperLength);
			a.tphAPIExtensionCouplingLowerLength = reader.TryReaddecimal( TemplatePumpMap.tphAPIExtensionCouplingLowerLength);
			a.tphFK_r_APISRPExtPumpType_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APISRPExtPumpType_GUID);
			a.tphFK_r_APISRPExtPumpType = reader.TryReadstring( TemplatePumpMap.tphFK_r_APISRPExtPumpType);
			a.tphFK_r_APISRPExtBarrelType_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APISRPExtBarrelType_GUID);
			a.tphFK_r_APISRPExtBarrelType = reader.TryReadstring( TemplatePumpMap.tphFK_r_APISRPExtBarrelType);
			a.tphFK_r_APISRPExtSeatAssyLocation_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APISRPExtSeatAssyLocation_GUID);
			a.tphFK_r_APISRPExtSeatAssyLocation = reader.TryReadstring( TemplatePumpMap.tphFK_r_APISRPExtSeatAssyLocation);
			a.tphFK_r_APISRPExtSeatAssyType_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APISRPExtSeatAssyType_GUID);
			a.tphFK_r_APISRPExtSeatAssyType = reader.TryReadstring( TemplatePumpMap.tphFK_r_APISRPExtSeatAssyType);
			a.tphFK_r_APISRPExtSand_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APISRPExtSand_GUID);
			a.tphFK_r_APISRPExtSand = reader.TryReadstring( TemplatePumpMap.tphFK_r_APISRPExtSand);
			a.tphFK_r_APISRPExtBblAcc_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APISRPExtBblAcc_GUID);
			a.tphFK_r_APISRPExtBblAcc = reader.TryReadstring( TemplatePumpMap.tphFK_r_APISRPExtBblAcc);
			a.tphFK_r_APISRPExtPlgAcc_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APISRPExtPlgAcc_GUID);
			a.tphFK_r_APISRPExtPlgAcc = reader.TryReadstring( TemplatePumpMap.tphFK_r_APISRPExtPlgAcc);
			a.tphFK_r_APISRPExtPlgType_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APISRPExtPlgType_GUID);
			a.tphFK_r_APISRPExtPlgType = reader.TryReadstring( TemplatePumpMap.tphFK_r_APISRPExtPlgType);
			a.tphFK_r_APISRPExtPlgPin_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APISRPExtPlgPin_GUID);
			a.tphFK_r_APISRPExtPlgPin = reader.TryReadstring( TemplatePumpMap.tphFK_r_APISRPExtPlgPin);
			a.tphFK_r_APISRPExtSV_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APISRPExtSV_GUID);
			a.tphFK_r_APISRPExtSV = reader.TryReadstring( TemplatePumpMap.tphFK_r_APISRPExtSV);
			a.tphFK_r_APISRPExtSVCage_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APISRPExtSVCage_GUID);
			a.tphFK_r_APISRPExtSVCage = reader.TryReadstring( TemplatePumpMap.tphFK_r_APISRPExtSVCage);
			a.tphFK_r_APISRPExtTV_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APISRPExtTV_GUID);
			a.tphFK_r_APISRPExtTV = reader.TryReadstring( TemplatePumpMap.tphFK_r_APISRPExtTV);
			a.tphFK_r_APISRPExtTVCage_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APISRPExtTVCage_GUID);
			a.tphFK_r_APISRPExtTVCage = reader.TryReadstring( TemplatePumpMap.tphFK_r_APISRPExtTVCage);
			a.tphFK_r_APISRPExtTVStPlg_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APISRPExtTVStPlg_GUID);
			a.tphFK_r_APISRPExtTVStPlg = reader.TryReadstring( TemplatePumpMap.tphFK_r_APISRPExtTVStPlg);
			a.tphFK_r_APISRPExtVRod_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APISRPExtVRod_GUID);
			a.tphFK_r_APISRPExtVRod = reader.TryReadstring( TemplatePumpMap.tphFK_r_APISRPExtVRod);
			a.tphFK_r_APISRPExtWiper_GUID = reader.TryReadGuid( TemplatePumpMap.tphFK_r_APISRPExtWiper_GUID);
			a.tphFK_r_APISRPExtWiper = reader.TryReadstring( TemplatePumpMap.tphFK_r_APISRPExtWiper);
			a.tphlBblPlgAvgClearance = reader.TryReaddecimal( TemplatePumpMap.tphlBblPlgAvgClearance);
			a.tphMaxSL = reader.TryReaddecimal( TemplatePumpMap.tphMaxSL);
			a.tphHelpText = reader.TryReadstring( TemplatePumpMap.tphHelpText);
    
            return a;
        }

   		public override void RegisterParams()
		{
			TemplatePump a = (TemplatePump)dataObj;
			this.AddParam( TemplatePumpMap.ID, TemplatePumpMap.Param.ID, a.ID );
			this.AddParam( TemplatePumpMap.tphPrimaryKey, TemplatePumpMap.Param.tphPrimaryKey, a.tphPrimaryKey );
			this.AddParam( TemplatePumpMap.tphLanguageCd, TemplatePumpMap.Param.tphLanguageCd, a.tphLanguageCd );
			this.AddParam( TemplatePumpMap.LastModified, TemplatePumpMap.Param.LastModified, a.LastModified );
			this.AddParam( TemplatePumpMap.tphLstChgUser, TemplatePumpMap.Param.tphLstChgUser, a.tphLstChgUser );
			this.AddParam( TemplatePumpMap.tphRefCaseDefined, TemplatePumpMap.Param.tphRefCaseDefined, a.tphRefCaseDefined );
			this.AddParam( TemplatePumpMap.tphRefUserDeleted, TemplatePumpMap.Param.tphRefUserDeleted, a.tphRefUserDeleted );
			this.AddParam( TemplatePumpMap.tphTemplateDescription, TemplatePumpMap.Param.tphTemplateDescription, a.tphTemplateDescription );
			this.AddParam( TemplatePumpMap.tphFK_r_APIPumpGraphics_GUID, TemplatePumpMap.Param.tphFK_r_APIPumpGraphics_GUID, a.tphFK_r_APIPumpGraphics_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APIPumpGraphics, TemplatePumpMap.Param.tphFK_r_APIPumpGraphics, a.tphFK_r_APIPumpGraphics );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPTubingSize_GUID, TemplatePumpMap.Param.tphFK_r_APISRPTubingSize_GUID, a.tphFK_r_APISRPTubingSize_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPTubingSize, TemplatePumpMap.Param.tphFK_r_APISRPTubingSize, a.tphFK_r_APISRPTubingSize );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPPumpBore_GUID, TemplatePumpMap.Param.tphFK_r_APISRPPumpBore_GUID, a.tphFK_r_APISRPPumpBore_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPPumpBore, TemplatePumpMap.Param.tphFK_r_APISRPPumpBore, a.tphFK_r_APISRPPumpBore );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPPumpType_GUID, TemplatePumpMap.Param.tphFK_r_APISRPPumpType_GUID, a.tphFK_r_APISRPPumpType_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPPumpType, TemplatePumpMap.Param.tphFK_r_APISRPPumpType, a.tphFK_r_APISRPPumpType );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPBarrelType_GUID, TemplatePumpMap.Param.tphFK_r_APISRPBarrelType_GUID, a.tphFK_r_APISRPBarrelType_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPBarrelType, TemplatePumpMap.Param.tphFK_r_APISRPBarrelType, a.tphFK_r_APISRPBarrelType );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPSeatAssyLocation_GUID, TemplatePumpMap.Param.tphFK_r_APISRPSeatAssyLocation_GUID, a.tphFK_r_APISRPSeatAssyLocation_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPSeatAssyLocation, TemplatePumpMap.Param.tphFK_r_APISRPSeatAssyLocation, a.tphFK_r_APISRPSeatAssyLocation );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPSeatAssyType_GUID, TemplatePumpMap.Param.tphFK_r_APISRPSeatAssyType_GUID, a.tphFK_r_APISRPSeatAssyType_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPSeatAssyType, TemplatePumpMap.Param.tphFK_r_APISRPSeatAssyType, a.tphFK_r_APISRPSeatAssyType );
			this.AddParam( TemplatePumpMap.tphAPIBarrelLength, TemplatePumpMap.Param.tphAPIBarrelLength, a.tphAPIBarrelLength );
			this.AddParam( TemplatePumpMap.tphAPIPlungerLength, TemplatePumpMap.Param.tphAPIPlungerLength, a.tphAPIPlungerLength );
			this.AddParam( TemplatePumpMap.tphAPIExtensionCouplingUpperLength, TemplatePumpMap.Param.tphAPIExtensionCouplingUpperLength, a.tphAPIExtensionCouplingUpperLength );
			this.AddParam( TemplatePumpMap.tphAPIExtensionCouplingLowerLength, TemplatePumpMap.Param.tphAPIExtensionCouplingLowerLength, a.tphAPIExtensionCouplingLowerLength );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtPumpType_GUID, TemplatePumpMap.Param.tphFK_r_APISRPExtPumpType_GUID, a.tphFK_r_APISRPExtPumpType_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtPumpType, TemplatePumpMap.Param.tphFK_r_APISRPExtPumpType, a.tphFK_r_APISRPExtPumpType );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtBarrelType_GUID, TemplatePumpMap.Param.tphFK_r_APISRPExtBarrelType_GUID, a.tphFK_r_APISRPExtBarrelType_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtBarrelType, TemplatePumpMap.Param.tphFK_r_APISRPExtBarrelType, a.tphFK_r_APISRPExtBarrelType );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtSeatAssyLocation_GUID, TemplatePumpMap.Param.tphFK_r_APISRPExtSeatAssyLocation_GUID, a.tphFK_r_APISRPExtSeatAssyLocation_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtSeatAssyLocation, TemplatePumpMap.Param.tphFK_r_APISRPExtSeatAssyLocation, a.tphFK_r_APISRPExtSeatAssyLocation );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtSeatAssyType_GUID, TemplatePumpMap.Param.tphFK_r_APISRPExtSeatAssyType_GUID, a.tphFK_r_APISRPExtSeatAssyType_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtSeatAssyType, TemplatePumpMap.Param.tphFK_r_APISRPExtSeatAssyType, a.tphFK_r_APISRPExtSeatAssyType );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtSand_GUID, TemplatePumpMap.Param.tphFK_r_APISRPExtSand_GUID, a.tphFK_r_APISRPExtSand_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtSand, TemplatePumpMap.Param.tphFK_r_APISRPExtSand, a.tphFK_r_APISRPExtSand );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtBblAcc_GUID, TemplatePumpMap.Param.tphFK_r_APISRPExtBblAcc_GUID, a.tphFK_r_APISRPExtBblAcc_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtBblAcc, TemplatePumpMap.Param.tphFK_r_APISRPExtBblAcc, a.tphFK_r_APISRPExtBblAcc );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtPlgAcc_GUID, TemplatePumpMap.Param.tphFK_r_APISRPExtPlgAcc_GUID, a.tphFK_r_APISRPExtPlgAcc_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtPlgAcc, TemplatePumpMap.Param.tphFK_r_APISRPExtPlgAcc, a.tphFK_r_APISRPExtPlgAcc );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtPlgType_GUID, TemplatePumpMap.Param.tphFK_r_APISRPExtPlgType_GUID, a.tphFK_r_APISRPExtPlgType_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtPlgType, TemplatePumpMap.Param.tphFK_r_APISRPExtPlgType, a.tphFK_r_APISRPExtPlgType );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtPlgPin_GUID, TemplatePumpMap.Param.tphFK_r_APISRPExtPlgPin_GUID, a.tphFK_r_APISRPExtPlgPin_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtPlgPin, TemplatePumpMap.Param.tphFK_r_APISRPExtPlgPin, a.tphFK_r_APISRPExtPlgPin );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtSV_GUID, TemplatePumpMap.Param.tphFK_r_APISRPExtSV_GUID, a.tphFK_r_APISRPExtSV_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtSV, TemplatePumpMap.Param.tphFK_r_APISRPExtSV, a.tphFK_r_APISRPExtSV );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtSVCage_GUID, TemplatePumpMap.Param.tphFK_r_APISRPExtSVCage_GUID, a.tphFK_r_APISRPExtSVCage_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtSVCage, TemplatePumpMap.Param.tphFK_r_APISRPExtSVCage, a.tphFK_r_APISRPExtSVCage );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtTV_GUID, TemplatePumpMap.Param.tphFK_r_APISRPExtTV_GUID, a.tphFK_r_APISRPExtTV_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtTV, TemplatePumpMap.Param.tphFK_r_APISRPExtTV, a.tphFK_r_APISRPExtTV );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtTVCage_GUID, TemplatePumpMap.Param.tphFK_r_APISRPExtTVCage_GUID, a.tphFK_r_APISRPExtTVCage_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtTVCage, TemplatePumpMap.Param.tphFK_r_APISRPExtTVCage, a.tphFK_r_APISRPExtTVCage );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtTVStPlg_GUID, TemplatePumpMap.Param.tphFK_r_APISRPExtTVStPlg_GUID, a.tphFK_r_APISRPExtTVStPlg_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtTVStPlg, TemplatePumpMap.Param.tphFK_r_APISRPExtTVStPlg, a.tphFK_r_APISRPExtTVStPlg );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtVRod_GUID, TemplatePumpMap.Param.tphFK_r_APISRPExtVRod_GUID, a.tphFK_r_APISRPExtVRod_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtVRod, TemplatePumpMap.Param.tphFK_r_APISRPExtVRod, a.tphFK_r_APISRPExtVRod );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtWiper_GUID, TemplatePumpMap.Param.tphFK_r_APISRPExtWiper_GUID, a.tphFK_r_APISRPExtWiper_GUID );
			this.AddParam( TemplatePumpMap.tphFK_r_APISRPExtWiper, TemplatePumpMap.Param.tphFK_r_APISRPExtWiper, a.tphFK_r_APISRPExtWiper );
			this.AddParam( TemplatePumpMap.tphlBblPlgAvgClearance, TemplatePumpMap.Param.tphlBblPlgAvgClearance, a.tphlBblPlgAvgClearance );
			this.AddParam( TemplatePumpMap.tphMaxSL, TemplatePumpMap.Param.tphMaxSL, a.tphMaxSL );
			this.AddParam( TemplatePumpMap.tphHelpText, TemplatePumpMap.Param.tphHelpText, a.tphHelpText );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the TemplatePumpDetail table in the PumpServicing Database.
    /// </summary>
    public partial class TemplatePumpDetailTransaction : BaseTransaction<TemplatePumpDetail>
    {
        public TemplatePumpDetailTransaction()
            : base(TemplatePumpDetailMap.TABLE_NAME, TemplatePumpDetailMap.ID)
        {
        }
        
        public TemplatePumpDetailTransaction( IBaseData _type )
			: base( _type, TemplatePumpDetailMap.TABLE_NAME, TemplatePumpDetailMap.ID )
		{
        }

        public TemplatePumpDetailTransaction( string _PrimaryKey )
			: base( TemplatePumpDetailMap.TABLE_NAME, TemplatePumpDetailMap.ID, _PrimaryKey )
		{
        }

        public TemplatePumpDetailTransaction( DateTime _lastModified )
            : base( TemplatePumpDetailMap.TABLE_NAME, TemplatePumpDetailMap.ID, _lastModified, TemplatePumpDetailMap.LastModified )
        {
        }				
    
        public override TemplatePumpDetail BuildFromReader(TransactionReader reader)
        {
            TemplatePumpDetail a = new TemplatePumpDetail( );
			a.tpdPrimaryKey_GUID = reader.TryReadGuid( TemplatePumpDetailMap.ID);
			a.tpdPrimaryKey = reader.TryReadstring( TemplatePumpDetailMap.tpdPrimaryKey);
			a.tpdLstChgDT = reader.TryReadDateTime( TemplatePumpDetailMap.LastModified);
			a.tpdLstChgUser = reader.TryReadstring( TemplatePumpDetailMap.tpdLstChgUser);
			a.tpdRefCaseDefined = reader.TryReadbool( TemplatePumpDetailMap.tpdRefCaseDefined);
			a.tpdRefUserDeleted = reader.TryReadbool( TemplatePumpDetailMap.tpdRefUserDeleted);
			a.tpdFK_TemplatePump_GUID = reader.TryReadGuid( TemplatePumpDetailMap.tpdFK_TemplatePump_GUID);
			a.tpdFK_TemplatePump = reader.TryReadstring( TemplatePumpDetailMap.tpdFK_TemplatePump);
			a.tpdFK_TemplateSubAssembly_GUID = reader.TryReadGuid( TemplatePumpDetailMap.tpdFK_TemplateSubAssembly_GUID);
			a.tpdFK_TemplateSubAssembly = reader.TryReadstring( TemplatePumpDetailMap.tpdFK_TemplateSubAssembly);
    
            return a;
        }

   		public override void RegisterParams()
		{
			TemplatePumpDetail a = (TemplatePumpDetail)dataObj;
			this.AddParam( TemplatePumpDetailMap.ID, TemplatePumpDetailMap.Param.ID, a.ID );
			this.AddParam( TemplatePumpDetailMap.tpdPrimaryKey, TemplatePumpDetailMap.Param.tpdPrimaryKey, a.tpdPrimaryKey );
			this.AddParam( TemplatePumpDetailMap.LastModified, TemplatePumpDetailMap.Param.LastModified, a.LastModified );
			this.AddParam( TemplatePumpDetailMap.tpdLstChgUser, TemplatePumpDetailMap.Param.tpdLstChgUser, a.tpdLstChgUser );
			this.AddParam( TemplatePumpDetailMap.tpdRefCaseDefined, TemplatePumpDetailMap.Param.tpdRefCaseDefined, a.tpdRefCaseDefined );
			this.AddParam( TemplatePumpDetailMap.tpdRefUserDeleted, TemplatePumpDetailMap.Param.tpdRefUserDeleted, a.tpdRefUserDeleted );
			this.AddParam( TemplatePumpDetailMap.tpdFK_TemplatePump_GUID, TemplatePumpDetailMap.Param.tpdFK_TemplatePump_GUID, a.tpdFK_TemplatePump_GUID );
			this.AddParam( TemplatePumpDetailMap.tpdFK_TemplatePump, TemplatePumpDetailMap.Param.tpdFK_TemplatePump, a.tpdFK_TemplatePump );
			this.AddParam( TemplatePumpDetailMap.tpdFK_TemplateSubAssembly_GUID, TemplatePumpDetailMap.Param.tpdFK_TemplateSubAssembly_GUID, a.tpdFK_TemplateSubAssembly_GUID );
			this.AddParam( TemplatePumpDetailMap.tpdFK_TemplateSubAssembly, TemplatePumpDetailMap.Param.tpdFK_TemplateSubAssembly, a.tpdFK_TemplateSubAssembly );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the TemplateSubAssembly table in the PumpServicing Database.
    /// </summary>
    public partial class TemplateSubAssemblyTransaction : BaseTransaction<TemplateSubAssembly>
    {
        public TemplateSubAssemblyTransaction()
            : base(TemplateSubAssemblyMap.TABLE_NAME, TemplateSubAssemblyMap.ID)
        {
        }
        
        public TemplateSubAssemblyTransaction( IBaseData _type )
			: base( _type, TemplateSubAssemblyMap.TABLE_NAME, TemplateSubAssemblyMap.ID )
		{
        }

        public TemplateSubAssemblyTransaction( string _PrimaryKey )
			: base( TemplateSubAssemblyMap.TABLE_NAME, TemplateSubAssemblyMap.ID, _PrimaryKey )
		{
        }

        public TemplateSubAssemblyTransaction( DateTime _lastModified )
            : base( TemplateSubAssemblyMap.TABLE_NAME, TemplateSubAssemblyMap.ID, _lastModified, TemplateSubAssemblyMap.LastModified )
        {
        }				
    
        public override TemplateSubAssembly BuildFromReader(TransactionReader reader)
        {
            TemplateSubAssembly a = new TemplateSubAssembly( );
			a.tahPrimaryKey_GUID = reader.TryReadGuid( TemplateSubAssemblyMap.ID);
			a.tahPrimaryKey = reader.TryReadstring( TemplateSubAssemblyMap.tahPrimaryKey);
			a.tahLanguageCd = reader.TryReadstring( TemplateSubAssemblyMap.tahLanguageCd);
			a.tahLstChgDT = reader.TryReadDateTime( TemplateSubAssemblyMap.LastModified);
			a.tahLstChgUser = reader.TryReadstring( TemplateSubAssemblyMap.tahLstChgUser);
			a.tahRefCaseDefined = reader.TryReadbool( TemplateSubAssemblyMap.tahRefCaseDefined);
			a.tahRefUserDeleted = reader.TryReadbool( TemplateSubAssemblyMap.tahRefUserDeleted);
			a.tahTemplateAssemblyDescription = reader.TryReadstring( TemplateSubAssemblyMap.tahTemplateAssemblyDescription);
			a.tahFK_r_APIPumpGraphic_GUID = reader.TryReadGuid( TemplateSubAssemblyMap.tahFK_r_APIPumpGraphic_GUID);
			a.tahFK_r_APIPumpGraphic = reader.TryReadstring( TemplateSubAssemblyMap.tahFK_r_APIPumpGraphic);
			a.tahFK_r_ComponentCategory_GUID = reader.TryReadGuid( TemplateSubAssemblyMap.tahFK_r_ComponentCategory_GUID);
			a.tahFK_r_ComponentCategory = reader.TryReadstring( TemplateSubAssemblyMap.tahFK_r_ComponentCategory);
			a.tahHelpText = reader.TryReadstring( TemplateSubAssemblyMap.tahHelpText);
    
            return a;
        }

   		public override void RegisterParams()
		{
			TemplateSubAssembly a = (TemplateSubAssembly)dataObj;
			this.AddParam( TemplateSubAssemblyMap.ID, TemplateSubAssemblyMap.Param.ID, a.ID );
			this.AddParam( TemplateSubAssemblyMap.tahPrimaryKey, TemplateSubAssemblyMap.Param.tahPrimaryKey, a.tahPrimaryKey );
			this.AddParam( TemplateSubAssemblyMap.tahLanguageCd, TemplateSubAssemblyMap.Param.tahLanguageCd, a.tahLanguageCd );
			this.AddParam( TemplateSubAssemblyMap.LastModified, TemplateSubAssemblyMap.Param.LastModified, a.LastModified );
			this.AddParam( TemplateSubAssemblyMap.tahLstChgUser, TemplateSubAssemblyMap.Param.tahLstChgUser, a.tahLstChgUser );
			this.AddParam( TemplateSubAssemblyMap.tahRefCaseDefined, TemplateSubAssemblyMap.Param.tahRefCaseDefined, a.tahRefCaseDefined );
			this.AddParam( TemplateSubAssemblyMap.tahRefUserDeleted, TemplateSubAssemblyMap.Param.tahRefUserDeleted, a.tahRefUserDeleted );
			this.AddParam( TemplateSubAssemblyMap.tahTemplateAssemblyDescription, TemplateSubAssemblyMap.Param.tahTemplateAssemblyDescription, a.tahTemplateAssemblyDescription );
			this.AddParam( TemplateSubAssemblyMap.tahFK_r_APIPumpGraphic_GUID, TemplateSubAssemblyMap.Param.tahFK_r_APIPumpGraphic_GUID, a.tahFK_r_APIPumpGraphic_GUID );
			this.AddParam( TemplateSubAssemblyMap.tahFK_r_APIPumpGraphic, TemplateSubAssemblyMap.Param.tahFK_r_APIPumpGraphic, a.tahFK_r_APIPumpGraphic );
			this.AddParam( TemplateSubAssemblyMap.tahFK_r_ComponentCategory_GUID, TemplateSubAssemblyMap.Param.tahFK_r_ComponentCategory_GUID, a.tahFK_r_ComponentCategory_GUID );
			this.AddParam( TemplateSubAssemblyMap.tahFK_r_ComponentCategory, TemplateSubAssemblyMap.Param.tahFK_r_ComponentCategory, a.tahFK_r_ComponentCategory );
			this.AddParam( TemplateSubAssemblyMap.tahHelpText, TemplateSubAssemblyMap.Param.tahHelpText, a.tahHelpText );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the TemplateSubAssemblyDetail table in the PumpServicing Database.
    /// </summary>
    public partial class TemplateSubAssemblyDetailTransaction : BaseTransaction<TemplateSubAssemblyDetail>
    {
        public TemplateSubAssemblyDetailTransaction()
            : base(TemplateSubAssemblyDetailMap.TABLE_NAME, TemplateSubAssemblyDetailMap.ID)
        {
        }
        
        public TemplateSubAssemblyDetailTransaction( IBaseData _type )
			: base( _type, TemplateSubAssemblyDetailMap.TABLE_NAME, TemplateSubAssemblyDetailMap.ID )
		{
        }

        public TemplateSubAssemblyDetailTransaction( string _PrimaryKey )
			: base( TemplateSubAssemblyDetailMap.TABLE_NAME, TemplateSubAssemblyDetailMap.ID, _PrimaryKey )
		{
        }

        public TemplateSubAssemblyDetailTransaction( DateTime _lastModified )
            : base( TemplateSubAssemblyDetailMap.TABLE_NAME, TemplateSubAssemblyDetailMap.ID, _lastModified, TemplateSubAssemblyDetailMap.LastModified )
        {
        }				
    
        public override TemplateSubAssemblyDetail BuildFromReader(TransactionReader reader)
        {
            TemplateSubAssemblyDetail a = new TemplateSubAssemblyDetail( );
			a.tadPrimaryKey_GUID = reader.TryReadGuid( TemplateSubAssemblyDetailMap.ID);
			a.tadPrimaryKey = reader.TryReadstring( TemplateSubAssemblyDetailMap.tadPrimaryKey);
			a.tadLstChgDT = reader.TryReadDateTime( TemplateSubAssemblyDetailMap.LastModified);
			a.tadLstChgUser = reader.TryReadstring( TemplateSubAssemblyDetailMap.tadLstChgUser);
			a.tadRefCaseDefined = reader.TryReadbool( TemplateSubAssemblyDetailMap.tadRefCaseDefined);
			a.tadRefUserDeleted = reader.TryReadbool( TemplateSubAssemblyDetailMap.tadRefUserDeleted);
			a.tadFK_TemplateSubAssembly_GUID = reader.TryReadGuid( TemplateSubAssemblyDetailMap.tadFK_TemplateSubAssembly_GUID);
			a.tadFK_TemplateSubAssembly = reader.TryReadstring( TemplateSubAssemblyDetailMap.tadFK_TemplateSubAssembly);
			a.tadAssemblyOrder = reader.TryReaddecimal( TemplateSubAssemblyDetailMap.tadAssemblyOrder);
			a.tadFK_r_PartType_GUID = reader.TryReadGuid( TemplateSubAssemblyDetailMap.tadFK_r_PartType_GUID);
			a.tadFK_r_PartType = reader.TryReadstring( TemplateSubAssemblyDetailMap.tadFK_r_PartType);
			a.tadFK_r_CatalogItem_GUID = reader.TryReadGuid( TemplateSubAssemblyDetailMap.tadFK_r_CatalogItem_GUID);
			a.tadFK_r_CatalogItem = reader.TryReadstring( TemplateSubAssemblyDetailMap.tadFK_r_CatalogItem);
			a.tadFK_r_ComponentGrouping_GUID = reader.TryReadGuid( TemplateSubAssemblyDetailMap.tadFK_r_ComponentGrouping_GUID);
			a.tadFK_r_ComponentGrouping = reader.TryReadstring( TemplateSubAssemblyDetailMap.tadFK_r_ComponentGrouping);
			a.tadQuantity = reader.TryReaddecimal( TemplateSubAssemblyDetailMap.tadQuantity);
    
            return a;
        }

   		public override void RegisterParams()
		{
			TemplateSubAssemblyDetail a = (TemplateSubAssemblyDetail)dataObj;
			this.AddParam( TemplateSubAssemblyDetailMap.ID, TemplateSubAssemblyDetailMap.Param.ID, a.ID );
			this.AddParam( TemplateSubAssemblyDetailMap.tadPrimaryKey, TemplateSubAssemblyDetailMap.Param.tadPrimaryKey, a.tadPrimaryKey );
			this.AddParam( TemplateSubAssemblyDetailMap.LastModified, TemplateSubAssemblyDetailMap.Param.LastModified, a.LastModified );
			this.AddParam( TemplateSubAssemblyDetailMap.tadLstChgUser, TemplateSubAssemblyDetailMap.Param.tadLstChgUser, a.tadLstChgUser );
			this.AddParam( TemplateSubAssemblyDetailMap.tadRefCaseDefined, TemplateSubAssemblyDetailMap.Param.tadRefCaseDefined, a.tadRefCaseDefined );
			this.AddParam( TemplateSubAssemblyDetailMap.tadRefUserDeleted, TemplateSubAssemblyDetailMap.Param.tadRefUserDeleted, a.tadRefUserDeleted );
			this.AddParam( TemplateSubAssemblyDetailMap.tadFK_TemplateSubAssembly_GUID, TemplateSubAssemblyDetailMap.Param.tadFK_TemplateSubAssembly_GUID, a.tadFK_TemplateSubAssembly_GUID );
			this.AddParam( TemplateSubAssemblyDetailMap.tadFK_TemplateSubAssembly, TemplateSubAssemblyDetailMap.Param.tadFK_TemplateSubAssembly, a.tadFK_TemplateSubAssembly );
			this.AddParam( TemplateSubAssemblyDetailMap.tadAssemblyOrder, TemplateSubAssemblyDetailMap.Param.tadAssemblyOrder, a.tadAssemblyOrder );
			this.AddParam( TemplateSubAssemblyDetailMap.tadFK_r_PartType_GUID, TemplateSubAssemblyDetailMap.Param.tadFK_r_PartType_GUID, a.tadFK_r_PartType_GUID );
			this.AddParam( TemplateSubAssemblyDetailMap.tadFK_r_PartType, TemplateSubAssemblyDetailMap.Param.tadFK_r_PartType, a.tadFK_r_PartType );
			this.AddParam( TemplateSubAssemblyDetailMap.tadFK_r_CatalogItem_GUID, TemplateSubAssemblyDetailMap.Param.tadFK_r_CatalogItem_GUID, a.tadFK_r_CatalogItem_GUID );
			this.AddParam( TemplateSubAssemblyDetailMap.tadFK_r_CatalogItem, TemplateSubAssemblyDetailMap.Param.tadFK_r_CatalogItem, a.tadFK_r_CatalogItem );
			this.AddParam( TemplateSubAssemblyDetailMap.tadFK_r_ComponentGrouping_GUID, TemplateSubAssemblyDetailMap.Param.tadFK_r_ComponentGrouping_GUID, a.tadFK_r_ComponentGrouping_GUID );
			this.AddParam( TemplateSubAssemblyDetailMap.tadFK_r_ComponentGrouping, TemplateSubAssemblyDetailMap.Param.tadFK_r_ComponentGrouping, a.tadFK_r_ComponentGrouping );
			this.AddParam( TemplateSubAssemblyDetailMap.tadQuantity, TemplateSubAssemblyDetailMap.Param.tadQuantity, a.tadQuantity );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the UserMaster table in the PumpServicing Database.
    /// </summary>
    public partial class UserMasterTransaction : BaseTransaction<UserMaster>
    {
        public UserMasterTransaction()
            : base(UserMasterMap.TABLE_NAME, UserMasterMap.ID)
        {
        }
        
        public UserMasterTransaction( IBaseData _type )
			: base( _type, UserMasterMap.TABLE_NAME, UserMasterMap.ID )
		{
        }

        public UserMasterTransaction( string _PrimaryKey )
			: base( UserMasterMap.TABLE_NAME, UserMasterMap.ID, _PrimaryKey )
		{
        }

        public UserMasterTransaction( DateTime _lastModified )
            : base( UserMasterMap.TABLE_NAME, UserMasterMap.ID, _lastModified, UserMasterMap.LastModified )
        {
        }				
    
        public override UserMaster BuildFromReader(TransactionReader reader)
        {
            UserMaster a = new UserMaster( );
			a.usrPrimaryKey_GUID = reader.TryReadGuid( UserMasterMap.ID);
			a.usrPrimaryKey = reader.TryReadstring( UserMasterMap.usrPrimaryKey);
			a.usrLstChgDT = reader.TryReadDateTime( UserMasterMap.LastModified);
			a.usrLstChgUser = reader.TryReadstring( UserMasterMap.usrLstChgUser);
			a.usrUserName = reader.TryReadstring( UserMasterMap.usrUserName);
			a.usrPwd = reader.TryReadstring( UserMasterMap.usrPwd);
			a.usrPwdExpiry = reader.TryReadDateTime( UserMasterMap.usrPwdExpiry);
			a.usrFullName = reader.TryReadstring( UserMasterMap.usrFullName);
			a.usrSUserName = reader.TryReadstring( UserMasterMap.usrSUserName);
			a.usrLevel = reader.TryReadstring( UserMasterMap.usrLevel);
			a.usrCreateDT = reader.TryReadDateTime( UserMasterMap.usrCreateDT);
			a.usrPumpService = reader.TryReadbool( UserMasterMap.usrPumpService);
			a.usrInActive = reader.TryReadbool( UserMasterMap.usrInActive);
			a.usrColor = reader.TryReadlong( UserMasterMap.usrColor);
			a.usrFK_Assembly_GUID = reader.TryReadGuid( UserMasterMap.usrFK_Assembly_GUID);
			a.usrFK_Assembly = reader.TryReadstring( UserMasterMap.usrFK_Assembly);
    
            return a;
        }

   		public override void RegisterParams()
		{
			UserMaster a = (UserMaster)dataObj;
			this.AddParam( UserMasterMap.ID, UserMasterMap.Param.ID, a.ID );
			this.AddParam( UserMasterMap.usrPrimaryKey, UserMasterMap.Param.usrPrimaryKey, a.usrPrimaryKey );
			this.AddParam( UserMasterMap.LastModified, UserMasterMap.Param.LastModified, a.LastModified );
			this.AddParam( UserMasterMap.usrLstChgUser, UserMasterMap.Param.usrLstChgUser, a.usrLstChgUser );
			this.AddParam( UserMasterMap.usrUserName, UserMasterMap.Param.usrUserName, a.usrUserName );
			this.AddParam( UserMasterMap.usrPwd, UserMasterMap.Param.usrPwd, a.usrPwd );
			this.AddParam( UserMasterMap.usrPwdExpiry, UserMasterMap.Param.usrPwdExpiry, a.usrPwdExpiry );
			this.AddParam( UserMasterMap.usrFullName, UserMasterMap.Param.usrFullName, a.usrFullName );
			this.AddParam( UserMasterMap.usrSUserName, UserMasterMap.Param.usrSUserName, a.usrSUserName );
			this.AddParam( UserMasterMap.usrLevel, UserMasterMap.Param.usrLevel, a.usrLevel );
			this.AddParam( UserMasterMap.usrCreateDT, UserMasterMap.Param.usrCreateDT, a.usrCreateDT );
			this.AddParam( UserMasterMap.usrPumpService, UserMasterMap.Param.usrPumpService, a.usrPumpService );
			this.AddParam( UserMasterMap.usrInActive, UserMasterMap.Param.usrInActive, a.usrInActive );
			this.AddParam( UserMasterMap.usrColor, UserMasterMap.Param.usrColor, a.usrColor );
			this.AddParam( UserMasterMap.usrFK_Assembly_GUID, UserMasterMap.Param.usrFK_Assembly_GUID, a.usrFK_Assembly_GUID );
			this.AddParam( UserMasterMap.usrFK_Assembly, UserMasterMap.Param.usrFK_Assembly, a.usrFK_Assembly );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the Well table in the PumpServicing Database.
    /// </summary>
    public partial class WellTransaction : BaseTransaction<Well>
    {
        public WellTransaction()
            : base(WellMap.TABLE_NAME, WellMap.ID)
        {
        }
        
        public WellTransaction( IBaseData _type )
			: base( _type, WellMap.TABLE_NAME, WellMap.ID )
		{
        }

        public WellTransaction( string _PrimaryKey )
			: base( WellMap.TABLE_NAME, WellMap.ID, _PrimaryKey )
		{
        }

        public WellTransaction( DateTime _lastModified )
            : base( WellMap.TABLE_NAME, WellMap.ID, _lastModified, WellMap.LastModified )
        {
        }				
    
        public override Well BuildFromReader(TransactionReader reader)
        {
            Well a = new Well( );
			a.welPrimaryKey_GUID = reader.TryReadGuid( WellMap.ID);
			a.welPrimaryKey = reader.TryReadstring( WellMap.welPrimaryKey);
			a.welLstChgDT = reader.TryReadDateTime( WellMap.LastModified);
			a.welLstChgUser = reader.TryReadstring( WellMap.welLstChgUser);
			a.welRefCaseDefined = reader.TryReadbool( WellMap.welRefCaseDefined);
			a.welFK_Assembly_GUID = reader.TryReadGuid( WellMap.welFK_Assembly_GUID);
			a.welFK_Assembly = reader.TryReadstring( WellMap.welFK_Assembly);
			a.welUWBID = reader.TryReadstring( WellMap.welUWBID);
			a.welFK_r_WellType = reader.TryReadstring( WellMap.welFK_r_WellType);
			a.welFK_r_WellProfile = reader.TryReadstring( WellMap.welFK_r_WellProfile);
			a.welFK_Owner = reader.TryReadstring( WellMap.welFK_Owner);
			a.welFK_BusinessOrganization_Producer_GUID = reader.TryReadGuid( WellMap.welFK_BusinessOrganization_Producer_GUID);
			a.welFK_BusinessOrganization_Producer = reader.TryReadstring( WellMap.welFK_BusinessOrganization_Producer);
			a.welFK_Lease_GUID = reader.TryReadGuid( WellMap.welFK_Lease_GUID);
			a.welFK_Lease = reader.TryReadstring( WellMap.welFK_Lease);
			a.welWellName = reader.TryReadstring( WellMap.welWellName);
			a.welLongWellName = reader.TryReadstring( WellMap.welLongWellName);
			a.welActive = reader.TryReadbool( WellMap.welActive);
			a.welSurfaceLatitude = reader.TryReaddecimal( WellMap.welSurfaceLatitude);
			a.welSurfaceLongitude = reader.TryReaddecimal( WellMap.welSurfaceLongitude);
			a.welFK_r_Country_GUID = reader.TryReadGuid( WellMap.welFK_r_Country_GUID);
			a.welFK_r_Country = reader.TryReadstring( WellMap.welFK_r_Country);
			a.welFK_r_StateProvince_GUID = reader.TryReadGuid( WellMap.welFK_r_StateProvince_GUID);
			a.welFK_r_StateProvince = reader.TryReadstring( WellMap.welFK_r_StateProvince);
			a.welSpudDate = reader.TryReadDateTime( WellMap.welSpudDate);
			a.welCompletionDate = reader.TryReadDateTime( WellMap.welCompletionDate);
			a.welAbandonmentDate = reader.TryReadDateTime( WellMap.welAbandonmentDate);
			a.welFK_r_Field_GUID = reader.TryReadGuid( WellMap.welFK_r_Field_GUID);
			a.welFK_r_Field = reader.TryReadstring( WellMap.welFK_r_Field);
			a.welLegalDesc = reader.TryReadstring( WellMap.welLegalDesc);
			a.welCCLTownshipDirection = reader.TryReadstring( WellMap.welCCLTownshipDirection);
			a.welCCLTownshipNumber = reader.TryReadstring( WellMap.welCCLTownshipNumber);
			a.welCCLRangeDirection = reader.TryReadstring( WellMap.welCCLRangeDirection);
			a.welCCLRangeNumber = reader.TryReadstring( WellMap.welCCLRangeNumber);
			a.welCCLSectionIndicator = reader.TryReadstring( WellMap.welCCLSectionIndicator);
			a.welCCLSectionNumber = reader.TryReadstring( WellMap.welCCLSectionNumber);
			a.welCCLUnit = reader.TryReadstring( WellMap.welCCLUnit);
			a.welCCLMeridianCode = reader.TryReadstring( WellMap.welCCLMeridianCode);
			a.welCCLMeridianName = reader.TryReadstring( WellMap.welCCLMeridianName);
			a.welFK_r_Foreman_GUID = reader.TryReadGuid( WellMap.welFK_r_Foreman_GUID);
			a.welFK_r_Foreman = reader.TryReadstring( WellMap.welFK_r_Foreman);
			a.welFK_r_Engineer_GUID = reader.TryReadGuid( WellMap.welFK_r_Engineer_GUID);
			a.welFK_r_Engineer = reader.TryReadstring( WellMap.welFK_r_Engineer);
			a.welPOCinService = reader.TryReadbool( WellMap.welPOCinService);
			a.welFK_BusinessOrganization_PumpService_GUID = reader.TryReadGuid( WellMap.welFK_BusinessOrganization_PumpService_GUID);
			a.welFK_BusinessOrganization_PumpService = reader.TryReadstring( WellMap.welFK_BusinessOrganization_PumpService);
			a.welPTTaxableStatus = reader.TryReadbool( WellMap.welPTTaxableStatus);
			a.welPTTaxRate = reader.TryReaddecimal( WellMap.welPTTaxRate);
			a.welUserDef10 = reader.TryReadstring( WellMap.welUserDef10);
			a.welRemarks = reader.TryReadstring( WellMap.welRemarks);
    
            return a;
        }

   		public override void RegisterParams()
		{
			Well a = (Well)dataObj;
			this.AddParam( WellMap.ID, WellMap.Param.ID, a.ID );
			this.AddParam( WellMap.welPrimaryKey, WellMap.Param.welPrimaryKey, a.welPrimaryKey );
			this.AddParam( WellMap.LastModified, WellMap.Param.LastModified, a.LastModified );
			this.AddParam( WellMap.welLstChgUser, WellMap.Param.welLstChgUser, a.welLstChgUser );
			this.AddParam( WellMap.welRefCaseDefined, WellMap.Param.welRefCaseDefined, a.welRefCaseDefined );
			this.AddParam( WellMap.welFK_Assembly_GUID, WellMap.Param.welFK_Assembly_GUID, a.welFK_Assembly_GUID );
			this.AddParam( WellMap.welFK_Assembly, WellMap.Param.welFK_Assembly, a.welFK_Assembly );
			this.AddParam( WellMap.welUWBID, WellMap.Param.welUWBID, a.welUWBID );
			this.AddParam( WellMap.welFK_r_WellType, WellMap.Param.welFK_r_WellType, a.welFK_r_WellType );
			this.AddParam( WellMap.welFK_r_WellProfile, WellMap.Param.welFK_r_WellProfile, a.welFK_r_WellProfile );
			this.AddParam( WellMap.welFK_Owner, WellMap.Param.welFK_Owner, a.welFK_Owner );
			this.AddParam( WellMap.welFK_BusinessOrganization_Producer_GUID, WellMap.Param.welFK_BusinessOrganization_Producer_GUID, a.welFK_BusinessOrganization_Producer_GUID );
			this.AddParam( WellMap.welFK_BusinessOrganization_Producer, WellMap.Param.welFK_BusinessOrganization_Producer, a.welFK_BusinessOrganization_Producer );
			this.AddParam( WellMap.welFK_Lease_GUID, WellMap.Param.welFK_Lease_GUID, a.welFK_Lease_GUID );
			this.AddParam( WellMap.welFK_Lease, WellMap.Param.welFK_Lease, a.welFK_Lease );
			this.AddParam( WellMap.welWellName, WellMap.Param.welWellName, a.welWellName );
			this.AddParam( WellMap.welLongWellName, WellMap.Param.welLongWellName, a.welLongWellName );
			this.AddParam( WellMap.welActive, WellMap.Param.welActive, a.welActive );
			this.AddParam( WellMap.welSurfaceLatitude, WellMap.Param.welSurfaceLatitude, a.welSurfaceLatitude );
			this.AddParam( WellMap.welSurfaceLongitude, WellMap.Param.welSurfaceLongitude, a.welSurfaceLongitude );
			this.AddParam( WellMap.welFK_r_Country_GUID, WellMap.Param.welFK_r_Country_GUID, a.welFK_r_Country_GUID );
			this.AddParam( WellMap.welFK_r_Country, WellMap.Param.welFK_r_Country, a.welFK_r_Country );
			this.AddParam( WellMap.welFK_r_StateProvince_GUID, WellMap.Param.welFK_r_StateProvince_GUID, a.welFK_r_StateProvince_GUID );
			this.AddParam( WellMap.welFK_r_StateProvince, WellMap.Param.welFK_r_StateProvince, a.welFK_r_StateProvince );
			this.AddParam( WellMap.welSpudDate, WellMap.Param.welSpudDate, a.welSpudDate );
			this.AddParam( WellMap.welCompletionDate, WellMap.Param.welCompletionDate, a.welCompletionDate );
			this.AddParam( WellMap.welAbandonmentDate, WellMap.Param.welAbandonmentDate, a.welAbandonmentDate );
			this.AddParam( WellMap.welFK_r_Field_GUID, WellMap.Param.welFK_r_Field_GUID, a.welFK_r_Field_GUID );
			this.AddParam( WellMap.welFK_r_Field, WellMap.Param.welFK_r_Field, a.welFK_r_Field );
			this.AddParam( WellMap.welLegalDesc, WellMap.Param.welLegalDesc, a.welLegalDesc );
			this.AddParam( WellMap.welCCLTownshipDirection, WellMap.Param.welCCLTownshipDirection, a.welCCLTownshipDirection );
			this.AddParam( WellMap.welCCLTownshipNumber, WellMap.Param.welCCLTownshipNumber, a.welCCLTownshipNumber );
			this.AddParam( WellMap.welCCLRangeDirection, WellMap.Param.welCCLRangeDirection, a.welCCLRangeDirection );
			this.AddParam( WellMap.welCCLRangeNumber, WellMap.Param.welCCLRangeNumber, a.welCCLRangeNumber );
			this.AddParam( WellMap.welCCLSectionIndicator, WellMap.Param.welCCLSectionIndicator, a.welCCLSectionIndicator );
			this.AddParam( WellMap.welCCLSectionNumber, WellMap.Param.welCCLSectionNumber, a.welCCLSectionNumber );
			this.AddParam( WellMap.welCCLUnit, WellMap.Param.welCCLUnit, a.welCCLUnit );
			this.AddParam( WellMap.welCCLMeridianCode, WellMap.Param.welCCLMeridianCode, a.welCCLMeridianCode );
			this.AddParam( WellMap.welCCLMeridianName, WellMap.Param.welCCLMeridianName, a.welCCLMeridianName );
			this.AddParam( WellMap.welFK_r_Foreman_GUID, WellMap.Param.welFK_r_Foreman_GUID, a.welFK_r_Foreman_GUID );
			this.AddParam( WellMap.welFK_r_Foreman, WellMap.Param.welFK_r_Foreman, a.welFK_r_Foreman );
			this.AddParam( WellMap.welFK_r_Engineer_GUID, WellMap.Param.welFK_r_Engineer_GUID, a.welFK_r_Engineer_GUID );
			this.AddParam( WellMap.welFK_r_Engineer, WellMap.Param.welFK_r_Engineer, a.welFK_r_Engineer );
			this.AddParam( WellMap.welPOCinService, WellMap.Param.welPOCinService, a.welPOCinService );
			this.AddParam( WellMap.welFK_BusinessOrganization_PumpService_GUID, WellMap.Param.welFK_BusinessOrganization_PumpService_GUID, a.welFK_BusinessOrganization_PumpService_GUID );
			this.AddParam( WellMap.welFK_BusinessOrganization_PumpService, WellMap.Param.welFK_BusinessOrganization_PumpService, a.welFK_BusinessOrganization_PumpService );
			this.AddParam( WellMap.welPTTaxableStatus, WellMap.Param.welPTTaxableStatus, a.welPTTaxableStatus );
			this.AddParam( WellMap.welPTTaxRate, WellMap.Param.welPTTaxRate, a.welPTTaxRate );
			this.AddParam( WellMap.welUserDef10, WellMap.Param.welUserDef10, a.welUserDef10 );
			this.AddParam( WellMap.welRemarks, WellMap.Param.welRemarks, a.welRemarks );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the WellCompletionReservoirs table in the PumpServicing Database.
    /// </summary>
    public partial class WellCompletionReservoirsTransaction : BaseTransaction<WellCompletionReservoirs>
    {
        public WellCompletionReservoirsTransaction()
            : base(WellCompletionReservoirsMap.TABLE_NAME, WellCompletionReservoirsMap.ID)
        {
        }
        
        public WellCompletionReservoirsTransaction( IBaseData _type )
			: base( _type, WellCompletionReservoirsMap.TABLE_NAME, WellCompletionReservoirsMap.ID )
		{
        }

        public WellCompletionReservoirsTransaction( string _PrimaryKey )
			: base( WellCompletionReservoirsMap.TABLE_NAME, WellCompletionReservoirsMap.ID, _PrimaryKey )
		{
        }

        public WellCompletionReservoirsTransaction( DateTime _lastModified )
            : base( WellCompletionReservoirsMap.TABLE_NAME, WellCompletionReservoirsMap.ID, _lastModified, WellCompletionReservoirsMap.LastModified )
        {
        }				
    
        public override WellCompletionReservoirs BuildFromReader(TransactionReader reader)
        {
            WellCompletionReservoirs a = new WellCompletionReservoirs( );
			a.wcrPrimaryKey_GUID = reader.TryReadGuid( WellCompletionReservoirsMap.ID);
			a.wcrLstChgDT = reader.TryReadDateTime( WellCompletionReservoirsMap.LastModified);
			a.wcrLstChgUser = reader.TryReadstring( WellCompletionReservoirsMap.wcrLstChgUser);
			a.wcrFK_WellCompletion_GUID = reader.TryReadGuid( WellCompletionReservoirsMap.wcrFK_WellCompletion_GUID);
			a.wcrFK_r_Reservoir_GUID = reader.TryReadGuid( WellCompletionReservoirsMap.wcrFK_r_Reservoir_GUID);
    
            return a;
        }

   		public override void RegisterParams()
		{
			WellCompletionReservoirs a = (WellCompletionReservoirs)dataObj;
			this.AddParam( WellCompletionReservoirsMap.ID, WellCompletionReservoirsMap.Param.ID, a.ID );
			this.AddParam( WellCompletionReservoirsMap.LastModified, WellCompletionReservoirsMap.Param.LastModified, a.LastModified );
			this.AddParam( WellCompletionReservoirsMap.wcrLstChgUser, WellCompletionReservoirsMap.Param.wcrLstChgUser, a.wcrLstChgUser );
			this.AddParam( WellCompletionReservoirsMap.wcrFK_WellCompletion_GUID, WellCompletionReservoirsMap.Param.wcrFK_WellCompletion_GUID, a.wcrFK_WellCompletion_GUID );
			this.AddParam( WellCompletionReservoirsMap.wcrFK_r_Reservoir_GUID, WellCompletionReservoirsMap.Param.wcrFK_r_Reservoir_GUID, a.wcrFK_r_Reservoir_GUID );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the WellCompletionXRef table in the PumpServicing Database.
    /// </summary>
    public partial class WellCompletionXRefTransaction : BaseTransaction<WellCompletionXRef>
    {
        public WellCompletionXRefTransaction()
            : base(WellCompletionXRefMap.TABLE_NAME, WellCompletionXRefMap.ID)
        {
        }
        
        public WellCompletionXRefTransaction( IBaseData _type )
			: base( _type, WellCompletionXRefMap.TABLE_NAME, WellCompletionXRefMap.ID )
		{
        }

        public WellCompletionXRefTransaction( string _PrimaryKey )
			: base( WellCompletionXRefMap.TABLE_NAME, WellCompletionXRefMap.ID, _PrimaryKey )
		{
        }

        public WellCompletionXRefTransaction( DateTime _lastModified )
            : base( WellCompletionXRefMap.TABLE_NAME, WellCompletionXRefMap.ID, _lastModified, WellCompletionXRefMap.LastModified )
        {
        }				
    
        public override WellCompletionXRef BuildFromReader(TransactionReader reader)
        {
            WellCompletionXRef a = new WellCompletionXRef( );
			a.wxrPrimaryKey_GUID = reader.TryReadGuid( WellCompletionXRefMap.ID);
			a.wxrPrimaryKey = reader.TryReadstring( WellCompletionXRefMap.wxrPrimaryKey);
			a.wxrLstChgDT = reader.TryReadDateTime( WellCompletionXRefMap.LastModified);
			a.wxrLstChgUser = reader.TryReadstring( WellCompletionXRefMap.wxrLstChgUser);
			a.wxrRefCaseDefined = reader.TryReadbool( WellCompletionXRefMap.wxrRefCaseDefined);
			a.wxrFK_Well_GUID = reader.TryReadGuid( WellCompletionXRefMap.wxrFK_Well_GUID);
			a.wxrFK_Well = reader.TryReadstring( WellCompletionXRefMap.wxrFK_Well);
			a.wxrAPI12 = reader.TryReadstring( WellCompletionXRefMap.wxrAPI12);
			a.wxrAPI14 = reader.TryReadstring( WellCompletionXRefMap.wxrAPI14);
			a.wxrShortWellCompName = reader.TryReadstring( WellCompletionXRefMap.wxrShortWellCompName);
			a.wxrLongWellCompName = reader.TryReadstring( WellCompletionXRefMap.wxrLongWellCompName);
			a.wxrCurrentTestDateTime = reader.TryReadDateTime( WellCompletionXRefMap.wxrCurrentTestDateTime);
			a.wxrCurrentTestOil = reader.TryReaddecimal( WellCompletionXRefMap.wxrCurrentTestOil);
			a.wxrCurrentTestGas = reader.TryReaddecimal( WellCompletionXRefMap.wxrCurrentTestGas);
			a.wxrCurrentTestWater = reader.TryReaddecimal( WellCompletionXRefMap.wxrCurrentTestWater);
			a.wxrCompletionDate = reader.TryReadDateTime( WellCompletionXRefMap.wxrCompletionDate);
			a.wxrAbandonmentDate = reader.TryReadDateTime( WellCompletionXRefMap.wxrAbandonmentDate);
    
            return a;
        }

   		public override void RegisterParams()
		{
			WellCompletionXRef a = (WellCompletionXRef)dataObj;
			this.AddParam( WellCompletionXRefMap.ID, WellCompletionXRefMap.Param.ID, a.ID );
			this.AddParam( WellCompletionXRefMap.wxrPrimaryKey, WellCompletionXRefMap.Param.wxrPrimaryKey, a.wxrPrimaryKey );
			this.AddParam( WellCompletionXRefMap.LastModified, WellCompletionXRefMap.Param.LastModified, a.LastModified );
			this.AddParam( WellCompletionXRefMap.wxrLstChgUser, WellCompletionXRefMap.Param.wxrLstChgUser, a.wxrLstChgUser );
			this.AddParam( WellCompletionXRefMap.wxrRefCaseDefined, WellCompletionXRefMap.Param.wxrRefCaseDefined, a.wxrRefCaseDefined );
			this.AddParam( WellCompletionXRefMap.wxrFK_Well_GUID, WellCompletionXRefMap.Param.wxrFK_Well_GUID, a.wxrFK_Well_GUID );
			this.AddParam( WellCompletionXRefMap.wxrFK_Well, WellCompletionXRefMap.Param.wxrFK_Well, a.wxrFK_Well );
			this.AddParam( WellCompletionXRefMap.wxrAPI12, WellCompletionXRefMap.Param.wxrAPI12, a.wxrAPI12 );
			this.AddParam( WellCompletionXRefMap.wxrAPI14, WellCompletionXRefMap.Param.wxrAPI14, a.wxrAPI14 );
			this.AddParam( WellCompletionXRefMap.wxrShortWellCompName, WellCompletionXRefMap.Param.wxrShortWellCompName, a.wxrShortWellCompName );
			this.AddParam( WellCompletionXRefMap.wxrLongWellCompName, WellCompletionXRefMap.Param.wxrLongWellCompName, a.wxrLongWellCompName );
			this.AddParam( WellCompletionXRefMap.wxrCurrentTestDateTime, WellCompletionXRefMap.Param.wxrCurrentTestDateTime, a.wxrCurrentTestDateTime );
			this.AddParam( WellCompletionXRefMap.wxrCurrentTestOil, WellCompletionXRefMap.Param.wxrCurrentTestOil, a.wxrCurrentTestOil );
			this.AddParam( WellCompletionXRefMap.wxrCurrentTestGas, WellCompletionXRefMap.Param.wxrCurrentTestGas, a.wxrCurrentTestGas );
			this.AddParam( WellCompletionXRefMap.wxrCurrentTestWater, WellCompletionXRefMap.Param.wxrCurrentTestWater, a.wxrCurrentTestWater );
			this.AddParam( WellCompletionXRefMap.wxrCompletionDate, WellCompletionXRefMap.Param.wxrCompletionDate, a.wxrCompletionDate );
			this.AddParam( WellCompletionXRefMap.wxrAbandonmentDate, WellCompletionXRefMap.Param.wxrAbandonmentDate, a.wxrAbandonmentDate );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the Workorder table in the PumpServicing Database.
    /// </summary>
    public partial class WorkorderTransaction : BaseTransaction<Workorder>
    {
        public WorkorderTransaction()
            : base(WorkorderMap.TABLE_NAME, WorkorderMap.ID)
        {
        }
        
        public WorkorderTransaction( IBaseData _type )
			: base( _type, WorkorderMap.TABLE_NAME, WorkorderMap.ID )
		{
        }

        public WorkorderTransaction( string _PrimaryKey )
			: base( WorkorderMap.TABLE_NAME, WorkorderMap.ID, _PrimaryKey )
		{
        }

        public WorkorderTransaction( DateTime _lastModified )
            : base( WorkorderMap.TABLE_NAME, WorkorderMap.ID, _lastModified, WorkorderMap.LastModified )
        {
        }				
    
        public override Workorder BuildFromReader(TransactionReader reader)
        {
            Workorder a = new Workorder( );
			a.pswPrimaryKey_GUID = reader.TryReadGuid( WorkorderMap.ID);
			a.pswPrimaryKey = reader.TryReadstring( WorkorderMap.pswPrimaryKey);
			a.pswLstChgDT = reader.TryReadDateTime( WorkorderMap.LastModified);
			a.pswLstChgUser = reader.TryReadstring( WorkorderMap.pswLstChgUser);
			a.pswFK_Job_GUID = reader.TryReadGuid( WorkorderMap.pswFK_Job_GUID);
			a.pswFK_Job = reader.TryReadstring( WorkorderMap.pswFK_Job);
			a.pswFK_r_WorkorderType_GUID = reader.TryReadGuid( WorkorderMap.pswFK_r_WorkorderType_GUID);
			a.pswFK_r_WorkorderType = reader.TryReadstring( WorkorderMap.pswFK_r_WorkorderType);
			a.pswWorkorderID = reader.TryReadstring( WorkorderMap.pswWorkorderID);
			a.pswFK_Component_GUID = reader.TryReadGuid( WorkorderMap.pswFK_Component_GUID);
			a.pswFK_Component = reader.TryReadstring( WorkorderMap.pswFK_Component);
			a.pswWorkorderGrouping = reader.TryReadstring( WorkorderMap.pswWorkorderGrouping);
			a.pswScheduledDateTime = reader.TryReadDateTime( WorkorderMap.pswScheduledDateTime);
			a.pswFK_r_WorkorderStatus_GUID = reader.TryReadGuid( WorkorderMap.pswFK_r_WorkorderStatus_GUID);
			a.pswFK_r_WorkorderStatus = reader.TryReadstring( WorkorderMap.pswFK_r_WorkorderStatus);
			a.pswFK_Invoice_GUID = reader.TryReadGuid( WorkorderMap.pswFK_Invoice_GUID);
			a.pswFK_Invoice = reader.TryReadstring( WorkorderMap.pswFK_Invoice);
			a.pswFK_r_WorkorderTypeTaskType = reader.TryReadstring( WorkorderMap.pswFK_r_WorkorderTypeTaskType);
			a.pswFK_r_WorkorderTypeTaskType_GUID = reader.TryReadGuid( WorkorderMap.pswFK_r_WorkorderTypeTaskType_GUID);
    
            return a;
        }

   		public override void RegisterParams()
		{
			Workorder a = (Workorder)dataObj;
			this.AddParam( WorkorderMap.ID, WorkorderMap.Param.ID, a.ID );
			this.AddParam( WorkorderMap.pswPrimaryKey, WorkorderMap.Param.pswPrimaryKey, a.pswPrimaryKey );
			this.AddParam( WorkorderMap.LastModified, WorkorderMap.Param.LastModified, a.LastModified );
			this.AddParam( WorkorderMap.pswLstChgUser, WorkorderMap.Param.pswLstChgUser, a.pswLstChgUser );
			this.AddParam( WorkorderMap.pswFK_Job_GUID, WorkorderMap.Param.pswFK_Job_GUID, a.pswFK_Job_GUID );
			this.AddParam( WorkorderMap.pswFK_Job, WorkorderMap.Param.pswFK_Job, a.pswFK_Job );
			this.AddParam( WorkorderMap.pswFK_r_WorkorderType_GUID, WorkorderMap.Param.pswFK_r_WorkorderType_GUID, a.pswFK_r_WorkorderType_GUID );
			this.AddParam( WorkorderMap.pswFK_r_WorkorderType, WorkorderMap.Param.pswFK_r_WorkorderType, a.pswFK_r_WorkorderType );
			this.AddParam( WorkorderMap.pswWorkorderID, WorkorderMap.Param.pswWorkorderID, a.pswWorkorderID );
			this.AddParam( WorkorderMap.pswFK_Component_GUID, WorkorderMap.Param.pswFK_Component_GUID, a.pswFK_Component_GUID );
			this.AddParam( WorkorderMap.pswFK_Component, WorkorderMap.Param.pswFK_Component, a.pswFK_Component );
			this.AddParam( WorkorderMap.pswWorkorderGrouping, WorkorderMap.Param.pswWorkorderGrouping, a.pswWorkorderGrouping );
			this.AddParam( WorkorderMap.pswScheduledDateTime, WorkorderMap.Param.pswScheduledDateTime, a.pswScheduledDateTime );
			this.AddParam( WorkorderMap.pswFK_r_WorkorderStatus_GUID, WorkorderMap.Param.pswFK_r_WorkorderStatus_GUID, a.pswFK_r_WorkorderStatus_GUID );
			this.AddParam( WorkorderMap.pswFK_r_WorkorderStatus, WorkorderMap.Param.pswFK_r_WorkorderStatus, a.pswFK_r_WorkorderStatus );
			this.AddParam( WorkorderMap.pswFK_Invoice_GUID, WorkorderMap.Param.pswFK_Invoice_GUID, a.pswFK_Invoice_GUID );
			this.AddParam( WorkorderMap.pswFK_Invoice, WorkorderMap.Param.pswFK_Invoice, a.pswFK_Invoice );
			this.AddParam( WorkorderMap.pswFK_r_WorkorderTypeTaskType, WorkorderMap.Param.pswFK_r_WorkorderTypeTaskType, a.pswFK_r_WorkorderTypeTaskType );
			this.AddParam( WorkorderMap.pswFK_r_WorkorderTypeTaskType_GUID, WorkorderMap.Param.pswFK_r_WorkorderTypeTaskType_GUID, a.pswFK_r_WorkorderTypeTaskType_GUID );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the WorkorderStatusHistory table in the PumpServicing Database.
    /// </summary>
    public partial class WorkorderStatusHistoryTransaction : BaseTransaction<WorkorderStatusHistory>
    {
        public WorkorderStatusHistoryTransaction()
            : base(WorkorderStatusHistoryMap.TABLE_NAME, WorkorderStatusHistoryMap.ID)
        {
        }
        
        public WorkorderStatusHistoryTransaction( IBaseData _type )
			: base( _type, WorkorderStatusHistoryMap.TABLE_NAME, WorkorderStatusHistoryMap.ID )
		{
        }

        public WorkorderStatusHistoryTransaction( string _PrimaryKey )
			: base( WorkorderStatusHistoryMap.TABLE_NAME, WorkorderStatusHistoryMap.ID, _PrimaryKey )
		{
        }

        public WorkorderStatusHistoryTransaction( DateTime _lastModified )
            : base( WorkorderStatusHistoryMap.TABLE_NAME, WorkorderStatusHistoryMap.ID, _lastModified, WorkorderStatusHistoryMap.LastModified )
        {
        }				
    
        public override WorkorderStatusHistory BuildFromReader(TransactionReader reader)
        {
            WorkorderStatusHistory a = new WorkorderStatusHistory( );
			a.xhdPrimaryKey_GUID = reader.TryReadGuid( WorkorderStatusHistoryMap.ID);
			a.xhdPrimaryKey = reader.TryReadstring( WorkorderStatusHistoryMap.xhdPrimaryKey);
			a.xhdLstChgDT = reader.TryReadDateTime( WorkorderStatusHistoryMap.LastModified);
			a.xhdLstChgUser = reader.TryReadstring( WorkorderStatusHistoryMap.xhdLstChgUser);
			a.xhdFK_Workorder_GUID = reader.TryReadGuid( WorkorderStatusHistoryMap.xhdFK_Workorder_GUID);
			a.xhdFK_Workorder = reader.TryReadstring( WorkorderStatusHistoryMap.xhdFK_Workorder);
			a.xhdFK_r_WorkorderStatus_GUID = reader.TryReadGuid( WorkorderStatusHistoryMap.xhdFK_r_WorkorderStatus_GUID);
			a.xhdFK_r_WorkorderStatus = reader.TryReadstring( WorkorderStatusHistoryMap.xhdFK_r_WorkorderStatus);
			a.xhdStatusChangeDT = reader.TryReadDateTime( WorkorderStatusHistoryMap.xhdStatusChangeDT);
			a.xhdFK_r_WorkorderTypeTaskType = reader.TryReadstring( WorkorderStatusHistoryMap.xhdFK_r_WorkorderTypeTaskType);
			a.xhdFK_r_WorkorderTypeTaskType_GUID = reader.TryReadGuid( WorkorderStatusHistoryMap.xhdFK_r_WorkorderTypeTaskType_GUID);
    
            return a;
        }

   		public override void RegisterParams()
		{
			WorkorderStatusHistory a = (WorkorderStatusHistory)dataObj;
			this.AddParam( WorkorderStatusHistoryMap.ID, WorkorderStatusHistoryMap.Param.ID, a.ID );
			this.AddParam( WorkorderStatusHistoryMap.xhdPrimaryKey, WorkorderStatusHistoryMap.Param.xhdPrimaryKey, a.xhdPrimaryKey );
			this.AddParam( WorkorderStatusHistoryMap.LastModified, WorkorderStatusHistoryMap.Param.LastModified, a.LastModified );
			this.AddParam( WorkorderStatusHistoryMap.xhdLstChgUser, WorkorderStatusHistoryMap.Param.xhdLstChgUser, a.xhdLstChgUser );
			this.AddParam( WorkorderStatusHistoryMap.xhdFK_Workorder_GUID, WorkorderStatusHistoryMap.Param.xhdFK_Workorder_GUID, a.xhdFK_Workorder_GUID );
			this.AddParam( WorkorderStatusHistoryMap.xhdFK_Workorder, WorkorderStatusHistoryMap.Param.xhdFK_Workorder, a.xhdFK_Workorder );
			this.AddParam( WorkorderStatusHistoryMap.xhdFK_r_WorkorderStatus_GUID, WorkorderStatusHistoryMap.Param.xhdFK_r_WorkorderStatus_GUID, a.xhdFK_r_WorkorderStatus_GUID );
			this.AddParam( WorkorderStatusHistoryMap.xhdFK_r_WorkorderStatus, WorkorderStatusHistoryMap.Param.xhdFK_r_WorkorderStatus, a.xhdFK_r_WorkorderStatus );
			this.AddParam( WorkorderStatusHistoryMap.xhdStatusChangeDT, WorkorderStatusHistoryMap.Param.xhdStatusChangeDT, a.xhdStatusChangeDT );
			this.AddParam( WorkorderStatusHistoryMap.xhdFK_r_WorkorderTypeTaskType, WorkorderStatusHistoryMap.Param.xhdFK_r_WorkorderTypeTaskType, a.xhdFK_r_WorkorderTypeTaskType );
			this.AddParam( WorkorderStatusHistoryMap.xhdFK_r_WorkorderTypeTaskType_GUID, WorkorderStatusHistoryMap.Param.xhdFK_r_WorkorderTypeTaskType_GUID, a.xhdFK_r_WorkorderTypeTaskType_GUID );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the WorkorderSubAssemblies table in the PumpServicing Database.
    /// </summary>
    public partial class WorkorderSubAssembliesTransaction : BaseTransaction<WorkorderSubAssemblies>
    {
        public WorkorderSubAssembliesTransaction()
            : base(WorkorderSubAssembliesMap.TABLE_NAME, WorkorderSubAssembliesMap.ID)
        {
        }
        
        public WorkorderSubAssembliesTransaction( IBaseData _type )
			: base( _type, WorkorderSubAssembliesMap.TABLE_NAME, WorkorderSubAssembliesMap.ID )
		{
        }

        public WorkorderSubAssembliesTransaction( string _PrimaryKey )
			: base( WorkorderSubAssembliesMap.TABLE_NAME, WorkorderSubAssembliesMap.ID, _PrimaryKey )
		{
        }

        public WorkorderSubAssembliesTransaction( DateTime _lastModified )
            : base( WorkorderSubAssembliesMap.TABLE_NAME, WorkorderSubAssembliesMap.ID, _lastModified, WorkorderSubAssembliesMap.LastModified )
        {
        }				
    
        public override WorkorderSubAssemblies BuildFromReader(TransactionReader reader)
        {
            WorkorderSubAssemblies a = new WorkorderSubAssemblies( );
			a.psiPrimaryKey_GUID = reader.TryReadGuid( WorkorderSubAssembliesMap.ID);
			a.psiPrimaryKey = reader.TryReadstring( WorkorderSubAssembliesMap.psiPrimaryKey);
			a.psiLstChgDT = reader.TryReadDateTime( WorkorderSubAssembliesMap.LastModified);
			a.psiLstChgUser = reader.TryReadstring( WorkorderSubAssembliesMap.psiLstChgUser);
			a.psiFK_Workorder_GUID = reader.TryReadGuid( WorkorderSubAssembliesMap.psiFK_Workorder_GUID);
			a.psiFK_Workorder = reader.TryReadstring( WorkorderSubAssembliesMap.psiFK_Workorder);
			a.psiFK_r_ComponentCategory_GUID = reader.TryReadGuid( WorkorderSubAssembliesMap.psiFK_r_ComponentCategory_GUID);
			a.psiFK_r_ComponentCategory = reader.TryReadstring( WorkorderSubAssembliesMap.psiFK_r_ComponentCategory);
			a.psiStartedDT = reader.TryReadDateTime( WorkorderSubAssembliesMap.psiStartedDT);
			a.psiFK_Assembly_GUID = reader.TryReadGuid( WorkorderSubAssembliesMap.psiFK_Assembly_GUID);
			a.psiFK_Assembly = reader.TryReadstring( WorkorderSubAssembliesMap.psiFK_Assembly);
			a.psiFK_UserMaster_GUID = reader.TryReadGuid( WorkorderSubAssembliesMap.psiFK_UserMaster_GUID);
			a.psiFK_UserMaster = reader.TryReadstring( WorkorderSubAssembliesMap.psiFK_UserMaster);
			a.psiFK_r_PSRSubAssemblyStatus_GUID = reader.TryReadGuid( WorkorderSubAssembliesMap.psiFK_r_PSRSubAssemblyStatus_GUID);
			a.psiFK_r_PSRSubAssemblyStatus = reader.TryReadstring( WorkorderSubAssembliesMap.psiFK_r_PSRSubAssemblyStatus);
			a.psiStatusDT = reader.TryReadDateTime( WorkorderSubAssembliesMap.psiStatusDT);
			a.psiCurrentGUIPhase = reader.TryReadstring( WorkorderSubAssembliesMap.psiCurrentGUIPhase);
    
            return a;
        }

   		public override void RegisterParams()
		{
			WorkorderSubAssemblies a = (WorkorderSubAssemblies)dataObj;
			this.AddParam( WorkorderSubAssembliesMap.ID, WorkorderSubAssembliesMap.Param.ID, a.ID );
			this.AddParam( WorkorderSubAssembliesMap.psiPrimaryKey, WorkorderSubAssembliesMap.Param.psiPrimaryKey, a.psiPrimaryKey );
			this.AddParam( WorkorderSubAssembliesMap.LastModified, WorkorderSubAssembliesMap.Param.LastModified, a.LastModified );
			this.AddParam( WorkorderSubAssembliesMap.psiLstChgUser, WorkorderSubAssembliesMap.Param.psiLstChgUser, a.psiLstChgUser );
			this.AddParam( WorkorderSubAssembliesMap.psiFK_Workorder_GUID, WorkorderSubAssembliesMap.Param.psiFK_Workorder_GUID, a.psiFK_Workorder_GUID );
			this.AddParam( WorkorderSubAssembliesMap.psiFK_Workorder, WorkorderSubAssembliesMap.Param.psiFK_Workorder, a.psiFK_Workorder );
			this.AddParam( WorkorderSubAssembliesMap.psiFK_r_ComponentCategory_GUID, WorkorderSubAssembliesMap.Param.psiFK_r_ComponentCategory_GUID, a.psiFK_r_ComponentCategory_GUID );
			this.AddParam( WorkorderSubAssembliesMap.psiFK_r_ComponentCategory, WorkorderSubAssembliesMap.Param.psiFK_r_ComponentCategory, a.psiFK_r_ComponentCategory );
			this.AddParam( WorkorderSubAssembliesMap.psiStartedDT, WorkorderSubAssembliesMap.Param.psiStartedDT, a.psiStartedDT );
			this.AddParam( WorkorderSubAssembliesMap.psiFK_Assembly_GUID, WorkorderSubAssembliesMap.Param.psiFK_Assembly_GUID, a.psiFK_Assembly_GUID );
			this.AddParam( WorkorderSubAssembliesMap.psiFK_Assembly, WorkorderSubAssembliesMap.Param.psiFK_Assembly, a.psiFK_Assembly );
			this.AddParam( WorkorderSubAssembliesMap.psiFK_UserMaster_GUID, WorkorderSubAssembliesMap.Param.psiFK_UserMaster_GUID, a.psiFK_UserMaster_GUID );
			this.AddParam( WorkorderSubAssembliesMap.psiFK_UserMaster, WorkorderSubAssembliesMap.Param.psiFK_UserMaster, a.psiFK_UserMaster );
			this.AddParam( WorkorderSubAssembliesMap.psiFK_r_PSRSubAssemblyStatus_GUID, WorkorderSubAssembliesMap.Param.psiFK_r_PSRSubAssemblyStatus_GUID, a.psiFK_r_PSRSubAssemblyStatus_GUID );
			this.AddParam( WorkorderSubAssembliesMap.psiFK_r_PSRSubAssemblyStatus, WorkorderSubAssembliesMap.Param.psiFK_r_PSRSubAssemblyStatus, a.psiFK_r_PSRSubAssemblyStatus );
			this.AddParam( WorkorderSubAssembliesMap.psiStatusDT, WorkorderSubAssembliesMap.Param.psiStatusDT, a.psiStatusDT );
			this.AddParam( WorkorderSubAssembliesMap.psiCurrentGUIPhase, WorkorderSubAssembliesMap.Param.psiCurrentGUIPhase, a.psiCurrentGUIPhase );
    
		}
    } 
    
    /// <summary>
    /// A partial class to work with the XR Transactions which represents the WorkorderSubAssembliesStatusHistory table in the PumpServicing Database.
    /// </summary>
    public partial class WorkorderSubAssembliesStatusHistoryTransaction : BaseTransaction<WorkorderSubAssembliesStatusHistory>
    {
        public WorkorderSubAssembliesStatusHistoryTransaction()
            : base(WorkorderSubAssembliesStatusHistoryMap.TABLE_NAME, WorkorderSubAssembliesStatusHistoryMap.ID)
        {
        }
        
        public WorkorderSubAssembliesStatusHistoryTransaction( IBaseData _type )
			: base( _type, WorkorderSubAssembliesStatusHistoryMap.TABLE_NAME, WorkorderSubAssembliesStatusHistoryMap.ID )
		{
        }

        public WorkorderSubAssembliesStatusHistoryTransaction( string _PrimaryKey )
			: base( WorkorderSubAssembliesStatusHistoryMap.TABLE_NAME, WorkorderSubAssembliesStatusHistoryMap.ID, _PrimaryKey )
		{
        }

        public WorkorderSubAssembliesStatusHistoryTransaction( DateTime _lastModified )
            : base( WorkorderSubAssembliesStatusHistoryMap.TABLE_NAME, WorkorderSubAssembliesStatusHistoryMap.ID, _lastModified, WorkorderSubAssembliesStatusHistoryMap.LastModified )
        {
        }				
    
        public override WorkorderSubAssembliesStatusHistory BuildFromReader(TransactionReader reader)
        {
            WorkorderSubAssembliesStatusHistory a = new WorkorderSubAssembliesStatusHistory( );
			a.xh4PrimaryKey_GUID = reader.TryReadGuid( WorkorderSubAssembliesStatusHistoryMap.ID);
			a.xh4PrimaryKey = reader.TryReadstring( WorkorderSubAssembliesStatusHistoryMap.xh4PrimaryKey);
			a.xh4LstChgDT = reader.TryReadDateTime( WorkorderSubAssembliesStatusHistoryMap.LastModified);
			a.xh4LstChgUser = reader.TryReadstring( WorkorderSubAssembliesStatusHistoryMap.xh4LstChgUser);
			a.xh4FK_WorkorderSubAssemblies_GUID = reader.TryReadGuid( WorkorderSubAssembliesStatusHistoryMap.xh4FK_WorkorderSubAssemblies_GUID);
			a.xh4FK_WorkorderSubAssemblies = reader.TryReadstring( WorkorderSubAssembliesStatusHistoryMap.xh4FK_WorkorderSubAssemblies);
			a.xh4FK_r_PSRSubAssemblyStatus_GUID = reader.TryReadGuid( WorkorderSubAssembliesStatusHistoryMap.xh4FK_r_PSRSubAssemblyStatus_GUID);
			a.xh4FK_r_PSRSubAssemblyStatus = reader.TryReadstring( WorkorderSubAssembliesStatusHistoryMap.xh4FK_r_PSRSubAssemblyStatus);
    
            return a;
        }

   		public override void RegisterParams()
		{
			WorkorderSubAssembliesStatusHistory a = (WorkorderSubAssembliesStatusHistory)dataObj;
			this.AddParam( WorkorderSubAssembliesStatusHistoryMap.ID, WorkorderSubAssembliesStatusHistoryMap.Param.ID, a.ID );
			this.AddParam( WorkorderSubAssembliesStatusHistoryMap.xh4PrimaryKey, WorkorderSubAssembliesStatusHistoryMap.Param.xh4PrimaryKey, a.xh4PrimaryKey );
			this.AddParam( WorkorderSubAssembliesStatusHistoryMap.LastModified, WorkorderSubAssembliesStatusHistoryMap.Param.LastModified, a.LastModified );
			this.AddParam( WorkorderSubAssembliesStatusHistoryMap.xh4LstChgUser, WorkorderSubAssembliesStatusHistoryMap.Param.xh4LstChgUser, a.xh4LstChgUser );
			this.AddParam( WorkorderSubAssembliesStatusHistoryMap.xh4FK_WorkorderSubAssemblies_GUID, WorkorderSubAssembliesStatusHistoryMap.Param.xh4FK_WorkorderSubAssemblies_GUID, a.xh4FK_WorkorderSubAssemblies_GUID );
			this.AddParam( WorkorderSubAssembliesStatusHistoryMap.xh4FK_WorkorderSubAssemblies, WorkorderSubAssembliesStatusHistoryMap.Param.xh4FK_WorkorderSubAssemblies, a.xh4FK_WorkorderSubAssemblies );
			this.AddParam( WorkorderSubAssembliesStatusHistoryMap.xh4FK_r_PSRSubAssemblyStatus_GUID, WorkorderSubAssembliesStatusHistoryMap.Param.xh4FK_r_PSRSubAssemblyStatus_GUID, a.xh4FK_r_PSRSubAssemblyStatus_GUID );
			this.AddParam( WorkorderSubAssembliesStatusHistoryMap.xh4FK_r_PSRSubAssemblyStatus, WorkorderSubAssembliesStatusHistoryMap.Param.xh4FK_r_PSRSubAssemblyStatus, a.xh4FK_r_PSRSubAssemblyStatus );
    
		}
    } 
    
}
