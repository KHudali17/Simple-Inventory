﻿using System;
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
            
            //Attempt to remove product, provide feedback
            try
            {
                Product product = _inventory.Retrieve(name);
                _inventory.RemoveProduct(product);
                Console.WriteLine($"Product {product.Name} has been removed successfully.");
            }catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
