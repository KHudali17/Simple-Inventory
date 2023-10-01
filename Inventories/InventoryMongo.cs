using MongoDB.Bson;
using MongoDB.Driver;

namespace SimpleInv.Inventories;

public class InventoryMongo : IInventory
{
    private readonly IMongoCollection<Product> _collection;

    public InventoryMongo(IMongoDatabase database)
    {
        _collection = database.GetCollection<Product>("Products");
    }

    public async Task AddProduct(Product product)
    {
        try
        {
            await _collection.InsertOneAsync(product);
        }
        catch { throw; }
    }

    public async Task RemoveProduct(Product product)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);

        try
        {
            await _collection.DeleteOneAsync(filter);
        }
        catch { throw; }
    }

    public async Task<Product> Retrieve(string productName)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Name, productName);
        try
        {
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
        catch { throw; }
    }

    public async Task<List<Product>> Retrieve()
    {
        try
        {
            return await _collection.Find(new BsonDocument()).ToListAsync();
        }
        catch { throw; }
    }

    public async Task UpdateProduct(Product oldProduct, Product newProduct)
    {
        try
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, oldProduct.Id);
            
            var update = Builders<Product>.Update
                .Set(p => p.Name, newProduct.Name)
                .Set(p => p.Price, newProduct.Price)
                .Set(p => p.Quantity, newProduct.Quantity);

            await _collection.UpdateOneAsync(filter, update);
        }
        catch { throw; }
    }
}
