using SimpleInv.Inventories;

namespace SimpleInv.Commands;

public class RemoveProductCommand : ICommand
{
    private readonly IInventory _inventory;

    public RemoveProductCommand(IInventory inventory)
    {
        _inventory = inventory;
    }

    public async void Execute()
    {
        //Prompt user for product name
        Console.WriteLine("Remove a product");

        Console.WriteLine("Enter product name to be deleted: ");
        string name = Console.ReadLine();

        //Attempt to remove product, provide feedback
        try
        {
            Product product = await _inventory.Retrieve(name);
            await _inventory.RemoveProduct(product);
            Console.WriteLine($"Product {product.Name} has been removed successfully.");
        }
        catch (ProductNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
