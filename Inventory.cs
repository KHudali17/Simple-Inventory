using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimpleInv
{
    public class Inventory : IInventory
    {
        private List<Product> _products;

        public Inventory() {
            _products = new List<Product>();
        }
        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            _products.Remove(product);
        }

        public Product Retrieve(string productName)
        {
            return _products.Find(x => x.Name.Equals(productName, StringComparison.OrdinalIgnoreCase));
        }

        public List<Product> Retrieve()
        {
            return _products;
        }

        public void UpdateProduct(Product product, string newName, decimal newPrice, decimal newQuantity)
        {
            product.Name = newName;
            product.Price = newPrice;
            product.Quantity = newQuantity;
        }
    }
}
