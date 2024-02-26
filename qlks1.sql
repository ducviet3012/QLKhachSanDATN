USE [master]
GO
/****** Object:  Database [QLKhachSanTTTN]    Script Date: 2/26/2024 3:45:59 PM ******/
CREATE DATABASE [QLKhachSanTTTN]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLKhachSanTTTN', FILENAME = N'D:\Tai lieu hoc ki 5\Pttk csdl\MSSQL15.SQLEXPRESS\MSSQL\DATA\QLKhachSanTTTN.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QLKhachSanTTTN_log', FILENAME = N'D:\Tai lieu hoc ki 5\Pttk csdl\MSSQL15.SQLEXPRESS\MSSQL\DATA\QLKhachSanTTTN_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [QLKhachSanTTTN] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLKhachSanTTTN].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLKhachSanTTTN] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLKhachSanTTTN] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLKhachSanTTTN] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLKhachSanTTTN] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLKhachSanTTTN] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLKhachSanTTTN] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QLKhachSanTTTN] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLKhachSanTTTN] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLKhachSanTTTN] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLKhachSanTTTN] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLKhachSanTTTN] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLKhachSanTTTN] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLKhachSanTTTN] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLKhachSanTTTN] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLKhachSanTTTN] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QLKhachSanTTTN] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLKhachSanTTTN] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLKhachSanTTTN] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLKhachSanTTTN] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLKhachSanTTTN] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLKhachSanTTTN] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLKhachSanTTTN] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLKhachSanTTTN] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QLKhachSanTTTN] SET  MULTI_USER 
GO
ALTER DATABASE [QLKhachSanTTTN] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLKhachSanTTTN] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLKhachSanTTTN] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLKhachSanTTTN] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QLKhachSanTTTN] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QLKhachSanTTTN] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [QLKhachSanTTTN] SET QUERY_STORE = OFF
GO
USE [QLKhachSanTTTN]
GO
/****** Object:  Table [dbo].[Blog]    Script Date: 2/26/2024 3:45:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[IDblog] [nvarchar](10) NOT NULL,
	[MaNV] [int] NULL,
	[Anh] [nvarchar](50) NULL,
	[TieuDe] [nvarchar](100) NULL,
	[ThongTin] [text] NULL,
	[NgayDang] [date] NULL,
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
	[IDblog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CSVC]    Script Date: 2/26/2024 3:45:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CSVC](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Nam] [int] NULL,
	[Thang] [int] NULL,
	[TongTien] [float] NULL,
 CONSTRAINT [PK_CSVC] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CTAnh]    Script Date: 2/26/2024 3:45:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTAnh](
	[TenAnh] [nvarchar](50) NOT NULL,
	[MaPhong] [int] NULL,
 CONSTRAINT [PK_CTAnh] PRIMARY KEY CLUSTERED 
(
	[TenAnh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DatPhong]    Script Date: 2/26/2024 3:45:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DatPhong](
	[MaPhong] [int] NOT NULL,
	[SoHoaDon] [int] NOT NULL,
	[NgayDen] [date] NULL,
	[NgayDi] [date] NULL,
	[SoNguoi] [int] NULL,
 CONSTRAINT [PK_DatPhong] PRIMARY KEY CLUSTERED 
(
	[MaPhong] ASC,
	[SoHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DichVu]    Script Date: 2/26/2024 3:45:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DichVu](
	[MaDV] [nvarchar](10) NOT NULL,
	[TenDV] [nvarchar](30) NULL,
	[Gia] [float] NULL,
	[Anh] [nvarchar](30) NULL,
	[ThongTin] [nvarchar](300) NULL,
 CONSTRAINT [PK_DichVu] PRIMARY KEY CLUSTERED 
(
	[MaDV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GopY]    Script Date: 2/26/2024 3:45:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GopY](
	[ID] [int] NOT NULL,
	[MaKH] [int] NULL,
	[MaPhong] [nvarchar](50) NULL,
	[Vote] [int] NULL,
	[Comment] [nvarchar](50) NULL,
 CONSTRAINT [PK_GopY] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HoaDon]    Script Date: 2/26/2024 3:45:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoaDon](
	[SoHoaDon] [int] IDENTITY(1,1) NOT NULL,
	[NgayThanhToan] [date] NULL,
	[MaNV] [int] NULL,
	[MaKH] [int] NULL,
	[TenKH] [nvarchar](50) NULL,
	[Email] [nvarchar](200) NULL,
	[SDT] [nvarchar](50) NULL,
 CONSTRAINT [PK_HoaDon] PRIMARY KEY CLUSTERED 
(
	[SoHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 2/26/2024 3:45:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[MaKH] [int] IDENTITY(1,1) NOT NULL,
	[TenKH] [nvarchar](50) NULL,
	[GioiTinh] [int] NULL,
	[CCCD] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[NgaySinh] [date] NULL,
	[DiaChi] [nvarchar](50) NULL,
	[SDT] [nvarchar](50) NULL,
	[LoaiKhachHang] [nvarchar](50) NULL,
	[UserId] [int] NULL,
	[HieuLuc] [int] NULL,
 CONSTRAINT [PK_KhachHang] PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KhachSan]    Script Date: 2/26/2024 3:45:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachSan](
	[MaKS] [int] IDENTITY(1,1) NOT NULL,
	[MaTinh] [int] NULL,
	[TenKhachSan] [nvarchar](50) NULL,
	[DiaChi] [nvarchar](50) NULL,
	[MoTa] [nvarchar](max) NULL,
	[Anh] [nvarchar](50) NULL,
	[DanhGia] [int] NULL,
 CONSTRAINT [PK_KhachSan] PRIMARY KEY CLUSTERED 
(
	[MaKS] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiPhong]    Script Date: 2/26/2024 3:45:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiPhong](
	[MaLP] [nvarchar](10) NOT NULL,
	[LoaiPhong] [nvarchar](50) NULL,
	[SoNguoiToiDa] [int] NULL,
	[Gia] [float] NULL,
	[Anh] [nvarchar](50) NULL,
	[ThongTin] [nvarchar](100) NULL,
	[KichThuoc] [nvarchar](20) NULL,
 CONSTRAINT [PK_LoaiPhong] PRIMARY KEY CLUSTERED 
(
	[MaLP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiUser]    Script Date: 2/26/2024 3:45:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiUser](
	[IdLoaiUser] [int] IDENTITY(1,1) NOT NULL,
	[TenLoai] [nvarchar](50) NULL,
 CONSTRAINT [PK_LoaiUser] PRIMARY KEY CLUSTERED 
(
	[IdLoaiUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NhanVien]    Script Date: 2/26/2024 3:45:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhanVien](
	[MaNV] [int] IDENTITY(1,1) NOT NULL,
	[TenNV] [nvarchar](50) NULL,
	[GioiTinh] [int] NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[CCCD] [nvarchar](50) NULL,
	[NgaySinh] [date] NULL,
	[DiaChi] [nvarchar](50) NULL,
	[SDT] [nvarchar](50) NULL,
	[ChucVu] [nvarchar](50) NULL,
	[Anh] [nvarchar](50) NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_NhanVien] PRIMARY KEY CLUSTERED 
(
	[MaNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phong]    Script Date: 2/26/2024 3:45:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phong](
	[MaPhong] [int] IDENTITY(1,1) NOT NULL,
	[MaKS] [int] NULL,
	[TenPhong] [nvarchar](50) NULL,
	[TinhTrang] [nvarchar](50) NULL,
	[MaLP] [nvarchar](10) NULL,
	[Anh] [nvarchar](50) NULL,
	[SLVote] [int] NULL,
	[MoTa] [nvarchar](max) NULL,
 CONSTRAINT [PK_Phong] PRIMARY KEY CLUSTERED 
(
	[MaPhong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuDungDichVu]    Script Date: 2/26/2024 3:45:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuDungDichVu](
	[MaDV] [nvarchar](10) NOT NULL,
	[SoHoaDon] [int] NOT NULL,
	[SoLuong] [int] NULL,
	[NgayMua] [date] NULL,
 CONSTRAINT [PK_SuDungDichVu_1] PRIMARY KEY CLUSTERED 
(
	[MaDV] ASC,
	[SoHoaDon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuDungThietBi]    Script Date: 2/26/2024 3:45:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuDungThietBi](
	[MaTB] [int] NOT NULL,
	[MaLP] [nvarchar](10) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tDoanhThu]    Script Date: 2/26/2024 3:45:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tDoanhThu](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Nam] [int] NULL,
	[Thang] [int] NULL,
	[TongTien] [float] NULL,
 CONSTRAINT [PK_tDoanhThu] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThietBi]    Script Date: 2/26/2024 3:45:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThietBi](
	[MaTB] [int] IDENTITY(1,1) NOT NULL,
	[TenTB] [nvarchar](50) NULL,
 CONSTRAINT [PK_ThietBi] PRIMARY KEY CLUSTERED 
(
	[MaTB] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TinhThanh]    Script Date: 2/26/2024 3:45:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TinhThanh](
	[MaTinh] [int] IDENTITY(1,1) NOT NULL,
	[TenTinh] [nvarchar](50) NULL,
	[Anh] [nvarchar](50) NULL,
 CONSTRAINT [PK_TinhThanh] PRIMARY KEY CLUSTERED 
(
	[MaTinh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2/26/2024 3:45:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] NOT NULL,
	[IdLoaiUser] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[CTAnh] ([TenAnh], [MaPhong]) VALUES (N'dlx.webp', 6)
INSERT [dbo].[CTAnh] ([TenAnh], [MaPhong]) VALUES (N'dlx1.webp', 6)
INSERT [dbo].[CTAnh] ([TenAnh], [MaPhong]) VALUES (N'dlx2.webp', 6)
INSERT [dbo].[CTAnh] ([TenAnh], [MaPhong]) VALUES (N'dlx3.webp', 6)
INSERT [dbo].[CTAnh] ([TenAnh], [MaPhong]) VALUES (N'dlx4.webp', 6)
INSERT [dbo].[CTAnh] ([TenAnh], [MaPhong]) VALUES (N'dlx5.webp', 6)
INSERT [dbo].[CTAnh] ([TenAnh], [MaPhong]) VALUES (N'std.jpg', 1)
INSERT [dbo].[CTAnh] ([TenAnh], [MaPhong]) VALUES (N'std1.jpg', 1)
INSERT [dbo].[CTAnh] ([TenAnh], [MaPhong]) VALUES (N'std2.jpg', 1)
INSERT [dbo].[CTAnh] ([TenAnh], [MaPhong]) VALUES (N'std3.jpg', 1)
INSERT [dbo].[CTAnh] ([TenAnh], [MaPhong]) VALUES (N'std4.jpg', 1)
INSERT [dbo].[CTAnh] ([TenAnh], [MaPhong]) VALUES (N'stdnen.jpg', 2)
INSERT [dbo].[CTAnh] ([TenAnh], [MaPhong]) VALUES (N'stdnen1.webp', 2)
INSERT [dbo].[CTAnh] ([TenAnh], [MaPhong]) VALUES (N'stdnen2.webp', 2)
INSERT [dbo].[CTAnh] ([TenAnh], [MaPhong]) VALUES (N'stdnen3.webp', 2)
INSERT [dbo].[CTAnh] ([TenAnh], [MaPhong]) VALUES (N'stdnen4.webp', 2)
GO
INSERT [dbo].[DatPhong] ([MaPhong], [SoHoaDon], [NgayDen], [NgayDi], [SoNguoi]) VALUES (1, 1, CAST(N'2024-01-24' AS Date), CAST(N'2024-01-27' AS Date), NULL)
INSERT [dbo].[DatPhong] ([MaPhong], [SoHoaDon], [NgayDen], [NgayDi], [SoNguoi]) VALUES (1, 2, CAST(N'2024-01-18' AS Date), CAST(N'2024-01-19' AS Date), NULL)
INSERT [dbo].[DatPhong] ([MaPhong], [SoHoaDon], [NgayDen], [NgayDi], [SoNguoi]) VALUES (1, 6, CAST(N'2024-02-17' AS Date), CAST(N'2024-02-20' AS Date), NULL)
INSERT [dbo].[DatPhong] ([MaPhong], [SoHoaDon], [NgayDen], [NgayDi], [SoNguoi]) VALUES (1, 7, CAST(N'2024-02-22' AS Date), CAST(N'2024-02-23' AS Date), 0)
INSERT [dbo].[DatPhong] ([MaPhong], [SoHoaDon], [NgayDen], [NgayDi], [SoNguoi]) VALUES (1, 8, CAST(N'2024-02-26' AS Date), CAST(N'2024-02-28' AS Date), 2)
INSERT [dbo].[DatPhong] ([MaPhong], [SoHoaDon], [NgayDen], [NgayDi], [SoNguoi]) VALUES (1, 1022, CAST(N'2024-02-29' AS Date), CAST(N'2024-02-29' AS Date), 3)
INSERT [dbo].[DatPhong] ([MaPhong], [SoHoaDon], [NgayDen], [NgayDi], [SoNguoi]) VALUES (2, 3, CAST(N'2024-01-20' AS Date), CAST(N'2024-01-24' AS Date), NULL)
INSERT [dbo].[DatPhong] ([MaPhong], [SoHoaDon], [NgayDen], [NgayDi], [SoNguoi]) VALUES (2, 4, CAST(N'2024-02-01' AS Date), CAST(N'2024-02-05' AS Date), NULL)
INSERT [dbo].[DatPhong] ([MaPhong], [SoHoaDon], [NgayDen], [NgayDi], [SoNguoi]) VALUES (2, 1016, CAST(N'2024-02-22' AS Date), CAST(N'2024-02-23' AS Date), 1)
INSERT [dbo].[DatPhong] ([MaPhong], [SoHoaDon], [NgayDen], [NgayDi], [SoNguoi]) VALUES (2, 1018, CAST(N'2024-02-24' AS Date), CAST(N'2024-02-26' AS Date), 2)
INSERT [dbo].[DatPhong] ([MaPhong], [SoHoaDon], [NgayDen], [NgayDi], [SoNguoi]) VALUES (2, 1019, CAST(N'2024-02-28' AS Date), CAST(N'2024-02-29' AS Date), 2)
INSERT [dbo].[DatPhong] ([MaPhong], [SoHoaDon], [NgayDen], [NgayDi], [SoNguoi]) VALUES (2, 1020, CAST(N'2024-03-01' AS Date), CAST(N'2024-03-01' AS Date), 1)
INSERT [dbo].[DatPhong] ([MaPhong], [SoHoaDon], [NgayDen], [NgayDi], [SoNguoi]) VALUES (2, 1021, CAST(N'2024-02-27' AS Date), CAST(N'2024-02-27' AS Date), 2)
INSERT [dbo].[DatPhong] ([MaPhong], [SoHoaDon], [NgayDen], [NgayDi], [SoNguoi]) VALUES (2, 1023, CAST(N'2024-03-05' AS Date), CAST(N'2024-03-05' AS Date), 2)
INSERT [dbo].[DatPhong] ([MaPhong], [SoHoaDon], [NgayDen], [NgayDi], [SoNguoi]) VALUES (2, 1024, CAST(N'2024-03-09' AS Date), CAST(N'2024-03-09' AS Date), 3)
INSERT [dbo].[DatPhong] ([MaPhong], [SoHoaDon], [NgayDen], [NgayDi], [SoNguoi]) VALUES (2, 1025, CAST(N'2024-03-08' AS Date), CAST(N'2024-03-08' AS Date), 2)
INSERT [dbo].[DatPhong] ([MaPhong], [SoHoaDon], [NgayDen], [NgayDi], [SoNguoi]) VALUES (2, 1026, CAST(N'2024-03-13' AS Date), CAST(N'2024-03-13' AS Date), 1)
GO
SET IDENTITY_INSERT [dbo].[HoaDon] ON 

INSERT [dbo].[HoaDon] ([SoHoaDon], [NgayThanhToan], [MaNV], [MaKH], [TenKH], [Email], [SDT]) VALUES (0, CAST(N'0001-01-01' AS Date), NULL, 1, NULL, NULL, NULL)
INSERT [dbo].[HoaDon] ([SoHoaDon], [NgayThanhToan], [MaNV], [MaKH], [TenKH], [Email], [SDT]) VALUES (1, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[HoaDon] ([SoHoaDon], [NgayThanhToan], [MaNV], [MaKH], [TenKH], [Email], [SDT]) VALUES (2, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[HoaDon] ([SoHoaDon], [NgayThanhToan], [MaNV], [MaKH], [TenKH], [Email], [SDT]) VALUES (3, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[HoaDon] ([SoHoaDon], [NgayThanhToan], [MaNV], [MaKH], [TenKH], [Email], [SDT]) VALUES (4, NULL, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[HoaDon] ([SoHoaDon], [NgayThanhToan], [MaNV], [MaKH], [TenKH], [Email], [SDT]) VALUES (6, CAST(N'2024-02-20' AS Date), NULL, 1, N'Đỗ Đức Việt', N'doduc123@gmail.com', N'0912345874')
INSERT [dbo].[HoaDon] ([SoHoaDon], [NgayThanhToan], [MaNV], [MaKH], [TenKH], [Email], [SDT]) VALUES (7, CAST(N'2024-02-23' AS Date), NULL, 1, N'Nguyễn Thị Linh', N'doducviet3@gmail.com', N'0924232846')
INSERT [dbo].[HoaDon] ([SoHoaDon], [NgayThanhToan], [MaNV], [MaKH], [TenKH], [Email], [SDT]) VALUES (8, CAST(N'2024-02-28' AS Date), NULL, 1, N'Nguyễn Thị Linh', N'doducviet4@gmail.com', N'0912345678')
INSERT [dbo].[HoaDon] ([SoHoaDon], [NgayThanhToan], [MaNV], [MaKH], [TenKH], [Email], [SDT]) VALUES (1016, CAST(N'2024-02-23' AS Date), NULL, 2, N'Viet', N'viet123@gmail.com', N'0123456789')
INSERT [dbo].[HoaDon] ([SoHoaDon], [NgayThanhToan], [MaNV], [MaKH], [TenKH], [Email], [SDT]) VALUES (1018, CAST(N'2024-02-26' AS Date), NULL, 2, N'Viet', N'viet123@gmail.com', N'0123456789')
INSERT [dbo].[HoaDon] ([SoHoaDon], [NgayThanhToan], [MaNV], [MaKH], [TenKH], [Email], [SDT]) VALUES (1019, CAST(N'2024-02-29' AS Date), NULL, 2, N'Viet', N'viet123@gmail.com', N'0123456789')
INSERT [dbo].[HoaDon] ([SoHoaDon], [NgayThanhToan], [MaNV], [MaKH], [TenKH], [Email], [SDT]) VALUES (1020, CAST(N'2024-03-01' AS Date), NULL, 5, N'viet', N'doducviet3012@gmail.com', N'0123456789')
INSERT [dbo].[HoaDon] ([SoHoaDon], [NgayThanhToan], [MaNV], [MaKH], [TenKH], [Email], [SDT]) VALUES (1021, CAST(N'2024-02-27' AS Date), NULL, 5, N'viet', N'doducviet3012@gmail.com', N'0123456789')
INSERT [dbo].[HoaDon] ([SoHoaDon], [NgayThanhToan], [MaNV], [MaKH], [TenKH], [Email], [SDT]) VALUES (1022, CAST(N'2024-02-29' AS Date), NULL, 5, N'viet', N'doducviet3012@gmail.com', N'0123456789')
INSERT [dbo].[HoaDon] ([SoHoaDon], [NgayThanhToan], [MaNV], [MaKH], [TenKH], [Email], [SDT]) VALUES (1023, CAST(N'2024-03-05' AS Date), NULL, 5, N'Đỗ Đức Việt', N'vietsex3012@gmail.com', N'0912345678')
INSERT [dbo].[HoaDon] ([SoHoaDon], [NgayThanhToan], [MaNV], [MaKH], [TenKH], [Email], [SDT]) VALUES (1024, CAST(N'2024-03-09' AS Date), NULL, 5, N'Đỗ Đức Việt', N'vietsex3012@gmail.com', N'0912345678')
INSERT [dbo].[HoaDon] ([SoHoaDon], [NgayThanhToan], [MaNV], [MaKH], [TenKH], [Email], [SDT]) VALUES (1025, CAST(N'2024-03-08' AS Date), NULL, 5, N'Đỗ Đức Việt', N'vietsex3012@gmail.com', N'0924232846')
INSERT [dbo].[HoaDon] ([SoHoaDon], [NgayThanhToan], [MaNV], [MaKH], [TenKH], [Email], [SDT]) VALUES (1026, CAST(N'2024-03-13' AS Date), NULL, 5, N'Đỗ Đức Việt', N'vietsex3012@gmail.com', N'0912345874')
SET IDENTITY_INSERT [dbo].[HoaDon] OFF
GO
SET IDENTITY_INSERT [dbo].[KhachHang] ON 

INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [GioiTinh], [CCCD], [Email], [Password], [NgaySinh], [DiaChi], [SDT], [LoaiKhachHang], [UserId], [HieuLuc]) VALUES (1, N'Viet', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [GioiTinh], [CCCD], [Email], [Password], [NgaySinh], [DiaChi], [SDT], [LoaiKhachHang], [UserId], [HieuLuc]) VALUES (2, N'Viet', 0, N'012345678911', N'viet123@gmail.com', N'e10adc3949ba59abbe56e057f20f883e', CAST(N'2333-02-11' AS Date), N'Hà Nội', N'0123456789', NULL, 1, 0)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [GioiTinh], [CCCD], [Email], [Password], [NgaySinh], [DiaChi], [SDT], [LoaiKhachHang], [UserId], [HieuLuc]) VALUES (3, N'viet', 1, N'23434343430', N'viet1234@gmail.com', N'e10adc3949ba59abbe56e057f20f883e', CAST(N'0222-02-22' AS Date), N'Hà Nội', N'0123456789', NULL, 1, 0)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [GioiTinh], [CCCD], [Email], [Password], [NgaySinh], [DiaChi], [SDT], [LoaiKhachHang], [UserId], [HieuLuc]) VALUES (4, N'viet', 1, N'12345678909', N'viet12345@gmail.com', N'e10adc3949ba59abbe56e057f20f883e', CAST(N'0323-02-11' AS Date), N'Hà Nội', N'0123456789', NULL, 1, 0)
INSERT [dbo].[KhachHang] ([MaKH], [TenKH], [GioiTinh], [CCCD], [Email], [Password], [NgaySinh], [DiaChi], [SDT], [LoaiKhachHang], [UserId], [HieuLuc]) VALUES (5, N'viet', 0, N'0123456789', N'doducviet3012@gmail.com', N'e10adc3949ba59abbe56e057f20f883e', CAST(N'2024-01-23' AS Date), N'Hà Nội', N'0123456789', NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[KhachHang] OFF
GO
SET IDENTITY_INSERT [dbo].[KhachSan] ON 

INSERT [dbo].[KhachSan] ([MaKS], [MaTinh], [TenKhachSan], [DiaChi], [MoTa], [Anh], [DanhGia]) VALUES (1, 1, N'Hotel Royal', N'Cầu Giấy Hà Nội', NULL, N'ks_hanoi.jpg', NULL)
INSERT [dbo].[KhachSan] ([MaKS], [MaTinh], [TenKhachSan], [DiaChi], [MoTa], [Anh], [DanhGia]) VALUES (2, 1, N'Apricot Hotel', N'Thanh Xuân Hà Nội', NULL, N'ks_hanoi1.jpg', NULL)
INSERT [dbo].[KhachSan] ([MaKS], [MaTinh], [TenKhachSan], [DiaChi], [MoTa], [Anh], [DanhGia]) VALUES (3, 1, N'Pan Pacific Hanoi', N'Thanh Trì Hà Nội', NULL, N'ks_hanoi2.jpg', NULL)
INSERT [dbo].[KhachSan] ([MaKS], [MaTinh], [TenKhachSan], [DiaChi], [MoTa], [Anh], [DanhGia]) VALUES (4, 3, N'Vinpearl Luxury Nha Trang', N'Nha Trang', NULL, N'ks_nhatrang.jpg', NULL)
INSERT [dbo].[KhachSan] ([MaKS], [MaTinh], [TenKhachSan], [DiaChi], [MoTa], [Anh], [DanhGia]) VALUES (5, 3, N'Amiana Resort Nha Trang', N'Nha Trang', NULL, N'ks_nhatrang1.jpg', NULL)
INSERT [dbo].[KhachSan] ([MaKS], [MaTinh], [TenKhachSan], [DiaChi], [MoTa], [Anh], [DanhGia]) VALUES (6, 3, N'Evason Ana Mandara Nha Trang', N'Nha Trang', NULL, N'ks_nhatrang2.jpg', NULL)
SET IDENTITY_INSERT [dbo].[KhachSan] OFF
GO
INSERT [dbo].[LoaiPhong] ([MaLP], [LoaiPhong], [SoNguoiToiDa], [Gia], [Anh], [ThongTin], [KichThuoc]) VALUES (N'DLX', N'Phòng Deluxe', 4, 800, N'dlx.webp', NULL, N'40m2')
INSERT [dbo].[LoaiPhong] ([MaLP], [LoaiPhong], [SoNguoiToiDa], [Gia], [Anh], [ThongTin], [KichThuoc]) VALUES (N'PGD', N'Phòng Family', 6, 900, N'fml.jpg', NULL, N'80m2')
INSERT [dbo].[LoaiPhong] ([MaLP], [LoaiPhong], [SoNguoiToiDa], [Gia], [Anh], [ThongTin], [KichThuoc]) VALUES (N'STD', N'Phòng Standard', 3, 500, N'std.jpg', NULL, N'25m2')
INSERT [dbo].[LoaiPhong] ([MaLP], [LoaiPhong], [SoNguoiToiDa], [Gia], [Anh], [ThongTin], [KichThuoc]) VALUES (N'SUP', N'Phòng Superior', 3, 400, N'sup.webp', NULL, N'30m2')
INSERT [dbo].[LoaiPhong] ([MaLP], [LoaiPhong], [SoNguoiToiDa], [Gia], [Anh], [ThongTin], [KichThuoc]) VALUES (N'SUT', N'Phòng Suite', 6, 1200, N'sui.jpg', NULL, N'100m2')
GO
SET IDENTITY_INSERT [dbo].[LoaiUser] ON 

INSERT [dbo].[LoaiUser] ([IdLoaiUser], [TenLoai]) VALUES (1, N'Admin')
INSERT [dbo].[LoaiUser] ([IdLoaiUser], [TenLoai]) VALUES (2, N'Nhân Viên')
INSERT [dbo].[LoaiUser] ([IdLoaiUser], [TenLoai]) VALUES (3, N'Khách Hàng')
SET IDENTITY_INSERT [dbo].[LoaiUser] OFF
GO
SET IDENTITY_INSERT [dbo].[NhanVien] ON 

INSERT [dbo].[NhanVien] ([MaNV], [TenNV], [GioiTinh], [Email], [Password], [CCCD], [NgaySinh], [DiaChi], [SDT], [ChucVu], [Anh], [UserId]) VALUES (1, N'Viet', 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[NhanVien] OFF
GO
SET IDENTITY_INSERT [dbo].[Phong] ON 

INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (1, 1, N'Phòng Standard Classic', N'Trống', N'STD', N'std.jpg', NULL, N'Mô Tả Phòng Standard

Phòng Standard là sự kết hợp hoàn hảo giữa thoải mái và tiện nghi. Với không gian rộng rãi và trang bị đầy đủ các tiện nghi cơ bản, phòng này là lựa chọn lý tưởng cho những du khách muốn tận hưởng kỳ nghỉ thoải mái mà không làm trống hết ngân sách.

Trong phòng Standard, bạn sẽ được trải nghiệm không gian ấm áp và trang nhã với trang trí tinh tế. Đèn sáng tự nhiên và bố cục thông minh tạo nên một không gian lý tưởng cho việc nghỉ ngơi và thư giãn.

Với giá cả hợp lý, phòng Standard không chỉ là nơi dừng chân tuyệt vời mà còn là điểm xuất phát hoàn hảo để khám phá và trải nghiệm mọi điều tuyệt vời mà địa điểm này mang đến. Hãy để phòng Standard trở thành điểm dừng lý tưởng cho chuyến hành trình của bạn.')
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (2, 1, N'Phòng Comfort Retreat', N'Trống', N'STD', N'stdnen1.webp', NULL, N'Mô Tả Phòng Standard    Phòng Standard là sự kết hợp hoàn hảo giữa thoải mái và tiện nghi. Với không gian rộng rãi và trang bị đầy đủ các tiện nghi cơ bản, phòng này là lựa chọn lý tưởng cho những du khách muốn tận hưởng kỳ nghỉ thoải mái mà không làm trống hết ngân sách.    Trong phòng Standard, bạn sẽ được trải nghiệm không gian ấm áp và trang nhã với trang trí tinh tế. Đèn sáng tự nhiên và bố cục thông minh tạo nên một không gian lý tưởng cho việc nghỉ ngơi và thư giãn.    Với giá cả hợp lý, phòng Standard không chỉ là nơi dừng chân tuyệt vời mà còn là điểm xuất phát hoàn hảo để khám phá và trải nghiệm mọi điều tuyệt vời mà địa điểm này mang đến. Hãy để phòng Standard trở thành điểm dừng lý tưởng cho chuyến hành trình của bạn.')
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (3, 1, N'Phòng Tranquil Oasis', N'Trống', N'STD', N'stdnen2.webp', NULL, NULL)
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (4, 1, N'Phòng Serenity', N'Trống', N'STD', N'stdnen3.webp', NULL, NULL)
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (5, 1, N'Phòng Relaxation Haven', N'Trống', N'STD', N'stdnen4.webp', NULL, NULL)
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (6, 1, N'Phòng Deluxe Serenade', N'Trống', N'DLX', N'dlxnen.jpg', NULL, N'Phòng Deluxe tại khách sạn chúng tôi là sự lựa chọn hoàn hảo cho những du khách đang tìm kiếm không gian lưu trú sang trọng và đẳng cấp. Với diện tích rộng lớn, phòng này mang lại cảm giác thoải mái và tiện nghi đỉnh cao.

Trải qua cửa vào, du khách sẽ ngay lập tức bị cuốn hút bởi sự sang trọng và tinh tế của trang trí nội thất. Mọi chi tiết đều được chăm chút kỹ lưỡng, từ đèn sáng tạo không gian tới sự sắp xếp thông minh của nội thất.

Phòng Deluxe không chỉ đơn thuần là nơi nghỉ ngơi mà còn là không gian riêng tư lý tưởng để tận hưởng những khoảnh khắc quý giá. Với sự linh hoạt trong sắp xếp, du khách có thể tận hưởng không gian ấm áp và trang nhã mà phòng mang lại.

Với giá cả phù hợp với dịch vụ cao cấp, phòng Deluxe tại khách sạn chúng tôi hứa hẹn mang đến trải nghiệm lưu trú đặc biệt và đáng nhớ cho mọi du khách.')
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (7, 1, N'Phòng Premier Bliss', N'Trống', N'DLX', N'dlxnen1.jpg', NULL, NULL)
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (8, 1, N'Phòng Elegance Enclave', N'Trống', N'DLX', N'dlxnen2.jpg', NULL, NULL)
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (9, 1, N'Phòng Luxury Radiance', N'Trống', N'DLX', N'dlxnen3.jpg', NULL, NULL)
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (10, 1, N'Phòng Opulent Sanctuary', N'Trống', N'DLX', N'dlxnen4.jpg', NULL, NULL)
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (11, 1, N'Gia Ðình Suite Harmony', N'Trống', N'PGD', N'fmlnen.jpg', NULL, NULL)
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (12, 1, N'Phòng Gia Ðình Joy', N'Trống', N'PGD', N'fmlnen1.jpg', NULL, NULL)
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (13, 1, N'Phòng Gia Ðình Spacious Retreat', N'Trống', N'PGD', N'fmlnen2.jpg', NULL, NULL)
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (14, 1, N'Phòng Gia Ðình Cozy Haven', N'Trống', N'PGD', N'fmlnen3.jpg', NULL, NULL)
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (15, 1, N'Gia Ðình Suite Comfort', N'Trống', N'PGD', N'fmlnen3.jpg', NULL, NULL)
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (17, 1, N'Suite Royal Elegance', N'Trống', N'SUT', N'suinen.webp', NULL, NULL)
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (18, 1, N'Suite Prestige Oasis', N'Trống', N'SUT', N'suinen1.webp', NULL, NULL)
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (19, 1, N'Suite Panorama Splendor', N'Trống', N'SUT', N'suinen2.webp', NULL, NULL)
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (20, 1, N'Suite Executive Tranquility', N'Trống', N'SUT', N'suinen3.webp', NULL, NULL)
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (21, 1, N'Suite Grandeur Retreat', N'Trống', N'SUT', N'suinen4.webp', NULL, NULL)
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (22, 1, N'Superior Comfort Room', N'Trống', N'SUP', N'supnen.webp', NULL, NULL)
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (23, 1, N'Premium Superior Suite', N'Trống', N'SUP', N'supnen1.webp', NULL, NULL)
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (24, 1, N'Elegant Superior Retreat', N'Trống', N'SUP', N'supnen2.webp', NULL, NULL)
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (25, 1, N'Superior Panorama View', N'Trống', N'SUP', N'supnen3.webp', NULL, NULL)
INSERT [dbo].[Phong] ([MaPhong], [MaKS], [TenPhong], [TinhTrang], [MaLP], [Anh], [SLVote], [MoTa]) VALUES (26, 5, N'Tranquil Superior Oasis', N'Trống', N'SUP', N'supnen4.webp', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Phong] OFF
GO
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (1, N'DLX')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (1, N'PGD')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (1, N'STD')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (1, N'SUP')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (1, N'SUT')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (2, N'DLX')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (2, N'PGD')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (2, N'STD')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (2, N'SUP')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (2, N'SUT')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (3, N'DLX')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (3, N'PGD')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (3, N'STD')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (3, N'SUP')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (3, N'SUT')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (4, N'DLX')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (4, N'PGD')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (4, N'STD')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (4, N'SUP')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (4, N'SUT')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (5, N'DLX')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (5, N'PGD')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (5, N'STD')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (5, N'SUP')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (5, N'SUT')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (6, N'DLX')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (6, N'PGD')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (6, N'SUP')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (6, N'SUT')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (7, N'PGD')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (7, N'SUP')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (7, N'SUT')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (8, N'PGD')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (9, N'SUP')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (9, N'SUT')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (10, N'SUT')
INSERT [dbo].[SuDungThietBi] ([MaTB], [MaLP]) VALUES (11, N'SUT')
GO
SET IDENTITY_INSERT [dbo].[ThietBi] ON 

INSERT [dbo].[ThietBi] ([MaTB], [TenTB]) VALUES (1, N'Wi-Fi miễn phí')
INSERT [dbo].[ThietBi] ([MaTB], [TenTB]) VALUES (2, N'Vệ sinh phòng hằng ngày')
INSERT [dbo].[ThietBi] ([MaTB], [TenTB]) VALUES (3, N'Điều hòa hai chiều')
INSERT [dbo].[ThietBi] ([MaTB], [TenTB]) VALUES (4, N'Dịch vụ phòng 24/7')
INSERT [dbo].[ThietBi] ([MaTB], [TenTB]) VALUES (5, N'Truyền hình cáp')
INSERT [dbo].[ThietBi] ([MaTB], [TenTB]) VALUES (6, N'MiniBar với đồ uống miễn phí')
INSERT [dbo].[ThietBi] ([MaTB], [TenTB]) VALUES (7, N'Bể bơi đặc biệt')
INSERT [dbo].[ThietBi] ([MaTB], [TenTB]) VALUES (8, N'Khu vui chơi cho trẻ em')
INSERT [dbo].[ThietBi] ([MaTB], [TenTB]) VALUES (9, N'Đồ nội thất cao cấp')
INSERT [dbo].[ThietBi] ([MaTB], [TenTB]) VALUES (10, N'Phòng tắm cao cấp và sang trọng')
INSERT [dbo].[ThietBi] ([MaTB], [TenTB]) VALUES (11, N'Phòng ngủ và khách riêng biệt')
SET IDENTITY_INSERT [dbo].[ThietBi] OFF
GO
SET IDENTITY_INSERT [dbo].[TinhThanh] ON 

INSERT [dbo].[TinhThanh] ([MaTinh], [TenTinh], [Anh]) VALUES (1, N'Hà Nội', N'hanoi_nen.jpg')
INSERT [dbo].[TinhThanh] ([MaTinh], [TenTinh], [Anh]) VALUES (2, N'Đà Nẵng', N'danang_nen.jpg')
INSERT [dbo].[TinhThanh] ([MaTinh], [TenTinh], [Anh]) VALUES (3, N'Nha Trang', N'nhatrang_nen.jpg')
INSERT [dbo].[TinhThanh] ([MaTinh], [TenTinh], [Anh]) VALUES (4, N'Quảng Ninh', N'quangninh_nen.webp')
INSERT [dbo].[TinhThanh] ([MaTinh], [TenTinh], [Anh]) VALUES (5, N'Hồ Chí Minh', N'hcm_nen.jpg')
SET IDENTITY_INSERT [dbo].[TinhThanh] OFF
GO
INSERT [dbo].[User] ([UserId], [IdLoaiUser]) VALUES (1, 3)
GO
ALTER TABLE [dbo].[Blog]  WITH CHECK ADD  CONSTRAINT [FK_Blog_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[Blog] CHECK CONSTRAINT [FK_Blog_NhanVien]
GO
ALTER TABLE [dbo].[CTAnh]  WITH CHECK ADD  CONSTRAINT [FK_CTAnh_Phong] FOREIGN KEY([MaPhong])
REFERENCES [dbo].[Phong] ([MaPhong])
GO
ALTER TABLE [dbo].[CTAnh] CHECK CONSTRAINT [FK_CTAnh_Phong]
GO
ALTER TABLE [dbo].[DatPhong]  WITH CHECK ADD  CONSTRAINT [FK_DatPhong_HoaDon] FOREIGN KEY([SoHoaDon])
REFERENCES [dbo].[HoaDon] ([SoHoaDon])
GO
ALTER TABLE [dbo].[DatPhong] CHECK CONSTRAINT [FK_DatPhong_HoaDon]
GO
ALTER TABLE [dbo].[DatPhong]  WITH CHECK ADD  CONSTRAINT [FK_DatPhong_Phong] FOREIGN KEY([MaPhong])
REFERENCES [dbo].[Phong] ([MaPhong])
GO
ALTER TABLE [dbo].[DatPhong] CHECK CONSTRAINT [FK_DatPhong_Phong]
GO
ALTER TABLE [dbo].[GopY]  WITH CHECK ADD  CONSTRAINT [FK_GopY_KhachHang] FOREIGN KEY([MaKH])
REFERENCES [dbo].[KhachHang] ([MaKH])
GO
ALTER TABLE [dbo].[GopY] CHECK CONSTRAINT [FK_GopY_KhachHang]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_KhachHang] FOREIGN KEY([MaKH])
REFERENCES [dbo].[KhachHang] ([MaKH])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_KhachHang]
GO
ALTER TABLE [dbo].[HoaDon]  WITH CHECK ADD  CONSTRAINT [FK_HoaDon_NhanVien] FOREIGN KEY([MaNV])
REFERENCES [dbo].[NhanVien] ([MaNV])
GO
ALTER TABLE [dbo].[HoaDon] CHECK CONSTRAINT [FK_HoaDon_NhanVien]
GO
ALTER TABLE [dbo].[KhachHang]  WITH CHECK ADD  CONSTRAINT [FK_KhachHang_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[KhachHang] CHECK CONSTRAINT [FK_KhachHang_User]
GO
ALTER TABLE [dbo].[KhachSan]  WITH CHECK ADD  CONSTRAINT [FK_KhachSan_TinhThanh] FOREIGN KEY([MaTinh])
REFERENCES [dbo].[TinhThanh] ([MaTinh])
GO
ALTER TABLE [dbo].[KhachSan] CHECK CONSTRAINT [FK_KhachSan_TinhThanh]
GO
ALTER TABLE [dbo].[NhanVien]  WITH CHECK ADD  CONSTRAINT [FK_NhanVien_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[NhanVien] CHECK CONSTRAINT [FK_NhanVien_User]
GO
ALTER TABLE [dbo].[Phong]  WITH CHECK ADD  CONSTRAINT [FK_Phong_KhachSan] FOREIGN KEY([MaKS])
REFERENCES [dbo].[KhachSan] ([MaKS])
GO
ALTER TABLE [dbo].[Phong] CHECK CONSTRAINT [FK_Phong_KhachSan]
GO
ALTER TABLE [dbo].[Phong]  WITH CHECK ADD  CONSTRAINT [FK_Phong_LoaiPhong] FOREIGN KEY([MaLP])
REFERENCES [dbo].[LoaiPhong] ([MaLP])
GO
ALTER TABLE [dbo].[Phong] CHECK CONSTRAINT [FK_Phong_LoaiPhong]
GO
ALTER TABLE [dbo].[SuDungDichVu]  WITH CHECK ADD  CONSTRAINT [FK_SuDungDichVu_DichVu] FOREIGN KEY([MaDV])
REFERENCES [dbo].[DichVu] ([MaDV])
GO
ALTER TABLE [dbo].[SuDungDichVu] CHECK CONSTRAINT [FK_SuDungDichVu_DichVu]
GO
ALTER TABLE [dbo].[SuDungDichVu]  WITH CHECK ADD  CONSTRAINT [FK_SuDungDichVu_HoaDon] FOREIGN KEY([SoHoaDon])
REFERENCES [dbo].[HoaDon] ([SoHoaDon])
GO
ALTER TABLE [dbo].[SuDungDichVu] CHECK CONSTRAINT [FK_SuDungDichVu_HoaDon]
GO
ALTER TABLE [dbo].[SuDungThietBi]  WITH CHECK ADD  CONSTRAINT [FK_SuDungThietBi_LoaiPhong] FOREIGN KEY([MaLP])
REFERENCES [dbo].[LoaiPhong] ([MaLP])
GO
ALTER TABLE [dbo].[SuDungThietBi] CHECK CONSTRAINT [FK_SuDungThietBi_LoaiPhong]
GO
ALTER TABLE [dbo].[SuDungThietBi]  WITH CHECK ADD  CONSTRAINT [FK_SuDungThietBi_ThietBi] FOREIGN KEY([MaTB])
REFERENCES [dbo].[ThietBi] ([MaTB])
GO
ALTER TABLE [dbo].[SuDungThietBi] CHECK CONSTRAINT [FK_SuDungThietBi_ThietBi]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_LoaiUser] FOREIGN KEY([IdLoaiUser])
REFERENCES [dbo].[LoaiUser] ([IdLoaiUser])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_LoaiUser]
GO
USE [master]
GO
ALTER DATABASE [QLKhachSanTTTN] SET  READ_WRITE 
GO
