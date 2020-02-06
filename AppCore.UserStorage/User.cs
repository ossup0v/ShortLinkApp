using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppCore.UserStorage
{

    [Serializable]
    [BsonIgnoreExtraElements]
    [DataContract]
    public class User
    {
        [BsonId]
        public Guid _id { get; set; }

        [DataMember]
        public string Id
        {
            get { return _id.ToString(); }
            set { _id = new Guid(value); }
        }

        public string Login { get; set; }
        public string Password{ get; set; }
        public Permissions Permissions { get; set; }
        public DateTime Created { get; set; }
        public List<string> TokensOfCreatedUries { get; set; }
    }

    public enum Permissions
    {
        UnregisteredUser,
        RegisteredUser,
        Moderator,
        Admin
    }
}
