using Hoshin.Core.Domain.Plant;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.AssignJobsSectorsPlant
{
    public interface IAssignJobsSectorsPlantUseCase
    {
        bool Execute(Plant plant);
    }
}
