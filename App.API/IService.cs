using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.API
{
    public interface IService
    {
        string FindFullLink(string shortLink);

        bool CreateShortLink(string fullLink);
    }
}
