using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using StrategyPattern.Business.Models;

namespace StrategyPattern.Business.Strategies.Shipping
{
    public class PostNordShippingStrategy : IShippingStrategy
    {
        public void Ship(Order order)
        {
            using (var client = new HttpClient())
            {
                Console.WriteLine("Order is shipped with PostNord");
            }
        }
    }
}
