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
    UpdatedAt DATETIME DEFAULT GETDATE(),
	IsDeleted BIT DEFAULT 0
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



-- Insertar 10 películas
INSERT INTO Movies (Name, Description, ReleaseDate, Director, Duration, Genre)
VALUES
('Inception', 'A mind-bending thriller', '20100716', 'Christopher Nolan', 148, 'Sci-Fi'),
('The Dark Knight', 'Batman faces Joker', '20080718', 'Christopher Nolan', 152, 'Action'),
('Interstellar', 'Space exploration drama', '20141107', 'Christopher Nolan', 169, 'Sci-Fi'),
('The Matrix', 'Reality is a simulation', '19990331', 'The Wachowskis', 136, 'Sci-Fi'),
('Pulp Fiction', 'Crime and drama', '19941014', 'Quentin Tarantino', 154, 'Crime'),
('Forrest Gump', 'Life story of Forrest', '19940706', 'Robert Zemeckis', 142, 'Drama'),
('Gladiator', 'Roman epic', '20000505', 'Ridley Scott', 155, 'Action'),
('The Shawshank Redemption', 'Prison drama', '19940923', 'Frank Darabont', 142, 'Drama'),
('Titanic', 'Historical romance', '19971219', 'James Cameron', 195, 'Romance'),
('Jurassic Park', 'Dinosaurs unleashed', '19930611', 'Steven Spielberg', 127, 'Adventure');

-- Insertar 10 salas de cine
INSERT INTO CinemaRooms (Name, Capacity, IsVIP)
VALUES
('Sala 1', 100, 0),
('Sala 2', 150, 1),
('Sala 3', 80, 0),
('Sala 4', 120, 1),
('Sala 5', 200, 0),
('Sala 6', 90, 0),
('Sala 7', 110, 1),
('Sala 8', 130, 0),
('Sala 9', 100, 0),
('Sala 10', 140, 1);

INSERT INTO MovieCinemaRooms (MovieId, CinemaRoomId, StartDate, EndDate)
VALUES
(1, 1, '20250501', '20250510'),
(2, 1, '20250511', '20250520'),
(3, 1, '20250521', '20250530'),

(4, 2, '20250501', '20250507'),
(5, 2, '20250508', '20250514'),
(6, 2, '20250515', '20250521'),
(7, 2, '20250522', '20250528'),
(8, 2, '20250529', '20250604'),

(9, 3, '20250501', '20250515'),

(10, 4, '20250501', '20250507'),
(1, 4, '20250508', '20250514'),
(2, 4, '20250515', '20250521'),
(3, 4, '20250522', '20250528'),

(4, 5, '20250501', '20250505'),
(5, 5, '20250506', '20250510'),
(6, 5, '20250511', '20250515'),
(7, 5, '20250516', '20250520'),
(8, 5, '20250521', '20250525'),
(9, 5, '20250526', '20250530'),

(10, 6, '20250501', '20250510'),
(1, 6, '20250511', '20250520'),

(2, 7, '20250501', '20250510'),
(3, 7, '20250511', '20250520'),
(4, 7, '20250521', '20250530'),

(5, 8, '20250501', '20250506'),
(6, 8, '20250507', '20250512'),
(7, 8, '20250513', '20250518'),
(8, 8, '20250519', '20250524'),
(9, 8, '20250525', '20250530'),

(10, 9, '20250501', '20250515'),

(1, 10, '20250501', '20250507'),
(2, 10, '20250508', '20250514'),
(3, 10, '20250515', '20250521'),
(4, 10, '20250522', '20250528');
