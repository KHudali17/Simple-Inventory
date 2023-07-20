using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInv
{
    public class Inventory : IInventory
    {
        private List<Product> products;
        public Inventory() {
            products = new List<Product>();
        }
        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Product Retrieve(string productName)
        {
            throw new NotImplementedException();
        }

        public List<Product> Retrieve()
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product, string newName, decimal newPrice, decimal newQuantity)
        {
            throw new NotImplementedException();
        }
    }
}
