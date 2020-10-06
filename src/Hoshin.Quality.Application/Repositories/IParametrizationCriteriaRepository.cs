using Hoshin.Quality.Domain.ParametrizationCriteria;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.Repositories
{
    public interface IParametrizationCriteriaRepository
    {
        ParametrizationCriteria Get(string name);
        ParametrizationCriteria Get(int id);
        ParametrizationCriteria Add(ParametrizationCriteria newparam);
        List<ParametrizationCriteria> GetAll();
        ParametrizationCriteria Update(ParametrizationCriteria updateparam);
    }
}
