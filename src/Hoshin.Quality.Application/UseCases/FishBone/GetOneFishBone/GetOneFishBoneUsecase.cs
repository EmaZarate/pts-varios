using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Hoshin.Quality.Application.Exceptions.Common;

namespace Hoshin.Quality.Application.UseCases.FishBone.GetOneFishBone
{
   public class GetOneFishBoneUseCase : IGetOneFishBoneUseCase
    {
        private IFishBoneRepository _fishBoneRepository;

        public GetOneFishBoneUseCase(IFishBoneRepository fishBoneRepository)
        {
            _fishBoneRepository = fishBoneRepository;

        }

        public FishBoneOutput Execute(int id) {
            var paramC = _fishBoneRepository.Get(id);
            if (paramC != null)
            {
                return new FishBoneOutput(paramC.FishboneID, paramC.Name, paramC.Color, paramC.Active);
            }
            throw new EntityNotFoundException(id, "No se encontro una espina de pescado con ese ID");
        }
    }
}
