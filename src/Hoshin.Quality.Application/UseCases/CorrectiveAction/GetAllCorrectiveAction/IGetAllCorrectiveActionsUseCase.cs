using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.CorrectiveAction.GetAllCorrectiveAction
{
    public interface IGetAllCorrectiveActionsUseCase
    {
        Task<List<CorrectiveActionOutput>> Execute();
    }
}
