


using System;
using WFT.PSService.ServiceLibrary;
using WFT.PSService.Data;
using LFI.Sync.DataManager;
using System.Collections.Generic;

namespace WFT.PSService.Service
{
    /// <summary>
    /// A partial AppServer class to work with the tables in the PumpServicing Database.
    /// </summary>
    public partial class AppServer : IRecordAccessor
    {
    
        public ServiceResult PutAssembly( SyncContext _sync, Assembly _Assembly )
        {
			return m_recordProcessor.Process<Assembly, AssemblyTransaction>( _sync, _Assembly );
        }
    
        public ServiceResult PutAssemblyComponent( SyncContext _sync, AssemblyComponent _AssemblyComponent )
        {
			return m_recordProcessor.Process<AssemblyComponent, AssemblyComponentTransaction>( _sync, _AssemblyComponent );
        }
    
        public ServiceResult PutAssemblyComponentBblPlgMeasure( SyncContext _sync, AssemblyComponentBblPlgMeasure _AssemblyComponentBblPlgMeasure )
        {
			return m_recordProcessor.Process<AssemblyComponentBblPlgMeasure, AssemblyComponentBblPlgMeasureTransaction>( _sync, _AssemblyComponentBblPlgMeasure );
        }
    
        public ServiceResult PutAssemblyComponentSRPump( SyncContext _sync, AssemblyComponentSRPump _AssemblyComponentSRPump )
        {
			return m_recordProcessor.Process<AssemblyComponentSRPump, AssemblyComponentSRPumpTransaction>( _sync, _AssemblyComponentSRPump );
        }
    
        public ServiceResult PutBusinessOrganization( SyncContext _sync, BusinessOrganization _BusinessOrganization )
        {
			return m_recordProcessor.Process<BusinessOrganization, BusinessOrganizationTransaction>( _sync, _BusinessOrganization );
        }
    
        public ServiceResult PutComponent( SyncContext _sync, Component _Component )
        {
			return m_recordProcessor.Process<Component, ComponentTransaction>( _sync, _Component );
        }
    
        public ServiceResult PutComponentSRPump( SyncContext _sync, ComponentSRPump _ComponentSRPump )
        {
			return m_recordProcessor.Process<ComponentSRPump, ComponentSRPumpTransaction>( _sync, _ComponentSRPump );
        }
    
        public ServiceResult PutDatabaseConfiguration( SyncContext _sync, DatabaseConfiguration _DatabaseConfiguration )
        {
			return m_recordProcessor.Process<DatabaseConfiguration, DatabaseConfigurationTransaction>( _sync, _DatabaseConfiguration );
        }
    
        public ServiceResult PutDeletedLog( SyncContext _sync, DeletedLog _DeletedLog )
        {
			return m_recordProcessor.Process<DeletedLog, DeletedLogTransaction>( _sync, _DeletedLog );
        }
    
        public ServiceResult PutDocument( SyncContext _sync, Document _Document )
        {
			return m_recordProcessor.Process<Document, DocumentTransaction>( _sync, _Document );
        }
    
        public ServiceResult PutEvent( SyncContext _sync, Event _Event )
        {
			return m_recordProcessor.Process<Event, EventTransaction>( _sync, _Event );
        }
    
        public ServiceResult PutEventAssembleSRPump( SyncContext _sync, EventAssembleSRPump _EventAssembleSRPump )
        {
			return m_recordProcessor.Process<EventAssembleSRPump, EventAssembleSRPumpTransaction>( _sync, _EventAssembleSRPump );
        }
    
        public ServiceResult PutEventComponentFailure( SyncContext _sync, EventComponentFailure _EventComponentFailure )
        {
			return m_recordProcessor.Process<EventComponentFailure, EventComponentFailureTransaction>( _sync, _EventComponentFailure );
        }
    
        public ServiceResult PutEventDetailCosts( SyncContext _sync, EventDetailCosts _EventDetailCosts )
        {
			return m_recordProcessor.Process<EventDetailCosts, EventDetailCostsTransaction>( _sync, _EventDetailCosts );
        }
    
        public ServiceResult PutEventDisassembleSRPump( SyncContext _sync, EventDisassembleSRPump _EventDisassembleSRPump )
        {
			return m_recordProcessor.Process<EventDisassembleSRPump, EventDisassembleSRPumpTransaction>( _sync, _EventDisassembleSRPump );
        }
    
        public ServiceResult PutEventInstallPump( SyncContext _sync, EventInstallPump _EventInstallPump )
        {
			return m_recordProcessor.Process<EventInstallPump, EventInstallPumpTransaction>( _sync, _EventInstallPump );
        }
    
        public ServiceResult PutEventPullPump( SyncContext _sync, EventPullPump _EventPullPump )
        {
			return m_recordProcessor.Process<EventPullPump, EventPullPumpTransaction>( _sync, _EventPullPump );
        }
    
