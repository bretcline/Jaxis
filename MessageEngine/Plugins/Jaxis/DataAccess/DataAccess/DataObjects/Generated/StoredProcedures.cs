


using System;
using SubSonic;
using SubSonic.Schema;
using SubSonic.DataProviders;
using System.Data;

namespace Jaxis.Inventory.Data
{
	public partial class BeverageMonitorDB
	{
		
		public StoredProcedure CleanupManufacturers(string Name)
		{
			StoredProcedure sp=new StoredProcedure("CleanupManufacturers",this.Provider);
								sp.Command.AddParameter("Name",Name,DbType.String);
							return sp;
		}
		
		public StoredProcedure GetPourData(DateTime StartTime,DateTime EndTime)
		{
			StoredProcedure sp=new StoredProcedure("GetPourData",this.Provider);
								sp.Command.AddParameter("StartTime",StartTime,DbType.DateTime);
								sp.Command.AddParameter("EndTime",EndTime,DbType.DateTime);
							return sp;
		}
		
		public StoredProcedure GetPourPoint(Guid PourID)
		{
			StoredProcedure sp=new StoredProcedure("GetPourPoint",this.Provider);
								sp.Command.AddParameter("PourID",PourID,DbType.Guid);
							return sp;
		}
		
		public StoredProcedure GetUnknownAliases()
		{
			StoredProcedure sp=new StoredProcedure("GetUnknownAliases",this.Provider);
							return sp;
		}
		
		public StoredProcedure procGetDisconnectWithVolume(DateTime StartTime,DateTime EndTime)
		{
			StoredProcedure sp=new StoredProcedure("procGetDisconnectWithVolume",this.Provider);
								sp.Command.AddParameter("StartTime",StartTime,DbType.DateTime);
								sp.Command.AddParameter("EndTime",EndTime,DbType.DateTime);
							return sp;
		}
		
		public StoredProcedure procGetHighestVolume(DateTime StartTime,DateTime EndTime)
		{
			StoredProcedure sp=new StoredProcedure("procGetHighestVolume",this.Provider);
								sp.Command.AddParameter("StartTime",StartTime,DbType.DateTime);
								sp.Command.AddParameter("EndTime",EndTime,DbType.DateTime);
							return sp;
		}
		
		public StoredProcedure procGetInventoryMovement(DateTime StartTime,DateTime EndTime)
		{
			StoredProcedure sp=new StoredProcedure("procGetInventoryMovement",this.Provider);
								sp.Command.AddParameter("StartTime",StartTime,DbType.DateTime);
								sp.Command.AddParameter("EndTime",EndTime,DbType.DateTime);
							return sp;
		}
		
		public StoredProcedure procGetPOSMissingAliases(DateTime StartTime,DateTime EndTime)
		{
			StoredProcedure sp=new StoredProcedure("procGetPOSMissingAliases",this.Provider);
								sp.Command.AddParameter("StartTime",StartTime,DbType.DateTime);
								sp.Command.AddParameter("EndTime",EndTime,DbType.DateTime);
							return sp;
		}
		
		public StoredProcedure procGetPOSMissingTickets(DateTime StartTime,DateTime EndTime)
		{
			StoredProcedure sp=new StoredProcedure("procGetPOSMissingTickets",this.Provider);
								sp.Command.AddParameter("StartTime",StartTime,DbType.DateTime);
								sp.Command.AddParameter("EndTime",EndTime,DbType.DateTime);
							return sp;
		}
		
		public StoredProcedure procGetPOSPourMatches(DateTime StartTime,DateTime EndTime)
		{
			StoredProcedure sp=new StoredProcedure("procGetPOSPourMatches",this.Provider);
								sp.Command.AddParameter("StartTime",StartTime,DbType.DateTime);
								sp.Command.AddParameter("EndTime",EndTime,DbType.DateTime);
							return sp;
		}
		
		public StoredProcedure procGetPOSStatusCounts(DateTime StartTime,DateTime EndTime)
		{
			StoredProcedure sp=new StoredProcedure("procGetPOSStatusCounts",this.Provider);
								sp.Command.AddParameter("StartTime",StartTime,DbType.DateTime);
								sp.Command.AddParameter("EndTime",EndTime,DbType.DateTime);
							return sp;
		}
		
