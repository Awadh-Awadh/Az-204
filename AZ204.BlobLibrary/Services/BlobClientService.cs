using AZ2_4.BlobLibrary.Interfaces;
using Azure.Identity;
using Azure.Storage.Blobs;

namespace AZ2_4.BlobLibrary.Services;

public class BlobClientService : IBlobClientService
{
    public BlobServiceClient GetBlobServiceClient(string connectionString) => new (connectionString);

    public BlobServiceClient GetBlobServiceClientUsingAccountName(string accountName)
    {
        BlobServiceClient client = new(
            new Uri($"https://{accountName}.blob.core.windows.net"),
            new DefaultAzureCredential()
            );

        return client;
    }

    public BlobContainerClient GetContainerClient(BlobServiceClient serviceClient, string containerName)
    {
        BlobContainerClient client = serviceClient.GetBlobContainerClient(containerName);

        return client;
    }

    public BlobContainerClient GetContainerClient(string accountName, string containerName)
    {
        BlobContainerClient client = new(new Uri($"https://{accountName}.blob.core.windows.net/{containerName}"),
            new DefaultAzureCredential());

        return client;
    }
    

    public BlobContainerClient GetContainerClient(string accountName, string containerName, BlobClientOptions clientOptions)
    {
        BlobContainerClient client = new(new Uri($"https://{accountName}.blob.core.windows.net/{containerName}"),
            new DefaultAzureCredential(),
            clientOptions);

        return client;
    }

    public BlobClient GetBlobClient(BlobServiceClient blobServiceClient, string containerName, string blobName)
    {
        BlobClient client = blobServiceClient
            .GetBlobContainerClient(containerName)
            .GetBlobClient(blobName);

        return client;
    }

    public BlobClient GetBlobClient(BlobContainerClient containerClient, string blobName)
    {
        var client = containerClient.GetBlobClient(blobName);

        return client;
    }
    
}