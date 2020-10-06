using Hoshin.Core.Domain.Plant;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.CRUDPlant.UpdatePlantUseCase
{
    public interface IUpdatePlantUseCase
    {
        PlantOutput Execute(Plant plant);
    }
}