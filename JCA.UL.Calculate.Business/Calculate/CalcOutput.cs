using System;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;
using Newtonsoft.Json.Linq;
using JCA.UL.Calculate.Repository.Calc;

namespace JCA.UL.Calculate.Business.Calculate
{
    public class CalcOutput : CalcBase
    {
        public CalcOutput()
        {
            Name = "CalcOutput";
            CalcType = "CalcOutput";
        }
        public string Value { get; set; } // ValorFinal, 50
        public override string Calculation(params string[] args)
        {
            
            return this.Replace(this.Value);  
        }
    }
}
