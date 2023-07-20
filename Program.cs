using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInv
{
    public class Program
    {
        private static IInventory inventory;

        static void Main(string[] args)
        {
            inventory = new Inventory();
            bool exit = false;
            IInvoker invoker = new Invoker();

            while (!exit)
            {
                //Present options to the user
                Console.WriteLine("A Simple Inventory Management System");
                Console.WriteLine("1. Add a product");
                Console.WriteLine("2. Edit a product");
                Console.WriteLine("3. Remove a product");
                Console.WriteLine("4. Search for a product");
                Console.WriteLine("5. View all products");
                Console.WriteLine("6. Exit");

                Console.WriteLine("Enter your choice (1-6): ");

                int choice = int.Parse(Console.ReadLine());
                Console.WriteLine();

                //Act on user's choice
                if (choice == 6)
                {
                    exit = true;
                    continue;
                }

                invoker.Invoke(choice);
                
                Console.WriteLine();
            }
        }
    }
}
