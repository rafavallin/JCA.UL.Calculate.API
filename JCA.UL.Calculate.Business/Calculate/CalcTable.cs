using System;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;
using org.mariuszgromada.math.mxparser;
using Newtonsoft.Json;
using System.Collections.Generic;
using JCA.UL.Calculate.Domain.Context;

namespace JCA.UL.Calculate.Business.Calculate
{
    class CalcTable : CalcBase
    {
        public CalcTable()
        {
           ChildElement = new CalcArray();
        }
        public string Conditional { get; set; }
        public PricingContext _context { get; set; }
        public BsonDocument Result { get; set; } 
        private CalcArray ChildElement { get; set; } 
        private List<object> OutParameters { get; set; } 
        public override string Calculation(params string[] args)
        {
            if(ChildElement.Count() == 0)
            {
                var db = PricingContext.conMongo.GetConnection().GetDatabase("PricingDB");

                var collection = db.GetCollection<BsonDocument>("PricingTable");

                var cond = JsonConvert.DeserializeObject<Dictionary<string, object>>(this.Replace(Conditional));

                BsonDocument filter = new BsonDocument{{ "Empresa", Empresa}, { "Name", Name }}; 

                var cond2 = new Dictionary<string,object>();
                foreach (var key in cond.Keys){cond2.Add("Data."+key, cond[key]);}
                
                filter.AddRange(cond2);

                var array = new []
                {
                    new BsonDocument("$unwind", "$Data"),
                    new BsonDocument("$match", filter)
                };


                var documents = collection.Aggregate<BsonDocument>(array).ToList();
                
                foreach (var item in documents)
                {
                    var data = JsonConvert.DeserializeObject<Dictionary<string,object>>(item.GetElement("Data").Value.ToString());

                    foreach (var arg in Result.ToArray())
                    {
                        if(data.ContainsKey(arg.Name))
                            ChildElement.AddElements(new CalcFixed() { Name= arg.Name, CalcType= "Fixed", Value = data[arg.Name].ToString()});
                    }
                    
                }
            }
            return (args.Count() > 0) ? ChildElement.Find(args[0]) : string.Empty;  
        }



    }
}

//db.PricingTable.aggregate([{$unwind:"$Data"},{$match:{Empresa: "Turismo", Name: "Pricing", "Data.BusStatus":"Parado"}}])

//db.PricingTable.aggregate([{$unwind:"$Data"},{$match:{"Data.BusStatus":"Disposição"}},{$project:{"Data.BusStatus":1,"Data.BusType":1,"Data.BusKMValue":1,"Data.BusDiaryValue":1,}}])0