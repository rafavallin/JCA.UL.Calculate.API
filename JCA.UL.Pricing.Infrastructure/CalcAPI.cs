using System;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;


namespace JCA.UL.Pricing.Infrastructure
{
    class CalcAPI : CalcBase
    {
        public CalcAPI()
        {
            
        }

        public string Url { get; set; }
        public string MethodType { get; set; }
        public string Auth { get; set; }
        public string UrlAuth { get; set; }
        public string UserAuth { get; set; }
        public string PassAuth { get; set; }
        public string TokenAuth { get; set; }
        public BsonDocument InParameters { get; set; } 
        public BsonDocument OutParameters { get; set; } 
        
        public override double Calculation()
        {
            
            return Convert.ToDouble(this.Value);       
        }
    }
}
