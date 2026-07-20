using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbSport.Repositories.Ef
{
    public class ActivityEfRepository
    {
        public void Create(Activities activity)
        {
            using (var context = new SportEntities())
            {
                context.Activities.Add(activity);
                context.SaveChanges();
            }
        }

        public List<Activities> GetAll()
        {
            using (var context = new SportEntities())
            {
                return context.Activities.ToList();
            }
        }

        public Activities GetById(Guid id)
        {
            using (var context = new SportEntities())
            {
                return context.Activities.FirstOrDefault(a => a.Id == id);
            }
        }

        public void Update(Activities activity)
        {
            using (var context = new SportEntities())
            {
                var existing = context.Activities.FirstOrDefault(a => a.Id == activity.Id);
                if (existing == null) return;

                existing.ActivityDate = activity.ActivityDate;
                existing.DurationMinutes = activity.DurationMinutes;
                existing.Notes = activity.Notes;
                existing.CaloriesBurned = activity.CaloriesBurned;

                context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            using (var context = new SportEntities())
            {
                var existing = context.Activities.FirstOrDefault(a => a.Id == id);
                if (existing == null) return;

                context.Activities.Remove(existing);
                context.SaveChanges();
            }
        }
    }
}
