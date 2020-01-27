using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Storage.API
{
     public enum FilterBy
    {
        Id = 0,
        FullURL = 1,
        ShortURL = 2,
        Token = 3,
        Clicked = 4,
        Created = 5,
        Creater = 6
    }
}
