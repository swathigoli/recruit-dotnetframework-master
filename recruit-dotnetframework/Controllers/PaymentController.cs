using recruit_dotnetframework.Models;
using recruit_dotnetframework.Service;
 using System; 
using System.Threading.Tasks;
using System.Web;
using System.Web.Http; 
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace recruit_dotnetframework.Controllers
{

     
     public class PaymentController : ApiController
    {
        [HttpPost] 
        public async Task<PaymentResponse> ProcessPayment(PaymentRequest request)
        {
            if (!IsCreditCardNumberValid(request.CardNumber))
            {
                return new PaymentResponse { Success = false, ErrorMessage = "Invalid payment request." };
            }

            if (!IsExpirationDateValid(request.ExpiryDate))
            {
                return new PaymentResponse { Success = false, ErrorMessage = "Invalid payment request." };
            }

            if (!IsCVVValid(request.Cvv))
            {
                return new PaymentResponse { Success = false, ErrorMessage = "Invalid payment request." };
            }


            var response = await PaymentProcessService.ProcessPaymentAsync(request);
            if(!string.IsNullOrEmpty(response)&& response == "Success")
                return new PaymentResponse { Success = true, TransactionId = GenerateTransactionId() };

            return new PaymentResponse { Success = false, ErrorMessage = "Invalid payment request." };

        }


        private bool IsCreditCardNumberValid(string cardNumber)
        {
              return !string.IsNullOrWhiteSpace(cardNumber) && cardNumber.Length == 16;
        }

        private bool IsExpirationDateValid(string expiryDate)
        {
            string[] expirydt = expiryDate.Split('/');
            int expiryMonth = 0, expiryYear = 0;

            if (expirydt.Length >2 || expirydt.Length < 2)
                return false;

            if (expirydt.Length == 2)
            {
                if (expirydt[0].Length <= 2)
                 expiryMonth = int.Parse(expirydt[0]); 
                else return false;

                if (expirydt[1].Length == 2 || expirydt[1].Length == 4)
                    expiryYear = int.Parse(expirydt[1]);
                else 
                    return false;
            }

             int currentYear = int.Parse(DateTime.UtcNow.Year.ToString().Substring(2));

            return expiryMonth >= 1 && expiryMonth <= 12 && ( (expiryYear >= currentYear && expirydt[1].Length == 2) || (expiryYear >= DateTime.UtcNow.Year  && expirydt[1].Length == 4));
        }

        private bool IsCVVValid(int cvv)
        { 
            return !string.IsNullOrWhiteSpace(cvv.ToString()) && (cvv.ToString().Length == 3 || cvv.ToString().Length == 4);
        }

        private string GenerateTransactionId()
        { 
            return Guid.NewGuid().ToString();  
        }

        

    }
   
}
 