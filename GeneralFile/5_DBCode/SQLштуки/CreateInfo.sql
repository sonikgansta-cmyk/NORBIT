USE Sport;
GO

INSERT INTO Users (Name, Email) VALUES
(N'Мира', 'mira@test.com'),
(N'Александр', 'alex@test.com');
GO

DECLARE @User1 UNIQUEIDENTIFIER = (SELECT Id FROM Users WHERE Email = 'test@test.com');
DECLARE @User2 UNIQUEIDENTIFIER = (SELECT Id FROM Users WHERE Email = 'mira@test.com');
DECLARE @User3 UNIQUEIDENTIFIER = (SELECT Id FROM Users WHERE Email = 'alex@test.com');

INSERT INTO Programs (UserId, Name, Type, IsActive) VALUES
(@User1, N'Силовая тренировка', N'Силовая', 1),
(@User2, N'Бег', N'Кардио', 1),
(@User3, N'Йога', N'Растяжка', 0);
GO

DECLARE @Prog1 UNIQUEIDENTIFIER = (SELECT Id FROM Programs WHERE Name = N'Силовая тренировка');
DECLARE @Prog2 UNIQUEIDENTIFIER = (SELECT Id FROM Programs WHERE Name = N'Бег');
DECLARE @Prog3 UNIQUEIDENTIFIER = (SELECT Id FROM Programs WHERE Name = N'Йога');

INSERT INTO Exercises (ProgramId, Name, IsActive) VALUES
(@Prog1, N'Приседания со штангой', 1),
(@Prog1, N'Жим лёжа', 1),
(@Prog2, N'Утренняя пробежка', 1),
(@Prog3, N'Растяжка спины', 0);
GO

INSERT INTO MuscleGroups (Name) VALUES
(N'Ноги'), (N'Грудь'), (N'Спина'), (N'Кардио');
GO

DECLARE @Ex1 UNIQUEIDENTIFIER = (SELECT Id FROM Exercises WHERE Name = N'Приседания со штангой');
DECLARE @Ex2 UNIQUEIDENTIFIER = (SELECT Id FROM Exercises WHERE Name = N'Жим лёжа');
DECLARE @Ex3 UNIQUEIDENTIFIER = (SELECT Id FROM Exercises WHERE Name = N'Утренняя пробежка');

DECLARE @MG_Legs UNIQUEIDENTIFIER = (SELECT Id FROM MuscleGroups WHERE Name = N'Ноги');
DECLARE @MG_Chest UNIQUEIDENTIFIER = (SELECT Id FROM MuscleGroups WHERE Name = N'Грудь');
DECLARE @MG_Cardio UNIQUEIDENTIFIER = (SELECT Id FROM MuscleGroups WHERE Name = N'Кардио');

INSERT INTO ExerciseMuscleGroups (ExerciseId, MuscleGroupId) VALUES
(@Ex1, @MG_Legs),
(@Ex2, @MG_Chest),
(@Ex3, @MG_Cardio);
GO

INSERT INTO Activities (ExerciseId, ActivityDate, DurationMinutes, Notes, CaloriesBurned) VALUES
(@Ex1, '2026-07-15', 40, N'Хорошее самочувствие', 250.5),
(@Ex2, '2026-07-15', 30, N'Тяжело', 180.0),
(@Ex3, '2026-07-16', 25, N'Лёгкая пробежка', 200.0),
(@Ex3, '2026-07-17', 35, N'Дождь, бежал медленнее', 220.0);
GO