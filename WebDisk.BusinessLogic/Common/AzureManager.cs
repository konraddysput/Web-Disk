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
        public static void DeleteFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public byte[] Download(string blobId)
        {
            // Retrieve reference to a previously created container.
            CloudBlobContainer container = _blobClient.GetContainerReference(AzureKeys.ContainerName);

            // Retrieve reference to a blob named "photo1.jpg".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobId);

            using (var memoryStream = new MemoryStream())
            {
                blockBlob.DownloadToStream(memoryStream);
                return ByteHelper.ReadToEnd(memoryStream);
            }
        }




    }
}
