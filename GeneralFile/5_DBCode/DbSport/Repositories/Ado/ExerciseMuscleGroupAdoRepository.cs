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
    public class ExerciseMuscleGroupAdoRepository
    {
        private readonly string _connectionString;

        public ExerciseMuscleGroupAdoRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["SportDbConnection"].ConnectionString;
        }

        public void Create(ExerciseMuscleGroup link)
        {
            const string sql = @"INSERT INTO ExerciseMuscleGroups (ExerciseId, MuscleGroupId)
                                  VALUES (@ExerciseId, @MuscleGroupId)";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@ExerciseId", SqlDbType.UniqueIdentifier).Value = link.ExerciseId;
                command.Parameters.Add("@MuscleGroupId", SqlDbType.UniqueIdentifier).Value = link.MuscleGroupId;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<ExerciseMuscleGroup> GetAll()
        {
            var links = new List<ExerciseMuscleGroup>();
            const string sql = "SELECT ExerciseId, MuscleGroupId FROM ExerciseMuscleGroups";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        links.Add(new ExerciseMuscleGroup
                        {
                            ExerciseId = reader.GetGuid(0),
                            MuscleGroupId = reader.GetGuid(1)
                        });
                    }
                }
            }
            return links;
        }

        // Обновления для таблицы связи обычно не нужны — есть либо связь, либо нет.
        // Вместо Update используем удаление старой связи + создание новой при необходимости.

        public void Delete(Guid exerciseId, Guid muscleGroupId)
        {
            const string sql = @"DELETE FROM ExerciseMuscleGroups 
                                  WHERE ExerciseId = @ExerciseId AND MuscleGroupId = @MuscleGroupId";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.Add("@ExerciseId", SqlDbType.UniqueIdentifier).Value = exerciseId;
                command.Parameters.Add("@MuscleGroupId", SqlDbType.UniqueIdentifier).Value = muscleGroupId;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
