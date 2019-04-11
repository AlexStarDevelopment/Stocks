CREATE PROCEDURE [dbo].[spGetTransactions]
AS
	SELECT u.FirstName, s.Ticker, t.Buy, t.Quantity, t.PricePerSharePaid, t.TransDate from tblUser u
	join tblStock s on u.Id = s.UserId
	join tblTransaction t on t.StockId = s.Id
	order by t.TransDate
RETURN 0
