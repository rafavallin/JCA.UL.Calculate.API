using System;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace DynamicPricing
{
    public class CalcOutput : CalcBase
    {
        public CalcOutput()
        {
            ChildElement = new CalcArray();
            Name = "CalcOutput";
            CalcType = "CalcOutput";
        }

        private CalcArray ChildElement { get; set; } 


        public void AddElements(ICalc c)
        {
            ChildElement.AddElements(c);
        }


        public override string Calculation(params string[] args)
        {
            
            return ChildElement.Find(args[0]);    
        }
    }
}
