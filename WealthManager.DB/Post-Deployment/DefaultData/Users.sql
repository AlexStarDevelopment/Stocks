BEGIN
	insert into [dbo].[tblUser] (Id, FirstName,LastName,Email,Password)
	Values
		(NEWID(), 'Alex', 'Star', 'admin', 'admin');

END