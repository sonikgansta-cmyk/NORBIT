using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbSport.Repositories.Ef
{
    public class MuscleGroupEfRepository
    {
        public void Create(MuscleGroups group)
        {
            using (var context = new SportEntities())
            {
                context.MuscleGroups.Add(group);
                context.SaveChanges();
            }
        }

        public List<MuscleGroups> GetAll()
        {
            using (var context = new SportEntities())
            {
                return context.MuscleGroups.ToList();
            }
        }

        public MuscleGroups GetById(Guid id)
        {
            using (var context = new SportEntities())
            {
                return context.MuscleGroups.FirstOrDefault(m => m.Id == id);
            }
        }

        public void Update(MuscleGroups group)
        {
            using (var context = new SportEntities())
            {
                var existing = context.MuscleGroups.FirstOrDefault(m => m.Id == group.Id);
                if (existing == null) return;

                existing.Name = group.Name;

                context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            using (var context = new SportEntities())
            {
                var existing = context.MuscleGroups.FirstOrDefault(m => m.Id == id);
                if (existing == null) return;

                context.MuscleGroups.Remove(existing);
                context.SaveChanges();
            }
        }
    }
}
