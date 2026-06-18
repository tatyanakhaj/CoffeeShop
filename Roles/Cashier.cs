using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    internal class Cashier : Employee, IOrderTaker, IPaymentProcessor
    {
        public int CashRegisterId { get; set; }
        public override RoleType Role { get; set; } = RoleType.Cashier;
        public int CustomersServedToday { get; set; }
        public int CurrentQueueLength { get; set; }

       
        public void TakeOrder(Order order, List<MenuItem> menu)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            if (menu == null) throw new ArgumentNullException(nameof(menu));

            // Minimal, safe handling to satisfy the interface and avoid assumptions
            CustomersServedToday++;
            if (CurrentQueueLength > 0) CurrentQueueLength--;
            Console.WriteLine("Order taken.");
        }

        
        public void ProcessPayment(Order order, double paidAmount, string method)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            if (paidAmount < 0) throw new ArgumentOutOfRangeException(nameof(paidAmount));

            
            Console.WriteLine($"Processed payment of {paidAmount} via {method ?? "unknown"}.");
        }
    }
}
