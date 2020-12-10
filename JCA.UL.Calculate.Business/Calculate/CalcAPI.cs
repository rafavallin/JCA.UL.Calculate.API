
using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JCA.UL.Calculate.Repository.Calc;

namespace JCA.UL.Calculate.Business.Calculate
{
    public static class FindInObjects
    {
        public static List<JToken> FindTokens(this JToken containerToken, string name)
        {
            List<JToken> matches = new List<JToken>();
            FindTokens(containerToken, name, matches);
            return matches;
        }

        private static void FindTokens(JToken containerToken, string name, List<JToken> matches)
        {
            if (containerToken.Type == JTokenType.Object)
            {
                foreach (JProperty child in containerToken.Children<JProperty>())
                {
                    if (child.Name == name)
                    {
                        matches.Add(child.Value);
                    }
                    FindTokens(child.Value, name, matches);
                }
            }
            else if (containerToken.Type == JTokenType.Array)
            {
                foreach (JToken child in containerToken.Children())
                {
                    FindTokens(child, name, matches);
                }
            }
        }
    }
    class CalcAPI : CalcBase
    {
        List<ICalc> Elements { get; set; }
        public CalcAPI()
        {
            ChildElement = new CalcArray();
        }
        public string Url { get; set; }
        public string MethodType { get; set; }
        public string Auth { get; set; }
        public string UrlAuth { get; set; }
        public string UserAuth { get; set; }
        public string PassAuth { get; set; }
        public string TokenAuth { get; set; }
        public string AccessToken { get; set; }
        public string Filter { get; set; }
        public string Value { get; set; }
        public BsonDocument InParameters { get; set; }
        public BsonDocument OutParameters { get; set; }
        private CalcArray ChildElement { get; set; }
       
        public override string Calculation(params string[] args)
        {
            if (ChildElement.Count() == 0)
            {
                List<string> calcInputParams = new List<string>();
                Dictionary<string, string> calcInputParamsNew = new Dictionary<string, string>();

                StringBuilder urlApiParams = new StringBuilder();

                var itemsInParameters = InParameters.ToList();  

                var itemsCalcInput = CalcArray.GetCalcTypeItems("Parameter").ToList();

                foreach (var item in itemsInParameters)
                {
                    calcInputParamsNew.Add(item.Name.ToString(),this.Replace(item.Value.ToString()));
                }
                
                foreach (var item in calcInputParamsNew)
                { 
                    urlApiParams.Append("&" + item.Key + "=" + item.Value);
                }


                var urlApi = (Url + AccessToken + urlApiParams);
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlApi);

                Console.WriteLine(urlApi);

                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = MethodType;

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                 using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                 {
                     var responseText = streamReader.ReadToEnd();
                     JObject jObj = JsonConvert.DeserializeObject<dynamic>(responseText);

                    //JObject jObj = JObject.Parse(File.ReadAllText(@"MongoDocuments/returnQualp.json"));

                    string key = null;
                    string arg = null;
                    string valArg = null;
                    string valTokenArg = null;

                    

                    arg = args[0].Split(".").Last();

                    foreach (var item in OutParameters.ToList())
                    {
                        var valResDec = 0m;
                        var valResDecAux = 0m;
                        var valResStr = "";
                        foreach (JToken token in jObj.FindTokens(item.Name.ToString()))
                        {
                            key = null;
                            String condAn = token.Path.Split(".").First().ToString();
                            String tokenAn = token.Path.ToString(), argAn = args[0];
                            string pattern = @"\[([a-z A-Z _ . 0-9]+)\]";

                            foreach(Match match in Regex.Matches(condAn, pattern))
                            {

                            key = Convert.ToString(match.Groups[0].Success);          
                    
                            }

                            var resAn = tokenAn == argAn ? true : key != null ? true:false;
                            

                            if (resAn == true && (Filter != null ? (tokenAn.Contains(Filter)) : true))
                            {
                                valTokenArg = token.ToString();

                                if(Decimal.TryParse(valTokenArg, out valResDecAux))
                                {
                                    if (key != null)
                                    {
                                        valResDec += valResDecAux;
                                        valTokenArg = valResDec.ToString();
                                    }
                                    else
                                    {
                                        valResStr = Convert.ToString(valTokenArg);
                                        valTokenArg = valResStr;
                                    }
                                }
                                    valArg = valTokenArg.ToString(); 
                            }

                        }

                        ChildElement.AddElements(new CalcFixed() { Name = item.Value.ToString(), CalcType = "Fixed", Value = valArg });
                    }
                 }

            }

            return (args.Count() > 0) ? ChildElement.Find(args[0], false) : string.Empty;
        }
    }
}
