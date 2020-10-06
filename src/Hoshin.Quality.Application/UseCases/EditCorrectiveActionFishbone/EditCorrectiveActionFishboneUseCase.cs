using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.CorrectiveActionFishbone;

namespace Hoshin.Quality.Application.UseCases.EditCorrectiveActionFishbone
{
    public class EditCorrectiveActionFishboneUseCase : IEditCorrectiveActionFishboneUseCase
    {
        private readonly ICorrectiveActionFishboneRepository _correctiveActionFishboneRepository;

        public EditCorrectiveActionFishboneUseCase(ICorrectiveActionFishboneRepository correctiveActionFishboneRepository)
        {
            _correctiveActionFishboneRepository = correctiveActionFishboneRepository;
        }
        public async Task Execute(List<CorrectiveActionFishbone> correctiveActionFishbone, string rootReason, int correctiveActionId)
        {
            _correctiveActionFishboneRepository.EditRootReason(correctiveActionId, rootReason);

            var quantityDeleted = _correctiveActionFishboneRepository.DeleteAllByCorrectionAction(correctiveActionId);

            foreach (var ac in correctiveActionFishbone)
            {
                ac.CorrectiveActionID = correctiveActionId;
            }

            //if (quantityDeleted == 0)
           // {
                //First edit of the fishbone
                await _correctiveActionFishboneRepository.AddRange(correctiveActionFishbone);
           // }
            //else
            //{
            //    _correctiveActionFishboneRepository.UpdateRange(correctiveActionFishbone);
            //}
            await _correctiveActionFishboneRepository.SaveChanges();
        }
    }
}
