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
    //ToDo: logging
    public class LinkStorage : ILinkStorage
    {
        private const string _URLDatabaseConnection = "mongodb://localhost:27017";
        private const string _databaseName = "ShortLinkApp";
        private readonly string[] _filterString = new string[] { "Id", "Uri.FullURL", "Uri.ShortURL", "Uri.Token", "Uri.Clicked", "Uri.Created", "Uri.Creater"};
        private MongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<BsonDocument> _checkCollection;

        private IMongoCollection<BsonDocument> _collection
        {
            get
            {
                if (_checkCollection != null)
                    return _checkCollection;
                else 
                    return _database.GetCollection<BsonDocument>(_databaseName);
            }

            set
            { _checkCollection = value; }
        }

        public void Create(IEntry entry)
        {
            var document = entry.ToBsonDocument();
            _collection.InsertOne(document);
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
            if (value == null)
                return null;
            var docs = new List<IEntry>();
            var filter = Builders<BsonDocument>.Filter.AnyEq<BsonValue>(_filterString[(int)field], value);
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

        public void Update(string id, IEntry config)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Id", id);
            var update = Builders<BsonDocument>.Update.Set("Uri", config.Uri);
            _collection.UpdateOne(filter, update);
        }

        public void Update(string token, int timesClicked)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Uri.Token", token);
            var update = Builders<BsonDocument>.Update.Set("Uri.Clicked", timesClicked);
            _collection.UpdateOne(filter, update);
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
