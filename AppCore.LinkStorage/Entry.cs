using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Runtime.Serialization;

namespace AppCore.LinkStorage.API
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
