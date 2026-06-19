using System;
using System.Collections.Generic;
using System.Text;

namespace CoffeeShop
{
    internal class ShiftManage
    {
        public static void SetShifts(List<Employee> employees)
        {
            foreach (var emp in employees)
            {
                emp.ShiftStart = DateTime.Now.AddHours(-1);
                emp.ShiftEnd = DateTime.Now.AddHours(2);
            }
        }

        public static void ShowWhoIsOnShift(List<Employee> employees)
        {
            Console.WriteLine("Employees on shift:");

            foreach (var emp in employees)
            {
                if (emp.IsOnShift())
                    Console.WriteLine($"- {emp.Role}");
            }
        }


        public static void CloseShift(Employee emp)
        {
            if (emp is not Manager)
            {
                Console.WriteLine("Only manager can close shift!");
                return;
            }

            Console.WriteLine("Shift closed.");
        }
    }
}
