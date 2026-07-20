using DbSport.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbSport.Repositories.Ado
{
    public class ExerciseAdoRepository
    {
        private readonly string _connectionString;

        public ExerciseAdoRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["SportDbConnection"].ConnectionString;
        }

        public void Create(Exercise exercise)
        {
            const string sql = @"INSERT INTO Exercises (Id, ProgramId, Name, IsActive, CreatedAt)
                                  VALUES (@Id, @ProgramId, @Name, @IsActive, @CreatedAt)";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = exercise.Id;
                command.Parameters.Add("@ProgramId", SqlDbType.UniqueIdentifier).Value = exercise.ProgramId;
                command.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = exercise.Name;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = exercise.IsActive;
                command.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value = exercise.CreatedAt;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<Exercise> GetAll()
        {
            var exercises = new List<Exercise>();
            const string sql = "SELECT Id, ProgramId, Name, IsActive, CreatedAt FROM Exercises";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        exercises.Add(new Exercise
                        {
                            Id = reader.GetGuid(0),
                            ProgramId = reader.GetGuid(1),
                            Name = reader.GetString(2),
                            IsActive = reader.GetBoolean(3),
                            CreatedAt = reader.GetDateTime(4)
                        });
                    }
                }
            }
            return exercises;
        }

        public Exercise GetById(Guid id)
        {
            const string sql = "SELECT Id, ProgramId, Name, IsActive, CreatedAt FROM Exercises WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Exercise
                        {
                            Id = reader.GetGuid(0),
                            ProgramId = reader.GetGuid(1),
                            Name = reader.GetString(2),
                            IsActive = reader.GetBoolean(3),
                            CreatedAt = reader.GetDateTime(4)
                        };
                    }
                }
            }
            return null;
        }

        public void Update(Exercise exercise)
        {
            const string sql = @"UPDATE Exercises 
                                  SET Name = @Name, IsActive = @IsActive
                                  WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = exercise.Id;
                command.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = exercise.Name;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = exercise.IsActive;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(Guid id)
        {
            const string sql = "DELETE FROM Exercises WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
