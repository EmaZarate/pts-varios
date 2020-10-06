using Hoshin.Quality.Domain.CorrectiveActionFishbone;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.EditCorrectiveActionFishbone
{
    public interface IEditCorrectiveActionFishboneUseCase
    {
        Task Execute(List<CorrectiveActionFishbone> correctiveActionFishbone, string rootReason, int correctiveActionId);
    }
}
