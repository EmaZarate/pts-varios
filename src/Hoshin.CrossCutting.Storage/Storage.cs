using Hanssens.Net;
using Hoshin.CrossCutting.MicrosoftGraph.DTO.User;
using Hoshin.CrossCutting.MicrosoftGraph.Services.Implementations;
using Hoshin.CrossCutting.MicrosoftGraph.Services.Interfaces;
using Hoshin.CrossCutting.Storage.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Hoshin.CrossCutting.Storage
{
    public class Storage : IStorage
    {
        private readonly IUserService _userService;
        private static Storage _instance;
        private LocalStorage _storage;
        public Storage(IUserService userService)
        {
            _userService = userService;
            var config = new LocalStorageConfiguration()
            {
                //set custom configuration
            };
            _storage = new LocalStorage();
            //if have custom configuration
            //_storage = new LocalStorage(config);         
        }
        //public static Storage Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //        {
        //            _instance = new Storage();
        //        }
        //        return _instance;
        //    }
        //}

        public void StoreToken(string id_identity, MicrosoftGraphAppAccessToken token)
        {
            var tokenStorage = new TokenStorageModel()
            {
                ID = id_identity,
                AccessToken = token.AccessToken,
                RefreshToken = token.RefreshToken,
                ExpiresIn = token.ExpiresIn,
                TimeAcquired = token.TimeAcquired
            };
            _storage.Store<TokenStorageModel>("Token", tokenStorage);
        }

        public async Task<TokenStorageModel> GetToken()
        {
            var actualToken = _storage.Get<TokenStorageModel>("Token");
            var timeElapsed = DateTime.Now.AddSeconds((-1 * actualToken.ExpiresIn));
            TimeSpan difference = timeElapsed - actualToken.TimeAcquired;
            //if actual token have more than 2 minutes of live, return this
            if (!(difference.Minutes > 2))
            {
                return actualToken;
            }
            else
            {
                var appAccessRefreshedToken = await _userService.RefreshAccessToken(actualToken.RefreshToken);
                this.ClearToken();
                this.StoreToken(actualToken.ID, appAccessRefreshedToken);

                return _storage.Get<TokenStorageModel>("Token");
            }
        }
        public void ClearToken()
        {
            _storage.Clear();
        }

    }
}
