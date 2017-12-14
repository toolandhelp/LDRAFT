USE [master]
GO
/****** Object:  Database [lm]    Script Date: 2017/12/14 23:18:25 ******/
CREATE DATABASE [lm] ON  PRIMARY 
( NAME = N'lm', FILENAME = N'G:\DatasetCentre\lm.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'lm_log', FILENAME = N'G:\DatasetCentre\lm_log.mdf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
/****** Object:  Table [dbo].[Sys_LOG]    Script Date: 2017/12/14 23:18:25 ******/
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
/****** Object:  Table [dbo].[Sys_Users]    Script Date: 2017/12/14 23:18:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_ClientMessage]    Script Date: 2017/12/14 23:18:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_CompanyInfo]    Script Date: 2017/12/14 23:18:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Dictionary]    Script Date: 2017/12/14 23:18:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Menu_A]    Script Date: 2017/12/14 23:18:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Menu_B]    Script Date: 2017/12/14 23:18:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_NewsCenter]    Script Date: 2017/12/14 23:18:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
	[ImgPath] [varchar](150) NULL,
 CONSTRAINT [PK_tb_NewsCenter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Project]    Script Date: 2017/12/14 23:18:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_TeamPeople]    Script Date: 2017/12/14 23:18:25 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_UploadFile]    Script Date: 2017/12/14 23:18:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Sys_LOG] ON 

INSERT [dbo].[Sys_LOG] ([ID], [DATES], [LEVELS], [LOGGER], [CLIENTUSER], [CLIENTIP], [REQUESTURL], [ACTION], [MESSAGE], [EXCEPTION]) VALUES (1, CAST(N'2017-11-09T22:19:29.693' AS DateTime), N'WARN', N'dblog', N'Dev', N'127.0.0.1', N'http://localhost:51815/Admin/Login/SysLogin', N'Login', N'系统登录，登录结果：验证码错误.', N'')
INSERT [dbo].[Sys_LOG] ([ID], [DATES], [LEVELS], [LOGGER], [CLIENTUSER], [CLIENTIP], [REQUESTURL], [ACTION], [MESSAGE], [EXCEPTION]) VALUES (2, CAST(N'2017-11-13T21:39:50.413' AS DateTime), N'WARN', N'dblog', N'Dev', N'127.0.0.1', N'http://localhost:51815/Admin/Login/SysLogin', N'Login', N'系统登录，登录结果：验证码错误.', N'')
INSERT [dbo].[Sys_LOG] ([ID], [DATES], [LEVELS], [LOGGER], [CLIENTUSER], [CLIENTIP], [REQUESTURL], [ACTION], [MESSAGE], [EXCEPTION]) VALUES (3, CAST(N'2017-11-13T21:39:55.520' AS DateTime), N'INFO', N'dblog', N'Dev', N'127.0.0.1', N'http://localhost:51815/Admin/Login/SysLogin', N'Login', N'系统登录，登录结果：登录成功.', N'')
INSERT [dbo].[Sys_LOG] ([ID], [DATES], [LEVELS], [LOGGER], [CLIENTUSER], [CLIENTIP], [REQUESTURL], [ACTION], [MESSAGE], [EXCEPTION]) VALUES (4, CAST(N'2017-12-06T10:43:04.860' AS DateTime), N'WARN', N'dblog', N'Dev', N'127.0.0.1', N'http://localhost:51815/Admin/Login/SysLogin', N'Login', N'系统登录，登录结果：验证码错误.', N'')
INSERT [dbo].[Sys_LOG] ([ID], [DATES], [LEVELS], [LOGGER], [CLIENTUSER], [CLIENTIP], [REQUESTURL], [ACTION], [MESSAGE], [EXCEPTION]) VALUES (5, CAST(N'2017-12-06T10:43:11.023' AS DateTime), N'INFO', N'dblog', N'Dev', N'127.0.0.1', N'http://localhost:51815/Admin/Login/SysLogin', N'Login', N'系统登录，登录结果：登录成功.', N'')
SET IDENTITY_INSERT [dbo].[Sys_LOG] OFF
INSERT [dbo].[Sys_Users] ([Id], [Account], [Password], [Name], [Sex], [Status], [Type], [BizCode], [CreateTime], [CrateId]) VALUES (N'4f3d37a2-9788-4ace-a2d5-c49fa81504e9', N'Dev', N'54809f31a5f991aeba92d0f910367544', N'Admin', 1, 1, 1, N' ', CAST(N'2017-10-12T11:35:06.783' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[tb_ClientMessage] ON 

INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (2, N'顶顶顶', N'111@ss.con', N'1111', CAST(N'2017-12-11T16:38:43.600' AS DateTime), 0, 0, 0, NULL, NULL)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (3, N'天天', N'11@11.com', N'testtest水龙头', CAST(N'2017-12-11T16:59:51.297' AS DateTime), 0, 0, 0, NULL, NULL)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (4, N'来来来', N'1@ww.com', N'lsjdfij', CAST(N'2017-12-11T17:03:40.217' AS DateTime), 0, 0, 0, NULL, NULL)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (5, N'ssdf', N'sdfs@qq.com', N'sdf sd', CAST(N'2017-12-11T17:12:34.750' AS DateTime), 0, 0, 0, NULL, NULL)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (6, N'你好', N'1@ww.com', N'她她她', CAST(N'2017-12-12T09:13:27.000' AS DateTime), 0, 0, 0, NULL, NULL)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (7, N'1212914', N'1@ww.com', N'你懂的', CAST(N'2017-12-12T09:15:06.847' AS DateTime), 0, 0, 0, NULL, NULL)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (8, N'你好', N'1@ww.com', N'0916', CAST(N'2017-12-12T09:17:06.313' AS DateTime), 0, 0, 0, NULL, NULL)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (9, N'你好', N'1@ww.com', N'3333', CAST(N'2017-12-12T09:20:25.910' AS DateTime), 0, 0, 0, NULL, NULL)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (10, N'你好', N'1@ww.com', N'212', CAST(N'2017-12-12T09:21:30.407' AS DateTime), 0, 0, 0, NULL, NULL)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (11, N'你好', N'1@ww.com', N'士大夫', CAST(N'2017-12-12T09:22:35.297' AS DateTime), 0, 0, 0, NULL, NULL)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (12, N'你好', N'1@ww.com', N'你', CAST(N'2017-12-12T09:24:33.640' AS DateTime), 0, 0, 0, NULL, NULL)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (13, N'试试', N'ss@qq.com', N'试试', CAST(N'2017-12-12T09:25:12.590' AS DateTime), 0, 0, 0, NULL, NULL)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (14, N'666', N'99@qq.cn', N'9595', CAST(N'2017-12-12T09:26:11.623' AS DateTime), 0, 0, 0, NULL, NULL)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (15, N'nihao', N'1@ww.com', N'nidhoa', CAST(N'2017-12-12T09:27:38.290' AS DateTime), 0, 0, 0, NULL, NULL)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (16, N'nihao', N'1@ww.com', N'111', CAST(N'2017-12-12T09:38:53.120' AS DateTime), 0, 0, 0, NULL, NULL)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (17, N'11', N'1@ww.com', N'11', CAST(N'2017-12-12T10:09:06.090' AS DateTime), 0, 0, 0, NULL, NULL)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (18, N'1', N'1@ww.com', N'11&lt;script&gt;&lt;/script&gt;', CAST(N'2017-12-12T10:13:27.547' AS DateTime), 0, 0, 0, NULL, NULL)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (19, N'1', N'1@ww.com', N'11', CAST(N'2017-12-12T11:13:21.697' AS DateTime), 0, 0, 0, NULL, 0)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (20, N'1', N'1@ww.com', N'11', CAST(N'2017-12-12T11:14:05.693' AS DateTime), 0, 0, 0, NULL, 0)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (21, N'1', N'1@ww.com', N'11', CAST(N'2017-12-12T11:14:27.340' AS DateTime), 0, 0, 0, NULL, 0)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (22, N'11', N'1@ww.com', N'11', CAST(N'2017-12-12T11:16:04.800' AS DateTime), 0, 0, 0, NULL, 0)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (23, N'1', N'1@ww.com', N'11', CAST(N'2017-12-12T11:18:21.077' AS DateTime), 0, 0, 0, NULL, 0)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (24, N'x姓名', N'1@ww.com', N'她她她', CAST(N'2017-12-12T14:36:38.553' AS DateTime), 0, 0, 0, NULL, 1)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (25, N'x姓名', N'1@ww.com', N'她她她', CAST(N'2017-12-12T14:37:18.643' AS DateTime), 0, 0, 0, NULL, 1)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (26, N'x姓名', N'1@ww.com', N'她她她', CAST(N'2017-12-12T14:38:00.917' AS DateTime), 0, 0, 0, NULL, 1)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (27, N'姓名2', N'1@ww.com', N'111&lt;script&gt;&lt;/asirp', CAST(N'2017-12-12T14:43:21.690' AS DateTime), 0, 0, 0, NULL, 1)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (28, N'姓名', N'1@ww.com', N'111', CAST(N'2017-12-12T14:46:16.043' AS DateTime), 0, 0, 0, NULL, 1)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (29, N'出差', N'1@ww.cn', N'111', CAST(N'2017-12-12T14:49:32.880' AS DateTime), 0, 0, 0, NULL, 1)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (30, N'出差', N'1@ww.cn', N'111', CAST(N'2017-12-12T14:50:22.677' AS DateTime), 0, 0, 0, NULL, 1)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (31, N'出差', N'1@ww.cn', N'111', CAST(N'2017-12-12T14:52:48.103' AS DateTime), 0, 0, 0, NULL, 1)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (32, N'aa', N'aa@qq.cn', N'111', CAST(N'2017-12-12T14:53:21.170' AS DateTime), 0, 0, 0, NULL, 1)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (33, N'xx', N'xx@qq.com', N'nidongd', CAST(N'2017-12-12T14:55:03.563' AS DateTime), 0, 0, 0, NULL, 1)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (34, N'11', N'1@ww.cn', N'11', CAST(N'2017-12-12T15:01:23.080' AS DateTime), 0, 0, 0, NULL, 1)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (35, N'姓名', N'22@qq.com', N'测试', CAST(N'2017-12-12T15:08:47.827' AS DateTime), 0, 0, 0, NULL, 1)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (36, N'谢谢', N'22@qq.com', N'测试2', CAST(N'2017-12-12T15:09:13.130' AS DateTime), 0, 0, 0, NULL, 1)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (37, N'姓名', N'22@qq.com', N'测试11', CAST(N'2017-12-12T15:10:54.843' AS DateTime), 0, 0, 0, NULL, 1)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (38, N'ss', N'22@qq.com', N'得分', CAST(N'2017-12-12T15:11:15.163' AS DateTime), 0, 0, 0, NULL, 1)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (39, N'是', N'22@qq.com', N'测试', CAST(N'2017-12-12T15:11:51.313' AS DateTime), 0, 0, 0, NULL, 1)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (40, N'xx', N'22@qq.co', N'xx', CAST(N'2017-12-12T15:16:27.177' AS DateTime), 0, 0, 0, NULL, 1)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (41, N'rr', N'5@qq.com', N'test', CAST(N'2017-12-12T15:32:12.290' AS DateTime), 0, 0, 0, NULL, 1)
INSERT [dbo].[tb_ClientMessage] ([Id], [Name], [Email], [Message], [SubmitDate], [IsDelete], [IsDisplay], [IsLook], [OperationDate], [MessType]) VALUES (42, N'ces', N'ces@qq.com', N'sdf', CAST(N'2017-12-12T15:32:42.603' AS DateTime), 0, 0, 0, NULL, 1)
SET IDENTITY_INSERT [dbo].[tb_ClientMessage] OFF
SET IDENTITY_INSERT [dbo].[tb_CompanyInfo] ON 

INSERT [dbo].[tb_CompanyInfo] ([Id], [Company_Name], [Company_Address], [Company_Call], [Company_Email], [Company_Description_T], [Company_description], [Company_WorkTime], [Company_AddersImgUrl], [Company_LogoUrl], [Company_copyright]) VALUES (5, N'上海建筑设计有效公司', N'上海市 ， 南京东路 ， 测试测试66弄88号', N'+86 182 8888 8888', N'88888888@qq.com', N'公司成员均为业内设计管理技术骨干，具有深厚的专业功底、开阔的视野、丰富的设计优化和设', N'公司自成立以来，先后为万科、中信、佳兆业、招商等众多知名开发商提供专业设计优化和设计咨询服务，成功实现每个项目节省成本15%左右，随着公司规&lt;br> by LDRAFT.COM . ', N'星期一 - 星期五 09：00 - 06：00', NULL, NULL, N'Copyright 2017 LDRaft')
SET IDENTITY_INSERT [dbo].[tb_CompanyInfo] OFF
SET IDENTITY_INSERT [dbo].[tb_Dictionary] ON 

INSERT [dbo].[tb_Dictionary] ([Id], [Name], [FId], [Description], [IconImg], [MenuLinkTemp], [Sort], [Targets]) VALUES (2, N'关于我们', N'002', N'同宽建筑，始创于2000年，由上海同宽建筑设计股份有限公司（上海总部、山东、镇江分公司）、上海同宽投资咨询有限公司、上海同宽健康管理咨询有限公司、上海天度环境造型艺术有限公司共同组成，主要业务范围包括：工程咨询，规划设计，建筑设计，改扩建设计，景观设计，室内设计，雕塑设计，BIM参数化设计、工程管理与工程总承包等业务，公司长期致力于以设计为龙头，涵盖设计的各个环节，专注健康产业（医疗，旅游，养老）的咨询、设计、管理、运营等产业链的全过程顾问服务。', N'icon-trophy', N'/AboutUs/Index', 1, N'main')
INSERT [dbo].[tb_Dictionary] ([Id], [Name], [FId], [Description], [IconImg], [MenuLinkTemp], [Sort], [Targets]) VALUES (4, N'服务范围', N'003', N' 同宽建筑在保持自我设计优势的同时，长期专注于医疗、健康、', N'icon-trophy', N'/ServiceRange/Index', 2, N'main')
INSERT [dbo].[tb_Dictionary] ([Id], [Name], [FId], [Description], [IconImg], [MenuLinkTemp], [Sort], [Targets]) VALUES (5, N'经典案例', N'004', N'上海优山美苑', N'icon-trophy', N'/ClassisPlan/Index', 3, N'main')
INSERT [dbo].[tb_Dictionary] ([Id], [Name], [FId], [Description], [IconImg], [MenuLinkTemp], [Sort], [Targets]) VALUES (6, N'新闻中心', N'005', N'新闻描述', N'icon-trophy', N'/NewConent/Index', 4, N'main')
SET IDENTITY_INSERT [dbo].[tb_Dictionary] OFF
SET IDENTITY_INSERT [dbo].[tb_Menu_A] ON 

INSERT [dbo].[tb_Menu_A] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note]) VALUES (1, N'001', N'系统管理', N'0', N'#', N'lnr-cog', N'系统管理', NULL, 2, 1, NULL)
INSERT [dbo].[tb_Menu_A] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note]) VALUES (2, N'001001', N'用户管理', N'001', N'/Admin/User/Index', NULL, N'用户管理', N'mainFrame', 1, 1, NULL)
INSERT [dbo].[tb_Menu_A] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note]) VALUES (3, N'001002', N'角色管理', N'001', N'/Admin/Role/Index', NULL, N'角色管理', N'mainFrame', 2, 1, NULL)
INSERT [dbo].[tb_Menu_A] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note]) VALUES (5, N'002', N'仪表板', N'0', N'/Admin/Home/Home', N'lnr-home', N'仪表板', N'mainFrame', 1, 1, NULL)
INSERT [dbo].[tb_Menu_A] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note]) VALUES (6, N'003', N'网站首页', N'0', N'/Admin/Website/Index', N'lnr-apartment', N'网站首页', N'mainFrame', 3, 1, NULL)
SET IDENTITY_INSERT [dbo].[tb_Menu_A] OFF
SET IDENTITY_INSERT [dbo].[tb_Menu_B] ON 

INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (1, N'001', N'首  页', N'0', N'/Home/Content', NULL, N'首  页', N'main', 1, 1, NULL, N'/Home/Content')
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (2, N'002', N'关于我们', N'0', N'#', NULL, N'关于我们', N'main', 2, 1, NULL, N'/AboutUs/Index')
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (3, N'002001', N'企业介绍', N'002', N'/AboutUs/EntInt', NULL, N'企业介绍', N'main', 1, 1, NULL, NULL)
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (5, N'002002', N'专业团队', N'002', N'/AboutUs/MajTeam', NULL, N'专业团队', N'main', 2, 1, NULL, NULL)
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (6, N'002003', N'企业理念', N'002', N'/AboutUs/EntPhi', NULL, N'企业理念', N'main', 3, 1, NULL, NULL)
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (7, N'002004', N'组织框架', N'002', N'/AboutUs/OrgFrames', NULL, N'组织框架', N'main', 4, 1, NULL, NULL)
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (8, N'002005', N'我们的客户', N'002', N'/AboutUs/OurClient', NULL, N'我们的客户', N'main', 5, 1, NULL, NULL)
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (9, N'003', N'服务范围', N'0', N'#', NULL, N'服务范围', N'main', 3, 1, NULL, N'/ServiceRange/Index')
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (11, N'003001', N'结构设计', N'003', N'/ServiceRange/Structure', NULL, N'结构设计', N'main', 1, 1, NULL, NULL)
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (12, N'003002', N'建筑方案', N'003', N'/ServiceRange/ArchPlan', NULL, N'建设方案', N'main', 2, 1, NULL, NULL)
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (14, N'003003', N'设备专业', N'003', N'/ServiceRange/EquMag', NULL, N'设备专业', N'main', 3, 1, NULL, NULL)
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (15, N'003004', N'幕墙钢结构', N'003', N'/ServiceRange/mqg', NULL, N'幕墙钢结', N'main', 4, 1, NULL, NULL)
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (16, N'003005', N'地下室停车', N'003', N'/ServiceRange/dxs', NULL, N'地下室停车', N'main', 5, 1, NULL, NULL)
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (17, N'003006', N'超限结构', N'003', N'/ServiceRange/cxjg', NULL, N'超限结构', N'main', 6, 1, NULL, NULL)
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (18, N'004', N'案例', N'0', N'#', NULL, N'案例', N'main', 4, 1, NULL, N'/ClassisPlan/Index')
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (19, N'004001', N'超高层/城市综合体', N'004', N'/ClassisPlan/index', NULL, N'超高层/城市综合体', N'main', 1, 1, NULL, NULL)
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (20, N'004002', N'酒店/办公/工业/旧城市改造', N'004', N'/ClassisPlan/index', NULL, N'酒店/办公/工业/旧城市改造', N'main', 2, 1, NULL, NULL)
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (21, N'004003', N'高层住宅', N'004', N'/ClassisPlan/index', NULL, N'高层住宅', N'main', 3, 1, NULL, NULL)
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (22, N'004004', N'低密度住宅', N'004', N'/ClassisPlan/index', NULL, N'低密度住宅  ', N'main', 4, 1, NULL, NULL)
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (23, N'004005', N'地下空间开发', N'004', N'/ClassisPlan/index', NULL, N'地下空间开发', N'main', 5, 1, NULL, NULL)
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (24, N'005', N'新闻中心', N'0', N'#', NULL, N'新闻中心', N'main', 5, 1, NULL, N'/NewConent/Index')
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (25, N'005001', N'行业动态', N'005', N'/NewConent/Industry', NULL, N'行业动态', N'main', 1, 1, NULL, NULL)
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (26, N'005002', N'同辰新闻', N'005', N'/NewConent/New', NULL, N'同辰新闻', N'main', 2, 1, NULL, NULL)
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (27, N'006', N'英才加盟', N'0', N'/JoinIn/Index', NULL, N'英才加盟', N'main', 6, 1, NULL, NULL)
INSERT [dbo].[tb_Menu_B] ([menu_Id], [menu_Code], [menu_Name], [menu_FatherId], [menu_Link], [menu_Image], [menu_Title], [menu_Target], [menu_Sort], [menu_Stage], [menu_Note], [menu_Link_temp]) VALUES (28, N'007', N'联系我们', N'0', N'/ContactUs/Index', NULL, N'联系我们', N'main', 7, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[tb_Menu_B] OFF
SET IDENTITY_INSERT [dbo].[tb_NewsCenter] ON 

INSERT [dbo].[tb_NewsCenter] ([ID], [ArticlesDate], [ArticlesType], [ArticlesContent], [ArticlesTitle], [CommentariesID], [Creationtime], [CreationPeople], [ImgPath]) VALUES (1, CAST(N'2016-09-03T00:00:00.000' AS DateTime), N'005001', N'行业动态内容', N'行业动态标题', NULL, CAST(N'2017-12-13T00:00:00.000' AS DateTime), 1, NULL)
INSERT [dbo].[tb_NewsCenter] ([ID], [ArticlesDate], [ArticlesType], [ArticlesContent], [ArticlesTitle], [CommentariesID], [Creationtime], [CreationPeople], [ImgPath]) VALUES (2, CAST(N'2016-09-03T00:00:00.000' AS DateTime), N'005001', N'行业动态内容', N'行业动态标题', NULL, CAST(N'2017-12-13T00:00:00.000' AS DateTime), 1, NULL)
INSERT [dbo].[tb_NewsCenter] ([ID], [ArticlesDate], [ArticlesType], [ArticlesContent], [ArticlesTitle], [CommentariesID], [Creationtime], [CreationPeople], [ImgPath]) VALUES (3, CAST(N'2016-09-03T00:00:00.000' AS DateTime), N'005001', N'行业动态内容', N'行业动态标题', NULL, CAST(N'2017-12-13T00:00:00.000' AS DateTime), 1, NULL)
INSERT [dbo].[tb_NewsCenter] ([ID], [ArticlesDate], [ArticlesType], [ArticlesContent], [ArticlesTitle], [CommentariesID], [Creationtime], [CreationPeople], [ImgPath]) VALUES (4, CAST(N'2016-09-03T00:00:00.000' AS DateTime), N'005001', N'行业动态内容', N'行业动态标题', NULL, CAST(N'2017-12-13T00:00:00.000' AS DateTime), 1, NULL)
INSERT [dbo].[tb_NewsCenter] ([ID], [ArticlesDate], [ArticlesType], [ArticlesContent], [ArticlesTitle], [CommentariesID], [Creationtime], [CreationPeople], [ImgPath]) VALUES (5, CAST(N'2016-09-03T00:00:00.000' AS DateTime), N'005001', N'行业动态内容', N'行业动态标题', NULL, CAST(N'2017-12-13T00:00:00.000' AS DateTime), 1, NULL)
INSERT [dbo].[tb_NewsCenter] ([ID], [ArticlesDate], [ArticlesType], [ArticlesContent], [ArticlesTitle], [CommentariesID], [Creationtime], [CreationPeople], [ImgPath]) VALUES (6, CAST(N'2016-09-03T00:00:00.000' AS DateTime), N'005002', N'行业新闻动态内容', N'行业动新闻态标题', NULL, CAST(N'2017-12-13T00:00:00.000' AS DateTime), 1, NULL)
INSERT [dbo].[tb_NewsCenter] ([ID], [ArticlesDate], [ArticlesType], [ArticlesContent], [ArticlesTitle], [CommentariesID], [Creationtime], [CreationPeople], [ImgPath]) VALUES (7, CAST(N'2016-09-03T00:00:00.000' AS DateTime), N'005002', N'行业新闻动态内容', N'行业新闻态标题', NULL, CAST(N'2017-12-13T00:00:00.000' AS DateTime), 1, NULL)
INSERT [dbo].[tb_NewsCenter] ([ID], [ArticlesDate], [ArticlesType], [ArticlesContent], [ArticlesTitle], [CommentariesID], [Creationtime], [CreationPeople], [ImgPath]) VALUES (8, CAST(N'2016-09-03T00:00:00.000' AS DateTime), N'005002', N'行业动新闻态内容', N'行业动态标题', NULL, CAST(N'2017-12-13T00:00:00.000' AS DateTime), 1, NULL)
INSERT [dbo].[tb_NewsCenter] ([ID], [ArticlesDate], [ArticlesType], [ArticlesContent], [ArticlesTitle], [CommentariesID], [Creationtime], [CreationPeople], [ImgPath]) VALUES (9, CAST(N'2016-09-03T00:00:00.000' AS DateTime), N'005002', N'行业动态内容', N'新闻标题', NULL, CAST(N'2017-12-13T00:00:00.000' AS DateTime), 1, NULL)
INSERT [dbo].[tb_NewsCenter] ([ID], [ArticlesDate], [ArticlesType], [ArticlesContent], [ArticlesTitle], [CommentariesID], [Creationtime], [CreationPeople], [ImgPath]) VALUES (10, CAST(N'2016-09-03T00:00:00.000' AS DateTime), N'005001', N'行业动态内容', N'行业动态标题', NULL, CAST(N'2017-12-13T00:00:00.000' AS DateTime), 1, NULL)
INSERT [dbo].[tb_NewsCenter] ([ID], [ArticlesDate], [ArticlesType], [ArticlesContent], [ArticlesTitle], [CommentariesID], [Creationtime], [CreationPeople], [ImgPath]) VALUES (11, CAST(N'2016-09-03T00:00:00.000' AS DateTime), N'005001', N'行业动态内容', N'行业动态标题', NULL, CAST(N'2017-12-13T00:00:00.000' AS DateTime), 1, NULL)
SET IDENTITY_INSERT [dbo].[tb_NewsCenter] OFF
SET IDENTITY_INSERT [dbo].[tb_Project] ON 

INSERT [dbo].[tb_Project] ([Id], [Project_Name], [Project_Location], [Project_People], [Project_Start], [Project_End], [Project_Description], [Create_date], [Project_ImgId], [Create_People], [Project_Type], [Project_IsShow]) VALUES (1, N'昌乐中心商务区', N'山东省 昌邑市', 11, CAST(N'2014-11-02T00:00:00.000' AS DateTime), CAST(N'2015-01-01T00:00:00.000' AS DateTime), N'本项目位于昌邑市中心城区,周围有完善的公共服务设施,具有独特的地理条件和环境优势.基地地形较为平坦,其南侧为规划中心商务区.小区设计采用整体设计手法,充分考虑多元化的居住空间要求', CAST(N'2017-12-13T00:00:00.000' AS DateTime), 1, 1, N'004001', 0)
INSERT [dbo].[tb_Project] ([Id], [Project_Name], [Project_Location], [Project_People], [Project_Start], [Project_End], [Project_Description], [Create_date], [Project_ImgId], [Create_People], [Project_Type], [Project_IsShow]) VALUES (5, N'测试博物馆', N'山东省 东平县', 12, CAST(N'2015-05-06T00:00:00.000' AS DateTime), CAST(N'2015-07-05T00:00:00.000' AS DateTime), N'该项目位于东平化充分', CAST(N'2017-10-13T00:00:00.000' AS DateTime), 2, 1, N'004001', 0)
INSERT [dbo].[tb_Project] ([Id], [Project_Name], [Project_Location], [Project_People], [Project_Start], [Project_End], [Project_Description], [Create_date], [Project_ImgId], [Create_People], [Project_Type], [Project_IsShow]) VALUES (6, N'上海优山美苑', N'上海市 松江区', 11, CAST(N'2016-11-05T00:00:00.000' AS DateTime), CAST(N'2017-05-08T00:00:00.000' AS DateTime), N'本项目基地呈四边形,其中东、南、西三边环水,北边与城市绿化带相接,与泗陈公路和轨道交通9号线隔”带”相望;南临115米宽的横泾港,景观开阔.加上南北向的两条规划路,形成”井字”路网格局.四通八达.主次分明并与周围城市道路连贯衔接,各个独立片区大多采用外环道路或主干道路,并通过相对二设的入口把片区与片区之间有机的联系起来,交通便捷,导向明确.因此本项目功能结构上呈现为”一心、二轴、五片区”的特点.为了充分利用地块内部水景资源营造滨水城市社区的景观氛围,通过自然水景结合生态绿化,并根据项目客户定位设定的主题水景园林,同时打造别具一格的异国风情.', CAST(N'2017-12-13T00:00:00.000' AS DateTime), 3, 1, N'004002', 0)
INSERT [dbo].[tb_Project] ([Id], [Project_Name], [Project_Location], [Project_People], [Project_Start], [Project_End], [Project_Description], [Create_date], [Project_ImgId], [Create_People], [Project_Type], [Project_IsShow]) VALUES (7, N'海南晟盛花园', N'海南省 海口市', 13, CAST(N'2016-05-05T00:00:00.000' AS DateTime), CAST(N'2016-05-09T00:00:00.000' AS DateTime), N'项目位于海口市西海岸，基地北面直接面向大海，具有良好的自然景观资源。用地东西窄，南北纵深较长，景观面较窄，为了换取更多的景观视野，我们将用地沿长轴方向一分为四，设计三个不同标高场地，使得离海岸线较远的用地也能获取景观视野，感受清爽海风。利用台地之下的空间设置停车库，底层住宅区坐到人车分流，提高小区档次。', CAST(N'2017-12-13T00:00:00.000' AS DateTime), 4, 1, N'004002', 0)
INSERT [dbo].[tb_Project] ([Id], [Project_Name], [Project_Location], [Project_People], [Project_Start], [Project_End], [Project_Description], [Create_date], [Project_ImgId], [Create_People], [Project_Type], [Project_IsShow]) VALUES (8, N'盛利率花园', N'海南省 海口市', 13, CAST(N'2016-05-05T00:00:00.000' AS DateTime), CAST(N'2016-05-09T00:00:00.000' AS DateTime), N'项目位于海口市西海岸，基地北面直接面向大海，具有良好的自然景观资源。用地东西窄，南北纵深较长，景观面较窄，为了换取更多的景观视野，我们将用地沿长轴方向一分为四，设计三个不同标高场地，使得离海岸线较远的用地也能获取景观视野，感受清爽海风。利用台地之下的空间设置停车库，底层住宅区坐到人车分流，提高小区档次。', CAST(N'2017-12-13T00:00:00.000' AS DateTime), 1, 1, N'004002', 1)
INSERT [dbo].[tb_Project] ([Id], [Project_Name], [Project_Location], [Project_People], [Project_Start], [Project_End], [Project_Description], [Create_date], [Project_ImgId], [Create_People], [Project_Type], [Project_IsShow]) VALUES (9, N'欸他花园', N'海南省 海口市', 13, CAST(N'2016-05-08T00:00:00.000' AS DateTime), CAST(N'2016-05-09T00:00:00.000' AS DateTime), N'项目位于海口市西海岸，基地北面直接面向大海，具有良好的自然景观资源。用地东西窄，南北纵深较长，景观面较窄，为了换取更多的景观视野，我们将用地沿长轴方向一分为四，设计三个不同标高场地，使得离海岸线较远的用地也能获取景观视野，感受清爽海风。利用台地之下的空间设置停车库，底层住宅区坐到人车分流，提高小区档次。', CAST(N'2017-12-13T00:00:00.000' AS DateTime), 2, 1, N'004002', 0)
INSERT [dbo].[tb_Project] ([Id], [Project_Name], [Project_Location], [Project_People], [Project_Start], [Project_End], [Project_Description], [Create_date], [Project_ImgId], [Create_People], [Project_Type], [Project_IsShow]) VALUES (11, N'“文昌花园”星级酒店及酒店式公寓', N'湖南省 长沙市', 6, CAST(N'2014-06-18T00:00:00.000' AS DateTime), CAST(N'2015-09-04T00:00:00.000' AS DateTime), N'文昌花园项目规划分四期进行建设.一期为东部高档住宅部分,其中包括三栋高层住宅及其地下车库.二期为东南部分的高级酒店部分,包括餐饮、娱乐、住宿等服务功能.三期为西侧综合商业办公部分,主要包括一家大型超市、商务办公楼及员工宿舍.四期为西北部普通住宅,包括数栋多层及高层住宅.一至四期彼此相邻又各自成独立的道路系统,互不干扰.故在整体规划布局上呈现出”一心、一轴、多组团”的特点并充分利用了周边的自然景观资源.同时体现高起点、高标准、高水平、现代化的要求,做到社区效益环境效益和经济效益的有机结合;努力塑造一个兼居住、酒店康乐、商业及办公为一体的现代化综合社区.', CAST(N'2017-12-13T00:00:00.000' AS DateTime), 5, 1, N'004003', 1)
INSERT [dbo].[tb_Project] ([Id], [Project_Name], [Project_Location], [Project_People], [Project_Start], [Project_End], [Project_Description], [Create_date], [Project_ImgId], [Create_People], [Project_Type], [Project_IsShow]) VALUES (12, N'花园”星级酒店及酒店式公寓', N'湖南省 长沙市', 6, CAST(N'2014-09-18T00:00:00.000' AS DateTime), CAST(N'2015-09-04T00:00:00.000' AS DateTime), N'文昌花园项目规划分四期进行建设.一期为东部高档住宅部分,其中包括三栋高层住宅及其地下车库.二期为东南部分的高级酒店部分,包括餐饮、娱乐、住宿等服务功能.三期为西侧综合商业办公部分,主要包括一家大型超市、商务办公楼及员工宿舍.四期为西北部普通住宅,包括数栋多层及高层住宅.一至四期彼此相邻又各自成独立的道路系统,互不干扰.故在整体规划布局上呈现出”一心、一轴、多组团”的特点并充分利用了周边的自然景观资源.同时体现高起点、高标准、高水平、现代化的要求,做到社区效益环境效益和经济效益的有机结合;努力塑造一个兼居住、酒店康乐、商业及办公为一体的现代化综合社区.', CAST(N'2017-12-13T00:00:00.000' AS DateTime), 3, 1, N'004003', 0)
INSERT [dbo].[tb_Project] ([Id], [Project_Name], [Project_Location], [Project_People], [Project_Start], [Project_End], [Project_Description], [Create_date], [Project_ImgId], [Create_People], [Project_Type], [Project_IsShow]) VALUES (13, N'“文昌花园”星级及酒式公寓', N'湖南省 长沙市', 6, CAST(N'2014-06-18T00:00:00.000' AS DateTime), CAST(N'2015-09-04T00:00:00.000' AS DateTime), N'文昌花园项目规划分四期进行建设.一期为东部高档住宅部分,其中包括三栋高层住宅及其地下车库.二期为东南部分的高级酒店部分,包括餐饮、娱乐、住宿等服务功能.三期为西侧综合商业办公部分,主要包括一家大型超市、商务办公楼及员工宿舍.四期为西北部普通住宅,包括数栋多层及高层住宅.一至四期彼此相邻又各自成独立的道路系统,互不干扰.故在整体规划布局上呈现出”一心、一轴、多组团”的特点并充分利用了周边的自然景观资源.同时体现高起点、高标准、高水平、现代化的要求,做到社区效益环境效益和经济效益的有机结合;努力塑造一个兼居住、酒店康乐、商业及办公为一体的现代化综合社区.', CAST(N'2017-12-13T00:00:00.000' AS DateTime), 4, 1, N'004003', 0)
INSERT [dbo].[tb_Project] ([Id], [Project_Name], [Project_Location], [Project_People], [Project_Start], [Project_End], [Project_Description], [Create_date], [Project_ImgId], [Create_People], [Project_Type], [Project_IsShow]) VALUES (14, N'“文昌星级酒公寓', N'湖南省 长沙市', 6, CAST(N'2014-06-18T00:00:00.000' AS DateTime), CAST(N'2015-09-04T00:00:00.000' AS DateTime), N'文昌花园项目规划分四期进行建设.一期为东部高档住宅部分,其中包括三栋高层住宅及其地下车库.二期为东南部分的高级酒店部分,包括餐饮、娱乐、住宿等服务功能.三期为西侧综合商业办公部分,主要包括一家大型超市、商务办公楼及员工宿舍.四期为西北部普通住宅,包括数栋多层及高层住宅.一至四期彼此相邻又各自成独立的道路系统,互不干扰.故在整体规划布局上呈现出”一心、一轴、多组团”的特点并充分利用了周边的自然景观资源.同时体现高起点、高标准、高水平、现代化的要求,做到社区效益环境效益和经济效益的有机结合;努力塑造一个兼居住、酒店康乐、商业及办公为一体的现代化综合社区.', CAST(N'2017-12-13T00:00:00.000' AS DateTime), 5, 1, N'004003', 0)
INSERT [dbo].[tb_Project] ([Id], [Project_Name], [Project_Location], [Project_People], [Project_Start], [Project_End], [Project_Description], [Create_date], [Project_ImgId], [Create_People], [Project_Type], [Project_IsShow]) VALUES (15, N'店及酒店式公寓', N'湖南省 长沙市', 6, CAST(N'2014-06-18T00:00:00.000' AS DateTime), CAST(N'2015-09-04T00:00:00.000' AS DateTime), N'文昌花园项目规划分四期进行建设.一期为东部高档住宅部分,其中包括三栋高层住宅及其地下车库.二期为东南部分的高级酒店部分,包括餐饮、娱乐、住宿等服务功能.三期为西侧综合商业办公部分,主要包括一家大型超市、商务办公楼及员工宿舍.四期为西北部普通住宅,包括数栋多层及高层住宅.一至四期彼此相邻又各自成独立的道路系统,互不干扰.故在整体规划布局上呈现出”一心、一轴、多组团”的特点并充分利用了周边的自然景观资源.同时体现高起点、高标准、高水平、现代化的要求,做到社区效益环境效益和经济效益的有机结合;努力塑造一个兼居住、酒店康乐、商业及办公为一体的现代化综合社区.', CAST(N'2017-12-13T00:00:00.000' AS DateTime), 4, 1, N'004003', 0)
INSERT [dbo].[tb_Project] ([Id], [Project_Name], [Project_Location], [Project_People], [Project_Start], [Project_End], [Project_Description], [Create_date], [Project_ImgId], [Create_People], [Project_Type], [Project_IsShow]) VALUES (16, N'芦山县第三小学', N'四川省 雅安市', 7, CAST(N'2016-12-08T00:00:00.000' AS DateTime), CAST(N'2017-05-03T00:00:00.000' AS DateTime), N'重建项目,该基地位于雅安市庐山县内庐山河东岸.东北边邻规划道路,东南邻平安大道沿街商业用地,西南邻东风路沿街为商业用地,西侧毗邻芦山县汽车站,用地距芦山县汽车站围墙24米.且用地方向偏45°利用率不高、南北高差2米左右.故该设计充分合理规划用地,将校园主入口设在东北侧规定道路上,由于距离道路交叉口较近,学校入口处形成后退广场,减轻交通压力.并在满足各项使用的规范的情况下创造出校园集中绿化,同时保证教学楼均正南朝北向.', CAST(N'2017-12-13T00:00:00.000' AS DateTime), 5, 1, N'004004', 1)
INSERT [dbo].[tb_Project] ([Id], [Project_Name], [Project_Location], [Project_People], [Project_Start], [Project_End], [Project_Description], [Create_date], [Project_ImgId], [Create_People], [Project_Type], [Project_IsShow]) VALUES (17, N'芦第学', N'四川省 雅安市', 7, CAST(N'2016-12-08T00:00:00.000' AS DateTime), CAST(N'2017-05-03T00:00:00.000' AS DateTime), N'本项目为四川省汶川地震灾后学校重建项目,该基地位于雅安市庐山县内庐山河东岸.东北边邻规划道路,东南邻平安大道沿街商业用地,西南邻东风路沿街为商业用地,西侧毗邻芦山县汽车站,用地距芦山县汽车站围墙24米.且用地方向偏45°利用率不高、南北高差2米左右.故该设计充分合理规划用地,将校园主入口设在东北侧规定道路上,由于距离道路交叉口较近,学校入口处形成后退广场,减轻交通压力.并在满足各项使用的规范的情况下创造出校园集中绿化,同时保证教学楼均正南朝北向.', CAST(N'2017-12-13T00:00:00.000' AS DateTime), 3, 1, N'004004', 0)
INSERT [dbo].[tb_Project] ([Id], [Project_Name], [Project_Location], [Project_People], [Project_Start], [Project_End], [Project_Description], [Create_date], [Project_ImgId], [Create_People], [Project_Type], [Project_IsShow]) VALUES (18, N'三小学', N'四川省 雅安市', 7, CAST(N'2016-12-08T00:00:00.000' AS DateTime), CAST(N'2017-05-03T00:00:00.000' AS DateTime), N'四川省汶川地震灾后学校重建项目,该基地位于雅安市庐山县内庐山河东岸.东北边邻规划道路,东南邻平安大道沿街商业用地,西南邻东风路沿街为商业用地,西侧毗邻芦山县汽车站,用地距芦山县汽车站围墙24米.且用地方向偏45°利用率不高、南北高差2米左右.故该设计充分合理规划用地,将校园主入口设在东北侧规定道路上,由于距离道路交叉口较近,学校入口处形成后退广场,减轻交通压力.并在满足各项使用的规范的情况下创造出校园集中向.', CAST(N'2017-12-13T00:00:00.000' AS DateTime), 2, 1, N'004004', 0)
INSERT [dbo].[tb_Project] ([Id], [Project_Name], [Project_Location], [Project_People], [Project_Start], [Project_End], [Project_Description], [Create_date], [Project_ImgId], [Create_People], [Project_Type], [Project_IsShow]) VALUES (19, N'第三小学', N'四川省 雅安市', 7, CAST(N'2016-12-08T00:00:00.000' AS DateTime), CAST(N'2017-05-03T00:00:00.000' AS DateTime), N'本项目为四川省汶川地震灾后学校重建项目,该基地位于雅安市庐山县内庐山河东岸.东北边邻规划道路,东南邻平安大南朝北向.', CAST(N'2017-12-13T00:00:00.000' AS DateTime), 1, 1, N'004004', 0)
INSERT [dbo].[tb_Project] ([Id], [Project_Name], [Project_Location], [Project_People], [Project_Start], [Project_End], [Project_Description], [Create_date], [Project_ImgId], [Create_People], [Project_Type], [Project_IsShow]) VALUES (20, N'县第三小学', N'四川省 雅安市', 7, CAST(N'2016-12-08T00:00:00.000' AS DateTime), CAST(N'2017-05-03T00:00:00.000' AS DateTime), N'汶川地震灾后学校重建项目,该基地位于雅安市庐山县内庐山河东岸.东北边邻规划道路,东南邻平安大道沿街商业用地,西南邻东风路沿街为商业用地,西侧毗邻芦山县汽车站,用地距芦山县汽车站围墙24米.且用地方向偏45°利用率不高、南北高差2米左右.故该设计充分合理规划用地,将校园主入口设在东北侧规定道路上,由于距离道路交叉口较近,学校入口处形成后退广场,减轻交通压力.并在满足各项使用的规范的情况下创造出校园集中绿化,同时保证教学楼', CAST(N'2017-12-13T00:00:00.000' AS DateTime), 2, 1, N'004004', 1)
INSERT [dbo].[tb_Project] ([Id], [Project_Name], [Project_Location], [Project_People], [Project_Start], [Project_End], [Project_Description], [Create_date], [Project_ImgId], [Create_People], [Project_Type], [Project_IsShow]) VALUES (21, N'芦山小学', N'四川省 雅安市', 7, CAST(N'2016-12-08T00:00:00.000' AS DateTime), CAST(N'2017-05-03T00:00:00.000' AS DateTime), N'本项目为四川省汶川地震灾后学校重建项目,该基地位于雅安市庐山县内庐山河东岸.东北边邻规划道路,东南邻平安大道沿街商业用地,西南邻东风路沿街为商业用地,西侧毗邻北向.', CAST(N'2017-12-13T00:00:00.000' AS DateTime), 3, 1, N'004004', 0)
INSERT [dbo].[tb_Project] ([Id], [Project_Name], [Project_Location], [Project_People], [Project_Start], [Project_End], [Project_Description], [Create_date], [Project_ImgId], [Create_People], [Project_Type], [Project_IsShow]) VALUES (22, N'山东新昌集团住宅及景观设计', N'山东省 昌邑县', 5, CAST(N'2012-11-19T00:00:00.000' AS DateTime), CAST(N'2013-08-09T00:00:00.000' AS DateTime), N'昌邑酒店占地5.1万平方米，为山东新昌集团开发的一座五星级度假酒店，现代简约风格，以自然流畅为主要的表现手法，临近昌邑市湿地公园。', CAST(N'2017-12-13T00:00:00.000' AS DateTime), 4, 1, N'004005', 0)
INSERT [dbo].[tb_Project] ([Id], [Project_Name], [Project_Location], [Project_People], [Project_Start], [Project_End], [Project_Description], [Create_date], [Project_ImgId], [Create_People], [Project_Type], [Project_IsShow]) VALUES (23, N'山东新昌集团住宅及景观设计', N'山东省 昌邑县', 5, CAST(N'2012-11-19T00:00:00.000' AS DateTime), CAST(N'2013-08-09T00:00:00.000' AS DateTime), N'昌邑酒店占地5.1万平方米，为山东新昌集团开发的一座五星级度假酒店，现代简约风格，以自然流畅为主要的表现手法，临近昌邑市湿地公园。', CAST(N'2017-12-13T00:00:00.000' AS DateTime), 1, 1, N'004005', 0)
SET IDENTITY_INSERT [dbo].[tb_Project] OFF
SET IDENTITY_INSERT [dbo].[tb_UploadFile] ON 

INSERT [dbo].[tb_UploadFile] ([Id], [DirId], [FileName], [Path], [Path_sm], [Uploader], [CreateTime], [FileSize], [FileType], [Sort]) VALUES (1, 5, N'图片.jpg', N'/UploadFile/5/115e0d7e-4efd-4efd-883a-f813edb90651_20170726094426.png', NULL, 1, CAST(N'2017-12-14T00:00:00.000' AS DateTime), N'3MB', 1, NULL)
INSERT [dbo].[tb_UploadFile] ([Id], [DirId], [FileName], [Path], [Path_sm], [Uploader], [CreateTime], [FileSize], [FileType], [Sort]) VALUES (2, 5, N'图片.jpg', N'/UploadFile/5/115e0d7e-4efd-4efd-883a-f813edb90651_20170726094426.png', NULL, 1, CAST(N'2017-12-14T00:00:00.000' AS DateTime), N'3MB', 1, NULL)
INSERT [dbo].[tb_UploadFile] ([Id], [DirId], [FileName], [Path], [Path_sm], [Uploader], [CreateTime], [FileSize], [FileType], [Sort]) VALUES (3, 5, N'图片.jpg', N'/UploadFile/5/115e0d7e-4efd-4efd-883a-f813edb90651_20170726094426.png', NULL, 1, CAST(N'2017-12-14T00:00:00.000' AS DateTime), N'3MB', 1, NULL)
INSERT [dbo].[tb_UploadFile] ([Id], [DirId], [FileName], [Path], [Path_sm], [Uploader], [CreateTime], [FileSize], [FileType], [Sort]) VALUES (4, 5, N'图片.jpg', N'/UploadFile/5/115e0d7e-4efd-4efd-883a-f813edb90651_20170726094426.png', NULL, 1, CAST(N'2017-12-14T00:00:00.000' AS DateTime), N'3MB', 1, NULL)
INSERT [dbo].[tb_UploadFile] ([Id], [DirId], [FileName], [Path], [Path_sm], [Uploader], [CreateTime], [FileSize], [FileType], [Sort]) VALUES (5, 5, N'图片.jpg', N'/UploadFile/5/115e0d7e-4efd-4efd-883a-f813edb90651_20170726094426.png', NULL, 1, CAST(N'2017-12-14T00:00:00.000' AS DateTime), N'3MB', 1, NULL)
SET IDENTITY_INSERT [dbo].[tb_UploadFile] OFF
ALTER TABLE [dbo].[tb_ClientMessage] ADD  CONSTRAINT [DF_tb_ClientMessage_SubmitDate]  DEFAULT (getdate()) FOR [SubmitDate]
GO
ALTER TABLE [dbo].[tb_ClientMessage] ADD  CONSTRAINT [DF_tb_ClientMessage_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[tb_ClientMessage] ADD  CONSTRAINT [DF_tb_ClientMessage_MessType]  DEFAULT ((0)) FOR [MessType]
GO
ALTER TABLE [dbo].[tb_Project] ADD  DEFAULT ((0)) FOR [Project_IsShow]
GO
ALTER TABLE [dbo].[tb_UploadFile] ADD  CONSTRAINT [DF_tb_UploadFile1_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:未删除;1删除' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ClientMessage', @level2type=N'COLUMN',@level2name=N'IsDelete'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:不显示;1:显示' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ClientMessage', @level2type=N'COLUMN',@level2name=N'IsDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:未看;1 已开' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ClientMessage', @level2type=N'COLUMN',@level2name=N'IsLook'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_ClientMessage', @level2type=N'COLUMN',@level2name=N'MessType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文章时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_NewsCenter', @level2type=N'COLUMN',@level2name=N'ArticlesDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'文字类型：005001=行业动态；005002=新闻' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_NewsCenter', @level2type=N'COLUMN',@level2name=N'ArticlesType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_NewsCenter', @level2type=N'COLUMN',@level2name=N'ArticlesContent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'标题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tb_NewsCenter', @level2type=N'COLUMN',@level2name=N'ArticlesTitle'
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
USE [master]
GO
ALTER DATABASE [lm] SET  READ_WRITE 
GO
