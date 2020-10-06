using Microsoft.Graph;
using System.Threading.Tasks;

namespace Hoshin.CrossCutting.MicrosoftGraph.Services.Interfaces
{
    public interface IDriveService
    {
        /// <summary>
        /// Get drive by id.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="driveId"></param>
        /// <returns></returns>
        Task<Drive> GetById(string accessToken, string hostName, string driveId);

        /// <summary>
        /// Get default drive from specified site.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <returns></returns>
        Task<Drive> GetDefaultDriveFromSite(string accessToken, string hostName);

        /// <summary>
        /// Get all drives from specified site.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <returns></returns>
        Task<ISiteDrivesCollectionPage> GetAllFromSite(string accessToken, string hostName);

        /// <summary>
        /// Get all children from a drive.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="driveId"></param>
        /// <returns></returns>
        Task<IDriveItemChildrenCollectionPage> GetAllChildrenFromDrive(string accessToken, string hostName, string driveId);

        /// <summary>
        /// Get drive by id.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="driveId"></param>
        /// <returns></returns>
        Task<string> GetByIdThroughAPI(string accessToken, string hostName, string driveId);

        /// <summary>
        /// Get default drive from specified site.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <returns></returns>
        Task<string> GetDefaultDriveFromSiteThroughAPI(string accessToken, string hostName);

        /// <summary>
        /// Get all drives from specified site.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <returns></returns>
        Task<string> GetAllFromSiteThroughAPI(string accessToken, string hostName);

        /// <summary>
        /// Get all children from a drive.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="hostName"></param>
        /// <param name="driveId"></param>
        /// <returns></returns>
        Task<string> GetAllChildrenFromDriveThroughAPI(string accessToken, string hostName, string driveId);
    }
}
