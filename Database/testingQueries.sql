create database UcuPenca2024;
use UcuPenca2024;

/* GROUP_STAGES */
CREATE TABLE GroupStages (
    Name varchar(1) PRIMARY KEY
);

INSERT INTO GroupStages (Name) VALUES
('A'), ('B'), ('C'), ('D');

/* USERS */
CREATE TABLE Users (
    Id varchar(20) PRIMARY KEY,
    FirstName varchar(50) NOT NULL,
    LastName varchar(50) NOT NULL,
    Gender varchar(20) NOT NULL,
    Email varchar(50) NOT NULL,
    Password varchar(254) NOT NULL,
);

INSERT INTO Users (Id, FirstName, LastName, Gender, Email) VALUES
('11111111', 'Gabriela', 'Hernández', 'Femenino', 'gabriela.hernandez@ucu.com'),
('22222222', 'Luis', 'Gómez', 'Masculino', 'luis.gomez@ucu.com'),
('33333333', 'Andrea', 'Martínez', 'Femenino', 'andrea.martinez@ucu.com'),
('44444444', 'Jorge', 'Díaz', 'Masculino', 'jorge.diaz@ucu.com'),
('55555555', 'Valentina', 'González', 'Femenino', 'valentina.gonzalez@ucu.com'),
('66666666', 'Martín', 'Pérez', 'Masculino', 'martin.perez@ucu.com'),
('77777777', 'Camila', 'Fernández', 'Femenino', 'camila.fernandez@ucu.com'),
('88888888', 'Sebastián', 'López', 'Masculino', 'sebastian.lopez@ucu.com'),
('99999999', 'Natalia', 'Sánchez', 'Femenino', 'natalia.sanchez@ucu.com'),
('12121212', 'Alejandro', 'Ruiz', 'Masculino', 'alejandro.ruiz@ucu.com');

/* STUDENTS */
CREATE TABLE Students (
    StudentId varchar(20) PRIMARY KEY,
    Score int DEFAULT 0,
    FOREIGN KEY (StudentId) REFERENCES Users (Id)
);

INSERT INTO Students (StudentId) VALUES
('11111111'), ('22222222'), ('44444444'), ('55555555'), ('66666666'), ('77777777'), ('99999999');

/* ADMINISTRATORS */
CREATE TABLE Administrators (
    AdminId varchar(20) PRIMARY KEY,
    FOREIGN KEY (AdminId) REFERENCES Users (Id)
);

INSERT INTO Administrators (AdminId) VALUES
('33333333'), ('88888888'), ('12121212');

/* CAREERS */
CREATE TABLE Careers (
    Id bigint PRIMARY KEY AUTO_INCREMENT,
    Name varchar(50) NOT NULL
);

INSERT INTO Careers (Name) VALUES
('Facultad de Ciencias de la Salud'),
('Facultad de Ciencias Empresariales'),
('Facultad de Derecho y Artes Liberales'),
('Facultad de Ingeniería y Tecnologías');

/* STUDENT-CAREER */
CREATE TABLE StudentCareer (
    StudentId varchar(20),
    CareerId bigint,
    PRIMARY KEY (StudentId, CareerId),
    FOREIGN KEY (StudentId) REFERENCES Students (StudentId),
    FOREIGN KEY (CareerId) REFERENCES Careers (Id)
);

INSERT INTO StudentCareer (StudentId, CareerId) VALUES
('11111111', 1), ('22222222', 1), ('44444444', 2), ('55555555', 2),
('66666666', 3), ('77777777', 1), ('99999999', 3);

/* NATIONAL_TEAMS */
CREATE TABLE NationalTeams (
    CountryName varchar(20) PRIMARY KEY,
    GroupStageId varchar(1) NOT NULL,
    FOREIGN KEY (GroupStageId) REFERENCES GroupStages (Name)
);

INSERT INTO NationalTeams (CountryName, GroupStageId) VALUES
('Argentina', 'A'), ('Perú', 'A'), ('Chile', 'A'), ('Canadá', 'A'),
('México', 'B'), ('Ecuador', 'B'), ('Venezuela', 'B'), ('Jamaica', 'B'),
('Estados Unidos', 'C'), ('Uruguay', 'C'), ('Panamá', 'C'), ('Bolivia', 'C'),
('Brasil', 'D'), ('Colombia', 'D'), ('Paraguay', 'D'), ('Costa Rica', 'D');


/* STADIUMS */
CREATE TABLE Stadiums (
    Id bigint PRIMARY KEY AUTO_INCREMENT,
    Name varchar(20) NOT NULL,
    State varchar(20) NOT NULL,
    City varchar(20) NOT NULL
);

INSERT INTO Stadiums (Name, State, City) VALUES
('Mercedes-Benz Stadium', 'Georgia', 'Atlanta'),
('AT&T Stadium', 'Texas', 'Arlington'),
('NRG Stadium', 'Texas', 'Houston'),
('Q2 Stadium', 'Texas', 'Austin'),
('Levi''s® Stadium', 'California', 'Santa Clara'),
('SoFi Stadium', 'California', 'Inglewood'),
('Hard Rock Stadium', 'Florida', 'Miami'),
('Children''s Mercy Park', 'Kansas', 'Kansas City'),
('GEHA Field at Arrowhead', 'Missouri', 'Kansas City'),
('MetLife Stadium', 'New Jersey', 'East Rutherford'),
('Allegiant Stadium', 'Nevada', 'Las Vegas'),
('State Farm Stadium', 'Arizona', 'Glendale'),
('Inter&Co Stadium', 'Florida', 'Orlando');


