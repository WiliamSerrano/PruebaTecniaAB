USE [master]
GO
/****** Object:  Database [DBVENTAS]    Script Date: 23/6/2024 17:12:27 ******/
CREATE DATABASE [DBVENTAS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBVENTAS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DBVENTAS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DBVENTAS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DBVENTAS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DBVENTAS] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBVENTAS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBVENTAS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBVENTAS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBVENTAS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBVENTAS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBVENTAS] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBVENTAS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DBVENTAS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBVENTAS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBVENTAS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBVENTAS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBVENTAS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBVENTAS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBVENTAS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBVENTAS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBVENTAS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DBVENTAS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBVENTAS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBVENTAS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBVENTAS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBVENTAS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBVENTAS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBVENTAS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBVENTAS] SET RECOVERY FULL 
GO
ALTER DATABASE [DBVENTAS] SET  MULTI_USER 
GO
ALTER DATABASE [DBVENTAS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBVENTAS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBVENTAS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBVENTAS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DBVENTAS] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DBVENTAS] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DBVENTAS', N'ON'
GO
ALTER DATABASE [DBVENTAS] SET QUERY_STORE = ON
GO
ALTER DATABASE [DBVENTAS] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DBVENTAS]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 23/6/2024 17:12:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[IdProduct] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[UnitPrice] [money] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 23/6/2024 17:12:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[IdSale] [int] IDENTITY(1,1) NOT NULL,
	[NameClient] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[Mail] [nvarchar](100) NOT NULL,
	[TotalPrice] [money] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[PaidDate] [datetime] NOT NULL,
	[IsPaid] [bit] NOT NULL,
 CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED 
(
	[IdSale] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesProducts]    Script Date: 23/6/2024 17:12:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesProducts](
	[IdSalesProduct] [int] IDENTITY(1,1) NOT NULL,
	[IdSale] [int] NOT NULL,
	[IdProduct] [int] NOT NULL,
 CONSTRAINT [PK_SalesProducts] PRIMARY KEY CLUSTERED 
(
	[IdSalesProduct] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 23/6/2024 17:12:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[IdUser] [int] IDENTITY(1,1) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Mail] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([IdProduct], [ProductName], [UnitPrice], [Quantity], [Active]) VALUES (2, N'Pepsi', 1.1500, 5, 0)
INSERT [dbo].[Product] ([IdProduct], [ProductName], [UnitPrice], [Quantity], [Active]) VALUES (3, N'Coca Cola', 0.8500, 5, 1)
INSERT [dbo].[Product] ([IdProduct], [ProductName], [UnitPrice], [Quantity], [Active]) VALUES (4, N'Del Valle', 1.4200, 5, 1)
INSERT [dbo].[Product] ([IdProduct], [ProductName], [UnitPrice], [Quantity], [Active]) VALUES (5, N'Te Lipton', 1.3300, 5, 1)
INSERT [dbo].[Product] ([IdProduct], [ProductName], [UnitPrice], [Quantity], [Active]) VALUES (6, N'Nachos', 0.1500, 9, 1)
INSERT [dbo].[Product] ([IdProduct], [ProductName], [UnitPrice], [Quantity], [Active]) VALUES (9, N'Ideapad lll', 1000.0000, 9, 1)
INSERT [dbo].[Product] ([IdProduct], [ProductName], [UnitPrice], [Quantity], [Active]) VALUES (10, N'Mouse', 15.0000, 10, 1)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Sales] ON 

