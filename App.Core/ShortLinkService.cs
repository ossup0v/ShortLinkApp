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
        private static string _idDefault = "";
        private string _token = _tokenDefault;
        private string _id = _idDefault;

        public ShortLinkService(ILinkStorage storage)
        {
            _storage = storage;
        }

        public string CreateShortLink(ServiceURI serviceUri)
        {
            //var ctx = UserContext.GetContext();
            var storageUri = ToStorageURI(serviceUri);
            GenerateShortLink(storageUri);
            //if (ctx != default(UserContext))
            //{
            //    return CreateShortLink(serviceUri, ctx);
            //}
            var entry = new Entry { Uri = storageUri, Id = storageUri.Id };
            _storage.Create(entry);
            return storageUri.ShortURI;
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
            var entry = _storage.Read(FilterBy.UriToken, serviceUri.Token)?.FirstOrDefault();
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
            var entry = _storage.Read(FilterBy.UriToken, serviceUri.Token).FirstOrDefault();
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

        private void GenerateShortLink(StorageURI storageUri)
        {
            var tokensAndIds = FindAllTokensAndIds();
            while (tokensAndIds.Exists(t => t.Item1 == _token ) || (_token == _tokenDefault))
            {
                GenerateToken();
            } 
            while (tokensAndIds.Exists(t => t.Item2 == _id) || (_id == _idDefault))
            {
                GenerateId();
            }
            storageUri.Token = _token;
            storageUri.Id = _id;
            storageUri.ShortURI = new ServiceURI().Config.BASE_URI + _token;
        }

        private void GenerateId()
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
            _id = temp.ToString().Substring(0, 24);
        }

        private void GenerateToken()
        {
            _token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

        private List<(string,string)> FindAllTokensAndIds()
        {
            var entries = _storage.Read();
            var tokensAndIds = new List<(string, string)>();
            foreach (var entry in entries)
            {
                tokensAndIds.Add((entry.Uri.Token, entry.Id));
            }
            return tokensAndIds;
        }

        private StorageURI ToStorageURI(ServiceURI serviceURI)
        {
            if (serviceURI != null)
                return new StorageURI
                {
                    Id = serviceURI?.Id.ToString(),
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
                    Id = new Guid(),
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
