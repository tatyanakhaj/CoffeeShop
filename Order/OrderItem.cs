using CoffeeShop.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    internal class OrderItem
    {
        public MenuItem MenuItem { get; set; } 
        public int Quantity { get; set; }

        public TeaType? TeaType { get; set; }
        public SandwichType? SandwichType { get; set; }
        public PastriesType? PastriesType { get; set; }
    }
}
