using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeShop.Interfaces
{
    public interface IShiftWorker
    {
        DateTime ShiftStart { get; set; }
        DateTime ShiftEnd { get; set; }

        bool IsOnShift();
    }
}
