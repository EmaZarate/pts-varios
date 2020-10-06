using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.ParametrizationCriteria.GetOneParametrizationCriteria
{
    public class GetOneParametrizationCriteriaUseCase : IGetOneParametrizationCriteriaUseCase
    {
        private readonly IParametrizationCriteriaRepository _parametrizationCriteriaRepository;

        public GetOneParametrizationCriteriaUseCase(IParametrizationCriteriaRepository parametrizationCriteriaRepository)
        {
            _parametrizationCriteriaRepository = parametrizationCriteriaRepository;
        }
        public ParametrizationCriteriaOutput Execute(int id)
        {
            var paramC = _parametrizationCriteriaRepository.Get(id);
            if(paramC != null)
            {
                return new ParametrizationCriteriaOutput(paramC.Id, paramC.Name, paramC.DataType);
            }

            throw new EntityNotFoundException(id, "No se encontró un criterio de parametrización con ese ID");
        }
    }
}
