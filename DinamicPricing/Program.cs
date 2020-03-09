using System;
using System.IO;
using System.Net;
using System.Text;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;
using DynamicPricing;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DynamicPricing
{
    public static class conMongo
    {
        static MongoClient conn = null;  
        public static MongoClient GetConnection()
        {
            if(conn == null)
                conn = new MongoClient("mongodb://localhost:27017");  

             return conn;   
        }
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            JObject x = new  JObject();
            var calcInput = new CalcInput();

            calcInput.AddElements(new CalcFixed(){ Name = "BusStatus", Value = "Parado", CalcType = "Fixed"});
            calcInput.AddElements(new CalcFixed(){ Name = "BusType", Value = "DD", CalcType = "Fixed"});
            
            calcInput.AddElements(new CalcFixed(){ Name = "KgPacote", Value = "25", CalcType = "Fixed"});
            calcInput.AddElements(new CalcFixed(){ Name = "ValNotaFiscal", Value = "15000", CalcType = "Fixed"});
            
            
            calcInput.AddElements(new CalcFixed(){ Name = "KgPacote", Value = "25", CalcType = "Fixed"});
            calcInput.AddElements(new CalcFixed(){ Name = "ValNotaFiscal", Value = "15000", CalcType = "Fixed"});
            calcInput.AddElements(new CalcFixed(){ Name = "ValAltura", Value = "30", CalcType = "Fixed"});
            calcInput.AddElements(new CalcFixed(){ Name = "ValLargura", Value = "30", CalcType = "Fixed"});
            calcInput.AddElements(new CalcFixed(){ Name = "ValComprimento", Value = "40", CalcType = "Fixed"});

            calcInput.AddElements(new CalcFixed(){ Name = "origem", Value = "-23.75458,-46.6646", CalcType = "Fixed"});
            calcInput.AddElements(new CalcFixed(){ Name = "destinos", Value = "-22.21816,-49.95057", CalcType = "Fixed"});
            calcInput.AddElements(new CalcFixed(){ Name = "categoria", Value = "onibus", CalcType = "Fixed"});
            calcInput.AddElements(new CalcFixed(){ Name = "eixos", Value = "4", CalcType = "Fixed"});
            calcInput.AddElements(new CalcFixed(){ Name = "calcular-volta", Value = "nao", CalcType = "Fixed"});
            calcInput.AddElements(new CalcFixed(){ Name = "format", Value = "json", CalcType = "Fixed"});

            calcInput.AddElements(new CalcFixed(){ Name = "pedagios.tarifa", Value = "237,6", CalcType = "Fixed"});
            calcInput.AddElements(new CalcFixed(){ Name = "distancia.valor", Value = "463", CalcType = "Fixed"});
            
            //string Empresa = "Buslog";    
            string Empresa = "Turismo";    

            
            var db = conMongo.GetConnection().GetDatabase("PricingDB");

            var collection = db.GetCollection<BsonDocument>("PricingComposition");

            var documents = collection.Find(new BsonDocument{ {"Empresa",Empresa}  }).FirstOrDefault();

            var calcArray = new CalcArray();

            calcArray.AddElements(documents.GetElement("Elements").Value.AsBsonArray);

            var teste = calcArray.GetCalcTypeItems("Parameter");

            calcArray.AddElements(calcInput);

            //calcArray.Find("CalculoQtdMotoristas");

            Console.WriteLine(calcArray.Find("BusStatus"));
            Console.WriteLine(calcArray.Find("BusType"));
            // Console.WriteLine(calcArray.Find("ValorTotalKM"));
            // Console.WriteLine(calcArray.Find("ValorFinal"));
            // Console.WriteLine(calcArray.Find("APIQualp"));
            Console.WriteLine(calcArray.Find("Pricing.BusKMValue"));
            Console.WriteLine(calcArray.Find("Pricing.BusDiaryValue"));

            // Console.WriteLine(calcArray.Find("ValColetaFinal"));
            // Console.WriteLine(calcArray.Find("ValEntregaFinal"));
            // Console.WriteLine(calcArray.Find("AdValorem"));
            // Console.WriteLine(calcArray.Find("ValGrisFinal"));
            // Console.WriteLine(calcArray.Find("ValCubagem"));

            // Console.WriteLine(calcArray.Find("APIQualp.distancia.valor"));
            //Console.WriteLine(calcArray.Find("APIQualp.pedagios.tarifa"));
            

        }
    }
}
