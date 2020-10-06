using Hoshin.Core.Domain.Job;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.Repositories
{
    public interface IJobRepository
    {
        Task<List<Job>> GetAll();
        Job GetOne(int id);
        Job Add(Job job);
        Job Update(Job job);
        Job CheckDuplicated(Job job);
    }
}
