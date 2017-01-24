using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using WebDisk.BusinessLogic.Keys;

namespace WebDisk.BusinessLogic.Common
{
    public class AzureManager
    {
        private readonly CloudBlobClient _blobClient;

        public AzureManager()
        {
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AzureKeys.CloudStorageAccountConnectionString);
            _blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve a reference to a container.
            CloudBlobContainer container = _blobClient.GetContainerReference(AzureKeys.ContainerName);
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
        }

        /// <summary>
        /// Method that allow user to store a data in Azure data store Defined by a project author
        /// </summary>
        /// <param name="content">array of file bytes</param>
        /// <returns>path to uploaded file</returns>
        public string UploadFile(byte[] content)
        {
            return UploadFile(ByteHelper.ByteArrayToStream(content));
        }

        public string Copy(string blobId)
        {
            var newBlobId = $"{Guid.NewGuid()}{Guid.NewGuid()}";
            return Copy(blobId, newBlobId);
        }

        public string Copy(string blobId, string newBlobId)
        {
            CloudBlobContainer container = _blobClient.GetContainerReference(AzureKeys.ContainerName);

            // Retrieve reference to a blob by using blobid".
            CloudBlockBlob sourceBlob = container.GetBlockBlobReference(blobId);
            CloudBlockBlob targetBlob = container.GetBlockBlobReference(newBlobId);
            targetBlob.StartCopy(sourceBlob);
            return newBlobId;
        }


        public string UploadFile(Stream content)
        {
            // Retrieve reference to a previously created container.
            CloudBlobContainer container = _blobClient.GetContainerReference(AzureKeys.ContainerName);

            // Retrieve reference to a blob named "myblob".
            var blobId = $"{Guid.NewGuid()}{Guid.NewGuid()}";
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobId);

            blockBlob.UploadFromStream(content);
            return blobId;
        }

        public static void DeleteFile(string blobReference)
        {
            if (string.IsNullOrWhiteSpace(blobReference))
            {
                throw new ArgumentException("Invalid blob reference");
            }
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(AzureKeys.CloudStorageAccountConnectionString);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(AzureKeys.ContainerName);

            // Retrieve reference to a blob named "myblob.txt".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobReference);

            // Delete the blob.
            blockBlob.Delete();
        }

        public byte[] Download(string blobId)
        {
            // Retrieve reference to a previously created container.
            CloudBlobContainer container = _blobClient.GetContainerReference(AzureKeys.ContainerName);

            // Retrieve reference to a blob by using blobid.
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobId);

            using (var memoryStream = new MemoryStream())
            {
                blockBlob.DownloadToStream(memoryStream);
                return ByteHelper.ReadToEnd(memoryStream);
            }
        }
    }
}
