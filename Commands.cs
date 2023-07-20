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

            //Retrieve product list
            List<Product> products = inventory.Retrieve();

            //Print list if not empty
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

    public class RemoveProductCommand : ICommand
    {
        private readonly IInventory inventory;

        public RemoveProductCommand(IInventory inventory)
        {
            this.inventory = inventory;
        }

        public void Execute()
        {
            //Prompt user for product name
            Console.WriteLine("Remove a product");

            Console.WriteLine("Enter product name to be deleted: ");
            string name = Console.ReadLine();

            Product product = inventory.Retrieve(name);
            
            if(product == null)
            {
                Console.WriteLine("Product not found.");
            }
            else
            {
                inventory.RemoveProduct(product);
                Console.WriteLine($"Product {product.Name} has been removed successfully.");
            }
        }
    }

    public class UpdateProductCommand : ICommand
    {
        private readonly IInventory inventory;

        public UpdateProductCommand(IInventory inventory)
        {
            this.inventory = inventory;
        }

        public void Execute()
        {   
            //Prompt user for product name
            Console.WriteLine("Edit a product");

            Console.WriteLine("Enter product name to be edited: ");
            string name = Console.ReadLine();

            Product product = inventory.Retrieve(name);
            
            //Retrieve product, if it exists prompt user further to update.
            if(product == null)
            {
                Console.WriteLine("Product not found.");
            }
            else
            {
                //Show current product details
                Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}");
                
                //start prompting for new details (give option to keep old).
                Console.WriteLine("Enter new product name (return to skip)");
                string newName0 = Console.ReadLine();
                string newName = string.IsNullOrEmpty(newName0) ? product.Name : newName0;

                Console.WriteLine("Enter new product price (return to skip)");
                string newPrice0 = Console.ReadLine();
                decimal newPrice = string.IsNullOrEmpty(newPrice0) ? product.Price : decimal.Parse(newPrice0);

                Console.WriteLine("Enter new product quantity (return to skip)");
                string newQuant0 = Console.ReadLine();
                decimal newQuant = string.IsNullOrEmpty(newQuant0) ? product.Quantity : decimal.Parse(newQuant0);

                //Apply user's wishes, provide feedback
                inventory.UpdateProduct(product, newName, newPrice, newQuant);

                Console.WriteLine("Product updated successfully");
                //Show new product details
                Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}");
            }
        }
    }
}
