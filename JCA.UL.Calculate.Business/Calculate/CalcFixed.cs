using System;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;


namespace JCA.UL.Calculate.Business.Calculate
{
    public class CalcFixed : CalcBase
    {
        public CalcFixed()
        {
            
        }
        public string Value { get; set; }

        public override string Calculation(params string[] args)
        {
            
            return this.Replace(this.Value);       
        }
    }
}
