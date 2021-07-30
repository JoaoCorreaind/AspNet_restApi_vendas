using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication1.Models
{
    public class Product
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string descricao { get; set; }
        public double valorVenda { get; set; }
        public string codigo { get; set; }
        public int quantidadeEstoque { get; set; }
        public double valorCompra { get; set; }
        public double impostoVenda { get; set; }
        public DateTime created_at { get; set; }
        public Categoria categoria { get; set; }
    }
    
}
