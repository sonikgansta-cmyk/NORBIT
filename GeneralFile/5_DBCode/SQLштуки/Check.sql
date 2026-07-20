USE Sport;
GO

--SELECT u.Name AS Пользователь, p.Name AS Программа, e.Name AS Упражнение
--FROM Users u
--JOIN Programs p ON p.UserId = u.Id
--JOIN Exercises e ON e.ProgramId = p.Id
--ORDER BY u.Name;

SELECT * FROM Users;
SELECT * FROM Programs;
SELECT * FROM Exercises;
SELECT * FROM MuscleGroups;
SELECT * FROM ExerciseMuscleGroups;
SELECT * FROM Activities;