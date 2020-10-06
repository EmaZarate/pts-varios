using Hoshin.Quality.Domain.ParametrizationCorrectiveAction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.Repositories
{
    public interface IParametrizationCorrectiveActionRepository
    {
        ParametrizationCorrectiveAction Get(string name, string code);
        ParametrizationCorrectiveAction GetByName(string name);
        ParametrizationCorrectiveAction Get(int id);
        ParametrizationCorrectiveAction Add(ParametrizationCorrectiveAction newparam);
        List<ParametrizationCorrectiveAction> GetAll();
        ParametrizationCorrectiveAction Update(ParametrizationCorrectiveAction updateparam);
    }
}
