using MongoDB.Bson;
using Projeto.DesafioStone.Entidades;
using Projeto.DesafioStone.Repository.Persistency;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Projeto.DesafioStone.Test
{
    public class ItemImobilizadoRepositoryTDD
    {
        [Fact]
        public void InsertItemImobilizado()
        {
            try
            {
                ItemImobilizado item = new ItemImobilizado()
                {
                    Andar = 9,
                    IdAtivo = "59551365718269164ce32340"
                };

                ItemImobilizadoRepository rep = new ItemImobilizadoRepository();

                rep.InsertItemImobilizado(item);

                List<ItemImobilizado> lista = rep.GetAllItensImobilizados();

                ItemImobilizado i = lista[lista.Count() - 1];
                if (i.Andar.Equals(item.Andar) && i.IdAtivo.Equals(item.IdAtivo))
                    Assert.True(true);
                else
                    Assert.True(false);
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
        }

        [Fact]
        public void GetItemImobilizadoByAtivoId()
        {
            try
            {
                ObjectId objId = new ObjectId("59551365718269164ce32340");
                ItemImobilizadoRepository rep = new ItemImobilizadoRepository();

                ItemImobilizado item = rep.GetItemImobilizadoByAtivoId(objId);

                Assert.True(item.IdAtivo.Equals("59551365718269164ce32340"));
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
        }

        [Fact]
        public void GetAllItensImobilizados()
        {
            try
            {
                ItemImobilizadoRepository rep = new ItemImobilizadoRepository();
                List<ItemImobilizado> lista = rep.GetAllItensImobilizados();

                Assert.True(lista.Count() > 0);
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
        }

        [Fact]
        public void UpdateItemImobilizado()
        {
            try
            {
                ItemImobilizado item = new ItemImobilizado()
                {
                    Andar = 1,
                    IdAtivo = "59551365718269164ce32340"
                };

                ItemImobilizadoRepository rep = new ItemImobilizadoRepository();

                rep.UpdateItemImobilizado(item);

                ObjectId objId = new ObjectId("59551365718269164ce32340");
                ItemImobilizado i = rep.GetItemImobilizadoByAtivoId(objId);

                Assert.Equal(item.Andar.ToString(), i.Andar.ToString());

            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
        }

        [Fact]
        public void DeleteItemImobilizado()
        {
            try
            {
                ItemImobilizado item = new ItemImobilizado()
                {
                    Andar = 5,
                    IdAtivo = "59551365718269164ce32340"
                };

                ItemImobilizadoRepository rep = new ItemImobilizadoRepository();
                rep.InsertItemImobilizado(item);

                ObjectId objId = new ObjectId("59551365718269164ce32340");
                rep.DeleteItemImobilizado(objId);

                ItemImobilizado i = rep.GetItemImobilizadoByAtivoId(objId);

                Assert.NotNull(i);
                
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
        }
    }
}
