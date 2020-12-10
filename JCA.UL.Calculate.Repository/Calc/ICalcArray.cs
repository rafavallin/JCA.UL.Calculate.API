using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;

namespace JCA.UL.Calculate.Repository.Calc
{
    public interface ICalcArray
    {
        
        void AddElements(BsonArray ba);
        List<string> GetCalcTypeItems(string calcType);
        void AddElements(ICalc ca);
        int Count();     

        string Find(string name, bool split = true);

    }
}