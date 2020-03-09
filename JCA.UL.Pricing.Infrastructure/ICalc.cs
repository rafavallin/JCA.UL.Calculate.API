using System;
using System.Linq;
using System.Linq.Expressions;

namespace JCA.UL.Pricing.Infrastructure
{
    public interface ICalc
    {
        double Calculation();

        string GetName();

        void AddReference(CalcArray ca);

    }
}