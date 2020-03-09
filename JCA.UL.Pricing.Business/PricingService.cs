using System;
using JCA.UL.Pricing.Domain.DTO;
using JCA.UL.Pricing.Repository;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using org.mariuszgromada.math.mxparser;
using DynamicPricing;
using MongoDB.Driver;
using MongoDB.Bson;

namespace JCA.UL.Pricing.Business
{
    public class PricingService
    {
        private readonly ProductRepository _productRep;
        private readonly ParameterRepository _parameterRep;
        private readonly RequestRepository _requestRep;
        private readonly RuleRepository _ruleRep;

        public PricingService(ProductRepository productRep, ParameterRepository parameterRep, RuleRepository ruleRep, RequestRepository requestRep)
        {
            _productRep = productRep;
            _parameterRep = parameterRep;
            _ruleRep = ruleRep;
            _requestRep = requestRep;
        }
        
        public Product GetProduct(string strProduct)
        {
            return _productRep.Find(x => x.strProduct == strProduct)
                    .Include(x => x.productParameters)
                    .Select(x => (Product)x)
                    .FirstOrDefault();
        }

        public BsonDocument GetMongoDocument(string strProduct)
        {
            var db = conMongo.GetConnection().GetDatabase("PricingDB");

            var collection = db.GetCollection<BsonDocument>("PricingComposition");

            return collection.Find(new BsonDocument{ {"Empresa",strProduct}  }).FirstOrDefault();
        }
        
        public List<Rule> GetRules(int idParameter)
        {
            return _ruleRep.Find(x => x.idParameter == idParameter)
            .Select(x => (Rule)x)
            .ToList();
        }
        public Request InsertRequest(string origem, string mensagem, string status)
        {
            Request r = new Request()
                            {
                                dtRequest = DateTime.Now,
                                strRequestOrigin = origem,
                                strRequestStatus = status,
                                strRequestMessage = mensagem
                            };
            return _requestRep.Save(r);
        }

        public List<RequestCalc> CalcExec(List<RequestCalc> rc)
        {
            List<RequestCalc> r = new List<RequestCalc>();
            foreach (var item in rc)
            {
                string stringSplit = item.strRequestCalcValue, value = item.strRequestCalcValue;

                while (stringSplit.Any(x => char.IsLetter(x)))
                {
                    string teste = string.Concat(stringSplit.Where(x => (char.IsLetter(x))));
                    string[] calcSplit = stringSplit.Split(']');
            
                    if(calcSplit.Count() > 1)
                    {
                        value = stringSplit;
                        foreach (var calc in calcSplit)
                        {
                            string newCalc = string.Concat(calc.Where(x => (char.IsLetter(x) || char.IsNumber(x)) && !char.IsSymbol(x) && !char.IsWhiteSpace(x)));
                            var existsParameter = rc.Where(x => x.strRequestCalcName == newCalc).FirstOrDefault();
                            if(existsParameter != null)
                            {
                                value = value.Replace(newCalc, existsParameter.strRequestCalcValue);                                       
                            }
                                
                        }
                    }
                    else
                    {
                        var existsParameter = rc.Where(x => x.strRequestCalcName == item.strRequestCalcValue).FirstOrDefault();
                        value = (existsParameter != null)  ?  existsParameter.strRequestCalcValue  :  item.strRequestCalcValue;
                        break;
                    }

                    stringSplit = value;
                }
                
                decimal validaValor;
                if(decimal.TryParse(stringSplit.Replace("]", String.Empty), out validaValor))
                {
                    Expression calcExpression = new Expression(stringSplit.Replace("]", String.Empty));
                    double result = calcExpression.calculate();
                    r.Add(new RequestCalc(){ idRequest = item.idRequest, strRequestCalcName = item.strRequestCalcName, strRequestCalcValue = result.ToString()});
                }else
                {
                    r.Add(new RequestCalc(){ idRequest = item.idRequest, strRequestCalcName = item.strRequestCalcName, strRequestCalcValue = value.Replace("]", String.Empty)});
                }
                

                
                
            }
            return r;
        }
        

        public bool UpdateRequestAsNoTracking(Request entity)
        {
            try
            {
                if (entity.idRequest != 0)
                {
                    var r = _requestRep.Find(x => x.idRequest == entity.idRequest).AsNoTracking().FirstOrDefault();
                    r.strRequestStatus = entity.strRequestStatus;
                    r.strRequestMessage = entity.strRequestMessage;
                    _requestRep.Update(r);
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                // TODO: Logar Exception
                throw new Exception(ex.Message);
            }
        }
    }
}