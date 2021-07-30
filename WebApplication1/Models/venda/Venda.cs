using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.cliente;
namespace WebApplication1.Models.venda
{
    public class Venda
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public double Valor { get; set; }
        public DateTime Created_at { get; set; }
        public Vendedor Vendedor { get; set; }
        public Cliente Cliente { get; set; }
        public List<Product> Produtos { get; set; }
        public void setDate()
        {
            this.Created_at = DateTime.Now;
        }
        public void setValor()
        {

        }
    }
}
