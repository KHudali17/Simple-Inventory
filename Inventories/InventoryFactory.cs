using MongoDB.Driver;
using System.Data.SqlClient;

namespace SimpleInv.Inventories;

public class InventoryFactory
{
    public static IInventory CreateInventory() 
        => new Inventory();

    public static IInventory CreateInventory(SqlConnection connection) 
        => new InventorySQLServer(connection);

    public static IInventory CreateInventory(IMongoDatabase database)
        => new InventoryMongo(database);
}
