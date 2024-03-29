USE [InventarisationDB]
GO
/****** Object:  UserDefinedFunction [dbo].[inv_num]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[inv_num](@i int) 
returns char(6) 
as 
begin 
  return (char(@i / 26000 % 26 + 65) + 
    char(@i / 10000 % 10 + 48) +
	char(@i / 1000 % 10 + 48) + 
    char(@i / 100 % 10 + 48) + 
    char(@i / 10 % 10 + 48) + 
    char(@i % 10 + 48)) 
  end
GO
/****** Object:  Table [dbo].[ActiveTasks]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActiveTasks](
	[id_task] [int] IDENTITY(1,1) NOT NULL,
	[parent_id] [int] NULL,
	[inventory_id] [int] NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_ActiveTasks] PRIMARY KEY CLUSTERED 
(
	[id_task] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[OrganizId] [int] NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Companies]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companies](
	[id_company] [int] IDENTITY(1,1) NOT NULL,
	[company_name] [varchar](100) NULL,
 CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED 
(
	[id_company] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employer]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employer](
	[id_empolyer] [int] IDENTITY(1,1) NOT NULL,
	[full_name] [varchar](150) NOT NULL,
 CONSTRAINT [PK_Employer] PRIMARY KEY CLUSTERED 
(
	[id_empolyer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventory]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[inv_num]  AS ([dbo].[inv_num]([id])),
	[move_id] [int] NULL,
	[company_id] [int] NULL,
	[payment_num] [varchar](200) NULL,
	[comment] [varchar](200) NULL,
	[invoice] [varchar](200) NULL,
	[dateInvCreate] [datetime] NULL,
	[subject_id] [int] NULL,
	[serial_number] [varchar](200) NULL,
 CONSTRAINT [PK__Inventor__3213E83F28C2101C] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movements]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movements](
	[id_movement] [int] IDENTITY(1,1) NOT NULL,
	[id_inventory] [int] NOT NULL,
	[date_move] [date] NULL,
	[placement_id] [int] NULL,
	[planner] [varchar](150) NULL,
	[employer_id] [int] NULL,
 CONSTRAINT [PK_Movements] PRIMARY KEY CLUSTERED 
(
	[id_movement] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nomenclature]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nomenclature](
	[id_nomenclature] [int] IDENTITY(1,1) NOT NULL,
	[name_device] [varchar](100) NULL,
	[date_creation] [date] NULL,
	[date_change] [date] NULL,
 CONSTRAINT [PK_Nomenclature] PRIMARY KEY CLUSTERED 
(
	[id_nomenclature] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhoneSessions]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhoneSessions](
	[id_session] [int] NOT NULL,
	[num_session] [varchar](50) NULL,
	[date_creation_session] [date] NULL,
	[date_expire_session] [date] NULL,
	[username_session] [varchar](50) NULL,
 CONSTRAINT [PK_PhoneSessions] PRIMARY KEY CLUSTERED 
(
	[id_session] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Placements]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Placements](
	[id_placement] [int] IDENTITY(1,1) NOT NULL,
	[name_placement] [varchar](200) NULL,
 CONSTRAINT [PK_Placements] PRIMARY KEY CLUSTERED 
(
	[id_placement] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QueueLists]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QueueLists](
	[id_list] [int] IDENTITY(1,1) NOT NULL,
	[id_parent] [int] NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_QueueLists] PRIMARY KEY CLUSTERED 
(
	[id_list] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Register]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Register](
	[Username] [varchar](50) NOT NULL,
	[Email] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
 CONSTRAINT [PK_Register] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RevisionItems]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RevisionItems](
	[id_queue] [int] IDENTITY(1,1) NOT NULL,
	[list_id] [int] NULL,
	[inventory_id] [int] NULL,
	[date_scan] [datetime] NULL,
	[is_done] [bit] NULL,
 CONSTRAINT [PK_RevisionItems] PRIMARY KEY CLUSTERED 
(
	[id_queue] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subjects]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subjects](
	[id_subject] [int] IDENTITY(1,1) NOT NULL,
	[name_subject] [varchar](200) NULL,
	[nomen_id] [int] NULL,
 CONSTRAINT [PK_Subjects] PRIMARY KEY CLUSTERED 
(
	[id_subject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UnitsMeasurement]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnitsMeasurement](
	[id_measure] [int] IDENTITY(1,1) NOT NULL,
	[name_unit] [varchar](150) NULL,
 CONSTRAINT [PK_UnitsMeasurement] PRIMARY KEY CLUSTERED 
(
	[id_measure] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id_user] [int] IDENTITY(1,1) NOT NULL,
	[last_name] [varchar](150) NOT NULL,
	[first_name] [varchar](150) NOT NULL,
	[patronymic] [varchar](150) NULL,
	[login] [varchar](150) NOT NULL,
	[password] [varchar](150) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id_user] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Workplace]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Workplace](
	[id_workplace] [int] IDENTITY(1,1) NOT NULL,
	[id_inventory] [int] NULL,
	[placement_id_wp] [int] NULL,
	[employer_id] [int] NULL,
 CONSTRAINT [PK_Workplace] PRIMARY KEY CLUSTERED 
(
	[id_workplace] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WriteOff]    Script Date: 31.03.2023 8:18:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WriteOff](
	[id_writeoff] [int] IDENTITY(1,1) NOT NULL,
	[id_inventory] [int] NOT NULL,
	[count_writeoff] [int] NULL,
	[reason] [varchar](150) NULL,
 CONSTRAINT [PK_WriteOff] PRIMARY KEY CLUSTERED 
(
	[id_writeoff] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'08344182-6f35-441e-b527-c82e8e962ec0', N'User', N'USER', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'beca238e-75cc-427a-85c2-6a785e8fed8f', N'Admin', N'ADMIN', NULL)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'15bd2586-a9aa-4d30-b26f-2aac6988cf4c', N'08344182-6f35-441e-b527-c82e8e962ec0')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'15bd2586-a9aa-4d30-b26f-2aac6988cf4c', N'beca238e-75cc-427a-85c2-6a785e8fed8f')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [OrganizId]) VALUES (N'15bd2586-a9aa-4d30-b26f-2aac6988cf4c', N'adminApi', N'ADMINAPI', N'sterhov@doker.ru', N'STERHOV@DOKER.RU', 0, N'AQAAAAIAAYagAAAAEFQwCLN2aDTtPAm5xh5kr9uEu0zxid+q31eS25tbKr9jYb2KtxQx6mmgsOBWZF3V7A==', N'KK4PDHJYG6FYIPSRZD3NNDVMKVYGM5MI', N'6688ca9c-a8e5-4988-a736-db1be9b1bc89', NULL, 0, 0, NULL, 1, 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[Companies] ON 

INSERT [dbo].[Companies] ([id_company], [company_name]) VALUES (1, N'C.I.T')
INSERT [dbo].[Companies] ([id_company], [company_name]) VALUES (2, N'Docker')
INSERT [dbo].[Companies] ([id_company], [company_name]) VALUES (3, N'Oboronka')
INSERT [dbo].[Companies] ([id_company], [company_name]) VALUES (11, N'ООО "Управление недвижимостью"')
SET IDENTITY_INSERT [dbo].[Companies] OFF
GO
SET IDENTITY_INSERT [dbo].[Employer] ON 

INSERT [dbo].[Employer] ([id_empolyer], [full_name]) VALUES (1, N'Нет')
INSERT [dbo].[Employer] ([id_empolyer], [full_name]) VALUES (2, N'Пупа')
INSERT [dbo].[Employer] ([id_empolyer], [full_name]) VALUES (3, N'Лупа')
INSERT [dbo].[Employer] ([id_empolyer], [full_name]) VALUES (4, N'Пулатов')
SET IDENTITY_INSERT [dbo].[Employer] OFF
GO
SET IDENTITY_INSERT [dbo].[Inventory] ON 

INSERT [dbo].[Inventory] ([id], [move_id], [company_id], [payment_num], [comment], [invoice], [dateInvCreate], [subject_id], [serial_number]) VALUES (111147, 37, 11, N'Б-57416413449', N'Резерв', N'B849148945', CAST(N'2023-03-28T09:53:01.683' AS DateTime), 11, N'U1QP306802964')
INSERT [dbo].[Inventory] ([id], [move_id], [company_id], [payment_num], [comment], [invoice], [dateInvCreate], [subject_id], [serial_number]) VALUES (111148, 38, 11, N'Б-825654968', N'Резерв', N'6546515684', CAST(N'2023-03-28T10:32:45.223' AS DateTime), 6, N'0')
SET IDENTITY_INSERT [dbo].[Inventory] OFF
GO
INSERT [dbo].[Login] ([Username], [Password]) VALUES (N'admin', N'1234')
GO
SET IDENTITY_INSERT [dbo].[Movements] ON 

INSERT [dbo].[Movements] ([id_movement], [id_inventory], [date_move], [placement_id], [planner], [employer_id]) VALUES (37, 111147, CAST(N'2023-03-28' AS Date), 1, NULL, 1)
INSERT [dbo].[Movements] ([id_movement], [id_inventory], [date_move], [placement_id], [planner], [employer_id]) VALUES (38, 111148, CAST(N'2023-03-28' AS Date), 1, NULL, 1)
INSERT [dbo].[Movements] ([id_movement], [id_inventory], [date_move], [placement_id], [planner], [employer_id]) VALUES (57, 111148, CAST(N'2023-03-29' AS Date), 1005, NULL, 3)
SET IDENTITY_INSERT [dbo].[Movements] OFF
GO
SET IDENTITY_INSERT [dbo].[Nomenclature] ON 

INSERT [dbo].[Nomenclature] ([id_nomenclature], [name_device], [date_creation], [date_change]) VALUES (2, N'Программное обеспечение', CAST(N'2022-05-23' AS Date), CAST(N'2023-06-04' AS Date))
INSERT [dbo].[Nomenclature] ([id_nomenclature], [name_device], [date_creation], [date_change]) VALUES (4, N'Серверное оборудование', CAST(N'2022-05-25' AS Date), CAST(N'2023-06-06' AS Date))
INSERT [dbo].[Nomenclature] ([id_nomenclature], [name_device], [date_creation], [date_change]) VALUES (5, N'Периферийные устройства', CAST(N'2022-05-26' AS Date), CAST(N'2023-06-07' AS Date))
INSERT [dbo].[Nomenclature] ([id_nomenclature], [name_device], [date_creation], [date_change]) VALUES (6, N'Мониторы и дисплеи', CAST(N'2022-05-27' AS Date), CAST(N'2023-06-08' AS Date))
INSERT [dbo].[Nomenclature] ([id_nomenclature], [name_device], [date_creation], [date_change]) VALUES (7, N'Компоненты: процессоры', CAST(N'2022-05-28' AS Date), CAST(N'2023-06-09' AS Date))
INSERT [dbo].[Nomenclature] ([id_nomenclature], [name_device], [date_creation], [date_change]) VALUES (8, N'Расходники', CAST(N'2022-05-29' AS Date), CAST(N'2023-06-10' AS Date))
INSERT [dbo].[Nomenclature] ([id_nomenclature], [name_device], [date_creation], [date_change]) VALUES (9, N'Картридж Kyocera', CAST(N'2022-05-30' AS Date), CAST(N'2023-06-11' AS Date))
INSERT [dbo].[Nomenclature] ([id_nomenclature], [name_device], [date_creation], [date_change]) VALUES (11, N'Камера', CAST(N'2022-06-01' AS Date), CAST(N'2023-06-13' AS Date))
SET IDENTITY_INSERT [dbo].[Nomenclature] OFF
GO
SET IDENTITY_INSERT [dbo].[Placements] ON 

INSERT [dbo].[Placements] ([id_placement], [name_placement]) VALUES (1, N'Склад')
INSERT [dbo].[Placements] ([id_placement], [name_placement]) VALUES (2, N'АБК, офис 401')
INSERT [dbo].[Placements] ([id_placement], [name_placement]) VALUES (3, N'АБК, 3 этаж, приемная')
INSERT [dbo].[Placements] ([id_placement], [name_placement]) VALUES (4, N'АБК, 4 этаж, коридор')
INSERT [dbo].[Placements] ([id_placement], [name_placement]) VALUES (5, N'СВХ, операторы')
INSERT [dbo].[Placements] ([id_placement], [name_placement]) VALUES (6, N'КТ, диспетчерская')
INSERT [dbo].[Placements] ([id_placement], [name_placement]) VALUES (1005, N'АБК, 401, 5')
SET IDENTITY_INSERT [dbo].[Placements] OFF
GO
SET IDENTITY_INSERT [dbo].[QueueLists] ON 

INSERT [dbo].[QueueLists] ([id_list], [id_parent], [is_active]) VALUES (1, 1, 1)
INSERT [dbo].[QueueLists] ([id_list], [id_parent], [is_active]) VALUES (2, 2, 0)
INSERT [dbo].[QueueLists] ([id_list], [id_parent], [is_active]) VALUES (3, 0, 0)
INSERT [dbo].[QueueLists] ([id_list], [id_parent], [is_active]) VALUES (4, 3, 1)
INSERT [dbo].[QueueLists] ([id_list], [id_parent], [is_active]) VALUES (5, 3, 1)
INSERT [dbo].[QueueLists] ([id_list], [id_parent], [is_active]) VALUES (6, 4, 1)
INSERT [dbo].[QueueLists] ([id_list], [id_parent], [is_active]) VALUES (7, 5, 1)
INSERT [dbo].[QueueLists] ([id_list], [id_parent], [is_active]) VALUES (8, 6, 1)
INSERT [dbo].[QueueLists] ([id_list], [id_parent], [is_active]) VALUES (9, 7, 1)
INSERT [dbo].[QueueLists] ([id_list], [id_parent], [is_active]) VALUES (10, 8, 1)
INSERT [dbo].[QueueLists] ([id_list], [id_parent], [is_active]) VALUES (11, 9, 1)
INSERT [dbo].[QueueLists] ([id_list], [id_parent], [is_active]) VALUES (12, 10, 1)
INSERT [dbo].[QueueLists] ([id_list], [id_parent], [is_active]) VALUES (13, 11, 1)
INSERT [dbo].[QueueLists] ([id_list], [id_parent], [is_active]) VALUES (14, 12, 1)
INSERT [dbo].[QueueLists] ([id_list], [id_parent], [is_active]) VALUES (15, 13, 1)
INSERT [dbo].[QueueLists] ([id_list], [id_parent], [is_active]) VALUES (16, 14, 1)
INSERT [dbo].[QueueLists] ([id_list], [id_parent], [is_active]) VALUES (17, 15, 1)
INSERT [dbo].[QueueLists] ([id_list], [id_parent], [is_active]) VALUES (18, 16, 1)
INSERT [dbo].[QueueLists] ([id_list], [id_parent], [is_active]) VALUES (19, 17, 1)
INSERT [dbo].[QueueLists] ([id_list], [id_parent], [is_active]) VALUES (20, 18, 1)
INSERT [dbo].[QueueLists] ([id_list], [id_parent], [is_active]) VALUES (21, 19, 1)
SET IDENTITY_INSERT [dbo].[QueueLists] OFF
GO
SET IDENTITY_INSERT [dbo].[Subjects] ON 

INSERT [dbo].[Subjects] ([id_subject], [name_subject], [nomen_id]) VALUES (1, N'WD Green 240GB', 2)
INSERT [dbo].[Subjects] ([id_subject], [name_subject], [nomen_id]) VALUES (2, N'Seagate 1TB', 2)
INSERT [dbo].[Subjects] ([id_subject], [name_subject], [nomen_id]) VALUES (3, N'AOC MV27100', 2)
INSERT [dbo].[Subjects] ([id_subject], [name_subject], [nomen_id]) VALUES (5, N'Windows 12', 2)
INSERT [dbo].[Subjects] ([id_subject], [name_subject], [nomen_id]) VALUES (6, N'Razer Viper mini', 5)
INSERT [dbo].[Subjects] ([id_subject], [name_subject], [nomen_id]) VALUES (11, N'Intel Core i5-11400', 7)
SET IDENTITY_INSERT [dbo].[Subjects] OFF
GO
SET IDENTITY_INSERT [dbo].[UnitsMeasurement] ON 

INSERT [dbo].[UnitsMeasurement] ([id_measure], [name_unit]) VALUES (1, N'мм')
INSERT [dbo].[UnitsMeasurement] ([id_measure], [name_unit]) VALUES (2, N'см')
INSERT [dbo].[UnitsMeasurement] ([id_measure], [name_unit]) VALUES (3, N'дм')
INSERT [dbo].[UnitsMeasurement] ([id_measure], [name_unit]) VALUES (4, N'м')
SET IDENTITY_INSERT [dbo].[UnitsMeasurement] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([id_user], [last_name], [first_name], [patronymic], [login], [password]) VALUES (1, N'Вася', N'Пупкин', N'Монтажникович', N'admin', N'1234')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Workplace] ON 

INSERT [dbo].[Workplace] ([id_workplace], [id_inventory], [placement_id_wp], [employer_id]) VALUES (1, 1, 1, 1)
INSERT [dbo].[Workplace] ([id_workplace], [id_inventory], [placement_id_wp], [employer_id]) VALUES (2, 2, 2, 2)
INSERT [dbo].[Workplace] ([id_workplace], [id_inventory], [placement_id_wp], [employer_id]) VALUES (3, 3, 1, 2)
INSERT [dbo].[Workplace] ([id_workplace], [id_inventory], [placement_id_wp], [employer_id]) VALUES (4, 1, 4, 3)
SET IDENTITY_INSERT [dbo].[Workplace] OFF
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_Companies] FOREIGN KEY([company_id])
REFERENCES [dbo].[Companies] ([id_company])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Inventory_Companies]
GO
ALTER TABLE [dbo].[Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_Subjects] FOREIGN KEY([subject_id])
REFERENCES [dbo].[Subjects] ([id_subject])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Inventory_Subjects]
GO
ALTER TABLE [dbo].[Movements]  WITH CHECK ADD  CONSTRAINT [FK_Movements_Employer] FOREIGN KEY([employer_id])
REFERENCES [dbo].[Employer] ([id_empolyer])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Movements] CHECK CONSTRAINT [FK_Movements_Employer]
GO
ALTER TABLE [dbo].[Movements]  WITH CHECK ADD  CONSTRAINT [FK_Movements_Inventory] FOREIGN KEY([id_inventory])
REFERENCES [dbo].[Inventory] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Movements] CHECK CONSTRAINT [FK_Movements_Inventory]
GO
ALTER TABLE [dbo].[Movements]  WITH CHECK ADD  CONSTRAINT [FK_Movements_Placements1] FOREIGN KEY([placement_id])
REFERENCES [dbo].[Placements] ([id_placement])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Movements] CHECK CONSTRAINT [FK_Movements_Placements1]
GO
ALTER TABLE [dbo].[RevisionItems]  WITH CHECK ADD  CONSTRAINT [FK_RevisionItems_Inventory] FOREIGN KEY([inventory_id])
REFERENCES [dbo].[Inventory] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RevisionItems] CHECK CONSTRAINT [FK_RevisionItems_Inventory]
GO
ALTER TABLE [dbo].[RevisionItems]  WITH CHECK ADD  CONSTRAINT [FK_RevisionItems_QueueLists] FOREIGN KEY([list_id])
REFERENCES [dbo].[QueueLists] ([id_list])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RevisionItems] CHECK CONSTRAINT [FK_RevisionItems_QueueLists]
GO
ALTER TABLE [dbo].[Subjects]  WITH CHECK ADD  CONSTRAINT [FK_Subjects_Nomenclature] FOREIGN KEY([nomen_id])
REFERENCES [dbo].[Nomenclature] ([id_nomenclature])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Subjects] CHECK CONSTRAINT [FK_Subjects_Nomenclature]
GO
ALTER TABLE [dbo].[WriteOff]  WITH CHECK ADD  CONSTRAINT [FK_WriteOff_Inventory] FOREIGN KEY([id_inventory])
REFERENCES [dbo].[Inventory] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WriteOff] CHECK CONSTRAINT [FK_WriteOff_Inventory]
GO
