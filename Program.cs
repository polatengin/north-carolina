using System.Text;
using System.Text.Json;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;

var connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING") ?? "";
var containerName = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONTAINER_NAME") ?? "";
var blobName = Environment.GetEnvironmentVariable("AZURE_STORAGE_BLOB_NAME") ?? "";

var blobServiceClient = new BlobServiceClient(connectionString);
var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

await blobContainerClient.CreateIfNotExistsAsync();

var appendBlobClient = blobContainerClient.GetAppendBlobClient(blobName);

await appendBlobClient.CreateIfNotExistsAsync();

var line = new { name = $"John Doe {Random.Shared.NextInt64()}", age = Random.Shared.Next(18, 99), timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()};

using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(line) + Environment.NewLine)))
{
  await appendBlobClient.AppendBlockAsync(stream);
}
