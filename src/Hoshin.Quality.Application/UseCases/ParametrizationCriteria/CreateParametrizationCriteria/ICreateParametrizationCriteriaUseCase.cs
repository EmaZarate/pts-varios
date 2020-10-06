using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.ParametrizationCriteria.CreateParametrizationCriteria
{
    public interface ICreateParametrizationCriteriaUseCase
    {
        ParametrizationCriteriaOutput Execute(string name, string datatype);
    }
}
