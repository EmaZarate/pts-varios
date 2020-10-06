using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.ParametrizationCriteria.GetAllParametrizationCriteria
{
    public class GetAllParametrizationCriteriaUseCase : IGetAllParametrizationCriteriaUseCase
    {
        private readonly IParametrizationCriteriaRepository _parametrizationCriteriaRepository;

        public GetAllParametrizationCriteriaUseCase(IParametrizationCriteriaRepository parametrizationCriteriaRepository)
        {
            _parametrizationCriteriaRepository = parametrizationCriteriaRepository;
        }

        public List<ParametrizationCriteriaOutput> Execute()
        {
            var listOutput = new List<ParametrizationCriteriaOutput>();
            var list = _parametrizationCriteriaRepository.GetAll();
            foreach (var pc in list)
            {
                listOutput.Add(new ParametrizationCriteriaOutput(pc.Id, pc.Name, pc.DataType));
            }

            return listOutput;
        }
    }
}
