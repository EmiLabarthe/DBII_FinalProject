create database UcuPenca2024;
use UcuPenca2024;

/* GROUP_STAGES */
CREATE TABLE GroupStages (
    Name varchar(1) PRIMARY KEY
);

CREATE TABLE Stages (
    Id varchar(20) PRIMARY KEY
);

/* USERS */
CREATE TABLE Users (
    Id varchar(20) PRIMARY KEY,
    FirstName varchar(50) NOT NULL,
    LastName varchar(50) NOT NULL,
    Gender varchar(20) NOT NULL,
    Email varchar(50) NOT NULL,
    Password varchar(254) NOT NULL
);

/* STUDENTS */
CREATE TABLE Students (
    StudentId varchar(20) PRIMARY KEY,
    Score int DEFAULT 0,
    FOREIGN KEY (StudentId) REFERENCES Users (Id)
);

/* ADMINISTRATORS */
CREATE TABLE Administrators (
    AdminId varchar(20) PRIMARY KEY,
    FOREIGN KEY (AdminId) REFERENCES Users (Id)
);

/* CAREERS */
CREATE TABLE Careers (
    Id bigint PRIMARY KEY AUTO_INCREMENT,
    Name varchar(50) NOT NULL
);

/* STUDENT-CAREER */
CREATE TABLE StudentCareer (
    StudentId varchar(20),
    CareerId bigint,
    PRIMARY KEY (StudentId, CareerId),
    FOREIGN KEY (StudentId) REFERENCES Students (StudentId),
    FOREIGN KEY (CareerId) REFERENCES Careers (Id)
);

/* NATIONAL_TEAMS */
CREATE TABLE NationalTeams (
    CountryName varchar(20) PRIMARY KEY,
    GroupStageId varchar(1) NOT NULL,
    FOREIGN KEY (GroupStageId) REFERENCES GroupStages (Name)
);

/* STADIUMS */
CREATE TABLE Stadiums (
    Id bigint PRIMARY KEY AUTO_INCREMENT,
    Name varchar(50) NOT NULL,
    State varchar(50) NOT NULL,
    City varchar(50) NOT NULL
);

/* MATCHES - NationalTeam vs NationalTeam */
CREATE TABLE Matches (
    Id bigint PRIMARY KEY AUTO_INCREMENT,
    LocalNationalTeam varchar(20) NOT NULL,
    VisitorNationalTeam varchar(20) NOT NULL,
    Date TIMESTAMP NOT NULL,
    StadiumId bigint NOT NULL,
    StageId varchar(20) NOT NULL,
    FOREIGN KEY (LocalNationalTeam) REFERENCES NationalTeams (CountryName),
    FOREIGN KEY (VisitorNationalTeam) REFERENCES NationalTeams (CountryName),
    FOREIGN KEY (StadiumId) REFERENCES Stadiums (Id),
    Foreign Key (StageId) REFERENCES Stages (Id)
);

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

/* NATIONAL_TEAMS-GROUP_STAGES */
CREATE TABLE NationalTeamGroupStage (
    Id bigint PRIMARY KEY AUTO_INCREMENT,
    NationalTeamId varchar(20),
    GroupStageId varchar(1),
    Points int DEFAULT 0,
    Wins int DEFAULT 0,
    Draws int DEFAULT 0,
    Losses int DEFAULT 0,
    GoalsFor int DEFAULT 0,
    GoalsAgainst int DEFAULT 0,
    GoalsDifference int DEFAULT 0,
    FOREIGN KEY (NationalTeamId) REFERENCES NationalTeams (CountryName),
    FOREIGN KEY (GroupStageId) REFERENCES GroupStages (Name)
);
