using AutoMapper;
using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.CorrectiveActionState;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CorrectiveActionStates.UpdateCorrectiveActionState
{
    public class UpdateCorrectiveActionStateUseCase : IUpdateCorrectiveActionStateUseCase
    {
        private readonly ICorrectiveActionStateRepository _correctiveActionStateRepository;
        private readonly IMapper _mapper;

        public UpdateCorrectiveActionStateUseCase(ICorrectiveActionStateRepository correctiveActionStateRepository, IMapper mapper)
        {
            _correctiveActionStateRepository = correctiveActionStateRepository;
            _mapper = mapper;
        }

        public CorrectiveActionStateOutput Execute(CorrectiveActionState updateCorrectiveActionState)
        {
            if(_correctiveActionStateRepository.CheckDuplicates(updateCorrectiveActionState) == null)
            {
                var correctiveActionState = _correctiveActionStateRepository.Update(updateCorrectiveActionState);

                return _mapper.Map<CorrectiveActionState, CorrectiveActionStateOutput>(correctiveActionState);
            }

            throw new DuplicateEntityException(updateCorrectiveActionState.Name, "Ya existe un estado de acción correctiva con este nombre, código o color");
        }
    }
}
