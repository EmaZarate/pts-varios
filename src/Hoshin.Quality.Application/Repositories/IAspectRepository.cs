using Hoshin.Quality.Domain.Aspect;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.Repositories
{
    public interface IAspectRepository
    {
        Aspect GetOne(int standardId, int aspectId);
    }
}
