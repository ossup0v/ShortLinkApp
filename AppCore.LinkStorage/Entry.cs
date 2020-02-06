using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Runtime.Serialization;

namespace AppCore.LinkStorage
{
    [Serializable]
    [BsonIgnoreExtraElements]
    [DataContract]
    public class Entry 
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
