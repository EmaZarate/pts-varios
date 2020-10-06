using Hoshin.Quality.Domain.FindingsState;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.Repositories
{
    public interface IFindingStateRepository
    {
        List<FindingsState> GetAll();

        FindingsState Get(int id);

        FindingsState Get(string code, string name, string color);

        FindingsState Add(FindingsState newparam);

        FindingsState Update(FindingsState updateparam);
        FindingsState CheckExistsFindingState(string code, string name, string colour, int id);
    }
}
