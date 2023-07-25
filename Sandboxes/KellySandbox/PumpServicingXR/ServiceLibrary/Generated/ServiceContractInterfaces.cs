


using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace WFT.PSService.ServiceLibrary
{
     /// <summary>
     /// A ServiceContract interface to work with the WCF Service Library which represents all the tables in the PumpServicing Database.
     /// </summary>
    [ServiceContract( Namespace = "http://www.lyonsforge.com/PumpServicing" )]
    public interface IRecordAccessor
    {
   		[OperationContract]
		ServiceResult PutAssembly( SyncContext sync, Assembly _Assembly );

   		[OperationContract]
		ServiceResult PutAssemblyComponent( SyncContext sync, AssemblyComponent _AssemblyComponent );

   		[OperationContract]
		ServiceResult PutAssemblyComponentBblPlgMeasure( SyncContext sync, AssemblyComponentBblPlgMeasure _AssemblyComponentBblPlgMeasure );

   		[OperationContract]
		ServiceResult PutAssemblyComponentSRPump( SyncContext sync, AssemblyComponentSRPump _AssemblyComponentSRPump );

   		[OperationContract]
		ServiceResult PutBusinessOrganization( SyncContext sync, BusinessOrganization _BusinessOrganization );

   		[OperationContract]
		ServiceResult PutComponent( SyncContext sync, Component _Component );

   		[OperationContract]
		ServiceResult PutComponentSRPump( SyncContext sync, ComponentSRPump _ComponentSRPump );

   		[OperationContract]
		ServiceResult PutDatabaseConfiguration( SyncContext sync, DatabaseConfiguration _DatabaseConfiguration );

   		[OperationContract]
		ServiceResult PutDeletedLog( SyncContext sync, DeletedLog _DeletedLog );

   		[OperationContract]
		ServiceResult PutDocument( SyncContext sync, Document _Document );

   		[OperationContract]
		ServiceResult PutEvent( SyncContext sync, Event _Event );

   		[OperationContract]
		ServiceResult PutEventAssembleSRPump( SyncContext sync, EventAssembleSRPump _EventAssembleSRPump );

   		[OperationContract]
		ServiceResult PutEventComponentFailure( SyncContext sync, EventComponentFailure _EventComponentFailure );

   		[OperationContract]
		ServiceResult PutEventDetailCosts( SyncContext sync, EventDetailCosts _EventDetailCosts );

   		[OperationContract]
		ServiceResult PutEventDisassembleSRPump( SyncContext sync, EventDisassembleSRPump _EventDisassembleSRPump );

   		[OperationContract]
		ServiceResult PutEventInstallPump( SyncContext sync, EventInstallPump _EventInstallPump );

   		[OperationContract]
		ServiceResult PutEventPullPump( SyncContext sync, EventPullPump _EventPullPump );

   		[OperationContract]
		ServiceResult PutFacility( SyncContext sync, Facility _Facility );

   		[OperationContract]
		ServiceResult PutInvoice( SyncContext sync, Invoice _Invoice );

   		[OperationContract]
		ServiceResult PutJob( SyncContext sync, Job _Job );

   		[OperationContract]
		ServiceResult PutJobStatusChangeLog( SyncContext sync, JobStatusChangeLog _JobStatusChangeLog );

   		[OperationContract]
		ServiceResult PutLease( SyncContext sync, Lease _Lease );

   		[OperationContract]
		ServiceResult PutOwner( SyncContext sync, Owner _Owner );

   		[OperationContract]
		ServiceResult PutStickyNotes( SyncContext sync, StickyNotes _StickyNotes );

   		[OperationContract]
		ServiceResult PutTemplatePump( SyncContext sync, TemplatePump _TemplatePump );

   		[OperationContract]
		ServiceResult PutTemplatePumpDetail( SyncContext sync, TemplatePumpDetail _TemplatePumpDetail );

   		[OperationContract]
		ServiceResult PutTemplateSubAssembly( SyncContext sync, TemplateSubAssembly _TemplateSubAssembly );

   		[OperationContract]
		ServiceResult PutTemplateSubAssemblyDetail( SyncContext sync, TemplateSubAssemblyDetail _TemplateSubAssemblyDetail );

   		[OperationContract]
		ServiceResult PutUserMaster( SyncContext sync, UserMaster _UserMaster );

   		[OperationContract]
		ServiceResult PutWell( SyncContext sync, Well _Well );

   		[OperationContract]
		ServiceResult PutWellCompletionReservoirs( SyncContext sync, WellCompletionReservoirs _WellCompletionReservoirs );

   		[OperationContract]
		ServiceResult PutWellCompletionXRef( SyncContext sync, WellCompletionXRef _WellCompletionXRef );

   		[OperationContract]
		ServiceResult PutWorkorder( SyncContext sync, Workorder _Workorder );

   		[OperationContract]
		ServiceResult PutWorkorderStatusHistory( SyncContext sync, WorkorderStatusHistory _WorkorderStatusHistory );

   		[OperationContract]
		ServiceResult PutWorkorderSubAssemblies( SyncContext sync, WorkorderSubAssemblies _WorkorderSubAssemblies );

   		[OperationContract]
		ServiceResult PutWorkorderSubAssembliesStatusHistory( SyncContext sync, WorkorderSubAssembliesStatusHistory _WorkorderSubAssembliesStatusHistory );

	}
}
