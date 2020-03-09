using System;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;
using org.mariuszgromada.math.mxparser;


namespace JCA.UL.Pricing.Infrastructure
{
    class CalcConditional : CalcBase
    {
        public CalcConditional()
        {
            
        }
        public override double Calculation()
        {
            Expression calcConditional = new Expression(this.Replace(this.Condition));
            double conditionalResult = calcConditional.calculate();
            string calcResult;

            if(conditionalResult == 1)
            {
                calcResult =  this.Replace(this.Value);
            }
            else
            {
                calcResult = this.Replace(this.ValueFalse);
            }
            
            return Convert.ToDouble(calcResult);       
        }
    }
}
