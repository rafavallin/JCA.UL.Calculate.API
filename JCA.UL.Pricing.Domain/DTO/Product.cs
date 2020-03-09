using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using JCA.UL.Pricing.Domain.Entities;

namespace JCA.UL.Pricing.Domain.DTO
{

    public class Product
    {

        public int idProduct { get; set; }
        public string strProduct { get; set; }
        public List<Parameter> productParameters { get; set; }


        public static implicit operator Product(ProductMap entity)
        {
            return entity == null ? null : new Product()
            {
                idProduct = entity.idProduct,
                strProduct = entity.strProduct,
                productParameters = entity.productParameters.Select(x => (Parameter)x).ToList()
                
            };
        }
    }
}