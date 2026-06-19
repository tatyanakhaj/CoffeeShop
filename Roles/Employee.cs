using CoffeeShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop
{
    abstract class Employee:IShiftWorker

    {
        protected int ID { get; set; }
        protected string Name { get; set; }
        public virtual RoleType Role { get; set; }

      
        private double Salary { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }


        public bool IsOnShift()
        {
            DateTime now = DateTime.Now;
            return now >= ShiftStart && now <= ShiftEnd;
        }
    }
}
