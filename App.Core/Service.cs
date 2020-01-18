using App.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core
{
    public class Service : IService
    {
        private IStorage _storage;

        public Service(IStorage storage)
        {
            _storage = storage;
        }
            
        public bool CreateShortLink(string fullLink)
        {
            throw new NotImplementedException();
        }

        public string FindFullLink(string shortLink)
        {
            throw new NotImplementedException();
        }
    }
}
