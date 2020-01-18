using App.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Storage
{
    public class LinkStorage : IStorage
    {
        public Guid Create()
        {
            throw new NotImplementedException();
        }

        public IEntry Read(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid id, IEntryConfig config)
        {
            throw new NotImplementedException();
        }
    }
}
