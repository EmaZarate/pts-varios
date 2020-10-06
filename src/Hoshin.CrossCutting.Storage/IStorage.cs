using Hoshin.CrossCutting.MicrosoftGraph.DTO.User;
using Hoshin.CrossCutting.Storage.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.CrossCutting.Storage
{
    public interface IStorage
    {
        void StoreToken(string id_identity, MicrosoftGraphAppAccessToken token);
        Task<TokenStorageModel> GetToken();
        void ClearToken();
    }
}
