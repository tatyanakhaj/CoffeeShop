using CoffeeShop.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoffeeShop
{
    internal class CoffeeShopMain
    {
        static List<Employee> employees = new List<Employee>
        {
            new Manager(),
            new Supervisor(),
            new Barista(),
            new Cashier()
        };

        static List<MenuItem> menu = new List<MenuItem>
        {
            new MenuItem { Name = "Coffee", Price = 3 },
            new MenuItem { Name = "Tea", Price = 2 },
            new MenuItem { Name = "Fresh", Price = 3 },
            new MenuItem { Name = "Sandwich", Price = 8 },
            new MenuItem { Name = "Pastrey", Price = 6 }
        };

        static void Main()
        {
            SetShifts();

            ShowWhoIsOnShift();

            Employee emp = Login();
            if (emp == null) return;

            if (!emp.IsOnShift())
            {
                Console.WriteLine("You are not on shift!");
                return;
            }

            Order order = new Order { Id = 1, Status = OrderStatus.Created };

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
                        OpenOrder(emp, order);
                        break;

                    case 2:
                        CloseOrder(emp, order);
                        break;

                    case 3:
                        CancelOrder(emp, order);
                        break;

                    case 4:
                        CloseShift(emp);
                        running = false;
                        break;

                    case 0:
                        running = false;
                        break;
                }
            }
        }

        //  SHIFT
        static void SetShifts()
        {
            foreach (var emp in employees)
            {
                emp.ShiftStart = DateTime.Now.AddHours(-1);
                emp.ShiftEnd = DateTime.Now.AddHours(2);
            }
        }

        static void ShowWhoIsOnShift()
        {
            Console.WriteLine("Employees on shift:");

            foreach (var emp in employees)
            {
                if (emp.IsOnShift())
                    Console.WriteLine($"- {emp.Role}");
            }
        }

        // LOGIN
        static Employee Login()
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
        static void OpenOrder(Employee emp, Order order)
        {
            if (emp is not IOrderTaker)
            {
                Console.WriteLine("You cannot open orders!");
                return;
            }

            if (order.Items == null)
                order.Items = new List<OrderItem>();

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

                    string typeInput = Console.ReadLine();

                    selectedType = (TeaType)Enum.Parse(typeof(TeaType), typeInput, true);
                }
                else if (selected.Category == "Sandwich")
                {
                    Console.WriteLine("Choose Sandwich type:");
                    foreach (var t in Enum.GetValues(typeof(SandwichType)))
                        Console.WriteLine($"- {t}");

                    string typeInput = Console.ReadLine();

                    selectedType = (SandwichType)Enum.Parse(typeof(SandwichType), typeInput, true);
                }
                else if (selected.Category == "Pastry")
                {
                    Console.WriteLine("Choose Pastry type:");
                    foreach (var t in Enum.GetValues(typeof(PastriesType)))
                        Console.WriteLine($"- {t}");

                    string typeInput = Console.ReadLine();

                    selectedType = (PastriesType)Enum.Parse(typeof(PastriesType), typeInput, true);
                }

                order.Items.Add(new OrderItem
                {
                    MenuItem = selected,
                    Quantity = qty,
                    TeaType = selectedType as TeaType?,
                    SandwichType = selectedType as SandwichType?,
                    PastriesType = selectedType as PastriesType?
                });

                double total = order.CalculateTotal();

                Console.WriteLine($"\nTOTAL: {total} lv");

                Console.Write("\nEnter paid amount: ");
                if (!double.TryParse(Console.ReadLine(), out double paid))
                {
                    Console.WriteLine("Invalid payment!");
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

                Console.WriteLine("\nHave a nice day!");
            }
        }
            static void CloseOrder(Employee emp, Order order)
            {
                if (emp is not IPaymentProcessor)
                {
                    Console.WriteLine("You cannot close orders!");
                    return;
                }

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

                Console.WriteLine("Have a nice day!");
            }

            // CANCEL
            static void CancelOrder(Employee emp, Order order)
            {
                if (emp is not IOrderCanceller)
                {
                    Console.WriteLine("You cannot cancel orders!");
                    return;
                }

                order.Status = OrderStatus.Cancelled;
                Console.WriteLine("Order cancelled.");
            }

            // CLOSE SHIFT
            static void CloseShift(Employee emp)
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

