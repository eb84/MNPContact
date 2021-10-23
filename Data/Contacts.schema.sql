USE [MNPContact]
GO

/****** Object:  Table [dbo].[Contacts]    Script Date: 2021-10-23 9:59:53 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Contacts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[JobTitle] [nvarchar](50) NOT NULL,
	[Company] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[LastContacted] [date] NULL,
	[Comments] [nvarchar](500) NULL
) ON [PRIMARY]
GO

