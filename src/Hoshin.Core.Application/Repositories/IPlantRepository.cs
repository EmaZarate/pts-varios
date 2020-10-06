using Hoshin.Core.Domain.Plant;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.Repositories
{
    public interface IPlantRepository
    {
        Task<List<Plant>> GetAll();
        Plant GetOne(int id);
        Plant Add(Plant plant);
        Plant Update(Plant plant);
        Plant CheckDuplicated(Plant plant);
        void UpdateAssociations(Plant plant);
    }
}
