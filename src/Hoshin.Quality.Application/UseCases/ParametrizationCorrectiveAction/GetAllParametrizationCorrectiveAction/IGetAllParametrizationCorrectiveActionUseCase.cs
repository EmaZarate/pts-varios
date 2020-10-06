using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.ParametrizationCorrectiveAction.GetAllParametrizationCorrectiveAction
{
    public interface IGetAllParametrizationCorrectiveActionUseCase
    {
        List<ParametrizationCorrectiveActionOutput> Execute();
    }
}
