USE [master]
GO
/****** Object:  Database [lm]    Script Date: 12/18/2017 13:55:05 ******/
CREATE DATABASE [lm] ON  PRIMARY 
( NAME = N'lm', FILENAME = N'F:\lm\lm.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'lm_log', FILENAME = N'F:\lm\lm_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [lm] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [lm].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [lm] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [lm] SET ANSI_NULLS OFF
GO
ALTER DATABASE [lm] SET ANSI_PADDING OFF
GO
ALTER DATABASE [lm] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [lm] SET ARITHABORT OFF
GO
ALTER DATABASE [lm] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [lm] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [lm] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [lm] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [lm] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [lm] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [lm] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [lm] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [lm] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [lm] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [lm] SET  DISABLE_BROKER
GO
ALTER DATABASE [lm] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [lm] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [lm] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [lm] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [lm] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [lm] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [lm] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [lm] SET  READ_WRITE
GO
ALTER DATABASE [lm] SET RECOVERY FULL
GO
ALTER DATABASE [lm] SET  MULTI_USER
GO
ALTER DATABASE [lm] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [lm] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'lm', N'ON'
GO
USE [lm]
GO
/****** Object:  Table [dbo].[tb_UploadFile]    Script Date: 12/18/2017 13:55:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_UploadFile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DirId] [int] NULL,
	[FileName] [varchar](500) NOT NULL,
	[Path] [varchar](500) NOT NULL,
	[Path_sm] [varchar](500) NULL,
	[Uploader] [int] NULL,
	[CreateTime] [datetime] NOT NULL,
	[FileSize] [varchar](50) NULL,
	[FileType] [int] NULL,
	[Sort] [int] NULL,
 CONSTRAINT [PK_tb_UploadFile1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目ID=路径文件夹' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_UploadFile', @level2type=N'COLUMN',@level2name=N'DirId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件原名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_UploadFile', @level2type=N'COLUMN',@level2name=N'FileName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上传文件原大小（路径）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_UploadFile', @level2type=N'COLUMN',@level2name=N'Path'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上传者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_UploadFile', @level2type=N'COLUMN',@level2name=N'Uploader'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上传时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_UploadFile', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件大小' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_UploadFile', @level2type=N'COLUMN',@level2name=N'FileSize'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文件类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_UploadFile', @level2type=N'COLUMN',@level2name=N'FileType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_UploadFile', @level2type=N'COLUMN',@level2name=N'Sort'
GO
/****** Object:  Table [dbo].[tb_TeamPeople]    Script Date: 12/18/2017 13:55:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_TeamPeople](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[People_En] [nvarchar](50) NULL,
	[People_Cn] [nvarchar](50) NULL,
	[People_Occupation] [nvarchar](50) NULL,
	[People_Specialty] [nvarchar](50) NULL,
	[People_Slogan] [nvarchar](50) NULL,
	[People_Profile] [nvarchar](250) NULL,
 CONSTRAINT [PK_td_TeamPeople] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Project]    Script Date: 12/18/2017 13:55:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Project_Name] [varchar](50) NOT NULL,
	[Project_Location] [varchar](50) NOT NULL,
	[Project_People] [int] NOT NULL,
	[Project_Start] [datetime] NOT NULL,
	[Project_End] [datetime] NOT NULL,
	[Project_Description] [varchar](800) NULL,
	[Create_date] [datetime] NOT NULL,
	[Project_ImgId] [int] NULL,
	[Create_People] [int] NULL,
	[Project_Type] [varchar](50) NOT NULL,
	[Project_IsShow] [bit] NOT NULL,
 CONSTRAINT [PK_td_Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_NewsCenter]    Script Date: 12/18/2017 13:55:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[tb_NewsCenter](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ArticlesDate] [datetime] NOT NULL,
	[ArticlesType] [varchar](50) NOT NULL,
	[ArticlesContent] [varchar](5000) NOT NULL,
	[ArticlesTitle] [varchar](200) NOT NULL,
	[CommentariesID] [int] NULL,
	[Creationtime] [datetime] NOT NULL,
	[CreationPeople] [int] NULL,
	[ImgPath] [varchar](150) NULL
) ON [PRIMARY]
SET ANSI_PADDING ON
ALTER TABLE [dbo].[tb_NewsCenter] ADD [tags] [varchar](50) NULL
ALTER TABLE [dbo].[tb_NewsCenter] ADD [IsComment] [bit] NOT NULL
ALTER TABLE [dbo].[tb_NewsCenter] ADD  CONSTRAINT [PK_tb_NewsCenter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_NewsCenter', @level2type=N'COLUMN',@level2name=N'ArticlesDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文字类型：005001=行业动态；005002=新闻' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_NewsCenter', @level2type=N'COLUMN',@level2name=N'ArticlesType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_NewsCenter', @level2type=N'COLUMN',@level2name=N'ArticlesContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_NewsCenter', @level2type=N'COLUMN',@level2name=N'ArticlesTitle'
GO
/****** Object:  Table [dbo].[tb_Menu_B]    Script Date: 12/18/2017 13:55:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Menu_B](
	[menu_Id] [int] IDENTITY(1,1) NOT NULL,
	[menu_Code] [varchar](50) NOT NULL,
	[menu_Name] [varchar](50) NOT NULL,
	[menu_FatherId] [varchar](50) NULL,
	[menu_Link] [varchar](200) NULL,
	[menu_Image] [varchar](50) NULL,
	[menu_Title] [varchar](50) NULL,
	[menu_Target] [varchar](50) NULL,
	[menu_Sort] [int] NULL,
	[menu_Stage] [int] NOT NULL,
	[menu_Note] [varchar](200) NULL,
	[menu_Link_temp] [varchar](200) NULL,
 CONSTRAINT [PK_td_Menu_B] PRIMARY KEY CLUSTERED 
(
	[menu_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Menu_A]    Script Date: 12/18/2017 13:55:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Menu_A](
	[menu_Id] [int] IDENTITY(1,1) NOT NULL,
	[menu_Code] [varchar](50) NOT NULL,
	[menu_Name] [varchar](50) NOT NULL,
	[menu_FatherId] [varchar](50) NULL,
	[menu_Link] [varchar](200) NULL,
	[menu_Image] [varchar](50) NULL,
	[menu_Title] [varchar](50) NULL,
	[menu_Target] [varchar](50) NULL,
	[menu_Sort] [int] NULL,
	[menu_Stage] [int] NOT NULL,
	[menu_Note] [varchar](200) NULL,
 CONSTRAINT [PK_td_Menu_A] PRIMARY KEY CLUSTERED 
(
	[menu_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Dictionary]    Script Date: 12/18/2017 13:55:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Dictionary](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[FId] [varchar](50) NOT NULL,
	[Description] [varchar](500) NOT NULL,
	[IconImg] [varchar](50) NOT NULL,
	[MenuLinkTemp] [varchar](200) NULL,
	[Sort] [int] NULL,
	[Targets] [varchar](200) NULL,
 CONSTRAINT [PK_td_Dictionary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_CompanyInfo]    Script Date: 12/18/2017 13:55:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_CompanyInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Company_Name] [nvarchar](50) NULL,
	[Company_Address] [nvarchar](150) NULL,
	[Company_Call] [varchar](25) NULL,
	[Company_Email] [varchar](50) NULL,
	[Company_Description_T] [varchar](300) NULL,
	[Company_description] [varchar](300) NULL,
	[Company_WorkTime] [varchar](300) NULL,
	[Company_AddersImgUrl] [varchar](200) NULL,
	[Company_LogoUrl] [varchar](200) NULL,
	[Company_copyright] [varchar](100) NULL,
 CONSTRAINT [PK_td_CompanyInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_CommentMessage]    Script Date: 12/18/2017 13:55:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_CommentMessage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CommentariesID] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Mobile] [varchar](50) NOT NULL,
	[Message] [nvarchar](500) NOT NULL,
	[SubmitDate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[IsDisplay] [bit] NOT NULL,
	[IsLook] [bit] NOT NULL,
	[OperationDate] [datetime] NULL,
	[MessType] [bit] NULL,
 CONSTRAINT [PK_tb_CommentMessage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_ClientMessage]    Script Date: 12/18/2017 13:55:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_ClientMessage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Message] [nvarchar](500) NOT NULL,
	[SubmitDate] [datetime] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[IsDisplay] [bit] NOT NULL,
	[IsLook] [bit] NOT NULL,
	[OperationDate] [datetime] NULL,
	[MessType] [bit] NULL,
 CONSTRAINT [PK_tb_ClientMessage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:未删除;1删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ClientMessage', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:不显示;1:显示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ClientMessage', @level2type=N'COLUMN',@level2name=N'IsDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:未看;1 已开' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ClientMessage', @level2type=N'COLUMN',@level2name=N'IsLook'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ClientMessage', @level2type=N'COLUMN',@level2name=N'MessType'
GO
/****** Object:  Table [dbo].[Sys_Users]    Script Date: 12/18/2017 13:55:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sys_Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Account] [varchar](255) NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Sex] [bit] NOT NULL,
	[Status] [bit] NOT NULL,
	[Type] [int] NOT NULL,
	[BizCode] [varchar](255) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CrateId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sys_LOG]    Script Date: 12/18/2017 13:55:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sys_LOG](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DATES] [datetime] NULL,
	[LEVELS] [nvarchar](20) NULL,
	[LOGGER] [nvarchar](200) NULL,
	[CLIENTUSER] [nvarchar](100) NULL,
	[CLIENTIP] [nvarchar](20) NULL,
	[REQUESTURL] [nvarchar](500) NULL,
	[ACTION] [nvarchar](20) NULL,
	[MESSAGE] [nvarchar](4000) NULL,
	[EXCEPTION] [nvarchar](4000) NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[v_NewsCenterComment]    Script Date: 12/18/2017 13:55:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_NewsCenterComment]
AS
SELECT     dbo.tb_CommentMessage.Name, dbo.tb_CommentMessage.Email, dbo.tb_CommentMessage.Mobile, dbo.tb_CommentMessage.Message, dbo.tb_CommentMessage.SubmitDate, 
                      dbo.tb_CommentMessage.IsDelete, dbo.tb_CommentMessage.IsDisplay, dbo.tb_CommentMessage.IsLook, dbo.tb_CommentMessage.OperationDate, dbo.tb_CommentMessage.MessType, 
                      dbo.tb_NewsCenter.ID, dbo.tb_NewsCenter.ArticlesDate, dbo.tb_NewsCenter.ArticlesType, dbo.tb_NewsCenter.ArticlesContent, dbo.tb_NewsCenter.ArticlesTitle, dbo.tb_NewsCenter.CommentariesID, 
                      dbo.tb_NewsCenter.Creationtime, dbo.tb_NewsCenter.CreationPeople, dbo.tb_NewsCenter.ImgPath, dbo.tb_NewsCenter.tags, dbo.tb_NewsCenter.IsComment, 
                      dbo.tb_CommentMessage.Id AS CommentID
FROM         dbo.tb_CommentMessage LEFT OUTER JOIN
                      dbo.tb_NewsCenter ON dbo.tb_CommentMessage.CommentariesID = dbo.tb_NewsCenter.ID
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[20] 4[15] 2[27] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "tb_CommentMessage"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 126
               Right = 206
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tb_NewsCenter"
            Begin Extent = 
               Top = 0
               Left = 304
               Bottom = 120
               Right = 472
            End
            DisplayFlags = 280
            TopColumn = 7
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 22
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_NewsCenterComment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'v_NewsCenterComment'
GO
/****** Object:  Default [DF_tb_UploadFile1_CreateTime]    Script Date: 12/18/2017 13:55:05 ******/
ALTER TABLE [dbo].[tb_UploadFile] ADD  CONSTRAINT [DF_tb_UploadFile1_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
/****** Object:  Default [DF__tb_Projec__Proje__173876EA]    Script Date: 12/18/2017 13:55:05 ******/
ALTER TABLE [dbo].[tb_Project] ADD  DEFAULT ((0)) FOR [Project_IsShow]
GO
/****** Object:  Default [DF__tb_NewsCe__IsCom__25869641]    Script Date: 12/18/2017 13:55:05 ******/
ALTER TABLE [dbo].[tb_NewsCenter] ADD  DEFAULT ((1)) FOR [IsComment]
GO
/****** Object:  Default [DF_tb_ClientMessage_SubmitDate]    Script Date: 12/18/2017 13:55:05 ******/
ALTER TABLE [dbo].[tb_ClientMessage] ADD  CONSTRAINT [DF_tb_ClientMessage_SubmitDate]  DEFAULT (getdate()) FOR [SubmitDate]
GO
/****** Object:  Default [DF_tb_ClientMessage_IsDelete]    Script Date: 12/18/2017 13:55:05 ******/
ALTER TABLE [dbo].[tb_ClientMessage] ADD  CONSTRAINT [DF_tb_ClientMessage_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
/****** Object:  Default [DF_tb_ClientMessage_MessType]    Script Date: 12/18/2017 13:55:05 ******/
ALTER TABLE [dbo].[tb_ClientMessage] ADD  CONSTRAINT [DF_tb_ClientMessage_MessType]  DEFAULT ((0)) FOR [MessType]
GO
