SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL Serializable
GO
BEGIN TRANSACTION
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[AspNetRoles]'
GO
CREATE TABLE [dbo].[AspNetRoles]
(
[Id] [nvarchar] (450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[NormalizedName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ConcurrencyStamp] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_AspNetRoles] on [dbo].[AspNetRoles]'
GO
ALTER TABLE [dbo].[AspNetRoles] ADD CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [RoleNameIndex] on [dbo].[AspNetRoles]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles] ([NormalizedName]) WHERE ([NormalizedName] IS NOT NULL)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[AspNetRoleClaims]'
GO
CREATE TABLE [dbo].[AspNetRoleClaims]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[RoleId] [nvarchar] (450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ClaimType] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ClaimValue] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_AspNetRoleClaims] on [dbo].[AspNetRoleClaims]'
GO
ALTER TABLE [dbo].[AspNetRoleClaims] ADD CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_AspNetRoleClaims_RoleId] on [dbo].[AspNetRoleClaims]'
GO
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims] ([RoleId])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[AspNetUsers]'
GO
CREATE TABLE [dbo].[AspNetUsers]
(
[Id] [nvarchar] (450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Email] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[EmailConfirmed] [bit] NOT NULL,
[PasswordHash] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SecurityStamp] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PhoneNumber] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PhoneNumberConfirmed] [bit] NOT NULL,
[TwoFactorEnabled] [bit] NOT NULL,
[LockoutEnabled] [bit] NOT NULL,
[AccessFailedCount] [int] NOT NULL,
[UserName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Address] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[City] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[State] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PostalCode] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FirstName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LastName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[NormalizedUserName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[NormalizedEmail] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ConcurrencyStamp] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LockoutEnd] [datetimeoffset] NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_AspNetUsers] on [dbo].[AspNetUsers]'
GO
ALTER TABLE [dbo].[AspNetUsers] ADD CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [EmailIndex] on [dbo].[AspNetUsers]'
GO
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers] ([NormalizedEmail])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [UserNameIndex] on [dbo].[AspNetUsers]'
GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers] ([NormalizedUserName]) WHERE ([NormalizedUserName] IS NOT NULL)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[AspNetUserClaims]'
GO
CREATE TABLE [dbo].[AspNetUserClaims]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[UserId] [nvarchar] (450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ClaimType] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ClaimValue] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_AspNetUserClaims] on [dbo].[AspNetUserClaims]'
GO
ALTER TABLE [dbo].[AspNetUserClaims] ADD CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_AspNetUserClaims_UserId] on [dbo].[AspNetUserClaims]'
GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims] ([UserId])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[AspNetUserLogins]'
GO
CREATE TABLE [dbo].[AspNetUserLogins]
(
[LoginProvider] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ProviderKey] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[UserId] [nvarchar] (450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ProviderDisplayName] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_AspNetUserLogins] on [dbo].[AspNetUserLogins]'
GO
ALTER TABLE [dbo].[AspNetUserLogins] ADD CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED  ([LoginProvider], [ProviderKey])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_AspNetUserLogins_UserId] on [dbo].[AspNetUserLogins]'
GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins] ([UserId])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[AspNetUserRoles]'
GO
CREATE TABLE [dbo].[AspNetUserRoles]
(
[UserId] [nvarchar] (450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[RoleId] [nvarchar] (450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_AspNetUserRoles] on [dbo].[AspNetUserRoles]'
GO
ALTER TABLE [dbo].[AspNetUserRoles] ADD CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED  ([UserId], [RoleId])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating index [IX_AspNetUserRoles_RoleId] on [dbo].[AspNetUserRoles]'
GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles] ([RoleId])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[AspNetUserTokens]'
GO
CREATE TABLE [dbo].[AspNetUserTokens]
(
[UserId] [nvarchar] (450) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[LoginProvider] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Name] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Value] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_AspNetUserTokens] on [dbo].[AspNetUserTokens]'
GO
ALTER TABLE [dbo].[AspNetUserTokens] ADD CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED  ([UserId], [LoginProvider], [Name])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[Styles]'
GO
CREATE TABLE [dbo].[Styles]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ProductId] [int] NOT NULL,
[SizeId] [int] NOT NULL,
[ColorId] [int] NOT NULL,
[Price] [smallmoney] NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_Styles_Id] on [dbo].[Styles]'
GO
ALTER TABLE [dbo].[Styles] ADD CONSTRAINT [PK_Styles_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding constraints to [dbo].[Styles]'
GO
ALTER TABLE [dbo].[Styles] ADD CONSTRAINT [UQ_Styles_ColorId_ProductId_SizeId] UNIQUE NONCLUSTERED  ([ColorId], [ProductId], [SizeId])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[Cart]'
GO
CREATE TABLE [dbo].[Cart]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CartId] [varchar] (68) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[StyleId] [int] NOT NULL,
[Count] [int] NOT NULL,
[DateCreated] [datetime] NOT NULL CONSTRAINT [DF__Cart__DateCreate__10566F31] DEFAULT (getutcdate())
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_Cart_Id] on [dbo].[Cart]'
GO
ALTER TABLE [dbo].[Cart] ADD CONSTRAINT [PK_Cart_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding constraints to [dbo].[Cart]'
GO
ALTER TABLE [dbo].[Cart] ADD CONSTRAINT [UQ_Cart_CartId_StyleId] UNIQUE NONCLUSTERED  ([CartId], [StyleId])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[Products]'
GO
CREATE TABLE [dbo].[Products]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ImageId] [int] NULL,
[ProductName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LinkName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_Products_Id] on [dbo].[Products]'
GO
ALTER TABLE [dbo].[Products] ADD CONSTRAINT [PK_Products_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[CartImages]'
GO
CREATE TABLE [dbo].[CartImages]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ProductId] [int] NOT NULL,
[ImageData] [varbinary] (max) NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_CartImages_Id] on [dbo].[CartImages]'
GO
ALTER TABLE [dbo].[CartImages] ADD CONSTRAINT [PK_CartImages_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[Images]'
GO
CREATE TABLE [dbo].[Images]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ImageData] [varbinary] (max) NOT NULL,
[MimeType] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Width] [smallint] NULL,
[Height] [smallint] NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_Images_Id] on [dbo].[Images]'
GO
ALTER TABLE [dbo].[Images] ADD CONSTRAINT [PK_Images_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding constraints to [dbo].[Images]'
GO
ALTER TABLE [dbo].[Images] ADD CONSTRAINT [UQ_Imagess_Name] UNIQUE NONCLUSTERED  ([Name])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[Categories]'
GO
CREATE TABLE [dbo].[Categories]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CategoryName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ImageId] [int] NULL,
[ImageUrl] [nvarchar] (2083) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Published] [bit] NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_Categories_Id] on [dbo].[Categories]'
GO
ALTER TABLE [dbo].[Categories] ADD CONSTRAINT [PK_Categories_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding constraints to [dbo].[Categories]'
GO
ALTER TABLE [dbo].[Categories] ADD CONSTRAINT [UQ_Categories_CategoryName] UNIQUE NONCLUSTERED  ([CategoryName])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[CategoryImages]'
GO
CREATE TABLE [dbo].[CategoryImages]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[CategoryId] [int] NOT NULL,
[ImageData] [varbinary] (max) NULL,
[MimeType] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_CategoryImages_Id] on [dbo].[CategoryImages]'
GO
ALTER TABLE [dbo].[CategoryImages] ADD CONSTRAINT [PK_CategoryImages_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[Orders]'
GO
CREATE TABLE [dbo].[Orders]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[UserName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SessionId] [char] (24) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IpAddress] [varchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Shipping] [smallmoney] NULL,
[Total] [smallmoney] NULL,
[DateCreated] [datetime] NULL CONSTRAINT [DF__Orders__DateCrea__114A936A] DEFAULT (getutcdate()),
[FirstName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LastName] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[City] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[State] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Zip] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SubTotal] [smallmoney] NULL,
[Email] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Phone] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Address] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[InvoiceId] [int] NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_Orders_Id] on [dbo].[Orders]'
GO
ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [PK_Orders_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[OrderItems]'
GO
CREATE TABLE [dbo].[OrderItems]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[OrderId] [int] NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Quantity] [int] NULL,
[Price] [smallmoney] NULL,
[SubTotal] [smallmoney] NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_OrderItems_Id] on [dbo].[OrderItems]'
GO
ALTER TABLE [dbo].[OrderItems] ADD CONSTRAINT [PK_OrderItems_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[OpenGraphTags]'
GO
CREATE TABLE [dbo].[OpenGraphTags]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Property] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_OpenGraphTags_Id] on [dbo].[OpenGraphTags]'
GO
ALTER TABLE [dbo].[OpenGraphTags] ADD CONSTRAINT [PK_OpenGraphTags_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding constraints to [dbo].[OpenGraphTags]'
GO
ALTER TABLE [dbo].[OpenGraphTags] ADD CONSTRAINT [UQ_OpenGraphTags_Tag] UNIQUE NONCLUSTERED  ([Property])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[PageOpenGraphTags]'
GO
CREATE TABLE [dbo].[PageOpenGraphTags]
(
[PageId] [int] NOT NULL,
[OpenGraphTagId] [int] NOT NULL,
[Content] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_PageOpenGraphTags_PageId_OpenGraphTagId] on [dbo].[PageOpenGraphTags]'
GO
ALTER TABLE [dbo].[PageOpenGraphTags] ADD CONSTRAINT [PK_PageOpenGraphTags_PageId_OpenGraphTagId] PRIMARY KEY CLUSTERED  ([PageId], [OpenGraphTagId])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[Pages]'
GO
CREATE TABLE [dbo].[Pages]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_Pages_Id] on [dbo].[Pages]'
GO
ALTER TABLE [dbo].[Pages] ADD CONSTRAINT [PK_Pages_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding constraints to [dbo].[Pages]'
GO
ALTER TABLE [dbo].[Pages] ADD CONSTRAINT [UQ_Pages_Name] UNIQUE NONCLUSTERED  ([Name])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[ProductCategories]'
GO
CREATE TABLE [dbo].[ProductCategories]
(
[CategoryId] [int] NOT NULL,
[ProductId] [int] NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_ProductCategories_CategoryId_ProductId] on [dbo].[ProductCategories]'
GO
ALTER TABLE [dbo].[ProductCategories] ADD CONSTRAINT [PK_ProductCategories_CategoryId_ProductId] PRIMARY KEY CLUSTERED  ([CategoryId], [ProductId])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[ProductImages]'
GO
CREATE TABLE [dbo].[ProductImages]
(
[ProductId] [int] NOT NULL,
[ImageId] [int] NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_ProductImages_ImageId_ProductId] on [dbo].[ProductImages]'
GO
ALTER TABLE [dbo].[ProductImages] ADD CONSTRAINT [PK_ProductImages_ImageId_ProductId] PRIMARY KEY CLUSTERED  ([ImageId], [ProductId])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[Reviews]'
GO
CREATE TABLE [dbo].[Reviews]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Email] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ReviewText] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[DateCreated] [datetime] NULL CONSTRAINT [DF__Reviews__DateCre__123EB7A3] DEFAULT (getutcdate()),
[Rating] [decimal] (18, 0) NOT NULL CONSTRAINT [DF__Reviews__Rating__1332DBDC] DEFAULT ((5)),
[ProductId] [int] NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_Reviews_Id] on [dbo].[Reviews]'
GO
ALTER TABLE [dbo].[Reviews] ADD CONSTRAINT [PK_Reviews_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[Colors]'
GO
CREATE TABLE [dbo].[Colors]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[ColorText] [nvarchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_Colors_Id] on [dbo].[Colors]'
GO
ALTER TABLE [dbo].[Colors] ADD CONSTRAINT [PK_Colors_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding constraints to [dbo].[Colors]'
GO
ALTER TABLE [dbo].[Colors] ADD CONSTRAINT [UQ_Colors_ColorText] UNIQUE NONCLUSTERED  ([ColorText])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[Sizes]'
GO
CREATE TABLE [dbo].[Sizes]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[SizeText] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SizeOrder] [int] NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_Sizes_Id] on [dbo].[Sizes]'
GO
ALTER TABLE [dbo].[Sizes] ADD CONSTRAINT [PK_Sizes_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding constraints to [dbo].[Sizes]'
GO
ALTER TABLE [dbo].[Sizes] ADD CONSTRAINT [UQ_Sizes_SizeOrder] UNIQUE NONCLUSTERED  ([SizeOrder])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding constraints to [dbo].[Sizes]'
GO
ALTER TABLE [dbo].[Sizes] ADD CONSTRAINT [UQ_Sizes_SizeText] UNIQUE NONCLUSTERED  ([SizeText])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[CartView]'
GO

create view [dbo].[CartView] AS
SELECT Cart.Id AS Id,CartId, [Count] As Quantity,Styles.ProductId,ProductName,LinkName,Price, StyleId,Colors.ColorText,Products.[Description],
CONCAT('Color: ',ColorText,'; Size: ',SizeText) As StyleText,
([Count] * Price) AS SubTotal,CartImages.ImageData as ImageData
FROM Cart INNER JOIN Styles ON Cart.StyleId = Styles.Id
	INNER JOIN Products ON Styles.ProductId = Products.Id
	INNER JOIN Sizes On Styles.SizeId = Sizes.Id
	INNER JOIN Colors On Styles.ColorId = Colors.Id
	Left JOIN CartImages On CartImages.ProductId = Products.Id
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[StyleView]'
GO

create view [dbo].[StyleView] as
select Styles.Id as Id,ProductId,SizeId,SizeOrder,SizeText,ColorId,ColorText,Price,SizeText + ' / ' + ColorText AS StyleText
from Styles inner join Sizes ON SizeId = Sizes.id
			inner join Colors on ColorId = Colors.Id
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[GuestUsers]'
GO
CREATE TABLE [dbo].[GuestUsers]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[SessionId] [char] (24) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FirstName] [nvarchar] (60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LastName] [nvarchar] (60) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PhoneNumber] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Email] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Address] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[City] [nvarchar] (75) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[State] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PostalCode] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_GuestUsers_Id] on [dbo].[GuestUsers]'
GO
ALTER TABLE [dbo].[GuestUsers] ADD CONSTRAINT [PK_GuestUsers_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[Invoices]'
GO
CREATE TABLE [dbo].[Invoices]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[SessionId] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Paid] [bit] NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[MailSettings]'
GO
CREATE TABLE [dbo].[MailSettings]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Port] [int] NOT NULL,
[SmtpServer] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Username] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Password] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK__MailSett__3214EC07A0FDE725] on [dbo].[MailSettings]'
GO
ALTER TABLE [dbo].[MailSettings] ADD CONSTRAINT [PK__MailSett__3214EC07A0FDE725] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[PayPalPayments]'
GO
CREATE TABLE [dbo].[PayPalPayments]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[tx] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[amt] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[cc] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_PaypalPayments_Id] on [dbo].[PayPalPayments]'
GO
ALTER TABLE [dbo].[PayPalPayments] ADD CONSTRAINT [PK_PaypalPayments_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[SiteSettings]'
GO
CREATE TABLE [dbo].[SiteSettings]
(
[SiteTitle] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Keywords] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Author] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Id] [int] NOT NULL IDENTITY(1, 1),
[AboutUs] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[GoogleClientId] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[GoogleClientSecret] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SiteUrl] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SiteLogoUrl] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FacebookAppId] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MailChimpApiKey] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MailChimpListId] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PaypalApiUsername] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ContactPhoneNumber] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ContactEmailAddress] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TwitterUrl] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FaceBookUrl] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LinkedInUrl] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CopyrightText] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CopyrightUrl] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CopyrightLinktext] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MicrosoftClientId] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MicrosoftClientSecret] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FacebookClientId] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FacebookClientSecret] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TwitterClientId] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TwitterClientSecret] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[InstagramClientId] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[InstagramClientSecret] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PaypalEndpointUrl] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FreeShippingThreshold] [money] NULL,
[FlatShippingRate] [money] NULL,
[PaypalSandboxAccount] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PaypalSandBoxClientId] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PaypalSandBoxSecret] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PaypalClientSecret] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PaypalClientId] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SmtpHost] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SmtpUsername] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SmtpPassword] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SmtpPort] [int] NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_SiteSettings_Id] on [dbo].[SiteSettings]'
GO
ALTER TABLE [dbo].[SiteSettings] ADD CONSTRAINT [PK_SiteSettings_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[AspNetRoleClaims]'
GO
ALTER TABLE [dbo].[AspNetRoleClaims] ADD CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[AspNetUserRoles]'
GO
ALTER TABLE [dbo].[AspNetUserRoles] ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[AspNetUserClaims]'
GO
ALTER TABLE [dbo].[AspNetUserClaims] ADD CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[AspNetUserLogins]'
GO
ALTER TABLE [dbo].[AspNetUserLogins] ADD CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[AspNetUserTokens]'
GO
ALTER TABLE [dbo].[AspNetUserTokens] ADD CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[CartImages]'
GO
ALTER TABLE [dbo].[CartImages] ADD CONSTRAINT [FK_CartImages_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[Cart]'
GO
ALTER TABLE [dbo].[Cart] ADD CONSTRAINT [FK_Cart_StyleId] FOREIGN KEY ([StyleId]) REFERENCES [dbo].[Styles] ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[CategoryImages]'
GO
ALTER TABLE [dbo].[CategoryImages] ADD CONSTRAINT [FK_CategoryImages_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[ProductCategories]'
GO
ALTER TABLE [dbo].[ProductCategories] ADD CONSTRAINT [FK_ProductCategories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[ProductCategories] ADD CONSTRAINT [FK_ProductCategories_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[Categories]'
GO
ALTER TABLE [dbo].[Categories] ADD CONSTRAINT [FK_Categories_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [dbo].[Images] ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[Styles]'
GO
ALTER TABLE [dbo].[Styles] ADD CONSTRAINT [FK_Styles_ColorId] FOREIGN KEY ([ColorId]) REFERENCES [dbo].[Colors] ([Id])
GO
ALTER TABLE [dbo].[Styles] ADD CONSTRAINT [FK_Styles_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[Styles] ADD CONSTRAINT [FK_Styles_SizeId] FOREIGN KEY ([SizeId]) REFERENCES [dbo].[Sizes] ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[ProductImages]'
GO
ALTER TABLE [dbo].[ProductImages] ADD CONSTRAINT [FK_ProductImages_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [dbo].[Images] ([Id])
GO
ALTER TABLE [dbo].[ProductImages] ADD CONSTRAINT [FK_ProductImages_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[Products]'
GO
ALTER TABLE [dbo].[Products] ADD CONSTRAINT [FK_Products_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [dbo].[Images] ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[PageOpenGraphTags]'
GO
ALTER TABLE [dbo].[PageOpenGraphTags] ADD CONSTRAINT [FK_PageOpenGraphTags_OpenGraphTagId] FOREIGN KEY ([OpenGraphTagId]) REFERENCES [dbo].[OpenGraphTags] ([Id])
GO
ALTER TABLE [dbo].[PageOpenGraphTags] ADD CONSTRAINT [FK_PageOpenGraphTags_PageId] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Pages] ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[OrderItems]'
GO
ALTER TABLE [dbo].[OrderItems] ADD CONSTRAINT [FK_OrderItems_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[Reviews]'
GO
ALTER TABLE [dbo].[Reviews] ADD CONSTRAINT [FK_Reviews_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
COMMIT TRANSACTION
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
-- This statement writes to the SQL Server Log so SQL Monitor can show this deployment.
IF HAS_PERMS_BY_NAME(N'sys.xp_logevent', N'OBJECT', N'EXECUTE') = 1
BEGIN
    DECLARE @databaseName AS nvarchar(2048), @eventMessage AS nvarchar(2048)
    SET @databaseName = REPLACE(REPLACE(DB_NAME(), N'\', N'\\'), N'"', N'\"')
    SET @eventMessage = N'Redgate SQL Compare: { "deployment": { "description": "Redgate SQL Compare deployed to ' + @databaseName + N'", "database": "' + @databaseName + N'" }}'
    EXECUTE sys.xp_logevent 55000, @eventMessage
END
GO
DECLARE @Success AS BIT
SET @Success = 1
SET NOEXEC OFF
IF (@Success = 1) PRINT 'The database update succeeded'
ELSE BEGIN
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
	PRINT 'The database update failed'
END
GO
