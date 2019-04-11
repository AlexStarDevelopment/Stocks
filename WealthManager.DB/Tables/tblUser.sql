CREATE TABLE [dbo].[tblUser]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[FirstName] NCHAR(50) NULL, 
    [LastName] NCHAR(50) NULL, 
    [Email] NCHAR(50) NULL, 
    [Password] NCHAR(128) NULL
)
