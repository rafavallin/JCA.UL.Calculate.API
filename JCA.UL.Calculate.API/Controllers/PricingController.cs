using System;
using JCA.UL.Calculate.Business;
using JCA.UL.Calculate.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using MongoDB.Driver;
using MongoDB.Bson;
using JCA.UL.Calculate.Business.Calculate;
using Newtonsoft.Json;
using System.Text.Json;

namespace UL.UTP.Calculate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingController : Controller
    {
        
        PricingService _PricingService;
        private List<string> _parametros;
        private Dictionary<string,string> _retornos;
        private string _returnMessage;
         private BsonDocument _documento;
         CalcArray calcArray;
         CalcInput calcInput = new CalcInput();
         CalcOutput calcOutput = new CalcOutput();
        public PricingController(PricingService PricingService)
        {
            _PricingService = PricingService;
            calcArray = new CalcArray();
            calcInput = new CalcInput();
            calcOutput = new CalcOutput();
            _retornos = new Dictionary<string,string>();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
          return Ok();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody]JObject obj)
        {          
            try
            {
                _returnMessage = "Por favor informe o produto correto." ;
                string sistema = obj["Sistema"].ToString();
                if(sistema == null)
                {
                    return BadRequest(_returnMessage);
                }

                _documento = _PricingService.GetMongoDocument(sistema);
                calcArray.AddElements(_documento.GetElement("Elements").Value.AsBsonArray);
                
                _parametros = calcArray.GetCalcTypeItems("Parameter");
                if(!CheckInputParameters(_parametros, obj))
                {
                    return BadRequest(_returnMessage);  
                }
                _parametros = calcArray.GetCalcTypeItems("Output");

                foreach (var item in _parametros)
                {
                   Console.WriteLine(calcArray.Find(item)); 
                   _retornos.Add(item, calcArray.Find(item));
                }
                
                return Ok(_retornos);  
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private bool CheckInputParameters(List<string> paramList, JObject obj)
        {
            JToken jsonValue = null;
            _returnMessage = "Por favor preencher o(s) parâmetro(s) de entrada: ";
            bool status = true;

            for (int i = 0; i < paramList.Count; i++) 
            {
                if(!obj.TryGetValue(paramList[i], out jsonValue))
                {
                    status = false;
                    if (i == paramList.Count - 1 || i == 0) 
                        _returnMessage = _returnMessage + " "+ paramList[i] + "";
                    else
                        _returnMessage = _returnMessage + " ,"+ paramList[i]  + " ";
                }else
                {  
                    calcInput.AddElements(new CalcFixed(){ Name = paramList[i], Value = jsonValue.ToString(), CalcType = "Fixed"});       
                }
            }
            
                        
            calcArray.AddElements(calcInput);
            return status;
        }
    }
}