using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.ParametrizationCorrectiveAction.GetOneParametrizationCorrectiveAction
{
    public interface IGetOneParametrizationCorrectiveActionUseCase
    {
        ParametrizationCorrectiveActionOutput Execute(int id);
    }
}
