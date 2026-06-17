using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    internal class Cashier:Employee, IOrderTaker, IPaymentProcessor
    {
        public int CashRegisterId { get; set; }
        public override RoleType Role { get; set; } = RoleType.Cashier;
        public int CustomersServedToday { get; set; }
        public int CurrentQueueLength { get; set; }

        public void TakeOrder(Order order, List<MenuItem> menu)
        {
            Console.WriteLine("Cashier is taking order...");
        }

        public void ProcessPayment(Order order, double paid, string method)
        {
            Console.WriteLine("Processing payment...");
        }
    }
}
