using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using JCA.UL.Pricing.Domain.Entities;

namespace JCA.UL.Pricing.Domain.DTO
{

    public class Parameter
    {

         public int idParameter { get; set; }
        public int idProduct { get; set; }
        public string strParameter { get; set; }
        public string strParameterType { get; set; }
        public char strParameterFunc { get; set; }

        public List<Rule> parameterRules { get; set; }

        public static implicit operator Parameter(ParameterMap entity)
        {
            return entity == null ? null : new Parameter()
            {
                idParameter = entity.idParameter,
                idProduct = entity.idProduct,
                strParameter = entity.strParameter,
                strParameterType = entity.strParameterType,
                strParameterFunc = entity.strParameterFunc
            };
        }
    }
}