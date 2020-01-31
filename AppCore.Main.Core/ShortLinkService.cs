using AppCore.LinkStorage.API;
using AppCore.Main.API;
using AppCore.Main.Context;
using AppCore.Main.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppCore.Main.Core
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

        #region API

        //ToDo: use usercontext
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

        public string FindFullLink(ServiceURI serviceUri)
        {
            var ctx = UserContext.GetContext();
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

        public List<(string, int)> FindAllShortLinks()
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

        #endregion

        #region Async API

        public async Task<string> CreateShortLinkAsync(ServiceURI serviceUri)
        {
            var storageUri = ToStorageURI(serviceUri);
            GenerateShortLink(storageUri);
            var entry = new Entry { Uri = storageUri, Id = storageUri.Id };
            await _storage.CreateAsync(entry);
            return storageUri.ShortURI;
        }

        public async Task<string> FindFullLinkAsync(ServiceURI serviceUri)
        {
            var entry = (await _storage.ReadAsync(FilterBy.UriToken, serviceUri.Token))?.FirstOrDefault();
            if (entry == null)
            {   
                return null;
            }
            await _storage.UpdateAsync(serviceUri.Token, entry.Uri.Clicked + 1);
            var storageUri = entry?.Uri;
            var serviceResultUri = ToServiceURI(storageUri);
            var fullLink = serviceResultUri.FullURI;
            return fullLink;
        }

        public async Task<List<(string, int)>> FindAllShortLinksAsync()
        {
            var entries = await _storage.ReadAsync();
            var result = new List<(string, int)>();
            foreach (var entry in entries)
            {
                var storageUri = entry.Uri;
                result.Add((storageUri.ShortURI, storageUri.Clicked));
            }
            return result;
        }

        #endregion

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
            while (tokensAndIds.Exists(t => t.Item1 == _token) || (_token == _tokenDefault))
            {
                GenerateToken();
            }
            while (tokensAndIds.Exists(t => t.Item2 == _id) || (_id == _idDefault))
            {
                GenerateId();
            }
            storageUri.Token = _token;
            storageUri.Id = _id;
            storageUri.ShortURI = UriConfig.BASE_URI + _token;
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
            if (_token.Contains('+') || _token.Contains('?') || _token.Contains('=') || _token.Contains('/'))
            {
                string temp = "";
                for (int i = 0; i < _token.Length; i++)
                {
                    if (_token[i] == '+' || _token[i] == '?' || _token[i] == '=' || _token[i] == '/')
                    {
                        temp += i.ToString();
                        continue;
                    }
                    temp += _token[i];
                }
                _token = temp;
            }
        }

        private List<(string, string)> FindAllTokensAndIds()
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
