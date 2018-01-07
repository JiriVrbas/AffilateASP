using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var db = new DbLayer.Database())
            {
                var users = db.Users;
                var affilatePartners = db.AffilatePartners;
                var userPartners = db.UsersPartners;
                var cashouts = db.Cashouts;

                db.Users.Add(new Models.User() {
                    UserId =1,
                    Balance = 0,
                    Email = "vrbasji@gmail.com",
                    IsConfirmed = true,
                    Link = "abcdef",
                    Password = "heslo"
                });
                db.SaveChanges();
            }
        }
    }
}
