using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FindingsStates.CreateFindingsStateUseCase
{
    public class CreateFindingsStateUseCase : ICreateFindingsStateUseCase
    {
        private readonly IFindingStateRepository _findingsStatesRepository;

        public CreateFindingsStateUseCase(IFindingStateRepository FindingsStatesRepository)
        {
            _findingsStatesRepository = FindingsStatesRepository;
        }

        public FindingsStatesOutput Execute(string code, string name, string colour, bool active)
        {
            if (_findingsStatesRepository.Get(code, name, colour) == null)
            {
                var newParam = new Domain.FindingsState.FindingsState(code, name, colour, active);
                newParam = _findingsStatesRepository.Add(newParam);
                return new FindingsStatesOutput(newParam.Id, newParam.Code, newParam.Name, newParam.Colour, newParam.Active);
            }
            else
            {
                throw new DuplicateEntityException(name, "Ya existe un estado de hallazgo con este nombre, código o color");
            }
        }
    }
}
