using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class CategoriaContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public CategoriaContext(IOptions<ConfigDb> opcoes)
        {
            MongoClient mongoClient = new MongoClient(opcoes.Value.ConnectString);
            if (mongoClient != null)
            {
                _mongoDatabase = mongoClient.GetDatabase(opcoes.Value.Database);
            }
        }
        public IMongoCollection<Categoria> Categoria
        {
            get
            {
                return _mongoDatabase.GetCollection<Categoria>("coCategorys");
            }
        }
    }
}
