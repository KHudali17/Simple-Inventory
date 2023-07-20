using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInv
{
    public class AddProductCommand : ICommand
    {
        private readonly IInventory inventory;
        public AddProductCommand(IInventory inventory)
        {
            this.inventory = inventory;
        }
        public void Execute()
        {
            //Worth dealing with nullable types warning in the future

            //Prompt user for product details
            Console.WriteLine("Add a product");

            Console.WriteLine("Enter product name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter product price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Enter product quantity: ");
            decimal quantity = decimal.Parse(Console.ReadLine());

            //Instantiate and add product to inventory, provide feedback.
            Product newProd = new Product
            {
                Name = name,
                Price = price,
                Quantity = quantity
            };
            inventory.AddProduct(newProd);

            Console.WriteLine("Product added successfully");
        }
    }

    public class RetrieveByNameCommand : ICommand
    {
        private readonly IInventory inventory;

        public RetrieveByNameCommand(IInventory inventory)
        {
            this.inventory = inventory;
        }

        public void Execute()
        {
            //Prompt user for product name
            Console.WriteLine("Search for a product");

            Console.WriteLine("Enter product name: ");
            string name = Console.ReadLine();

            //Search for product, provide feedback
            Product product = inventory.Retrieve(name);

            if(product == null)
            {
                Console.WriteLine("Product not found.");
            }
            else
            {
                Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}.");
            }
        }
    }

    public class RetrieveAllCommand : ICommand
    { 
        private readonly IInventory inventory;

        public RetrieveAllCommand(IInventory inventory)
        {
            this.inventory = inventory;
        }

        public void Execute()
        {
            Console.WriteLine("View all products");

            List<Product> products = inventory.Retrieve();

            if(products.Count == 0)
            {
                Console.WriteLine("Inventory is Empty.");
            }
            else
            {
                foreach(Product product in products)
                {
                    Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}");
                }
            }

        }
    }
}
