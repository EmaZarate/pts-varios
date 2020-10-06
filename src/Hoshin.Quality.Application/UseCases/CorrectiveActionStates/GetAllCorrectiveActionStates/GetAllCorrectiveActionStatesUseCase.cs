using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.CorrectiveActionState;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CorrectiveActionStates.GetAllCorrectiveActionStates
{
    public class GetAllCorrectiveActionStatesUseCase : IGetAllCorrectiveActionStatesUseCase
    {
        private readonly ICorrectiveActionStateRepository _correctiveActionStateRepository;
        private readonly IMapper _mapper;

        public GetAllCorrectiveActionStatesUseCase(ICorrectiveActionStateRepository correctiveActionStateRepository, IMapper mapper)
        {
            _correctiveActionStateRepository = correctiveActionStateRepository;
            _mapper = mapper;
        }

        public List<CorrectiveActionStateOutput> Execute()
        {
            List<CorrectiveActionState> correctiveActionStates = _correctiveActionStateRepository.GetAll();

            return _mapper.Map<List<CorrectiveActionState>, List<CorrectiveActionStateOutput>>(correctiveActionStates);
        }
    }
}
