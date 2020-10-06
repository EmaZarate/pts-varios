using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CorrectiveAction.GetOneCorrectiveAction
{
    public class GetOneCorrectiveActionUseCase : IGetOneCorrectiveActionUseCase
    {
        private readonly ICorrectiveActionRepository _correctiveActionRepository;
        private readonly IMapper _mapper;
        public GetOneCorrectiveActionUseCase(ICorrectiveActionRepository correctiveActionRepository, IMapper mapper)
        {
            _correctiveActionRepository = correctiveActionRepository;
            _mapper = mapper;
        }

        public CorrectiveActionOutput Execute(int id)
        {
            var correctiveAction = _correctiveActionRepository.GetOne(id);

            var a = _mapper.Map<Domain.CorrectiveAction.CorrectiveAction, CorrectiveActionOutput>(correctiveAction);
            return a;
        }
    }
}
