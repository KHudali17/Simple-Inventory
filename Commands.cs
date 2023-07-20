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
}
