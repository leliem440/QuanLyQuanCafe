CREATE DATABASE QuanLyQuanCafe
GO

USE QuanLyQuanCafe
Go

CREATE TABLE TableFood
(
   id INT IDENTITY PRIMARY KEY,
   name NVARCHAR(100) NOT NULL DEFAULT N'Bàn chưa đặt tên',
   status NVARCHAR(100) NOT NULL DEFAULT N'Trống' -- Trống || Có người
)
GO 

CREATE TABLE Account
(
  UserName NVARCHAR(100) PRIMARY KEY,
  DisplayName NVARCHAR(100) NOT NULL DEFAULT N'TLT',
  PassWord NVARCHAR(1000) NOT NULL DEFAULT 0,
  Type INT NOT NULL DEFAULT 0 --1: admin && 0: staff
)
GO

CREATE TABLE FoodCategory
(
  id INT IDENTITY PRIMARY KEY,
  name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên'
)
GO 

CREATE TABLE Food
(
  id INT IDENTITY PRIMARY KEY,
  name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
  idCategory INT NOT NULL,
  price FLOAT NOT NULL DEFAULT 0

  FOREIGN KEY (idCategory) REFERENCES dbo.FoodCategory(id)
)
GO

CREATE TABLE Bill
(
  id INT IDENTITY PRIMARY KEY,
  DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
  DateCheckOut DATE ,
  idTable INT NOT NULL,
  status INT NOT NULL DEFAULT 0 --1: đã thanh toán && 0: chưa thanh toán

  FOREIGN KEY (idTable) REFERENCES dbo.TableFood(id) 
)
GO

CREATE TABLE BillInfo
(
  id INT IDENTITY PRIMARY KEY,
  idBill INT NOT NULL,
  idFood INT NOT NULL,
  count INT NOT NULL DEFAULT 0

  FOREIGN KEY (idBill) REFERENCES dbo.Bill(id),
  FOREIGN KEY (idFood) REFERENCES dbo.Food(id)
)

INSERT INTO Account (UserName, DisplayName, PassWord, Type) VALUES (N'K9', N'RongK9', N'1', 1)
INSERT INTO Account (UserName, DisplayName, PassWord, Type) VALUES (N'staff', N'staff', N'1',0)
GO

CREATE PROC USP_GetAccountByUserName
@username NVARCHAR(100)
AS
BEGIN
     SELECT * FROM Account WHERE UserName = @username
END
GO

EXEC USP_GetAccountByUserName @username = N'K9'
GO

CREATE PROC USP_Login
@userName nvarchar(100), @passWord nvarchar(100)
AS
BEGIN
 SELECT * FROM dbo.Account WHERE UserName = @userName AND PassWord = @passWord
 END
 GO

 --Thêm bàn--
DECLARE @i INT = 0

WHILE @i <= 10
BEGIN 
     INSERT dbo.TableFood (name)VALUES (N'Bàn ' + CAST(@i AS nvarchar(100)))
	 SET @i = @i + 1
	 END
GO

CREATE PROC USP_GetTableList
AS SELECT * FROM dbo.TableFood
GO

UPDATE dbo.TableFood SET STATUS = N'Có người' WHERE id = 9

EXEC dbo.USP_GetTableList
GO
---Thêm Category----
INSERT dbo.FoodCategory
		(name)
VALUES	( N'Hải sản'
		)
INSERT dbo.FoodCategory
		(name)
VALUES	( N'Nông sản'  
		)
INSERT dbo.FoodCategory
		(name)
VALUES	( N'Lâm sản'
		)
INSERT dbo.FoodCategory
		(name)
VALUES	( N'Sản sản'
		)
INSERT dbo.FoodCategory
		(name)
VALUES	( N'Nước'
		)

--thêm món ăn
INSERT dbo.Food
		(name, idCategory, price)
VALUES	( N'Mực một nắng nước sa tế', 1, 120000)
INSERT dbo.Food
		(name, idCategory, price)
VALUES	( N'Nghêu hấp xả', 1, 50000) 
INSERT dbo.Food
		(name, idCategory, price)
VALUES	( N'Dú dê nướng sữa', 2, 50000) 
INSERT dbo.Food
		(name, idCategory, price)
VALUES	( N'Heo rừng nướng muối ớt', 3, 75000) 
INSERT dbo.Food
		(name, idCategory, price)
VALUES	( N'Cơm chiên mushi', 4, 999999) 
INSERT dbo.Food
		(name, idCategory, price)
VALUES	( N'7Up', 5, 15000)
INSERT dbo.Food
		(name, idCategory, price)
VALUES	( N'Cafe', 5, 12000) 

