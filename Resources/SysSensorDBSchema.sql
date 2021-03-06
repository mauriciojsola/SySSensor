/****** Object:  Table [dbo].[LogFile]    Script Date: 10/1/2017 3:42:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'LogFile'))
BEGIN
    CREATE TABLE [dbo].[LogFile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](50) NOT NULL,
	[ProcessDate] [datetime] NULL,
	[FileContent] [nvarchar](max) NULL,
 CONSTRAINT [PK_LogFile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END


IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'SensorLog'))
BEGIN
    CREATE TABLE [dbo].[SensorLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SensorId] [nvarchar](20) NULL,
	[ReadDate] [datetime] NOT NULL,
	[Temperature] [float] NOT NULL,
	[Humidity] [float] NOT NULL,
 CONSTRAINT [PK_dbo.SensorLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

END
