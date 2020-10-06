using System;
using System.Collections.Generic;
using System.Text;
using Hoshin.Quality.Application.Repositories;

namespace Hoshin.Quality.Application.UseCases.FishBone.GetAll
{
   public class GetAllFishBoneUseCase: IGetAllFishBoneUseCase
    {
        private readonly IFishBoneRepository _FishBoneRepository;
        public GetAllFishBoneUseCase(IFishBoneRepository FishBoneRepository)
        {
            _FishBoneRepository = FishBoneRepository;
        }

        public List<FishBoneOutput> Execute()
        {
            var listOutput = new List<FishBoneOutput>();
            var list = _FishBoneRepository.GetAll();
            foreach (var fb in list)
            {
                listOutput.Add(new FishBoneOutput(fb.FishboneID, fb.Name, fb.Color, fb.Active));
            }
            return listOutput;
        }

    }
}
