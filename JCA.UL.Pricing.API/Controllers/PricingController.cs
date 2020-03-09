using System;
using JCA.UL.Pricing.Business;
using JCA.UL.Pricing.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Json;
using System.Collections.Generic;
using JCA.UL.Pricing.Shared.Enum;
using DynamicPricing;
using Newtonsoft.Json.Linq;
using MongoDB.Driver;
using MongoDB.Bson;

namespace UL.UTP.Pricing.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PricingController : Controller
    {
        private readonly PricingService _PricingService;
        private readonly IConfiguration _configuration;
        private string _returnMessage;
        //private Product _product;
        private List<string> _parametros;
        private BsonDocument _documento;

        public PricingController(PricingService PricingService, 
                                 IConfiguration configuration)
        {
            _PricingService = PricingService;
            _configuration = configuration;
            
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult PricingCalculator([FromBody]JsonObject obj)
        {
            var calcArray = new CalcArray();
            Request request = new Request();
            var calcInput = new CalcInput();
            var calcOutput = new CalcOutput();

            

            try
            {
                JsonValue value = null;
                
                _returnMessage = "Por favor informe o produto correto." ;
                if(obj == null || !obj.TryGetValue("Product", out value))
                {
                    request = _PricingService.InsertRequest("OrigemProvisória", _returnMessage, EnumRequestStatus.Erro.ToString());
                    return new JsonResult(new {sucesso = false, retorno = _returnMessage });
                }
                //_product = _PricingService.GetProduct(value);
                _documento = _PricingService.GetMongoDocument(value);
                calcArray.AddElements(_documento.GetElement("Elements").Value.AsBsonArray);

                calcInput.AddElements(new CalcFixed(){ Name = "BusStatus", Value = "Parado", CalcType = "Fixed"});
                calcInput.AddElements(new CalcFixed(){ Name = "BusType", Value = "DD", CalcType = "Fixed"});

                calcOutput.AddElements(new CalcFixed(){ Name = "Pricing.BusKMValue", Value = "[Pricing.BusKMValue]", CalcType = "Fixed"});
                calcOutput.AddElements(new CalcFixed(){ Name = "Pricing.BusDiaryValue", Value = "[Pricing.BusDiaryValue]", CalcType = "Fixed"});
                
                calcArray.AddElements(calcInput);
                calcArray.AddElements(calcOutput);

                _parametros = calcArray.GetCalcTypeItems("Parameter");

                _returnMessage = "Por favor enviar os parametros de entrada corretamente.";
                if(!valParameters(obj)) 
                { 
                    request = _PricingService.InsertRequest("OrigemProvisória", _returnMessage, EnumRequestStatus.Erro.ToString());
                    return new JsonResult(new {sucesso = false, retorno = _returnMessage});
                }
                
                _returnMessage = "Testando a API" ;
                request = _PricingService.InsertRequest("OrigemProvisória", _returnMessage, EnumRequestStatus.Processado.ToString());
                
                Console.WriteLine(calcArray.Find("BusStatus"));
                Console.WriteLine(calcArray.Find("BusType"));
                Console.WriteLine(calcArray.Find("Pricing.BusKMValue"));
                Console.WriteLine(calcArray.Find("Pricing.BusDiaryValue"));
                // List<RequestCalc> calcReturn = fillCalcReturn(request.idRequest, obj);
                // if(calcReturn != null)
                //     calcReturn = _PricingService.CalcExec(calcReturn);
                
                return new JsonResult(new {sucesso = true, retorno = "" });
                
            }
            catch (Exception ex)
            {
                _returnMessage = "Testando a API" ;
                request = _PricingService.InsertRequest("OrigemProvisória", _returnMessage, EnumRequestStatus.Processado.ToString());
                return new JsonResult(new {sucesso = false, retorno = "Erro ao Logar: " + ex.Message });
            }
        }

        private bool valParameters(JsonObject obj)
        {
            foreach (var p in _parametros)
            {
                if(!obj.ContainsKey(p))
                {
                   _returnMessage = "Não foi encontrado o parâmetro: " + p;
                   return false;
                }  
            } 
            return true;                    
        }

        // private List<RequestCalc> fillCalcReturn(int idrequest, JsonObject obj)
        // {
        //     List<RequestCalc> r = new List<RequestCalc>();
        //     JsonValue value = null;

        //     foreach (var p in _product.productParameters)
        //     {
        //         if(p.strParameterFunc == '1' && obj.TryGetValue(p.strParameter, out value))
        //         {
        //             if(!(value).ToString().Contains("\""))
        //                 r.Add(new RequestCalc(){strRequestCalcName = p.strParameter, strRequestCalcValue = (value).ToString(), idRequest = idrequest });
        //             else  
        //                 r.Add(new RequestCalc(){strRequestCalcName = p.strParameter, strRequestCalcValue = value, idRequest = idrequest });  
        //         }
        //         else if(p.strParameterFunc == '2')
        //         {
        //             var listRules = _PricingService.GetRules(p.idParameter);
        //             foreach (var item in listRules)
        //                r.Add(new RequestCalc(){strRequestCalcName = p.strParameter, strRequestCalcValue = item.strRuleValue, idRequest = idrequest }); 
                    
        //         }
                
        //     } 
        //     return r;   
        // }
    }
}