using System;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System.Linq;
using org.mariuszgromada.math.mxparser;
using System.Collections.Generic;


namespace JCA.UL.Calculate.Business.Calculate
{
    class CalcExpression : CalcBase
    {
        public CalcExpression()
        {
            
        }
        public string Value { get; set; }
        public string ResultadoFinal { get; set; } // ValorFinal, 50
        public override string Calculation(params string[] args)
        {
            if(ResultadoFinal == null)
            {
                Expression calcExpression = new Expression(this.Replace(this.Value));
                ResultadoFinal= calcExpression.calculate().ToString();
            }

            return ResultadoFinal;
        }



    }
}
