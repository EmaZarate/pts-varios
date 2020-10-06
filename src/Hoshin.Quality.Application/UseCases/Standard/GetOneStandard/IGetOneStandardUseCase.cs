using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Standard.GetOneStandard
{
    public interface IGetOneStandardUseCase
    {
        StandardOutput Execute(int id);
    }
}
