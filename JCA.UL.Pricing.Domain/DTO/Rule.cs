using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JCA.UL.Pricing.Domain.Entities;

namespace JCA.UL.Pricing.Domain.DTO
{

    public class Rule
    {

        public int idRule { get; set; }
        public int idParameter { get; set; }
        public string strRuleName { get; set; }
        public string strRuleValue { get; set; }
        public string strRuleType { get; set; }

        public static implicit operator Rule(RuleMap entity)
        {
            return entity == null ? null : new Rule()
            {
                idRule = entity.idRule,
                idParameter = entity.idParameter,
                strRuleName = entity.strRuleName,
                strRuleValue = entity.strRuleValue,
                strRuleType = entity.strRuleType
            };
        }
    }
}