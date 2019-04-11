CREATE TABLE [dbo].[tblCryptoTran]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [TransDate] DATE NOT NULL, 
    [Buy] BIT NOT NULL, 
    [Quantity] DECIMAL(18, 8) NOT NULL, 
    [Price] DECIMAL(18, 2) NOT NULL, 
    [CryptoId] UNIQUEIDENTIFIER NOT NULL
)
