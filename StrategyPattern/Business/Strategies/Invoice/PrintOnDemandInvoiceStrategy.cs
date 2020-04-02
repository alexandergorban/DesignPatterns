using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using StrategyPattern.Business.Models;

namespace StrategyPattern.Business.Strategies.Invoice
{
    public class PrintOnDemandInvoiceStrategy : IInvoiceStrategy
    {
        public void Generate(Order order)
        {
            using (var client = new HttpClient())
            {
                var content = JsonConvert.SerializeObject(order);

                client.BaseAddress = new Uri("http://localhost:8080");
                client.PostAsync("/print-on-demand", new StringContent(content));
            }
        }
    }
}
