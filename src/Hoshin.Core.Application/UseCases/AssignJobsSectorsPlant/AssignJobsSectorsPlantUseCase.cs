using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Domain.Plant;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.AssignJobsSectorsPlant
{
    public class AssignJobsSectorsPlantUseCase : IAssignJobsSectorsPlantUseCase
    {
        private readonly IPlantRepository _plantRepository;
        public AssignJobsSectorsPlantUseCase(IPlantRepository plantRepository)
        {
            _plantRepository = plantRepository;
        }

        public bool Execute(Plant plant)
        {
            try
            {
                _plantRepository.UpdateAssociations(plant);
                return true;
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                throw ex;
            }
        }
    }
}
