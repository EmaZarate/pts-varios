using Hoshin.CrossCutting.MicrosoftGraph.Configuration;
using Hoshin.CrossCutting.MicrosoftGraph.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hoshin.CrossCutting.MicrosoftGraph.Services.Implementations
{
    public class DriveItemService : BaseService, IDriveItemService
    {
        public DriveItemService(IOptions<MicrosoftGraphApiSettings> microsoftGraphApiSettingsOptions) : base(microsoftGraphApiSettingsOptions)
        {
        }

        public async Task<DriveItem> GetById(string accessToken, string hostName, string parentDriveId, string itemId, IList<QueryOption> options = null)
        {
            return await GetGraphServiceClient(accessToken).Sites[hostName].Drives[parentDriveId].Items[itemId].Request(options).GetAsync();
        }

        public async Task<DriveItem> GetByRelativePath(string accessToken, string hostName, string parentDriveId, string relativePath, IList<QueryOption> options = null)
        {
            return await GetGraphServiceClient(accessToken).Sites[hostName].Drives[parentDriveId].Root.ItemWithPath(relativePath).Request(options).GetAsync();
        }

        public async Task<DriveItem> GetRootFolderForDefaultDrive(string accessToken, string hostName)
        {
            return await GetGraphServiceClient(accessToken).Sites[hostName].Drive.Root.Request().GetAsync();
        }

        public async Task<IDriveItemChildrenCollectionPage> GetAllByRelativePath(string accessToken, string hostName, string parentDriveId, string relativePath, IList<QueryOption> options = null)
        {
            return await GetGraphServiceClient(accessToken).Sites[hostName].Drives[parentDriveId].Root.ItemWithPath(relativePath).Children.Request(options).GetAsync();
        }

        public async Task<IDriveItemSearchCollectionPage> Search(string accessToken, string hostName, string driveId, string query)
        {
            return await GetGraphServiceClient(accessToken).Sites[hostName].Drives[driveId].Root.Search(query).Request().GetAsync();
        }

        public async Task<Stream> DownloadDriveItemContent(string accessToken, string hostName, string parentDriveId, string itemId)
        {
            return await GetGraphServiceClient(accessToken).Sites[hostName].Drives[parentDriveId].Items[itemId].Content.Request().GetAsync();
        }

        public async Task<DriveItem> CreateFolder(string accessToken, string hostName, string parentDriveId, string parentItemId, string folderName)
        {
            var items = GetGraphServiceClient(accessToken).Sites[hostName].Drives[parentDriveId].Items;

            if (string.IsNullOrWhiteSpace(parentItemId))
            {
                return await items.Request().AddAsync(new DriveItem { Name = folderName, Folder = new Folder() });
            }
            else
            {
                return await items[parentItemId].Children.Request().AddAsync(new DriveItem { Name = folderName, Folder = new Folder() });
            }
        }

        public async Task<DriveItem> UpdateNameDescriptionById(string accessToken, string hostName, string parentDriveId, string itemId, string name, string description)
        {
            return await UpdateNameDescription(GetGraphServiceClient(accessToken).Sites[hostName].Drives[parentDriveId].Items[itemId], name, description);
        }

        public async Task<DriveItem> UpdateNameDescriptionByRelativePath(string accessToken, string hostName, string parentDriveId, string relativePath, string name, string description)
        {
            return await UpdateNameDescription(GetGraphServiceClient(accessToken).Sites[hostName].Drives[parentDriveId].Root.ItemWithPath(relativePath), name, description);
        }

        /// <summary>
        /// Update drive item´s name and description.
        /// </summary>
        /// <param name="driveItemRequestBuilder"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        private async Task<DriveItem> UpdateNameDescription(IDriveItemRequestBuilder driveItemRequestBuilder, string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(description)) throw new Exception("Error: you have to provide at least name or description.");

            DriveItem driveItem = new DriveItem();
            if (!string.IsNullOrWhiteSpace(name)) driveItem.Name = name;
            if (!string.IsNullOrWhiteSpace(description)) driveItem.Description = description;

            return await driveItemRequestBuilder.Request().UpdateAsync(driveItem);
        }
        
        public async Task<FieldValueSet> UpdateListItemPropertiesById(string accessToken, string hostName, string parentDriveId, string itemId, IDictionary<string, object> fields)
        {
            return await UpdateListItemProperties(GetGraphServiceClient(accessToken).Sites[hostName].Drives[parentDriveId].Items[itemId], fields);
        }

        public async Task<FieldValueSet> UpdateListItemPropertiesByRelativePath(string accessToken, string hostName, string parentDriveId, string relativePath, IDictionary<string, object> fields)
        {
            return await UpdateListItemProperties(GetGraphServiceClient(accessToken).Sites[hostName].Drives[parentDriveId].Root.ItemWithPath(relativePath), fields);
        }

        /// <summary>
        /// Update drive item´s list item properties.
        /// </summary>
        /// <param name="driveItemRequestBuilder"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        private async Task<FieldValueSet> UpdateListItemProperties(IDriveItemRequestBuilder driveItemRequestBuilder, IDictionary<string, object> fields)
        {
            return await driveItemRequestBuilder.ListItem.Fields.Request().UpdateAsync(new FieldValueSet { AdditionalData = fields });
        }

        public async Task<DriveItem> UploadFileById(string accessToken, string hostName, string parentDriveId, string itemId, string fileName, string base64File)
        {
            if (string.IsNullOrWhiteSpace(itemId))
            {
                DriveItem driveItem = await CreateDriveItem(accessToken, hostName, parentDriveId, new DriveItem { Name = fileName, File = new Microsoft.Graph.File() });
                itemId = driveItem.Id;
            }

            return await GetGraphServiceClient(accessToken).Sites[hostName].Drives[parentDriveId].Items[itemId].Content.Request().PutAsync<DriveItem>(new MemoryStream(Convert.FromBase64String(base64File)));
        }

        /// <summary>
        /// Create new drive item.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="driveItem"></param>
        /// <returns></returns>
        private async Task<DriveItem> CreateDriveItem(string accessToken, string hostName, string parentDriveId, DriveItem driveItem)
        {
            return await GetGraphServiceClient(accessToken).Sites[hostName].Drives[parentDriveId].Items.Request().AddAsync(driveItem);
        }

        public async Task<DriveItem> UploadFileByRelativePath(string accessToken, string hostName, string parentDriveId, string relativePath, string base64File)
        {
            return await GetGraphServiceClient(accessToken).Sites[hostName].Drives[parentDriveId].Root.ItemWithPath(relativePath).Content.Request().PutAsync<DriveItem>(new MemoryStream(Convert.FromBase64String(base64File)));
        }

        public async Task<DriveItem> UploadLargeFileById(string accessToken, string hostName, string parentDriveId, string itemId, string fileName, string clientMachineRelativePath)
        {
            return await UploadLargeFile(accessToken, hostName, parentDriveId, itemId, fileName, null, clientMachineRelativePath);
        }

        public async Task<DriveItem> UploadLargeFileByRelativePath(string accessToken, string hostName, string parentDriveId, string relativePath, string clientMachineRelativePath)
        {
            return await UploadLargeFile(accessToken, hostName, parentDriveId, null, null, relativePath, clientMachineRelativePath);
        }

        /// <summary>
        /// Upload or replace the contents of a file (more than 4MB).
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="itemId"></param>
        /// <param name="fileName"></param>
        /// <param name="relativePath"></param>
        /// <param name="clientMachineRelativePath"></param>
        /// <returns></returns>
        private async Task<DriveItem> UploadLargeFile(string accessToken, string hostName, string parentDriveId, string itemId, string fileName, string relativePath, string clientMachineRelativePath)
        {
            using (MemoryStream ms = new MemoryStream(System.IO.File.ReadAllBytes(clientMachineRelativePath)))
            {
                // Describe the file to upload. Pass into CreateUploadSession, when the service works as expected.
                //var props = new DriveItemUploadableProperties();
                //props.Name = "_hamilton.png";
                //props.Description = "This is a pictureof Mr. Hamilton.";
                //props.FileSystemInfo = new FileSystemInfo();
                //props.FileSystemInfo.CreatedDateTime = System.DateTimeOffset.Now;
                //props.FileSystemInfo.LastModifiedDateTime = System.DateTimeOffset.Now;

                var graphClient = GetGraphServiceClient(accessToken);

                UploadSession uploadSession = null;
                if (!string.IsNullOrWhiteSpace(relativePath))
                {
                    if (!string.IsNullOrWhiteSpace(parentDriveId))
                    {
                        uploadSession = await graphClient.Sites[hostName].Drives[parentDriveId].Root.ItemWithPath(relativePath).CreateUploadSession().Request().PostAsync();
                    }
                    else
                    {
                        uploadSession = await graphClient.Sites[hostName].Drive.Root.ItemWithPath(relativePath).CreateUploadSession().Request().PostAsync();
                    }
                }
                else if (!string.IsNullOrWhiteSpace(parentDriveId) && !string.IsNullOrWhiteSpace(fileName))
                {
                    if (!string.IsNullOrWhiteSpace(itemId))
                    {
                        uploadSession = await graphClient.Sites[hostName].Drives[parentDriveId].Items[itemId].ItemWithPath(fileName).CreateUploadSession().Request().PostAsync();
                    }
                    else
                    {
                        uploadSession = await graphClient.Sites[hostName].Drives[parentDriveId].Root.ItemWithPath(fileName).CreateUploadSession().Request().PostAsync();
                    }
                }

                //Note: If your app splits a file into multiple byte ranges, the size of each byte range MUST be a multiple of 320 KiB(327, 680 bytes).Using a fragment size that does not divide evenly by 320 KiB will result in errors committing some files.
                var maxChunkSize = 327680; //320 * 1024; // 320 KB
                var provider = new ChunkedUploadProvider(uploadSession, graphClient, ms, maxChunkSize);
                // Setup the chunk request necessities
                var chunkRequests = provider.GetUploadChunkRequests();
                var readBuffer = new byte[maxChunkSize];
                var trackedExceptions = new List<Exception>();
                DriveItem itemResult = null;
                //upload the chunks
                foreach (var request in chunkRequests)
                {
                    // Do your updates here: update progress bar, etc.
                    // ...
                    // Send chunk request
                    var result = await provider.GetChunkRequestResponseAsync(request, readBuffer, trackedExceptions);
                    if (result.UploadSucceeded)
                    {
                        itemResult = result.ItemResponse;
                    }
                }
                // Check that upload succeeded
                if (itemResult == null)
                {
                    // Retry the upload
                    // ...
                }

                return itemResult;
            }
        }

        public async Task<DriveItem> Copy(string accessToken, string hostName, string parentDriveId, string itemId, string fileName, string parentReferenceDriveId, string parentReferenceDriveItemId)
        {
            return await GetGraphServiceClient(accessToken).Sites[hostName].Drives[parentDriveId].Items[itemId]
                .Copy(!string.IsNullOrWhiteSpace(fileName) ? fileName : null, !string.IsNullOrWhiteSpace(parentReferenceDriveId) && !string.IsNullOrWhiteSpace(parentReferenceDriveItemId) ? new ItemReference { DriveId = parentReferenceDriveId, Id = parentReferenceDriveItemId } : null).Request().PostAsync();
        }

        public async Task DeleteById(string accessToken, string hostName, string parentDriveId, string itemId)
        {
            await GetGraphServiceClient(accessToken).Sites[hostName].Drives[parentDriveId].Items[itemId].Request().DeleteAsync();
        }

        public async Task DeleteByRelativePath(string accessToken, string hostName, string parentDriveId, string relativePath)
        {
            await GetGraphServiceClient(accessToken).Sites[hostName].Drives[parentDriveId].Root.ItemWithPath(relativePath).Request().DeleteAsync();
        }

        public async Task<string> GetByIdThroughAPI(string accessToken, string hostName, string parentDriveId, string itemId)
        {
            string response;
            using (HttpClient httpClient = GetHttpClient(accessToken))
            {
                response = await httpClient.GetStringAsync(GetQueryStringForGetById(hostName, parentDriveId) + "/items/" + itemId);
            }

            return response;
        }

        public async Task<string> GetByRelativePathThroughAPI(string accessToken, string hostName, string parentDriveId, string relativePath)
        {
            string response;
            using (HttpClient httpClient = GetHttpClient(accessToken))
            {
                response = await httpClient.GetStringAsync(GetQueryStringForGetByRelativePath(hostName, parentDriveId, relativePath));
            }

            return response;
        }

        public async Task<string> GetAllByRelativePathThroughAPI(string accessToken, string hostName, string parentDriveId, string relativePath)
        {
            string response;
            using (HttpClient httpClient = GetHttpClient(accessToken))
            {
                response = await httpClient.GetStringAsync(GetQueryStringForGetByRelativePath(hostName, parentDriveId, relativePath) + "children");
            }

            return response;
        }

        public async Task<HttpResponseMessage> CheckoutByIdThroughAPI(string accessToken, string hostName, string parentDriveId, string itemId)
        {
            HttpResponseMessage response;
            using (HttpClient httpClient = GetHttpClient(accessToken))
            {
                response = await httpClient.PostAsync(GetQueryStringForGetById(hostName, parentDriveId, Version.Beta) + "/items/" + itemId + "/checkout", null);
            }

            return response;
        }

        public async Task<HttpResponseMessage> CheckoutByRelativePathThroughAPI(string accessToken, string hostName, string parentDriveId, string relativePath)
        {
            HttpResponseMessage response;
            using (HttpClient httpClient = GetHttpClient(accessToken))
            {
                response = await httpClient.PostAsync(GetQueryStringForGetByRelativePath(hostName, parentDriveId, relativePath, Version.Beta) + "checkout", null);
            }

            return response;
        }

        public async Task<HttpResponseMessage> CheckinByIdThroughAPI(string accessToken, string hostName, string parentDriveId, string itemId, string propertiesValuesJson)
        {
            return await ExecuteAsyncJsonContent(HTTPMethod.Post, accessToken, GetQueryStringForGetById(hostName, parentDriveId, Version.Beta) + "/items/" + itemId + "/checkin", propertiesValuesJson);
        }

        public async Task<HttpResponseMessage> CheckinByRelativePathThroughAPI(string accessToken, string hostName, string parentDriveId, string relativePath, string propertiesValuesJson)
        {
            return await ExecuteAsyncJsonContent(HTTPMethod.Post, accessToken, GetQueryStringForGetByRelativePath(hostName, parentDriveId, relativePath, Version.Beta) + "checkin", propertiesValuesJson);
        }

        public async Task<HttpResponseMessage> UpdatePropertiesByIdThroughAPI(string accessToken, string hostName, string parentDriveId, string itemId, string propertiesValuesJson, bool areListitemProperties)
        {
            return await ExecuteAsyncJsonContent(HTTPMethod.Patch, accessToken, GetQueryStringForGetById(hostName, parentDriveId) + "/items/" + itemId + (areListitemProperties ? "/listItem/fields" : string.Empty), propertiesValuesJson);
        }

        public async Task<HttpResponseMessage> UpdatePropertiesByRelativePathThroughAPI(string accessToken, string hostName, string parentDriveId, string relativePath, string propertiesValuesJson)
        {
            return await ExecuteAsyncJsonContent(HTTPMethod.Patch, accessToken, GetQueryStringForGetByRelativePath(hostName, parentDriveId, relativePath), propertiesValuesJson);
        }

        public async Task<HttpResponseMessage> UploadFileByIdThroughAPI(string accessToken, string hostName, string parentDriveId, string fileName, string base64File)
        {
            HttpResponseMessage response;
            using (HttpClient httpClient = GetHttpClient(accessToken))
            {
                response = await httpClient.PutAsync(GetQueryStringForGetById(hostName, parentDriveId) + "/root:/" + fileName + ":/content", new ByteArrayContent(Convert.FromBase64String(base64File)));
            }

            return response;
        }
        
        public async Task<HttpResponseMessage> UploadFileByRelativePathThroughAPI(string accessToken, string hostName, string parentDriveId, string relativePath, string base64File)
        {
            HttpResponseMessage response;
            using (HttpClient httpClient = GetHttpClient(accessToken))
            {
                response = await httpClient.PutAsync(GetQueryStringForGetByRelativePath(hostName, parentDriveId, relativePath) + "content", new ByteArrayContent(Convert.FromBase64String(base64File)));
            }

            return response;
        }

        public async Task<HttpResponseMessage> DeleteByIdThroughAPI(string accessToken, string hostName, string parentDriveId, string itemId)
        {
            HttpResponseMessage response;
            using (HttpClient httpClient = GetHttpClient(accessToken))
            {
                response = await httpClient.DeleteAsync(GetQueryStringForGetById(hostName, parentDriveId) + "/items/" + itemId);
            }

            return response;
        }

        public async Task<HttpResponseMessage> DeleteByRelativePathThroughAPI(string accessToken, string hostName, string parentDriveId, string relativePath)
        {
            HttpResponseMessage response;
            using (HttpClient httpClient = GetHttpClient(accessToken))
            {
                response = await httpClient.DeleteAsync(GetQueryStringForGetByRelativePath(hostName, parentDriveId, relativePath));
            }

            return response;
        }
    }
}
