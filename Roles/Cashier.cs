using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    internal class Cashier:Employee
    {
        public int CashRegisterId { get; set; }
        public override RoleType Role { get; set; } = RoleType.Cashier;
        public int CustomersServedToday { get; set; }
        public int CurrentQueueLength { get; set; }
    }
}
