using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.DesafioStone.Service.Validations
{
    /// <summary>
    /// validação de Tipos de ativo
    /// </summary>
    public class TipoAtivoValidation:ValidationAttribute
    {
     
        /// <summary>
        /// validação de Tipo de Ativo
        /// </summary>
        /// <param name="value">objeto a ser validado</param>
        /// <returns>verdadeiro ou falso</returns>
        public override bool IsValid(object value)
        {
            if (value.ToString() is string)
            {
                if (value.ToString().Equals("ComputadorPeriferico")
                || value.ToString().Equals("Movel") || value.ToString().Equals("Utensilio")
                || value.ToString().Equals("Ferramenta") || value.ToString().Equals("Equipamento"))
                {
                    return true;
                }
            }
            return false;
        }
    }
}