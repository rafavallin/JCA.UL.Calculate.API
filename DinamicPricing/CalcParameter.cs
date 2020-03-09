using System;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;
using org.mariuszgromada.math.mxparser;


namespace DynamicPricing
{
    class CalcParameter : CalcBase
    {
        public CalcParameter()
        {
            
        }
        public string Value { get; set; } // ValorFinal, 50
        public override string Calculation(params string[] args)
        {           
            return this.Replace(this.Value);
        }
    }
}
