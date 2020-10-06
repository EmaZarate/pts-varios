using Hoshin.Quality.Domain.CorrectiveActionState;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.Repositories
{
    public interface ICorrectiveActionStateRepository
    {
        List<CorrectiveActionState> GetAll();

        CorrectiveActionState Get(int id);

        CorrectiveActionState Get(string code, string name, string color);

        CorrectiveActionState Add(CorrectiveActionState newCorrectiveActionState);

        CorrectiveActionState Update(CorrectiveActionState updateCorrectiveActionState);

        CorrectiveActionState CheckDuplicates(CorrectiveActionState checkCorrectiveActionState);
        int GetByCode(string code);
    }
}
