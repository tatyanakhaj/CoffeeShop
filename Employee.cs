using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    abstract class Employee
    {
        protected int ID { get; set; }
        protected string Name { get; set; }
        public virtual RoleType Role { get; set; }

      
        private double Salary { get; set; }
        protected bool IsAvaliable { get; set; }

    }
}
