using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models.vendedor;
namespace WebApplication1.Models.venda
{
    public class Venda
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Vendedor vendedor { get; set; }
        public List<Product> produtos { get; set; }
        public double valor { get; set; }
        public DateTime created_at { get; set; }

        public void setDate()
        {
            this.created_at = DateTime.Now;
        }
        public void setValor()
        {

        }
    }
}
