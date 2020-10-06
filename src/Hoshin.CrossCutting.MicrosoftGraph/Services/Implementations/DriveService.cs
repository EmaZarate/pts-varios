using Hoshin.CrossCutting.MicrosoftGraph.Configuration;
using Hoshin.CrossCutting.MicrosoftGraph.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hoshin.CrossCutting.MicrosoftGraph.Services.Implementations
{
    public class DriveService : BaseService, IDriveService
    {
        public DriveService(IOptions<MicrosoftGraphApiSettings> microsoftGraphApiSettingsOptions) : base(microsoftGraphApiSettingsOptions)
        {
        }

        public async Task<Drive> GetById(string accessToken, string hostName, string driveId)
        {
            return await GetGraphServiceClient(accessToken).Sites[hostName].Drives[driveId].Request().GetAsync();
        }

        public async Task<Drive> GetDefaultDriveFromSite(string accessToken, string hostName)
        {
            return await GetGraphServiceClient(accessToken).Sites[hostName].Drive.Request().GetAsync();
        }

        public async Task<ISiteDrivesCollectionPage> GetAllFromSite(string accessToken, string hostName)
        {
            return await GetGraphServiceClient(accessToken).Sites[hostName].Drives.Request().GetAsync();
        }

        public async Task<IDriveItemChildrenCollectionPage> GetAllChildrenFromDrive(string accessToken, string hostName, string driveId)
        {
            return await GetGraphServiceClient(accessToken).Sites[hostName].Drives[driveId].Root.Children.Request().GetAsync();
        }

        public async Task<string> GetByIdThroughAPI(string accessToken, string hostName, string driveId)
        {
            string response;
            using (HttpClient httpClient = GetHttpClient(accessToken))
            {
                response = await httpClient.GetStringAsync(GetQueryStringForGetById(hostName, driveId));
            }

            return response;
        }

        public async Task<string> GetDefaultDriveFromSiteThroughAPI(string accessToken, string hostName)
        {
            string response;
            using (HttpClient httpClient = GetHttpClient(accessToken))
            {
                response = await httpClient.GetStringAsync(GetQueryStringForGetHost(hostName) + "/drive");
            }

            return response;
        }

        public async Task<string> GetAllFromSiteThroughAPI(string accessToken, string hostName)
        {
            string response;
            using (HttpClient httpClient = GetHttpClient(accessToken))
            {
                response = await httpClient.GetStringAsync(GetQueryStringForGetHost(hostName) + "/drives");
            }

            return response;
        }

        public async Task<string> GetAllChildrenFromDriveThroughAPI(string accessToken, string hostName, string driveId)
        {
            string response;
            using (HttpClient httpClient = GetHttpClient(accessToken))
            {
                response = await httpClient.GetStringAsync(GetQueryStringForGetById(hostName, driveId) + "/root/children");
            }

            return response;
        }
    }
}
