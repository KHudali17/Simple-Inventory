using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInv
{
    public class RetrieveAllCommand : ICommand
    {
        private readonly IInventory _inventory;

        public RetrieveAllCommand(IInventory inventory)
        {
            this._inventory = inventory;
        }

        public void Execute()
        {
            Console.WriteLine("View all products");

            //Retrieve product list
            List<Product> products = _inventory.Retrieve();

            //Print list if not empty
            if (products.Count == 0)
            {
                Console.WriteLine("Inventory is Empty.");
                return;
            }

            foreach (Product product in products)
            {
                Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}");
            }
        }
    }
}
