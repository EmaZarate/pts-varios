using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.ParametrizationCorrectiveAction.UpdateParametrizationCorrectiveAction
{
    public interface IUpdateParametrizationCorrectiveActionUseCase
    {
        ParametrizationCorrectiveActionOutput Execute(int id, string name, string code, int value);
    }
}
