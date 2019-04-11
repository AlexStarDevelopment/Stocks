using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WealthManager.BL
{
    public static class CLogin
    {
        public static Guid UserLoggedIn { get; set; }

        public static void set(Guid id)
        {
            UserLoggedIn = id;
        }
    }
}
