using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.Exceptions.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FindingsStates.UpdateFindingsStates
{
    public class UpdateFindingsStatesUseCase : IUpdateFindingsStatesUseCase
    {
        private readonly IFindingStateRepository _findingsStatesRepository;

        public UpdateFindingsStatesUseCase(IFindingStateRepository findingsStatesRepository)
        {
            _findingsStatesRepository = findingsStatesRepository;
        }

        public FindingsStatesOutput Execute(int id, string code, string name, string colour, bool active)
        {

            var findinS = _findingsStatesRepository.Get(id);
            if (findinS != null)
            {
                if (_findingsStatesRepository.CheckExistsFindingState(code, name, colour, id) == null)
                {
                    findinS.Name = name;
                    findinS.Colour = colour;
                    findinS.Code = code;
                    findinS.Active = active;
                    var res = _findingsStatesRepository.Update(findinS);

                    return new FindingsStatesOutput(findinS.Id, findinS.Code, findinS.Name, findinS.Colour, findinS.Active);
                }
                else
                {
                    throw new DuplicateEntityException(name, "Ya existe un estado de hallazgo con este nombre, codigo o color");
                }
            }
            throw new EntityNotFoundException(id, "No se encontró un estado de hallazgo con ese ID");
        }
    }
}
