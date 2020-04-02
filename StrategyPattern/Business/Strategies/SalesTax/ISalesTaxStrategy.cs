using System;
using System.Collections.Generic;
using System.Text;
using StrategyPattern.Business.Models;

namespace StrategyPattern.Business.Strategies.SalesTax
{
    public interface ISalesTaxStrategy
    {
        public decimal GetTaxFor(Order order);
    }
}
