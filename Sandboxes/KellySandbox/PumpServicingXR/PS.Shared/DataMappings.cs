using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace WFT.PS.Shared
{
	public static class DataTagMapping
	{
		public static Dictionary<DataTag, string> DataTagTables { get; private set; }
		public static Dictionary<DataTag, string> DataTagIDColumns { get; private set; }
		public static Dictionary<DataTag, Guid> DataTagIDs { get; private set; }
		public static Dictionary<Guid, DataTag> DataTagsByID { get; private set; }
		public static Dictionary<DataTag, string> FriendlyNames { get; private set; }

		static DataTagMapping()
		{
			// Database tables mapped by DataTag
			DataTagTables = new Dictionary<DataTag, string>()
			{
                {DataTag.Assembly, "Assembly"},
                {DataTag.AssemblyComponent, "AssemblyComponent"},
                {DataTag.AssemblyComponentBblPlgMeasure, "AssemblyComponentBblPlgMeasure"},
                {DataTag.AssemblyComponentSRPump, "AssemblyComponentSRPump"},
                {DataTag.BusinessOrganization, "BusinessOrganization"},
                {DataTag.Component, "Component"},
                {DataTag.ComponentSRPump, "ComponentSRPump"},
                {DataTag.DatabaseConfiguration, "DatabaseConfiguration"},
                {DataTag.DeletedLog, "DeletedLog"},
                {DataTag.Document, "Document"},
                {DataTag.Event, "Event"},
                {DataTag.EventAssembleSRPump, "EventAssembleSRPump"},
                {DataTag.EventComponentFailure, "EventComponentFailure"},
                {DataTag.EventDetailCosts, "EventDetailCosts"},
                {DataTag.EventDisassembleSRPump, "EventDisassembleSRPump"},
                {DataTag.EventInstallPump, "EventInstallPump"},
                {DataTag.EventPullPump, "EventPullPump"},
                {DataTag.Facility, "Facility"},
                {DataTag.Invoice, "Invoice"},
                {DataTag.Job, "Job"},
                {DataTag.JobStatusChangeLog, "JobStatusChangeLog"},
                {DataTag.Lease, "Lease"},
                {DataTag.Owner, "Owner"},
                {DataTag.StickyNotes, "StickyNotes"},
                {DataTag.TemplatePump, "TemplatePump"},
                {DataTag.TemplatePumpDetail, "TemplatePumpDetail"},
                {DataTag.TemplateSubAssembly, "TemplateSubAssembly"},
                {DataTag.TemplateSubAssemblyDetail, "TemplateSubAssemblyDetail"},
                {DataTag.UserMaster, "UserMaster"},
                {DataTag.Well, "Well"},
                {DataTag.WellCompletionReservoirs, "WellCompletionReservoirs"},
                {DataTag.WellCompletionXRef, "WellCompletionXRef"},
                {DataTag.Workorder, "Workorder"},
                {DataTag.WorkorderStatusHistory, "WorkorderStatusHistory"},
                {DataTag.WorkorderSubAssemblies, "WorkorderSubAssemblies"},
                {DataTag.WorkorderSubAssembliesStatusHistory, "WorkorderSubAssembliesStatusHistory"}
		    };

			// ID Columns in the tables mapped by DataTag
			DataTagIDColumns = new Dictionary<DataTag, string>()
			{
                {DataTag.Assembly, "aclPrimaryKey_GUID"},
                {DataTag.AssemblyComponent, "ascPrimaryKey_GUID"},
                {DataTag.AssemblyComponentBblPlgMeasure, "acmPrimaryKey_GUID"},
                {DataTag.AssemblyComponentSRPump, "arpPrimaryKey_GUID"},
                {DataTag.BusinessOrganization, "venPrimaryKey_GUID"},
                {DataTag.Component, "cmcPrimaryKey_GUID"},
                {DataTag.ComponentSRPump, "cspPrimaryKey_GUID"},
                {DataTag.DatabaseConfiguration, "dbcPrimaryKey_GUID"},
                {DataTag.DeletedLog, "delPrimaryKey_GUID"},
                {DataTag.Document, "docPrimaryKey_GUID"},
                {DataTag.Event, "evcPrimaryKey_GUID"},
                {DataTag.EventAssembleSRPump, "psrPrimaryKey_GUID"},
                {DataTag.EventComponentFailure, "acfPrimaryKey_GUID"},
                {DataTag.EventDetailCosts, "ecsPrimaryKey_GUID"},
                {DataTag.EventDisassembleSRPump, "ptdPrimaryKey_GUID"},
                {DataTag.EventInstallPump, "eipPrimaryKey_GUID"},
                {DataTag.EventPullPump, "eppPrimaryKey_GUID"},
                {DataTag.Facility, "facPrimaryKey_GUID"},
                {DataTag.Invoice, "xh5PrimaryKey_GUID"},
                {DataTag.Job, "ecgPrimaryKey_GUID"},
                {DataTag.JobStatusChangeLog, "jscPrimaryKey_GUID"},
                {DataTag.Lease, "lsePrimaryKey_GUID"},
                {DataTag.Owner, "ownPrimaryKey"},
                {DataTag.StickyNotes, "styPrimaryKey_GUID"},
                {DataTag.TemplatePump, "tphPrimaryKey_GUID"},
                {DataTag.TemplatePumpDetail, "tpdPrimaryKey_GUID"},
                {DataTag.TemplateSubAssembly, "tahPrimaryKey_GUID"},
                {DataTag.TemplateSubAssemblyDetail, "tadPrimaryKey_GUID"},
                {DataTag.UserMaster, "usrPrimaryKey_GUID"},
                {DataTag.Well, "welPrimaryKey_GUID"},
                {DataTag.WellCompletionReservoirs, "wcrPrimaryKey_GUID"},
                {DataTag.WellCompletionXRef, "wxrPrimaryKey_GUID"},
                {DataTag.Workorder, "pswPrimaryKey_GUID"},
                {DataTag.WorkorderStatusHistory, "xhdPrimaryKey_GUID"},
                {DataTag.WorkorderSubAssemblies, "psiPrimaryKey_GUID"},
                {DataTag.WorkorderSubAssembliesStatusHistory, "xh4PrimaryKey_GUID"}
		    };

			DataTagIDs = new Dictionary<DataTag, Guid>()
			{
                {DataTag.Assembly, new Guid("D2BA7694-0297-482E-91E2-0D8EEB50F136")},
                {DataTag.AssemblyComponent, new Guid("E086ED87-E07B-4770-97DF-13CFA640D87C")},
                {DataTag.AssemblyComponentBblPlgMeasure, new Guid("91328E6E-AFF3-4773-B4CE-A0808A063DB4")},
                {DataTag.AssemblyComponentSRPump, new Guid("3EC968FF-4540-4D2F-9693-A78A88750523")},
                {DataTag.BusinessOrganization, new Guid("75661E43-E5FD-4381-A254-5FC0622C1984")},
                {DataTag.Component, new Guid("A07908B9-79FA-4193-BF53-4BBC8E431399")},
                {DataTag.ComponentSRPump, new Guid("C27D8775-CD65-4F1B-A9B9-948A09CEFA96")},
                {DataTag.DatabaseConfiguration, new Guid("1503BD35-F837-4D61-ABA9-859D3CBAE3AE")},
                {DataTag.DeletedLog, new Guid("B9272A69-5BF3-4ADD-AD74-C28AC8FC325A")},
                {DataTag.Document, new Guid("8F25701B-1DBF-42FC-A6AE-85D8257E89A8")},
                {DataTag.Event, new Guid("EA0B65CF-43CC-47CA-B246-5E91DFA72F17")},
                {DataTag.EventAssembleSRPump, new Guid("7CFF1279-7BA3-415A-95C9-3123DA8308C5")},
                {DataTag.EventComponentFailure, new Guid("64B75B19-1C64-4E6D-A79D-932B1C47D53B")},
                {DataTag.EventDetailCosts, new Guid("B39A9AF8-D30B-4EB9-A221-8173FFD3D08A")},
                {DataTag.EventDisassembleSRPump, new Guid("160B3734-6046-4B60-AAC7-563F1035D222")},
                {DataTag.EventInstallPump, new Guid("EC357E17-4B1A-4921-A123-C6EB1FF27278")},
                {DataTag.EventPullPump, new Guid("449324DD-9478-4619-80D4-FBBEBBF74E3A")},
                {DataTag.Facility, new Guid("8EB960D0-E93E-412F-AE4E-EC836EA47E28")},
                {DataTag.Invoice, new Guid("C00015C2-10BE-440E-8DF6-E737F3FEACD6")},
                {DataTag.Job, new Guid("471BF973-B45F-4FC9-803C-D95E4199F273")},
                {DataTag.JobStatusChangeLog, new Guid("7E606968-CA87-42E6-9BAD-81CA3BF00A9D")},
                {DataTag.Lease, new Guid("9B4C3796-8F5B-402C-8FEE-EC7B01202EF9")},
                {DataTag.Owner, new Guid("C7FEF8E7-88D4-496E-82D9-080DE910FD33")},
                {DataTag.StickyNotes, new Guid("C8EB5A85-A1EE-4AEC-9178-C9CBADE39EAC")},
                {DataTag.TemplatePump, new Guid("1C04BAE8-0188-46D5-B276-13B08A15F937")},
                {DataTag.TemplatePumpDetail, new Guid("7BB0BFFA-A885-4328-9C91-6B2F07D5B72D")},
                {DataTag.TemplateSubAssembly, new Guid("2A103568-5703-4C53-A987-A2277A5E7575")},
                {DataTag.TemplateSubAssemblyDetail, new Guid("DD8448F9-8F14-4D99-B3BC-87190E1CE994")},
                {DataTag.UserMaster, new Guid("3BB5D62F-E362-4E08-8A15-0375373AAE0A")},
                {DataTag.Well, new Guid("D5FC3F19-23D8-4A41-B8CE-067E01AEE851")},
                {DataTag.WellCompletionReservoirs, new Guid("1C08730F-FF4C-4F2B-88FC-2812CEFB9AE3")},
                {DataTag.WellCompletionXRef, new Guid("2114D249-A69E-4FBD-A8BB-D420A6E93EEE")},
                {DataTag.Workorder, new Guid("A35F2301-971D-44F0-A19C-2F8891BDA78E")},
                {DataTag.WorkorderStatusHistory, new Guid("44E9A698-972C-48FC-A76A-B5124FB14FDF")},
                {DataTag.WorkorderSubAssemblies, new Guid("51491CB7-B9E4-4B38-9865-61377F9A22EB")},
                {DataTag.WorkorderSubAssembliesStatusHistory, new Guid("0CAD311E-4388-484E-BBD2-218E7C4F05C8")}
            };

            // Build "reverse-lookup" dictionary
            DataTagsByID = new Dictionary<Guid, DataTag>( );
            foreach ( var swap in DataTagIDs ) 
            {
                DataTagsByID.Add( swap.Value, swap.Key );
            }

            InitTags();
        }

        public static void InitTags()
        {
            FriendlyNames = new Dictionary<DataTag, string>()
            {
                {DataTag.Assembly, WFT.PS.Shared.Properties.Resources.Assembly},
                {DataTag.AssemblyComponent, WFT.PS.Shared.Properties.Resources.AssemblyComponent},
                {DataTag.AssemblyComponentBblPlgMeasure, WFT.PS.Shared.Properties.Resources.AssemblyComponentBblPlgMeasure},
                {DataTag.AssemblyComponentSRPump, WFT.PS.Shared.Properties.Resources.AssemblyComponentSRPump},
                {DataTag.BusinessOrganization, WFT.PS.Shared.Properties.Resources.BusinessOrganization},
                {DataTag.Component, WFT.PS.Shared.Properties.Resources.Component},
                {DataTag.ComponentSRPump, WFT.PS.Shared.Properties.Resources.ComponentSRPump},
                {DataTag.DatabaseConfiguration, WFT.PS.Shared.Properties.Resources.DatabaseConfiguration},
                {DataTag.DeletedLog, WFT.PS.Shared.Properties.Resources.DeletedLog},
                {DataTag.Document, WFT.PS.Shared.Properties.Resources.Document},
                {DataTag.Event, WFT.PS.Shared.Properties.Resources.Event},
                {DataTag.EventAssembleSRPump, WFT.PS.Shared.Properties.Resources.EventAssembleSRPump},
                {DataTag.EventComponentFailure, WFT.PS.Shared.Properties.Resources.EventComponentFailure},
                {DataTag.EventDetailCosts, WFT.PS.Shared.Properties.Resources.EventDetailCosts},
                {DataTag.EventDisassembleSRPump, WFT.PS.Shared.Properties.Resources.EventDisassembleSRPump},
                {DataTag.EventInstallPump, WFT.PS.Shared.Properties.Resources.EventInstallPump},
                {DataTag.EventPullPump, WFT.PS.Shared.Properties.Resources.EventPullPump},
                {DataTag.Facility, WFT.PS.Shared.Properties.Resources.Facility},
                {DataTag.Invoice, WFT.PS.Shared.Properties.Resources.Invoice},
                {DataTag.Job, WFT.PS.Shared.Properties.Resources.Job},
                {DataTag.JobStatusChangeLog, WFT.PS.Shared.Properties.Resources.JobStatusChangeLog},
                {DataTag.Lease, WFT.PS.Shared.Properties.Resources.Lease},
                {DataTag.Owner, WFT.PS.Shared.Properties.Resources.Owner},
                {DataTag.StickyNotes, WFT.PS.Shared.Properties.Resources.StickyNotes},
                {DataTag.TemplatePump, WFT.PS.Shared.Properties.Resources.TemplatePump},
                {DataTag.TemplatePumpDetail, WFT.PS.Shared.Properties.Resources.TemplatePumpDetail},
                {DataTag.TemplateSubAssembly, WFT.PS.Shared.Properties.Resources.TemplateSubAssembly},
                {DataTag.TemplateSubAssemblyDetail, WFT.PS.Shared.Properties.Resources.TemplateSubAssemblyDetail},
                {DataTag.UserMaster, WFT.PS.Shared.Properties.Resources.UserMaster},
                {DataTag.Well, WFT.PS.Shared.Properties.Resources.Well},
                {DataTag.WellCompletionReservoirs, WFT.PS.Shared.Properties.Resources.WellCompletionReservoirs},
                {DataTag.WellCompletionXRef, WFT.PS.Shared.Properties.Resources.WellCompletionXRef},
                {DataTag.Workorder, WFT.PS.Shared.Properties.Resources.Workorder},
                {DataTag.WorkorderStatusHistory, WFT.PS.Shared.Properties.Resources.WorkorderStatusHistory},
                {DataTag.WorkorderSubAssemblies, WFT.PS.Shared.Properties.Resources.WorkorderSubAssemblies},
                {DataTag.WorkorderSubAssembliesStatusHistory, WFT.PS.Shared.Properties.Resources.WorkorderSubAssembliesStatusHistory}
            };
        }
	}
}
