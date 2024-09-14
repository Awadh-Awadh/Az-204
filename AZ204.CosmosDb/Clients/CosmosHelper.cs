using System.Text.Json;
using Microsoft.Azure.Cosmos;

namespace AZ204.CosmosDb.Clients;

public class CosmosHelper
{
    private static readonly string EndpointUri = "documentEndpoint";

    private static readonly string PrimaryKey = "primary key";
    
    private readonly CosmosClient _cosmosClient;
    private  Database _database;
    private  Container _container;
    
    private string databaseId = "az204Database";
    private string containerId = "az204Container";
    public CosmosHelper()
    {
        _cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
    }

    public async Task<Database> CreateDatabase()
    {
       _database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(id: databaseId);

        return _database;
    }

    public async Task<Container> CreateContainer(string partitionKey)
    {
        var containerResponse = await _database.CreateContainerIfNotExistsAsync(id: containerId,
            partitionKeyPath: partitionKey,
            throughput: 400);
        
        _container = containerResponse.Container;
        
        return containerResponse.Container;
    }

    public async Task<Container> GetContainerById(string id)
    {
        var containerProperties = await _container.ReadContainerAsync();

        return _container;
    }

    public async Task DeleteContainer(string containerId)
    {
        await _database.GetContainer(containerId).DeleteContainerAsync();
    }

    public async Task CreateItem<T>() where T : new()
    {
        ItemResponse<Type> response = await _container.CreateItemAsync(typeof(T), new PartitionKey(nameof(T)));
        
    }

    public async Task ReadAnItem<T>()
    {
        string id = "[id]";
        string accountNumber = "[partitionKey]";
        ItemResponse<T> response = await _container.ReadItemAsync<T>(id, new PartitionKey(accountNumber));
    }

    public async Task QueryAsync<T>()
    {
        var container = await GetContainerById("containerId");
        QueryDefinition query = new QueryDefinition(
                "select * from sales where s.AccountNumber = @AccountInput")
            .WithParameter("@AccountInput", "Account1");
        FeedIterator<T> resultSet = container.GetItemQueryIterator<T>(
            query,
            requestOptions: new QueryRequestOptions
            {
                PartitionKey = new PartitionKey("Account1"),
                MaxItemCount = 1
            });
    }
}