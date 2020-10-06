using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.AddFindingToAspect
{
    public interface IAddFindingToAspectUseCase
    {
        bool Execute(Domain.Finding.Finding finding);
    }
}
