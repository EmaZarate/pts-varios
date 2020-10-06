using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.ParametrizationCorrectiveAction.UpdateParametrizationCorrectiveAction
{
    public class UpdateParametrizationCorrectiveActionUseCase : IUpdateParametrizationCorrectiveActionUseCase
    {
        private readonly IParametrizationCorrectiveActionRepository _ParametrizationCorrectiveActionRepository;

        public UpdateParametrizationCorrectiveActionUseCase(IParametrizationCorrectiveActionRepository ParametrizationCorrectiveActionRepository)
        {
            _ParametrizationCorrectiveActionRepository = ParametrizationCorrectiveActionRepository;
        }
        public ParametrizationCorrectiveActionOutput Execute(int id, string name, string code, int value)
        {
            var paramC = _ParametrizationCorrectiveActionRepository.Get(id);
            if(paramC != null)
            {
                var validateName = _ParametrizationCorrectiveActionRepository.GetByName(name);
                if (validateName == null || validateName.Id == id)
                {
                    paramC.Name = name;
                    paramC.Value = value;
                
                    var res = _ParametrizationCorrectiveActionRepository.Update(paramC);

                    return new ParametrizationCorrectiveActionOutput(res.Id, res.Name, res.Code, res.Value);
                }
                else
                {
                    throw new DuplicateEntityException(name, "Ya existe un parametro con ese nombre");
                }
            }
            throw new EntityNotFoundException(id, "No se encontró un criterio de parametrización con ese ID");
        }
    }
}
