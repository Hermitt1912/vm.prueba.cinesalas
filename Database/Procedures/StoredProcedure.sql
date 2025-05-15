X|CREATE TABLE Movies (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    ReleaseDate DATETIME NOT NULL,
    Director NVARCHAR(100),
    Duration INT, -- en minutos
    Genre NVARCHAR(50),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE CinemaRooms (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Capacity INT NOT NULL,
    IsVIP BIT DEFAULT 0,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE MovieCinemaRooms (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MovieId INT FOREIGN KEY REFERENCES Movies(Id),
    CinemaRoomId INT FOREIGN KEY REFERENCES CinemaRooms(Id),
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

-- Stored Procedure para obtener salas de cine con su cantidad de películas y estado
CREATE DATABASE CINEMA_VMT

USE CINEMA_VMT



CREATE TABLE Movies (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    ReleaseDate DATETIME NOT NULL,
    Director NVARCHAR(100),
    Duration INT, -- en minutos
    Genre NVARCHAR(50),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE CinemaRooms (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Capacity INT NOT NULL,
    IsVIP BIT DEFAULT 0,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE MovieCinemaRooms (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MovieId INT FOREIGN KEY REFERENCES Movies(Id),
    CinemaRoomId INT FOREIGN KEY REFERENCES CinemaRooms(Id),
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

-- Stored Procedure para obtener salas de cine con su cantidad de películas y estado
CREATE PROCEDURE GetCinemaRoomsWithMovieCount AS
BEGIN
    SELECT 
        cr.Id,
        cr.Name,
        COUNT(mcr.Id) AS MovieCount,
        CASE 
            WHEN COUNT(mcr.Id) < 3 THEN 'Sala disponible'
            WHEN COUNT(mcr.Id) BETWEEN 3 AND 5 THEN CONCAT('Sala con ', COUNT(mcr.Id), ' películas asignadas')
            ELSE 'Sala no disponible'
        END AS Status
    FROM CinemaRooms cr
    LEFT JOIN MovieCinemaRooms mcr ON cr.Id = mcr.CinemaRoomId AND mcr.IsActive = 1
    WHERE cr.IsActive = 1
    GROUP BY cr.Id, cr.Name
END


