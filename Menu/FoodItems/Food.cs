using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    internal class Food:MenuItem
    {
       public bool IsVegan { get; set; }    
       public DateTime ExparationDate { get; set; }
    }
}