INSERT [dbo].[Sales] ([IdSale], [NameClient], [Description], [Mail], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (1, N'Pablo Juarez', N'venta ejemplo', N'venta@gmail.com', 9.1700, CAST(N'2024-06-17T00:40:36.120' AS DateTime), CAST(N'2024-06-17T09:10:37.920' AS DateTime), 1)
INSERT [dbo].[Sales] ([IdSale], [NameClient], [Description], [Mail], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (2, N'Maria', N'Venta 2', N'Maria@gmail.com', 18.8800, CAST(N'2024-06-17T01:06:12.593' AS DateTime), CAST(N'2024-06-17T01:06:12.593' AS DateTime), 1)
INSERT [dbo].[Sales] ([IdSale], [NameClient], [Description], [Mail], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (3, N'Carlos', N'Venta humero 3', N'Carlos@gmail.com', 9.4300, CAST(N'2024-06-17T08:32:03.327' AS DateTime), CAST(N'2024-06-17T09:13:31.877' AS DateTime), 1)
INSERT [dbo].[Sales] ([IdSale], [NameClient], [Description], [Mail], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (4, N'Mario', N'Ventade prueba 4', N'Mario@gmail.com', 9.4300, CAST(N'2024-06-17T09:12:03.507' AS DateTime), CAST(N'2024-06-17T09:12:30.040' AS DateTime), 1)
INSERT [dbo].[Sales] ([IdSale], [NameClient], [Description], [Mail], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (5, N'Maria Delgado', N'Nueva venta', N'Maria@gmail.com', 9.4300, CAST(N'2024-06-17T09:13:05.350' AS DateTime), CAST(N'2024-06-23T14:31:16.657' AS DateTime), 1)
INSERT [dbo].[Sales] ([IdSale], [NameClient], [Description], [Mail], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (6, N'Fernando', N'Venta Nueva 1', N'Fer@gmail.com', 1012.8500, CAST(N'2024-06-21T23:49:06.177' AS DateTime), CAST(N'2024-06-22T01:17:55.250' AS DateTime), 1)
INSERT [dbo].[Sales] ([IdSale], [NameClient], [Description], [Mail], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (8, N'Enrique Sanchez', N'Nueva venta 3', N'Enrique@gmail.com', 1004.9000, CAST(N'2024-06-22T00:28:05.223' AS DateTime), CAST(N'2024-06-22T00:28:05.223' AS DateTime), 0)
INSERT [dbo].[Sales] ([IdSale], [NameClient], [Description], [Mail], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (9, N'Gabriel Zamora', N'Nueva venta 4', N'zamora@gmail.com', 6013.1800, CAST(N'2024-06-22T00:30:23.050' AS DateTime), CAST(N'2024-06-22T01:03:44.063' AS DateTime), 1)
INSERT [dbo].[Sales] ([IdSale], [NameClient], [Description], [Mail], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (10, N'Fernando', N'venta 5', N'fer@gmail.com', 1004.9000, CAST(N'2024-06-22T00:33:20.877' AS DateTime), CAST(N'2024-06-22T01:03:35.160' AS DateTime), 1)
INSERT [dbo].[Sales] ([IdSale], [NameClient], [Description], [Mail], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (11, N'Luis', N'Venta prueba nueva', N'luis@gmail.com', 1004.9000, CAST(N'2024-06-23T00:12:37.940' AS DateTime), CAST(N'2024-06-23T00:12:37.940' AS DateTime), 0)
INSERT [dbo].[Sales] ([IdSale], [NameClient], [Description], [Mail], [TotalPrice], [CreationDate], [PaidDate], [IsPaid]) VALUES (12, N'Juana', N'venta nueva', N'juana@gmail.com', 1004.9000, CAST(N'2024-06-23T00:18:48.627' AS DateTime), CAST(N'2024-06-23T01:03:07.953' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Sales] OFF
GO
SET IDENTITY_INSERT [dbo].[SalesProducts] ON 

INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (1, 1, 2)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (2, 1, 3)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (3, 1, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (4, 1, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (5, 1, 5)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (6, 1, 5)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (7, 1, 5)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (8, 1, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (9, 1, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (10, 1, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (11, 1, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (12, 2, 2)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (13, 2, 2)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (14, 2, 2)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (15, 2, 2)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (16, 2, 2)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (17, 2, 3)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (18, 2, 3)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (19, 2, 3)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (20, 2, 3)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (21, 2, 3)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (22, 2, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (23, 2, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (24, 2, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (25, 2, 5)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (26, 2, 5)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (27, 2, 5)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (28, 2, 5)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (29, 2, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (30, 2, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (31, 2, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (32, 2, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (33, 3, 2)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (34, 3, 2)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (35, 3, 3)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (36, 3, 3)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (37, 3, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (38, 3, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (39, 3, 5)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (40, 3, 5)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (41, 3, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (42, 3, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (43, 3, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (44, 4, 2)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (45, 4, 2)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (46, 4, 3)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (47, 4, 3)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (48, 4, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (49, 4, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (50, 4, 5)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (51, 4, 5)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (52, 4, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (53, 4, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (54, 4, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (55, 5, 2)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (56, 5, 2)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (57, 5, 3)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (58, 5, 3)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (59, 5, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (60, 5, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (61, 5, 5)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (62, 5, 5)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (63, 5, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (64, 5, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (65, 5, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (66, 6, 2)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (67, 6, 3)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (68, 6, 3)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (69, 6, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (70, 6, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (71, 6, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (72, 6, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (73, 6, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (74, 6, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (75, 6, 5)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (76, 6, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (77, 6, 9)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (78, 8, 2)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (79, 8, 3)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (80, 8, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (81, 8, 5)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (82, 8, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (83, 8, 9)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (84, 9, 2)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (85, 9, 3)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (86, 9, 3)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (87, 9, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (88, 9, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (89, 9, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (90, 9, 5)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (91, 9, 5)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (92, 9, 5)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (93, 9, 5)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (94, 9, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (95, 9, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (96, 9, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (97, 9, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (98, 9, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (99, 9, 9)
GO
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (100, 9, 9)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (101, 9, 9)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (102, 9, 9)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (103, 9, 9)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (104, 9, 9)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (105, 10, 2)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (106, 10, 3)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (107, 10, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (108, 10, 5)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (109, 10, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (110, 10, 9)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (111, 11, 2)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (112, 11, 3)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (113, 11, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (114, 11, 5)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (115, 11, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (116, 11, 9)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (117, 12, 2)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (118, 12, 3)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (119, 12, 4)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (120, 12, 5)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (121, 12, 6)
INSERT [dbo].[SalesProducts] ([IdSalesProduct], [IdSale], [IdProduct]) VALUES (122, 12, 9)
SET IDENTITY_INSERT [dbo].[SalesProducts] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([IdUser], [Role], [FirstName], [LastName], [Mail], [Password]) VALUES (2, N'Seller', N'Pablo', N'Juarez', N'pablo@gmail.com', N'12345')
INSERT [dbo].[User] ([IdUser], [Role], [FirstName], [LastName], [Mail], [Password]) VALUES (3, N'Accountant', N'Maria', N'Delgado', N'maria@gmail.com', N'12345')
INSERT [dbo].[User] ([IdUser], [Role], [FirstName], [LastName], [Mail], [Password]) VALUES (5, N'Administrador', N'William', N'Serrano', N'william@gmail.com', N'12345')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[SalesProducts]  WITH CHECK ADD  CONSTRAINT [FK_SalesProducts_Product] FOREIGN KEY([IdProduct])
REFERENCES [dbo].[Product] ([IdProduct])
GO
ALTER TABLE [dbo].[SalesProducts] CHECK CONSTRAINT [FK_SalesProducts_Product]
GO
ALTER TABLE [dbo].[SalesProducts]  WITH CHECK ADD  CONSTRAINT [FK_SalesProducts_Sales] FOREIGN KEY([IdSale])
REFERENCES [dbo].[Sales] ([IdSale])
GO
ALTER TABLE [dbo].[SalesProducts] CHECK CONSTRAINT [FK_SalesProducts_Sales]
GO
USE [master]
GO
ALTER DATABASE [DBVENTAS] SET  READ_WRITE 
GO
