using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.ParametrizationCorrectiveAction.GetOneParametrizationCorrectiveAction
{
    public class GetOneParametrizationCorrectiveActionUseCase : IGetOneParametrizationCorrectiveActionUseCase
    {
        private readonly IParametrizationCorrectiveActionRepository _ParametrizationCorrectiveActionRepository;

        public GetOneParametrizationCorrectiveActionUseCase(IParametrizationCorrectiveActionRepository ParametrizationCorrectiveActionRepository)
        {
            _ParametrizationCorrectiveActionRepository = ParametrizationCorrectiveActionRepository;
        }
        public ParametrizationCorrectiveActionOutput Execute(int id)
        {
            var paramC = _ParametrizationCorrectiveActionRepository.Get(id);
            if(paramC != null)
            {
                return new ParametrizationCorrectiveActionOutput(paramC.Id, paramC.Name, paramC.Code, paramC.Value);
            }

            throw new EntityNotFoundException(id, "No se encontró un criterio de parametrización con ese ID");
        }
    }
}
