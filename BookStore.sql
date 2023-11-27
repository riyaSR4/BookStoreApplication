create database BookStoreApp

Create table UserRegister(
UserId int primary key identity(1,1),
FullName varchar(30),
EmailId varchar(30),
Password varchar(20),
MobileNumber varchar(20)
);

create procedure UserRegistration
(
@FullName varchar(30),
@EmailId varchar(30),
@Password varchar(20),
@MobileNumber varchar(20)
)
As
Begin
Insert into UserRegister(FullName,EmailId,Password,MobileNumber) 
values(@FullName,@EmailId,@Password,@MobileNumber)
End

EXEC sp_rename 'UserRegistration', 'spUserRegistration';

Create procedure GetUser(
@EmailId varchar(30)
)
As Begin 
Select * from UserRegister where EmailId = @EmailId
end

EXEC sp_rename 'GetUser', 'spGetUser';

Create Procedure spResetPassword(
@FullName varchar(30),
@EmailId varchar(30),
@Password varchar(20),
@MobileNumber varchar(20)
)
As
begin
update UserRegister 
set FullName=@FullName,MobileNumber=@MobileNumber,EmailId=@EmailId,Password=@Password 
where EmailId=@EmailId
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

create procedure spAddBook
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
Insert into Book(BookName,BookDescription,BookAuthor,BookImage,BookCount,BookPrice,Rating) 
values(@BookName,@BookDescription,@BookAuthor,@BookImage,@BookCount,@BookPrice,@Rating)
End

Create procedure GetAllBooks
As
Begin
Select * from Book
End

EXEC sp_rename 'GetAllBooks', 'spGetAllBooks';

Create procedure spUpdateBook
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
Update Book set BookName=@BookName, BookDescription=@BookDescription, BookAuthor=@BookAuthor, 
BookImage=@BookImage, BookCount=@BookCount, BookPrice=@BookPrice, Rating=@Rating
where BookId=@BookId
End

Create procedure spDeleteEmployee
(
	@BookId int
)
As
Begin
Delete from Book where BookId=@BookId;
End

EXEC sp_rename 'spDeleteEmployee', 'spDeleteBook';

create procedure spUploadImage
(
	@BookId int,
	@FileLink varchar(max)
)
as begin 
	update Book set BookImage = @FileLink where BookId=@BookId
end

Create table Wishlist(
    WishlistId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    BookId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES UserRegister(UserId),
    FOREIGN KEY (BookId) REFERENCES Book(BookId)
)


Create procedure spAddWishlist(
@UserId int,
@BookId int
)
as 
begin
insert into Wishlist (UserId,BookId)
values (@UserId,@BookId)
end

alter procedure spGetWishList
(
	@UserId int
)
as begin
	select * from 
		Wishlist INNER JOIN
		 Book on Book.BookId = Wishlist.BookId 
		 where Wishlist.UserId = @UserId
end

create Procedure spDeleteWishList
(
	@UserId int,
	@BookId int
)
as begin
	DELETE FROM Wishlist WHERE BookId=@BookId and UserID=@UserId;
end



Create table Cart(
    CartId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    BookId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES UserRegister(UserId),
    FOREIGN KEY (BookId) REFERENCES Book(BookId)
)

Create procedure spAddCart(
@UserId int,
@BookId int
)
as 
begin
insert into Cart (UserId,BookId)
values (@UserId,@BookId)
end

create procedure spGetCart
(
	@UserId int
)
as begin
	select * from 
		Cart INNER JOIN
		 Book on Book.BookId = Cart.BookId 
		 where Cart.UserId = @UserId
end

create Procedure spDeleteCart
(
	@UserId int,
	@BookId int
)
as begin
	DELETE FROM Cart WHERE BookId=@BookId and UserID=@UserId;
end

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





