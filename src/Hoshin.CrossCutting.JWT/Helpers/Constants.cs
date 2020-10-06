using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.JWT.Helpers
{
    public class Constants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id", Sector="sector",Plant="plant", Job="job";
            }

            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
            }
        }
    }
}
