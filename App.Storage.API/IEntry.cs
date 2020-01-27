using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Storage.API
{
    public interface IEntry
    {
        Guid Id { get; set; }

        object Data { get; set; }
    }
}
