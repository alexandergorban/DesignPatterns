using System;
using System.Collections.Generic;
using System.Text;
using StrategyPattern.Business.Models;

namespace StrategyPattern.Business.Strategies.SalesTax
{
    public class SwedenSalesTaxStrategy : ISalesTaxStrategy
    {
        public decimal GetTaxFor(Order order)
        {
            var destination = order.ShippingDetails.DestinationCountry.ToLowerInvariant();
            var origin = order.ShippingDetails.OriginCountry.ToLowerInvariant();

            if (destination == origin)
            {
                return order.TotalPrice * 0.25m;
            }

            return 0;
        }
    }
}
