using MongoDB.Driver;
using System;
using System.Configuration;

namespace Projeto.DesafioStone.Repository.DataSource
{
    public class MongoDataSource
    {
        //método estatico que aponte para a base do MongoDB
        protected static IMongoDatabase GetDatabase()
        {
            try
            {
                IMongoClient client = new MongoClient(ConfigurationManager.ConnectionStrings["mongoDB"].ConnectionString);
                //IMongoClient client = new MongoClient("mongodb://localhost:27017");// somente para testes
                
                return client.GetDatabase("DesafioStone");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
