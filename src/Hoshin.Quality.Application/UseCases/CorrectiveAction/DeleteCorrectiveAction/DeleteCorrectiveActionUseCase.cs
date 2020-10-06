using Hoshin.Core.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.CorrectiveAction.DeleteCorrectiveAction
{
    public class DeleteCorrectiveActionUseCase : IDeleteCorrectiveActionUseCase
    {
        private readonly ICorrectiveActionRepository _correctiveActionRepository;
        public DeleteCorrectiveActionUseCase(ICorrectiveActionRepository correctiveActionRepository)
        {
            this._correctiveActionRepository = correctiveActionRepository;
        }
        public void Execute(int id)
        {
            var correctiveAction = _correctiveActionRepository.GetOne(id);
            if (correctiveAction != null)
            {
                _correctiveActionRepository.Delete(correctiveAction);
            }
            else
            {
                throw new EntityNotFoundException(Convert.ToString(id), "No se encontró una acción correctiva con ese ID");
            }
        }
    }
}
