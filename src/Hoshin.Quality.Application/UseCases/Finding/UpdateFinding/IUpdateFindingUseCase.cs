using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Finding.UpdateFinding
{
    public interface IUpdateFindingUseCase
    {
        bool Execute(Domain.Finding.Finding finding);
    }
}
