using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbSport.Repositories.Ef
{
    public class ExerciseEfRepository
    {
        public void Create(Exercises exercise)
        {
            using (var context = new SportEntities())
            {
                context.Exercises.Add(exercise);
                context.SaveChanges();
            }
        }

        public List<Exercises> GetAll()
        {
            using (var context = new SportEntities())
            {
                return context.Exercises.ToList();
            }
        }

        public Exercises GetById(Guid id)
        {
            using (var context = new SportEntities())
            {
                return context.Exercises.FirstOrDefault(e => e.Id == id);
            }
        }

        public void Update(Exercises exercise)
        {
            using (var context = new SportEntities())
            {
                var existing = context.Exercises.FirstOrDefault(e => e.Id == exercise.Id);
                if (existing == null) return;

                existing.Name = exercise.Name;
                existing.IsActive = exercise.IsActive;

                context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            using (var context = new SportEntities())
            {
                var existing = context.Exercises.FirstOrDefault(e => e.Id == id);
                if (existing == null) return;

                context.Exercises.Remove(existing);
                context.SaveChanges();
            }
        }
    }
}
