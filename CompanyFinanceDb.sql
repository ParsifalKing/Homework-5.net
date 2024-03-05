create table Customers
(
    Customer_Id serial primary key,
	FullName varchar(200),
	Balance numeric default 0
);

create table Transactions
(
    Transaction_Id serial primary key,
	Sender_Id int references Customers(Customer_Id),
	Recipient_Id int references Customers(Customer_Id),
	Sum numeric,
	DateOfTransaction date
);

insert into Customers(FullName,Balance) values('Akmal Nazriev',430);


select * from Customers
select * from Transactions

