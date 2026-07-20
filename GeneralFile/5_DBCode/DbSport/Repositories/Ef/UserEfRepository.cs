using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbSport.Repositories.Ef
{
    public class UserEfRepository
    {
        public void Create(Users user)
        {
            using (var context = new SportEntities())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public List<Users> GetAll()
        {
            using (var context = new SportEntities())
            {
                return context.Users.ToList();
            }
        }

        public Users GetById(Guid id)
        {
            using (var context = new SportEntities())
            {
                return context.Users.FirstOrDefault(u => u.Id == id);
            }
        }

        public void Update(Users user)
        {
            using (var context = new SportEntities())
            {
                var existing = context.Users.FirstOrDefault(u => u.Id == user.Id);
                if (existing == null) return;

                existing.Name = user.Name;
                existing.Email = user.Email;
                existing.IsActive = user.IsActive;

                context.SaveChanges();
            }
        }

        public void Delete(Guid id)
        {
            using (var context = new SportEntities())
            {
                var existing = context.Users.FirstOrDefault(u => u.Id == id);
                if (existing == null) return;

                context.Users.Remove(existing);
                context.SaveChanges();
            }
        }
    }
}
