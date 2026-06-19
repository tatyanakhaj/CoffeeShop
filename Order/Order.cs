using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    public class Order
    {
        public int Id { get; set; }

        public List<OrderItem> Items = new();
        public OrderStatus Status { get; set; }

        public DateTime CreatedTime { get; set; }

        public double TotalPrice { get; set; }

        
        public double CalculateTotal()
        {
            double total = 0;
            if (Items != null && Items.Count > 0)
            {
                total = Items.Sum(i => (i?.MenuItem?.Price ?? 0) * i.Quantity);
            }
            TotalPrice = total;
            return total;
        }
       
    }
}
