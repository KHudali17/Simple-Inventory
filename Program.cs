using Microsoft.Extensions.Configuration;
using SimpleInv.Inventories;
using SimpleInv.Invoke;
using System.Data.SqlClient;
using MongoDB.Driver;
using MongoDB.Bson;

namespace SimpleInv;

public class Program
{
    static void Main(string[] args)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json")
        .Build();

        var connectionString = config.GetConnectionString("AtlasConnection");

        var settings = MongoClientSettings.FromConnectionString(connectionString);

        // Set the ServerApi field of the settings object to Stable API version 1
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        // Create a new client and connect to the server
        var client = new MongoClient(settings);

        // Send a ping to confirm a successful connection
        try
        {
            var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
            Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Environment.Exit(1);
        }


        IInventory inventory = new InventoryMongo(client.GetDatabase("Simple-Inventory"));
  
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
