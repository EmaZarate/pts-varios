using AutoMapper;
using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.CorrectiveActionState;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CorrectiveActionStates.GetOneCorrectiveActionState
{
    public class GetOneCorrectiveActionStateUseCase : IGetOneCorrectiveActionStateUseCase
    {
        private readonly ICorrectiveActionStateRepository _correctiveActionStateRepository;
        private readonly IMapper _mapper;

        public GetOneCorrectiveActionStateUseCase(ICorrectiveActionStateRepository correctiveActionStateRepository, IMapper mapper)
        {
            _correctiveActionStateRepository = correctiveActionStateRepository;
            _mapper = mapper;
        }

        public CorrectiveActionStateOutput Execute(int id)
        {
            var correctiveActionState = _correctiveActionStateRepository.Get(id);

            if(correctiveActionState != null)
            {
                return _mapper.Map<CorrectiveActionState, CorrectiveActionStateOutput>(correctiveActionState);
            }

            throw new EntityNotFoundException(id, "No se encontró un estado de acción correctiva con ese ID");
        }
    }
}
