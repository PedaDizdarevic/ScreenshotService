Create Table Screenshot (
	Id int IDENTITY(1,1) PRIMARY KEY,
	[FileName] varchar(100) NOT NULL,
	[Url] varchar(100) NOT NULL,
	Cerated Date NOT NULL Default GetDate(),
	[Image] Image NOT NULL
)