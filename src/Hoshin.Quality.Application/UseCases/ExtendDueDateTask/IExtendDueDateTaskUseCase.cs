using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.ExtendDueDateTask
{
    public interface IExtendDueDateTaskUseCase
    {
        bool Execute(Domain.Task.Task correctiveAction);
    }
}
