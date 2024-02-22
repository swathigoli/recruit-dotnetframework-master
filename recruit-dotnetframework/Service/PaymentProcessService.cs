using recruit_dotnetframework.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace recruit_dotnetframework.Service
{
    public static class PaymentProcessService
    {
        public  async static Task<string> ProcessPaymentAsync(PaymentRequest request)
        {

            try
            {
                var customers = new CustomerService();
                var charges = new ChargeService();

                var options = new RequestOptions
                {
                    ApiKey = System.Configuration.ConfigurationManager.AppSettings["Stripe_Key"]
                };

                var optionToken = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = request.CardNumber,
                        ExpMonth = request.ExpiryDate.Split('/')[0],
                        ExpYear = request.ExpiryDate.Split('/')[1],
                        Cvc = request.Cvv.ToString(),
                        Name = "Test",
                        Currency = "NZD"
                    },
                };
                var tokenService = new TokenService();
                Token paymentToken = await tokenService.CreateAsync(optionToken, options);



                var customer = new Customer();
                var customerEmail = "";
                var stripeCustomer = await customers.ListAsync(new CustomerListOptions
                {
                    Email = customerEmail,
                    Limit = 1
                }, options);

                if (stripeCustomer.Data.Count == 0)
                { 
                    customer = await customers.CreateAsync(new CustomerCreateOptions
                    {
                        Source = paymentToken.Id,

                        Name = "Test",
                        Email = customerEmail,
                    }, options);

                }
                else
                {
                    customer = stripeCustomer.FirstOrDefault();
                }

                var charge = await charges.CreateAsync(new ChargeCreateOptions
                {
                    Source = paymentToken.Id,
                    Amount = (long?)request.Amount,
                    Currency = "NZD",
                    ReceiptEmail = customer.Email,
                    Description = "Test payment",

                }, options);


                if (charge.Status.ToLower().Equals("succeeded"))
                {
                    return "Success";
                }
                else
                {
                    return "Fail";
                }

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            return string.Empty;
        }
    }
}