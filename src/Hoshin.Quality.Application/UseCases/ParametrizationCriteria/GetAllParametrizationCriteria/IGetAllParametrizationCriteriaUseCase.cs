using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.ParametrizationCriteria.GetAllParametrizationCriteria
{
    public interface IGetAllParametrizationCriteriaUseCase
    {
        List<ParametrizationCriteriaOutput> Execute();
    }
}
