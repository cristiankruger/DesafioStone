using MongoDB.Bson;
using Projeto.DesafioStone.Entidades;
using Projeto.DesafioStone.Repository.Persistency;
using Projeto.DesafioStone.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Projeto.DesafioStone.Service.Controllers
{
    /// <summary>
    /// Serviços sobre a Imobilização do Ativo previamente cadastrado.
    /// </summary>
    [RoutePrefix("api/itemimobilizado")]
    public class ItemImobilizadoController : ApiController
    {
        /// <summary>
        /// Imobilizar um ativo cadastrado. 
        /// </summary>
        /// <param name="model">int Andar, string idAtivo</param>
        /// <returns></returns>
        [HttpPost]
        [Route("imobilizar")]
        public HttpResponseMessage ImobilizarItem(ItemImobilizadoModelImobilizar model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AtivoRepository ativoRep = new AtivoRepository();
                    ItemImobilizadoRepository itemRep = new ItemImobilizadoRepository();
                    ObjectId objId = new ObjectId(model.IdAtivo);
                    Ativo ativo = ativoRep.GetAtivoById(objId);

                    if (ativo != null)
                    {
                        ItemImobilizado item = itemRep.GetItemImobilizadoByAtivoId(new ObjectId(model.IdAtivo));

                        if (item == null)
                        {
                            ItemImobilizado i = new ItemImobilizado();

                            i.IdAtivo = model.IdAtivo;
                            i.Andar = model.Andar;

                            itemRep.InsertItemImobilizado(i);

                            return Request.CreateResponse(HttpStatusCode.OK, $"Item '{i.IdAtivo}' Imobilizado com sucesso.");
                        }
                        else
                            return Request.CreateResponse(HttpStatusCode.Forbidden, "Este Ativo já está imobilizado.");
                    }
                    else
                        return Request.CreateResponse(HttpStatusCode.NotFound, $"Item '{model.IdAtivo}' Não existe.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, ModelState);
                }
            }
            catch (FormatException)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "Favor inserir um Id válido.");
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Consultar todos os Ativos imobilizados.
        /// </summary>
        /// <returns>uma lista com todos os Ativos imobilizados</returns>
        [HttpGet]
        [Route("consultarImobilizados")]
        public HttpResponseMessage ConsultarTodosImobilizados()
        {
            try
            {
                AtivoRepository ativoRep = new AtivoRepository();
                ItemImobilizadoRepository itemRep = new ItemImobilizadoRepository();
                List<ItemImobilizadoModelConsultar> lista = new List<ItemImobilizadoModelConsultar>();

                foreach (var item in itemRep.GetAllItensImobilizados())
                {
                    ItemImobilizadoModelConsultar model = new ItemImobilizadoModelConsultar();
                    model.Id = item.Id.ToString();
                    model.IdAtivo = item.IdAtivo;
                    model.Andar = item.Andar.ToString();

                    lista.Add(model);
                };

                if (lista.Count > 0)
                {
                    //resgatar todos os dados dos ativos e exibir atraves 
                    //do IdAtivo da lista

                    List<ItemImobilizadoModelConsultar> itemLista = new List<ItemImobilizadoModelConsultar>();

                    foreach (var i in lista)
                    {
                        var id = new ObjectId(i.IdAtivo);

                        Ativo a = ativoRep.GetAtivoById(id);

                        ItemImobilizadoModelConsultar itemConsulta = new ItemImobilizadoModelConsultar();
                        itemConsulta.Andar = i.Andar;
                        itemConsulta.Id = i.Id;
                        itemConsulta.IdAtivo = a.Id.ToString();
                        itemConsulta.DataAquisicao = a.DataAquisicao;
                        itemConsulta.Descricao = a.Descricao;
                        itemConsulta.Nome = a.Nome;
                        itemConsulta.NumeroSerie = a.Nome;
                        itemConsulta.TipoAtivo = a.TipoAtivo.ToString();
                        itemConsulta.Valor = a.Valor.ToString("0.00");

                        itemLista.Add(itemConsulta);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, itemLista);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Não existem Ativos Imobilizados.");
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Consultar todos os Ativos desmobilizados.
        /// </summary>
        /// <returns>uma lista com todos os Ativos desmobilizados</returns>
        [HttpGet]
        [Route("consultardesmobilizados")]
        public HttpResponseMessage ConsultarTodosDesmobilizados()
        {
            try
            {
                ItemImobilizadoRepository itemRep = new ItemImobilizadoRepository();
                AtivoRepository ativoRep = new AtivoRepository();
                List<ItemImobilizadoModelConsultar> lista = new List<ItemImobilizadoModelConsultar>();

                foreach (var a in ativoRep.GetAllAtivos())
                {
                    var imobilizado = itemRep.GetItemImobilizadoByAtivoId(a.Id);
                    if (imobilizado == null)
                    {
                        ItemImobilizadoModelConsultar item = new ItemImobilizadoModelConsultar();

                        item.Andar = string.Empty;
                        item.Id = string.Empty;
                        item.DataAquisicao = a.DataAquisicao;
                        item.Descricao = a.Descricao;
                        item.IdAtivo = a.Id.ToString();
                        item.Nome = a.Nome;
                        item.NumeroSerie = a.NumeroSerie;
                        item.TipoAtivo = a.TipoAtivo.ToString();
                        item.Valor = a.Valor.ToString("0.00");

                        lista.Add(item);
                    }
                }

                if (lista.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, lista);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Não existem Ativos Desmobilizados.");
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Consultar um Ativo imobilizado por id.
        /// </summary>
        /// <param name="idAtivo">id do ativo cadastrado</param>
        /// <returns>retorna o ativo Imobilizado.</returns>
        [HttpGet]
        [Route("consultarporid")]
        public HttpResponseMessage ConsultarPorAtivoId(string idAtivo)
        {
            try
            {
                AtivoRepository ativoRep = new AtivoRepository();
                ItemImobilizadoRepository itemRep = new ItemImobilizadoRepository();
                ObjectId objId = new ObjectId(idAtivo);

                var a = ativoRep.GetAtivoById(objId);

                if (a != null)
                {
                    var item = itemRep.GetItemImobilizadoByAtivoId(objId);

                    if (item != null)
                    {
                        ItemImobilizadoModelConsultar itemModelConsulta = new ItemImobilizadoModelConsultar();
                        itemModelConsulta.Andar = item.Andar.ToString();
                        itemModelConsulta.Id = item.Id.ToString();
                        itemModelConsulta.IdAtivo = a.Id.ToString();
                        itemModelConsulta.DataAquisicao = a.DataAquisicao;
                        itemModelConsulta.Descricao = a.Descricao;
                        itemModelConsulta.Nome = a.Nome;
                        itemModelConsulta.NumeroSerie = a.Nome;
                        itemModelConsulta.TipoAtivo = a.TipoAtivo.ToString();
                        itemModelConsulta.Valor = a.Valor.ToString("0.00");

                        return Request.CreateResponse(HttpStatusCode.OK, itemModelConsulta);
                    }
                    else
                        return Request.CreateResponse(HttpStatusCode.Forbidden, $"O Ativo '{idAtivo}' ainda não foi imobilizado.");
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, $"O Ativo '{idAtivo}' não existe.");


            }
            catch (FormatException)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "Favor inserir um Id válido.");
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        /// <summary>
        /// Liberar um ativo imobilizado.
        /// </summary>
        /// <param name="idAtivo">id do ativo a ser liberado</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("liberar")]
        public HttpResponseMessage LiberarItem(string idAtivo)
        {
            try
            {
                AtivoRepository ativoRep = new AtivoRepository();

                ObjectId objId = new ObjectId(idAtivo);

                Ativo ativo = ativoRep.GetAtivoById(objId);

                if (ativo != null)
                {
                    ItemImobilizadoRepository itemRep = new ItemImobilizadoRepository();
                    var ItemImobilizado = itemRep.GetItemImobilizadoByAtivoId(objId);
                    if (ItemImobilizado != null)
                    {
                        itemRep.DeleteItemImobilizado(objId);

                        return Request.CreateResponse(HttpStatusCode.OK, $"Ativo '{ativo.Nome}' liberado com sucesso.");
                    }
                    else
                        return Request.CreateResponse(HttpStatusCode.Forbidden, $"Ativo '{ativo.Nome}' Não está Imobilizado.");
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, $"Item '{idAtivo}' Não existe.");
            }
            catch (FormatException)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "Favor inserir um Id válido.");
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Editar os dados do Item Imobilizado.
        /// </summary>
        /// <param name="model">string Id do Ativo, int Andar.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("editar")]
        public HttpResponseMessage EditarItem(ItemImobilizadoModelEditar model)
        {
            try
            {
                AtivoRepository ativoRep = new AtivoRepository();

                ObjectId objId = new ObjectId(model.IdAtivo);

                Ativo ativo = ativoRep.GetAtivoById(objId);

                if (ativo != null)
                {
                    ItemImobilizadoRepository itemRep = new ItemImobilizadoRepository();
                    var ItemImobilizado = itemRep.GetItemImobilizadoByAtivoId(objId);
                    if (ItemImobilizado != null)
                    {
                        ItemImobilizado item = new ItemImobilizado();
                        item.Andar = model.Andar;
                        item.IdAtivo = model.IdAtivo;
                        itemRep.UpdateItemImobilizado(item);

                        return Request.CreateResponse(HttpStatusCode.OK, $"Item '{ativo.Nome}' atualizado com sucesso.");
                    }
                    else
                        return Request.CreateResponse(HttpStatusCode.NotFound, $"Item '{model.IdAtivo}' Não está Imobilizado.");
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, $"Item '{model.IdAtivo}' Não existe.");
            }
            catch (FormatException)
            {
                return Request.CreateResponse(HttpStatusCode.Forbidden, "Favor inserir um Id válido.");
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }


    }
}
