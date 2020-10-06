using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Storage.Models
{
    public class TokenStorageModel
    {
        public string ID { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
        public DateTime TimeAcquired { get; set; }
    }
}
