using Hoshin.CrossCutting.Authorization.Claims.Quality;
using Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.GetClaims.ClaimsTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.GetClaims
{
    public class ClaimsDTO
    {

        public FindingClaims findings
        {
            get { return new FindingClaims(); }
            set { }
        }

    }
}
