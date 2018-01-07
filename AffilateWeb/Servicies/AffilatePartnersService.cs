using DbLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AffilateWeb.Servicies
{
    public class AffilatePartnersService
    {
        public const int NUMBER_OF_PARTNERS = 9;
        public IList<UserPartners> UserPartners { get; private set; }
        public AffilatePartnersService(string link = null)
        {
            this.UserPartners = new List<UserPartners>();
            using (var db = new Database())
            {
                var user = db.Users.FirstOrDefault(x => x.Link == link);
                if (user != null)
                {
                    var partners = db.UsersPartners
                        .Include("Partner")
                        .Where(x => x.UserId == user.UserId)
                        .Take(NUMBER_OF_PARTNERS);

                    foreach (var partner in partners)
                    {
                        this.UserPartners.Add(partner);
                    }
                }

                if (this.UserPartners.Count < NUMBER_OF_PARTNERS)
                {
                    var mainPartners = db.UsersPartners
                        .Include("Partner")
                        .Where(x => x.UserId == 1)
                        .Take(NUMBER_OF_PARTNERS - this.UserPartners.Count);

                    foreach (var partner in mainPartners)
                    {
                        this.UserPartners.Add(partner);
                    }
                }
            }
        }
    }
}