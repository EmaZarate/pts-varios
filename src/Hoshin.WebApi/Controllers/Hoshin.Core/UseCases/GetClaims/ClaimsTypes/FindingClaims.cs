using Hoshin.CrossCutting.Authorization.Claims.Quality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.GetClaims.ClaimsTypes
{
    public class FindingClaims
    {
        public string Add => Findings.Add;
        public string UpdateApproved => Findings.UpdateApproved;
        public string Approve => Findings.Approve;
    }
}
