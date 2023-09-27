namespace SimpleInv.Inventories;

public interface IInventory
{
    void AddProduct(Product product);

    void RemoveProduct(Product product);

    void UpdateProduct(Product product, string newName, decimal newPrice, decimal newQuantity);

    Product Retrieve(string productName);

    List<Product> Retrieve();
}
