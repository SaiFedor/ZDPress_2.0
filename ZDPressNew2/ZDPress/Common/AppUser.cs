using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDPress.UI.Common
{
    public class AppUser
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public AppRole Role { get; set; }


        public string UserTitle 
        {
            get { return Role != null && Role.Name == "User" ? "Пользователь" : "Администратор"; }
        }

        public static string Admin = "Admin";

        public static string User = "User";
    }
}
