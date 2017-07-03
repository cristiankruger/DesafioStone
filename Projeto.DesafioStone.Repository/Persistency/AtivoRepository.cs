using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.DesafioStone.Entidades;
using Projeto.DesafioStone.Repository.DataSource;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Projeto.DesafioStone.Repository.Persistency
{
    public class AtivoRepository:MongoDataSource
    {
        //atributo para definir o nome da collection utilizada pelo mongo para armazenar os dados do Ativo
        private readonly string collectionName = typeof(Ativo).Name;

        private IMongoCollection<Ativo> collection;//dbSet

        public AtivoRepository()
        {
            //inicializar o atributo collection para estabelecer acesso ao mongo
            collection = GetDatabase().GetCollection<Ativo>(collectionName);
        }


        public void InsertAtivo(Ativo Ativo)
        {
            try
            {
                collection.InsertOne(Ativo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteAtivo(Ativo Ativo)
        {
            try
            {
                var filter = Builders<Ativo>.Filter.Where(f => f.Id == Ativo.Id);
                collection.DeleteOne(filter);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateAtivo(Ativo Ativo)
        {
            try
            {
                var filter = Builders<Ativo>.Filter.Where(f => f.Id == Ativo.Id);
                var update = Builders<Ativo>.Update
                                    .Set("Nome", Ativo.Nome)
                                    .Set("DataAquisicao", Ativo.DataAquisicao)
                                    .Set("Descricao", Ativo.Descricao)
                                    .Set("NumeroSerie", Ativo.NumeroSerie)
                                    .Set("Valor", Ativo.Valor)
                                    .Set("TipoAtivo", Ativo.TipoAtivo);
                collection.UpdateOne(filter, update);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public List<Ativo> GetAllAtivos()
        {
            try
            {
                var filter = Builders<Ativo>.Filter.Exists("_id");
                return collection.Find(filter).ToList<Ativo>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Ativo GetAtivoById(ObjectId id)
        {
            try
            {
                var filter = Builders<Ativo>.Filter.Where(f => f.Id == id);
                return collection.Find(filter).ToList().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
