using Hoshin.Quality.Domain.AuditType;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.Repositories
{
    public interface IAuditTypeRepository
    {
        AuditType Get(int id);

        Task<List<AuditType>> GetAll();

        Task<List<AuditType>> GetAllActives();

        AuditType Add(AuditType auditType);

        AuditType Update(AuditType auditType);

        AuditType CheckDuplicated(AuditType auditType);
    }
}
