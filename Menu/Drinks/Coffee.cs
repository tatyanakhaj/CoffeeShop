using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    internal class Coffee:Drink
    {
          
        public bool HasMilk { get; set; }   

        public string  Temperature { get; set; }    
        public override bool  HasCoffeine { get { return true; } } 

    }
}
