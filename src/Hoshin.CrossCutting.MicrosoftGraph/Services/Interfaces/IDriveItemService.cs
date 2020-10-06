using Microsoft.Graph;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hoshin.CrossCutting.MicrosoftGraph.Services.Interfaces
{
    public interface IDriveItemService
    {
        /// <summary>
        /// Get drive item by id.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="itemId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<DriveItem> GetById(string accessToken, string hostName, string parentDriveId, string itemId, IList<QueryOption> options = null);
        
        /// <summary>
        /// Get drive item by relative path.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="relativePath"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<DriveItem> GetByRelativePath(string accessToken, string hostName, string parentDriveId, string relativePath, IList<QueryOption> options = null);

        /// <summary>
        /// Get root folder for user's default Drive.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <returns></returns>
        Task<DriveItem> GetRootFolderForDefaultDrive(string accessToken, string hostName);

        /// <summary>
        /// Get all drive items inside a folder by relative path.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="relativePath"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<IDriveItemChildrenCollectionPage> GetAllByRelativePath(string accessToken, string hostName, string parentDriveId, string relativePath, IList<QueryOption> options = null);

        /// <summary>
        /// Search for a DriveItems within a drive.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="driveId"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<IDriveItemSearchCollectionPage> Search(string accessToken, string hostName, string driveId, string query);

        /// <summary>
        /// Download the contents of the primary stream (file) of a DriveItem.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        Task<Stream> DownloadDriveItemContent(string accessToken, string hostName, string parentDriveId, string itemId);

        /// <summary>
        /// Create folder in the parent item specified.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="parentItemId"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        Task<DriveItem> CreateFolder(string accessToken, string hostName, string parentDriveId, string parentItemId, string folderName);

        /// <summary>
        /// Update drive item´s name and description by id.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="itemId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        Task<DriveItem> UpdateNameDescriptionById(string accessToken, string hostName, string parentDriveId, string itemId, string name, string description);

        /// <summary>
        /// Update drive item´s name and description by relative path.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="relativePath"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        Task<DriveItem> UpdateNameDescriptionByRelativePath(string accessToken, string hostName, string parentDriveId, string relativePath, string name, string description);

        /// <summary>
        /// Update drive item´s list item properties by id.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="itemId"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        Task<FieldValueSet> UpdateListItemPropertiesById(string accessToken, string hostName, string parentDriveId, string itemId, IDictionary<string, object> fields);

        /// <summary>
        /// Update drive item´s list item properties by relative path.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="relativePath"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        Task<FieldValueSet> UpdateListItemPropertiesByRelativePath(string accessToken, string hostName, string parentDriveId, string relativePath, IDictionary<string, object> fields);

        /// <summary>
        /// Upload or replace the contents of a file by id.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="itemId">Set to replace the contents of a existing file</param>
        /// <param name="fileName"></param>
        /// <param name="base64File"></param>
        /// <returns></returns>
        Task<DriveItem> UploadFileById(string accessToken, string hostName, string parentDriveId, string itemId, string fileName, string base64File);

        /// <summary>
        /// Upload or replace the contents of a file by relative path.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="relativePath"></param>
        /// <param name="base64File"></param>
        /// <returns></returns>
        Task<DriveItem> UploadFileByRelativePath(string accessToken, string hostName, string parentDriveId, string relativePath, string base64File);

        /// <summary>
        /// Upload or replace the contents of a file by id (more than 4MB).
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="itemId"></param>
        /// <param name="fileName"></param>
        /// <param name="clientMachineRelativePath"></param>
        /// <returns></returns>
        Task<DriveItem> UploadLargeFileById(string accessToken, string hostName, string parentDriveId, string itemId, string fileName, string clientMachineRelativePath);

        /// <summary>
        /// Upload or replace the contents of a file by relative path (more than 4MB).
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="relativePath"></param>
        /// <param name="clientMachineRelativePath"></param>
        /// <returns></returns>
        Task<DriveItem> UploadLargeFileByRelativePath(string accessToken, string hostName, string parentDriveId, string relativePath, string clientMachineRelativePath);

        /// <summary>
        /// Asynchronously creates a copy of an driveItem (including any children), under a new parent item or with a new name.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="itemId">Id of the drive item to copy</param>
        /// <param name="fileName"></param>
        /// <param name="parentReferenceDriveId"></param>
        /// <param name="parentReferenceDriveItemId"></param>
        /// <returns></returns>
        Task<DriveItem> Copy(string accessToken, string hostName, string parentDriveId, string itemId, string fileName, string parentReferenceDriveId, string parentReferenceDriveItemId);

        /// <summary>
        /// Delete drive item by id.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        Task DeleteById(string accessToken, string hostName, string parentDriveId, string itemId);

        /// <summary>
        /// Delete drive item by relative path.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        Task DeleteByRelativePath(string accessToken, string hostName, string parentDriveId, string relativePath);

        /// <summary>
        /// Get drive item by id.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        Task<string> GetByIdThroughAPI(string accessToken, string hostName, string parentDriveId, string itemId);

        /// <summary>
        /// Get drive item by relative path.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        Task<string> GetByRelativePathThroughAPI(string accessToken, string hostName, string parentDriveId, string relativePath);

        /// <summary>
        /// Get all drive items inside a drive by relative path.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        Task<string> GetAllByRelativePathThroughAPI(string accessToken, string hostName, string parentDriveId, string relativePath);

        /// <summary>
        /// Checkout drive item by id.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> CheckoutByIdThroughAPI(string accessToken, string hostName, string parentDriveId, string itemId);

        /// <summary>
        /// Checkout drive item by relative path.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> CheckoutByRelativePathThroughAPI(string accessToken, string hostName, string parentDriveId, string relativePath);

        /// <summary>
        /// Checkin drive item by id.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="itemId"></param>
        /// <param name="propertiesValuesJson"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> CheckinByIdThroughAPI(string accessToken, string hostName, string parentDriveId, string itemId, string propertiesValuesJson);

        /// <summary>
        /// Checkin drive item by relative path.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="relativePath"></param>
        /// <param name="propertiesValuesJson"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> CheckinByRelativePathThroughAPI(string accessToken, string hostName, string parentDriveId, string relativePath, string propertiesValuesJson);

        /// <summary>
        /// Update the metadata for a drive item by id.
        /// You can also move an item to another parent by updating the item's parentReference property.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="itemId"></param>
        /// <param name="propertiesValuesJson"></param>
        /// <param name="areListitemProperties">True if properties to modify are from the drive item´s list item</param>
        /// <returns></returns>
        Task<HttpResponseMessage> UpdatePropertiesByIdThroughAPI(string accessToken, string hostName, string parentDriveId, string itemId, string propertiesValuesJson, bool areListitemProperties);

        /// <summary>
        /// Update the metadata for a drive item by relative path.
        /// You can also move an item to another parent by updating the item's parentReference property.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="relativePath"></param>
        /// <param name="propertiesValuesJson"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> UpdatePropertiesByRelativePathThroughAPI(string accessToken, string hostName, string parentDriveId, string relativePath, string propertiesValuesJson);

        /// <summary>
        /// Upload or replace the contents of a file by id (up to 4MB).
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="fileName"></param>
        /// <param name="base64File"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> UploadFileByIdThroughAPI(string accessToken, string hostName, string parentDriveId, string fileName, string base64File);

        /// <summary>
        /// Upload or replace the contents of a file by id (up to 4MB).
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="relativePath"></param>
        /// <param name="base64File"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> UploadFileByRelativePathThroughAPI(string accessToken, string hostName, string parentDriveId, string relativePath, string base64File);

        /// <summary>
        /// Delete drive item by id.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> DeleteByIdThroughAPI(string accessToken, string hostName, string parentDriveId, string itemId);

        /// <summary>
        /// Delete drive item by relative path.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="parentDriveId"></param>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> DeleteByRelativePathThroughAPI(string accessToken, string hostName, string parentDriveId, string relativePath);
    }
}
