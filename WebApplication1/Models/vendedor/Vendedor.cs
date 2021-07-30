using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.vendedor
{
    public class Vendedor
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string nome { get; set; }
        public string telefone { get; set; }
        public string cpf { get; set; }
        public DateTime dataNascimento { get; set; }
        public DateTime created_at { get; set; }

        public void setDate()
        {
            this.created_at = DateTime.Now;
        }
    }
}
