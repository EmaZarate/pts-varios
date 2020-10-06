using AutoMapper;
using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.CorrectiveActionState;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CorrectiveActionStates.CreateCorrectiveActionState
{
    public class CreateCorrectiveActionStateUseCase : ICreateCorrectiveActionStateUseCase
    {
        private readonly ICorrectiveActionStateRepository _correctiveActionStateRepository;
        private readonly IMapper _mapper;

        public CreateCorrectiveActionStateUseCase(ICorrectiveActionStateRepository correctiveActionStateRepository, IMapper mapper)
        {
            _correctiveActionStateRepository = correctiveActionStateRepository;
            _mapper = mapper;
        }

        public CorrectiveActionStateOutput Execute(CorrectiveActionState newCorrectiveActionState)
        {
            if(_correctiveActionStateRepository.CheckDuplicates(newCorrectiveActionState) == null)
            {
                var correctiveActionState = _correctiveActionStateRepository.Add(newCorrectiveActionState);

                return _mapper.Map<CorrectiveActionState, CorrectiveActionStateOutput>(correctiveActionState);
            }

            throw new DuplicateEntityException(newCorrectiveActionState.Name, "Ya existe un estado de acción correctiva con este nombre, código o color");
        }
    }
}
