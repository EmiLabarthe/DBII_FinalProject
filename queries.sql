create database UcuPenca2024;
use UcuPenca2024;

/* USERS */
CREATE TABLE Users (
    Id varchar(20) PRIMARY KEY,
    FirstName varchar(50) NOT NULL,
    LastName varchar(50) NOT NULL,
    Gender varchar(20) NOT NULL,
    Email varchar(50) NOT NULL
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
    StudentId varchar(20) PRIMARY KEY,
    CareerId varchar(50) PRIMARY KEY,
    FOREIGN KEY (StudentId) REFERENCES Users (Id),
    FOREIGN KEY (CareerId) REFERENCES Careers (Id)
);

INSERT INTO StudentCareer (StudentId, CareerId) VALUES
('11111111', 1), ('22222222', 1), ('44444444', 2), ('55555555', 2),
('66666666', 3), ('77777777', 1), ('99999999', 3);

/* NATIONAL_TEAMS */
CREATE TABLE NationalTeams (
    CountryName varchar(20) PRIMARY KEY, /* Determinar ID */
    GroupStageId varchar(1) NOT NULL,
    FOREIGN KEY (GroupStageId) REFERENCES GroupStages (Name)
);

INSERT INTO NationalTeams (CountryName) VALUES
('Argentina'), ('Perú'), ('Chile'), ('Canadá'),
('México'), ('Ecuador'), ('Venezuela'), ('Jamaica'),
('Estados Unidos'), ('Uruguay'), ('Panamá'), ('Bolivia'),
('Brasil'), ('Colombia'), ('Paraguay'), ('Costa Rica');

/* MATCHES - NationalTeam vs NationalTeam */
CREATE TABLE Matches (
    Id bigint PRIMARY KEY AUTO_INCREMENT,
    LocalNationalTeam varchar(100) NOT NULL, /* Determinar PK */
    VisitorNationalTeam varchar(100) NOT NULL, /* Determinar PK */
    Date TIMESTAMP NOT NULL,
    City varchar(50) NOT NULL,
    FOREIGN KEY (LocalNationalTeam) REFERENCES NationalTeams (CountryName),
    FOREIGN KEY (VisitorNationalTeam) REFERENCES NationalTeams (CountryName)
);

INSERT INTO Matches (LocalNationalTeam, VisitorNationalTeam, Date, City) VALUES
/* FECHA 1 */
('Argentina','Canadá', '2024-06-20 21:00:00', 'Atlanta'),
('Perú','Chile', '2024-06-21 21:00:00', 'Arlington'),
('Ecuador','Venezuela', '2024-06-22 19:00:00', 'Santa Clara'),
('México','Jamaica', '2024-06-22 22:00:00', 'Houston'),
('Estados Unidos','Bolivia', '2024-06-23 19:00:00', 'Arlington'),
('Uruguay','Panamá', '2024-06-23 22:00:00', 'Miami'),
('Colombia','Paraguay', '2024-06-24 19:00:00', 'Houston'),
('Brasil','Costa Rica', '2024-06-24 20:00:00', 'Inglewood');

/* MATCH_RESULTS */
CREATE TABLE MatchResults (
    Id bigint PRIMARY KEY AUTO_INCREMENT,
    MatchId bigint NOT NULL,
    LocalNationalTeamGoals int DEFAULT 0,
    VisitorNationalTeamGoals int DEFAULT 0,
    WinnerId varchar(20), /* Determinar PK */
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
('11111111', 0, 3, 1), ('22222222', 0, 2, 2), ('44444444', 0, 3, 0),
('11111111', 1, 0, 0), ('22222222', 1, 2, 3),
('44444444', 2, 3, 0);


/* STUDENT-TOURNAMENT_PREDICTIONS */
CREATE TABLE StudentTournamentPrediction (
    StudentId varchar(20) PRIMARY KEY,
    ChampionId varchar(20) PRIMARY KEY,  /* Determinar PK */
    ViceChampionId varchar(20) PRIMARY KEY,  /* Determinar PK */
    FOREIGN KEY (StudentId) REFERENCES Students (StudentId),
    FOREIGN KEY (ChampionId) REFERENCES NationalTeams (CountryName),
    FOREIGN KEY (ViceChampionId) REFERENCES NationalTeams (CountryName)
);

INSERT INTO StudentTournamentPrediction (StudentId, ChampionId, ViceChampionId) VALUES
('11111111', 'Uruguay', 'Argentina'), ('22222222', 'Brasil', 'Chile'), ('44444444', 'Uruguay', 'México'),
('66666666', 'Colombia', 'Argentina'), ('77777777', 'Argentina', 'Paraguay'), ('99999999', 'Brasil', 'Perú'),;


/* GROUP_STAGES */
CREATE TABLE GroupStages (
    Name varchar(1) PRIMARY KEY,
);

INSERT INTO GroupStages (Name) VALUES
('A'), ('B'), ('C'), ('D');


/* NATIONAL_TEAMS-GROUP_STAGES */
CREATE TABLE NationalTeamGroupStage (
    Id bigint PRIMARY KEY AUTO_INCREMENT,
    NationalTeamId varchar(20) PRIMARY KEY, /* Determinar PK */
    Points int DEFAULT 0,
    Wins int DEFAULT 0,
    Draws int DEFAULT 0,
    Losses int DEFAULT 0,
    GoalsFor int DEFAULT 0,
    GoalsAgainst int DEFAULT 0,
    GoalsDifference int DEFAULT 0,
    FOREIGN KEY (NationalTeamId) REFERENCES NationalTeams (CountryName)
);