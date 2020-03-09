using JCA.UL.Pricing.Domain.Context;
using JCA.UL.Pricing.Domain.Entities;
using JCA.UL.Pricing.Repository.Base;

namespace JCA.UL.Pricing.Repository
{
    public class RequestCalcRepository : Repository<RequestCalcMap>
    {
        public RequestCalcRepository(PricingContext context) : base(context)
        {

        }
    }
}