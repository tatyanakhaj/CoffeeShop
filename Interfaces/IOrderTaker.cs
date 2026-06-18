using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
   public interface IOrderTaker
    {
        void TakeOrder(Order order, List<MenuItem> menu);
    }
}