		public StoredProcedure procGetPOSTicketItems(DateTime StartTime,DateTime EndTime)
		{
			StoredProcedure sp=new StoredProcedure("procGetPOSTicketItems",this.Provider);
								sp.Command.AddParameter("StartTime",StartTime,DbType.DateTime);
								sp.Command.AddParameter("EndTime",EndTime,DbType.DateTime);
							return sp;
		}
		
		public StoredProcedure procGetPours(DateTime StartTime,DateTime EndTime)
		{
			StoredProcedure sp=new StoredProcedure("procGetPours",this.Provider);
								sp.Command.AddParameter("StartTime",StartTime,DbType.DateTime);
								sp.Command.AddParameter("EndTime",EndTime,DbType.DateTime);
							return sp;
		}
		
		public StoredProcedure procGetRecipeByTicketItemAlias(string Description)
		{
			StoredProcedure sp=new StoredProcedure("procGetRecipeByTicketItemAlias",this.Provider);
								sp.Command.AddParameter("Description",Description,DbType.String);
							return sp;
		}
		
		public StoredProcedure procUpdateInventoryCost(Guid UPCID,double Cost)
		{
			StoredProcedure sp=new StoredProcedure("procUpdateInventoryCost",this.Provider);
								sp.Command.AddParameter("UPCID",UPCID,DbType.Guid);
								sp.Command.AddParameter("Cost",Cost,DbType.Double);
							return sp;
		}
		
		public StoredProcedure procUpdateParLevel(Guid UPCID,Guid LocationID,double ParLevel)
		{
			StoredProcedure sp=new StoredProcedure("procUpdateParLevel",this.Provider);
								sp.Command.AddParameter("UPCID",UPCID,DbType.Guid);
								sp.Command.AddParameter("LocationID",LocationID,DbType.Guid);
								sp.Command.AddParameter("ParLevel",ParLevel,DbType.Double);
							return sp;
		}
		
		public StoredProcedure rptAlerts(DateTime StartDate,DateTime EndDate)
		{
			StoredProcedure sp=new StoredProcedure("rptAlerts",this.Provider);
								sp.Command.AddParameter("StartDate",StartDate,DbType.DateTime);
								sp.Command.AddParameter("EndDate",EndDate,DbType.DateTime);
							return sp;
		}
		
		public StoredProcedure rptCategoryCosts(DateTime BeginDate,DateTime EndDate)
		{
			StoredProcedure sp=new StoredProcedure("rptCategoryCosts",this.Provider);
								sp.Command.AddParameter("BeginDate",BeginDate,DbType.DateTime);
								sp.Command.AddParameter("EndDate",EndDate,DbType.DateTime);
							return sp;
		}
		
		public StoredProcedure rptInventoryCost(DateTime Date)
		{
			StoredProcedure sp=new StoredProcedure("rptInventoryCost",this.Provider);
								sp.Command.AddParameter("Date",Date,DbType.DateTime);
							return sp;
		}
		
		public StoredProcedure rptPourSummary(DateTime StartDate,DateTime EndDate)
		{
			StoredProcedure sp=new StoredProcedure("rptPourSummary",this.Provider);
								sp.Command.AddParameter("StartDate",StartDate,DbType.DateTime);
								sp.Command.AddParameter("EndDate",EndDate,DbType.DateTime);
							return sp;
		}
		
		public StoredProcedure rptPourSummaryByTag(DateTime StartDate,DateTime EndDate)
		{
			StoredProcedure sp=new StoredProcedure("rptPourSummaryByTag",this.Provider);
								sp.Command.AddParameter("StartDate",StartDate,DbType.DateTime);
								sp.Command.AddParameter("EndDate",EndDate,DbType.DateTime);
							return sp;
		}
		
		public StoredProcedure rptTagList()
		{
			StoredProcedure sp=new StoredProcedure("rptTagList",this.Provider);
							return sp;
		}
		
		public StoredProcedure widgetHourlyConsumption(DateTime Day)
		{
			StoredProcedure sp=new StoredProcedure("widgetHourlyConsumption",this.Provider);
								sp.Command.AddParameter("Day",Day,DbType.DateTime);
							return sp;
		}
		
	}

}

