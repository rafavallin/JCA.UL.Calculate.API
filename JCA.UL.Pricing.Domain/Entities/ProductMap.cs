using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JCA.UL.Pricing.Domain.DTO;

namespace JCA.UL.Pricing.Domain.Entities
{
    [Table("tb_product")]
    public class ProductMap
    {
        [Key]
        [Column("idProduct")]
        public int idProduct { get; set; }

        [Column("strProduct")]
        public string strProduct { get; set; }
        
        public List<ParameterMap> productParameters { get; set; }

        public static implicit operator ProductMap(Product entity)
        {
            return new ProductMap()
            {
                idProduct = entity.idProduct,
                strProduct = entity.strProduct,
            };
        }
    }
}