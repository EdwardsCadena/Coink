create database coink
use coink

CREATE TABLE Countries (
    Id INT PRIMARY KEY identity,
    Name NVARCHAR(50) NOT NULL
);

CREATE TABLE Departments (
    Id INT PRIMARY KEY identity,
    Name NVARCHAR(50) NOT NULL,
    CountryId INT FOREIGN KEY REFERENCES Countries(Id)
);

CREATE TABLE Municipalities (
    Id INT PRIMARY KEY identity,
    Name NVARCHAR(50) NOT NULL,
    DepartmentId INT FOREIGN KEY REFERENCES Departments(Id)
);

CREATE TABLE Users (
    Id INT PRIMARY KEY identity,
    Name NVARCHAR(50) NOT NULL,
    Phone NVARCHAR(15),
    Address NVARCHAR(100),
    CountryId INT FOREIGN KEY REFERENCES Countries(Id),
    DepartmentId INT FOREIGN KEY REFERENCES Departments(Id),
    MunicipalityId INT FOREIGN KEY REFERENCES Municipalities(Id)
);

CREATE PROCEDURE GetAllUsers
AS
BEGIN
    SELECT 
        U.Id,
        U.Name,
        U.Phone,
        U.Address,
        U.CountryId,
        C.Name AS CountryName,
        U.DepartmentId,
        D.Name AS DepartmentName,
        U.MunicipalityId,
        M.Name AS MunicipalityName
    FROM 
        Users U
        INNER JOIN Countries C ON U.CountryId = C.Id
        INNER JOIN Departments D ON U.DepartmentId = D.Id
        INNER JOIN Municipalities M ON U.MunicipalityId = M.Id
END
go

-- Procedure to create a new user
CREATE PROCEDURE CreateUser
    @Name NVARCHAR(50),
    @Phone NVARCHAR(15),
    @Address NVARCHAR(100),
    @CountryId INT,
    @DepartmentId INT,
    @MunicipalityId INT
AS
INSERT INTO Users (Name, Phone, Address, CountryId, DepartmentId, MunicipalityId)
VALUES (@Name, @Phone, @Address, @CountryId, @DepartmentId, @MunicipalityId)
GO

-- Procedure to read a user by ID
CREATE PROCEDURE GetUserById
    @Id INT
AS
SELECT * FROM Users WHERE Id = @Id
GO

-- Procedure to update a user
CREATE PROCEDURE UpdateUser
    @Id INT,
    @Name NVARCHAR(50),
    @Phone NVARCHAR(15),
    @Address NVARCHAR(100),
    @CountryId INT,
    @DepartmentId INT,
    @MunicipalityId INT
AS
UPDATE Users
SET Name = @Name, Phone = @Phone, Address = @Address, CountryId = @CountryId, DepartmentId = @DepartmentId, MunicipalityId = @MunicipalityId
WHERE Id = @Id
GO

-- Procedure to delete a user
CREATE PROCEDURE DeleteUser
    @Id INT
AS
DELETE FROM Users WHERE Id = @Id
GO
-- Insertando datos en la tabla Countries
INSERT INTO Countries (Name) VALUES 
('Landia'),
('Oceanica'),
('Aeria');

-- Insertando datos en la tabla Departments
INSERT INTO Departments (Name, CountryId) VALUES 
('North Landia', 1),
('South Landia', 1),
('West Oceanica', 2),
('East Oceanica', 2),
('Central Aeria', 3),
('South Aeria', 3);

-- Insertando datos en la tabla Municipalities
INSERT INTO Municipalities (Name, DepartmentId) VALUES 
('Downtown North Landia', 1),
('Uptown South Landia', 2),
('Harbortown West Oceanica', 3),
('Beachtown East Oceanica', 4),
('Midtown Central Aeria', 5),
('Southtown South Aeria', 6);

-- Insertando datos en la tabla Users
INSERT INTO Users (Name, Phone, Address, CountryId, DepartmentId, MunicipalityId) VALUES 
('John Doe', '123-456-7890', '123 Main St, Downtown North Landia', 1, 1, 1),
('Jane Smith', '456-789-0123', '456 Elm St, Uptown South Landia', 1, 2, 2),
('Bob Johnson', '789-012-3456', '789 Oak St, Harbortown West Oceanica', 2, 3, 3),
('Alice Williams', '012-345-6789', '012 Pine St, Beachtown East Oceanica', 2, 4, 4),
('Charlie Brown', '345-678-9012', '345 Maple St, Midtown Central Aeria', 3, 5, 5),
('Diana Davis', '678-901-2345', '678 Birch St, Southtown South Aeria', 3, 6, 6);