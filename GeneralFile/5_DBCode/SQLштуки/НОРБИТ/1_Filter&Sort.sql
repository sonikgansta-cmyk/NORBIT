USE Sport;
GO

-- Активности более 30 минут. Сортировка по дате (сначала новые)
SELECT Id, ActivityDate, DurationMinutes, Notes, CaloriesBurned
FROM Activities
WHERE DurationMinutes > 30
ORDER BY ActivityDate DESC;
GO