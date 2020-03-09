using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JCA.UL.Pricing.Domain.DTO;

namespace JCA.UL.Pricing.Domain.Entities
{
    [Table("tb_rule")]
    public class RuleMap
    {
        [Key]
        [Column("idRule")]
        public int idRule { get; set; }

        [Column("idParameter")]
        public int idParameter { get; set; }

        [Column("strRuleName")]
        public string strRuleName { get; set; }

        [Column("strRuleValue")]
        public string strRuleValue { get; set; }

        [Column("strRuleType")]
        public string strRuleType { get; set; }

        [ForeignKey("idParameter")]
        public ParameterMap Parameter { get; set; }

        public static implicit operator RuleMap(Rule entity)
        {
            return new RuleMap()
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