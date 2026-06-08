using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    internal class Supervisor:Employee, IOrderTaker, IOrderCanceller
    {
     
            
        public override RoleType Role { get; set; } = RoleType.Supervisor;
      
        public bool CanReassignTasks { get; set; }


        public void TakeOrder(Order order, List<MenuItem> menu)
        {
            Console.WriteLine("Supervisor is taking the order...");
            order.Status = OrderStatus.Created;
        }

        public void CancelOrder(Order order)
        {
            order.Status = OrderStatus.Cancelled;
            Console.WriteLine("Order cancelled by Supervisor.");
        }
    }
}

