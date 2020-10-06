using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Standard.CreateStandard
{
    public interface ICreateStandardUseCase
    {
        string Execute(Domain.Standard.Standard standard);
    }
}
