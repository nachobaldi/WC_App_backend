using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbUtils
{
    public static class SeedData
    {
        public static void SeedUsers(DataContext dataContext)
        {
            if (dataContext.Users.Any())
            {
                return;
            }
            var user = new User(123456789, "admin", "admin", "admin", "admin@gmail.com", "0501112222", 1, 1);
            dataContext.Users.Add(user);
            dataContext.SaveChanges();
        }
        public static void SeedStatuses(DataContext dataContext)
        {
            if (dataContext.Statuses.Any())
            {
                return;
            }
            var status1 = new Status("active");
            var status2 = new Status("not active");

            dataContext.Statuses.Add(status1);
            dataContext.Statuses.Add(status2);

            dataContext.SaveChanges();
        }
        public static void SeedUserTypes(DataContext dataContext)
        {
            if (dataContext.UserTypes.Any())
            {
                return;
            }
            var type1 = new UserType("admin");
            var type2 = new UserType("mentor");
            var type3 = new UserType("student");


            dataContext.UserTypes.Add(type1);
            dataContext.UserTypes.Add(type2);
            dataContext.UserTypes.Add(type3);


            dataContext.SaveChanges();
        }
    }
}
