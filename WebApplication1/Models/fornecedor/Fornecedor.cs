using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.fornecedor
{
    public class Fornecedor
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        public string nome { get; set; }
        public string nomeFantasia { get; set; }
        public string cnpj { get; set; }
        public string ie { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        public DateTime created_at { get; set; }

        public void setDate()
        {
            this.created_at = DateTime.Now;
        }

    }
}
