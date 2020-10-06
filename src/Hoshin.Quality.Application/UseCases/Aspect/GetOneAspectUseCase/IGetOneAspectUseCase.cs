using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Aspect.GetOneAspectUseCase
{
    public interface IGetOneAspectUseCase
    {
        Domain.Aspect.Aspect Execute(int standardId, int aspectId);
    }
}
