using App.Storage.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Storage
{
    public class Entry : IEntry
    {
        public Guid Id { get; set; }
        public object Data { get; set ; }
    }
}
