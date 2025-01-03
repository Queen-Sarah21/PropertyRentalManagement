USE [master]
GO
/****** Object:  Database [Property_Rental_DB]    Script Date: 2024-12-03 10:53:53 PM ******/
CREATE DATABASE [Property_Rental_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Property_Rental_DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Property_Rental_DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Property_Rental_DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Property_Rental_DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Property_Rental_DB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Property_Rental_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Property_Rental_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Property_Rental_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Property_Rental_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Property_Rental_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Property_Rental_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [Property_Rental_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Property_Rental_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Property_Rental_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Property_Rental_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Property_Rental_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Property_Rental_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Property_Rental_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Property_Rental_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Property_Rental_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Property_Rental_DB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Property_Rental_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Property_Rental_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Property_Rental_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Property_Rental_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Property_Rental_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Property_Rental_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Property_Rental_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Property_Rental_DB] SET RECOVERY FULL 
GO
ALTER DATABASE [Property_Rental_DB] SET  MULTI_USER 
GO
ALTER DATABASE [Property_Rental_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Property_Rental_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Property_Rental_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Property_Rental_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Property_Rental_DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Property_Rental_DB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Property_Rental_DB', N'ON'
GO
ALTER DATABASE [Property_Rental_DB] SET QUERY_STORE = ON
GO
ALTER DATABASE [Property_Rental_DB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Property_Rental_DB]
GO
/****** Object:  Table [dbo].[Apartments]    Script Date: 2024-12-03 10:53:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Apartments](
	[ApartmentNum] [int] IDENTITY(1,1) NOT NULL,
	[BuildingCode] [nvarchar](3) NOT NULL,
	[Rooms] [int] NOT NULL,
	[Bathrooms] [int] NOT NULL,
	[Description] [nvarchar](255) NULL,
	[Rent] [decimal](10, 2) NOT NULL,
	[StatusId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ApartmentNum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Appointments]    Script Date: 2024-12-03 10:53:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointments](
	[AppointmentId] [int] IDENTITY(1,1) NOT NULL,
	[TenantId] [int] NOT NULL,
	[ManagerId] [int] NOT NULL,
	[ApartmentNum] [int] NOT NULL,
	[ScheduleId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AppointmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Buildings]    Script Date: 2024-12-03 10:53:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Buildings](
	[BuildingCode] [nvarchar](3) NOT NULL,
	[LandlordId] [int] NOT NULL,
	[ManagerId] [int] NOT NULL,
	[BuildingName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NULL,
	[Address] [nvarchar](50) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
	[Province] [nvarchar](50) NOT NULL,
	[ZipCode] [nvarchar](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[BuildingCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Landlords]    Script Date: 2024-12-03 10:53:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Landlords](
	[LandlordId] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[LandlordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Leases]    Script Date: 2024-12-03 10:53:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Leases](
	[LeaseId] [nvarchar](4) NOT NULL,
	[TenantId] [int] NOT NULL,
	[ApartmentNum] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[MonthlyRent] [decimal](10, 2) NOT NULL,
	[StatusId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LeaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Managers]    Script Date: 2024-12-03 10:53:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Managers](
	[ManagerId] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[ManagerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 2024-12-03 10:53:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[MessageId] [int] IDENTITY(1,1) NOT NULL,
	[SenderId] [int] NOT NULL,
	[ReceiverId] [int] NOT NULL,
	[MessageSubject] [nvarchar](50) NULL,
	[MessageBody] [nvarchar](255) NULL,
	[MessageDate] [datetime] NOT NULL,
	[StatusId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 2024-12-03 10:53:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[LeaseId] [nvarchar](4) NOT NULL,
	[TenantId] [int] NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[PaymentDate] [date] NOT NULL,
	[StatusId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedules]    Script Date: 2024-12-03 10:53:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedules](
	[ScheduleId] [int] IDENTITY(1,1) NOT NULL,
	[ManagerId] [int] NOT NULL,
	[ScheduleDate] [date] NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 2024-12-03 10:53:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statuses](
	[StatusId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](60) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tenants]    Script Date: 2024-12-03 10:53:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tenants](
	[TenantId] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[TenantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2024-12-03 10:53:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Apartments] ON 

INSERT [dbo].[Apartments] ([ApartmentNum], [BuildingCode], [Rooms], [Bathrooms], [Description], [Rent], [StatusId]) VALUES (1, N'B05', 4, 2, N'Bright apartment with hardwood floors.', CAST(1200.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[Apartments] ([ApartmentNum], [BuildingCode], [Rooms], [Bathrooms], [Description], [Rent], [StatusId]) VALUES (2, N'B01', 2, 2, N'Compact apartment, perfect for singles.', CAST(900.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[Apartments] ([ApartmentNum], [BuildingCode], [Rooms], [Bathrooms], [Description], [Rent], [StatusId]) VALUES (3, N'B01', 4, 2, N'Spacious family apartment.', CAST(1500.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[Apartments] ([ApartmentNum], [BuildingCode], [Rooms], [Bathrooms], [Description], [Rent], [StatusId]) VALUES (4, N'B01', 3, 1, N'Recently renovated, modern interior.', CAST(1250.00 AS Decimal(10, 2)), 3)
INSERT [dbo].[Apartments] ([ApartmentNum], [BuildingCode], [Rooms], [Bathrooms], [Description], [Rent], [StatusId]) VALUES (5, N'B02', 2, 1, N'Cozy apartment with great lighting.', CAST(950.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[Apartments] ([ApartmentNum], [BuildingCode], [Rooms], [Bathrooms], [Description], [Rent], [StatusId]) VALUES (6, N'B02', 3, 2, N'Modern apartment with open layout.', CAST(1300.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[Apartments] ([ApartmentNum], [BuildingCode], [Rooms], [Bathrooms], [Description], [Rent], [StatusId]) VALUES (7, N'B02', 1, 1, N'Affordable studio.', CAST(800.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[Apartments] ([ApartmentNum], [BuildingCode], [Rooms], [Bathrooms], [Description], [Rent], [StatusId]) VALUES (8, N'B02', 2, 1, N'Comfortable and peaceful surroundings.', CAST(1000.00 AS Decimal(10, 2)), 3)
INSERT [dbo].[Apartments] ([ApartmentNum], [BuildingCode], [Rooms], [Bathrooms], [Description], [Rent], [StatusId]) VALUES (9, N'B03', 5, 3, N'Luxury penthouse.', CAST(3000.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[Apartments] ([ApartmentNum], [BuildingCode], [Rooms], [Bathrooms], [Description], [Rent], [StatusId]) VALUES (10, N'B03', 4, 2, N'Apartment with private terrace.', CAST(2000.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[Apartments] ([ApartmentNum], [BuildingCode], [Rooms], [Bathrooms], [Description], [Rent], [StatusId]) VALUES (11, N'B03', 2, 1, N'Stylish apartment with city view.', CAST(1500.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[Apartments] ([ApartmentNum], [BuildingCode], [Rooms], [Bathrooms], [Description], [Rent], [StatusId]) VALUES (12, N'B03', 3, 2, N'High-end finishes, great location.', CAST(1800.00 AS Decimal(10, 2)), 3)
INSERT [dbo].[Apartments] ([ApartmentNum], [BuildingCode], [Rooms], [Bathrooms], [Description], [Rent], [StatusId]) VALUES (13, N'B04', 2, 1, N'Affordable housing with basic amenities.', CAST(750.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[Apartments] ([ApartmentNum], [BuildingCode], [Rooms], [Bathrooms], [Description], [Rent], [StatusId]) VALUES (14, N'B04', 1, 1, N'Small and economical.', CAST(600.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[Apartments] ([ApartmentNum], [BuildingCode], [Rooms], [Bathrooms], [Description], [Rent], [StatusId]) VALUES (15, N'B04', 3, 2, N'Family-sized apartment.', CAST(1100.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[Apartments] ([ApartmentNum], [BuildingCode], [Rooms], [Bathrooms], [Description], [Rent], [StatusId]) VALUES (16, N'B04', 2, 1, N'Close to public transport.', CAST(800.00 AS Decimal(10, 2)), 3)
INSERT [dbo].[Apartments] ([ApartmentNum], [BuildingCode], [Rooms], [Bathrooms], [Description], [Rent], [StatusId]) VALUES (17, N'B05', 3, 2, N'Comfortable living with a garden view.', CAST(1250.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[Apartments] ([ApartmentNum], [BuildingCode], [Rooms], [Bathrooms], [Description], [Rent], [StatusId]) VALUES (18, N'B05', 2, 1, N'Cozy apartment near parks.', CAST(950.00 AS Decimal(10, 2)), 1)
INSERT [dbo].[Apartments] ([ApartmentNum], [BuildingCode], [Rooms], [Bathrooms], [Description], [Rent], [StatusId]) VALUES (19, N'B05', 4, 3, N'Spacious and modern interior.', CAST(2000.00 AS Decimal(10, 2)), 2)
INSERT [dbo].[Apartments] ([ApartmentNum], [BuildingCode], [Rooms], [Bathrooms], [Description], [Rent], [StatusId]) VALUES (20, N'B05', 1, 1, N'Studio with easy access to amenities.', CAST(750.00 AS Decimal(10, 2)), 3)
SET IDENTITY_INSERT [dbo].[Apartments] OFF
GO
SET IDENTITY_INSERT [dbo].[Appointments] ON 

INSERT [dbo].[Appointments] ([AppointmentId], [TenantId], [ManagerId], [ApartmentNum], [ScheduleId], [StatusId]) VALUES (12, 2000001, 10001, 2, 1, 5)
INSERT [dbo].[Appointments] ([AppointmentId], [TenantId], [ManagerId], [ApartmentNum], [ScheduleId], [StatusId]) VALUES (13, 2000001, 10001, 2, 1, 5)
INSERT [dbo].[Appointments] ([AppointmentId], [TenantId], [ManagerId], [ApartmentNum], [ScheduleId], [StatusId]) VALUES (14, 2000001, 10001, 2, 1, 6)
INSERT [dbo].[Appointments] ([AppointmentId], [TenantId], [ManagerId], [ApartmentNum], [ScheduleId], [StatusId]) VALUES (16, 2000002, 10002, 17, 2, 4)
SET IDENTITY_INSERT [dbo].[Appointments] OFF
GO
INSERT [dbo].[Buildings] ([BuildingCode], [LandlordId], [ManagerId], [BuildingName], [Description], [Address], [City], [Province], [ZipCode]) VALUES (N'B01', 3001, 10001, N'Downtown Apartments', N'Luxury apartments', N'123 Main St', N'Montreal', N'Quebec', N'H3Z2Y7')
INSERT [dbo].[Buildings] ([BuildingCode], [LandlordId], [ManagerId], [BuildingName], [Description], [Address], [City], [Province], [ZipCode]) VALUES (N'B02', 3001, 10002, N'Riverside Villas', N'Near the riverbank', N'456 Elm St', N'Laval', N'Quebec', N'H7T1N3')
INSERT [dbo].[Buildings] ([BuildingCode], [LandlordId], [ManagerId], [BuildingName], [Description], [Address], [City], [Province], [ZipCode]) VALUES (N'B03', 3001, 10003, N'Mountain View Condos', N'Scenic view apartments', N'789 Oak St', N'Quebec City', N'Quebec', N'G1R3W2')
INSERT [dbo].[Buildings] ([BuildingCode], [LandlordId], [ManagerId], [BuildingName], [Description], [Address], [City], [Province], [ZipCode]) VALUES (N'B04', 3001, 10001, N'Suburban Homes', N'Family-friendly neighborhood', N'101 Maple St', N'Sherbrooke', N'Quebec', N'J1L2Y5')
INSERT [dbo].[Buildings] ([BuildingCode], [LandlordId], [ManagerId], [BuildingName], [Description], [Address], [City], [Province], [ZipCode]) VALUES (N'B05', 3001, 10002, N'Urban Lofts', N'Compact urban living', N'202 Pine St', N'Gatineau', N'Quebec', N'J8X4C2')
INSERT [dbo].[Buildings] ([BuildingCode], [LandlordId], [ManagerId], [BuildingName], [Description], [Address], [City], [Province], [ZipCode]) VALUES (N'B11', 3001, 10002, N'Baby Palace', N'nice building', N'Rue Shell-Leclerc', N'Saint Eustache', N'Quebec', N'J7P 5w6')
GO
INSERT [dbo].[Landlords] ([LandlordId], [FirstName], [LastName], [Email], [Phone]) VALUES (3001, N'Alice', N'Smith', N'alicesmith@gmail.com', N'234-567-8901')
GO
INSERT [dbo].[Managers] ([ManagerId], [FirstName], [LastName], [Email], [Phone]) VALUES (10001, N'John', N'Bob', N'johndoe@gmail.com', N'123-456-7890')
INSERT [dbo].[Managers] ([ManagerId], [FirstName], [LastName], [Email], [Phone]) VALUES (10002, N'Emily', N'Clark', N'emilyclark@gmail.com', N'456-789-0123')
INSERT [dbo].[Managers] ([ManagerId], [FirstName], [LastName], [Email], [Phone]) VALUES (10003, N'Michael', N'Smith', N'michaelsmith@gmail.com', N'789-012-3456')
INSERT [dbo].[Managers] ([ManagerId], [FirstName], [LastName], [Email], [Phone]) VALUES (10004, N'Queen ', N'Anumu Bih', N'anumusarah@yahoo.com', N'438-867-6346')
INSERT [dbo].[Managers] ([ManagerId], [FirstName], [LastName], [Email], [Phone]) VALUES (10007, N'Patrice', N'Brandon', N'jennifer@yahoo.ca', N'438-867-6346')
INSERT [dbo].[Managers] ([ManagerId], [FirstName], [LastName], [Email], [Phone]) VALUES (10012, N'Queen Sarah', N' Bih', N'anumah@yahoo.com', N'430-067-6346')
GO
SET IDENTITY_INSERT [dbo].[Messages] ON 

INSERT [dbo].[Messages] ([MessageId], [SenderId], [ReceiverId], [MessageSubject], [MessageBody], [MessageDate], [StatusId]) VALUES (1, 10001, 3001, N'Property Event Report', N'Hello, I need the ceiling repaired. Thanks.', CAST(N'2024-11-30T22:06:32.870' AS DateTime), 7)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [ReceiverId], [MessageSubject], [MessageBody], [MessageDate], [StatusId]) VALUES (2, 10001, 3001, N'Property Event Report', N'I will be pleased to have a quick meeting with you', CAST(N'2024-11-30T22:15:33.090' AS DateTime), 8)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [ReceiverId], [MessageSubject], [MessageBody], [MessageDate], [StatusId]) VALUES (3, 10001, 3001, N'Property Event Report', N'The buildings need to be checked.
Thanks.', CAST(N'2024-11-30T22:17:43.323' AS DateTime), 8)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [ReceiverId], [MessageSubject], [MessageBody], [MessageDate], [StatusId]) VALUES (4, 2000001, 10001, N'Apartment', N'Please I need a tank refill', CAST(N'2024-12-01T17:05:19.593' AS DateTime), 7)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [ReceiverId], [MessageSubject], [MessageBody], [MessageDate], [StatusId]) VALUES (5, 2000001, 10002, N'Apartment Ceiling', N'Ceiling is licking', CAST(N'2024-12-01T18:03:21.663' AS DateTime), 7)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [ReceiverId], [MessageSubject], [MessageBody], [MessageDate], [StatusId]) VALUES (14, 2000002, 10001, N'Apartment Ceiling', N'I will need help with the ceiling please. 
Thanks.', CAST(N'2024-12-01T19:13:38.383' AS DateTime), 7)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [ReceiverId], [MessageSubject], [MessageBody], [MessageDate], [StatusId]) VALUES (38, 10001, 3001, N'Property Event Report', N'Hello sir', CAST(N'2024-12-01T19:43:33.680' AS DateTime), 7)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [ReceiverId], [MessageSubject], [MessageBody], [MessageDate], [StatusId]) VALUES (40, 10001, 2000001, N'Apartment Ceiling', N'OK, I will get back to you soon.', CAST(N'2024-12-01T20:31:08.053' AS DateTime), 7)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [ReceiverId], [MessageSubject], [MessageBody], [MessageDate], [StatusId]) VALUES (42, 10001, 2000002, N'Apartment', N'OK, I will get to the bottom of this', CAST(N'2024-12-01T20:46:40.450' AS DateTime), 8)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [ReceiverId], [MessageSubject], [MessageBody], [MessageDate], [StatusId]) VALUES (43, 10001, 2000002, N'Apartment Ceiling', N'OK i will get to the root of this.', CAST(N'2024-12-01T20:48:15.123' AS DateTime), 8)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [ReceiverId], [MessageSubject], [MessageBody], [MessageDate], [StatusId]) VALUES (44, 10001, 2000002, N'Apartment', NULL, CAST(N'2024-12-01T20:48:34.440' AS DateTime), 8)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [ReceiverId], [MessageSubject], [MessageBody], [MessageDate], [StatusId]) VALUES (45, 10001, 2000002, N'Apartment Ceiling', N'dfdfgf', CAST(N'2024-12-01T20:52:53.097' AS DateTime), 8)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [ReceiverId], [MessageSubject], [MessageBody], [MessageDate], [StatusId]) VALUES (46, 10001, 3001, N'Property Event Report', N'Can we talk?', CAST(N'2024-12-01T23:19:38.313' AS DateTime), 7)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [ReceiverId], [MessageSubject], [MessageBody], [MessageDate], [StatusId]) VALUES (47, 10001, 3001, N'Property Event Report', N'Hello sir, I have something to tell you about building B01', CAST(N'2024-12-02T08:13:21.040' AS DateTime), 7)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [ReceiverId], [MessageSubject], [MessageBody], [MessageDate], [StatusId]) VALUES (48, 2000003, 10003, N'Apartment Floor', N'Hello, I need a change of floor.
Thanks.', CAST(N'2024-12-02T23:40:43.630' AS DateTime), 7)
INSERT [dbo].[Messages] ([MessageId], [SenderId], [ReceiverId], [MessageSubject], [MessageBody], [MessageDate], [StatusId]) VALUES (49, 10003, 2000003, N'Apartment Floor', N'Ok I will get back to you.', CAST(N'2024-12-02T23:41:50.890' AS DateTime), 7)
SET IDENTITY_INSERT [dbo].[Messages] OFF
GO
SET IDENTITY_INSERT [dbo].[Schedules] ON 

INSERT [dbo].[Schedules] ([ScheduleId], [ManagerId], [ScheduleDate], [StartTime], [EndTime]) VALUES (1, 10001, CAST(N'2024-12-01' AS Date), CAST(N'09:00:00' AS Time), CAST(N'09:30:00' AS Time))
INSERT [dbo].[Schedules] ([ScheduleId], [ManagerId], [ScheduleDate], [StartTime], [EndTime]) VALUES (2, 10002, CAST(N'2024-12-04' AS Date), CAST(N'09:30:00' AS Time), CAST(N'10:00:00' AS Time))
SET IDENTITY_INSERT [dbo].[Schedules] OFF
GO
SET IDENTITY_INSERT [dbo].[Statuses] ON 

INSERT [dbo].[Statuses] ([StatusId], [Description]) VALUES (1, N'Available')
INSERT [dbo].[Statuses] ([StatusId], [Description]) VALUES (2, N'Occupied')
INSERT [dbo].[Statuses] ([StatusId], [Description]) VALUES (3, N'Maintenance')
INSERT [dbo].[Statuses] ([StatusId], [Description]) VALUES (4, N'Pending')
INSERT [dbo].[Statuses] ([StatusId], [Description]) VALUES (5, N'Confirmed')
INSERT [dbo].[Statuses] ([StatusId], [Description]) VALUES (6, N'Canceled')
INSERT [dbo].[Statuses] ([StatusId], [Description]) VALUES (7, N'Read')
INSERT [dbo].[Statuses] ([StatusId], [Description]) VALUES (8, N'Unread')
INSERT [dbo].[Statuses] ([StatusId], [Description]) VALUES (9, N'Fully Paid')
INSERT [dbo].[Statuses] ([StatusId], [Description]) VALUES (10, N'Completed')
INSERT [dbo].[Statuses] ([StatusId], [Description]) VALUES (11, N'Renewal Pending')
SET IDENTITY_INSERT [dbo].[Statuses] OFF
GO
INSERT [dbo].[Tenants] ([TenantId], [FirstName], [LastName], [Email], [Phone]) VALUES (2000001, N'Martha', N'Doe', N'janedoe@gmail.com', N'345-678-9012')
INSERT [dbo].[Tenants] ([TenantId], [FirstName], [LastName], [Email], [Phone]) VALUES (2000002, N'Mark', N'Taylor', N'marktaylor@gmail.com', N'456-789-0123')
INSERT [dbo].[Tenants] ([TenantId], [FirstName], [LastName], [Email], [Phone]) VALUES (2000003, N'Lucy', N'Brown', N'lucybrown@gmail.com', N'567-890-1234')
INSERT [dbo].[Tenants] ([TenantId], [FirstName], [LastName], [Email], [Phone]) VALUES (2000008, N'Patrice', N'Brandon', N'jennifer@yahoo.ca', N'438-867-6346')
INSERT [dbo].[Tenants] ([TenantId], [FirstName], [LastName], [Email], [Phone]) VALUES (2000009, N'Nelsya Sarah', N'Mambo Bih', N'anumupatrice@yahoo.com', N'438-000-6346')
INSERT [dbo].[Tenants] ([TenantId], [FirstName], [LastName], [Email], [Phone]) VALUES (2000010, N'sarah', N'queen', N'anumusa@yahoo.com', N'123-342-4567')
INSERT [dbo].[Tenants] ([TenantId], [FirstName], [LastName], [Email], [Phone]) VALUES (2000011, N'Queen Sarah', N'Anumu Bih', N'anumusarah@yahoo.com', N'438-867-6346')
INSERT [dbo].[Tenants] ([TenantId], [FirstName], [LastName], [Email], [Phone]) VALUES (2000012, N'Getrude', N'Sam', N'sam@gmail.com', N'234-542-6788')
GO
INSERT [dbo].[Users] ([UserId], [Password]) VALUES (3001, N'landlord3001')
INSERT [dbo].[Users] ([UserId], [Password]) VALUES (10000, N'manager1000')
INSERT [dbo].[Users] ([UserId], [Password]) VALUES (10001, N'manager10001')
INSERT [dbo].[Users] ([UserId], [Password]) VALUES (10002, N'manager10002')
INSERT [dbo].[Users] ([UserId], [Password]) VALUES (10003, N'manager10003')
INSERT [dbo].[Users] ([UserId], [Password]) VALUES (10004, N'manager10004')
INSERT [dbo].[Users] ([UserId], [Password]) VALUES (10007, N'manager10007')
INSERT [dbo].[Users] ([UserId], [Password]) VALUES (10009, N'john12345')
INSERT [dbo].[Users] ([UserId], [Password]) VALUES (10012, N'manager12')
INSERT [dbo].[Users] ([UserId], [Password]) VALUES (10234, N'sddfgf231')
INSERT [dbo].[Users] ([UserId], [Password]) VALUES (200001, N'passtenant1')
INSERT [dbo].[Users] ([UserId], [Password]) VALUES (2000001, N'tenantpass1')
INSERT [dbo].[Users] ([UserId], [Password]) VALUES (2000002, N'tenantpass2')
INSERT [dbo].[Users] ([UserId], [Password]) VALUES (2000003, N'tenantpass3')
INSERT [dbo].[Users] ([UserId], [Password]) VALUES (2000008, N'tenantpass8')
INSERT [dbo].[Users] ([UserId], [Password]) VALUES (2000009, N'tenantpass9')
INSERT [dbo].[Users] ([UserId], [Password]) VALUES (2000010, N'tenantpass10')
INSERT [dbo].[Users] ([UserId], [Password]) VALUES (2000011, N'tenantpass11')
INSERT [dbo].[Users] ([UserId], [Password]) VALUES (2000012, N'tenatpass12')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Landlord__A9D105344D42405F]    Script Date: 2024-12-03 10:53:54 PM ******/
ALTER TABLE [dbo].[Landlords] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Managers__A9D10534F41DB7F0]    Script Date: 2024-12-03 10:53:54 PM ******/
ALTER TABLE [dbo].[Managers] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Tenants__A9D10534AE5F9978]    Script Date: 2024-12-03 10:53:54 PM ******/
ALTER TABLE [dbo].[Tenants] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Apartments]  WITH CHECK ADD FOREIGN KEY([BuildingCode])
REFERENCES [dbo].[Buildings] ([BuildingCode])
GO
ALTER TABLE [dbo].[Apartments]  WITH CHECK ADD FOREIGN KEY([StatusId])
REFERENCES [dbo].[Statuses] ([StatusId])
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD FOREIGN KEY([ApartmentNum])
REFERENCES [dbo].[Apartments] ([ApartmentNum])
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD FOREIGN KEY([ManagerId])
REFERENCES [dbo].[Managers] ([ManagerId])
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD FOREIGN KEY([ScheduleId])
REFERENCES [dbo].[Schedules] ([ScheduleId])
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD FOREIGN KEY([StatusId])
REFERENCES [dbo].[Statuses] ([StatusId])
GO
ALTER TABLE [dbo].[Appointments]  WITH CHECK ADD FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants] ([TenantId])
GO
ALTER TABLE [dbo].[Buildings]  WITH CHECK ADD FOREIGN KEY([LandlordId])
REFERENCES [dbo].[Landlords] ([LandlordId])
GO
ALTER TABLE [dbo].[Buildings]  WITH CHECK ADD FOREIGN KEY([ManagerId])
REFERENCES [dbo].[Managers] ([ManagerId])
GO
ALTER TABLE [dbo].[Landlords]  WITH CHECK ADD FOREIGN KEY([LandlordId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Leases]  WITH CHECK ADD FOREIGN KEY([ApartmentNum])
REFERENCES [dbo].[Apartments] ([ApartmentNum])
GO
ALTER TABLE [dbo].[Leases]  WITH CHECK ADD FOREIGN KEY([StatusId])
REFERENCES [dbo].[Statuses] ([StatusId])
GO
ALTER TABLE [dbo].[Leases]  WITH CHECK ADD FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants] ([TenantId])
GO
ALTER TABLE [dbo].[Managers]  WITH CHECK ADD FOREIGN KEY([ManagerId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD FOREIGN KEY([ReceiverId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD FOREIGN KEY([SenderId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD FOREIGN KEY([StatusId])
REFERENCES [dbo].[Statuses] ([StatusId])
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD FOREIGN KEY([LeaseId])
REFERENCES [dbo].[Leases] ([LeaseId])
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD FOREIGN KEY([StatusId])
REFERENCES [dbo].[Statuses] ([StatusId])
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD FOREIGN KEY([TenantId])
REFERENCES [dbo].[Tenants] ([TenantId])
GO
ALTER TABLE [dbo].[Schedules]  WITH CHECK ADD FOREIGN KEY([ManagerId])
REFERENCES [dbo].[Managers] ([ManagerId])
GO
ALTER TABLE [dbo].[Tenants]  WITH CHECK ADD FOREIGN KEY([TenantId])
REFERENCES [dbo].[Users] ([UserId])
GO
USE [master]
GO
ALTER DATABASE [Property_Rental_DB] SET  READ_WRITE 
GO
