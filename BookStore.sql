create database BookStoreApp

Create table UserRegister(
UserId int primary key identity(1,1),
FullName varchar(30),
EmailId varchar(30),
Password varchar(20),
MobileNumber varchar(20)
);

alter procedure spUserRegistration
(
@FullName varchar(30),
@EmailId varchar(30),
@Password varchar(20),
@MobileNumber varchar(20)
)
As
Begin
Begin try
Insert into UserRegister(FullName,EmailId,Password,MobileNumber) 
values(@FullName,@EmailId,@Password,@MobileNumber)
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
End

EXEC sp_rename 'UserRegistration', 'spUserRegistration';

Alter procedure spGetUser(
@EmailId varchar(30)
)
As Begin 
Begin try
Select * from UserRegister where EmailId = @EmailId
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
end

EXEC sp_rename 'GetUser', 'spGetUser';

alter Procedure spResetPassword(
@FullName varchar(30),
@EmailId varchar(30),
@Password varchar(20),
@MobileNumber varchar(20)
)
As
begin
Begin try
update UserRegister 
set FullName=@FullName,MobileNumber=@MobileNumber,EmailId=@EmailId,Password=@Password 
where EmailId=@EmailId
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
End

Create table Book(
BookId int primary key identity(1,1),
BookName varchar(50),
BookDescription varchar(50),
BookAuthor varchar(50),
BookImage varchar(50),
BookCount int,
BookPrice int,
Rating int,
);

alter procedure spAddBook
(
@BookName varchar(50),
@BookDescription varchar(50),
@BookAuthor varchar(50),
@BookImage varchar(50),
@BookCount int,
@BookPrice int,
@Rating int
)
As
Begin
Begin try
Insert into Book(BookName,BookDescription,BookAuthor,BookImage,BookCount,BookPrice,Rating) 
values(@BookName,@BookDescription,@BookAuthor,@BookImage,@BookCount,@BookPrice,@Rating)
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
End

alter procedure spGetAllBooks
As
Begin
Begin try
Select * from Book
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
End

EXEC sp_rename 'GetAllBooks', 'spGetAllBooks';

alter procedure spUpdateBook
(
@BookId int,
@BookName varchar(50),
@BookDescription varchar(50),
@BookAuthor varchar(50),
@BookImage varchar(50),
@BookCount int,
@BookPrice int,
@Rating int
)
As 
Begin
Begin try
Update Book set BookName=@BookName, BookDescription=@BookDescription, BookAuthor=@BookAuthor, 
BookImage=@BookImage, BookCount=@BookCount, BookPrice=@BookPrice, Rating=@Rating
where BookId=@BookId
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
End

alter procedure spDeleteBook
(
	@BookId int
)
As
Begin
Begin try
Delete from Book where BookId=@BookId;
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
End

EXEC sp_rename 'spDeleteEmployee', 'spDeleteBook';

alter procedure spUploadImage
(
	@BookId int,
	@FileLink varchar(max)
)
as begin
Begin try 
	update Book set BookImage = @FileLink where BookId=@BookId
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
end

Create table Wishlist(
    WishlistId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    BookId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES UserRegister(UserId),
    FOREIGN KEY (BookId) REFERENCES Book(BookId)
)

alter procedure spAddWishlist(
@UserId int,
@BookId int
)
as 
begin
Begin try
insert into Wishlist (UserId,BookId)
values (@UserId,@BookId)
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
end

alter procedure spGetWishList
(
	@UserId int
)
as begin
Begin try
	select * from 
		Wishlist INNER JOIN
		 Book on Book.BookId = Wishlist.BookId 
		 where Wishlist.UserId = @UserId
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
end

EXEC sp_rename 'spGetWishList', 'spGetAllWishList';

alter Procedure spDeleteWishList
(
	@UserId int,
	@BookId int
)
as begin
Begin try
	DELETE FROM Wishlist WHERE BookId=@BookId and UserID=@UserId;
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
end

Create table Cart(
    CartId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    BookId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES UserRegister(UserId),
    FOREIGN KEY (BookId) REFERENCES Book(BookId),
	Count INT NOT NULL
)

