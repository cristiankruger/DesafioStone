using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Projeto.DesafioStone.Entidades.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DesafioStone.Entidades
{
    public class Ativo
    {
        public ObjectId Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public string NumeroSerie { get; set; }
        public DateTime DataAquisicao { get; set; }

        [BsonRepresentation(BsonType.String)]
        public TipoAtivo TipoAtivo { get; set; }

        public Ativo()
        {
        }

        public Ativo(ObjectId id, string nome, string descricao, decimal valor, string numeroSerie, DateTime dataAquisicao, TipoAtivo tipoAtivo)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            NumeroSerie = numeroSerie;
            DataAquisicao = dataAquisicao;
            TipoAtivo = tipoAtivo;
        }
    }
}
