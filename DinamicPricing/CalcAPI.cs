
using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace DynamicPricing
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
        public string Value { get; set; }
        public BsonDocument InParameters { get; set; }
        public BsonDocument OutParameters { get; set; }
        private CalcArray ChildElement { get; set; }

        public override string Calculation(params string[] args)
        {
            if (ChildElement.Count() == 0)
            {
                string[] items = new string[]
                {
                    CalcArray.Find("CalcInput.origem"), CalcArray.Find("CalcInput.destinos"),
                    CalcArray.Find("CalcInput.categoria"), CalcArray.Find("CalcInput.eixos"),
                    CalcArray.Find("CalcInput.calcular-volta"), CalcArray.Find("CalcInput.format")
                };

                List<string> apiParams = new List<string>();

                foreach (var values in InParameters)
                {
                    string vStr = values.Name;
                    apiParams.Add(vStr);
                }

                foreach (string item in items)
                {
                    string iStr = item;
                    apiParams.Add(iStr);
                }

                StringBuilder sb = new StringBuilder();
                int c = apiParams.Count();
                for (int j = 0; j < c; j++)
                {
                    if (j > (c/2))
                    {
                        break;
                    }

                    int k = (j + ((c/2)-1));
                    sb.Append("&" + apiParams[j] + "=" + apiParams[k]);
                }

                var urlApi = (Url + AccessToken + sb);
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlApi);
                Console.WriteLine(urlApi);

                // httpWebRequest.ContentType = "application/json; charset=utf-8";
                // httpWebRequest.Method = "GET";

                // var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                // using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                // {
                //     var responseText = streamReader.ReadToEnd();
                //     JObject jObj = JsonConvert.DeserializeObject<dynamic>(responseText);

                    JObject jObj = JObject.Parse(File.ReadAllText(@"MongoDocuments/returnQualp.json"));

                    string key = null;
                    string arg = null;
                    string valArg = null;
                    string valTokenArg = null;

                    var valResDec = 0m;
                    var valResStr = "";

                    arg = args[0].Split(".").Last();
                    
                    foreach (JToken token in jObj.FindTokens(arg))
                    {
                        String condAn = token.Path.Split(".").First().ToString();
                        String tokenAn = token.Path.ToString(), argAn = args[0];
                        string pattern = @"\[([a-z A-Z _ . 0-9]+)\]";

                        foreach(Match match in Regex.Matches(condAn, pattern))
                        {

                          key = Convert.ToString(match.Groups[0].Success);          
                
                        }

                        var resAn = tokenAn == argAn ? true : key != null ? true:false;
                        
                        if (resAn == true)
                        {
                            valTokenArg = token.ToString();

                            if (key != null)
                            {
                                valResDec += Convert.ToDecimal(valTokenArg);
                                valTokenArg = valResDec.ToString();
                            }
                            else
                            {
                                valResStr = Convert.ToString(valTokenArg);
                                valTokenArg = valResStr;
                            }

                            valArg = valTokenArg.ToString();
                        }

                    }

                    ChildElement.AddElements(new CalcFixed() { Name = args[0], CalcType = "Fixed", Value = valArg });
                // }

            }

            return (args.Count() > 0) ? ChildElement.Find(args[0], false) : string.Empty;
        }
    }
}
