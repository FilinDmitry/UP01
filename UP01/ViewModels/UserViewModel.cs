using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UP01.Models;

namespace UP01.ViewModels
{
    public class UserViewModel
    {
        public Users user;
        public List<string> roles_ = new List<string>()
        {
            "Читатель", "Писатель", "Администратор"
        };
        public List<string> roles => roles_;

        public string new_role_;
        public string role { get { return user.Roles.Name; } set { new_role_ = value; } }
        
        public string Name { get { return user.Name; } set { user.Name = value; } }
        public string Login { get { return user.Login; } set { user.Login = value; } }
        public string Password { get { return user.Password; } set { user.Password = value; } }
    }
}
