using Projeto.DesafioStone.Entidades.Tipos;
using Projeto.DesafioStone.Service.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Projeto.DesafioStone.Service.Models
{
    public class AtivoModelCadastrar
    {
        [Required(ErrorMessage ="Favor inserir Data de Aquisição.")]
        [DataType(DataType.DateTime, ErrorMessage = "Data deve ter o formato: mm/dd/yyyy")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataAquisicao { get; set; }

        [Required(ErrorMessage = "Favor inserir Descricao")]
        [RegularExpression("^[a-zA-Zà-üÀ-Ü0-9\\s.]{3,100}$", ErrorMessage = "Descrição deve conter apenas letras, números e espaços e deve conter entre 3 e 100 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Favor inserir Nome")]
        [RegularExpression("^[a-zA-Zà-üÀ-Ü\\s]{3,50}$", ErrorMessage = "Nome deve conter apenas letras e espaços e deve conter entre 3 e 50 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Favor inserir Número de série.")]
        public string NumeroSerie { get; set; }

        [Required(ErrorMessage = "Favor Inserir Tipo.")]
        [TipoAtivoValidation(ErrorMessage = "Por favor Selecione entre: ComputadorPeriferico, Movel, Utensilio, Ferramenta ou Equipamento.")]
        public TipoAtivo TipoAtivo { get; set; }

        [Required(ErrorMessage = "Favor inserir Valor.")]
        [Range(0.01, Int32.MaxValue, ErrorMessage ="Valor deve ser maior que 0.")]
        public decimal Valor { get; set; }
    }
}