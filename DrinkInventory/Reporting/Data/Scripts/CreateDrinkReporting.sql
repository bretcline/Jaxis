USE [master]
GO
/****** Object:  Database [DrinkReporting]    Script Date: 07/12/2012 14:36:48 ******/
CREATE DATABASE [DrinkReporting]
GO
ALTER DATABASE [DrinkReporting] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DrinkReporting].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DrinkReporting] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [DrinkReporting] SET ANSI_NULLS OFF
GO
ALTER DATABASE [DrinkReporting] SET ANSI_PADDING OFF
GO
ALTER DATABASE [DrinkReporting] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [DrinkReporting] SET ARITHABORT OFF
GO
ALTER DATABASE [DrinkReporting] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [DrinkReporting] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [DrinkReporting] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [DrinkReporting] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [DrinkReporting] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [DrinkReporting] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [DrinkReporting] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [DrinkReporting] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [DrinkReporting] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [DrinkReporting] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [DrinkReporting] SET  DISABLE_BROKER
GO
ALTER DATABASE [DrinkReporting] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [DrinkReporting] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [DrinkReporting] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [DrinkReporting] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [DrinkReporting] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [DrinkReporting] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [DrinkReporting] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [DrinkReporting] SET  READ_WRITE
GO
ALTER DATABASE [DrinkReporting] SET RECOVERY FULL
GO
ALTER DATABASE [DrinkReporting] SET  MULTI_USER
GO
ALTER DATABASE [DrinkReporting] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [DrinkReporting] SET DB_CHAINING OFF
GO
USE [DrinkReporting]
GO
/****** Object:  Table [dbo].[Alerts]    Script Date: 07/12/2012 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Alerts](
	[AlertId] [uniqueidentifier] NOT NULL,
	[Message] [nvarchar](152) NOT NULL,
	[AlertTime] [datetime] NOT NULL,
	[AlertType] [int] NOT NULL,
	[OrganizationId] [uniqueidentifier] NOT NULL,
	[Severity] [int] NOT NULL,
	[LocationName] [nvarchar](256) NULL,
	[ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Alerts] PRIMARY KEY CLUSTERED 
(
	[AlertId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POSTicketItems]    Script Date: 07/12/2012 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POSTicketItems](
	[POSTicketItemId] [uniqueidentifier] NOT NULL,
	[CheckNumber] [nvarchar](50) NOT NULL,
	[Comment] [nvarchar](256) NULL,
	[Description] [nvarchar](256) NOT NULL,
	[Price] [money] NULL,
	[Quantity] [int] NOT NULL,
	[LocationName] [nvarchar](256) NOT NULL,
	[Establishment] [nvarchar](256) NOT NULL,
	[TicketDate] [datetime] NOT NULL,
	[OrganizationId] [uniqueidentifier] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_POSTicketItems] PRIMARY KEY CLUSTERED 
(
	[POSTicketItemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Areas]    Script Date: 07/12/2012 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Areas](
	[AreaId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[Order] [int] NOT NULL,
	[ShortName] [nvarchar](30) NOT NULL,
	[Controller] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Areas] PRIMARY KEY CLUSTERED 
(
	[AreaId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Devices]    Script Date: 07/12/2012 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Devices](
	[DeviceId] [uniqueidentifier] NOT NULL,
	[DeviceNumber] [nvarchar](50) NOT NULL,
	[OrganizationId] [uniqueidentifier] NOT NULL,
	[Status] [int] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[LocationName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Devices] PRIMARY KEY CLUSTERED 
(
	[DeviceNumber] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Organizations]    Script Date: 07/12/2012 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organizations](
	[OrganizationId] [uniqueidentifier] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[ParentId] [uniqueidentifier] NULL,
	[ShortName] [nvarchar](50) NOT NULL,
	[Path] [nvarchar](max) NULL,
	[Name] [nvarchar](256) NULL,
	[Description] [nvarchar](512) NULL,
 CONSTRAINT [PK_Organizations] PRIMARY KEY CLUSTERED 
(
	[OrganizationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UPCs]    Script Date: 07/12/2012 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UPCs](
	[UPCId] [uniqueidentifier] NOT NULL,
	[ItemNumber] [nvarchar](256) NOT NULL,
	[ChildItemNumber] [nvarchar](256) NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Size] [int] NOT NULL,
	[SizeLabel] [nvarchar](20) NOT NULL,
	[CategoryName] [nvarchar](256) NOT NULL,
	[RootCategoryName] [nvarchar](256) NOT NULL,
	[Validated] [bit] NOT NULL,
	[BottleCount] [int] NULL,
	[ManufacturerName] [nvarchar](512) NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_UPCs] PRIMARY KEY CLUSTERED 
(
	[ItemNumber] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schema]    Script Date: 07/12/2012 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schema](
	[SchemaId] [uniqueidentifier] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[Version] [int] NULL,
 CONSTRAINT [PK_Schema] PRIMARY KEY CLUSTERED 
(
	[SchemaId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reports]    Script Date: 07/12/2012 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reports](
	[ReportId] [uniqueidentifier] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Order] [int] NOT NULL,
	[Type] [nvarchar](20) NOT NULL,
	[SelectCommand] [nvarchar](max) NOT NULL,
	[ShortName] [nvarchar](50) NOT NULL,
	[ReportClassName] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Reports] PRIMARY KEY CLUSTERED 
(
	[ReportId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pours]    Script Date: 07/12/2012 14:36:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pours](
	[PourId] [uniqueidentifier] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[PourTime] [datetime] NOT NULL,
	[TagNumber] [nvarchar](50) NOT NULL,
	[DeviceNumber] [nvarchar](50) NOT NULL,
	[Volume] [float] NOT NULL,
	[Duration] [float] NOT NULL,
	[AmountLeft] [float] NOT NULL,
	[Temperature] [float] NOT NULL,
	[RawData] [nvarchar](2048) NULL,
	[BatteryVoltage] [float] NOT NULL,
	[ItemNumber] [nvarchar](256) NOT NULL,
	[LocationName] [nvarchar](256) NOT NULL,
	[OrganizationId] [uniqueidentifier] NOT NULL,
	[POSTicketItemId] [uniqueidentifier] NULL,
	[Status] [int] NOT NULL,
	[Cost] [money] NOT NULL,
 CONSTRAINT [PK_Pours] PRIMARY KEY CLUSTERED 
(
	[PourId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[GetOrganizationPath]    Script Date: 07/12/2012 14:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetOrganizationPath](@OrganizationId UNIQUEIDENTIFIER)
RETURNS NVARCHAR(370)
AS
BEGIN
	DECLARE @Path NVARCHAR(370)
	SET @Path = ''
	WHILE (@OrganizationId IS NOT NULL)
	BEGIN	
		SET @Path = CAST(@OrganizationId AS NVARCHAR(36)) + '/' + @Path
		SET @OrganizationId = (SELECT ParentId FROM Organizations WHERE OrganizationId = @OrganizationId)
	END
	RETURN @Path
END
GO
/****** Object:  Table [dbo].[Columns]    Script Date: 07/12/2012 14:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Columns](
	[ColumnId] [uniqueidentifier] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[DisplayName] [nvarchar](100) NOT NULL,
	[ReportId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Columns] PRIMARY KEY CLUSTERED 
(
	[ColumnId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parameters]    Script Date: 07/12/2012 14:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parameters](
	[ParameterId] [uniqueidentifier] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[ReportId] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](20) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Order] [int] NOT NULL,
	[Label] [nvarchar](50) NULL,
 CONSTRAINT [PK_Parameters] PRIMARY KEY CLUSTERED 
(
	[ParameterId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserGroups]    Script Date: 07/12/2012 14:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroups](
	[UserGroupId] [uniqueidentifier] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[OrganizationId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserGroups] PRIMARY KEY CLUSTERED 
(
	[UserGroupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 07/12/2012 14:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[VisibleWidgetIds] [nvarchar](max) NOT NULL,
	[OrganizationId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Users] ON [dbo].[Users] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserGroupXOrganizations]    Script Date: 07/12/2012 14:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroupXOrganizations](
	[UserGroupXOrganizationId] [uniqueidentifier] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[UserGroupId] [uniqueidentifier] NOT NULL,
	[OrganizationId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserGroupXOrganizations] PRIMARY KEY CLUSTERED 
(
	[UserGroupXOrganizationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserGroupMemberships]    Script Date: 07/12/2012 14:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroupMemberships](
	[UserGroupMembershipId] [uniqueidentifier] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[UserGroupId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UserGroupMemberships] PRIMARY KEY CLUSTERED 
(
	[UserGroupMembershipId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AreaMemberships]    Script Date: 07/12/2012 14:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AreaMemberships](
	[AreaMembershipId] [uniqueidentifier] NOT NULL,
	[AreaId] [uniqueidentifier] NOT NULL,
	[UserGroupId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[ShortName] [nvarchar](50) NULL,
	[ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_AreaMemberships] PRIMARY KEY CLUSTERED 
(
	[AreaMembershipId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sessions]    Script Date: 07/12/2012 14:36:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sessions](
	[SessionId] [uniqueidentifier] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ExpirationTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Sessions] PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[SecurityView]    Script Date: 07/12/2012 14:36:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[SecurityView] AS
SELECT DISTINCT o.OrganizationId AS OrganizationId, s.SessionId AS SessionId
FROM Organizations o
JOIN Organizations orgUser ON LEFT(o.[Path], LEN(orgUser.[Path])) = orgUser.[Path]
JOIN UserGroupXOrganizations ugxo ON orgUser.OrganizationId = ugxo.OrganizationId
JOIN UserGroups userGroup ON ugxo.UserGroupId = userGroup.UserGroupId
JOIN UserGroupMemberships membership ON userGroup.UserGroupId = membership.UserGroupId
JOIN Sessions s ON membership.UserId = s.UserId
WHERE s.ExpirationTime > GETDATE()
GO
/****** Object:  Default [DF_Areas_Id]    Script Date: 07/12/2012 14:36:49 ******/
ALTER TABLE [dbo].[Areas] ADD  CONSTRAINT [DF_Areas_Id]  DEFAULT (newid()) FOR [AreaId]
GO
/****** Object:  Default [DF_Areas_ModifiedOn]    Script Date: 07/12/2012 14:36:49 ******/
ALTER TABLE [dbo].[Areas] ADD  CONSTRAINT [DF_Areas_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Locations_Id]    Script Date: 07/12/2012 14:36:49 ******/
ALTER TABLE [dbo].[Organizations] ADD  CONSTRAINT [DF_Locations_Id]  DEFAULT (newid()) FOR [OrganizationId]
GO
/****** Object:  Default [DF_Locations_ModifiedOn]    Script Date: 07/12/2012 14:36:49 ******/
ALTER TABLE [dbo].[Organizations] ADD  CONSTRAINT [DF_Locations_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Schema_Id]    Script Date: 07/12/2012 14:36:49 ******/
ALTER TABLE [dbo].[Schema] ADD  CONSTRAINT [DF_Schema_Id]  DEFAULT (newid()) FOR [SchemaId]
GO
/****** Object:  Default [DF_Schema_ModifiedOn]    Script Date: 07/12/2012 14:36:49 ******/
ALTER TABLE [dbo].[Schema] ADD  CONSTRAINT [DF_Schema_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Columns_Id]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[Columns] ADD  CONSTRAINT [DF_Columns_Id]  DEFAULT (newid()) FOR [ColumnId]
GO
/****** Object:  Default [DF_Columns_ModifiedOn]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[Columns] ADD  CONSTRAINT [DF_Columns_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Parameters_Id]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[Parameters] ADD  CONSTRAINT [DF_Parameters_Id]  DEFAULT (newid()) FOR [ParameterId]
GO
/****** Object:  Default [DF_Parameters_ModifiedOn]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[Parameters] ADD  CONSTRAINT [DF_Parameters_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_UserGroups_Id]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[UserGroups] ADD  CONSTRAINT [DF_UserGroups_Id]  DEFAULT (newid()) FOR [UserGroupId]
GO
/****** Object:  Default [DF_UserGroups_ModifiedOn]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[UserGroups] ADD  CONSTRAINT [DF_UserGroups_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Users_Id]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Id]  DEFAULT (newid()) FOR [UserId]
GO
/****** Object:  Default [DF_Users_ModifiedOn]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Users_VisibleWidgetIds]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_VisibleWidgetIds]  DEFAULT ('') FOR [VisibleWidgetIds]
GO
/****** Object:  Default [DF_UserGroupMemberships_Id]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[UserGroupMemberships] ADD  CONSTRAINT [DF_UserGroupMemberships_Id]  DEFAULT (newid()) FOR [UserGroupMembershipId]
GO
/****** Object:  Default [DF_UserGroupMemberships_ModifiedOn]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[UserGroupMemberships] ADD  CONSTRAINT [DF_UserGroupMemberships_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_AreaMemberships_Id]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[AreaMemberships] ADD  CONSTRAINT [DF_AreaMemberships_Id]  DEFAULT (newid()) FOR [AreaMembershipId]
GO
/****** Object:  Default [DF_AreaMemberships_ModifiedOn]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[AreaMemberships] ADD  CONSTRAINT [DF_AreaMemberships_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  Default [DF_Sessions_Id]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[Sessions] ADD  CONSTRAINT [DF_Sessions_Id]  DEFAULT (newid()) FOR [SessionId]
GO
/****** Object:  Default [DF_Sessions_ModifiedOn]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[Sessions] ADD  CONSTRAINT [DF_Sessions_ModifiedOn]  DEFAULT (getdate()) FOR [ModifiedOn]
GO
/****** Object:  ForeignKey [FK_Organizations_Organizations]    Script Date: 07/12/2012 14:36:49 ******/
ALTER TABLE [dbo].[Organizations]  WITH CHECK ADD  CONSTRAINT [FK_Organizations_Organizations] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Organizations] ([OrganizationId])
GO
ALTER TABLE [dbo].[Organizations] CHECK CONSTRAINT [FK_Organizations_Organizations]
GO
/****** Object:  ForeignKey [FK_Pours_POSTicketItems]    Script Date: 07/12/2012 14:36:49 ******/
ALTER TABLE [dbo].[Pours]  WITH CHECK ADD  CONSTRAINT [FK_Pours_POSTicketItems] FOREIGN KEY([POSTicketItemId])
REFERENCES [dbo].[POSTicketItems] ([POSTicketItemId])
GO
ALTER TABLE [dbo].[Pours] CHECK CONSTRAINT [FK_Pours_POSTicketItems]
GO
/****** Object:  ForeignKey [FK_Columns_Reports]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[Columns]  WITH CHECK ADD  CONSTRAINT [FK_Columns_Reports] FOREIGN KEY([ReportId])
REFERENCES [dbo].[Reports] ([ReportId])
GO
ALTER TABLE [dbo].[Columns] CHECK CONSTRAINT [FK_Columns_Reports]
GO
/****** Object:  ForeignKey [FK_Parameters_Reports]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[Parameters]  WITH CHECK ADD  CONSTRAINT [FK_Parameters_Reports] FOREIGN KEY([ReportId])
REFERENCES [dbo].[Reports] ([ReportId])
GO
ALTER TABLE [dbo].[Parameters] CHECK CONSTRAINT [FK_Parameters_Reports]
GO
/****** Object:  ForeignKey [FK_UserGroups_Organizations]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[UserGroups]  WITH CHECK ADD  CONSTRAINT [FK_UserGroups_Organizations] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organizations] ([OrganizationId])
GO
ALTER TABLE [dbo].[UserGroups] CHECK CONSTRAINT [FK_UserGroups_Organizations]
GO
/****** Object:  ForeignKey [FK_Users_Organizations]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Organizations] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organizations] ([OrganizationId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Organizations]
GO
/****** Object:  ForeignKey [FK_UserGroupXOrganizations_Organizations]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[UserGroupXOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_UserGroupXOrganizations_Organizations] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organizations] ([OrganizationId])
GO
ALTER TABLE [dbo].[UserGroupXOrganizations] CHECK CONSTRAINT [FK_UserGroupXOrganizations_Organizations]
GO
/****** Object:  ForeignKey [FK_UserGroupXOrganizations_UserGroups]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[UserGroupXOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_UserGroupXOrganizations_UserGroups] FOREIGN KEY([UserGroupId])
REFERENCES [dbo].[UserGroups] ([UserGroupId])
GO
ALTER TABLE [dbo].[UserGroupXOrganizations] CHECK CONSTRAINT [FK_UserGroupXOrganizations_UserGroups]
GO
/****** Object:  ForeignKey [FK_UserGroupMemberships_UserGroups]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[UserGroupMemberships]  WITH CHECK ADD  CONSTRAINT [FK_UserGroupMemberships_UserGroups] FOREIGN KEY([UserGroupId])
REFERENCES [dbo].[UserGroups] ([UserGroupId])
GO
ALTER TABLE [dbo].[UserGroupMemberships] CHECK CONSTRAINT [FK_UserGroupMemberships_UserGroups]
GO
/****** Object:  ForeignKey [FK_UserGroupMemberships_Users]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[UserGroupMemberships]  WITH CHECK ADD  CONSTRAINT [FK_UserGroupMemberships_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserGroupMemberships] CHECK CONSTRAINT [FK_UserGroupMemberships_Users]
GO
/****** Object:  ForeignKey [FK_AreaMemberships_Areas]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[AreaMemberships]  WITH CHECK ADD  CONSTRAINT [FK_AreaMemberships_Areas] FOREIGN KEY([AreaId])
REFERENCES [dbo].[Areas] ([AreaId])
GO
ALTER TABLE [dbo].[AreaMemberships] CHECK CONSTRAINT [FK_AreaMemberships_Areas]
GO
/****** Object:  ForeignKey [FK_AreaMemberships_UserGroups]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[AreaMemberships]  WITH CHECK ADD  CONSTRAINT [FK_AreaMemberships_UserGroups] FOREIGN KEY([UserGroupId])
REFERENCES [dbo].[UserGroups] ([UserGroupId])
GO
ALTER TABLE [dbo].[AreaMemberships] CHECK CONSTRAINT [FK_AreaMemberships_UserGroups]
GO
/****** Object:  ForeignKey [FK_Sessions_Users]    Script Date: 07/12/2012 14:36:51 ******/
ALTER TABLE [dbo].[Sessions]  WITH CHECK ADD  CONSTRAINT [FK_Sessions_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Sessions] CHECK CONSTRAINT [FK_Sessions_Users]
GO


