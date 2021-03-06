﻿using AutoMapper;
using Hoshin.Core.Application.Exceptions.Plant;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Domain.Plant;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.CRUDPlant.UpdatePlantUseCase
{
    public class UpdatePlantUseCase : IUpdatePlantUseCase
    {
        private readonly IPlantRepository _plantRepository;
        private readonly IMapper _mapper;
        public UpdatePlantUseCase(IPlantRepository plantRepository, IMapper mapper)
        {
            _plantRepository = plantRepository;
            _mapper = mapper;
        }
        public PlantOutput Execute(Plant plant)
        {
            var p = _plantRepository.CheckDuplicated(plant);
            if (p == null)
            {
                return _mapper.Map<Plant, PlantOutput>(_plantRepository.Update(plant));
            }
            else
            {
                throw new PlantWithThisNameAndCountryAlreadyExists(plant.Name, plant.Country, "Ya existe una planta con este Nombre en este País");
            }            
        }
    }
}
