USE Sport;
GO

SELECT p.Name AS Программа, e.Name AS Упражнение
FROM Programs p
INNER JOIN Exercises e ON e.ProgramId = p.Id;
GO

SELECT p.Name AS Программа, e.Name AS Упражнение
FROM Programs p
LEFT JOIN Exercises e ON e.ProgramId = p.Id;
GO

SELECT e.Name AS Упражнение, mg.Name AS ГруппаМышц
FROM Exercises e
JOIN ExerciseMuscleGroups emg ON emg.ExerciseId = e.Id
RIGHT JOIN MuscleGroups mg ON mg.Id = emg.MuscleGroupId
ORDER BY mg.Name;
GO