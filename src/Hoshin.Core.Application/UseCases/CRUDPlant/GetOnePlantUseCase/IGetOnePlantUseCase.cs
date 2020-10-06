using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.CRUDPlant.GetOnePlantUseCase
{
    public interface IGetOnePlantUseCase
    {
        PlantOutput Execute(int id);
    }
}