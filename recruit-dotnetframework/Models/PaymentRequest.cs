using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace recruit_dotnetframework.Models
{
    public class PaymentRequest
    {
        public string CardNumber { get; set; } 
        public string ExpiryDate { get; set; }
        public int Cvv { get; set; }
        public decimal Amount { get; set; }
    }
}