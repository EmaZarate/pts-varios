using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.AspectStates;

namespace Hoshin.Quality.Application.UseCases.AspectStates.UpdateAspectStatus
{
    public class UpdateAspectStatusUseCase : IUpdateAspectStatusUseCase
    {
        private readonly IAspectStatesRepository _aspectStatesRepository;
        private readonly IMapper _mapper;
        public UpdateAspectStatusUseCase(IMapper mapper, IAspectStatesRepository aspectStatesRepository)
        {
            _mapper = mapper;
            _aspectStatesRepository = aspectStatesRepository;
        }
        public AspectStatesOutput Execute(int id, string name, string colour, bool active)
        {

            var aspectStatusDomain = _aspectStatesRepository.GetOne(id);
            if (aspectStatusDomain != null)
            {
                if (_aspectStatesRepository.CheckExistsAspectState(name, colour, id) == null)
                {
                    var aspectStatus = new Domain.AspectStates.AspectStates(id, name, colour, active);
                    aspectStatusDomain = _aspectStatesRepository.Update(aspectStatus);
                    return _mapper.Map<Domain.AspectStates.AspectStates, AspectStatesOutput>(aspectStatusDomain);
                }
                throw new DuplicateEntityException(name, "Ya existe un estado de aspecto con este nombre o color");

            }
            throw new EntityNotFoundException(id, "No se encontró un estado de aspecto con ese ID");
            
        }
    }
}
