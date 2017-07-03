using Projeto.DesafioStone.Entidades;
using Projeto.DesafioStone.Service.Models;
using Projeto.DesafioStone.Repository.Persistency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Bson;

namespace Projeto.DesafioStone.Service.Controllers
{
    /// <summary>
    /// Serviços sobre o Ativo.
    /// </summary>
    [RoutePrefix("api/ativo")]
    public class AtivoController : ApiController
    {
        /// <summary>
        /// Cadastrar um Ativo adquirido.
        /// </summary>
        /// <param name="model">DateTime DataAquisicao, 
        /// string Descricao, string Nome, sstring NumeroSerie, string TipoAtivo, decimal Valor</param>
        /// <returns></returns>
        [HttpPost]
        [Route("cadastrar")]
        public HttpResponseMessage CadastrarAtivo(AtivoModelCadastrar model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Ativo a = new Ativo();
                    a.DataAquisicao = model.DataAquisicao;
                    a.Descricao = model.Descricao;
                    a.Nome = model.Nome;
                    a.NumeroSerie = model.NumeroSerie;
                    a.TipoAtivo = model.TipoAtivo;
                    a.Valor = model.Valor;

                    AtivoRepository rep = new AtivoRepository();

                    rep.InsertAtivo(a);

                    return Request.CreateResponse(HttpStatusCode.OK, $"Item '{a.Nome}' Cadastrado com sucesso.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, ModelState);
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        /// <summary>
        /// Consultar todos os Ativos cadastrados.
        /// </summary>
        /// <returns>retorna uma lista de ativos cadastrados</returns>
        [HttpGet]
        [Route("consultarTodos")]
        public HttpResponseMessage ConsultarTodos()
        {
            try
            {
                var lista = new List<AtivoModelConsultar>();

                AtivoRepository rep = new AtivoRepository();

                foreach (Ativo a in rep.GetAllAtivos())
                {
                    var model = new AtivoModelConsultar();
                    model.Id = a.Id;
                    model.Nome = a.Nome;
                    model.DataAquisicao = a.DataAquisicao;
                    model.Descricao = a.Descricao;
                    model.NumeroSerie = a.NumeroSerie;
                    model.TipoAtivo = a.TipoAtivo.ToString();
                    model.Valor = a.Valor.ToString("0.00");

                    lista.Add(model);
                }

                if (lista.Count > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, lista);
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Não existem itens cadastrados");

            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// Consultar um Ativo através do id.
        /// </summary>
        /// <param name="id">id do ativo a ser consultado.</param>
        /// <returns>retorna um ativo cadastrado</returns>
        [HttpGet]
        [Route("consultarporid")]//url: /api/ativo/consultarporid?id=idString
        public HttpResponseMessage ConsultarAtivoPorId(string id)
        {
            try
            {
                var objId = new ObjectId(id);
                AtivoRepository rep = new AtivoRepository();
                Ativo a = rep.GetAtivoById(objId);
                if (a != null)
                {
                    var model = new AtivoModelConsultar();

                    model.Id = a.Id;
                    model.Nome = a.Nome;
                    model.DataAquisicao = a.DataAquisicao;
                    model.Descricao = a.Descricao;
                    model.NumeroSerie = a.NumeroSerie;
                    model.TipoAtivo = a.TipoAtivo.ToString();
                    model.Valor = a.Valor.ToString("0.00");

                    return Request.CreateResponse(HttpStatusCode.OK, model);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, $"o item '{id}' Não existe.");

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
        /// Deletar um ativo cadastrado.
        /// Se o Ativo estiver Imobilizado este será Desmobilizado.
        /// </summary>
        /// <param name="id">id so ativo a ser deletado</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deletar")]
        public HttpResponseMessage DeletarAtivo(string id)
        {
            try
            {
                var objId = new ObjectId(id);
                AtivoRepository ativoRep = new AtivoRepository();
                Ativo a = ativoRep.GetAtivoById(objId);

                if (a != null)
                {
                    ItemImobilizadoRepository itemRep = new ItemImobilizadoRepository();
                    ItemImobilizado item = itemRep.GetItemImobilizadoByAtivoId(objId);

                    if (item != null)
                    {
                        itemRep.DeleteItemImobilizado(objId);
                    }

                    ativoRep.DeleteAtivo(a);

                    return Request.CreateResponse(HttpStatusCode.OK, $"Item '{a.Nome}' excluído com sucesso.");
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, $"Item id: '{id}' não encontrado.");
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
        /// Editar os dados de um Ativo cadastrado.
        /// </summary>
        /// <param name="model">DateTime DataAquisicao, 
        /// string Descricao, string Nome, sstring NumeroSerie, string TipoAtivo, decimal Valor</param>
        /// <returns></returns>
        [HttpPut]
        [Route("editar")]
        public HttpResponseMessage EditarAtivo(AtivoModelEditar model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objId = new ObjectId(model.Id);
                    AtivoRepository rep = new AtivoRepository();

                    Ativo a = rep.GetAtivoById(objId);

                    if (a != null)
                    {
                        a.DataAquisicao = model.DataAquisicao;
                        a.Descricao = model.Descricao;
                        a.Nome = model.Nome;
                        a.NumeroSerie = model.NumeroSerie;
                        a.TipoAtivo = model.TipoAtivo;
                        a.Valor = model.Valor;

                        rep.UpdateAtivo(a);

                        return Request.CreateResponse(HttpStatusCode.OK, $"Item '{a.Nome}' Editado com sucesso.");
                    }
                    else
                        return Request.CreateResponse(HttpStatusCode.NotFound, $"Item id:{model.Id} não encontrado.");
                }
                else
                    return Request.CreateResponse(HttpStatusCode.Forbidden, ModelState);
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