using MongoDB.Bson;
using MongoDB.Driver;
using Projeto.DesafioStone.Entidades;
using Projeto.DesafioStone.Repository.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DesafioStone.Repository.Persistency
{
    public class ItemImobilizadoRepository : MongoDataSource
    {
        //atributo para definir o nome da collection utilizada pelo mongo para armazenar os dados do ItemImobilizado
        private readonly string collectionName = typeof(ItemImobilizado).Name;

        private IMongoCollection<ItemImobilizado> collection;//dbSet

        public ItemImobilizadoRepository()
        {
            //inicializar o atributo collection para estabelecer acesso ao mongo
            collection = GetDatabase().GetCollection<ItemImobilizado>(collectionName);
        }

        public void InsertItemImobilizado(ItemImobilizado ItemImobilizado)
        {
            try
            {
                collection.InsertOne(ItemImobilizado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ItemImobilizado GetItemImobilizadoByAtivoId(ObjectId id)
        {
            try
            {
                var filter = Builders<ItemImobilizado>.Filter.Where(i => i.IdAtivo == id.ToString());
                return collection.Find(filter).ToList().FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<ItemImobilizado> GetAllItensImobilizados()
        {
            try
            {
                var filter = Builders<ItemImobilizado>.Filter.Exists("IdAtivo");
                return collection.Find(filter).ToList<ItemImobilizado>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateItemImobilizado(ItemImobilizado item)
        {
            try
            {
                var filter = Builders<ItemImobilizado>.Filter.Where(f => f.IdAtivo == item.IdAtivo);
                var update = Builders<ItemImobilizado>.Update
                                    .Set("Andar", item.Andar);
                collection.UpdateOne(filter, update);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteItemImobilizado(ObjectId id)
        {
            try
            {
                var filter = Builders<ItemImobilizado>.Filter.Where(i => i.IdAtivo == id.ToString());
                collection.DeleteOne(filter);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}