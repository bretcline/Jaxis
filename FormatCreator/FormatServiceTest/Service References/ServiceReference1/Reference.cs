﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3074
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FormatServiceTest.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TagData", Namespace="http://schemas.datacontract.org/2004/07/LFI.RFID.Format")]
    [System.SerializableAttribute()]
    public partial class TagData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private FormatServiceTest.ServiceReference1.DataRow[] DataRowsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid FormatIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private FormatServiceTest.ServiceReference1.DataRow HeaderRowField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public FormatServiceTest.ServiceReference1.DataRow[] DataRows {
            get {
                return this.DataRowsField;
            }
            set {
                if ((object.ReferenceEquals(this.DataRowsField, value) != true)) {
                    this.DataRowsField = value;
                    this.RaisePropertyChanged("DataRows");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid FormatID {
            get {
                return this.FormatIDField;
            }
            set {
                if ((this.FormatIDField.Equals(value) != true)) {
                    this.FormatIDField = value;
                    this.RaisePropertyChanged("FormatID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public FormatServiceTest.ServiceReference1.DataRow HeaderRow {
            get {
                return this.HeaderRowField;
            }
            set {
                if ((object.ReferenceEquals(this.HeaderRowField, value) != true)) {
                    this.HeaderRowField = value;
                    this.RaisePropertyChanged("HeaderRow");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DataRow", Namespace="http://schemas.datacontract.org/2004/07/LFI.RFID.Format")]
    [System.SerializableAttribute()]
    public partial class DataRow : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsLockedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.Dictionary<string, string> ValuesField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsLocked {
            get {
                return this.IsLockedField;
            }
            set {
                if ((this.IsLockedField.Equals(value) != true)) {
                    this.IsLockedField = value;
                    this.RaisePropertyChanged("IsLocked");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.Dictionary<string, string> Values {
            get {
                return this.ValuesField;
            }
            set {
                if ((object.ReferenceEquals(this.ValuesField, value) != true)) {
                    this.ValuesField = value;
                    this.RaisePropertyChanged("Values");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FormatDef", Namespace="http://schemas.datacontract.org/2004/07/LFI.RFID.Format")]
    [System.SerializableAttribute()]
    public partial class FormatDef : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private FormatServiceTest.ServiceReference1.DataRowDef DataRowDefField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private FormatServiceTest.ServiceReference1.DataRowDef HeaderRowDefField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int MaxDataRowsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public FormatServiceTest.ServiceReference1.DataRowDef DataRowDef {
            get {
                return this.DataRowDefField;
            }
            set {
                if ((object.ReferenceEquals(this.DataRowDefField, value) != true)) {
                    this.DataRowDefField = value;
                    this.RaisePropertyChanged("DataRowDef");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public FormatServiceTest.ServiceReference1.DataRowDef HeaderRowDef {
            get {
                return this.HeaderRowDefField;
            }
            set {
                if ((object.ReferenceEquals(this.HeaderRowDefField, value) != true)) {
                    this.HeaderRowDefField = value;
                    this.RaisePropertyChanged("HeaderRowDef");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int MaxDataRows {
            get {
                return this.MaxDataRowsField;
            }
            set {
                if ((this.MaxDataRowsField.Equals(value) != true)) {
                    this.MaxDataRowsField = value;
                    this.RaisePropertyChanged("MaxDataRows");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DataRowDef", Namespace="http://schemas.datacontract.org/2004/07/LFI.RFID.Format")]
    [System.SerializableAttribute()]
    public partial class DataRowDef : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private FormatServiceTest.ServiceReference1.DataElementDef[] ElementDefsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public FormatServiceTest.ServiceReference1.DataElementDef[] ElementDefs {
            get {
                return this.ElementDefsField;
            }
            set {
                if ((object.ReferenceEquals(this.ElementDefsField, value) != true)) {
                    this.ElementDefsField = value;
                    this.RaisePropertyChanged("ElementDefs");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DataElementDef", Namespace="http://schemas.datacontract.org/2004/07/LFI.RFID.Format")]
    [System.SerializableAttribute()]
    public partial class DataElementDef : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ConstraintsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private FormatServiceTest.ServiceReference1.DataType DataTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool RequiredField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Constraints {
            get {
                return this.ConstraintsField;
            }
            set {
                if ((object.ReferenceEquals(this.ConstraintsField, value) != true)) {
                    this.ConstraintsField = value;
                    this.RaisePropertyChanged("Constraints");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public FormatServiceTest.ServiceReference1.DataType DataType {
            get {
                return this.DataTypeField;
            }
            set {
                if ((this.DataTypeField.Equals(value) != true)) {
                    this.DataTypeField = value;
                    this.RaisePropertyChanged("DataType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Required {
            get {
                return this.RequiredField;
            }
            set {
                if ((this.RequiredField.Equals(value) != true)) {
                    this.RequiredField = value;
                    this.RaisePropertyChanged("Required");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DataType", Namespace="http://schemas.datacontract.org/2004/07/LFI.RFID.Format")]
    public enum DataType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Text = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        TextUnicode = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DateOnly = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        TimeOnly = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        DateTime = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PickList = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PickListUnicode = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PickListKeyValue = 7,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Bool = 8,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Double = 9,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Float = 10,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Int16 = 11,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Int32 = 12,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Guid = 13,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IFormatService")]
    public interface IFormatService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFormatService/PostTagData", ReplyAction="http://tempuri.org/IFormatService/PostTagDataResponse")]
        void PostTagData(FormatServiceTest.ServiceReference1.TagData data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFormatService/GetFormat", ReplyAction="http://tempuri.org/IFormatService/GetFormatResponse")]
        FormatServiceTest.ServiceReference1.FormatDef GetFormat(System.Guid formatID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFormatService/GetFormatAsString", ReplyAction="http://tempuri.org/IFormatService/GetFormatAsStringResponse")]
        string GetFormatAsString(System.Guid formatID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IFormatServiceChannel : FormatServiceTest.ServiceReference1.IFormatService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class FormatServiceClient : System.ServiceModel.ClientBase<FormatServiceTest.ServiceReference1.IFormatService>, FormatServiceTest.ServiceReference1.IFormatService {
        
        public FormatServiceClient() {
        }
        
        public FormatServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FormatServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FormatServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FormatServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void PostTagData(FormatServiceTest.ServiceReference1.TagData data) {
            base.Channel.PostTagData(data);
        }
        
        public FormatServiceTest.ServiceReference1.FormatDef GetFormat(System.Guid formatID) {
            return base.Channel.GetFormat(formatID);
        }
        
        public string GetFormatAsString(System.Guid formatID) {
            return base.Channel.GetFormatAsString(formatID);
        }
    }
}
