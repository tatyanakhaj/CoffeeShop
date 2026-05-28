using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    internal class Fresh:Drink
    {
        public string Fruit {  get; set; }
        public override bool HasCoffeine { get { return false; } } 
    }
}
