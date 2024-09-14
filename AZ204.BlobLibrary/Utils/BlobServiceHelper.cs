using AZ2_4.BlobLibrary.Interfaces;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AZ2_4.BlobLibrary.Utils;

public static class BlobServiceHelper
{
    public static  async void UploadBlob(BlobContainerClient blobContainerClient, string blobName)
    {
        string path = "./data/";
        string fileName = $"wtfile{Guid.NewGuid().ToString()}.txt";
        await File.WriteAllTextAsync(path, "Hello World");
        
        BlobClient blobClient = blobContainerClient.GetBlobClient(blobName);

        await using FileStream stream = File.OpenRead(path);
        await blobClient.UploadAsync(stream);
    }

    public static async IAsyncEnumerable<string> ListBlobs(BlobContainerClient containerClient)
    {
        await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
        {
            yield return blobItem.Name;
        } 
    }

    public static async Task<BlobContainerProperties?> ReadContainerPropertiesAsync(BlobContainerClient containerClient)
    {
        try
        {
            var response = await containerClient.GetPropertiesAsync();
            var properties = response.Value;
            var uri = containerClient.Uri;
            var publicAccessLevel = properties.PublicAccess;
            var lastModified = properties.LastModified;
            var metadata = properties.Metadata;

            return properties;
        }
        catch (RequestFailedException e)
        {
            Console.WriteLine($"HTTP error code {e.Status}: {e.ErrorCode}");
            Console.WriteLine(e.Message);
            Console.ReadLine();
        }
        
        return null;
    }

    public static async Task AddContainerMetadataAsync(BlobContainerClient client)
    {
        try
        {
            IDictionary<string, string> metadata = new Dictionary<string, string>
            {
                { "docType", "textDocuments" },
                { "category", "guidance" }
            };

            await client.SetMetadataAsync(metadata);
        }
        catch (RequestFailedException e)
        {
            Console.WriteLine($"HTTP error code {e.Status}: {e.ErrorCode}");
            Console.WriteLine(e.Message);
            Console.ReadLine();
        }
    }
}