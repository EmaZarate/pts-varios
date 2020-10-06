using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.ParametrizationCorrectiveAction.CreateParametrizationCorrectiveAction
{
    public interface ICreateParametrizationCorrectiveActionUseCase
    {
        ParametrizationCorrectiveActionOutput Execute(string name, string code, int value);
    }
}
