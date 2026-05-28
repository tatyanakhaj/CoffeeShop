using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    internal class Order
    {
        public int Id { get; set; }

        public List<OrderItem> Items;
        public OrderStatus Status { get; set; }

        public DateTime CreatedTime { get; set; }

        public double TotalPrice { get; set; }

        

    }
}
