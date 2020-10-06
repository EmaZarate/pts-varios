using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.EditCorrectiveActionWorkgroup
{
    public class EditCorrectiveActionWorkgroupUseCase : IEditCorrectiveActionWorkgroupUseCase
    {
        private readonly IUserCorrectiveActionsRepository _userCorrectiveActionsRepository;

        public EditCorrectiveActionWorkgroupUseCase(IUserCorrectiveActionsRepository userCorrectiveActionsRepository)
        {
            _userCorrectiveActionsRepository = userCorrectiveActionsRepository;

        }

        public async Task Execute(int correctiveActionID, List<string> usersID)
        {
            _userCorrectiveActionsRepository.RemoveAll(correctiveActionID);
            await _userCorrectiveActionsRepository.AddRange(correctiveActionID, usersID);
            await _userCorrectiveActionsRepository.SaveChanges();
        }
    }
}
