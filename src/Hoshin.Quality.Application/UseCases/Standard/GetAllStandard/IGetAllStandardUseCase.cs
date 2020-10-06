using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Standard.GetAllStandard
{
    public interface IGetAllStandardUseCase
    {
        List<StandardOutput> Execute();
    }
}
