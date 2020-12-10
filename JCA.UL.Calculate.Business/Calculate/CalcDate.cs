using System;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;


namespace JCA.UL.Calculate.Business.Calculate
{
    public class CalcDate : CalcBase
    {
        public CalcDate()
        {
            
        }
        public string Value { get; set; }
        public string Form { get; set; }

        public override string Calculation(params string[] args)
        {
            string[] arrayDate = this.Replace(this.Value).Split(";");
            DateTime d1,d2;
            TimeSpan date;
            int i2;
            DateTime.TryParse(arrayDate[0], out d1);
            if(Form == "Dif")
            {
                DateTime.TryParse(arrayDate[1], out d2);
                date = d1 - d2;
                return date.Days.ToString();
            }
            else
            {
                int.TryParse(arrayDate[1], out i2);
                return d1.AddDays(i2).ToString();
            }    
        }
    }
}
