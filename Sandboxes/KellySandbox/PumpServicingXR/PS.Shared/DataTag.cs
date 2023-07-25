using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFT.PS.Shared
{
    public enum DataTag
    {
        Assembly,
        AssemblyComponent,
        AssemblyComponentBblPlgMeasure,
        AssemblyComponentSRPump,
        BusinessOrganization,
        Component,
        ComponentSRPump,
        DatabaseConfiguration,
        DeletedLog,
        Document,
        Event,
        EventAssembleSRPump,
        EventComponentFailure,
        EventDetailCosts,
        EventDisassembleSRPump,
        EventInstallPump,
        EventPullPump,
        Facility,
        Invoice,
        Job,
        JobStatusChangeLog,
        Lease,
        Owner,
        StickyNotes,
        TemplatePump,
        TemplatePumpDetail,
        TemplateSubAssembly,
        TemplateSubAssemblyDetail,
        UserMaster,
        Well,
        WellCompletionReservoirs,
        WellCompletionXRef,
        Workorder,
        WorkorderStatusHistory,
        WorkorderSubAssemblies,
        WorkorderSubAssembliesStatusHistory,

        None    // LEAVE THIS AS THE LAST ITEM
    }
}
