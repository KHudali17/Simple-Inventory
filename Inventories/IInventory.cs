namespace SimpleInv.Inventories;

public interface IInventory
{
    Task AddProduct(Product product);

    Task RemoveProduct(Product product);

    Task UpdateProduct(Product oldproduct, Product newProduct);

    Task<Product> Retrieve(string productName);

    Task<List<Product>> Retrieve();
}
