using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.EditCorrectiveActionWorkgroup
{
    public interface IEditCorrectiveActionWorkgroupUseCase
    {
        Task Execute(int correctiveActionID, List<string> usersID);
    }
}
