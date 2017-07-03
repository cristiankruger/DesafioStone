using System;
using Xunit;
using Projeto.DesafioStone.Repository.Persistency;
using Projeto.DesafioStone.Entidades;
using Projeto.DesafioStone.Entidades.Tipos;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;

namespace Projeto.DesafioStone.Test
{
    public class AtivoRepositoryTDD
    {
        [Fact]
        public void InsertAtivoTest()
        {
            try
            {
                Ativo a = new Ativo()
                {
                    DataAquisicao = new DateTime(2017, 03, 31, 5, 30, 0),
                    Descricao = "descricao teste insert muito longo so pra teste",
                    Nome = "Esse teste",
                    NumeroSerie = "ASDFG123456",
                    Valor = 123.98m,
                    TipoAtivo = TipoAtivo.Movel
                };

                AtivoRepository rep = new AtivoRepository();

                rep.InsertAtivo(a);

                List<Ativo> lista = rep.GetAllAtivos();

                Ativo ativo = lista[lista.Count() - 1];

                if (ativo.DataAquisicao.Date == a.DataAquisicao.Date &&
                    ativo.Descricao.Equals(a.Descricao) &&
                    ativo.Nome.Equals(a.Nome) &&
                    ativo.NumeroSerie.Equals(a.NumeroSerie) &&
                    ativo.TipoAtivo.Equals(a.TipoAtivo) &&
                    ativo.Valor.Equals(a.Valor))
                {
                    Assert.True(true);
                }
                else
                    Assert.True(false);

            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
        }

        [Fact]
        public void GetAllAtivosTest()
        {
            try
            {
                AtivoRepository rep = new AtivoRepository();
                List<Ativo> lista = rep.GetAllAtivos();

                Assert.NotEmpty(lista);
                Assert.True(lista.Count() > 0);

            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
        }

        [Fact]
        public void GetAtivoByIdTest()
        {
            try
            {
                AtivoRepository rep = new AtivoRepository();
                ObjectId objId = new ObjectId("5959b9937182692fc8a873a5");
                Ativo a = rep.GetAtivoById(objId);

                Assert.NotNull(a);
                Assert.Equal(objId.ToString(), a.Id.ToString());
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
        }

        [Fact]
        public void UpdateAtivoTest()
        {
            try
            {
                Ativo a = new Ativo()
                {
                    DataAquisicao = new DateTime(2015, 03, 31),
                    Descricao = "descricao teste insert",
                    Nome = "Nome teste",
                    NumeroSerie = "AA123",
                    Valor = 55.90m,
                    TipoAtivo = TipoAtivo.ComputadorPeriferico,
                    Id = new ObjectId("5959b9937182692fc8a873a5")
                };

                AtivoRepository rep = new AtivoRepository();

                rep.UpdateAtivo(a);

                Ativo ativo = rep.GetAtivoById(new ObjectId("5959b9937182692fc8a873a5"));

                if (ativo.DataAquisicao.Date == a.DataAquisicao.Date &&
                    ativo.Descricao.Equals(a.Descricao) &&
                    ativo.Nome.Equals(a.Nome) &&
                    ativo.NumeroSerie.Equals(a.NumeroSerie) &&
                    ativo.TipoAtivo.Equals(a.TipoAtivo) &&
                    ativo.Valor.Equals(a.Valor))
                {
                    Assert.True(true);
                }
                else
                    Assert.True(false);
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
        }

        [Fact]
        public void DeleteAtivoTest()
        {
            try
            {
                Ativo a = new Ativo
                {
                    DataAquisicao = new DateTime(2015, 03, 31, 0, 0, 0),
                    Descricao = "descricao teste insert",
                    Nome = "Nome teste",
                    NumeroSerie = "AA123",
                    Valor = 55.90m,
                    TipoAtivo = TipoAtivo.ComputadorPeriferico,
                    Id = new ObjectId("5959705b7182692fb4ab0506")
                };

                AtivoRepository rep = new AtivoRepository();

                rep.DeleteAtivo(a);

                List<Ativo> lista = rep.GetAllAtivos();

                foreach (Ativo ativo in lista)
                {
                    if (ativo.Id.Equals("5959705b7182692fb4ab0506"))
                    {
                        Assert.True(false);
                    }
                }
                Assert.True(true);
            }
            catch (Exception e)
            {
                Assert.True(false, e.Message);
            }
        }
    }
}
