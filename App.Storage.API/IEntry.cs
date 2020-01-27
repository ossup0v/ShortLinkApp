using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.LinkStorage.API
{
    public interface IEntry
    {
        Guid Id { get; set; }

        object Data { get; set; }
    }
}
