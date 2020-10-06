using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.Standard;

namespace Hoshin.Quality.Application.UseCases.Standard.UpdateStandard
{
    public class UpdateStandardUseCase : IUpdateStandardUseCase
    {
        private readonly IStandardRepository _standardRepository;

        public UpdateStandardUseCase(IStandardRepository standardRepository)
        {
            _standardRepository = standardRepository;
        }

        public string Execute(Domain.Standard.Standard standard)
        {
            var existStandard = _standardRepository.Get(standard.StandardID);

            if (standard.Active != existStandard.Active)
            {
                standard.Aspects = existStandard.Aspects;
            }

            string response = _standardRepository.Update(standard);

            switch (response)
            {
                case "NameExist":
                    throw new DuplicateEntityException(standard.Name, "Ya existe una norma con este nombre o código", 436);
                case "OneActive":
                    throw new DuplicateEntityException(standard.Name, "Debe existir al menos un aspecto activo.", 436);
                default:
                    return "Ok";
            }
        }
    }
}
