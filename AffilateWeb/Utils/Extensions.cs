using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AffilateWeb.Utils
{
    public static class Extensions
    {
        public static bool IsValidForRegistration(this User user)
        {
            return true;
        }
    }
}