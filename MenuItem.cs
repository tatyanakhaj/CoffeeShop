using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    abstract class MenuItem
    {
        protected int ID { get; set; } 
        public string Name { get; set; }    
        protected string Description { get; set; } 

        public double Price { get; set; } 

        public bool IsAvaliable { get; set; }  
        


    }
}
