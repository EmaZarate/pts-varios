using Hoshin.Quality.Domain.AuditState;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.Repositories
{
    public interface IAuditStateRepository
    {
        List<AuditState> GetAll();
        AuditState Get(int id);
        AuditState Add(AuditState newAuditState);
        AuditState Update(AuditState updateAuditState);
    }
}
