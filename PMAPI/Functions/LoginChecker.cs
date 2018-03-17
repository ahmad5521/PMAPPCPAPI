using PMAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMAPI.Functions
{
    public class LoginChecker
    {
        public static bool Login(string userName, string password/*, string deviceID*/)
        {
            using (DATA db = new DATA())
            {
                return db.Users.Any(u => u.userName == userName && u.Password == password /*&& u.deviceID == deviceID*/);
            }
        }
    }
}