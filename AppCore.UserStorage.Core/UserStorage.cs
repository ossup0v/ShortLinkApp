using AppCore.UserStorage;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.UserStorage.Core
{
    public class UserStorage : IUserStorage
    {
        private const string _databaseName = "UserStorage";
        private const string _URLDatabaseConnection = "mongodb://localhost:27017";
        private MongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<User> _checkCollection;

        private IMongoCollection<User> _collection
        {
            get
            {
                if (_checkCollection != null)
                    return _checkCollection;
                else
                    return _database.GetCollection<User>(_databaseName);
            }

            set
            { _checkCollection = value; }
        }

        public UserStorage()
        {
            Configure();
        }

        public void Create(User user)
        {
            _collection.InsertOne(user);
        }

        public IList<User> Read()
        {
            return _collection.Find(new BsonDocument()).ToList();
        }

        public User Read(string id)
        {
            var filter = CreateEqFilter(UserField.Id, id);
            return _collection.Find(filter).FirstOrDefault();
        }

        public IList<User> Read(UserField filterField, object filterValue)
        {
            var filter = CreateEqFilter(filterField, filterValue);
            return _collection.Find(filter).ToList();
        }

        public void Update(UserField filterField, object filterValue, UserField updateField, object updateValue)
        {
            var filter = CreateEqFilter(filterField, filterValue);
            var update = CreateSetUpdate(updateField, updateValue);
            _collection.UpdateMany(filter, update);
        }

        public void Remove()
        {
            _collection.DeleteMany(new BsonDocument());
        }

        public void Remove(string id)
        {
            var filter = CreateEqFilter(UserField.Id, id);
            _collection.DeleteOne(filter);
        }

        private void Configure()
        {
            _client = new MongoClient(_URLDatabaseConnection);
            _database = _client.GetDatabase(_databaseName);
        }

        private UpdateDefinition<User> CreateSetUpdate(UserField field, object value)
        {
            switch (field)
            {
                case UserField.Id:
                    return Builders<User>.Update.Set(x => x.Id, value.ToString());
                case UserField.Login: 
                    return Builders<User>.Update.Set(x => x.Login, value.ToString());
                case UserField.Password:
                    return Builders<User>.Update.Set(x => x.Password, value.ToString());
                case UserField.Permissions: 
                    return Builders<User>.Update.Set(x => x.Permissions, (Permissions)value);
                case UserField.Created:
                    return Builders<User>.Update.Set(x => x.Created, (DateTime)value);
                case UserField.TokensOfCreatedUries:
                    return Builders<User>.Update.Set(x => x.TokensOfCreatedUries, (IList<string>)value);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        private FilterDefinition<User> CreateEqFilter(UserField field, object value)
        {
            switch (field)
            {
                case UserField.Id:
                    return Builders<User>.Filter.Eq(x => x.Id, value.ToString());
                case UserField.Login:
                    return Builders<User>.Filter.Eq(x => x.Login, value.ToString());
                case UserField.Password:
                    return Builders<User>.Filter.Eq(x => x.Password, value.ToString());
                case UserField.Permissions:
                    return Builders<User>.Filter.Eq(x => x.Permissions, (Permissions)value);
                case UserField.Created:
                    return Builders<User>.Filter.Eq(x => x.Created, (DateTime)value);
                case UserField.TokensOfCreatedUries:
                    return Builders<User>.Filter.Eq(x => x.TokensOfCreatedUries, (IList<string>)value);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
