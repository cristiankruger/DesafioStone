using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.DesafioStone.Service.Validations
{
    public class ObjectIdValidation : ValidationAttribute
    {
        /// <summary>
        /// Validar um mongodb ObjectId
        /// </summary>
        /// <param name="value">ObjectId a ser validado</param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            try
            {
                string objectId = value.ToString();

                if (string.IsNullOrEmpty(objectId))
                {
                    return false;
                }

                if (objectId.Length != 24)
                {
                    return false;
                }

                new ObjectId(objectId);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}