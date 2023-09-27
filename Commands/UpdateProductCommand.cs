using SimpleInv.Inventories;

namespace SimpleInv.Commands;

public class UpdateProductCommand : ICommand
{
    private readonly IInventory _inventory;

    public UpdateProductCommand(IInventory inventory)
    {
        _inventory = inventory;
    }

    public async void Execute()
    {
        //Prompt user for product name
        Console.WriteLine("Edit a product");

        Console.WriteLine("Enter product name to be edited: ");
        string name = Console.ReadLine();

        //Retrieve product, if it exists prompt user further to update
        try
        {
            Product product = await _inventory.Retrieve(name);

            //Show current product details
            Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}");

            //start prompting for new details (give option to keep old).
            Console.WriteLine("Enter new product name (return to skip)");
            string? newName0 = Console.ReadLine();
            string newName = string.IsNullOrEmpty(newName0) ? product.Name : newName0;

            Console.WriteLine("Enter new product price (return to skip)");
            string? newPrice0 = Console.ReadLine();
            decimal newPrice = string.IsNullOrEmpty(newPrice0) ? product.Price : decimal.Parse(newPrice0);

            Console.WriteLine("Enter new product quantity (return to skip)");
            string? newQuant0 = Console.ReadLine();
            decimal newQuant = string.IsNullOrEmpty(newQuant0) ? product.Quantity : decimal.Parse(newQuant0);

            //Apply user's wishes, provide feedback
            var updatedProduct = new Product()
            { 
                Name = newName, 
                Price = newPrice, 
                Quantity = newQuant
            };

            _inventory.UpdateProduct(product, updatedProduct);

            Console.WriteLine("Product updated successfully");
            //Show new product details
            Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}");

        }
        catch (ProductNotFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
