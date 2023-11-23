create database BookStoreApp

Create table UserRegister(
UserId int primary key identity(1,1),
FullName varchar(30),
EmailId varchar(30),
Password varchar(20),
MobileNumber varchar(20)
);

Create procedure UserRegistration
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

Create procedure GetUser(
@EmailId varchar(30)
)
As Begin 
Select * from UserRegister where EmailId = @EmailId
end