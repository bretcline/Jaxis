﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.235.
// 
#pragma warning disable 1591

namespace InSyncReports.svcContextService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ContextReportServiceWSServiceSoapBinding", Namespace="http://service.web.context.edgeware.insync.com/")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SOAPException))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(getReportDefinitionResponse))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(getReportDefinition))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(getReportPageCountResponse))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(getReportPageCount))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(queryResponse))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(query))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(DataComposer[]))]
    public partial class ContextReportServiceWSService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback queryOperationCompleted;
        
        private System.Threading.SendOrPostCallback getReportPageCountOperationCompleted;
        
        private System.Threading.SendOrPostCallback getReportDefinitionOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ContextReportServiceWSService() {
            this.Url = global::InSyncReports.Properties.Settings.Default.InSyncReports_svcContextService_ContextReportServiceWSService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event queryCompletedEventHandler queryCompleted;
        
        /// <remarks/>
        public event getReportPageCountCompletedEventHandler getReportPageCountCompleted;
        
        /// <remarks/>
        public event getReportDefinitionCompletedEventHandler getReportDefinitionCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://service.web.context.edgeware.insync.com/", ResponseNamespace="http://service.web.context.edgeware.insync.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlArrayAttribute("return", IsNullable=true)]
        [return: System.Xml.Serialization.XmlArrayItemAttribute(Namespace="http://util.common.insync.com")]
        public ResultDataComposer[] query([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string reportName, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string organizationName, [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)] [System.Xml.Serialization.XmlArrayItemAttribute("entry", IsNullable=false)] string2stringMapEntry[] parameters) {
            object[] results = this.Invoke("query", new object[] {
                        reportName,
                        organizationName,
                        parameters});
            return ((ResultDataComposer[])(results[0]));
        }
        
        /// <remarks/>
        public void queryAsync(string reportName, string organizationName, string2stringMapEntry[] parameters) {
            this.queryAsync(reportName, organizationName, parameters, null);
        }
        
        /// <remarks/>
        public void queryAsync(string reportName, string organizationName, string2stringMapEntry[] parameters, object userState) {
            if ((this.queryOperationCompleted == null)) {
                this.queryOperationCompleted = new System.Threading.SendOrPostCallback(this.OnqueryOperationCompleted);
            }
            this.InvokeAsync("query", new object[] {
                        reportName,
                        organizationName,
                        parameters}, this.queryOperationCompleted, userState);
        }
        
        private void OnqueryOperationCompleted(object arg) {
            if ((this.queryCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.queryCompleted(this, new queryCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://service.web.context.edgeware.insync.com/", ResponseNamespace="http://service.web.context.edgeware.insync.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", IsNullable=true)]
        public ResultDataComposer getReportPageCount([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string reportName, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string organizationName, [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)] [System.Xml.Serialization.XmlArrayItemAttribute("entry", IsNullable=false)] string2stringMapEntry[] parameters) {
            object[] results = this.Invoke("getReportPageCount", new object[] {
                        reportName,
                        organizationName,
                        parameters});
            return ((ResultDataComposer)(results[0]));
        }
        
        /// <remarks/>
        public void getReportPageCountAsync(string reportName, string organizationName, string2stringMapEntry[] parameters) {
            this.getReportPageCountAsync(reportName, organizationName, parameters, null);
        }
        
        /// <remarks/>
        public void getReportPageCountAsync(string reportName, string organizationName, string2stringMapEntry[] parameters, object userState) {
            if ((this.getReportPageCountOperationCompleted == null)) {
                this.getReportPageCountOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetReportPageCountOperationCompleted);
            }
            this.InvokeAsync("getReportPageCount", new object[] {
                        reportName,
                        organizationName,
                        parameters}, this.getReportPageCountOperationCompleted, userState);
        }
        
        private void OngetReportPageCountOperationCompleted(object arg) {
            if ((this.getReportPageCountCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getReportPageCountCompleted(this, new getReportPageCountCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace="http://service.web.context.edgeware.insync.com/", ResponseNamespace="http://service.web.context.edgeware.insync.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("return", IsNullable=true)]
        public ReportDefinitionComposer getReportDefinition([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string reportName, [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] string organizationName) {
            object[] results = this.Invoke("getReportDefinition", new object[] {
                        reportName,
                        organizationName});
            return ((ReportDefinitionComposer)(results[0]));
        }
        
        /// <remarks/>
        public void getReportDefinitionAsync(string reportName, string organizationName) {
            this.getReportDefinitionAsync(reportName, organizationName, null);
        }
        
        /// <remarks/>
        public void getReportDefinitionAsync(string reportName, string organizationName, object userState) {
            if ((this.getReportDefinitionOperationCompleted == null)) {
                this.getReportDefinitionOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetReportDefinitionOperationCompleted);
            }
            this.InvokeAsync("getReportDefinition", new object[] {
                        reportName,
                        organizationName}, this.getReportDefinitionOperationCompleted, userState);
        }
        
        private void OngetReportDefinitionOperationCompleted(object arg) {
            if ((this.getReportDefinitionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getReportDefinitionCompleted(this, new getReportDefinitionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://service.web.context.edgeware.insync.com/")]
    public partial class string2stringMapEntry {
        
        private string keyField;
        
        private string valueField;
        
        /// <remarks/>
        public string key {
            get {
                return this.keyField;
            }
            set {
                this.keyField = value;
            }
        }
        
        /// <remarks/>
        public string value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://soap.xml.javax")]
    public partial class SOAPException {
        
        private Throwable causeField;
        
        private string messageField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public Throwable cause {
            get {
                return this.causeField;
            }
            set {
                this.causeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://lang.java")]
    public partial class Throwable {
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.web.context.edgeware.insync.com")]
    public partial class ReportParameterComposer {
        
        private string dataTypeField;
        
        private string fieldNameField;
        
        private string opnd1Field;
        
        private string opnd2Field;
        
        private string ordinalField;
        
        private string paramTypeField;
        
        private string tableAliasField;
        
        private string tableNameField;
        
        private string val1Field;
        
        private string val2Field;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string dataType {
            get {
                return this.dataTypeField;
            }
            set {
                this.dataTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string fieldName {
            get {
                return this.fieldNameField;
            }
            set {
                this.fieldNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string opnd1 {
            get {
                return this.opnd1Field;
            }
            set {
                this.opnd1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string opnd2 {
            get {
                return this.opnd2Field;
            }
            set {
                this.opnd2Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string ordinal {
            get {
                return this.ordinalField;
            }
            set {
                this.ordinalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string paramType {
            get {
                return this.paramTypeField;
            }
            set {
                this.paramTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string tableAlias {
            get {
                return this.tableAliasField;
            }
            set {
                this.tableAliasField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string tableName {
            get {
                return this.tableNameField;
            }
            set {
                this.tableNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string val1 {
            get {
                return this.val1Field;
            }
            set {
                this.val1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string val2 {
            get {
                return this.val2Field;
            }
            set {
                this.val2Field = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.web.context.edgeware.insync.com")]
    public partial class ReportFieldComposer {
        
        private string columnNameField;
        
        private string dataFormatField;
        
        private string dataTypeField;
        
        private string displayNameField;
        
        private string fieldNameField;
        
        private bool hiddenField;
        
        private bool hiddenFieldSpecified;
        
        private string ordinalField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string columnName {
            get {
                return this.columnNameField;
            }
            set {
                this.columnNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string dataFormat {
            get {
                return this.dataFormatField;
            }
            set {
                this.dataFormatField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string dataType {
            get {
                return this.dataTypeField;
            }
            set {
                this.dataTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string displayName {
            get {
                return this.displayNameField;
            }
            set {
                this.displayNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string fieldName {
            get {
                return this.fieldNameField;
            }
            set {
                this.fieldNameField = value;
            }
        }
        
        /// <remarks/>
        public bool hidden {
            get {
                return this.hiddenField;
            }
            set {
                this.hiddenField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool hiddenSpecified {
            get {
                return this.hiddenFieldSpecified;
            }
            set {
                this.hiddenFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string ordinal {
            get {
                return this.ordinalField;
            }
            set {
                this.ordinalField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.web.context.edgeware.insync.com")]
    public partial class ReportDefinitionComposer {
        
        private string categoryField;
        
        private string chartTypeField;
        
        private string displayNameField;
        
        private ReportFieldComposer[] fieldsField;
        
        private string lastRunByField;
        
        private string lastRunStringTimeField;
        
        private string majorVersionField;
        
        private string minorVersionField;
        
        private ReportParameterComposer[] parametersField;
        
        private System.Nullable<decimal> reportKeyField;
        
        private bool reportKeyFieldSpecified;
        
        private string reportNameField;
        
        private string reportTypeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string category {
            get {
                return this.categoryField;
            }
            set {
                this.categoryField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string chartType {
            get {
                return this.chartTypeField;
            }
            set {
                this.chartTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string displayName {
            get {
                return this.displayNameField;
            }
            set {
                this.displayNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        public ReportFieldComposer[] fields {
            get {
                return this.fieldsField;
            }
            set {
                this.fieldsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string lastRunBy {
            get {
                return this.lastRunByField;
            }
            set {
                this.lastRunByField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string lastRunStringTime {
            get {
                return this.lastRunStringTimeField;
            }
            set {
                this.lastRunStringTimeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string majorVersion {
            get {
                return this.majorVersionField;
            }
            set {
                this.majorVersionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string minorVersion {
            get {
                return this.minorVersionField;
            }
            set {
                this.minorVersionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        public ReportParameterComposer[] parameters {
            get {
                return this.parametersField;
            }
            set {
                this.parametersField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Nullable<decimal> reportKey {
            get {
                return this.reportKeyField;
            }
            set {
                this.reportKeyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool reportKeySpecified {
            get {
                return this.reportKeyFieldSpecified;
            }
            set {
                this.reportKeyFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string reportName {
            get {
                return this.reportNameField;
            }
            set {
                this.reportNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string reportType {
            get {
                return this.reportTypeField;
            }
            set {
                this.reportTypeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.web.context.edgeware.insync.com/")]
    public partial class getReportDefinitionResponse {
        
        private ReportDefinitionComposer returnField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public ReportDefinitionComposer @return {
            get {
                return this.returnField;
            }
            set {
                this.returnField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.web.context.edgeware.insync.com/")]
    public partial class getReportDefinition {
        
        private string reportNameField;
        
        private string organizationNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string reportName {
            get {
                return this.reportNameField;
            }
            set {
                this.reportNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string organizationName {
            get {
                return this.organizationNameField;
            }
            set {
                this.organizationNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.web.context.edgeware.insync.com/")]
    public partial class getReportPageCountResponse {
        
        private ResultDataComposer returnField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public ResultDataComposer @return {
            get {
                return this.returnField;
            }
            set {
                this.returnField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://util.common.insync.com")]
    public partial class ResultDataComposer {
        
        private DataComposer[] dataListField;
        
        private string keyField;
        
        private string levelField;
        
        private string nameField;
        
        private string parentField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        public DataComposer[] dataList {
            get {
                return this.dataListField;
            }
            set {
                this.dataListField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string key {
            get {
                return this.keyField;
            }
            set {
                this.keyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string level {
            get {
                return this.levelField;
            }
            set {
                this.levelField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string parent {
            get {
                return this.parentField;
            }
            set {
                this.parentField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://util.common.insync.com")]
    public partial class DataComposer {
        
        private string nameField;
        
        private object valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public object value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.web.context.edgeware.insync.com/")]
    public partial class getReportPageCount {
        
        private string reportNameField;
        
        private string organizationNameField;
        
        private string2stringMapEntry[] parametersField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string reportName {
            get {
                return this.reportNameField;
            }
            set {
                this.reportNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string organizationName {
            get {
                return this.organizationNameField;
            }
            set {
                this.organizationNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        [System.Xml.Serialization.XmlArrayItemAttribute("entry", IsNullable=false)]
        public string2stringMapEntry[] parameters {
            get {
                return this.parametersField;
            }
            set {
                this.parametersField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.web.context.edgeware.insync.com/")]
    public partial class queryResponse {
        
        private ResultDataComposer[] returnField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        [System.Xml.Serialization.XmlArrayItemAttribute(Namespace="http://util.common.insync.com")]
        public ResultDataComposer[] @return {
            get {
                return this.returnField;
            }
            set {
                this.returnField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.web.context.edgeware.insync.com/")]
    public partial class query {
        
        private string reportNameField;
        
        private string organizationNameField;
        
        private string2stringMapEntry[] parametersField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string reportName {
            get {
                return this.reportNameField;
            }
            set {
                this.reportNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string organizationName {
            get {
                return this.organizationNameField;
            }
            set {
                this.organizationNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        [System.Xml.Serialization.XmlArrayItemAttribute("entry", IsNullable=false)]
        public string2stringMapEntry[] parameters {
            get {
                return this.parametersField;
            }
            set {
                this.parametersField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void queryCompletedEventHandler(object sender, queryCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class queryCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal queryCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ResultDataComposer[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ResultDataComposer[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void getReportPageCountCompletedEventHandler(object sender, getReportPageCountCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getReportPageCountCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getReportPageCountCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ResultDataComposer Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ResultDataComposer)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void getReportDefinitionCompletedEventHandler(object sender, getReportDefinitionCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getReportDefinitionCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getReportDefinitionCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ReportDefinitionComposer Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ReportDefinitionComposer)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591