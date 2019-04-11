CREATE TABLE [dbo].[tblTransaction]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [TransDate] DATETIME NULL, 
    [Buy] BIT NULL, 
    [Quantity] INT NULL, 
    [PricePerSharePaid] DECIMAL(18, 2) NULL, 
    [StockId] UNIQUEIDENTIFIER NULL, 

)
