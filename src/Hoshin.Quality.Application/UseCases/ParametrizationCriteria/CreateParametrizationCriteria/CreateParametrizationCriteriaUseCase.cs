using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hoshin.Quality.Domain.ParametrizationCriteria;
using Hoshin.Quality.Application.Exceptions.Common;

namespace Hoshin.Quality.Application.UseCases.ParametrizationCriteria.CreateParametrizationCriteria
{
    public class CreateParametrizationCriteriaUseCase: ICreateParametrizationCriteriaUseCase
    {
        private readonly IParametrizationCriteriaRepository _parametrizationCriteriaRepository;

        public CreateParametrizationCriteriaUseCase(IParametrizationCriteriaRepository parametrizationCriteriaRepository)
        {
            _parametrizationCriteriaRepository = parametrizationCriteriaRepository;
        }
        public ParametrizationCriteriaOutput Execute(string name, string datatype)
        {
            if(_parametrizationCriteriaRepository.Get(name) == null)
            {
                var newParam = new Domain.ParametrizationCriteria.ParametrizationCriteria(name, datatype);
                newParam = _parametrizationCriteriaRepository.Add(newParam);
                return new ParametrizationCriteriaOutput(newParam.Id, newParam.Name, newParam.DataType);
            }
            else
            {
                throw new DuplicateEntityException(name, "Ya existe una entidad con este nombre");
            }
        }
    }
}
