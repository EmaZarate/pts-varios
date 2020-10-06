using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.Repositories
{
    public interface ITasksEvidenceRepository
    {
        bool Update(int taskID, List<string> addUrls, List<string> deletUrls);
    }
}
