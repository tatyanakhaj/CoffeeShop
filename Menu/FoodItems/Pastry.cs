using CoffeeShop.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Menu
{
    internal class Pastry:Food
    {

        public bool ContainsNuts { get; set; }

        public bool IsGlutenFree { get; set; }

        public PastriesType PastriesType { get; set; }  
    }
}
