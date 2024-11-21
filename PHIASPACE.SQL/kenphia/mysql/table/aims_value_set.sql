USE [Aims]
GO

/****** Object:  Table [dbo].[aims_value_set]    Script Date: 02/09/2024 02:20:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[aims_value_set](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](20) NULL,
	[TableName] [nvarchar](30) NULL,
	[SpName] [nvarchar](60) NULL,
	[Value] [nvarchar](60) NULL,
	[ValueId] [int] NULL,
	[ValuesetType] [nvarchar](10) NULL,
	[Label] [nvarchar](70) NULL,
	[RecordId] [int] NULL,
	[CountParameter] [nvarchar](50) NULL,
 CONSTRAINT [PK_aims_value_set] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

