using Microsoft.Extensions.Configuration;
using SimpleInv.Inventories;
using SimpleInv.Invoke;

namespace SimpleInv;

public class Program
{
    static void Main(string[] args)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json")
        .Build();

        var connection = Connection.EstablishWithAtlas(config, "Simple-Inventory");

        IInventory inventory = InventoryFactory.CreateInventory(connection);

        IInvoker invoker = new Invoker(inventory);

        bool exit = false;
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