        public ServiceResult PutFacility( SyncContext _sync, Facility _Facility )
        {
			return m_recordProcessor.Process<Facility, FacilityTransaction>( _sync, _Facility );
        }
    
        public ServiceResult PutInvoice( SyncContext _sync, Invoice _Invoice )
        {
			return m_recordProcessor.Process<Invoice, InvoiceTransaction>( _sync, _Invoice );
        }
    
        public ServiceResult PutJob( SyncContext _sync, Job _Job )
        {
			return m_recordProcessor.Process<Job, JobTransaction>( _sync, _Job );
        }
    
        public ServiceResult PutJobStatusChangeLog( SyncContext _sync, JobStatusChangeLog _JobStatusChangeLog )
        {
			return m_recordProcessor.Process<JobStatusChangeLog, JobStatusChangeLogTransaction>( _sync, _JobStatusChangeLog );
        }
    
        public ServiceResult PutLease( SyncContext _sync, Lease _Lease )
        {
			return m_recordProcessor.Process<Lease, LeaseTransaction>( _sync, _Lease );
        }
    
        public ServiceResult PutOwner( SyncContext _sync, Owner _Owner )
        {
			return m_recordProcessor.Process<Owner, OwnerTransaction>( _sync, _Owner );
        }
    
        public ServiceResult PutStickyNotes( SyncContext _sync, StickyNotes _StickyNotes )
        {
			return m_recordProcessor.Process<StickyNotes, StickyNotesTransaction>( _sync, _StickyNotes );
        }
    
        public ServiceResult PutTemplatePump( SyncContext _sync, TemplatePump _TemplatePump )
        {
			return m_recordProcessor.Process<TemplatePump, TemplatePumpTransaction>( _sync, _TemplatePump );
        }
    
        public ServiceResult PutTemplatePumpDetail( SyncContext _sync, TemplatePumpDetail _TemplatePumpDetail )
        {
			return m_recordProcessor.Process<TemplatePumpDetail, TemplatePumpDetailTransaction>( _sync, _TemplatePumpDetail );
        }
    
        public ServiceResult PutTemplateSubAssembly( SyncContext _sync, TemplateSubAssembly _TemplateSubAssembly )
        {
			return m_recordProcessor.Process<TemplateSubAssembly, TemplateSubAssemblyTransaction>( _sync, _TemplateSubAssembly );
        }
    
        public ServiceResult PutTemplateSubAssemblyDetail( SyncContext _sync, TemplateSubAssemblyDetail _TemplateSubAssemblyDetail )
        {
			return m_recordProcessor.Process<TemplateSubAssemblyDetail, TemplateSubAssemblyDetailTransaction>( _sync, _TemplateSubAssemblyDetail );
        }
    
        public ServiceResult PutUserMaster( SyncContext _sync, UserMaster _UserMaster )
        {
			return m_recordProcessor.Process<UserMaster, UserMasterTransaction>( _sync, _UserMaster );
        }
    
        public ServiceResult PutWell( SyncContext _sync, Well _Well )
        {
			return m_recordProcessor.Process<Well, WellTransaction>( _sync, _Well );
        }
    
        public ServiceResult PutWellCompletionReservoirs( SyncContext _sync, WellCompletionReservoirs _WellCompletionReservoirs )
        {
			return m_recordProcessor.Process<WellCompletionReservoirs, WellCompletionReservoirsTransaction>( _sync, _WellCompletionReservoirs );
        }
    
        public ServiceResult PutWellCompletionXRef( SyncContext _sync, WellCompletionXRef _WellCompletionXRef )
        {
			return m_recordProcessor.Process<WellCompletionXRef, WellCompletionXRefTransaction>( _sync, _WellCompletionXRef );
        }
    
        public ServiceResult PutWorkorder( SyncContext _sync, Workorder _Workorder )
        {
			return m_recordProcessor.Process<Workorder, WorkorderTransaction>( _sync, _Workorder );
        }
    
        public ServiceResult PutWorkorderStatusHistory( SyncContext _sync, WorkorderStatusHistory _WorkorderStatusHistory )
        {
			return m_recordProcessor.Process<WorkorderStatusHistory, WorkorderStatusHistoryTransaction>( _sync, _WorkorderStatusHistory );
        }
    
        public ServiceResult PutWorkorderSubAssemblies( SyncContext _sync, WorkorderSubAssemblies _WorkorderSubAssemblies )
        {
			return m_recordProcessor.Process<WorkorderSubAssemblies, WorkorderSubAssembliesTransaction>( _sync, _WorkorderSubAssemblies );
        }
    
        public ServiceResult PutWorkorderSubAssembliesStatusHistory( SyncContext _sync, WorkorderSubAssembliesStatusHistory _WorkorderSubAssembliesStatusHistory )
        {
			return m_recordProcessor.Process<WorkorderSubAssembliesStatusHistory, WorkorderSubAssembliesStatusHistoryTransaction>( _sync, _WorkorderSubAssembliesStatusHistory );
        }
	}
}
