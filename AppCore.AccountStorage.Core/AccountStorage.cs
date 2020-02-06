using System;
using System.ComponentModel;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using AppCore.AccountStorage;

namespace AppCore.AccountStorage.Core
{
    public class AccountStorage : IAccountStorage
    {
        private const string _databaseName = "AccountStorage";
        private const string _URLDatabaseConnection = "mongodb://localhost:27017";
        private MongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<Account> _checkCollection;

        private IMongoCollection<Account> _collection
        {
            get
            {
                if (_checkCollection != null)
                    return _checkCollection;
                else
                    return _database.GetCollection<Account>(_databaseName);
            }

            set
            { _checkCollection = value; }
        }

        public AccountStorage()
        {
            Configure();
        }

        public void Create(Account account)
        {
            _collection.InsertOne(account);
        }

        public IList<Account> Read()
        {
            return _collection.Find(new BsonDocument()).ToList();
        }

        public Account Read(string id)
        {
            var filter = CreateEqFilter(AccountField.Id, id);
            return _collection.Find(filter).FirstOrDefault();
        }

        public IList<Account> Read(AccountField filterField, object filterValue)
        {
            var filter = CreateEqFilter(filterField, filterValue);
            return _collection.Find(filter).ToList();
        }

        public void Update(AccountField filterField, object filterValue, AccountField updateField, object updateValue)
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
            var filter = CreateEqFilter(AccountField.Id, id);
            _collection.DeleteOne(filter);
        }

        private void Configure()
        {
            _client = new MongoClient(_URLDatabaseConnection);
            _database = _client.GetDatabase(_databaseName);
        }

        private UpdateDefinition<Account> CreateSetUpdate(AccountField field, object value)
        {
            switch (field)
            {
                case AccountField.Account:
                    return Builders<Account>.Update.Set(x => x, (Account)value);
                case AccountField.Id:
                    return Builders<Account>.Update.Set(x => x.Id, value.ToString());
                case AccountField.Login: 
                    return Builders<Account>.Update.Set(x => x.Login, value.ToString());
                case AccountField.Password:
                    return Builders<Account>.Update.Set(x => x.Password, value.ToString());
                case AccountField.Permissions: 
                    return Builders<Account>.Update.Set(x => x.Permissions, (Permissions)value);
                case AccountField.Created:
                    return Builders<Account>.Update.Set(x => x.Created, (DateTime)value);
                case AccountField.TokensOfCreatedUries:
                    return Builders<Account>.Update.Set(x => x.TokensOfCreatedUries, (IList<string>)value);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        private FilterDefinition<Account> CreateEqFilter(AccountField field, object value)
        {
            switch (field)
            {
                case AccountField.Account:
                    return Builders<Account>.Filter.Eq(x => x, (Account)value);
                case AccountField.Id:
                    return Builders<Account>.Filter.Eq(x => x.Id, value.ToString());
                case AccountField.Login:
                    return Builders<Account>.Filter.Eq(x => x.Login, value.ToString());
                case AccountField.Password:
                    return Builders<Account>.Filter.Eq(x => x.Password, value.ToString());
                case AccountField.Permissions:
                    return Builders<Account>.Filter.Eq(x => x.Permissions, (Permissions)value);
                case AccountField.Created:
                    return Builders<Account>.Filter.Eq(x => x.Created, (DateTime)value);
                case AccountField.TokensOfCreatedUries:
                    return Builders<Account>.Filter.Eq(x => x.TokensOfCreatedUries, (IList<string>)value);
                default:
                    throw new InvalidEnumArgumentException();
            }
        }
    }
}
