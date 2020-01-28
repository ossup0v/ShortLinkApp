using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.API
{
    public class ServiceURI
    {
        public Guid Id { get; set; }
        public string FullURI { get; set; }
        public string ShortURI { get; set; }
        public string Token { get; set; }
        public int Clicked { get; set; } = 0;
        public DateTime Created { get; set; }
        public User Creater { get; set; }
        public UriConfig Config { get; set; } = new UriConfig();
    }
}
