using Hoshin.Quality.Domain.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.Repositories
{
    public interface IStandardRepository
    {
        List<Standard> GetAll();
        Standard Get(int id);
        string Add(Standard newAuditState);
        string Update(Standard updateAuditState);
    }
}
