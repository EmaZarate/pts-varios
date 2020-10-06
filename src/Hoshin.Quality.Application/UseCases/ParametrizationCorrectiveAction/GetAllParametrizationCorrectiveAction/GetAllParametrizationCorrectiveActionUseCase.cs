using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.ParametrizationCorrectiveAction.GetAllParametrizationCorrectiveAction;
using Hoshin.Quality.Application.UseCases.ParametrizationCorrectiveAction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.ParametrizationCorrectiveAction.GetAllParametrizationCorrectiveAction
{
    public class GetAllParametrizationCorrectiveActionUseCase : IGetAllParametrizationCorrectiveActionUseCase
    {
        private readonly IParametrizationCorrectiveActionRepository _parametrizationCorrectiveActionRepository;

        public GetAllParametrizationCorrectiveActionUseCase(IParametrizationCorrectiveActionRepository parametrizationCorrectiveActionRepository)
        {
            _parametrizationCorrectiveActionRepository = parametrizationCorrectiveActionRepository;
        }

        public List<ParametrizationCorrectiveActionOutput> Execute()
        {
            var listOutput = new List<ParametrizationCorrectiveActionOutput>();
            var list = _parametrizationCorrectiveActionRepository.GetAll();
            foreach (var pc in list)
            {
                listOutput.Add(new ParametrizationCorrectiveActionOutput(pc.Id, pc.Name, pc.Code, pc.Value));
            }

            return listOutput;
        }
    }
}
