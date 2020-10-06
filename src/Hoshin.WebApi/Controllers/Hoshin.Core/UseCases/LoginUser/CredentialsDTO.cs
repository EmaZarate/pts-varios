using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.LoginUser
{
    public class CredentialsDTO
    {        
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsLocked { get; set; }
        public string AccessToken { get; set; }
    }
}
