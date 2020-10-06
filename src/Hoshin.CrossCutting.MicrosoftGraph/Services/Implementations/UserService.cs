using Hoshin.CrossCutting.MicrosoftGraph.Configuration;
using Hoshin.CrossCutting.MicrosoftGraph.DTO.User;
using Hoshin.CrossCutting.MicrosoftGraph.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hoshin.CrossCutting.MicrosoftGraph.Services.Implementations
{
    public class UserService : BaseService, IUserService
    {
        private enum TypeTokenRequest { AccessToken, RefreshToken };
        private readonly MicrosoftGraphAuthSettings _msftAuthSettings;
        public UserService(IOptions<MicrosoftGraphApiSettings> microsoftGraphApiSettingsOptions, IOptions<MicrosoftGraphAuthSettings> msftAuthSettings) : base(microsoftGraphApiSettingsOptions)
        {
            _msftAuthSettings = msftAuthSettings.Value;
        }

        public async Task<MicrosoftGraphAppAccessToken> GetAccessToken(string accessToken)
        {
            var list = MakeRequestBody(_msftAuthSettings, accessToken, TypeTokenRequest.AccessToken);
            MicrosoftGraphAppAccessToken appAccesToken = await MakeTokenPost(list);
            appAccesToken.TimeAcquired = DateTime.Now;

            return appAccesToken;
        }
        public async Task<MicrosoftGraphAppAccessToken> RefreshAccessToken(string accessToken)
        {
            var list = MakeRequestBody(_msftAuthSettings, accessToken, TypeTokenRequest.RefreshToken);
            return await MakeTokenPost(list); 
        }

        public async Task<MicrosoftGraphUserData> GetMe(string accessToken)
        {
            string response;
            using (HttpClient httpClient = GetHttpClient(accessToken))
            {
                response = await httpClient.GetStringAsync(GetQueryString() + "/me");
            }

            return JsonConvert.DeserializeObject<MicrosoftGraphUserData>(response);
        }

        private IList<KeyValuePair<string, string>> MakeRequestBody(MicrosoftGraphAuthSettings _authSettings, string accessToken, TypeTokenRequest type)
        {
            var list = new List<KeyValuePair<string, string>>();
            switch (type)
            {
                case TypeTokenRequest.AccessToken:
                    list.Add(new KeyValuePair<string, string>("code", accessToken));
                    list.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
                    break;
                case TypeTokenRequest.RefreshToken:
                    list.Add(new KeyValuePair<string, string>("refresh_token", accessToken));
                    list.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
                    break;
            }
            list.Add(new KeyValuePair<string, string>("client_id", _authSettings.AppId));
            list.Add(new KeyValuePair<string, string>("client_secret", _authSettings.AppSecret));
            list.Add(new KeyValuePair<string, string>("redirect_uri", _authSettings.RedirectUri));        

            return list;
        }
        private async Task<MicrosoftGraphAppAccessToken> MakeTokenPost(IList<KeyValuePair<string, string>> list)
        {
            string accessTokenResponse = string.Empty;

            using (var httpClient = new HttpClient())
            {
                using (var content = new FormUrlEncodedContent(list))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    var TokenResponse = await httpClient.PostAsync($"https://login.microsoftonline.com/common/oauth2/v2.0/token", content);
                    accessTokenResponse = TokenResponse.Content.ReadAsStringAsync().Result;
                }
            }

            return JsonConvert.DeserializeObject<MicrosoftGraphAppAccessToken>(accessTokenResponse);
        }
    }
}
