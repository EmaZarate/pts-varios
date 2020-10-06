using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FindingsStates.GetAllFindingsStates
{
    public class GetAllFindingsStatesUseCase : IGetAllFindingsStatesUseCase
    {
        private readonly IFindingStateRepository _findingStatesRepository;

        public GetAllFindingsStatesUseCase(IFindingStateRepository FindingsStatesRepository)
        {
            _findingStatesRepository = FindingsStatesRepository;
        }

        public List<FindingsStatesOutput> Execute()
        {
            var listOutput = new List<FindingsStatesOutput>();
            var list = _findingStatesRepository.GetAll();
            foreach(var fs in list)
            {
                listOutput.Add(new FindingsStatesOutput(fs.Id, fs.Code, fs.Name, fs.Colour, fs.Active));
            }

            return listOutput;
        }
    }
}
