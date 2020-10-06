using AutoMapper;
using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.AspectStates.CreateAspectState
{
    public class CreateAspectStateUseCase : ICreateAspectStateUseCase
    {
        private readonly IAspectStatesRepository _aspectStatesRepository;
        private readonly IMapper _mapper;
        public CreateAspectStateUseCase(IMapper mapper, IAspectStatesRepository aspectStatesRepository)
        {
            _mapper = mapper;
            _aspectStatesRepository = aspectStatesRepository;
        }
        public AspectStatesOutput Execute(string name, string colour, bool active)
        {
            if(_aspectStatesRepository.Get(name, colour) == null)
            {
                var aspectStatus = new Hoshin.Quality.Domain.AspectStates.AspectStates(name, colour, active);
                aspectStatus = _aspectStatesRepository.Add(aspectStatus);
                return _mapper.Map<Domain.AspectStates.AspectStates, AspectStatesOutput>(aspectStatus);
            }
            throw new DuplicateEntityException(name, "Ya existe un estado de aspecto con este nombre, código o color");

        }
    }
}
