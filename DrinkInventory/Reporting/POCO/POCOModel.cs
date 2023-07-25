//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Jaxis.DrinkInventory.Reporting.DataInterfaces;


namespace Jaxis.DrinkInventory.Reporting.Data.POCO
{
    
    
    [Serializable()]
    [DataContract(IsReference=true)]
    public partial class Alert : IAlert
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Alert object.
        /// </summary>
        /// <param name="alertId">Initial value of the AlertId property.</param>
        /// <param name="message">Initial value of the Message property.</param>
        /// <param name="alertTime">Initial value of the AlertTime property.</param>
        /// <param name="alertType">Initial value of the AlertType property.</param>
        /// <param name="organizationId">Initial value of the OrganizationId property.</param>
        /// <param name="severity">Initial value of the Severity property.</param>
        /// <param name="modifiedOn">Initial value of the ModifiedOn property.</param>
        public static Alert CreateAlert(global::System.Guid alertId, global::System.String message, global::System.DateTime alertTime, global::System.Int32 alertType, global::System.Guid organizationId, global::System.Int32 severity, global::System.DateTime modifiedOn)
        {
            Alert alert = new Alert();
            alert.AlertId = alertId;
            alert.Message = message;
            alert.AlertTime = alertTime;
            alert.AlertType = alertType;
            alert.OrganizationId = organizationId;
            alert.Severity = severity;
            alert.ModifiedOn = modifiedOn;
            return alert;
        }

