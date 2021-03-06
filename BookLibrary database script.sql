USE msdb

CREATE DATABASE [BookLibrary]
GO

USE [BookLibrary]
GO
/****** Object:  Table [dbo].[Author]    Script Date: 12/09/2018 12:09:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Author](
	[AuthorId] [int] IDENTITY(1,1) NOT NULL,
	[AuthorName] [varchar](250) NOT NULL,
	[Nacionality] [varchar](150) NOT NULL,
 CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED 
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Book]    Script Date: 12/09/2018 12:09:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Book](
	[BookId] [int] IDENTITY(1,1) NOT NULL,
	[AuthorId] [int] NOT NULL,
	[Title] [varchar](250) NOT NULL,
	[Price] [money] NOT NULL,
	[Editorial] [varchar](150) NOT NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Category]    Script Date: 12/09/2018 12:09:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](250) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CategoryBook]    Script Date: 12/09/2018 12:09:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryBook](
	[CategoryId] [int] NOT NULL,
	[BookId] [int] NOT NULL,
 CONSTRAINT [PK_CategoryBook] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC,
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Author] ON 

GO
INSERT [dbo].[Author] ([AuthorId], [AuthorName], [Nacionality]) VALUES (1, N'Matilde Asensi', N'Spain')
GO
INSERT [dbo].[Author] ([AuthorId], [AuthorName], [Nacionality]) VALUES (2, N'Laura Ann Griemman', N'United States')
GO
INSERT [dbo].[Author] ([AuthorId], [AuthorName], [Nacionality]) VALUES (3, N'Guillermo del Toro', N'Mexico')
GO
SET IDENTITY_INSERT [dbo].[Author] OFF
GO
SET IDENTITY_INSERT [dbo].[Book] ON 

GO
INSERT [dbo].[Book] ([BookId], [AuthorId], [Title], [Price], [Editorial]) VALUES (1, 3, N'The Strain', 10.9400, N'Trolla')
GO
INSERT [dbo].[Book] ([BookId], [AuthorId], [Title], [Price], [Editorial]) VALUES (2, 2, N'Curse the dark', 8.5000, N'Luna')
GO
INSERT [dbo].[Book] ([BookId], [AuthorId], [Title], [Price], [Editorial]) VALUES (3, 1, N'El ultimo caton', 2.3400, N'Random House')
GO
SET IDENTITY_INSERT [dbo].[Book] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (3, N'Comedy')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (2, N'Drama')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (1, N'Fiction')
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
INSERT [dbo].[CategoryBook] ([CategoryId], [BookId]) VALUES (1, 1)
GO
INSERT [dbo].[CategoryBook] ([CategoryId], [BookId]) VALUES (1, 2)
GO
INSERT [dbo].[CategoryBook] ([CategoryId], [BookId]) VALUES (2, 3)
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Author]    Script Date: 12/09/2018 12:09:17 p. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Author] ON [dbo].[Author]
(
	[AuthorName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Book]    Script Date: 12/09/2018 12:09:17 p. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Book] ON [dbo].[Book]
(
	[AuthorId] ASC,
	[Title] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Category]    Script Date: 12/09/2018 12:09:17 p. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Category] ON [dbo].[Category]
(
	[CategoryName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Author] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Author] ([AuthorId])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Author]
GO
ALTER TABLE [dbo].[CategoryBook]  WITH CHECK ADD  CONSTRAINT [FK_CategoryBook_Book1] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([BookId])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[CategoryBook] CHECK CONSTRAINT [FK_CategoryBook_Book1]
GO
ALTER TABLE [dbo].[CategoryBook]  WITH CHECK ADD  CONSTRAINT [FK_CategoryBook_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CategoryBook] CHECK CONSTRAINT [FK_CategoryBook_Category]
GO
