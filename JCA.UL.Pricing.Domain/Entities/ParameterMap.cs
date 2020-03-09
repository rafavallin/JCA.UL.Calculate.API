using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JCA.UL.Pricing.Domain.DTO;

namespace JCA.UL.Pricing.Domain.Entities
{
    [Table("tb_Parameter")]
    public class ParameterMap
    {
        [Key]
        [Column("idParameter")]
        public int idParameter { get; set; }
        [Column("idProduct")]
        
        public int idProduct { get; set; }
        [Column("strParameter")]
        public string strParameter { get; set; }
        [Column("strParameterType")]
        public string strParameterType { get; set; }
        [Column("strParameterFunc")]
        public char strParameterFunc { get; set; }
        [ForeignKey("idProduct")]
        public ProductMap Product { get; set; }
        public List<RuleMap> parameterRules { get; set; }
        public static implicit operator ParameterMap(Parameter entity)
        {
            return new ParameterMap()
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