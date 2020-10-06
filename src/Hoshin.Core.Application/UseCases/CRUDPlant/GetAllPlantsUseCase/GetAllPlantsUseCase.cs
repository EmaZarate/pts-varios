using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Domain.Plant;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.CRUDPlant.GetAllPlantsUseCase
{
    public class GetAllPlantsUseCase : IGetAllPlantsUseCase
    {
        private readonly IPlantRepository _plantRepository;
        private readonly IMapper _mapper;
        public GetAllPlantsUseCase(IPlantRepository plantRepository, IMapper mapper)
        {
            _plantRepository = plantRepository;
            _mapper = mapper;
        }
        public async Task<List<PlantOutput>> Execute()
        {
            var list = await _plantRepository.GetAll();
            return _mapper.Map<List<Plant>, List<PlantOutput>>(list);   
        }
    }
}
