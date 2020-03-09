using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace JCA.UL.Pricing.Infrastructure
{
    public class CalcBase : ICalc
    {
        public string Name { get; set; }
        public string CalcType { get; set; }
        public string Value { get; set; }
        public string ValueFalse { get; set; }
        public string Condition { get; set; }



        public CalcArray CalcArray { get; set; }
        
        public virtual double Calculation()
        {
            return 0;      
        }

        public string GetName()
        {
            return this.Name;
        }

        protected string Replace(string calc)

        {

            string pattern = @"\[([a-z A-Z _ . 0-9]+)\]";

            foreach(Match match in Regex.Matches(calc, pattern))

            {

                string key = match.Groups[1].Value;

                string toReplace = match.Value;

                double value = this.CalcArray.Find(key);

                calc = calc.Replace(toReplace, value.ToString());

            }
 

            return calc;

        }
        public void AddReference(CalcArray ca)
        {
            CalcArray =  ca;
        }

    }
}