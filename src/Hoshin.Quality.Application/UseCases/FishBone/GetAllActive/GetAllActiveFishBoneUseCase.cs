using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FishBone.GetAllActive
{
    public class GetAllActiveFishBoneUseCase : IGetAllActiveFishBoneUseCase
    {
        private readonly IFishBoneRepository _FishBoneRepository;
        private readonly IMapper _mapper;
        public GetAllActiveFishBoneUseCase(IFishBoneRepository FishBoneRepository, IMapper mapper)
        {
            _FishBoneRepository = FishBoneRepository;
            _mapper = mapper;
        }
        public List<FishBoneOutput> Execute()
        {
            var listOutput = new List<FishBoneOutput>();
            var list = _FishBoneRepository.GetAllActive();
            foreach (var fb in list)
            {
                listOutput.Add(new FishBoneOutput(fb.FishboneID, fb.Name, fb.Color, fb.Active));
            }
            //return _mapper.Map<List<FishBoneOutput>>(list);
            return listOutput;
        }
    }
}