/* MATCHES - NationalTeam vs NationalTeam */
CREATE TABLE Matches (
    Id bigint PRIMARY KEY AUTO_INCREMENT,
    LocalNationalTeam varchar(20) NOT NULL,
    VisitorNationalTeam varchar(20) NOT NULL,
    Date TIMESTAMP NOT NULL,
    StadiumId bigint NOT NULL,
    FOREIGN KEY (LocalNationalTeam) REFERENCES NationalTeams (CountryName),
    FOREIGN KEY (VisitorNationalTeam) REFERENCES NationalTeams (CountryName),
    FOREIGN KEY (StadiumId) REFERENCES Stadiums (Id)
);

INSERT INTO Matches (LocalNationalTeam, VisitorNationalTeam, Date, StadiumId) VALUES
/* FECHA 1 */
('Argentina','Canadá', '2024-06-20 21:00:00', 0),
('Perú','Chile', '2024-06-21 21:00:00', 1),
('Ecuador','Venezuela', '2024-06-22 19:00:00', 4),
('México','Jamaica', '2024-06-22 22:00:00', 2),
('Estados Unidos','Bolivia', '2024-06-23 19:00:00', 1),
('Uruguay','Panamá', '2024-06-23 22:00:00', 6),
('Colombia','Paraguay', '2024-06-24 19:00:00', 2),
('Brasil','Costa Rica', '2024-06-24 20:00:00', 5),
/* FECHA 2 */
('Perú','Canadá', '2024-06-25 17:00:00', 7),
('Chile','Argentina', '2024-06-25 21:00:00', 9),
('Ecuador','Jamaica', '2024-06-26 15:00:00', 10),
('Venezuela','México', '2024-06-26 18:00:00', 5),
('Panamá','Estados Unidos', '2024-06-27 18:00:00', 0),
('Uruguay','Bolivia', '2024-06-27 21:00:00', 9),
('Colombia','Costa Rica', '2024-06-28 15:00:00', 11),
('Paraguay','Brasil', '2024-06-28 18:00:00', 10),
/* FECHA 3 */
('Argentina','Perú', '2024-06-29 20:00:00', 6),
('Canadá','Chile', '2024-06-29 20:00:00', 12),
('México','Ecuador', '2024-06-30 17:00:00', 11),
('Jamaica','Venezuela', '2024-06-30 19:00:00', 3),
('Estados Unidos','Uruguay', '2024-07-01 20:00:00', 8),
('Bolivia','Panamá', '2024-07-01 21:00:00', 12),
('Brasil','Colombia', '2024-07-02 18:00:00', 4),
('Costa Rica','Paraguay', '2024-07-02 20:00:00', 3);


/* MATCH_RESULTS */
CREATE TABLE MatchResults (
    Id bigint PRIMARY KEY AUTO_INCREMENT,
    MatchId bigint NOT NULL,
    LocalNationalTeamGoals int DEFAULT 0,
    VisitorNationalTeamGoals int DEFAULT 0,
    WinnerId varchar(20),
    FOREIGN KEY (MatchId) REFERENCES Matches (Id),
    FOREIGN KEY (WinnerId) REFERENCES NationalTeams (CountryName)
);

/* PREDICTIONS */
CREATE TABLE Predictions (
    Id bigint PRIMARY KEY AUTO_INCREMENT,
    StudentId varchar(20) NOT NULL,
    MatchId bigint NOT NULL,
    LocalNationalTeamPredictedGoals int DEFAULT 0,
    VisitorNationalTeamPredictedGoals int DEFAULT 0,
    FOREIGN KEY (MatchId) REFERENCES Matches (Id),
    FOREIGN KEY (StudentId) REFERENCES Students (StudentId)
);

INSERT INTO Predictions (StudentId, MatchId, LocalNationalTeamPredictedGoals, VisitorNationalTeamPredictedGoals) VALUES
('11111111', 1, 3, 1), ('22222222', 1, 2, 2), ('44444444', 1, 3, 0),
('11111111', 2, 0, 0), ('22222222', 2, 2, 3),
('44444444', 3, 3, 0);

/* STUDENT-TOURNAMENT_PREDICTIONS */
CREATE TABLE StudentTournamentPrediction (
    StudentId varchar(20),
    ChampionId varchar(20),
    ViceChampionId varchar(20),
    PRIMARY KEY (StudentId, ChampionId, ViceChampionId),
    FOREIGN KEY (StudentId) REFERENCES Students (StudentId),
    FOREIGN KEY (ChampionId) REFERENCES NationalTeams (CountryName),
    FOREIGN KEY (ViceChampionId) REFERENCES NationalTeams (CountryName)
);

INSERT INTO StudentTournamentPrediction (StudentId, ChampionId, ViceChampionId) VALUES
('11111111', 'Uruguay', 'Argentina'), ('22222222', 'Brasil', 'Chile'), ('44444444', 'Uruguay', 'México'),
('66666666', 'Colombia', 'Argentina'), ('77777777', 'Argentina', 'Paraguay'), ('99999999', 'Brasil', 'Perú');

/* NATIONAL_TEAMS-GROUP_STAGES */
CREATE TABLE NationalTeamGroupStage (
    Id bigint PRIMARY KEY AUTO_INCREMENT,
    NationalTeamId varchar(20),
    Points int DEFAULT 0,
    Wins int DEFAULT 0,
    Draws int DEFAULT 0,
    Losses int DEFAULT 0,
    GoalsFor int DEFAULT 0,
    GoalsAgainst int DEFAULT 0,
    GoalsDifference int DEFAULT 0,
    FOREIGN KEY (NationalTeamId) REFERENCES NationalTeams (CountryName)
);
