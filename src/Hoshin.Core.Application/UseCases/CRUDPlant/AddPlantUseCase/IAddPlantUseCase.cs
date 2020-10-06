using Hoshin.Core.Domain.Plant;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.CRUDPlant.AddPlantUseCase
{
    public interface IAddPlantUseCase
    {
        PlantOutput Execute(Plant plant);
    }
}