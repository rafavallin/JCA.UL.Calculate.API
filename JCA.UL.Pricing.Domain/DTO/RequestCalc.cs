using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using JCA.UL.Pricing.Domain.Entities;

namespace JCA.UL.Pricing.Domain.DTO
{

    public class RequestCalc
    {

         public int idRequestCalc { get; set; }
        public int idRequest { get; set; }
        public string strRequestCalcName { get; set; }
        public string strRequestCalcValue { get; set; }
        public Request Request { get; set; }

        public static implicit operator RequestCalc(RequestCalcMap entity)
        {
            return entity == null ? null : new RequestCalc()
            {
                idRequestCalc = entity.idRequestCalc,
                idRequest = entity.idRequest,
                strRequestCalcName = entity.strRequestCalcName,
                strRequestCalcValue = entity.strRequestCalcValue
            };
        }
    }
}