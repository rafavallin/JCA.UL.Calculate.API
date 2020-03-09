using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JCA.UL.Pricing.Domain.DTO;

namespace JCA.UL.Pricing.Domain.Entities
{
    [Table("tb_request")]
    public class RequestMap
    {
        [Key]
        [Column("idRequest")]
        public int idRequest { get; set; }
        [Column("dtRequest")]
        public DateTime dtRequest { get; set; }
        [Column("strRequestOrigin")]
        public string strRequestOrigin { get; set; }
        [Column("strRequestStatus")]
        public string strRequestStatus { get; set; }
        [Column("strRequestMessage")]
        public string strRequestMessage { get; set; }

        public List<RequestCalcMap> requestCalcs { get; set; }
        
        public static implicit operator RequestMap(Request entity)
        {
            return new RequestMap()
            {
                idRequest = entity.idRequest,
                dtRequest = entity.dtRequest,
                strRequestOrigin = entity.strRequestOrigin,
                strRequestStatus = entity.strRequestStatus,
                strRequestMessage = entity.strRequestMessage
            };
        }
    }
}