using System;
using System.Linq;
using System.Linq.Expressions;

namespace DynamicPricing
{
    public interface ICalc
    {
        string Calculation(params string[] args);

        string GetName();
        string GetCalcType();

        void AddReference(CalcArray ca);

    }
}