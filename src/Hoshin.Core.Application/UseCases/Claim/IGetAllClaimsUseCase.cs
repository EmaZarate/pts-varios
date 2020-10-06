using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.Claim
{
    public interface IGetAllClaimsUseCase
    {
        List<KeyValuePair<string, List<KeyValuePair<string, List<KeyValuePair<string, string>>>>>> Execute();
    }
}
