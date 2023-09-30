using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace SimpleInv.Inventories;

public class InventorySQLServer : IInventory
{
    private readonly SqlConnection _connection;

    public InventorySQLServer(SqlConnection connection)
    {
        _connection = connection;
    }

    public async Task AddProduct(Product product)
    {
        if (_connection.State != ConnectionState.Open) await _connection.OpenAsync();

        using SqlCommand insertCommand = _connection.CreateCommand();

        insertCommand.CommandText = "INSERT INTO Products (Name, Price, Quantity) " +
                                    "VALUES (@Name, @Price, @Quantity)";

        insertCommand.Parameters.AddWithValue("@Name", product.Name);
        insertCommand.Parameters.AddWithValue("@Price", product.Price);
        insertCommand.Parameters.AddWithValue("@Quantity", product.Quantity);

        try
        {
            await insertCommand.ExecuteNonQueryAsync();
        }
        catch (DbException)
        {
            Console.WriteLine("Database action failed.");
        }

        await _connection.CloseAsync();
    }

    public async Task RemoveProduct(Product product)
    {
        if (_connection.State != ConnectionState.Open) await _connection.OpenAsync();

        using SqlCommand deleteCommand = _connection.CreateCommand();

        deleteCommand.CommandText =
            "DELETE FROM Products " +
            "WHERE Name = @Name AND Price = @Price AND Quantity = @Quantity";

        deleteCommand.Parameters.AddWithValue("@Name", product.Name);
        deleteCommand.Parameters.AddWithValue("@Price", product.Price);
        deleteCommand.Parameters.AddWithValue("@Quantity", product.Quantity);

        try
        {
            await deleteCommand.ExecuteNonQueryAsync();
        }
        catch (DbException)
        {
            Console.WriteLine("Database action failed.");
        }

        await _connection.CloseAsync();
    }

    public async Task<Product> Retrieve(string productName)
    {
        if (_connection.State != ConnectionState.Open) await _connection.OpenAsync();

        using SqlCommand searchByName = _connection.CreateCommand();

        searchByName.CommandText = "SELECT Name, Price, Quantity " +
                                   "FROM Products " +
                                   "WHERE Name = @Name";

        searchByName.Parameters.AddWithValue("@Name", productName);

        try
        {
            using SqlDataReader reader = await searchByName.ExecuteReaderAsync();

            if (!await reader.ReadAsync()) throw new ProductNotFoundException("Product not found.");

            Product product = new Product
            {
                Name = reader["Name"].ToString()!,
                Price = (decimal)reader["Price"],
                Quantity = (decimal)reader["Quantity"]
            };

            await _connection.CloseAsync();
            return product;
        }
        catch (DbException)
        {
            Console.WriteLine("Database action failed.");

            await _connection.CloseAsync();
            return new Product();
        }
    }

    public async Task<List<Product>> Retrieve()
    {
        if (_connection.State != ConnectionState.Open) await _connection.OpenAsync();

        using SqlCommand retrieveAllCommand = _connection.CreateCommand();

        retrieveAllCommand.CommandText = "SELECT Name, Price, Quantity FROM Products";

        List<Product> products = new();

        try
        {
            using SqlDataReader reader = await retrieveAllCommand.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                Product product = new Product
                {
                    Name = reader["Name"].ToString()!,
                    Price = (decimal)reader["Price"],
                    Quantity = (decimal)reader["Quantity"]
                };

                products.Add(product);
            }
        }
        catch (DbException)
        {
            Console.WriteLine("Database action failed."); 
        }

        await _connection.CloseAsync();
        return products;
    }

    public async Task UpdateProduct(Product oldProduct, Product newProduct)
    {
        if (_connection.State != ConnectionState.Open) await _connection.OpenAsync();

        using SqlCommand updateCommand = _connection.CreateCommand();

        updateCommand.CommandText = 
            "UPDATE Products " +
            "SET Name = @NewName, Price = @NewPrice, Quantity = @NewQuantity " +
            "WHERE Name = @OldName AND Price = @OldPrice AND Quantity = @OldQuantity";

        updateCommand.Parameters.AddWithValue("@NewName", newProduct.Name);
        updateCommand.Parameters.AddWithValue("@NewPrice", newProduct.Price);
        updateCommand.Parameters.AddWithValue("@NewQuantity", newProduct.Quantity);
        updateCommand.Parameters.AddWithValue("@OldName", oldProduct.Name);
        updateCommand.Parameters.AddWithValue("@OldPrice", oldProduct.Price);
        updateCommand.Parameters.AddWithValue("@OldQuantity", oldProduct.Quantity);

        try
        {
            await updateCommand.ExecuteNonQueryAsync();
        }
        catch (DbException)
        {
            Console.WriteLine("Database action failed.");
        }

        await _connection.CloseAsync();
    }
}
