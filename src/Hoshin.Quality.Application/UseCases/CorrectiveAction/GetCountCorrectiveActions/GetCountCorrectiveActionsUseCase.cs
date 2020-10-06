using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CorrectiveAction.GetCountCorrectiveActions
{
    public class GetCountCorrectiveActionsUseCase : IGetCountCorrectiveActionsUseCase
    {
        private readonly ICorrectiveActionRepository _correctiveActionRepository;
        public GetCountCorrectiveActionsUseCase(ICorrectiveActionRepository correctiveActionRepository)
        {
            _correctiveActionRepository = correctiveActionRepository;
        }
        public int Execute()
        {
            return _correctiveActionRepository.GetCount();
        }
    }
}
