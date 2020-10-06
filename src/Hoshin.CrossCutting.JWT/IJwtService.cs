using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.CrossCutting.JWT
{
    public interface IJwtService
    {
        Task<string> GenerateJWT(string user, string id, int plantId, int sectorId, int jobId);
    }
}
