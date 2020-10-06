using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.AuditStandardAspect.GetAllAuditStandardAspect
{
    public interface IGetAllAuditStandardAspectUseCase
    {
        List<AuditStandardAspectOutput> Execute(int id);
    }
}
