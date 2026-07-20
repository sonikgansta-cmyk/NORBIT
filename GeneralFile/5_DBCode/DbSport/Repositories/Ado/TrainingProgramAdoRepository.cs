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
    public class TrainingProgramAdoRepository
    {
        private readonly string _connectionString;

        public TrainingProgramAdoRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["SportDbConnection"].ConnectionString;
        }

        public void Create(TrainingProgram program)
        {
            const string sql = @"INSERT INTO Programs (Id, UserId, Name, Type, IsActive, CreatedAt)
                                  VALUES (@Id, @UserId, @Name, @Type, @IsActive, @CreatedAt)";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = program.Id;
                command.Parameters.Add("@UserId", SqlDbType.UniqueIdentifier).Value = program.UserId;
                command.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = program.Name;
                command.Parameters.Add("@Type", SqlDbType.NVarChar, 50).Value = program.Type;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = program.IsActive;
                command.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value = program.CreatedAt;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<TrainingProgram> GetAll()
        {
            var programs = new List<TrainingProgram>();
            const string sql = "SELECT Id, UserId, Name, Type, IsActive, CreatedAt FROM Programs";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        programs.Add(new TrainingProgram
                        {
                            Id = reader.GetGuid(0),
                            UserId = reader.GetGuid(1),
                            Name = reader.GetString(2),
                            Type = reader.GetString(3),
                            IsActive = reader.GetBoolean(4),
                            CreatedAt = reader.GetDateTime(5)
                        });
                    }
                }
            }
            return programs;
        }

        public TrainingProgram GetById(Guid id)
        {
            const string sql = "SELECT Id, UserId, Name, Type, IsActive, CreatedAt FROM Programs WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new TrainingProgram
                        {
                            Id = reader.GetGuid(0),
                            UserId = reader.GetGuid(1),
                            Name = reader.GetString(2),
                            Type = reader.GetString(3),
                            IsActive = reader.GetBoolean(4),
                            CreatedAt = reader.GetDateTime(5)
                        };
                    }
                }
            }
            return null;
        }

        public void Update(TrainingProgram program)
        {
            const string sql = @"UPDATE Programs 
                                  SET Name = @Name, Type = @Type, IsActive = @IsActive
                                  WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = program.Id;
                command.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = program.Name;
                command.Parameters.Add("@Type", SqlDbType.NVarChar, 50).Value = program.Type;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = program.IsActive;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(Guid id)
        {
            const string sql = "DELETE FROM Programs WHERE Id = @Id";

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
