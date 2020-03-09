using JCA.UL.Pricing.Domain.Context;
using JCA.UL.Pricing.Domain.Entities;
using JCA.UL.Pricing.Repository.Base;

namespace JCA.UL.Pricing.Repository
{
    public class ParameterRepository : Repository<ParameterMap>
    {
        public ParameterRepository(PricingContext context) : base(context)
        {

        }
    }
}