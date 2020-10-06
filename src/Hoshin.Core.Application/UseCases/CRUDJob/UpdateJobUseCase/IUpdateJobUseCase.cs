using Hoshin.Core.Domain.Job;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.CRUDJob.UpdateJobUseCase
{
    public interface IUpdateJobUseCase
    {
        JobOutput Execute(Job job);
    }
}
