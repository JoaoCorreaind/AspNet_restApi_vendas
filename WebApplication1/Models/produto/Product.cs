using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WebApplication1.Models.fornecedor;

namespace WebApplication1.Models
{
    public class Product
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nome { get; set; }
        public double ValorVenda { get; set; }
        public string Codigo { get; set; }
        public int QuantidadeEstoque { get; set; }
        public double ValorCompra { get; set; }
        public double ImpostoVenda { get; set; }
        public DateTime Created_at { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public Categoria Categoria { get; set; }

        public void setDate()
        {
            this.Created_at = DateTime.Now;
        }
    }
    
}
