using Projeto.DesafioStone.Entidades.Tipos;
using Projeto.DesafioStone.Service.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.DesafioStone.Service.Models
{
    public class ItemImobilizadoModelImobilizar
    {
        [Required(ErrorMessage = "Favor inserir um ObjectId de um ativo válido.")]
        [ObjectIdValidation(ErrorMessage ="informe um ObjectId válido.")]
        public string IdAtivo { get; set; }

        [Range(0, 150, ErrorMessage = "Favor inserir apenas números entre 0 e 150.")]
        [Required(ErrorMessage = "Favor inserir o Andar em o item será imobilizado.")]
        public int Andar { get; set; }

    }
}