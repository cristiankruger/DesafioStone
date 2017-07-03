using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DesafioStone.Entidades
{
    public class ItemImobilizado
    {
        public ObjectId Id { get; set; }
        public int Andar { get; set; }
        //public Ativo Ativo { get; set; }
        public string IdAtivo { get; set; }

        public ItemImobilizado()
        {
        }

        public ItemImobilizado(ObjectId id, string idAtivo, int andar)
        {
            Id = id;
            IdAtivo = idAtivo;
            Andar = andar;
        }
    }
}
