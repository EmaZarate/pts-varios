using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Aspect.GetOneAspectUseCase
{
    public class GetOneAspectUseCase : IGetOneAspectUseCase
    {
        private readonly IAspectRepository _aspectRepository;

        public GetOneAspectUseCase(IAspectRepository aspectRepository)
        {
            _aspectRepository = aspectRepository;
        }
        public Domain.Aspect.Aspect Execute(int standardId, int aspectId)
        {
            var aspect = _aspectRepository.GetOne(standardId, aspectId);
            if(aspect == null)
            {
                throw new EntityNotFoundException(aspectId, "No se encontro un aspecto con ese id");
            }

            return aspect;
        }
    }
}
