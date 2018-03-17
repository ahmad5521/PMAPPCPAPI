using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PMAPP.Models
{
    public static class DataStore
    {
        public static string userName;
        public static string password;
        public static string name;
        public static int userID;
        public static string modirate;
        public static string regon;
        public static string role;
        
        public static void setUserName(string v)
        {
            userName = v;
        }

        public static string getUserName()
        {
            return userName;
        }

        public static void setPassword(string v)
        {
            password = v;
        }

        public static string getPassword()
        {
            return password;
        }

        public static void setName(string v)
        {
            name = v;
        }

        public static string getName()
        {
            return name;
        }

        public static void setUserID(int v)
        {
            userID = v;
        }

        public static int getUserID()
        {
            return userID;
        }

        public static void setModirate(string v)
        {
            modirate = v;
        }

        public static string getModirate()
        {
            return modirate;
        }
        
        public static void setRegon(string v)
        {
            regon = v;
        }

        public static string getRegon()
        {
            return regon;
        }

        public static void setRole(string v)
        {
            role = v;
        }

        public static string getRole()
        {
            return role;
        }
    }
}
