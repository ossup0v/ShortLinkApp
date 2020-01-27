using App.API;
using App.Context;
using App.LinkStorage.API;
using System;
using System.Collections.Generic;

namespace App.Core
{
    //TODO logging
    public class ShortLinkService : IShortLinkService
    {
        private ILinkStorage _storage;

        public ShortLinkService(ILinkStorage storage)
        {
            _storage = storage;
        }

        public string CreateShortLink(string fullLink)
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

        public IList<string> FindAllShortLinks()
        {
            return new List<string> { "", "link1", "link2" };
        }

        private string CreateShortLink(string fullLink, UserContext context)
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
