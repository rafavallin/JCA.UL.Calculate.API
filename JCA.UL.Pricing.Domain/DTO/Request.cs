using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using JCA.UL.Pricing.Domain.Entities;

namespace JCA.UL.Pricing.Domain.DTO
{

    public class Request
    {

         public int idRequest { get; set; }
        public DateTime dtRequest { get; set; }
        public string strRequestOrigin { get; set; }
        public string strRequestStatus { get; set; }
        public string strRequestMessage { get; set; }

        public static implicit operator Request(RequestMap entity)
        {
            return entity == null ? null : new Request()
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