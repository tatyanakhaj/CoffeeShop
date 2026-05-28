using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    internal class Manager:Employee, IOrderTaker, IPaymentProcessor, IOrderCanceller
    {
            public override RoleType Role { get; set; } = RoleType.Manager;
     
            public List<Employee> Employees { get; set; } = new List<Employee>();

            public List<Order> ActiveOrders { get; set; } = new List<Order>();

            public int DelayedOrdersCount { get; set; }


        public void TakeOrder(Order order, List<MenuItem> menu)
        {
            Console.WriteLine("Manager is taking the order...");

            foreach (var item in order.Items)
            {
                if (!menu.Contains(item.MenuItem))
                {
                    Console.WriteLine($"Item {item.MenuItem.Name} is NOT in menu!");
                    return;
                }
            }

            Console.WriteLine("Order accepted.");
            order.Status = OrderStatus.Created;
        }

        public void ProcessPayment(Order order, double paidAmount, string method)
        {
            double total = order.TotalPrice;

            Console.WriteLine($"Payment method: {method}");

            if (paidAmount < total)
            {
                Console.WriteLine("Not enough money!");
                return;
            }

            double change = paidAmount - total;

            Console.WriteLine($"Paid: {paidAmount}");
            Console.WriteLine($"Total: {total}");
            Console.WriteLine($"Change: {change}");

            order.Status = OrderStatus.Completed;
        }

        public void CancelOrder(Order order)
        {
            order.Status = OrderStatus.Cancelled;
            Console.WriteLine("Order cancelled by Manager.");
        }
    }

}

