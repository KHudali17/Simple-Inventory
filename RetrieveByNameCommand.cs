using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInv
{
    public class RetrieveByNameCommand : ICommand
    {
        private readonly IInventory _inventory;

        public RetrieveByNameCommand(IInventory inventory)
        {
            this._inventory = inventory;
        }

        public void Execute()
        {
            //Prompt user for product name
            Console.WriteLine("Search for a product");

            Console.WriteLine("Enter product name: ");
            string name = Console.ReadLine();

            //Search for product, provide feedback
            try
            {
                Product product = _inventory.Retrieve(name);
                Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}.");
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
