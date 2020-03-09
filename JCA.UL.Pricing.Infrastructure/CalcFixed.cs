using System;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;


namespace JCA.UL.Pricing.Infrastructure
{
    class CalcFixed : CalcBase
    {
        public CalcFixed()
        {
            
        }
        public override double Calculation()
        {
            
            return Convert.ToDouble(this.Value);       
        }
    }
}
