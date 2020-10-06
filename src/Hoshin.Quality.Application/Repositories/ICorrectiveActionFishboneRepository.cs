using Hoshin.Quality.Domain.CorrectiveAction;
using Hoshin.Quality.Domain.CorrectiveActionFishbone;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.Repositories
{
    public interface ICorrectiveActionFishboneRepository
    {
        int DeleteAllByCorrectionAction(int correctiveActionId);
        Task AddRange(List<CorrectiveActionFishbone> correctiveActionFishbones);
        void UpdateRange(List<CorrectiveActionFishbone> correctiveActionFishbones);
        void EditRootReason(int correctiveActionId, string rootReason);
        Task SaveChanges();
    }
}
