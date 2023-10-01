using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Data.SqlClient;

namespace SimpleInv;

public class Connection
{

    public static IMongoDatabase EstablishWithAtlas(IConfigurationRoot configRoot, string databaseName)
    {
        var connectionString = configRoot.GetConnectionString("AtlasConnection");

        var settings = MongoClientSettings.FromConnectionString(connectionString);

        // Set the ServerApi field of the settings object to Stable API version 1
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        // Create a new client and connect to the server
        var client = new MongoClient(settings);

        // Send a ping to confirm a successful connection
        try
        {
            var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Environment.Exit(1);
        }
        return client.GetDatabase("Simple-Inventory");
    }

    public static SqlConnection EstablishWithSqlServer(IConfigurationRoot configRoot)
    {
        try
        {
            var connectionString = configRoot.GetConnectionString("SQLServerConnection");
            return new SqlConnection(connectionString);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Environment.Exit(1);
            return null;
        }
    }
}
