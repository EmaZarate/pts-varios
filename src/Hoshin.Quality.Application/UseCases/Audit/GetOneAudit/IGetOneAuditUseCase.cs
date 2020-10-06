using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.Audit.GetOneAudit
{
    public interface IGetOneAuditUseCase
    {
        AuditOutput Execute(int id);
    }
}
