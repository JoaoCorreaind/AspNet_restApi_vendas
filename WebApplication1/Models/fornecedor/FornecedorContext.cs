using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace WebApplication1.Models.fornecedor
{
    public class FornecedorContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public FornecedorContext(IOptions<ConfigDb> opcoes)
        {
            MongoClient mongoClient = new MongoClient(opcoes.Value.ConnectString);
            if (mongoClient != null)
            {
                _mongoDatabase = mongoClient.GetDatabase(opcoes.Value.Database);
            }
        }
        public IMongoCollection<Fornecedor> Fornecedores
        {
            get
            {
                return _mongoDatabase.GetCollection<Fornecedor>("coFornecedor");
            }
        }
    }
}
