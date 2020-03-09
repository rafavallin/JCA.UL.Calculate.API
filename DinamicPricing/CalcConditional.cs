using System;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;
using org.mariuszgromada.math.mxparser;


namespace DynamicPricing
{
    class CalcConditional : CalcBase
    {
        public CalcConditional()
        {
            
        }
        public string Value { get; set; }
        public string ValueFalse { get; set; }
        public string Condition { get; set; }
        public string ResultadoFinal { get; set; } // ValorFinal, 50
        public override string Calculation(params string[] args)
        {
            if(string.IsNullOrEmpty(ResultadoFinal))
            {
                Expression calcConditional = new Expression(this.Replace(this.Condition));
                double conditionalResult = calcConditional.calculate();
                Expression calc;   

                if(conditionalResult == 1)
                    calc = new Expression(this.Replace(this.Value));
                else
                    calc = new Expression(this.Replace(this.ValueFalse));
                
                ResultadoFinal =  calc.calculate().ToString();
            }
            return ResultadoFinal;       
        }
    }
}
