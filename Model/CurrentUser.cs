using FlatRental.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatRental.Model
{
    public static class CurrentUser
    {
        private static User instance;
        private static object syncRoot = new Object();

        public static User GetInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new User();
                }
            }
            return instance;
        }
        public static void SetInstance(User user)
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = user;
                }
            }
        }
    }
}
