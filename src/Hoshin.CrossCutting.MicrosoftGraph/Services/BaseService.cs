using Hoshin.CrossCutting.MicrosoftGraph.Configuration;
using Microsoft.Graph;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;

namespace Hoshin.CrossCutting.MicrosoftGraph.Services
{
    public abstract class BaseService
    {
        public enum Version { Beta, V10 };
        public enum HTTPMethod { Patch, Post };
        private readonly MicrosoftGraphApiSettings _microsoftGraphApiSettings;

        public BaseService(IOptions<MicrosoftGraphApiSettings> microsoftGraphApiSettingsOptions)
        {
            _microsoftGraphApiSettings = microsoftGraphApiSettingsOptions.Value;
        }

        /// <summary>
        /// Get query string version to use Microsoft.Graph API.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public string GetQueryStringVersion(Version? version = null)
        {
            switch (version)
            {
                case Version.Beta: return _microsoftGraphApiSettings.VersionBeta;
                case Version.V10: return _microsoftGraphApiSettings.Version10;
                default: return _microsoftGraphApiSettings.Version;
            }
        }

        /// <summary>
        /// Get base query string to use Microsoft.Graph API.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public string GetQueryString(Version? version = null)
        {
            return _microsoftGraphApiSettings.Protocol + "://" + _microsoftGraphApiSettings.HostName + "/" + GetQueryStringVersion(version);
        }

        /// <summary>
        /// Get query string for get host.
        /// </summary>
        /// <param name="hostName"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public string GetQueryStringForGetHost(string hostName, Version? version = null)
        {
            return GetQueryString(version) + "/sites/" + hostName;
        }

        /// <summary>
        /// Get query string for GetById.
        /// </summary>
        /// <param name="hostName"></param>
        /// <param name="driveId">if driveId is null, returns query string for the default drive url.</param>
        /// <param name="version"></param>
        /// <returns></returns>
        public string GetQueryStringForGetById(string hostName, string driveId, Version? version = null)
        {
            return GetQueryStringForGetHost(hostName, version) + (string.IsNullOrWhiteSpace(driveId) ? "/drive" : "/drives/" + driveId);
        }

        /// <summary>
        /// Get query string for GetByIdGetByRelativePath.
        /// </summary>
        /// <param name="hostName"></param>
        /// <param name="driveId"></param>
        /// <param name="relativePath"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public string GetQueryStringForGetByRelativePath(string hostName, string driveId, string relativePath, Version? version = null)
        {
            return (string.IsNullOrWhiteSpace(driveId) ?
                        GetQueryStringForGetHost(hostName, version) + "/drive" :
                        GetQueryStringForGetById(hostName, driveId, version)) +
                    "/root:/" + relativePath + ":/";
        }

        /// <summary>
        /// Get the GraphServiceClient to query through Microsoft.Graph.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public GraphServiceClient GetGraphServiceClient(string accessToken)
        {
            return new GraphServiceClient(new DelegateAuthenticationProvider((requestMessage) =>
            {
                requestMessage.Headers.Authorization = GetAuthenticationHeaderValue(accessToken);
                return Task.FromResult(0);
            }));
        }

        /// <summary>
        /// Get the HttpClient configured for the specified access token.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public HttpClient GetHttpClient(string accessToken)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = GetAuthenticationHeaderValue(accessToken);
            return httpClient;
        }

        /// <summary>
        /// Get the AuthenticationHeaderValue for the specified access token.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public AuthenticationHeaderValue GetAuthenticationHeaderValue(string accessToken)
        {
            return new AuthenticationHeaderValue("Bearer", accessToken);
        }

        /// <summary>
        /// Get HTTP method name.
        /// </summary>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        public string GetHTTPMethodName(HTTPMethod httpMethod)
        {
            switch (httpMethod)
            {
                case HTTPMethod.Patch: return "PATCH";
                case HTTPMethod.Post: return "POST";
                default: return string.Empty;
            }
        }

        /// <summary>
        /// Executes async method with json content.
        /// </summary>
        /// <param name="httpMethod"></param>
        /// <param name="accessToken"></param>
        /// <param name="requestUri"></param>
        /// <param name="propertiesValuesJson"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ExecuteAsyncJsonContent(HTTPMethod httpMethod, string accessToken, string requestUri, string propertiesValuesJson)
        {
            HttpMethod method = new HttpMethod(GetHTTPMethodName(httpMethod));
            HttpRequestMessage request = new HttpRequestMessage(method, requestUri)
            {
                Content = new StringContent(propertiesValuesJson, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response;
            using (HttpClient httpClient = GetHttpClient(accessToken))
            {
                response = await httpClient.SendAsync(request);
            }
            return response;
        }
    }
}
