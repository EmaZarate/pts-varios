using Hoshin.CrossCutting.JWT.Factory;
using Hoshin.CrossCutting.JWT.Helpers;
using Hoshin.CrossCutting.JWT.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.CrossCutting.JWT
{
    public class JwtService : IJwtService
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        public JwtService(IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }
        public async Task<string> GenerateJWT(string user, string id, int plantId, int sectorId, int jobId)
        {
            var identity = _jwtFactory.GenerateClaimsIdentity(user, id, plantId, sectorId, jobId);
            return await Tokens.GenerateJwt(identity, _jwtFactory, user, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
    }
}
