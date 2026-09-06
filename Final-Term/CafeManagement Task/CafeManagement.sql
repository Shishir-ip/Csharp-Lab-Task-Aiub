IF DB_ID('CafeManagement') IS NULL
BEGIN
    CREATE DATABASE [CafeManagement];
END
GO

USE [CafeManagement];
GO

-- Customers table
CREATE TABLE IF NOT EXISTS Customers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    Phone NVARCHAR(50) NULL,
    Gender NVARCHAR(10) NULL,
    Membership NVARCHAR(50) NULL
);
GO

-- Orders table
CREATE TABLE IF NOT EXISTS Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    OrderDate DATETIME NOT NULL DEFAULT (GETDATE()),
    CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id) ON DELETE CASCADE
);
GO

-- OrderItems table
CREATE TABLE IF NOT EXISTS OrderItems (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    ItemName NVARCHAR(200) NOT NULL,
    Quantity INT NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE
);
GO

-- Indexes to speed lookups by name / foreign keys
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Customers_Name' AND object_id = OBJECT_ID('Customers'))
    CREATE INDEX IX_Customers_Name ON Customers(Name);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Orders_CustomerId' AND object_id = OBJECT_ID('Orders'))
    CREATE INDEX IX_Orders_CustomerId ON Orders(CustomerId);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_OrderItems_OrderId' AND object_id = OBJECT_ID('OrderItems'))
    CREATE INDEX IX_OrderItems_OrderId ON OrderItems(OrderId);
GO

-- Sample data (optional) - can be removed
IF NOT EXISTS (SELECT 1 FROM Customers WHERE Name = 'John Doe')
BEGIN
    INSERT INTO Customers (Name, Phone, Gender, Membership) VALUES ('John Doe', '1234567890', 'Male', 'Regular');
    DECLARE @Cid INT = SCOPE_IDENTITY();
    INSERT INTO Orders (CustomerId, OrderDate) VALUES (@Cid, GETDATE());
    DECLARE @Oid INT = SCOPE_IDENTITY();
    INSERT INTO OrderItems (OrderId, ItemName, Quantity, Price) VALUES (@Oid, 'Coffee', 2, 3.50), (@Oid, 'Pasta', 1, 7.00);
END
GO
