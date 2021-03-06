﻿using System;
using System.Collections.Generic;
using StrategyPattern.Business.Models;
using StrategyPattern.Business.Strategies.Invoice;
using StrategyPattern.Business.Strategies.SalesTax;
using StrategyPattern.Business.Strategies.Shipping;

namespace StrategyPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Debug

            var orders = new[] {
                new Order
                {
                    ShippingDetails = new ShippingDetails
                    {
                        OriginCountry = "Sweden"
                    }
                },
                new Order
                {
                    ShippingDetails = new ShippingDetails
                    {
                        OriginCountry = "USA"
                    }
                },
                new Order
                {
                    ShippingDetails = new ShippingDetails
                    {
                        OriginCountry = "Sweden"
                    }
                },
                new Order
                {
                    ShippingDetails = new ShippingDetails
                    {
                        OriginCountry = "USA"
                    }
                },
                new Order
                {
                    ShippingDetails = new ShippingDetails
                    {
                        OriginCountry = "Singapore"
                    }
                }
            };

            Print(orders);

            Console.WriteLine();
            Console.WriteLine("Sorting..");
            Console.WriteLine();

            Array.Sort(orders, new OrderAmountComparer());

            Print(orders);

            #endregion

            Console.WriteLine("Please select an origin country: ");
            var origin = Console.ReadLine()?.Trim();

            Console.WriteLine("Please select a destination country: ");
            var destination = Console.ReadLine()?.Trim();

            Console.WriteLine("Choose one of the following shipping providers.");
            Console.WriteLine("1. PostNord");
            Console.WriteLine("2. DHL");
            Console.WriteLine("3. USPS");
            Console.WriteLine("4. Fedex");
            Console.WriteLine("5. UPS");
            Console.WriteLine("Select shipping provider: ");
            var provider = Convert.ToInt32(Console.ReadLine()?.Trim());

            Console.WriteLine("Choose one of the following invoice delivery options.");
            Console.WriteLine("1. E-mail");
            Console.WriteLine("2. File");
            Console.WriteLine("3. Mail");
            Console.WriteLine("Select invoice delivery options: ");
            var invoiceOption = Convert.ToInt32(Console.ReadLine()?.Trim());

            var order = new Order
            {
                ShippingDetails = new ShippingDetails
                {
                    OriginCountry = "Sweden",
                    DestinationCountry = "Sweden"
                },
                SalesTaxStrategy = GetSalesTaxStrategyFor(origin),
                InvoiceStrategy = GetInvoiceStrategyFor(invoiceOption),
                ShippingStrategy = GetShippingStrategyFor(provider)
            };

            order.SelectedPayments.Add(new Payment() { PaymentProvider = PaymentProvider.Invoice });
            order.LineItems.Add(new Item("CSHARP", "name", 10, ItemType.Service), 10);

            Console.WriteLine(order.GetTax());

            order.InvoiceStrategy = new FileInvoiceStrategy();
            order.FinalizeOrder();
        }

        private static IShippingStrategy GetShippingStrategyFor(in int provider)
        {
            switch (provider)
            {
                case 1: return new PostNordShippingStrategy();
                case 2: return new DhlShippingStrategy();
                case 3: return new UspsShippingStrategy();
                case 4: return new FedexShippingStrategy();
                case 5: return new UpsShippingStrategy();
                default: throw new Exception("Unsupported shipping method");
            }
        }

        private static IInvoiceStrategy GetInvoiceStrategyFor(in int invoiceOption)
        {
            switch (invoiceOption)
            {
                case 1: return new EmailInvoiceStrategy();
                case 2: return new FileInvoiceStrategy();
                case 3: return new PrintOnDemandInvoiceStrategy();
                default: throw new Exception("Unsupported invoice delivery option");
            }
        }

        private static ISalesTaxStrategy GetSalesTaxStrategyFor(string origin)
        {
            if (origin.ToLowerInvariant() == "sweden")
            {
                return new SwedenSalesTaxStrategy();
            }
            else if (origin.ToLowerInvariant() == "use")
            {
                return new UsaStateSalesTaxStrategy();
            }
            else
            {
                throw new Exception("Unsupported region");
            }
        }

        #region Debug

        static void Print(IEnumerable<Order> orders)
        {
            foreach (var order in orders)
            {
                Console.WriteLine(order.ShippingDetails.OriginCountry);
            }
        }

        #endregion
    }

    #region Debug

    public class OrderAmountComparer : IComparer<Order>
    {
        public int Compare(Order x, Order y)
        {
            var xTotal = x.TotalPrice;
            var yTotal = y.TotalPrice;
            if (xTotal == yTotal)
            {
                return 0;
            }
            else if (xTotal > yTotal)
            {
                return 1;
            }

            return -1;
        }
    }
    public class OrderOriginComparer : IComparer<Order>
    {
        public int Compare(Order x, Order y)
        {
            var xDest = x.ShippingDetails.OriginCountry.ToLowerInvariant();
            var yDest = y.ShippingDetails.OriginCountry.ToLowerInvariant();
            if (xDest == yDest)
            {
                return 0;
            }
            else if (xDest[0] > yDest[0])
            {
                return 1;
            }

            return -1;
        }
    }

    #endregion
}
