namespace SimpleInv.Inventories;

public class Inventory : IInventory
{
    private readonly List<Product> _products;

    public Inventory()
    {
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
        var found = _products.Find(x => x.Name.Equals(productName, StringComparison.OrdinalIgnoreCase)) ?? throw new ProductNotFoundException("Product not found.");
        return found;
    }

    public List<Product> Retrieve()
    {
        return _products.Any() ? _products : throw new ProductNotFoundException("Inventory is Empty.");
    }

    public void UpdateProduct(Product oldProduct, Product newProduct)
    {
        oldProduct.Name = newProduct.Name;
        oldProduct.Price = newProduct.Price;
        oldProduct.Quantity = newProduct.Quantity;
    }
}
