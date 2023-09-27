namespace SimpleInv.Inventories;

public interface IInventory
{
    void AddProduct(Product product);

    void RemoveProduct(Product product);

    void UpdateProduct(Product oldproduct, Product newProduct);

    Product Retrieve(string productName);

    List<Product> Retrieve();
}
