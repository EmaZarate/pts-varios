using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FindingsStates.GetOneFindingsStates
{
    public class GetOneFindingsStatesUseCase : IGetOneFindingsStatesUseCase
    {
        private IFindingStateRepository _findingsStatesRepository;

        public GetOneFindingsStatesUseCase(IFindingStateRepository findingsStatesRepository)
        {
            _findingsStatesRepository = findingsStatesRepository;
        }

        public FindingsStatesOutput Execute(int id)
        {
            var paramC = _findingsStatesRepository.Get(id);
            if(paramC != null)
            {
                return new FindingsStatesOutput(paramC.Id, paramC.Code, paramC.Name, paramC.Colour, paramC.Active);
            }

            throw new EntityNotFoundException(id, "No se encontró un estado de hallazgo con ese ID");
        }
    }
}
