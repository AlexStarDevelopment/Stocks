BEGIN

Declare @sid uniqueidentifier;

select @sid = Id
from tblStock
where Ticker = 'SPY'

	insert into [dbo].[tblTransaction] (Id, TransDate, Buy, Quantity, PricePerSharePaid, StockId)
	Values
		(NEWID(), 2014-06-10, 1, 100, 175, @sid);

END