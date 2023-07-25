using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInv
{
    public class RemoveProductCommand : ICommand
    {
        private readonly IInventory _inventory;

        public RemoveProductCommand(IInventory inventory)
        {
            this._inventory = inventory;
        }

        public void Execute()
        {
            //Prompt user for product name
            Console.WriteLine("Remove a product");

            Console.WriteLine("Enter product name to be deleted: ");
            string name = Console.ReadLine();

            Product product = _inventory.Retrieve(name);

            if (product == null)
            {
                Console.WriteLine("Product not found.");
            }
            else
            {
                _inventory.RemoveProduct(product);
                Console.WriteLine($"Product {product.Name} has been removed successfully.");
            }
        }
    }
}
