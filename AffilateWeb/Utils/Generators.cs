using DbLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AffilateWeb.Utils
{
    public static class Generators
    {
        private static Random random = new Random();
        public static string GetRandomUniqueLink(int lenght)
        {
            string randomString = string.Empty;
            using (var db = new Database())
            {
                bool isNotUniqe = true;
                while (isNotUniqe)
                {
                    randomString = RandomString(lenght);
                    isNotUniqe = db.Users.Any(x => x.Link == randomString);
                }
            }
            return randomString;
        }
        public static string GetRandomConfirmationLink(string serverPath)
        {
            string randomString = string.Empty;
            using (var db = new Database())
            {
                bool isNotUniqe = true;
                while (isNotUniqe)
                {
                    randomString = RandomString(12);
                    isNotUniqe = db.Users.Any(x => x.ConfirmationLink.EndsWith(randomString));
                }
            }
            return serverPath + "/Confirmation?clink=" + randomString;
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}