using App.API;
using App.Context;
using App.LinkStorage.API;
using App.Storage.API;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Core
{
    //ToDo: logging
    public class ShortLinkService : IShortLinkService
    {
        private ILinkStorage _storage;
        private static string _tokenDefault = "";
        private string _token = _tokenDefault;

        public ShortLinkService(ILinkStorage storage)
        {
            _storage = storage;
        }

        public string CreateShortLink(ServiceURI serviceUri)
        {
            //var ctx = UserContext.GetContext();
            GenerateShortLink(serviceUri);
            //if (ctx != default(UserContext))
            //{
            //    return CreateShortLink(serviceUri, ctx);
            //}
            var storageUri = ToStorageURI(serviceUri);
            var entry = new Entry { Uri = storageUri, Id = storageUri.Token };
            _storage.Create(entry);
            return serviceUri.ShortURI;
        }

        //ToDo: use usercontext
        private string CreateShortLink(ServiceURI serviceUri, UserContext context)
        {
            var storageUri = ToStorageURI(serviceUri);
            var entry = new Entry { Uri = storageUri, Id = storageUri.Token };
            _storage.Create(entry);
            return serviceUri.ShortURI;
        }

        public string FindFullLink(ServiceURI serviceUri)
        {
            var ctx = UserContext.GetContext();
            //if (ctx != default(UserContext))
            //{
            //    return FindFullLink(serviceUri, ctx);
            //}
            var entry = _storage.Read(FilterBy.Id, serviceUri.Token).FirstOrDefault();
            if (entry == null)
            {
                return null;
            }
            _storage.Update(serviceUri.Token, entry.Uri.Clicked + 1);
            var storageUri = entry?.Uri;
            var serviceResultUri = ToServiceURI(storageUri);
            var fullLink = serviceResultUri.FullURI;
            return fullLink;
        }

        //ToDo: use usercontext
        private string FindFullLink(ServiceURI serviceUri, UserContext context)
        {
            var entry = _storage.Read(FilterBy.Id, serviceUri.Token).FirstOrDefault();
            var serviceResultUri = ToServiceURI(entry.Uri);
            var fullLink = serviceResultUri.FullURI;
            return fullLink;
        }

        public IList<(string, int)> FindAllShortLinks()
        {
            var entries = _storage.Read();
            var result = new List<(string, int)>();
            foreach (var entry in entries)
            {
                var storageUri = entry.Uri;
                result.Add((storageUri.ShortURI, storageUri.Clicked));
            }
            return result;
        }

        //ToDo: only admin have access to remove 
        public void Remove()
        {
            _storage.Remove();
        }

        public void Remove(FilterBy filter, string value)
        {
            _storage.Remove(filter, value);
        }

        private List<string> FindAllTokens()
        {
            var entries = _storage.Read();
            var tokens = new List<string>();
            foreach (var entry in entries)
            {
                tokens.Add(entry.Uri.Token);
            }
            return tokens;
        }

        private void GenerateShortLink(ServiceURI serviceUri)
        {
            var tokens = FindAllTokens();
            while (tokens.Exists(t => t == _token) || (_token == _tokenDefault))
            {
                GenerateToken();
            }
            serviceUri.Token = _token;
            serviceUri.ShortURI = new ServiceURI().Config.BASE_URI + _token;
        }

        private void GenerateToken()
        {
            string urlsafe = string.Empty;
            Enumerable.Range(10, 8)
              .OrderBy(o => new Random().Next(9, 25))
              .ToList()
              .ForEach(i => urlsafe += i.ToString());
            var temp = Convert.ToInt64(urlsafe, 16).ToString();
            for (int i = temp.Length; i < 25; i++)
            {
                temp += new Random().Next(0, 9);
            }
            _token = temp.ToString().Substring(0, 24);
        }

        private StorageURI ToStorageURI(ServiceURI serviceURI)
        {
            if (serviceURI != null)
                return new StorageURI
                {
                    Id = serviceURI.Id,
                    FullURI = serviceURI?.FullURI,
                    ShortURI = serviceURI?.ShortURI,
                    Token = serviceURI?.Token,
                    Clicked = serviceURI.Clicked,
                    Created = serviceURI.Created,
                    Creater = serviceURI?.Creater?.Credentials?.Login
                };
            else
                return new StorageURI();
        }

        private ServiceURI ToServiceURI(StorageURI storageURI)
        {
            if (storageURI != null)
                return new ServiceURI
                {
                    Id = storageURI.Id,
                    FullURI = storageURI?.FullURI,
                    ShortURI = storageURI?.ShortURI,
                    Token = storageURI?.Token,
                    Clicked = storageURI.Clicked,
                    Created = storageURI.Created,
                    Creater = new User { Credentials = new Credentials { Login = storageURI.Creater } }
                };
            else 
                return new ServiceURI();
        }
    }
}
