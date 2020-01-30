﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.LinkStorage.API
{
    [Serializable]
    [BsonIgnoreExtraElements]
    public class StorageURI
    {
        public string Id { get; set; }
        public string FullURI { get; set; }
        public string ShortURI { get; set; }
        public string Token { get; set; }
        public int Clicked { get; set; } = 0;
        public DateTime Created { get; set; }
        public string Creater { get; set; }
    }
}