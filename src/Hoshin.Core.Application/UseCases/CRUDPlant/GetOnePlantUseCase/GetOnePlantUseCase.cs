using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Domain.Plant;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.CRUDPlant.GetOnePlantUseCase
{
    public class GetOnePlantUseCase : IGetOnePlantUseCase
    {
        private readonly IPlantRepository _plantRepository;
        private readonly IMapper _mapper;

        public GetOnePlantUseCase(IPlantRepository plantRepository, IMapper mapper)
        {
            _plantRepository = plantRepository;
            _mapper = mapper;
        }
        public PlantOutput Execute(int id)
        {
            return _mapper.Map<Plant, PlantOutput>(_plantRepository.GetOne(id));
        }
    }
}
