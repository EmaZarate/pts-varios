using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.ParametrizationCriteria.GetOneParametrizationCriteria
{
    public interface IGetOneParametrizationCriteriaUseCase
    {
        ParametrizationCriteriaOutput Execute(int id);
    }
}
