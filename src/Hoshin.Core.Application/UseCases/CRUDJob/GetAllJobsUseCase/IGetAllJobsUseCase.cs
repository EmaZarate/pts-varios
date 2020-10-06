using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.CRUDJob.GetAllJobsUseCase
{
    public interface IGetAllJobsUseCase
    {
        Task<List<JobOutput>> Execute();
    }
}
