USE Sport;
GO

--Показывает сколько упражнений в каждой программе
SELECT 
    p.Name AS Программа,
    COUNT(e.Id) AS КоличествоУпражнений
FROM Programs p
LEFT JOIN Exercises e ON e.ProgramId = p.Id
GROUP BY p.Name
ORDER BY КоличествоУпражнений DESC;