Alter procedure spAddCart(
@UserId int,
@BookId int,
@Count int
)
as 
begin
Begin try
insert into Cart (UserId,BookId,Count)
values (@UserId,@BookId,@Count)
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
end

alter procedure spGetCart
(
	@UserId int
)
as begin
Begin try
	select * from 
		Cart INNER JOIN
		 Book on Book.BookId = Cart.BookId 
		 where Cart.UserId = @UserId
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
end

EXEC sp_rename 'spGetCart', 'spGetAllCart';

alter Procedure spDeleteCart
(
	@UserId int,
	@BookId int
)
as begin
Begin try
	DELETE FROM Cart WHERE BookId=@BookId and UserId=@UserId;
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
end

alter procedure spUpdateCart(
    @UserId int,
	@BookId int,
	@Count int
)
As 
Begin
Begin try
Update Cart set Count = @Count
where BookId=@BookId and UserId = @UserId;
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
End


Create table Type(
TypeId INT PRIMARY KEY IDENTITY(1,1),
TypeName varchar(50)
)

Create table CustomerDetails(
CustomerId INT PRIMARY KEY IDENTITY(1,1),
UserId INT NOT NULL,
TypeId INT NOT NULL,
FullName varchar(50),
MobileNumber varchar(15),
Address varchar(max),
CityOrTown varchar(max),
State varchar(50),
FOREIGN KEY (UserId) REFERENCES UserRegister(UserId),
FOREIGN KEY (TypeId) REFERENCES Type(TypeId)
)
drop table CustomerDetails
select * from CustomerDetails;
Select * from type;
Select * from book

Alter procedure spAddAddressDetails(
@UserId int,
@TypeId int,
@FullName varchar(50),
@MobileNumber varchar(15),
@Address varchar(max),
@CityOrTown varchar(max),
@State varchar(50)
)
as 
begin
Begin try
insert into CustomerDetails (UserId,TypeId,FullName,MobileNumber,Address,CityOrTown,State)
values (@UserId,@TypeId,@FullName,@MobileNumber,@Address,@CityOrTown,@State)
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
end

 
create procedure spGetAllAddress
(
	@UserId int
)
as begin
Begin try
	select * from 
		CustomerDetails INNER JOIN
		 Type on Type.TypeId = CustomerDetails.TypeId 
		 where CustomerDetails.UserId = @UserId
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
end

EXEC sp_rename 'spGetAllCustomerDetails', 'spGetAllAddress';

create Procedure spDeleteAddress
(
	@CustomerId int,
	@UserId int
)
as begin
Begin try
	DELETE FROM CustomerDetails WHERE CustomerId=@CustomerId and UserId=@UserId;
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
end

create procedure spUpdateAddress(
@CustomerId int,
@UserId int,
@TypeId int,
@FullName varchar(50),
@MobileNumber varchar(15),
@Address varchar(max),
@CityOrTown varchar(max),
@State varchar(50)
)
As 
Begin
Begin try
Update CustomerDetails set UserId = @UserId, TypeId = @TypeId, FullName = @FullName,
MobileNumber = @MobileNumber, Address = @Address, CityOrTown = @CityOrTown, State = @State  
where CustomerId=@CustomerId and UserId=@UserId;
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
End




Create table CustomerFeedback(
    FeedbackId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    BookId INT NOT NULL,
	CustomerDescription varchar(max),
	Rating INT,
    FOREIGN KEY (UserId) REFERENCES UserRegister(UserId),
    FOREIGN KEY (BookId) REFERENCES Book(BookId)
)

alter procedure spAddFeedback(
@UserId int,
@BookId int,
@CustomerDescription varchar(max),
@Rating int
)
as 
begin
Begin try
insert into CustomerFeedback (UserId,BookId,CustomerDescription,Rating)
values (@UserId,@BookId,@CustomerDescription,@Rating)
End try
Begin catch
Print 'An Error occured: ' + ERROR_MESSAGE();
End Catch
end;

Select * from CustomerFeedback

