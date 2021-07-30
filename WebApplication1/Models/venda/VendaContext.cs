using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using WebApplication1.Models.vendedor;
namespace WebApplication1.Models.venda
{
    public class VendaContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public VendaContext(IOptions<ConfigDb> opcoes)
        {
            MongoClient mongoClient = new MongoClient(opcoes.Value.ConnectString);
            if (mongoClient != null)
            {
                _mongoDatabase = mongoClient.GetDatabase(opcoes.Value.Database);
            }
        }
        public IMongoCollection<Venda> Vendas
        {
            get
            {
                return _mongoDatabase.GetCollection<Venda>("coVendas");
            }
        }
    }
}
