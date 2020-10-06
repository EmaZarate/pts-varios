using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.CRUDPlant.GetAllPlantsUseCase
{
    public interface IGetAllPlantsUseCase
    {
        Task<List<PlantOutput>> Execute();
    }
}
