using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Libmongocrypt;
using System.Linq;
using MongoDB.Bson.Serialization;
using AppCore.LinkStorage.API;
using System;
using System.Threading.Tasks;

namespace AppCore.LinkStorage.Core
{
    //ToDo: logging
    public class LinkStorage : ILinkStorage
    {
        private const string _URLDatabaseConnection = "mongodb://localhost:27017";
        private const string _databaseName = "ShortLinkApp";
        private readonly string[] _filterString = new string[] { "Id", "Uri.FullURL", "Uri.ShortURL", "Uri.Token", "Uri.Clicked", "Uri.Created", "Uri.Creater" };
        private MongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<IEntry> _checkCollection;

        private IMongoCollection<IEntry> _collection
        {
            get
            {
                if (_checkCollection != null)
                    return _checkCollection;
                else
                    return _database.GetCollection<IEntry>(_databaseName);
            }

            set
            { _checkCollection = value; }
        }

        public void Create(IEntry entry)
        {
            _collection.InsertOne(entry);
        }

        public IList<IEntry> Read()
        {
            var entries = _collection.Find(new BsonDocument()).ToList();
            return entries;
        }

        public IList<IEntry> Read(FilterBy field, string value)
        {
            if (value == null)
                return null;
            var filter = Builders<IEntry>.Filter.AnyEq<BsonValue>(_filterString[(int)field], value);
            var entries = _collection.Find(filter).ToList();
            return entries;
        }

        public void Update(string id, IEntry config)
        {
            var filter = Builders<IEntry>.Filter.Eq("Id", id);
            var update = Builders<IEntry>.Update.Set("Uri", config.Uri);
            _collection.UpdateOne(filter, update);
        }

        public void Update(string token, int timesClicked)
        {
            var filter = Builders<IEntry>.Filter.Eq("Uri.Token", token);
            var update = Builders<IEntry>.Update.Set("Uri.Clicked", timesClicked);
            _collection.UpdateOne(filter, update);
        }

        public bool Remove()
        {
            throw new NotImplementedException();
        }

        public bool Remove(FilterBy filterBy, string value)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(IEntry entry)
        {
            await _collection.InsertOneAsync(entry);
        }

        public async Task<IList<IEntry>> ReadAsync()
        {
            var entries = (await _collection.FindAsync(new BsonDocument())).ToList();
            return entries;
        }

        public async Task<IList<IEntry>> ReadAsync(FilterBy field, string value)
        {
            if (value == null)
                return null;
            var filter = Builders<IEntry>.Filter.AnyEq<BsonValue>(_filterString[(int)field], value);
            var entries = (await _collection.FindAsync(filter)).ToList();
            return entries;
        }

        public async Task UpdateAsync(string id, IEntry config)
        {
            var filter = Builders<IEntry>.Filter.Eq("Id", id);
            var update = Builders<IEntry>.Update.Set("Uri", config.Uri);
            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateAsync(string token, int timesClicked)
        {
            var filter = Builders<IEntry>.Filter.Eq("Uri.Token", token);
            var update = Builders<IEntry>.Update.Set("Uri.Clicked", timesClicked);
            await _collection.UpdateOneAsync(filter, update);
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
