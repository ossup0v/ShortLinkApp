using App.LinkStorage.API;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace App.Storage.API
{
    [Serializable]
    [BsonIgnoreExtraElements]
    [DataContract]
    public class Entry : IEntry
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [DataMember]
        public string Id
        {
            get { return _id.ToString(); }
            set { _id = ObjectId.Parse(value); }
        }

        public StorageURI Uri { get; set; }
    }
}
