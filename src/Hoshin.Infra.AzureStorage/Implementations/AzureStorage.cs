using AutoMapper;
using Hoshin.Infra.AzureStorage.Configuration;
using Hoshin.Infra.AzureStorage.DTO;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.Evidence;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Hoshin.Infra.AzureStorage.Implementations
{
    public class AzureStorage : IAzureStorageRepository
    {

        private readonly AzureStorageBlobSettings _azureStorageSettings;
        private readonly IMapper _mapper;
        public AzureStorage(IOptions<AzureStorageBlobSettings> azureStorageSettings, IMapper mapper)
        {
            _azureStorageSettings = azureStorageSettings.Value;
            _mapper = mapper;
        }

        public static string[] TYPES_OF_VALID_ARCHIVES = new string[] { "data:audio/mp3;base64,"
            ,"data:audio/wav;base64,"
            ,"data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64,"
            ,"data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,"
            ,"data:application/pdf;base64,"
        };

        public async Task<string> InsertFileAzureStorage(Evidence file, TypeData type,string containerName)
        {
            FileAzureDTO fileAzure = new FileAzureDTO();

            CloudBlobContainer blobContainer = InitializeConexionAzure(containerName).Result as CloudBlobContainer;

            var blob = blobContainer.GetBlockBlobReference(file.FileName);
            blob.UploadFromByteArrayAsync(file.Bytes,0, file.Bytes.Length, null, new BlobRequestOptions() { AbsorbConditionalErrorsOnRetry = true, RetryPolicy = new Microsoft.WindowsAzure.Storage.RetryPolicies.LinearRetry(TimeSpan.FromSeconds(20), 4) }, null).ContinueWith((res) =>
               {
                    if (res.IsCompletedSuccessfully)
                    {
                        fileAzure.FileName = blob.Name;
                        fileAzure.Base64 = blob.Name.ToString();
                        fileAzure.Url = blob.Uri.AbsoluteUri;
                    }
                    else if (res.IsFaulted)
                    {
                        string errorAnswer = res.Exception.Message.ToString();
                    }
                }).Wait();

                return fileAzure.Url;

            
        }
        public async Task<string> InsertFileAzureStorage(Evidence file, string containerName)
        {
            try
            {
                FileAzureDTO fileAzure = new FileAzureDTO();

            if (IsBase64String(file.Base64))
            {
                CloudBlobContainer blobContainer = InitializeConexionAzure(containerName).Result as CloudBlobContainer;

                    var blob = blobContainer.GetBlockBlobReference(file.FileName);
                    var bytes = Convert.FromBase64String(RemoveAttributeBase64(file.Base64));

                    using (var stream = new MemoryStream(bytes))
                    {

                        blob.UploadFromStreamAsync(stream, null, new BlobRequestOptions() { AbsorbConditionalErrorsOnRetry = true, RetryPolicy = new Microsoft.WindowsAzure.Storage.RetryPolicies.LinearRetry(TimeSpan.FromSeconds(20), 4) }, null).ContinueWith((res) =>
                        {
                            if (res.IsCompletedSuccessfully)
                            {
                                fileAzure.FileName = blob.Name;
                                fileAzure.Base64 = blob.Name.ToString();
                                fileAzure.Url = blob.Uri.AbsoluteUri;
                            }
                            else if (res.IsFaulted)
                            {
                                string errorAnswer = res.Exception.Message.ToString();
                            }
                        }).Wait();
                }

                return fileAzure.Url;
                }
                else return null;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> DeleteFileAzureStorage(Evidence file, string containerName)
        {
            try
            {
                FileAzureDTO fileAzure = new FileAzureDTO();

                CloudBlobContainer blobContainer = InitializeConexionAzure(containerName).Result as CloudBlobContainer;
                CloudBlockBlob blob = blobContainer.GetBlockBlobReference(file.FileName);
                
                await blob.DeleteAsync().ContinueWith((res =>
                {
                    if (res.IsCompletedSuccessfully)
                    {
                        fileAzure.Url = blob.Uri.AbsoluteUri.ToString();
                    }
                    else
                    {
                        fileAzure.Url = null;
                    }
                }));

                return fileAzure.Url;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static bool IsBase64String(string s)
        {
            s = s.Trim();
            return s.Contains("base64,");
        }

        private async Task<CloudBlobContainer> InitializeConexionAzure(string containerName)
        {
            try
            {
                CloudStorageAccount storageAccount = InicializarConexion(_azureStorageSettings.StorageConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerName);
                await blobContainer.CreateIfNotExistsAsync();
                BlobContainerPermissions permissions = new BlobContainerPermissions();
                permissions.PublicAccess = BlobContainerPublicAccessType.Blob;
                await blobContainer.SetPermissionsAsync(permissions);

                return blobContainer;

            }
            catch (Exception ex)
            {

                throw;
            }

           
        }

        public static CloudStorageAccount InicializarConexion(string ConnectionStringAzure)
        {
            CloudStorageAccount azureStorageBlob;
            try
            {
                azureStorageBlob = CloudStorageAccount.Parse(ConnectionStringAzure);
            }
            catch (FormatException)
            {
                throw;
            }

            return azureStorageBlob;
        }

        private static string RemoveAttributeBase64(string base64)
        {
            foreach (string type in TYPES_OF_VALID_ARCHIVES)
            {
                if (base64.Contains(type))
                    return base64.Split(',')[1];
            }
            return base64.Split(',')[1];
        }

        //public async Task<string> DownloadFileAzure(string oldFileNanme)
        //{
        //    JObject answer = new JObject();
        //    try
        //    {
        //        //CloudBlockBlob blobDownload = new CloudBlockBlob(new Uri(string.Format(_azureStorageSettings.UrlContainer + "{0}", nameFile)));

        //        CloudBlobContainer container = InitializeConexionAzure().Result as CloudBlobContainer;

        //        string newFileName = oldFileNanme.Substring(32, oldFileNanme.Length - 32);
        //        string urlDownload = string.Empty;
        //        CloudBlockBlob blobCopy = container.GetBlockBlobReference(newFileName);
        //        if (!await blobCopy.ExistsAsync())
        //        {
        //            CloudBlockBlob blob = container.GetBlockBlobReference(oldFileNanme);

        //            if (await blob.ExistsAsync())
        //            {
        //                await blobCopy.StartCopyAsync(blob);
        //                await blob.DeleteIfExistsAsync();
        //                urlDownload = container.GetBlockBlobReference(newFileName).Uri.AbsoluteUri;
        //            }
        //            else
        //            {
        //                return JsonConvert.SerializeObject(new JObject(
        //                new JProperty("Error", "OK"),
        //                new JProperty("Mensaje", "No se encontro el archivo")));
        //            }

        //        }
        //        else
        //        {
        //            urlDownload = container.GetBlockBlobReference(newFileName).Uri.AbsoluteUri;
        //        }


        //        answer = new JObject(
        //            new JProperty("Error", "OK"),
        //            new JProperty("Mensaje", urlDownload));

        //    }
        //    catch (Exception ex)
        //    {
        //        answer = new JObject(
        //        new JProperty("Error", "OK"),
        //        new JProperty("Mensaje", ex.Message.ToString()));
        //    }


        //    return JsonConvert.SerializeObject(answer);
        //}

    }
}
