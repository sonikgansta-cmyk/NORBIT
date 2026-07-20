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
    public class MuscleGroupAdoRepository
    {
        private readonly string _connectionString;

        public MuscleGroupAdoRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["SportDbConnection"].ConnectionString;
        }

        public void Create(MuscleGroup group)
        {
            const string sql = "INSERT INTO MuscleGroups (Id, Name) VALUES (@Id, @Name)";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = group.Id;
                command.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = group.Name;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<MuscleGroup> GetAll()
        {
            var groups = new List<MuscleGroup>();
            const string sql = "SELECT Id, Name FROM MuscleGroups";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        groups.Add(new MuscleGroup
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
            }
            return groups;
        }

        public MuscleGroup GetById(Guid id)
        {
            const string sql = "SELECT Id, Name FROM MuscleGroups WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new MuscleGroup
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1)
                        };
                    }
                }
            }
            return null;
        }

        public void Update(MuscleGroup group)
        {
            const string sql = "UPDATE MuscleGroups SET Name = @Name WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = group.Id;
                command.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = group.Name;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(Guid id)
        {
            const string sql = "DELETE FROM MuscleGroups WHERE Id = @Id";

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
