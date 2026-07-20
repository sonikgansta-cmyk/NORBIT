USE Sport;
GO

UPDATE Activities
SET CaloriesBurned = 260.0,
    Notes = N'Самочувствие отличное'
WHERE ActivityDate = '2026-07-15' AND DurationMinutes = 40;
GO

SELECT * FROM Activities WHERE ActivityDate = '2026-07-15' AND DurationMinutes = 40;
GO

DELETE FROM Activities
WHERE DurationMinutes = 25;
GO

SELECT * FROM Activities;
GO