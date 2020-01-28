using App.Storage.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.LinkStorage.API
{
    public interface IEntry
    {
        string Id { get; set; }

        StorageURI Uri { get; set; }
    }
}
