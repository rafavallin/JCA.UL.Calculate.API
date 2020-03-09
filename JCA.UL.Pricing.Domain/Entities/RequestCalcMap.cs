using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JCA.UL.Pricing.Domain.DTO;

namespace JCA.UL.Pricing.Domain.Entities
{
    [Table("tb_RequestCalc")]
    public class RequestCalcMap
    {
        [Key]
        [Column("idRequestCalc")]
        public int idRequestCalc { get; set; }

        [Column("idRequest")]
        public int idRequest { get; set; }

        [Column("strRequestCalcName")]
        public string strRequestCalcName { get; set; }
        
        [Column("strRequestCalcValue")]
        public string strRequestCalcValue { get; set; }
        
        [ForeignKey("idRequest")]
        public RequestMap Request { get; set; }
        public static implicit operator RequestCalcMap(RequestCalc entity)
        {
            return new RequestCalcMap()
            {
                idRequestCalc = entity.idRequestCalc,
                idRequest = entity.idRequest,
                strRequestCalcName = entity.strRequestCalcName,
                strRequestCalcValue = entity.strRequestCalcValue
            };
        }
    }
}