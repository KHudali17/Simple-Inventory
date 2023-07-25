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

            //Retrieve product list and print
            try
            {
                List<Product> products = _inventory.Retrieve();
                foreach (Product product in products)
                {
                    Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}");
                }
            }catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
