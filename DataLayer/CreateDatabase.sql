IF EXISTS (SELECT * FROM sys.databases WHERE name = 'DataShop')
BEGIN
    ALTER DATABASE DataShop SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE DataShop;
END
GO

CREATE DATABASE DataShop;
GO

USE DataShop;
GO

CREATE TABLE Users (
    Id INT PRIMARY KEY,
    Type NVARCHAR(50)
);

CREATE TABLE Catalogs (
    Product NVARCHAR(50) PRIMARY KEY,
    Price FLOAT NOT NULL
);

CREATE TABLE States (
    Product NVARCHAR(50) PRIMARY KEY,
    Amount INT
);

CREATE TABLE StoreStates (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TotalFunds FLOAT NOT NULL
);

CREATE TABLE Inventories (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(50) NOT NULL,
    Quantity INT NOT NULL,
    Price FLOAT NOT NULL
);

CREATE TABLE EventLogs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Timestamp DATETIME NOT NULL,
    EventType NVARCHAR(50) NOT NULL,
    Message NVARCHAR(MAX) NOT NULL,
    Details NVARCHAR(MAX)
);

-- Insert initial store state
INSERT INTO StoreStates (TotalFunds) VALUES (100.0);
GO 