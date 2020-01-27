using App.LinkStorage.API;
using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Libmongocrypt;
using System.Linq;
using App.Storage.API;
using MongoDB.Bson.Serialization;
using System.ComponentModel;

namespace App.LinkStorage
{
    //TODO logging
    public class LinkStorage : ILinkStorage
    {
        private const string _URLDatabaseConnection = "mongodb://localhost:27017";
        private const string _databaseName = "test";
        private readonly string[] _filterString = new string[] { "id", "fullURL", "shortURL", "token", "clicked", "created", "creater"};
        private MongoClient _client;
        private IMongoDatabase _database;

        private IMongoCollection<BsonDocument> _collection { 
            get 
            { 
                if (_collection != null) 
                    return _collection; 
                else return _database.GetCollection<BsonDocument>(_databaseName); 
            } 

            set 
            { _collection = value; } 
        }

        public Guid Create(IEntryConfig config)
        {
            var document = config.ToBsonDocument();
            _collection.InsertOne(document);
            return new Guid(document.ToBson());
        }

        public IList<IEntry> Read()
        {
            var docs = new List<IEntry>();
            var documents = _collection.Find(new BsonDocument()).ToList();
            foreach (var document in documents)
            {
                docs.Add(BsonSerializer.Deserialize<Entry>(document));
            }
            return docs;
        }

        public IList<IEntry> Read(FilterBy field, string value)
        {
            var docs = new List<IEntry>();
            var filter = Builders<BsonDocument>.Filter.ElemMatch<BsonValue>(_filterString[(int)field], value);
            var documents = _collection.Find(filter).ToList();
            foreach (var document in documents)
            {
                docs.Add(BsonSerializer.Deserialize<Entry>(document));
            }
            return docs;
        }

        public bool Remove()
        {
            throw new NotImplementedException();
        }

        public bool Remove(FilterBy filterBy, string value)
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
