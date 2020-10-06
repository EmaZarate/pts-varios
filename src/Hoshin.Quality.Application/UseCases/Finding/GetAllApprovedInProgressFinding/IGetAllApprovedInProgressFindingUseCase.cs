using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Finding.GetAllApprovedInProgressFinding
{
    public interface IGetAllApprovedInProgressFindingUseCase
    {
        List<FindingOutput> Execute();
    }
}
