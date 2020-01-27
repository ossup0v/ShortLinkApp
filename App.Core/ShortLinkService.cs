using App.API;
using App.Context;
using App.Storage.API;
using System;

namespace App.Core
{
    //TODO logging
    public class ShortLinkService : IShortLinkService
    {
        private IStorage _storage;

        public ShortLinkService(IStorage storage)
        {
            _storage = storage;
        }

        public bool CreateShortLink(string fullLink)
        {
            var ctx = UserContext.GetContext();
            if (ctx != default(UserContext))
            {
                return CreateShortLink(fullLink, ctx);
            }
            throw new NotImplementedException();
        }

        public string FindFullLink(string shortLink)
        {
            var ctx = UserContext.GetContext();
            if (ctx != default(UserContext))
            {
                return FindFullLink(shortLink, ctx);
            }
            throw new NotImplementedException();
        }

        private bool CreateShortLink(string fullLink, UserContext context)
        {
            throw new NotImplementedException();
        }

        private string FindFullLink(string shortLink, UserContext context)
        {
            throw new NotImplementedException();
        }

        private string CreateToken(string url)
        {
            var ctx = UserContext.GetContext();
            if (ctx != default(UserContext))
            {
                return CreateToken(url, ctx);
            }
            throw new NotImplementedException();
        }

        private string CreateToken(string url, UserContext context)
        {
            throw new NotImplementedException();
        }
    }
}
