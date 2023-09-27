using SimpleInv.Inventories;

namespace SimpleInv.Commands;

public class AddProductCommand : ICommand
{
    private readonly IInventory _inventory;

    public AddProductCommand(IInventory inventory)
    {
        _inventory = inventory;
    }

    public void Execute()
    {
        //Prompt user for product details
        Console.WriteLine("Add a product");

        Console.WriteLine("Enter product name: ");
        string name = Console.ReadLine();

        Console.WriteLine("Enter product price: ");
        decimal price = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Enter product quantity: ");
        decimal quantity = decimal.Parse(Console.ReadLine());

        //Instantiate and add product to inventory, provide feedback.
        Product newProd = new Product
        {
            Name = name,
            Price = price,
            Quantity = quantity
        };
        _inventory.AddProduct(newProd);

        Console.WriteLine("Product added successfully");
    }
}