--thêm bill
INSERT dbo.Bill
		( DateCheckIn ,
		DateCheckOut,
		idTable,
		status
		)
VALUES	( GETDATE() , 
		  NULL,
		  1 ,
		  0
		)

INSERT dbo.Bill
		( DateCheckIn ,
		DateCheckOut,
		idTable,
		status
		)
VALUES	( GETDATE() , 
		  NULL,
		  2 ,
		  0
		)

INSERT dbo.Bill
		( DateCheckIn ,
		DateCheckOut,
		idTable,
		status
		)
VALUES	( GETDATE() , 
		  GETDATE(),
		  3 ,
		  1
		)


--THÊM BILL INFO---
INSERT dbo.BillInfo
		( idBill, idFood, count)
VALUES	( 1,
		  1,
		  2
		)
INSERT dbo.BillInfo
		( idBill, idFood, count)
VALUES	( 1,
		  3,
		  4
		)
INSERT dbo.BillInfo
		( idBill, idFood, count)
VALUES	( 1,
		  5,
		  1
		)
INSERT dbo.BillInfo
		( idBill, idFood, count)
VALUES	( 2,
		  1,
		  2
		)
INSERT dbo.BillInfo
		( idBill, idFood, count)
VALUES	( 2,
		  6,
		  2
		)
INSERT dbo.BillInfo
		( idBill, idFood, count)
VALUES	( 3,
		  5,
		  2
		)
GO

CREATE PROC USP_InsertBill
@idTable INT
AS
BEGIN
INSERT dbo.Bill
          ( DateCheckIn ,
		DateCheckOut,
		idTable,
		status
		)
VALUES	( GETDATE() , 
		  NULL,
		  @idTable ,
		  0
		)
END
GO


CREATE PROC USP_InsertBillInfo
@idBill INT, @idFood INT, @count INT
AS
BEGIN 
	  INSERT dbo.BillInfo
		( idBill, idFood, count)
VALUES	( @idBill,
		  @idFood,
		  @count
		)
		
END
GO

ALTER PROC USP_InsertBillInfo
@idBill INT, @idFood INT, @count INT
AS
BEGIN 
      DECLARE @isExitsBillInfo INT
	  DECLARE @foodCount INT = 1

	  SELECT @isExitsBillInfo = id, @foodCount = b.count 
	  FROM dbo.BillInfo AS b 
	  WHERE idBill = @idBill AND idFood = @idFood

	  IF (@isExitsBillInfo > 0)
	  BEGIN
	       DECLARE @newCount INT = @foodCount + @count
	       IF(@newCount > 0)
	           UPDATE dbo.BillInfo SET count = @foodCount + @count WHERE idFood =@idFood 
	       ELSE 
	           DELETE dbo.BillInfo WHERE idBill = @idBill AND idFood = @idFood
	  END
	  ELSE
	  BEGIN
	       INSERT dbo.BillInfo
		   ( idBill, idFood, count)
           VALUES	( @idBill,
		            @idFood,
		            @count
		            )
	  END
END
GO



SELECT MAX(id) FROM dbo.Bill

SELECT f.name, bi.count, f.price, f.price*bi.count AS totalPrice FROM dbo.BillInfo AS bi, dbo.Bill AS b, dbo.Food AS f
WHERE bi.idBill = b.id AND bi.idFood = f.id AND b.idTable = 1


SELECT * FROM dbo.Bill
SELECT * FROM dbo.BillInfo
SELECT * FROM dbo.Food
SELECT * FROM dbo.FoodCategory

SELECT * FROM dbo.Food

UPDATE dbo.Bill SET status = 1 WHERE id = 1

CREATE TRIGGER UTG_UpdateBillInfo
ON dbo.BillInfo FOR INSERT , UPDATE
AS
BEGIN
     DECLARE @idBill INT
	 SELECT @idBill = idBill FROM inserted
	 DECLARE @idTable INT
	 SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill AND status = 0
	 UPDATE dbo.TableFood SET status = N'Có người' WHERE id = @idTable
END
GO

CREATE TRIGGER UTG_UpdateBill
ON dbo.Bill FOR UPDATE
AS
BEGIN
     DECLARE @idBill INT
	 SELECT @idBill = id FROM inserted
	 DECLARE @idTable INT
	 SELECT @idTable = idTable FROM dbo.Bill WHERE id = @idBill
	 DECLARE @count int = 0
	 SELECT @count = COUNT(*) FROM dbo.Bill WHERE idTable = @idTable AND status = 0
	 IF(@count = 0)
	   UPDATE dbo.TableFood SET status = N'Trống' WHERE id = @idTable
END
GO

DELETE dbo.BillInfo
DELETE dbo.Bill






