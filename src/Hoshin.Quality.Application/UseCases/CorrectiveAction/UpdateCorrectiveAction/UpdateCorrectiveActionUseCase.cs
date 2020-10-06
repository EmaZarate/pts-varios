using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.CorrectiveAction;

namespace Hoshin.Quality.Application.UseCases.CorrectiveAction.UpdateCorrectiveAction
{
    public class UpdateCorrectiveActionUseCase : IUpdateCorrectiveActionUseCase
    {
        private readonly ICorrectiveActionRepository _correctiveActionRepository;
        private readonly IMapper _mapper;
        public UpdateCorrectiveActionUseCase(ICorrectiveActionRepository correctiveActionRepository, IMapper mapper)
        {
            _correctiveActionRepository = correctiveActionRepository;
            _mapper = mapper;
        }
        public CorrectiveActionOutput Execute(Domain.CorrectiveAction.CorrectiveAction correctiveAction)
        {
            return _mapper.Map< Domain.CorrectiveAction.CorrectiveAction ,CorrectiveActionOutput >(_correctiveActionRepository.Update(correctiveAction)); 
        }
    }
}
