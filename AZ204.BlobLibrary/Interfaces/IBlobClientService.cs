using AZ2_4.BlobLibrary.Services;
using Azure.Storage.Blobs;

namespace AZ2_4.BlobLibrary.Interfaces;

public interface IBlobClientService
{
    BlobServiceClient GetBlobServiceClient(string connectionString);
    BlobServiceClient GetBlobServiceClientUsingAccountName(string accountName);
    BlobContainerClient GetContainerClient(BlobServiceClient serviceClient, string containerName);
    BlobContainerClient GetContainerClient(string accountName, string containerName);

    BlobContainerClient GetContainerClient(
        string accountName, 
        string containerName,
        BlobClientOptions clientOptions);

    BlobClient GetBlobClient(BlobServiceClient blobServiceClient, string containerName, string blobName);
    BlobClient GetBlobClient(BlobContainerClient containerClient, string blobName);
}