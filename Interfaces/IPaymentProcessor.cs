using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    public interface IPaymentProcessor
    {
        void ProcessPayment(Order order, double paidAmount, string method);
    }
}
