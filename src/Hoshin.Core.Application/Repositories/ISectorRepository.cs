using Hoshin.Core.Domain.Sector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.Repositories
{
    public interface ISectorRepository
    {
        Task<List<Sector>> GetAll();
        Sector GetOne(int id);
        Sector Add(Sector sector);
        Sector Update(Sector sector);
        Sector CheckDuplicated(Sector sector);
    }
}
