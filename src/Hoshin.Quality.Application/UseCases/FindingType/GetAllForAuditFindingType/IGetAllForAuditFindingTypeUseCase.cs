using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FindingType.GetAllForAuditFindingType
{
    public interface IGetAllForAuditFindingTypeUseCase
    {
        List<FindingTypeOutput> Execute();
    }
}
