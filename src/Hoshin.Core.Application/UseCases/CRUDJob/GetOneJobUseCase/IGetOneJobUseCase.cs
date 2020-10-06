using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.CRUDJob.GetOneJobUseCase
{
    public interface IGetOneJobUseCase
    {
        JobOutput Execute(int id);
    }
}