        #endregion
        [DataMember]
        public Guid AlertId { get; set; }
        [DataMember]
        public DateTime AlertTime { get; set; }
        [DataMember]
        public Int32 AlertType { get; set; }
        [DataMember]
        public String LocationName { get; set; }
        [DataMember]
        public String Message { get; set; }
        [DataMember]
        public DateTime ModifiedOn { get; set; }
        [DataMember]
        public Guid OrganizationId { get; set; }
        [DataMember]
        public Int32 Severity { get; set; }
    
    }
    
    [Serializable()]
    [DataContract(IsReference=true)]
    public partial class Area : IArea
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Area object.
        /// </summary>
        /// <param name="areaId">Initial value of the AreaId property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="modifiedOn">Initial value of the ModifiedOn property.</param>
        /// <param name="order">Initial value of the Order property.</param>
        /// <param name="shortName">Initial value of the ShortName property.</param>
        /// <param name="controller">Initial value of the Controller property.</param>
        public static Area CreateArea(global::System.Guid areaId, global::System.String name, global::System.DateTime modifiedOn, global::System.Int32 order, global::System.String shortName, global::System.String controller)
        {
            Area area = new Area();
            area.AreaId = areaId;
            area.Name = name;
            area.ModifiedOn = modifiedOn;
            area.Order = order;
            area.ShortName = shortName;
            area.Controller = controller;
            return area;
        }

        #endregion
        [DataMember]
        public Guid AreaId { get; set; }
        [DataMember]
        public String Controller { get; set; }
        [DataMember]
        public DateTime ModifiedOn { get; set; }
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        public Int32 Order { get; set; }
        [DataMember]
        public String ShortName { get; set; }
    
    #region Navigation Properties
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public List<AreaMembership> AreaMemberships
        {
    		get; set;
        }

    #endregion
    }
    
    [Serializable()]
    [DataContract(IsReference=true)]
    public partial class AreaMembership : IAreaMembership
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new AreaMembership object.
        /// </summary>
        /// <param name="areaMembershipId">Initial value of the AreaMembershipId property.</param>
        /// <param name="areaId">Initial value of the AreaId property.</param>
        /// <param name="userGroupId">Initial value of the UserGroupId property.</param>
        /// <param name="modifiedOn">Initial value of the ModifiedOn property.</param>
        public static AreaMembership CreateAreaMembership(global::System.Guid areaMembershipId, global::System.Guid areaId, global::System.Guid userGroupId, global::System.DateTime modifiedOn)
        {
            AreaMembership areaMembership = new AreaMembership();
            areaMembership.AreaMembershipId = areaMembershipId;
            areaMembership.AreaId = areaId;
            areaMembership.UserGroupId = userGroupId;
            areaMembership.ModifiedOn = modifiedOn;
            return areaMembership;
        }

        #endregion
        [DataMember]
        public Guid AreaId { get; set; }
        [DataMember]
        public Guid AreaMembershipId { get; set; }
        [DataMember]
        public String Description { get; set; }
        [DataMember]
        public DateTime ModifiedOn { get; set; }
        [DataMember]
        public String ShortName { get; set; }
        [DataMember]
        public Guid UserGroupId { get; set; }
    
    #region Navigation Properties
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public Area Area
        {
    		get; set;
        }
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public UserGroup UserGroup
        {
    		get; set;
        }

    #endregion
    }
    
    [Serializable()]
    [DataContract(IsReference=true)]
    public partial class Column : IColumn
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Column object.
        /// </summary>
        /// <param name="columnId">Initial value of the ColumnId property.</param>
        /// <param name="modifiedOn">Initial value of the ModifiedOn property.</param>
        /// <param name="displayName">Initial value of the DisplayName property.</param>
        public static Column CreateColumn(global::System.Guid columnId, global::System.DateTime modifiedOn, global::System.String displayName)
        {
            Column column = new Column();
            column.ColumnId = columnId;
            column.ModifiedOn = modifiedOn;
            column.DisplayName = displayName;
            return column;
        }

        #endregion
        [DataMember]
        public Guid ColumnId { get; set; }
        [DataMember]
        public String DisplayName { get; set; }
        [DataMember]
        public DateTime ModifiedOn { get; set; }
        [DataMember]
        public Guid? ReportId { get; set; }
    
    #region Navigation Properties
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public Report NavReport
        {
    		get; set;
        }

    #endregion
    }
    
    [Serializable()]
    [DataContract(IsReference=true)]
    public partial class Device : IDevice
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Device object.
        /// </summary>
        /// <param name="deviceId">Initial value of the DeviceId property.</param>
        /// <param name="deviceNumber">Initial value of the DeviceNumber property.</param>
        /// <param name="organizationId">Initial value of the OrganizationId property.</param>
        /// <param name="status">Initial value of the Status property.</param>
        /// <param name="modifiedOn">Initial value of the ModifiedOn property.</param>
        /// <param name="locationName">Initial value of the LocationName property.</param>
        public static Device CreateDevice(global::System.Guid deviceId, global::System.String deviceNumber, global::System.Guid organizationId, global::System.Int32 status, global::System.DateTime modifiedOn, global::System.String locationName)
        {
            Device device = new Device();
            device.DeviceId = deviceId;
            device.DeviceNumber = deviceNumber;
            device.OrganizationId = organizationId;
            device.Status = status;
            device.ModifiedOn = modifiedOn;
            device.LocationName = locationName;
            return device;
        }

        #endregion
        [DataMember]
        public Guid DeviceId { get; set; }
        [DataMember]
        public String DeviceNumber { get; set; }
        [DataMember]
        public String LocationName { get; set; }
        [DataMember]
        public DateTime ModifiedOn { get; set; }
        [DataMember]
        public Guid OrganizationId { get; set; }
        [DataMember]
        public Int32 Status { get; set; }
    
    }
    
    [Serializable()]
    [DataContract(IsReference=true)]
    public partial class Organization : IOrganization
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Organization object.
        /// </summary>
        /// <param name="organizationId">Initial value of the OrganizationId property.</param>
        /// <param name="modifiedOn">Initial value of the ModifiedOn property.</param>
        /// <param name="shortName">Initial value of the ShortName property.</param>
        public static Organization CreateOrganization(global::System.Guid organizationId, global::System.DateTime modifiedOn, global::System.String shortName)
        {
            Organization organization = new Organization();
            organization.OrganizationId = organizationId;
            organization.ModifiedOn = modifiedOn;
            organization.ShortName = shortName;
            return organization;
        }

        #endregion
        [DataMember]
        public String Description { get; set; }
        [DataMember]
        public DateTime ModifiedOn { get; set; }
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        public Guid OrganizationId { get; set; }
        [DataMember]
        public Guid? ParentId { get; set; }
        [DataMember]
        public String Path { get; set; }
        [DataMember]
        public String ShortName { get; set; }
    
    #region Navigation Properties
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public List<Organization> NavChildren
        {
    		get; set;
        }
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public Organization NavParent
        {
    		get; set;
        }
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public List<UserGroup> NavUserGroups
        {
    		get; set;
        }
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public List<User> NavUsers
        {
    		get; set;
        }
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public List<UserGroupXOrganization> UserGroupXOrganizations
        {
    		get; set;
        }

    #endregion
    }
    
    [Serializable()]
    [DataContract(IsReference=true)]
    public partial class Parameter : IParameter
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Parameter object.
        /// </summary>
        /// <param name="parameterId">Initial value of the ParameterId property.</param>
        /// <param name="modifiedOn">Initial value of the ModifiedOn property.</param>
        /// <param name="type">Initial value of the Type property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="order">Initial value of the Order property.</param>
        /// <param name="reportId">Initial value of the ReportId property.</param>
        public static Parameter CreateParameter(global::System.Guid parameterId, global::System.DateTime modifiedOn, global::System.String type, global::System.String name, global::System.Int32 order, global::System.Guid reportId)
        {
            Parameter parameter = new Parameter();
            parameter.ParameterId = parameterId;
            parameter.ModifiedOn = modifiedOn;
            parameter.Type = type;
            parameter.Name = name;
            parameter.Order = order;
            parameter.ReportId = reportId;
            return parameter;
        }

        #endregion
        [DataMember]
        public String Label { get; set; }
        [DataMember]
        public DateTime ModifiedOn { get; set; }
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        public Int32 Order { get; set; }
        [DataMember]
        public Guid ParameterId { get; set; }
        [DataMember]
        public Guid ReportId { get; set; }
        [DataMember]
        public String Type { get; set; }
    
    #region Navigation Properties
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public Report NavReport
        {
    		get; set;
        }

    #endregion
    }
    
    [Serializable()]
    [DataContract(IsReference=true)]
    public partial class POSTicketItem : IPOSTicketItem
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new POSTicketItem object.
        /// </summary>
        /// <param name="pOSTicketItemId">Initial value of the POSTicketItemId property.</param>
        /// <param name="checkNumber">Initial value of the CheckNumber property.</param>
        /// <param name="description">Initial value of the Description property.</param>
        /// <param name="quantity">Initial value of the Quantity property.</param>
        /// <param name="locationName">Initial value of the LocationName property.</param>
        /// <param name="establishment">Initial value of the Establishment property.</param>
        /// <param name="ticketDate">Initial value of the TicketDate property.</param>
        /// <param name="organizationId">Initial value of the OrganizationId property.</param>
        /// <param name="modifiedOn">Initial value of the ModifiedOn property.</param>
        /// <param name="status">Initial value of the Status property.</param>
        public static POSTicketItem CreatePOSTicketItem(global::System.Guid pOSTicketItemId, global::System.String checkNumber, global::System.String description, global::System.Int32 quantity, global::System.String locationName, global::System.String establishment, global::System.DateTime ticketDate, global::System.Guid organizationId, global::System.DateTime modifiedOn, global::System.Int32 status)
        {
            POSTicketItem pOSTicketItem = new POSTicketItem();
            pOSTicketItem.POSTicketItemId = pOSTicketItemId;
            pOSTicketItem.CheckNumber = checkNumber;
            pOSTicketItem.Description = description;
            pOSTicketItem.Quantity = quantity;
            pOSTicketItem.LocationName = locationName;
            pOSTicketItem.Establishment = establishment;
            pOSTicketItem.TicketDate = ticketDate;
            pOSTicketItem.OrganizationId = organizationId;
            pOSTicketItem.ModifiedOn = modifiedOn;
            pOSTicketItem.Status = status;
            return pOSTicketItem;
        }

        #endregion
        [DataMember]
        public String CheckNumber { get; set; }
        [DataMember]
        public String Comment { get; set; }
        [DataMember]
        public String Description { get; set; }
        [DataMember]
        public String Establishment { get; set; }
        [DataMember]
        public String LocationName { get; set; }
        [DataMember]
        public DateTime ModifiedOn { get; set; }
        [DataMember]
        public Guid OrganizationId { get; set; }
        [DataMember]
        public Guid POSTicketItemId { get; set; }
        [DataMember]
        public Decimal? Price { get; set; }
        [DataMember]
        public Int32 Quantity { get; set; }
        [DataMember]
        public Int32 Status { get; set; }
        [DataMember]
        public DateTime TicketDate { get; set; }
    
    #region Navigation Properties
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public List<Pour> NavPours
        {
    		get; set;
        }

    #endregion
    }
    
    [Serializable()]
    [DataContract(IsReference=true)]
    public partial class Pour : IPour
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Pour object.
        /// </summary>
        /// <param name="pourId">Initial value of the PourId property.</param>
        /// <param name="modifiedOn">Initial value of the ModifiedOn property.</param>
        /// <param name="pourTime">Initial value of the PourTime property.</param>
        /// <param name="tagNumber">Initial value of the TagNumber property.</param>
        /// <param name="deviceNumber">Initial value of the DeviceNumber property.</param>
        /// <param name="volume">Initial value of the Volume property.</param>
        /// <param name="duration">Initial value of the Duration property.</param>
        /// <param name="amountLeft">Initial value of the AmountLeft property.</param>
        /// <param name="temperature">Initial value of the Temperature property.</param>
        /// <param name="batteryVoltage">Initial value of the BatteryVoltage property.</param>
        /// <param name="itemNumber">Initial value of the ItemNumber property.</param>
        /// <param name="locationName">Initial value of the LocationName property.</param>
        /// <param name="organizationId">Initial value of the OrganizationId property.</param>
        /// <param name="status">Initial value of the Status property.</param>
        /// <param name="cost">Initial value of the Cost property.</param>
        public static Pour CreatePour(global::System.Guid pourId, global::System.DateTime modifiedOn, global::System.DateTime pourTime, global::System.String tagNumber, global::System.String deviceNumber, global::System.Double volume, global::System.Double duration, global::System.Double amountLeft, global::System.Double temperature, global::System.Double batteryVoltage, global::System.String itemNumber, global::System.String locationName, global::System.Guid organizationId, global::System.Int32 status, global::System.Decimal cost)
        {
            Pour pour = new Pour();
            pour.PourId = pourId;
            pour.ModifiedOn = modifiedOn;
            pour.PourTime = pourTime;
            pour.TagNumber = tagNumber;
            pour.DeviceNumber = deviceNumber;
            pour.Volume = volume;
            pour.Duration = duration;
            pour.AmountLeft = amountLeft;
            pour.Temperature = temperature;
            pour.BatteryVoltage = batteryVoltage;
            pour.ItemNumber = itemNumber;
            pour.LocationName = locationName;
            pour.OrganizationId = organizationId;
            pour.Status = status;
            pour.Cost = cost;
            return pour;
        }

        #endregion
        [DataMember]
        public Double AmountLeft { get; set; }
        [DataMember]
        public Double BatteryVoltage { get; set; }
        [DataMember]
        public Decimal Cost { get; set; }
        [DataMember]
        public String DeviceNumber { get; set; }
        [DataMember]
        public Double Duration { get; set; }
        [DataMember]
        public String ItemNumber { get; set; }
        [DataMember]
        public String LocationName { get; set; }
        [DataMember]
        public DateTime ModifiedOn { get; set; }
        [DataMember]
        public Guid OrganizationId { get; set; }
        [DataMember]
        public Guid? POSTicketItemId { get; set; }
        [DataMember]
        public Guid PourId { get; set; }
        [DataMember]
        public DateTime PourTime { get; set; }
        [DataMember]
        public String RawData { get; set; }
        [DataMember]
        public Int32 Status { get; set; }
        [DataMember]
        public String TagNumber { get; set; }
        [DataMember]
        public Double Temperature { get; set; }
        [DataMember]
        public Double Volume { get; set; }
    
    #region Navigation Properties
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public POSTicketItem NavPOSTicketItem
        {
    		get; set;
        }

    #endregion
    }
    
    [Serializable()]
    [DataContract(IsReference=true)]
    public partial class Report : IReport
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Report object.
        /// </summary>
        /// <param name="reportId">Initial value of the ReportId property.</param>
        /// <param name="modifiedOn">Initial value of the ModifiedOn property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="order">Initial value of the Order property.</param>
        /// <param name="type">Initial value of the Type property.</param>
        /// <param name="selectCommand">Initial value of the SelectCommand property.</param>
        /// <param name="shortName">Initial value of the ShortName property.</param>
        /// <param name="reportClassName">Initial value of the ReportClassName property.</param>
        public static Report CreateReport(global::System.Guid reportId, global::System.DateTime modifiedOn, global::System.String name, global::System.Int32 order, global::System.String type, global::System.String selectCommand, global::System.String shortName, global::System.String reportClassName)
        {
            Report report = new Report();
            report.ReportId = reportId;
            report.ModifiedOn = modifiedOn;
            report.Name = name;
            report.Order = order;
            report.Type = type;
            report.SelectCommand = selectCommand;
            report.ShortName = shortName;
            report.ReportClassName = reportClassName;
            return report;
        }

        #endregion
        [DataMember]
        public DateTime ModifiedOn { get; set; }
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        public Int32 Order { get; set; }
        [DataMember]
        public String ReportClassName { get; set; }
        [DataMember]
        public Guid ReportId { get; set; }
        [DataMember]
        public String SelectCommand { get; set; }
        [DataMember]
        public String ShortName { get; set; }
        [DataMember]
        public String Type { get; set; }
    
    #region Navigation Properties
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public List<Column> NavColumns
        {
    		get; set;
        }
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public List<Parameter> NavParameters
        {
    		get; set;
        }

    #endregion
    }
    
    [Serializable()]
    [DataContract(IsReference=true)]
    public partial class Schema : ISchema
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Schema object.
        /// </summary>
        /// <param name="schemaId">Initial value of the SchemaId property.</param>
        /// <param name="modifiedOn">Initial value of the ModifiedOn property.</param>
        public static Schema CreateSchema(global::System.Guid schemaId, global::System.DateTime modifiedOn)
        {
            Schema schema = new Schema();
            schema.SchemaId = schemaId;
            schema.ModifiedOn = modifiedOn;
            return schema;
        }

        #endregion
        [DataMember]
        public DateTime ModifiedOn { get; set; }
        [DataMember]
        public Guid SchemaId { get; set; }
        [DataMember]
        public Int32? Version { get; set; }
    
    }
    
    [Serializable()]
    [DataContract(IsReference=true)]
    public partial class SecurityView : ISecurityView
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new SecurityView object.
        /// </summary>
        /// <param name="organizationId">Initial value of the OrganizationId property.</param>
        /// <param name="sessionId">Initial value of the SessionId property.</param>
        public static SecurityView CreateSecurityView(global::System.Guid organizationId, global::System.Guid sessionId)
        {
            SecurityView securityView = new SecurityView();
            securityView.OrganizationId = organizationId;
            securityView.SessionId = sessionId;
            return securityView;
        }

        #endregion
        [DataMember]
        public Guid OrganizationId { get; set; }
        [DataMember]
        public Guid SessionId { get; set; }
    
    }
    
    [Serializable()]
    [DataContract(IsReference=true)]
    public partial class Session : ISession
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Session object.
        /// </summary>
        /// <param name="sessionId">Initial value of the SessionId property.</param>
        /// <param name="modifiedOn">Initial value of the ModifiedOn property.</param>
        /// <param name="userId">Initial value of the UserId property.</param>
        /// <param name="expirationTime">Initial value of the ExpirationTime property.</param>
        public static Session CreateSession(global::System.Guid sessionId, global::System.DateTime modifiedOn, global::System.Guid userId, global::System.DateTime expirationTime)
        {
            Session session = new Session();
            session.SessionId = sessionId;
            session.ModifiedOn = modifiedOn;
            session.UserId = userId;
            session.ExpirationTime = expirationTime;
            return session;
        }

        #endregion
        [DataMember]
        public DateTime ExpirationTime { get; set; }
        [DataMember]
        public DateTime ModifiedOn { get; set; }
        [DataMember]
        public Guid SessionId { get; set; }
        [DataMember]
        public Guid UserId { get; set; }
    
    #region Navigation Properties
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public User NavUser
        {
    		get; set;
        }

    #endregion
    }
    
    [Serializable()]
    [DataContract(IsReference=true)]
    public partial class UPC : IUPC
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new UPC object.
        /// </summary>
        /// <param name="uPCId">Initial value of the UPCId property.</param>
        /// <param name="itemNumber">Initial value of the ItemNumber property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="size">Initial value of the Size property.</param>
        /// <param name="sizeLabel">Initial value of the SizeLabel property.</param>
        /// <param name="categoryName">Initial value of the CategoryName property.</param>
        /// <param name="rootCategoryName">Initial value of the RootCategoryName property.</param>
        /// <param name="validated">Initial value of the Validated property.</param>
        /// <param name="manufacturerName">Initial value of the ManufacturerName property.</param>
        /// <param name="modifiedOn">Initial value of the ModifiedOn property.</param>
        public static UPC CreateUPC(global::System.Guid uPCId, global::System.String itemNumber, global::System.String name, global::System.Int32 size, global::System.String sizeLabel, global::System.String categoryName, global::System.String rootCategoryName, global::System.Boolean validated, global::System.String manufacturerName, global::System.DateTime modifiedOn)
        {
            UPC uPC = new UPC();
            uPC.UPCId = uPCId;
            uPC.ItemNumber = itemNumber;
            uPC.Name = name;
            uPC.Size = size;
            uPC.SizeLabel = sizeLabel;
            uPC.CategoryName = categoryName;
            uPC.RootCategoryName = rootCategoryName;
            uPC.Validated = validated;
            uPC.ManufacturerName = manufacturerName;
            uPC.ModifiedOn = modifiedOn;
            return uPC;
        }

        #endregion
        [DataMember]
        public Int32? BottleCount { get; set; }
        [DataMember]
        public String CategoryName { get; set; }
        [DataMember]
        public String ChildItemNumber { get; set; }
        [DataMember]
        public String ItemNumber { get; set; }
        [DataMember]
        public String ManufacturerName { get; set; }
        [DataMember]
        public DateTime ModifiedOn { get; set; }
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        public String RootCategoryName { get; set; }
        [DataMember]
        public Int32 Size { get; set; }
        [DataMember]
        public String SizeLabel { get; set; }
        [DataMember]
        public Guid UPCId { get; set; }
        [DataMember]
        public Boolean Validated { get; set; }
    
    }
    
    [Serializable()]
    [DataContract(IsReference=true)]
    public partial class User : IUser
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new User object.
        /// </summary>
        /// <param name="userId">Initial value of the UserId property.</param>
        /// <param name="userName">Initial value of the UserName property.</param>
        /// <param name="password">Initial value of the Password property.</param>
        /// <param name="modifiedOn">Initial value of the ModifiedOn property.</param>
        /// <param name="visibleWidgetIds">Initial value of the VisibleWidgetIds property.</param>
        /// <param name="organizationId">Initial value of the OrganizationId property.</param>
        public static User CreateUser(global::System.Guid userId, global::System.String userName, global::System.String password, global::System.DateTime modifiedOn, global::System.String visibleWidgetIds, global::System.Guid organizationId)
        {
            User user = new User();
            user.UserId = userId;
            user.UserName = userName;
            user.Password = password;
            user.ModifiedOn = modifiedOn;
            user.VisibleWidgetIds = visibleWidgetIds;
            user.OrganizationId = organizationId;
            return user;
        }

        #endregion
        [DataMember]
        public DateTime ModifiedOn { get; set; }
        [DataMember]
        public Guid OrganizationId { get; set; }
        [DataMember]
        public String Password { get; set; }
        [DataMember]
        public Guid UserId { get; set; }
        [DataMember]
        public String UserName { get; set; }
        [DataMember]
        public String VisibleWidgetIds { get; set; }
    
    #region Navigation Properties
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public Organization NavOrganization
        {
    		get; set;
        }
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public List<Session> NavSessions
        {
    		get; set;
        }
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public List<UserGroupMembership> NavUserGroupMemberships
        {
    		get; set;
        }

    #endregion
    }
    
    [Serializable()]
    [DataContract(IsReference=true)]
    public partial class UserGroup : IUserGroup
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new UserGroup object.
        /// </summary>
        /// <param name="userGroupId">Initial value of the UserGroupId property.</param>
        /// <param name="modifiedOn">Initial value of the ModifiedOn property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="organizationId">Initial value of the OrganizationId property.</param>
        public static UserGroup CreateUserGroup(global::System.Guid userGroupId, global::System.DateTime modifiedOn, global::System.String name, global::System.Guid organizationId)
        {
            UserGroup userGroup = new UserGroup();
            userGroup.UserGroupId = userGroupId;
            userGroup.ModifiedOn = modifiedOn;
            userGroup.Name = name;
            userGroup.OrganizationId = organizationId;
            return userGroup;
        }

        #endregion
        [DataMember]
        public DateTime ModifiedOn { get; set; }
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        public Guid OrganizationId { get; set; }
        [DataMember]
        public Guid UserGroupId { get; set; }
    
    #region Navigation Properties
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public List<AreaMembership> AreaMemberships
        {
    		get; set;
        }
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public Organization NavOrganization
        {
    		get; set;
        }
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public List<UserGroupMembership> UserGroupMemberships
        {
    		get; set;
        }
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public List<UserGroupXOrganization> UserGroupXOrganizations
        {
    		get; set;
        }

    #endregion
    }
    
    [Serializable()]
    [DataContract(IsReference=true)]
    public partial class UserGroupMembership : IUserGroupMembership
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new UserGroupMembership object.
        /// </summary>
        /// <param name="userGroupMembershipId">Initial value of the UserGroupMembershipId property.</param>
        /// <param name="modifiedOn">Initial value of the ModifiedOn property.</param>
        /// <param name="userId">Initial value of the UserId property.</param>
        /// <param name="userGroupId">Initial value of the UserGroupId property.</param>
        public static UserGroupMembership CreateUserGroupMembership(global::System.Guid userGroupMembershipId, global::System.DateTime modifiedOn, global::System.Guid userId, global::System.Guid userGroupId)
        {
            UserGroupMembership userGroupMembership = new UserGroupMembership();
            userGroupMembership.UserGroupMembershipId = userGroupMembershipId;
            userGroupMembership.ModifiedOn = modifiedOn;
            userGroupMembership.UserId = userId;
            userGroupMembership.UserGroupId = userGroupId;
            return userGroupMembership;
        }

        #endregion
        [DataMember]
        public DateTime ModifiedOn { get; set; }
        [DataMember]
        public Guid UserGroupId { get; set; }
        [DataMember]
        public Guid UserGroupMembershipId { get; set; }
        [DataMember]
        public Guid UserId { get; set; }
    
    #region Navigation Properties
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public User User
        {
    		get; set;
        }
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public UserGroup UserGroup
        {
    		get; set;
        }

    #endregion
    }
    
    [Serializable()]
    [DataContract(IsReference=true)]
    public partial class UserGroupXOrganization : IUserGroupXOrganization
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new UserGroupXOrganization object.
        /// </summary>
        /// <param name="userGroupXOrganizationId">Initial value of the UserGroupXOrganizationId property.</param>
        /// <param name="modifiedOn">Initial value of the ModifiedOn property.</param>
        /// <param name="userGroupId">Initial value of the UserGroupId property.</param>
        /// <param name="organizationId">Initial value of the OrganizationId property.</param>
        public static UserGroupXOrganization CreateUserGroupXOrganization(global::System.Guid userGroupXOrganizationId, global::System.DateTime modifiedOn, global::System.Guid userGroupId, global::System.Guid organizationId)
        {
            UserGroupXOrganization userGroupXOrganization = new UserGroupXOrganization();
            userGroupXOrganization.UserGroupXOrganizationId = userGroupXOrganizationId;
            userGroupXOrganization.ModifiedOn = modifiedOn;
            userGroupXOrganization.UserGroupId = userGroupId;
            userGroupXOrganization.OrganizationId = organizationId;
            return userGroupXOrganization;
        }

        #endregion
        [DataMember]
        public DateTime ModifiedOn { get; set; }
        [DataMember]
        public Guid OrganizationId { get; set; }
        [DataMember]
        public Guid UserGroupId { get; set; }
        [DataMember]
        public Guid UserGroupXOrganizationId { get; set; }
    
    #region Navigation Properties
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public Organization Organization
        {
    		get; set;
        }
    
        //[XmlIgnoreAttribute()]
        //[SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        public UserGroup UserGroup
        {
    		get; set;
        }

    #endregion
    }
    
}