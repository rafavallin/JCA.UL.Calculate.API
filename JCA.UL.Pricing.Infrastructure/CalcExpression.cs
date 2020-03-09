using System;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;
using org.mariuszgromada.math.mxparser;


namespace JCA.UL.Pricing.Infrastructure
{
    class CalcExpression : CalcBase
    {
        public CalcExpression()
        {
            
        }
        public override double Calculation()
        {
            Expression calcExpression = new Expression(this.Replace(this.Value));
            double result = calcExpression.calculate();

            return result;       
        }



    }
}
