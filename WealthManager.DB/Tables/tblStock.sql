CREATE TABLE [dbo].[tblStock]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [Ticker] NCHAR(5) NOT NULL, 
    [CurrentPricePerShare] DECIMAL(10, 2) NOT NULL
)
