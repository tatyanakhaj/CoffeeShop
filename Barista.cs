using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    internal class Barista : Employee   
    {
        public int DrinksPreparedToday { get; set; }
        public override RoleType Role { get; set; } = RoleType.Barista;
    }
}
