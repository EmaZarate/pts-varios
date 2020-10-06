using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Standard.UpdateStandard
{
    public interface IUpdateStandardUseCase
    {
        string Execute(Domain.Standard.Standard standard);
    }
}
