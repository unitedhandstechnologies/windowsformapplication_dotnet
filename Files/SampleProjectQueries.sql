--Students table creation

create table students(
Id int not null,
FirstName varchar(50),
LastName varchar(50),
DOB date,
Gender varchar(15),
Street varchar(50),
City varchar(50),
State varchar(50),
Country varchar(50)
Primary key(Id));

--Students table sample entry

insert into students values(1,'Sindhu','Mohanraj', '1990-05-19','Female','Vaiyapuri Nagar','Karur','TamilNadu','India');
insert into students values(2,'Divya','Rajesh', '1990-06-27','Female','Vaiyapuri Nagar','Karur','TamilNadu','India');
insert into students values(5,'Madhan','R', '1990-06-27','Male','Vaiyapuri Nagar','Karur','TamilNadu','India');

--Country table creation

create table Country(
Id int not null,
Name varchar(50)
primary key (Id));

--Country table sample creation

insert into Country values(1,'India');
insert into Country values(2,'Pakistan');
insert into Country values(3,'China');
insert into Country values(4,'Russia');
insert into Country values(5,'America');
insert into Country values(6,'England');
insert into Country values(7,'Australia');

--State table creation

create table State(
StateId int not null,
Name varchar(50),
CountryId int,
primary key(StateId),
FOREIGN KEY(CountryId) REFERENCES Country(Id));

--State table sample entry

insert into State values(1,'TamilNadu',1);
insert into State values(2,'Uttar Pradesh',1);
insert into State values(3,'Gujarat',1);
insert into State values(4,'Maharashtra',1);
insert into State values(5,'Kerala',1);
insert into State values(6,'Andhra Pradesh',1);
insert into State values(7,'Balochistan',2);

--District table creation

create table district(
Id int not null,
Primary key(Id),
Name varchar(50),
StateId int,
foreign key (StateId) references State(StateId));

--District table sample creation

insert into district values(1,'Karur',1);
insert into district values(2,'Namakkal',1);
insert into district values(3,'Salem',1);
insert into district values(4,'Trichy',1);
insert into district values(5,'Chennai',1);
insert into district values(6,'Sivakasi',1);

insert into district values(7,'Agra',2);
insert into district values(8,'Mathura',2);
insert into district values(9,'Aligarh',2);
insert into district values(10,'Allahabad',2);
insert into district values(11,'Ballia',2);
insert into district values(12,'Basti',2);
insert into district values(13,'Banda',2);


insert into district values(14,'Ahmedabad',3);
insert into district values(15,'Anand',3);
insert into district values(16,'Aravalli',3);
insert into district values(17,'GandhiNagar',3);
insert into district values(18,'JamNagar',3);


insert into district values(19,'Mumbai',4);
insert into district values(20,'Pune',4);
insert into district values(21,'Sangli',4);
insert into district values(22,'Satara',4);
insert into district values(23,'Nashik',4);
insert into district values(24,'Nagpur',4);


insert into district values(25,'Vayanad',5);
insert into district values(26,'Palakkad',5);
insert into district values(27,'Malabar',5);
insert into district values(28,'Cochin',5);
insert into district values(29,'Travancore',5);
insert into district values(30,'Alappuzha',5);


insert into district values(31,'Kadappah',6);
insert into district values(32,'Godavari',6);
insert into district values(33,'Vizag',6);
insert into district values(34,'Nellore',6);
insert into district values(35,'Guntur',6);


insert into district values(36,'Awaran',7);
insert into district values(37,'Barkhan',7);

--Procedure 1. 'getStates'

create procedure getStates @countryId int
as
select * from State
where CountryId =@countryId
go

--Procedure 2. 'displayCountry'

create procedure displayCountry
as
select * from Country
go

--Procedure 3. 'displaystudents'

create procedure displaystudents
as
select * from students
go

--Procedure 4. 'getDistricts'

create procedure getDistricts @stateId int
as
select * from district
where StateId =@stateId
go

--Procedure 5. 'addStudent'

create procedure addStudent @fName nvarchar(50),@lastName nvarchar(50),@lName date,@Gender nvarchar(50),@street nvarchar(50),@City nvarchar(50),@State nvarchar(50),@Country nvarchar(50)

as
declare @count int;
set @count = (select count(*) from students)
set @count = @count+1

insert into students values(@count,@fName,@lName,@lName,@Gender,@street,@City,@State,@Country);
go

--Procedure 6. 'modifyStudent'

create procedure modifyStudent @Id int, @fName nvarchar(50),@lastName nvarchar(50),@date date,@Gender nvarchar(50),@street nvarchar(50),@City nvarchar(50),@State nvarchar(50),@Country nvarchar(50)
as
update Students set FirstName = @fName,LastName = @lastName,DOB = @date,Gender = @Gender,Street = @street, City = @city,State = @State,Country = @Country where Id = @Id
go

--Procedure 7. 'FillForm'

create procedure FillForm @Id int
as
select * from Students where Id = @Id
go

--Procedure 8. 'displayStudentwithId'

create procedure displayStudentwithId @StudentId
as
select * from Students where Id = @StudentId
go

--Procedure 9. 'deleteStudent'

create procedure deleteStudent @StudentId int
as
delete from students where Id = @StudentId
go
