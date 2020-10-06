using System;
using System.Collections.Generic;
using System.Text;
using Hoshin.Quality.Domain.AspectStates;

namespace Hoshin.Quality.Application.Repositories
{
    public interface IAspectStatesRepository
    {
        List<AspectStates> GetAll();

        AspectStates Update(AspectStates aspectStates);

        AspectStates Add(AspectStates aspectStates);

        AspectStates GetOne(int id);

        AspectStates Get(string name, string colour);

        AspectStates CheckExistsAspectState(string name, string colour, int id);

        Int32 GetAspectStateID(string name);
    }
}
