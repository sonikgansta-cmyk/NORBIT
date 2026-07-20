using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbSport.Repositories.Ef
{
    public class TrainingProgramEfRepository
    {
        public void Create(Programs program)
        {
            using (var context = new SportEntities())
            {
                context.Programs.Add(program);
                context.SaveChanges();
            }
        }

        public List<Programs> GetAll()
        {
            using (var context = new SportEntities())
            {
                return context.Programs.ToList();
            }
        }

        public Programs GetById(Guid id)
        {
            using (var context = new SportEntities())
            {
                return context.Programs.FirstOrDefault(p => p.Id == id);
            }
        }

        public void Update(Programs program)
        {
            using (var context = new SportEntities())
            {
                var existing = context.Programs.FirstOrDefault(p => p.Id == program.Id);
                if (existing == null) return;

                existing.Name = program.Name;
                existing.Type = program.Type;
                existing.IsActive = program.IsActive;

                context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            using (var context = new SportEntities())
            {
                var existing = context.Programs.FirstOrDefault(p => p.Id == id);
                if (existing == null) return;

                context.Programs.Remove(existing);
                context.SaveChanges();
            }
        }
    }
}
