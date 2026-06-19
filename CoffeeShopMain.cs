using CoffeeShop.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeShop
{
    internal class CoffeeShopMain
    {
        static public List<Employee> employees = new List<Employee>
        {
            new Manager(),
            new Supervisor(),
            new Barista(),
            new Cashier()
        };

        public List<MenuItem> menu = new List<MenuItem>
        {
            new MenuItem { Name = "Coffee", Price = 3 },
            new MenuItem { Name = "Tea", Price = 2 },
            new MenuItem { Name = "Fresh", Price = 3 },
            new MenuItem { Name = "Sandwich", Price = 8 },
            new MenuItem { Name = "Pastrey", Price = 6 }
        };

        public static void Main()
        {
            //DONE: Moved all shift methods in IShiftWorker and ShiftManage
            //Added 'Builder' pattern for Order class   
            //'Cashier' implemented IOrderTaker and IPaymentProcessor interfaces    


            var app = new CoffeeShopMain();


            ShiftManage.SetShifts(CoffeeShopMain.employees);
            ShiftManage.ShowWhoIsOnShift(CoffeeShopMain.employees);

            Employee emp = app.Login();
            if (emp == null) return;

            if (!emp.IsOnShift())
            {
                Console.WriteLine("You are not on shift!");
                return;
            }


            OrderBuilder builder = new OrderBuilder();
            builder.SetId(1);
            app.OpenOrder(emp, builder);
            Console.WriteLine("\nDo you want to close the order? (yes/no)");
            string input = Console.ReadLine();

            if (input.Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                app.CloseOrder(emp, builder);
            }
            else
            {
                app.CancelOrder(emp, builder);
            }

            bool running = true;

            while (running)
            {
                Console.WriteLine("\nChoose action:");

                Console.WriteLine("1 - Open Order");
                Console.WriteLine("2 - Close Order");

                if (emp is Manager || emp is Supervisor)
                    Console.WriteLine("3 - Cancel Order");

                if (emp is Manager)
                    Console.WriteLine("4 - Close Shift");

                Console.WriteLine("0 - Exit");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        app.OpenOrder(emp, builder);
                        break;

                    case 2:
                        app.CloseOrder(emp, builder);
                        break;

                    case 3:
                        app.CancelOrder(emp, builder);
                        break;

                    case 4:
                        ShiftManage.CloseShift(emp);
                        running = false;
                        break;

                    case 0:
                        running = false;
                        break;
                }
            }
        }

        // LOGIN
        public Employee Login()
        {
            Console.WriteLine("\nLogin as:");
            Console.WriteLine("1 - Manager");
            Console.WriteLine("2 - Supervisor");
            Console.WriteLine("3 - Barista");
            Console.WriteLine("4 - Cashier");

            int choice = int.Parse(Console.ReadLine());

            return choice switch
            {
                1 => employees.OfType<Manager>().First(),
                2 => employees.OfType<Supervisor>().First(),
                3 => employees.OfType<Barista>().First(),
                4 => employees.OfType<Cashier>().First(),
                _ => null
            };
        }

        // OPEN ORDER
        public void OpenOrder(Employee emp, OrderBuilder builder)
        {
            if (emp is not IOrderTaker)
            {
                Console.WriteLine("You cannot open orders!");
                return;
            }

            Console.WriteLine("Menu:");

            foreach (var item in menu)
            {
                Console.WriteLine($"{item.Name} - {item.Price} lv");
            }

            Console.WriteLine("\nStart ordering. Type 'End' to finish.\n");

            while (true)
            {
                Console.Write("Product: ");
                string input = Console.ReadLine();

                if (input.Equals("End", StringComparison.OrdinalIgnoreCase))
                    break;

                var selected = menu.FirstOrDefault(m =>
                    m.Name.Equals(input, StringComparison.OrdinalIgnoreCase));

                if (selected == null)
                {
                    Console.WriteLine("Item not found!");
                    continue;
                }

                Console.Write("Quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int qty))
                {
                    Console.WriteLine("Invalid quantity!");
                    continue;
                }

                object selectedType = null;

                if (selected.Category == "Tea")
                {
                    Console.WriteLine("Choose Tea type:");
                    foreach (var t in Enum.GetValues(typeof(TeaType)))
                        Console.WriteLine($"- {t}");

                    selectedType = (TeaType)Enum.Parse(typeof(TeaType), Console.ReadLine(), true);
                }
                else if (selected.Category == "Sandwich")
                {
                    Console.WriteLine("Choose Sandwich type:");
                    foreach (var t in Enum.GetValues(typeof(SandwichType)))
                        Console.WriteLine($"- {t}");

                    selectedType = (SandwichType)Enum.Parse(typeof(SandwichType), Console.ReadLine(), true);
                }
                else if (selected.Category == "Pastry")
                {
                    Console.WriteLine("Choose Pastry type:");
                    foreach (var t in Enum.GetValues(typeof(PastriesType)))
                        Console.WriteLine($"- {t}");

                    selectedType = (PastriesType)Enum.Parse(typeof(PastriesType), Console.ReadLine(), true);
                }

                builder.AddItem(new OrderItem
                {
                    MenuItem = selected,
                    Quantity = qty,
                    TeaType = selectedType as TeaType?,
                    SandwichType = selectedType as SandwichType?,
                    PastriesType = selectedType as PastriesType?
                });
            }
        }


        public void CloseOrder(Employee emp, OrderBuilder builder)
        {
            if (emp is not IPaymentProcessor)
            {
                Console.WriteLine("You cannot close orders!");
                return;
            }

            Order order = builder.Build();

            if (order.Items == null || order.Items.Count == 0)
            {
                Console.WriteLine("No items in order!");
                return;
            }

            double total = order.CalculateTotal();

            Console.WriteLine($"\nTOTAL PRICE: {total} lv");

            Console.Write("Enter paid amount: ");
            if (!double.TryParse(Console.ReadLine(), out double paid))
            {
                Console.WriteLine("Invalid input!");
                return;
            }

            if (paid < total)
            {
                Console.WriteLine("Not enough money!");
                return;
            }

            double change = paid - total;

            Console.WriteLine($"Paid: {paid} lv");
            Console.WriteLine($"Change: {change} lv");

            order.Status = OrderStatus.Completed;

            Console.WriteLine("Order completed. Have a nice day!");

           
            builder.Reset();
        }
        public void CancelOrder(Employee emp, OrderBuilder builder)
        {
            if (emp is not IOrderCanceller)
            {
                Console.WriteLine("You cannot cancel orders!");
                return;
            }

            Order order = builder.Build();

            if (order.Items == null || order.Items.Count == 0)
            {
                Console.WriteLine("Nothing to cancel!");
                return;
            }

            order.Status = OrderStatus.Cancelled;

            Console.WriteLine("Order cancelled.");

            
            builder.Reset();
        }

       
    }
}

