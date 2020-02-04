using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Libmongocrypt;
using System.Linq;
using MongoDB.Bson.Serialization;
using AppCore.LinkStorage.API;
using System;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AppCore.LinkStorage.Core
{
    //ToDo: logging
    public class LinkStorage : ILinkStorage
    {
        private const string _URLDatabaseConnection = "mongodb://localhost:27017";
        private const string _databaseName = "ShortLinkApp";
        private MongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<Entry> _checkCollection;

        private IMongoCollection<Entry> _collection
        {
            get
            {
                if (_checkCollection != null)
                    return _checkCollection;
                else
                    return _database.GetCollection<Entry>(_databaseName);
            }

            set
            { _checkCollection = value; }
        }

        public void Create(Entry entry)
        {
            _collection.InsertOne(entry);
        }

        public IList<Entry> Read()
        {
            var entries = _collection.Find(new BsonDocument()).ToList();
            return entries;
        }

        public IList<Entry> Read(Field field, string value)
        {
            if (value == null)
                return null;
            var filter = CreateEqFilter(field, value);
            var entries = _collection.Find(filter).ToList();
            return entries;
        }

        public void Update(string id, Entry config)
        {
            var filter = CreateEqFilter(Field.EntryId, id);
            var update = CreateSetUpdate(Field.Uri, config.Uri);
            _collection.UpdateOne(filter, update);
        }

        public void Update(Field filterField, object filterValue, Field updateField, object updateValue)
        {
            var filter = CreateEqFilter(filterField, filterValue);
            var update = CreateSetUpdate(updateField, updateValue);
            _collection.UpdateOne(filter, update);
        }

        public bool Remove()
        {
            throw new NotImplementedException();
        }

        public bool Remove(Field Field, string value)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(Entry entry)
        {
            await _collection.InsertOneAsync(entry);
        }

        public async Task<IList<Entry>> ReadAsync()
        {
            var entries = (await _collection.FindAsync(new BsonDocument())).ToList();
            return entries;
        }

        public async Task<IList<Entry>> ReadAsync(Field field, string value)
        {
            if (value == null)
                return null;
            var filter = CreateEqFilter(field, value);
            var entries = (await _collection.FindAsync(filter)).ToList();
            return entries;
        }

        public async Task UpdateAsync(string id, Entry config)
        {
            var filter = CreateEqFilter(Field.EntryId, id);
            var update = CreateSetUpdate(Field.Uri, config.Uri);
            await _collection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateAsync(Field filterField, object filterValue, Field updateField, object updateValue)
        {
            var filter = CreateEqFilter(filterField, filterValue);
            var update = CreateSetUpdate(updateField, updateValue);
            await _collection.UpdateOneAsync(filter, update);
        }

        public LinkStorage()
        {
            Configure();
        }

        private UpdateDefinition<Entry> CreateSetUpdate(Field Field, object value)
        {
            switch (Field)
            {
                case Field.EntryId:
                    return Builders<Entry>.Update.Set(x => x.Id, value.ToString());
                case Field.Uri:
                    return Builders<Entry>.Update.Set(x => x.Uri, (StorageURI)value);
                case Field.UriFullURI:
                    return Builders<Entry>.Update.Set(x => x.Uri.FullURI, value.ToString());
                case Field.UriShortURI:
                    return Builders<Entry>.Update.Set(x => x.Uri.ShortURI, value.ToString());
                case Field.UriToken:
                    return Builders<Entry>.Update.Set(x => x.Uri.Token, value.ToString());
                case Field.UriClicked:
                    return Builders<Entry>.Update.Set(x => x.Uri.Clicked, (int)value);
                case Field.UriCreated:
                    return Builders<Entry>.Update.Set(x => x.Uri.Created, (DateTime)value);
                case Field.UriCreater:
                    return Builders<Entry>.Update.Set(x => x.Uri.Creater, value.ToString());
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        private FilterDefinition<Entry> CreateEqFilter(Field Field, object value)
        {
            switch (Field)
            {
                case Field.EntryId:
                    return Builders<Entry>.Filter.Eq(x => x.Id, value.ToString());
                case Field.Uri:
                    return Builders<Entry>.Filter.Eq(x => x.Uri, (StorageURI)value);
                case Field.UriFullURI:
                    return Builders<Entry>.Filter.Eq(x => x.Uri.FullURI, value.ToString());
                case Field.UriShortURI:
                    return Builders<Entry>.Filter.Eq(x => x.Uri.ShortURI, value.ToString());
                case Field.UriToken:
                    return Builders<Entry>.Filter.Eq(x => x.Uri.Token, value.ToString());
                case Field.UriClicked:
                    return Builders<Entry>.Filter.Eq(x => x.Uri.Clicked, (int)value);
                case Field.UriCreated:
                    return Builders<Entry>.Filter.Eq(x => x.Uri.Created, (DateTime)value);
                case Field.UriCreater:
                    return Builders<Entry>.Filter.Eq(x => x.Uri.Creater, value.ToString());
                default:
                    throw new InvalidEnumArgumentException();
            }

        }

        private void Configure()
        {
            try
            {
                _client = new MongoClient(_URLDatabaseConnection);
                _database = _client.GetDatabase(_databaseName);
                BsonClassMap.RegisterClassMap<Entry>(cm =>
                {
                    cm.AutoMap();
                });
            }
            catch
            {
                
            };
        }
    }
}
