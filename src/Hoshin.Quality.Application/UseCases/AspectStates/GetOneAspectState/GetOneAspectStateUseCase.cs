using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Hoshin.Quality.Domain.AspectStates;
using Hoshin.Quality.Application.Exceptions.Common;

namespace Hoshin.Quality.Application.UseCases.AspectStates.GetOneAspectState
{
    public class GetOneAspectStateUseCase: IGetOneAspectStateUseCase
    {
        private readonly IAspectStatesRepository _aspectStatesRepository;
        private readonly IMapper _mapper;

        public GetOneAspectStateUseCase(IMapper mapper, IAspectStatesRepository aspectStatesRepository)
        {
            _mapper = mapper;
            _aspectStatesRepository = aspectStatesRepository;
        }

        public AspectStatesOutput Execute(int id)
        {
            var aspectStateDomain = _aspectStatesRepository.GetOne(id);
            if (aspectStateDomain != null)
            {
                return _mapper.Map<Hoshin.Quality.Domain.AspectStates.AspectStates, AspectStatesOutput>(aspectStateDomain);
            }
            throw new EntityNotFoundException(id, "No se encontró un estado de aspecto con ese ID");
        }
    }
}
