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
    public class UserAdoRepository
    {
        private readonly string _connectionString;

        public UserAdoRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["SportDbConnection"].ConnectionString;
        }

        // CREATE
        public void Create(User user)
        {
            const string sql = @"INSERT INTO Users (Id, Name, Email, RegisteredAt, IsActive)
                                  VALUES (@Id, @Name, @Email, @RegisteredAt, @IsActive)";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = user.Id;
                command.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = user.Name;
                command.Parameters.Add("@Email", SqlDbType.NVarChar, 200).Value = user.Email;
                command.Parameters.Add("@RegisteredAt", SqlDbType.DateTime).Value = user.RegisteredAt;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = user.IsActive;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // READ ALL
        public List<User> GetAll()
        {
            var users = new List<User>();
            const string sql = "SELECT Id, Name, Email, RegisteredAt, IsActive FROM Users";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1),
                            Email = reader.GetString(2),
                            RegisteredAt = reader.GetDateTime(3),
                            IsActive = reader.GetBoolean(4)
                        });
                    }
                }
            }
            return users;
        }

        // READ BY ID
        public User GetById(Guid id)
        {
            const string sql = "SELECT Id, Name, Email, RegisteredAt, IsActive FROM Users WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1),
                            Email = reader.GetString(2),
                            RegisteredAt = reader.GetDateTime(3),
                            IsActive = reader.GetBoolean(4)
                        };
                    }
                }
            }
            return null;
        }

        // UPDATE
        public void Update(User user)
        {
            const string sql = @"UPDATE Users 
                                  SET Name = @Name, Email = @Email, IsActive = @IsActive
                                  WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = user.Id;
                command.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = user.Name;
                command.Parameters.Add("@Email", SqlDbType.NVarChar, 200).Value = user.Email;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = user.IsActive;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // DELETE
        public void Delete(Guid id)
        {
            const string sql = "DELETE FROM Users WHERE Id = @Id";

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
