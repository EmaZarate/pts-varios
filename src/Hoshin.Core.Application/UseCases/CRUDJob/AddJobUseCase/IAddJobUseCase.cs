using Hoshin.Core.Domain.Job;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.CRUDJob.AddJobUseCase
{
    public interface IAddJobUseCase
    {
        JobOutput Execute(Job job);
    }
}
