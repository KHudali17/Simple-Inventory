namespace SimpleInv.Inventories;

public class Inventory : IInventory
{
    private readonly List<Product> _products;

    public Inventory()
    {
        _products = new List<Product>();
    }
    public Task AddProduct(Product product)
    {
        _products.Add(product);
        return Task.CompletedTask;
    }

    public Task RemoveProduct(Product product)
    {
        _products.Remove(product);
        return Task.CompletedTask;
    }

    public Task<Product> Retrieve(string productName)
    {
        var found = _products.Find(x => x.Name.Equals(productName, StringComparison.OrdinalIgnoreCase)) ?? throw new ProductNotFoundException("Product not found.");
        return Task.FromResult(found);
    }

    public Task<List<Product>> Retrieve()
    {
        return _products.Any() ? Task.FromResult(_products) : throw new ProductNotFoundException("Inventory is Empty.");
    }

    public Task UpdateProduct(Product oldProduct, Product newProduct)
    {
        oldProduct.Name = newProduct.Name;
        oldProduct.Price = newProduct.Price;
        oldProduct.Quantity = newProduct.Quantity;

        return Task.CompletedTask;
    }
}
