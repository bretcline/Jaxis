using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using Jaxis.DrinkInventory.Reporting.Web2.Infrastructure;
using Jaxis.DrinkInventory.Reporting.Web2.Models;
using Jaxis.DrinkInventory.Reporting.Web2.ReportingService;

namespace Jaxis.DrinkInventory.Reporting.Web2.Controllers
{
    public class ReportController : BaseController
    {
        [UserSessionFilter] public ActionResult Index(Guid? _reportId)
        {
            return HandleAction(() =>
            {
                var model = new ReportModel();
                var reportsResult = (GetReportsResult)ServiceFactory.Reporting.WithService(_svc => _svc.GetReports(SessionId));
                model.Reports = reportsResult.Reports.ToList();
                model.SelectedReportId = _reportId ?? Guid.Empty;

                if (_reportId.HasValue)
                {
                    var reportResult = (GetReportResult)ServiceFactory.Reporting.WithService(
                        _svc => _svc.GetReport(SessionId, _reportId.Value));
                    model.ReportViewModel = GetReportViewModel(reportResult.Report);
                }

                return View(model);
            }, View());
        }

        private ReportViewModel GetReportViewModel(Data.POCO.Report _report)
        {
            var parameterValues = new List<object>();
            var parametersResult = (GetParametersResult)ServiceFactory.Reporting.WithService(
                _svc => _svc.GetParameters(SessionId, _report.ReportId));

            foreach (var parameter in parametersResult.Parameters)
            {
                if (string.Compare(parameter.Name, "SessionId", true) == 0)
                {
                    parameterValues.Add(SessionId);
                }
                else
                {
                    var inputId = "rp_" + _report.ShortName + "_" + parameter.Name;
                    var inputValue = (Request[inputId] ?? string.Empty).Trim();
                    parameterValues.Add(inputValue);
                }
            }

            var result = (GetDataTableResult)ServiceFactory.Reporting.WithService(_svc =>
                _svc.GetReportData(SessionId, _report.ReportId, parameterValues));

            var model = new ReportViewModel
            {
                ReportId = _report.ReportId,
                ReportName = _report.Name,
                ShortName = _report.ShortName,
                Parameters = parametersResult.Parameters.ToList(),
                XtraReport = ReportFactory.CreateReport(_report)
            };

            model.XtraReport.DataMember = "Table";
            var ds = new DataSet();
            ds.Tables.Add(result.Data);
            model.XtraReport.DataSource = ds;

            return model;
        }

        public ActionResult RefreshView(Guid? _reportId)
        {
            return HandleAction(() =>
            {
                // ReSharper disable PossibleInvalidOperationException
                var result = (GetReportResult)ServiceFactory.Reporting.WithService(
                    _svc => _svc.GetReport(SessionId, _reportId.Value));
                // ReSharper restore PossibleInvalidOperationException
                var reportViewModel = GetReportViewModel(result.Report);
                return PartialView("_ViewDataPartial", reportViewModel);
            }, Content("Could not retrieve data.  Please try reloading the page."));
        }

        public ActionResult ReportViewerExportTo(Guid? _reportId)
        {
            return HandleAction(() =>
            {
                // ReSharper disable PossibleInvalidOperationException
                var result = (GetReportResult)ServiceFactory.Reporting.WithService(
                    _svc => _svc.GetReport(SessionId, _reportId.Value));
                // ReSharper restore PossibleInvalidOperationException
                var reportViewModel = GetReportViewModel(result.Report);
                return ReportViewerExtension.ExportTo(reportViewModel.XtraReport);
            }, Content("Could not retrive data.  Please try reloading the page."));
        }
    }
}
