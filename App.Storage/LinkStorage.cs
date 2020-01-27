using App.Storage.API;
using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Libmongocrypt;
using System.Linq;

namespace App.Storage
{
    //TODO logging
    public class LinkStorage : IStorage
    {
        private const string _URLDatabaseConnection = "mongodb://localhost:27017";
        private const string _databaseName = "test";
        private MongoClient _client;
        private IMongoDatabase _database;

        public Guid Create(IEntryConfig config)
        {
            throw new NotImplementedException();
        }

        public IEntry Read(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<IEntry> Read(IList<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IList<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid id, IEntryConfig config)
        {
            throw new NotImplementedException();
        }

        public LinkStorage()
        {
            Configure();
        }

        private void Configure()
        {
            _client = new MongoClient(_URLDatabaseConnection);
            _database = _client.GetDatabase(_databaseName);
        }
    }
}
