using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.API
{
    public class ServiceURL
    {
        public Guid Id { get; set; }
        public string FullURL { get; set; }
        public string ShortURL { get; set; }
        public string Token { get; set; }
        public short Clicked { get; set; } = 0;
        public DateTime Created { get; set; }
        public User Creater { get; set; }
    }
}
