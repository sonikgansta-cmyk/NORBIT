using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbSport.Repositories.Ef
{
    public class ExerciseMuscleGroupEfRepository
    {
        public void AddLink(Guid exerciseId, Guid muscleGroupId)
        {
            using (var context = new SportEntities())
            {
                var exercise = context.Exercises.FirstOrDefault(e => e.Id == exerciseId);
                var muscleGroup = context.MuscleGroups.FirstOrDefault(m => m.Id == muscleGroupId);

                if (exercise == null || muscleGroup == null) return;

                exercise.MuscleGroups.Add(muscleGroup);
                context.SaveChanges();
            }
        }

        public void RemoveLink(Guid exerciseId, Guid muscleGroupId)
        {
            using (var context = new SportEntities())
            {
                var exercise = context.Exercises.FirstOrDefault(e => e.Id == exerciseId);
                var muscleGroup = context.MuscleGroups.FirstOrDefault(m => m.Id == muscleGroupId);

                if (exercise == null || muscleGroup == null) return;

                var linked = exercise.MuscleGroups.FirstOrDefault(m => m.Id == muscleGroupId);
                if (linked != null)
                {
                    exercise.MuscleGroups.Remove(linked);
                    context.SaveChanges();
                }
            }
        }

        public void PrintAllLinks()
        {
            using (var context = new SportEntities())
            {
                var exercises = context.Exercises.ToList();
                foreach (var exercise in exercises)
                {
                    foreach (var mg in exercise.MuscleGroups)
                    {
                        Console.WriteLine($"{exercise.Name} -> {mg.Name}");
                    }
                }
            }
        }
    }
}
