using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace WebApplication1.Models
{
    public class Categoria 
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
       
        public string nome { get; set; }

        public string descricao { get; set; }
        public DateTime created_at { get; set; }

    }
}
