using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.ParametrizationCriteria.UpdateParametrizationCriteria
{
    public interface IUpdateParametrizationCriteriaUseCase
    {
        ParametrizationCriteriaOutput Execute(int id, string name, string datatype);
    }
}
