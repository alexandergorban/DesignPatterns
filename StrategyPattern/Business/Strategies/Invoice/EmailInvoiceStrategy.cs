using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using StrategyPattern.Business.Models;

namespace StrategyPattern.Business.Strategies.Invoice
{
    public class EmailInvoiceStrategy : InvoiceStrategy
    {
        public override void Generate(Order order)
        {
            var body = GenerateTextInvoice(order);

            using (SmtpClient client = new SmtpClient("smtp.sendgrid.net", 587))
            {
                NetworkCredential credentials = new NetworkCredential("fekberg", " ");
                client.Credentials = credentials;

                MailMessage mail = new MailMessage("from@email.com", "to@email.com")
                {
                    Subject = "We've created an invoice for your order",
                    Body = body
                };

                client.Send(mail);
            }
        }
    }
}
