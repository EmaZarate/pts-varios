using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hoshin.Quality.Domain.ParametrizationCorrectiveAction;
using Hoshin.Quality.Application.Exceptions.Common;

namespace Hoshin.Quality.Application.UseCases.ParametrizationCorrectiveAction.CreateParametrizationCorrectiveAction
{
    public class CreateParametrizationCorrectiveActionUseCase: ICreateParametrizationCorrectiveActionUseCase
    {
        private readonly IParametrizationCorrectiveActionRepository _ParametrizationCorrectiveActionRepository;

        public CreateParametrizationCorrectiveActionUseCase(IParametrizationCorrectiveActionRepository ParametrizationCorrectiveActionRepository)
        {
            _ParametrizationCorrectiveActionRepository = ParametrizationCorrectiveActionRepository;
        }
        public ParametrizationCorrectiveActionOutput Execute(string name, string code, int value)
        {
            if(_ParametrizationCorrectiveActionRepository.Get(name, code) == null)
            {
                var newParam = new Domain.ParametrizationCorrectiveAction.ParametrizationCorrectiveAction(name, code, value);
                newParam = _ParametrizationCorrectiveActionRepository.Add(newParam);
                return new ParametrizationCorrectiveActionOutput(newParam.Id, newParam.Name, newParam.Code, newParam.Value);
            }
            else
            {
                throw new DuplicateEntityException(name, "Ya existe una entidad con este nombre y/o código");
            }
        }
    }
}
