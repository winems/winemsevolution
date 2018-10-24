SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[IntegrationMappings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [nvarchar](30) NOT NULL,
	[Guid] [uniqueidentifier] NOT NULL,
	[IntegrationDocumentNumber] [nvarchar](20) NOT NULL,
	[IntegrationDocumentType] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_IntegrationMappings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


