USE [master]
GO
/****** Object:  Database [QLVDHT]    Script Date: 6/28/2021 6:58:08 PM ******/
CREATE DATABASE [QLVDHT]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLVDHT', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\QLVDHT.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QLVDHT_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\QLVDHT_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [QLVDHT] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLVDHT].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLVDHT] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLVDHT] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLVDHT] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLVDHT] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLVDHT] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLVDHT] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QLVDHT] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLVDHT] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLVDHT] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLVDHT] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLVDHT] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLVDHT] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLVDHT] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLVDHT] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLVDHT] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QLVDHT] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLVDHT] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLVDHT] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLVDHT] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLVDHT] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLVDHT] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLVDHT] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLVDHT] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QLVDHT] SET  MULTI_USER 
GO
ALTER DATABASE [QLVDHT] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLVDHT] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLVDHT] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLVDHT] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QLVDHT] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QLVDHT] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [QLVDHT] SET QUERY_STORE = OFF
GO
USE [QLVDHT]
GO
/****** Object:  UserDefinedFunction [dbo].[fuc_ThongKe]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[fuc_ThongKe](@ngayBd date, @ngayKt date)
returns @bang 
table (tongView int, tongBl int, tenVdXemNn nvarchar(100),tenVdBlNn nvarchar(100),
tenKhXemNn nvarchar(100),tenTkXemNn nvarchar(30), tenTkBlNn nvarchar(30) ) 
as
begin
	insert into @bang 
	values(
		(select count(id) from LichSuCoi where ngayCoi >= @ngayBd and ngayCoi <= @ngayKt),
		(select count(idBl) from BinhLuan where ngayBl >= @ngayBd and ngayBl <= @ngayKt),
		(select top 1 tenVd
		 from LichSuCoi inner join Videos 
		 on LichSuCoi.idVd = Videos.idVd
		 where ngayCoi >= @ngayBd and ngayCoi <= @ngayKt
		 Group by tenVd
		 Order by count(tenVd) desc),
		(select top 1 tenVd
		 from BinhLuan inner join Videos 
		 on BinhLuan.idVd = Videos.idVd
		 where ngayBl >= @ngayBd and ngayBl <= @ngayKt
		 Group by tenVd
		 Order by count(tenVd) desc),
		(select top 1 tenKh
		 from LichSuCoi inner join Videos
		 on LichSuCoi.idVd = Videos.idVd
		 inner join KhoaHoc
		 on KhoaHoc.idKh = Videos.idKh
		 where ngayCoi >= @ngayBd and ngayCoi <= @ngayKt
		 Group by tenKh
		 Order by count(tenKh) desc),
		(select top 1 tenDn
		 from LichSuCoi inner join TaiKhoan 
		 on LichSuCoi.idTk = TaiKhoan.idTk
		 where ngayCoi >= @ngayBd and ngayCoi <= @ngayKt
		 Group by tenDn
		 Order by count(tenDn) desc),
		(select top 1 tenDn
		 from BinhLuan inner join TaiKhoan 
		 on BinhLuan.idTk = TaiKhoan.idTk
		 where ngayBl >= @ngayBd and ngayBl <= @ngayKt
		 Group by tenDn
		 Order by count(tenDn) desc)
	)
return
end
GO
/****** Object:  UserDefinedFunction [dbo].[func_CheckUser]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[func_CheckUser](@ten varchar(30))
returns int as
begin
	return (select count(idTk) as 'sl' from TaiKhoan where tenDn = @ten)
end
GO
/****** Object:  UserDefinedFunction [dbo].[func_DangNhap]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[func_DangNhap](
@taiKhoan varchar(30),
@matKhau varchar(20)
)
returns int as
begin
	declare @temp int;
	select @temp = idTk from TaiKhoan
	where @taiKhoan = tenDn and @matKhau = matKhau;
	if( @temp is null)
		set @temp = 0;
	return @temp;
end
GO
/****** Object:  UserDefinedFunction [dbo].[func_DemKhoaHocDk]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[func_DemKhoaHocDk](@id int)
returns int as
begin
	return(select count(idDk) from DKKH where idTk = @id);
end
GO
/****** Object:  UserDefinedFunction [dbo].[func_DemTongLuotXem]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[func_DemTongLuotXem](@idTk int)
returns int as
begin
	return (select count(id) from LichSuCoi where idTk = @idTk);
end
GO
/****** Object:  UserDefinedFunction [dbo].[func_DemVdTrongKh]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[func_DemVdTrongKh](@id int)
returns int as
begin
	return(select count(idKh) from Videos
	where @id = idKh)
end
GO
/****** Object:  UserDefinedFunction [dbo].[func_KiemTraDaCoiVdChua]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[func_KiemTraDaCoiVdChua](@idVd int , @idTk int)
returns int as
begin
	return (select count(id) as 'sl' from LichSuCoi where idVd = @idVd and idTk = @idTk);
end
GO
/****** Object:  UserDefinedFunction [dbo].[func_KiemTraDKKH]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[func_KiemTraDKKH](
@idKh int,
@idTk int
) returns int as
begin
	declare @temp int;
	select @temp = COUNT(idDk) from DKKH                                                       
	where @idKh = idKh and @idTk = idTk;
	return @temp;
end
GO
/****** Object:  UserDefinedFunction [dbo].[func_KtraKhTonTaiChua]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[func_KtraKhTonTaiChua](@tenKh nvarchar(100))
returns int as
begin
	return (select count(idKh) from KhoaHoc where tenKh = @tenKh);
end
GO
/****** Object:  UserDefinedFunction [dbo].[func_KtraKhTonTaiId]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[func_KtraKhTonTaiId](@id int)
returns int as
begin
	return (select count(idKh) from KhoaHoc where idKh = @id);
end
GO
/****** Object:  UserDefinedFunction [dbo].[func_KtrVdTonTaiChua]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[func_KtrVdTonTaiChua](
@tenVd nvarchar(100),
@urlThumbails varchar(200),
@urlVd varchar(200),
@idKh int)
returns int as
begin
	return (select count(idVd) from Videos where tenVd = @tenVd and urlThumbails = @urlThumbails and urlVd = @urlVd and @idKh = idKh);
end
GO
/****** Object:  UserDefinedFunction [dbo].[func_LayTenKhCuaVd]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create function [dbo].[func_LayTenKhCuaVd](@id int)
returns nvarchar(100) as
begin
	return (select tenKh from KhoaHoc where idKh = @id);
end
GO
/****** Object:  Table [dbo].[Videos]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Videos](
	[idVd] [int] IDENTITY(10001,1) NOT NULL,
	[tenVd] [nvarchar](100) NOT NULL,
	[urlThumbails] [varchar](200) NOT NULL,
	[urlVd] [varchar](200) NOT NULL,
	[luotXem] [int] NULL,
	[idKh] [int] NULL,
	[ngayDang] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idVd] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LichSuCoi]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LichSuCoi](
	[id] [int] IDENTITY(10001,1) NOT NULL,
	[idVd] [int] NOT NULL,
	[idTk] [int] NOT NULL,
	[ngayCoi] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[func_VideoSaCoi]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[func_VideoSaCoi](@id int)
returns  table as
	return (select Videos.idVd as'idVd' 
		from Videos inner join LichSuCoi on Videos.idVd = LichSuCoi.idVd 
		where idTk = @id);
GO
/****** Object:  Table [dbo].[BinhLuan]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BinhLuan](
	[idBl] [int] IDENTITY(10001,1) NOT NULL,
	[noiDung] [nvarchar](200) NOT NULL,
	[idVd] [int] NOT NULL,
	[idTk] [int] NOT NULL,
	[ngayBl] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idBl] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DKKH]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DKKH](
	[idDk] [int] IDENTITY(10001,1) NOT NULL,
	[idKh] [int] NOT NULL,
	[idTk] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idDk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhoaHoc]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhoaHoc](
	[idKh] [int] IDENTITY(10001,1) NOT NULL,
	[tenKh] [nvarchar](100) NOT NULL,
	[ngayTao] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idKh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[idTk] [int] IDENTITY(10000,1) NOT NULL,
	[tenDn] [varchar](30) NOT NULL,
	[matKhau] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[idTk] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BinhLuan] ON 

INSERT [dbo].[BinhLuan] ([idBl], [noiDung], [idVd], [idTk], [ngayBl]) VALUES (10035, N'Bài này hay nha mọi người', 10048, 10000, CAST(N'2021-06-28T11:27:34.183' AS DateTime))
INSERT [dbo].[BinhLuan] ([idBl], [noiDung], [idVd], [idTk], [ngayBl]) VALUES (10036, N'ui chu choa ứng dụng này nhìn chán thê', 10048, 10000, CAST(N'2021-06-28T11:55:26.340' AS DateTime))
SET IDENTITY_INSERT [dbo].[BinhLuan] OFF
GO
SET IDENTITY_INSERT [dbo].[DKKH] ON 

INSERT [dbo].[DKKH] ([idDk], [idKh], [idTk]) VALUES (10066, 10013, 10000)
SET IDENTITY_INSERT [dbo].[DKKH] OFF
GO
SET IDENTITY_INSERT [dbo].[KhoaHoc] ON 

INSERT [dbo].[KhoaHoc] ([idKh], [tenKh], [ngayTao]) VALUES (10012, N'Lập trình trên Windows', CAST(N'2021-06-03T14:53:21.120' AS DateTime))
INSERT [dbo].[KhoaHoc] ([idKh], [tenKh], [ngayTao]) VALUES (10013, N'Lập trình hướng đối tượng', CAST(N'2021-06-03T14:53:35.890' AS DateTime))
INSERT [dbo].[KhoaHoc] ([idKh], [tenKh], [ngayTao]) VALUES (10014, N'Lập trình cơ bản', CAST(N'2021-06-03T14:53:48.950' AS DateTime))
INSERT [dbo].[KhoaHoc] ([idKh], [tenKh], [ngayTao]) VALUES (10016, N'Nhập môn mạng máy tính', CAST(N'2021-06-28T11:50:19.690' AS DateTime))
SET IDENTITY_INSERT [dbo].[KhoaHoc] OFF
GO
SET IDENTITY_INSERT [dbo].[LichSuCoi] ON 

INSERT [dbo].[LichSuCoi] ([id], [idVd], [idTk], [ngayCoi]) VALUES (10067, 10042, 10000, CAST(N'2021-06-28T11:18:28.430' AS DateTime))
INSERT [dbo].[LichSuCoi] ([id], [idVd], [idTk], [ngayCoi]) VALUES (10068, 10042, 10002, CAST(N'2021-06-28T11:19:44.410' AS DateTime))
INSERT [dbo].[LichSuCoi] ([id], [idVd], [idTk], [ngayCoi]) VALUES (10069, 10048, 10000, CAST(N'2021-06-28T11:27:17.710' AS DateTime))
INSERT [dbo].[LichSuCoi] ([id], [idVd], [idTk], [ngayCoi]) VALUES (10070, 10043, 10000, CAST(N'2021-06-28T11:28:04.743' AS DateTime))
INSERT [dbo].[LichSuCoi] ([id], [idVd], [idTk], [ngayCoi]) VALUES (10071, 10044, 10000, CAST(N'2021-06-28T11:54:51.990' AS DateTime))
INSERT [dbo].[LichSuCoi] ([id], [idVd], [idTk], [ngayCoi]) VALUES (10072, 10046, 10000, CAST(N'2021-06-28T11:54:53.740' AS DateTime))
INSERT [dbo].[LichSuCoi] ([id], [idVd], [idTk], [ngayCoi]) VALUES (10073, 10045, 10000, CAST(N'2021-06-28T11:54:55.833' AS DateTime))
INSERT [dbo].[LichSuCoi] ([id], [idVd], [idTk], [ngayCoi]) VALUES (10074, 10047, 10000, CAST(N'2021-06-28T11:54:57.757' AS DateTime))
SET IDENTITY_INSERT [dbo].[LichSuCoi] OFF
GO
SET IDENTITY_INSERT [dbo].[TaiKhoan] ON 

INSERT [dbo].[TaiKhoan] ([idTk], [tenDn], [matKhau]) VALUES (10000, N'admin', N'123')
INSERT [dbo].[TaiKhoan] ([idTk], [tenDn], [matKhau]) VALUES (10002, N'TuanAnh', N'123')
INSERT [dbo].[TaiKhoan] ([idTk], [tenDn], [matKhau]) VALUES (10003, N'Trung', N'123')
INSERT [dbo].[TaiKhoan] ([idTk], [tenDn], [matKhau]) VALUES (10004, N'Viet', N'123')
SET IDENTITY_INSERT [dbo].[TaiKhoan] OFF
GO
SET IDENTITY_INSERT [dbo].[Videos] ON 

INSERT [dbo].[Videos] ([idVd], [tenVd], [urlThumbails], [urlVd], [luotXem], [idKh], [ngayDang]) VALUES (10042, N'Cài  đặt môi trường lập trình trên Windows', N'D:\tuana\Pictures\Screenshots\wpfcomuniti.png', N'D:\tuana\Videos\Video_2021-05-30_105218.wmv', 2, 10012, CAST(N'2021-06-28T11:18:20.773' AS DateTime))
INSERT [dbo].[Videos] ([idVd], [tenVd], [urlThumbails], [urlVd], [luotXem], [idKh], [ngayDang]) VALUES (10043, N'Lập trình trên Windows bài 1', N'D:\tuana\Pictures\Screenshots\Screenshot (1).png', N'D:\tuana\Videos\Video_2021-05-30_105218.wmv', 1, 10012, CAST(N'2021-06-28T11:20:53.910' AS DateTime))
INSERT [dbo].[Videos] ([idVd], [tenVd], [urlThumbails], [urlVd], [luotXem], [idKh], [ngayDang]) VALUES (10044, N'Lập trình trên Windows bài 2', N'D:\tuana\Pictures\Screenshots\wpfcomuniti.png', N'D:\tuana\Videos\Video_2021-05-30_105218.wmv', 1, 10012, CAST(N'2021-06-28T11:22:36.530' AS DateTime))
INSERT [dbo].[Videos] ([idVd], [tenVd], [urlThumbails], [urlVd], [luotXem], [idKh], [ngayDang]) VALUES (10045, N'Lập trình trên Windows bài 3', N'D:\tuana\Pictures\Screenshots\Screenshot (1).png', N'D:\tuana\Videos\Video_2021-05-30_105218.wmv', 1, 10012, CAST(N'2021-06-28T11:22:58.097' AS DateTime))
INSERT [dbo].[Videos] ([idVd], [tenVd], [urlThumbails], [urlVd], [luotXem], [idKh], [ngayDang]) VALUES (10046, N'Lập trình trên Windows bài 4', N'D:\tuana\Pictures\Screenshots\wpfcomuniti.png', N'D:\tuana\Videos\Video_2021-05-30_105218.wmv', 1, 10012, CAST(N'2021-06-28T11:23:14.620' AS DateTime))
INSERT [dbo].[Videos] ([idVd], [tenVd], [urlThumbails], [urlVd], [luotXem], [idKh], [ngayDang]) VALUES (10047, N'Cài  đặt môi trường lập trình hướng đối tượng', N'D:\tuana\Pictures\Screenshots\wpfcomuniti.png', N'D:\tuana\Videos\Video_2021-05-30_105218.wmv', 1, 10013, CAST(N'2021-06-28T11:23:44.113' AS DateTime))
INSERT [dbo].[Videos] ([idVd], [tenVd], [urlThumbails], [urlVd], [luotXem], [idKh], [ngayDang]) VALUES (10048, N'Lập trình hướng đối tượng bài 1', N'D:\tuana\Pictures\Screenshots\wpfcomuniti.png', N'D:\tuana\Videos\Video_2021-05-30_105218.wmv', 1, 10013, CAST(N'2021-06-28T11:24:03.863' AS DateTime))
INSERT [dbo].[Videos] ([idVd], [tenVd], [urlThumbails], [urlVd], [luotXem], [idKh], [ngayDang]) VALUES (10049, N'Lập trình hướng đối tượng bài 2', N'D:\tuana\Pictures\Screenshots\wpfcomuniti.png', N'D:\tuana\Videos\Video_2021-05-30_105218.wmv', NULL, 10013, CAST(N'2021-06-28T11:24:10.200' AS DateTime))
INSERT [dbo].[Videos] ([idVd], [tenVd], [urlThumbails], [urlVd], [luotXem], [idKh], [ngayDang]) VALUES (10050, N'Lập trình hướng đối tượng bài 3', N'D:\tuana\Pictures\Screenshots\wpfcomuniti.png', N'D:\tuana\Videos\Video_2021-05-30_105218.wmv', NULL, 10013, CAST(N'2021-06-28T11:24:16.587' AS DateTime))
INSERT [dbo].[Videos] ([idVd], [tenVd], [urlThumbails], [urlVd], [luotXem], [idKh], [ngayDang]) VALUES (10051, N'Lập trình hướng đối tượng bài 4', N'D:\tuana\Pictures\Screenshots\wpfcomuniti.png', N'D:\tuana\Videos\Video_2021-05-30_105218.wmv', NULL, 10013, CAST(N'2021-06-28T11:24:23.673' AS DateTime))
INSERT [dbo].[Videos] ([idVd], [tenVd], [urlThumbails], [urlVd], [luotXem], [idKh], [ngayDang]) VALUES (10052, N'Lập trình cơ bản bài 1', N'D:\tuana\Pictures\Screenshots\wpfcomuniti.png', N'D:\tuana\Videos\Video_2021-05-30_105218.wmv', NULL, 10014, CAST(N'2021-06-28T11:24:50.290' AS DateTime))
INSERT [dbo].[Videos] ([idVd], [tenVd], [urlThumbails], [urlVd], [luotXem], [idKh], [ngayDang]) VALUES (10053, N'Lập trình cơ bản bài 2', N'D:\tuana\Pictures\Screenshots\wpfcomuniti.png', N'D:\tuana\Videos\Video_2021-05-30_105218.wmv', NULL, 10014, CAST(N'2021-06-28T11:24:56.250' AS DateTime))
INSERT [dbo].[Videos] ([idVd], [tenVd], [urlThumbails], [urlVd], [luotXem], [idKh], [ngayDang]) VALUES (10054, N'Lập trình cơ bản bài 3', N'D:\tuana\Pictures\Screenshots\wpfcomuniti.png', N'D:\tuana\Videos\Video_2021-05-30_105218.wmv', NULL, 10014, CAST(N'2021-06-28T11:25:03.027' AS DateTime))
INSERT [dbo].[Videos] ([idVd], [tenVd], [urlThumbails], [urlVd], [luotXem], [idKh], [ngayDang]) VALUES (10055, N'Lập trình cơ bản bài 4', N'D:\tuana\Pictures\Screenshots\wpfcomuniti.png', N'D:\tuana\Videos\Video_2021-05-30_105218.wmv', NULL, 10014, CAST(N'2021-06-28T11:25:11.483' AS DateTime))
INSERT [dbo].[Videos] ([idVd], [tenVd], [urlThumbails], [urlVd], [luotXem], [idKh], [ngayDang]) VALUES (10056, N'Lập trình cơ bản bài 5', N'D:\tuana\Pictures\Screenshots\wpfcomuniti.png', N'D:\tuana\Videos\Video_2021-05-30_105218.wmv', NULL, 10014, CAST(N'2021-06-28T11:25:21.593' AS DateTime))
INSERT [dbo].[Videos] ([idVd], [tenVd], [urlThumbails], [urlVd], [luotXem], [idKh], [ngayDang]) VALUES (10057, N'Lập trình cơ bản bài 6', N'D:\tuana\Pictures\Screenshots\wpfcomuniti.png', N'D:\tuana\Videos\Video_2021-05-30_105218.wmv', NULL, 10014, CAST(N'2021-06-28T11:25:28.690' AS DateTime))
INSERT [dbo].[Videos] ([idVd], [tenVd], [urlThumbails], [urlVd], [luotXem], [idKh], [ngayDang]) VALUES (10058, N'Lập trình cơ bản bài 7', N'D:\tuana\Pictures\Screenshots\wpfcomuniti.png', N'D:\tuana\Videos\Video_2021-05-30_105218.wmv', NULL, 10014, CAST(N'2021-06-28T11:25:38.740' AS DateTime))
INSERT [dbo].[Videos] ([idVd], [tenVd], [urlThumbails], [urlVd], [luotXem], [idKh], [ngayDang]) VALUES (10059, N'Lập trình cơ bản bài 8', N'D:\tuana\Pictures\Screenshots\wpfcomuniti.png', N'D:\tuana\Videos\Video_2021-05-30_105218.wmv', NULL, 10014, CAST(N'2021-06-28T11:25:45.507' AS DateTime))
INSERT [dbo].[Videos] ([idVd], [tenVd], [urlThumbails], [urlVd], [luotXem], [idKh], [ngayDang]) VALUES (10060, N'Lập trình cơ bản bài 9', N'D:\tuana\Pictures\Screenshots\wpfcomuniti.png', N'D:\tuana\Videos\Video_2021-05-30_105218.wmv', NULL, 10014, CAST(N'2021-06-28T11:25:53.450' AS DateTime))
INSERT [dbo].[Videos] ([idVd], [tenVd], [urlThumbails], [urlVd], [luotXem], [idKh], [ngayDang]) VALUES (10061, N'Lập trình cơ bản bài 10', N'D:\tuana\Pictures\Screenshots\wpfcomuniti.png', N'D:\tuana\Videos\Video_2021-05-30_105218.wmv', NULL, 10014, CAST(N'2021-06-28T11:26:03.897' AS DateTime))
INSERT [dbo].[Videos] ([idVd], [tenVd], [urlThumbails], [urlVd], [luotXem], [idKh], [ngayDang]) VALUES (10062, N'Lập trình hướng đối tượng bài 5', N'D:\tuana\Pictures\Screenshots\Screenshot (1).png', N'D:\tuana\Videos\Video_2021-05-30_105218.wmv', NULL, 10013, CAST(N'2021-06-28T11:26:25.863' AS DateTime))
SET IDENTITY_INSERT [dbo].[Videos] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__TaiKhoan__FB7499D7CAD53EBB]    Script Date: 6/28/2021 6:58:08 PM ******/
ALTER TABLE [dbo].[TaiKhoan] ADD UNIQUE NONCLUSTERED 
(
	[tenDn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Videos] ADD  DEFAULT ((0)) FOR [luotXem]
GO
ALTER TABLE [dbo].[BinhLuan]  WITH CHECK ADD FOREIGN KEY([idTk])
REFERENCES [dbo].[TaiKhoan] ([idTk])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BinhLuan]  WITH CHECK ADD FOREIGN KEY([idVd])
REFERENCES [dbo].[Videos] ([idVd])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DKKH]  WITH CHECK ADD FOREIGN KEY([idKh])
REFERENCES [dbo].[KhoaHoc] ([idKh])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DKKH]  WITH CHECK ADD FOREIGN KEY([idTk])
REFERENCES [dbo].[TaiKhoan] ([idTk])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LichSuCoi]  WITH CHECK ADD FOREIGN KEY([idTk])
REFERENCES [dbo].[TaiKhoan] ([idTk])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LichSuCoi]  WITH CHECK ADD FOREIGN KEY([idVd])
REFERENCES [dbo].[Videos] ([idVd])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Videos]  WITH CHECK ADD FOREIGN KEY([idKh])
REFERENCES [dbo].[KhoaHoc] ([idKh])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
/****** Object:  StoredProcedure [dbo].[pc_HuyDKKH]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[pc_HuyDKKH](
@idKh int,
@idTk int
)as
begin
	declare @temp int;
	select @temp = idDk from DKKH where idKh = @idKh and idTk = @idTk;
	if(@temp is null)
		return 0;
	else 
		begin
			delete from DKKH where idDk = @temp;
			set @temp = 1;
		end
	return @temp;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_BinhLuanVoVd]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_BinhLuanVoVd](
@noiDung nvarchar(200),
@idVd int,
@idTk int )
as
begin
	insert into BinhLuan values(@noiDung ,@idVd,@idTk,getdate())
end
GO
/****** Object:  StoredProcedure [dbo].[sp_DoiMk]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_DoiMk] (@idTk int, @tenDn varchar(30), @matKhau varchar(20))
as
begin
	update TaiKhoan set matKhau = @matKhau where idTk = @idTk and tenDn = @tenDn;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_DsIdVDaXem]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_DsIdVDaXem](@id int)
as
begin
	select Videos.idVd
	from Videos inner join LichSuCoi on Videos.idVd = LichSuCoi.idVd 
	where idTk = @id;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_KiemTraDaCoiVdChua]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_KiemTraDaCoiVdChua](@idVd int , @idTk int)
as 
select count(id) as 'sl' from LichSuCoi where idVd = @idVd and idTk = @idTk 
GO
/****** Object:  StoredProcedure [dbo].[sp_LayBinhLuan]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_LayBinhLuan](@idVd int)
as
begin
	select idBl as 'id', (select Concat(tenDn , ': ' , noiDung)) 
	as 'noiDung', ngayBl as'ngay' from BinhLuan 
	inner join Videos on BinhLuan.idVd = Videos.idVd 
	inner join TaiKhoan on BinhLuan.idTk = TaiKhoan.idTk
	where BinhLuan.idVd = @idVd
	order by ngayBl asc
end
GO
/****** Object:  StoredProcedure [dbo].[sp_SuaKh]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_SuaKh](@idKh int,
@ten nvarchar(100))
as
begin
	update KhoaHoc set tenKh = @ten where idKh = @idKh;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_SuaVd]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_SuaVd](
@idVd int,
@tenVd nvarchar(100),
@urlThumbails varchar(200),
@urlVd varchar(200),
@idKh int)
as
begin
	update Videos set tenVd = @tenVd, urlThumbails = @urlThumbails, urlVd = @urlVd, idKh = @idKh where idVd = @idVd;
end
GO
/****** Object:  StoredProcedure [dbo].[sp_TaoKhoaHoc]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_TaoKhoaHoc](@Ten nvarchar(100))
as
begin
	if((select count(idKh) from KhoaHoc where tenKh = @Ten)=0)
		insert into KhoaHoc values(@ten, getdate());
end
GO
/****** Object:  StoredProcedure [dbo].[sp_TaoUser]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_TaoUser](@tenDn varchar(30),@matKhau varchar(20))
as
begin
	insert into TaiKhoan (tenDn, matKhau) values(@tenDn, @matKhau)
end
GO
/****** Object:  StoredProcedure [dbo].[sp_TaoVd]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_TaoVd](
@tenVd nvarchar(100),
@urlThumbails varchar(200),
@urlVd varchar(200),
@idKh int)
as
begin 
	insert into Videos values(@tenVd, @urlThumbails, @urlVd ,null, @idKh, getdate());
end
GO
/****** Object:  StoredProcedure [dbo].[sp_TaoVdNoIdKh]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_TaoVdNoIdKh](
@tenVd nvarchar(100),
@urlThumbails varchar(200),
@urlVd varchar(200))
as
begin 
	insert into Videos values(@tenVd, @urlThumbails, @urlVd ,null, null, getdate());
end
GO
/****** Object:  StoredProcedure [dbo].[sp_Themkh]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_Themkh](
@idKh int,
@idTk int
)as
begin
	insert into DKKH values(@idKh, @idTk)
	select * from DKKH where @idKh = idKh and @idTk = idTk
end
GO
/****** Object:  StoredProcedure [dbo].[sp_TimKh]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_TimKh](@tenKh nvarchar(100))
as
begin
	select * From KhoaHoc 
	where (@tenKh IS NULL OR tenKh LIKE '%' + @tenKh + '%')
end
GO
/****** Object:  StoredProcedure [dbo].[sp_TimVd]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_TimVd](@tenVd nvarchar(100))
as
begin
	select * From Videos 
	where (@tenVd IS NULL OR tenVd LIKE '%' + @tenVd + '%')
end
GO
/****** Object:  StoredProcedure [dbo].[sp_XemDsKh]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[sp_XemDsKh]
as
begin
	select * From KhoaHoc
end
GO
/****** Object:  StoredProcedure [dbo].[sp_XemDsKhCuaToi]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_XemDsKhCuaToi](@id int)
as
begin
	select KhoaHoc.idKh, tenKh From KhoaHoc inner join DKKH
	on KhoaHoc.idKh = DKKH.idKh
	where idTk = @id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_XemDsVd]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[sp_XemDsVd]
as
begin
	select * From Videos
end
GO
/****** Object:  StoredProcedure [dbo].[sp_XemDsVdTheoKh]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[sp_XemDsVdTheoKh](@id int)
as
begin
	select * From Videos
	where idKh = @id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_XemTTTK]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[sp_XemTTTK](@id int)
as
begin
	select * From TaiKhoan
	where idTk = @id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_XemVd]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_XemVd](
@idVd int,
@idTk int
)as
begin
	insert into LichSuCoi values(@idVd, @idTk, getdate())
	select id from LichSuCoi where @idVd = idVd and @idTk = idTk
end
GO
/****** Object:  StoredProcedure [dbo].[sp_XoaKhoaHoc]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_XoaKhoaHoc](@idKh int)
as delete KhoaHoc where idKh = @idKh
GO
/****** Object:  StoredProcedure [dbo].[sp_XoaVideo]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_XoaVideo](@idVd int)
as delete Videos where idVd = @idVd
GO
/****** Object:  StoredProcedure [dbo].[sp_XoaVideoKhoiKh]    Script Date: 6/28/2021 6:58:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[sp_XoaVideoKhoiKh](@idVd int)
as update Videos set idKh = null where idVd = @idVd
GO
USE [master]
GO
ALTER DATABASE [QLVDHT] SET  READ_WRITE 
GO
