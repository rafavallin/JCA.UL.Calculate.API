using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace DynamicPricing
{
    public class CalcBase : ICalc
    {
        public string Name { get; set; }
        public string Empresa { get; set; }
        public string CalcType { get; set; }
        public CalcArray CalcArray { get; set; }
        
        public virtual string Calculation(params string[] args)
        {
            return "";      
        }

        public string GetName()
        {
            return this.Name;
        }
        public string GetCalcType()
        {
            return this.CalcType;
        }

        protected string Replace(string calc)

        {

            string pattern = @"\[([a-z A-Z _ . 0-9]+)\]";

            foreach(Match match in Regex.Matches(calc, pattern))

            {

                string key = match.Groups[1].Value;

                string toReplace = match.Value;

                string value = this.CalcArray.Find(key);               

                calc = calc.Replace(toReplace, value);
            }
 
            return calc;

        }
        public void AddReference(CalcArray ca)
        {
            CalcArray =  ca;
        }

    }
}