using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace JCA.UL.Pricing.Infrastructure
{
    public class CalcArray
    {
        List<ICalc> Elements {get; set;}
        public CalcArray()
        {
            Elements = new List<ICalc>();
        }
        

        public void AddElements(BsonArray ba)
        {
            foreach (var item in ba)
            {
                var array = item.ToBsonDocument();
                var element = array.GetElement("CalcType").Value;
                Type type = Type.GetType("DynamicPricing.Calc" + element);
                ICalc obj =  (ICalc)BsonSerializer.Deserialize(array,type);
                obj.AddReference(this);
                Elements.Add(obj);
            }
        }

        public double Find(string name)
        {
            return Elements.First(x => x.GetName() == name).Calculation();
        }

    }
    
}