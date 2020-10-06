using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.Audit.GetAllAudit
{
    public interface IGetAllAuditUseCase
    {
        List<AuditOutput> Execute();
    }
}
