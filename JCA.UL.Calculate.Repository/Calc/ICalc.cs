using System;
using System.Linq;
using System.Linq.Expressions;

namespace JCA.UL.Calculate.Repository.Calc
{
    public interface ICalc
    {
        string Calculation(params string[] args);

        string GetName();
        string GetCalcType();

        void AddReference(ICalcArray ca);

    }
}