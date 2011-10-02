/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.DeploymentOutput
	(
	DeploymentOutputId uniqueidentifier NOT NULL,
	Output text NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.DeploymentOutput ADD CONSTRAINT
	PK_DeploymentOutput PRIMARY KEY CLUSTERED 
	(
	DeploymentOutputId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Deployment ADD DeploymentOutputId uniqueidentifier NULL
GO
ALTER TABLE dbo.Deployment ADD CONSTRAINT	FK_Deployment_DeploymentOutput FOREIGN KEY (DeploymentOutputId) REFERENCES dbo.DeploymentOutput
	(
	DeploymentOutputId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO

/* Copy the data here */
insert into DeploymentOutput (DeploymentOutputId, [Output])
  select DeploymentId, [Output] from Deployment

update Deployment set DeploymentOutputId = DeploymentId

ALTER TABLE dbo.Deployment
	DROP COLUMN Output
GO
COMMIT
