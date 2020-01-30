using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Main.API
{
    public class User
    {
        public Guid Id { get; set; }
        public Credentials Credentials { get; set; }
        public Permissions Permitions { get; set; } = Permissions.UnregisteredUser;
        public DateTime Created { get; set; }
    }
}
