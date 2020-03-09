using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace DynamicPricing
{
    public class CalcArray
    {
        List<ICalc> Elements { get; set; }
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
                ICalc obj = (ICalc)BsonSerializer.Deserialize(array, type);
                obj.AddReference(this);
                Elements.Add(obj);
            }
        }
        public List<string> GetCalcTypeItems(string calcType)
        {
            return Elements.Where( x=> x.GetCalcType() == calcType).Select(y => y.GetName()).ToList();
        }
        public void AddElements(ICalc ca)
        {
            Elements.Add(ca);
        }
        public int Count()
        {
            return Elements.Count();
        }      

        public string Find(string name, bool split = true)
        {
            string[] splitReturn = name.Split('.');

            if (splitReturn.Length > 1 && split)
            {

                return Elements.First(x => x.GetName() == splitReturn[0]).Calculation(String.Join(".", splitReturn.Skip(1)));
            }
            else
            {
                return Elements.First(x => x.GetName() == name).Calculation();
            }
        }

    }

}