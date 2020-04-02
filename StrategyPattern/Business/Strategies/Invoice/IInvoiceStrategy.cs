using System;
using System.Collections.Generic;
using System.Text;
using StrategyPattern.Business.Models;

namespace StrategyPattern.Business.Strategies.Invoice
{
    public interface IInvoiceStrategy
    {
        public void Generate(Order order);
    }
}
