USE [master]
GO

--Creates TCC database
CREATE DATABASE [TCC]


USE [TCC]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HealthCheckerInfoCapture](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PercentageTime] [decimal](19, 5) NULL,
	[CPUUsage] [real] NULL,
	[MemoryUsage] [real] NULL,
	[Time] [bigint] NULL,
	[Type] [nvarchar](255) NULL,
	[WorkerId] [int] NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO




SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HeartBeatInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Beat] [bit] NULL,
	[WorkerId] [int] NULL,
	[CreatedAt] [datetime] NULL,
 CONSTRAINT [PK__HeartBea__3214EC07B070798D] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



/****** Object:  Table [dbo].[laboratorio]    Script Date: 09/09/2016 19:38:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[laboratorio](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[razao_social] [varchar](255) NOT NULL,
	[nome_fantasia] [varchar](255) NOT NULL,
	[telefone] [varchar](12) NOT NULL,
	[enderecoId] [int] NOT NULL,
 CONSTRAINT [PK_laboratorio] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



--INCLUDE 50.000 Lab
DECLARE @qtLab INT
DECLARE @id INT
DECLARE @fone VARCHAR(10)
SET  @qtLab = 10000

WHILE @qtLab <= 50000 BEGIN 
SET @fone = (1000000000 + CRYPT_GEN_RANDOM(1) % 999999999)
INSERT INTO [dbo].[laboratorio] (razao_social, nome_fantasia, telefone) VALUES ( CONCAT('LAB RAZAO SOCIAL ',@qtLab), CONCAT('LAB NOME FANSTASIA ',@qtLab),@fone )
SET @qtLab = @qtLab + 1
END