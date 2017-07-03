using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Projeto.DesafioStone.Entidades.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.DesafioStone.Service.Models
{
    public class AtivoModelConsultar
    {
        public ObjectId Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataAquisicao { get; set; }
        public string Descricao { get; set; }
        public string NumeroSerie { get; set; }
        public string Valor { get; set; }
        public string TipoAtivo{ get; set; }
    }
}