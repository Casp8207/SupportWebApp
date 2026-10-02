using Microsoft.Azure.Cosmos;
using SupportWebApp.Models;

namespace SupportWebApp.Services;

public class CosmosDbService
{
    private readonly Container _container;

    public CosmosDbService(IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionString:CosmosDB"];
        var databaseName = configuration["CosmosDbSettings:DatabaseName"];
        var containerName = configuration["CosmosDbSettings:ContainerName"];

        var client = new CosmosClient(connectionString);
        _container = client.GetContainer(databaseName, containerName);
    }

    public async Task AddSupportMessageAsync(SupportMessage message)
    {
        await _container.CreateItemAsync(
            message,
            new PartitionKey(message.Category));
    }
    public async Task<List<SupportMessage>> GetSupportMsgByCategoryAsync(
        string category)
    {
        var queryDef = new QueryDefinition(
                "SELECT * FROM c WHERE c.category = @category ORDER BY c.oprettet DESC")
            .WithParameter("@category", category);
        var query = _container.GetItemQueryIterator<SupportMessage>(queryDef,
            requestOptions: new QueryRequestOptions {
                PartitionKey = new PartitionKey(category) });
        var results = new List<SupportMessage>();
        while (query.HasMoreResults)
        {
            results.AddRange(await query.ReadNextAsync());
        }
        return results;
    }
    public async Task<List<SupportMessage>> GetSupportMessagesAsync()
    {
        var query = _container.GetItemQueryIterator<SupportMessage>(
            "SELECT * FROM c");

        var results = new List<SupportMessage>();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }
}