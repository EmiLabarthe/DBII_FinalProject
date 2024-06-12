use UcuPenca2024;

INSERT INTO GroupStages (Name) VALUES
('A'), ('B'), ('C'), ('D');

INSERT INTO Stages (Id) VALUES
('Grupos'), ('Cuartos AB'), ('Cuartos BA'),
('Cuartos CD'), ('Cuartos DC'),
('Semifinal AB'), ('Semifinal CD'), ('Final');

INSERT INTO Users (Id, FirstName, LastName, Gender, Email, Password) VALUES
('11111111', 'Gabriela', 'Hernández', 'Femenino', 'gabriela.hernandez@ucu.com', '123456'),
('22222222', 'Luis', 'Gómez', 'Masculino', 'luis.gomez@ucu.com', '123456'),
('33333333', 'Andrea', 'Martínez', 'Femenino', 'andrea.martinez@ucu.com', '123456'),
('44444444', 'Jorge', 'Díaz', 'Masculino', 'jorge.diaz@ucu.com', '123456'),
('55555555', 'Valentina', 'González', 'Femenino', 'valentina.gonzalez@ucu.com', '123456'),
('66666666', 'Martín', 'Pérez', 'Masculino', 'martin.perez@ucu.com', '123456'),
('77777777', 'Camila', 'Fernández', 'Femenino', 'camila.fernandez@ucu.com', '123456'),
('88888888', 'Sebastián', 'López', 'Masculino', 'sebastian.lopez@ucu.com', '123456'),
('99999999', 'Natalia', 'Sánchez', 'Femenino', 'natalia.sanchez@ucu.com', '123456'),
('12121212', 'Alejandro', 'Ruiz', 'Masculino', 'alejandro.ruiz@ucu.com', '123456');

INSERT INTO Students (StudentId) VALUES
('11111111'), ('22222222'), ('44444444'), ('55555555'), ('66666666'), ('77777777'), ('99999999');

INSERT INTO Administrators (AdminId) VALUES
('33333333'), ('88888888'), ('12121212');

INSERT INTO Careers (Name) VALUES
('Facultad de Ciencias de la Salud'),
('Facultad de Ciencias Empresariales'),
('Facultad de Derecho y Artes Liberales'),
('Facultad de Ingeniería y Tecnologías');

INSERT INTO StudentCareer (StudentId, CareerId) VALUES
('11111111', 1), ('22222222', 1), ('44444444', 2), ('55555555', 2),
('66666666', 3), ('77777777', 1), ('99999999', 3);

INSERT INTO NationalTeams (CountryName, GroupStageId) VALUES
('Argentina', 'A'), ('Perú', 'A'), ('Chile', 'A'), ('Canadá', 'A'),
('México', 'B'), ('Ecuador', 'B'), ('Venezuela', 'B'), ('Jamaica', 'B'),
('Estados Unidos', 'C'), ('Uruguay', 'C'), ('Panamá', 'C'), ('Bolivia', 'C'),
('Brasil', 'D'), ('Colombia', 'D'), ('Paraguay', 'D'), ('Costa Rica', 'D');

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

INSERT INTO Matches (LocalNationalTeam, VisitorNationalTeam, Date, StadiumId, StageId) VALUES
/* FECHA 1 */
('Argentina','Canadá', '2024-06-20 21:00:00', 1, 'Grupos'),
('Perú','Chile', '2024-06-21 21:00:00', 2, 'Grupos'),
('Ecuador','Venezuela', '2024-06-22 19:00:00', 5, 'Grupos'),
('México','Jamaica', '2024-06-22 22:00:00', 3, 'Grupos'),
('Estados Unidos','Bolivia', '2024-06-23 19:00:00', 2, 'Grupos'),
('Uruguay','Panamá', '2024-06-23 22:00:00', 7, 'Grupos'),
('Colombia','Paraguay', '2024-06-24 19:00:00', 3, 'Grupos'),
('Brasil','Costa Rica', '2024-06-24 20:00:00', 6, 'Grupos'),
/* FECHA 2 */
('Perú','Canadá', '2024-06-25 17:00:00', 8, 'Grupos'),
('Chile','Argentina', '2024-06-25 21:00:00', 10, 'Grupos'),
('Ecuador','Jamaica', '2024-06-26 15:00:00', 11, 'Grupos'),
('Venezuela','México', '2024-06-26 18:00:00', 6, 'Grupos'),
('Panamá','Estados Unidos', '2024-06-27 18:00:00', 1, 'Grupos'),
('Uruguay','Bolivia', '2024-06-27 21:00:00', 10, 'Grupos'),
('Colombia','Costa Rica', '2024-06-28 15:00:00', 12, 'Grupos'),
('Paraguay','Brasil', '2024-06-28 18:00:00', 11, 'Grupos'),
/* FECHA 3 */
('Argentina','Perú', '2024-06-29 20:00:00', 7, 'Grupos'),
('Canadá','Chile', '2024-06-29 20:00:00', 13, 'Grupos'),
('México','Ecuador', '2024-06-30 17:00:00', 12, 'Grupos'),
('Jamaica','Venezuela', '2024-06-30 19:00:00', 4, 'Grupos'),
('Estados Unidos','Uruguay', '2024-07-01 20:00:00', 9, 'Grupos'),
('Bolivia','Panamá', '2024-07-01 21:00:00', 13, 'Grupos'),
('Brasil','Colombia', '2024-07-02 18:00:00', 5, 'Grupos'),
('Costa Rica','Paraguay', '2024-07-02 20:00:00', 4, 'Grupos');

INSERT INTO Predictions (StudentId, MatchId, LocalNationalTeamPredictedGoals, VisitorNationalTeamPredictedGoals) VALUES
('11111111', 1, 3, 1), ('22222222', 1, 2, 2), ('44444444', 1, 3, 0),
('11111111', 2, 0, 0), ('22222222', 2, 2, 3),
('44444444', 3, 3, 0);

INSERT INTO StudentTournamentPrediction (StudentId, ChampionId, ViceChampionId) VALUES
('11111111', 'Uruguay', 'Argentina'), ('22222222', 'Brasil', 'Chile'), ('44444444', 'Uruguay', 'México'),
('66666666', 'Colombia', 'Argentina'), ('77777777', 'Argentina', 'Paraguay'), ('99999999', 'Brasil', 'Perú');

INSERT INTO NationalTeamGroupStage (NationalTeamId, GroupStageId) VALUES
('Argentina', 'A'), 
('Perú', 'A'), 
('Chile', 'A'), 
('Canadá', 'A'),
('México', 'B'), 
('Ecuador', 'B'), 
('Venezuela', 'B'), 
('Jamaica', 'B'),
('Estados Unidos', 'C'), 
('Uruguay', 'C'), 
('Panamá', 'C'), 
('Bolivia', 'C'),
('Brasil', 'D'), 
('Colombia', 'D'), 
('Paraguay', 'D'), 
('Costa Rica', 'D');