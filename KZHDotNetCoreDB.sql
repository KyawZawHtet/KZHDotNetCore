USE [master]
GO
/****** Object:  Database [KZHDotNetCore]    Script Date: 4/23/2024 9:03:22 AM ******/
CREATE DATABASE [KZHDotNetCore] ON  PRIMARY 
( NAME = N'KZHDotNetCore', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\KZHDotNetCore.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'KZHDotNetCore_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\KZHDotNetCore_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [KZHDotNetCore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [KZHDotNetCore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [KZHDotNetCore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [KZHDotNetCore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [KZHDotNetCore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [KZHDotNetCore] SET ARITHABORT OFF 
GO
ALTER DATABASE [KZHDotNetCore] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [KZHDotNetCore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [KZHDotNetCore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [KZHDotNetCore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [KZHDotNetCore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [KZHDotNetCore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [KZHDotNetCore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [KZHDotNetCore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [KZHDotNetCore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [KZHDotNetCore] SET  DISABLE_BROKER 
GO
ALTER DATABASE [KZHDotNetCore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [KZHDotNetCore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [KZHDotNetCore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [KZHDotNetCore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [KZHDotNetCore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [KZHDotNetCore] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [KZHDotNetCore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [KZHDotNetCore] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [KZHDotNetCore] SET  MULTI_USER 
GO
ALTER DATABASE [KZHDotNetCore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [KZHDotNetCore] SET DB_CHAINING OFF 
GO
USE [KZHDotNetCore]
GO
/****** Object:  Table [dbo].[Tbl_Blog]    Script Date: 4/23/2024 9:03:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Blog](
	[BlogId] [int] IDENTITY(1,1) NOT NULL,
	[BlogTitle] [varchar](50) NULL,
	[BlogAuthor] [varchar](50) NULL,
	[BlogContent] [varchar](50) NULL,
 CONSTRAINT [PK_Tbl_Blog] PRIMARY KEY CLUSTERED 
(
	[BlogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Tbl_Blog] ON 

INSERT [dbo].[Tbl_Blog] ([BlogId], [BlogTitle], [BlogAuthor], [BlogContent]) VALUES (1, N'Test Title', N'Test Author', N'Test Content')
INSERT [dbo].[Tbl_Blog] ([BlogId], [BlogTitle], [BlogAuthor], [BlogContent]) VALUES (2, N'Test Title', N'Test Author', N'Test Content')
INSERT [dbo].[Tbl_Blog] ([BlogId], [BlogTitle], [BlogAuthor], [BlogContent]) VALUES (3, N'Test Title', N'Test Author', N'Test Content')
INSERT [dbo].[Tbl_Blog] ([BlogId], [BlogTitle], [BlogAuthor], [BlogContent]) VALUES (4, N'Test Title', N'Test Author', N'Test Content')
INSERT [dbo].[Tbl_Blog] ([BlogId], [BlogTitle], [BlogAuthor], [BlogContent]) VALUES (5, N'Test Title', N'Test Author', N'Test Content')
INSERT [dbo].[Tbl_Blog] ([BlogId], [BlogTitle], [BlogAuthor], [BlogContent]) VALUES (6, N'Test Title', N'Test Author', N'Test Content')
INSERT [dbo].[Tbl_Blog] ([BlogId], [BlogTitle], [BlogAuthor], [BlogContent]) VALUES (7, N'Test Title', N'Test Author', N'Test Content')
INSERT [dbo].[Tbl_Blog] ([BlogId], [BlogTitle], [BlogAuthor], [BlogContent]) VALUES (8, N'Test Title', N'Test Author', N'Test Content')
INSERT [dbo].[Tbl_Blog] ([BlogId], [BlogTitle], [BlogAuthor], [BlogContent]) VALUES (9, N'Test Title', N'Test Author', N'Test Content')
INSERT [dbo].[Tbl_Blog] ([BlogId], [BlogTitle], [BlogAuthor], [BlogContent]) VALUES (10, N'Test Title', N'Test Author', N'Test Content')
INSERT [dbo].[Tbl_Blog] ([BlogId], [BlogTitle], [BlogAuthor], [BlogContent]) VALUES (11, N'title', N'author', N'content')
INSERT [dbo].[Tbl_Blog] ([BlogId], [BlogTitle], [BlogAuthor], [BlogContent]) VALUES (12, N'test title', N'test author', N'test content')
INSERT [dbo].[Tbl_Blog] ([BlogId], [BlogTitle], [BlogAuthor], [BlogContent]) VALUES (15, N'new title another', N'new author another', N'new content another')
SET IDENTITY_INSERT [dbo].[Tbl_Blog] OFF
GO
USE [master]
GO
ALTER DATABASE [KZHDotNetCore] SET  READ_WRITE 
GO
