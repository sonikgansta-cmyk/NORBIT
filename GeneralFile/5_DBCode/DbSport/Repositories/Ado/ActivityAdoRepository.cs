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
    public class ActivityAdoRepository
    {
        private readonly string _connectionString;

        public ActivityAdoRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["SportDbConnection"].ConnectionString;
        }

        public void Create(Activity activity)
        {
            const string sql = @"INSERT INTO Activities (Id, ExerciseId, ActivityDate, DurationMinutes, Notes, CaloriesBurned, CreatedAt)
                                  VALUES (@Id, @ExerciseId, @ActivityDate, @DurationMinutes, @Notes, @CaloriesBurned, @CreatedAt)";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = activity.Id;
                command.Parameters.Add("@ExerciseId", SqlDbType.UniqueIdentifier).Value = activity.ExerciseId;
                command.Parameters.Add("@ActivityDate", SqlDbType.Date).Value = activity.ActivityDate;
                command.Parameters.Add("@DurationMinutes", SqlDbType.Int).Value = activity.DurationMinutes;
                command.Parameters.Add("@Notes", SqlDbType.NVarChar, 200).Value = (object)activity.Notes ?? DBNull.Value;
                command.Parameters.Add("@CaloriesBurned", SqlDbType.Decimal).Value = (object)activity.CaloriesBurned ?? DBNull.Value;
                command.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value = activity.CreatedAt;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<Activity> GetAll()
        {
            var activities = new List<Activity>();
            const string sql = "SELECT Id, ExerciseId, ActivityDate, DurationMinutes, Notes, CaloriesBurned, CreatedAt FROM Activities";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        activities.Add(new Activity
                        {
                            Id = reader.GetGuid(0),
                            ExerciseId = reader.GetGuid(1),
                            ActivityDate = reader.GetDateTime(2),
                            DurationMinutes = reader.GetInt32(3),
                            Notes = reader.IsDBNull(4) ? null : reader.GetString(4),
                            CaloriesBurned = reader.IsDBNull(5) ? (decimal?)null : reader.GetDecimal(5),
                            CreatedAt = reader.GetDateTime(6)
                        });
                    }
                }
            }
            return activities;
        }

        public Activity GetById(Guid id)
        {
            const string sql = "SELECT Id, ExerciseId, ActivityDate, DurationMinutes, Notes, CaloriesBurned, CreatedAt FROM Activities WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = id;

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Activity
                        {
                            Id = reader.GetGuid(0),
                            ExerciseId = reader.GetGuid(1),
                            ActivityDate = reader.GetDateTime(2),
                            DurationMinutes = reader.GetInt32(3),
                            Notes = reader.IsDBNull(4) ? null : reader.GetString(4),
                            CaloriesBurned = reader.IsDBNull(5) ? (decimal?)null : reader.GetDecimal(5),
                            CreatedAt = reader.GetDateTime(6)
                        };
                    }
                }
            }
            return null;
        }

        public void Update(Activity activity)
        {
            const string sql = @"UPDATE Activities 
                                  SET ActivityDate = @ActivityDate, DurationMinutes = @DurationMinutes, 
                                      Notes = @Notes, CaloriesBurned = @CaloriesBurned
                                  WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@Id", SqlDbType.UniqueIdentifier).Value = activity.Id;
                command.Parameters.Add("@ActivityDate", SqlDbType.Date).Value = activity.ActivityDate;
                command.Parameters.Add("@DurationMinutes", SqlDbType.Int).Value = activity.DurationMinutes;
                command.Parameters.Add("@Notes", SqlDbType.NVarChar, 200).Value = (object)activity.Notes ?? DBNull.Value;
                command.Parameters.Add("@CaloriesBurned", SqlDbType.Decimal).Value = (object)activity.CaloriesBurned ?? DBNull.Value;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(Guid id)
        {
            const string sql = "DELETE FROM Activities WHERE Id = @Id";

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
