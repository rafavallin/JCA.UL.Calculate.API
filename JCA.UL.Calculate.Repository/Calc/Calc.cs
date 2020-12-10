using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace JCA.UL.Calculate.Repository.Calc
{
    public abstract class Calc<T> : ICalc, IDisposable where T : class
    {
        public void AddReference(ICalcArray ca)
        {
            throw new NotImplementedException();
        }

        public string Calculation(params string[] args)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public string GetCalcType()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }
    }
}