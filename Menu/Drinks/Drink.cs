using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    internal class Drink:MenuItem
    {
        protected string Size {  get; set; }  
        public virtual bool   HasCoffeine { get; set; }  
    }
}
