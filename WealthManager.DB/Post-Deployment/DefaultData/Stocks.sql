BEGIN

Declare @uid uniqueidentifier;

select @uid = Id
from tblUser
where FirstName = 'Alex';


	insert into [dbo].[tblStock] (Id, UserId, Ticker, CurrentPricePerShare)
	Values
		(NEWID(), @uid, 'SPY', 100.00)
END