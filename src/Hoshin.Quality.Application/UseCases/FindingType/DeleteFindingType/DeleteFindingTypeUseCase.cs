using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FindingType.DeleteFindingType
{
    public class DeleteFindingTypeUseCase : IDeleteFindingTypeUseCase
    {
        private readonly IFindingTypeRepository _findingTypeRepository;

        public DeleteFindingTypeUseCase(IFindingTypeRepository findingTypeRepository)
        {
            _findingTypeRepository = findingTypeRepository;
        }
        public bool Execute(int id)
        {
            return _findingTypeRepository.Delete(id);
        }
    }
}
