using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
namespace WebApplication1.Models
{
    public class ProductContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public ProductContext(IOptions<ConfigDb> opcoes)
        {
            MongoClient mongoClient = new MongoClient(opcoes.Value.ConnectString);
            if (mongoClient != null)
            {
                _mongoDatabase = mongoClient.GetDatabase(opcoes.Value.Database);
            }
        }
        public IMongoCollection<Product> Produtos
        {
            get
            {
                return _mongoDatabase.GetCollection<Product>("coProducts");
            }
        }
    }
}
