using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.ParametrizationCriteria.UpdateParametrizationCriteria
{
    public class UpdateParametrizationCriteriaUseCase : IUpdateParametrizationCriteriaUseCase
    {
        private readonly IParametrizationCriteriaRepository _parametrizationCriteriaRepository;

        public UpdateParametrizationCriteriaUseCase(IParametrizationCriteriaRepository parametrizationCriteriaRepository)
        {
            _parametrizationCriteriaRepository = parametrizationCriteriaRepository;
        }
        public ParametrizationCriteriaOutput Execute(int id, string name, string datatype)
        {
            var paramC = _parametrizationCriteriaRepository.Get(id);
            if(paramC != null)
            {

                var validateName = _parametrizationCriteriaRepository.Get(name);
                if (validateName == null || validateName.Id == id)
                {
                    paramC.Name = name;
                    paramC.DataType = datatype;

                    var res = _parametrizationCriteriaRepository.Update(paramC);

                    return new ParametrizationCriteriaOutput(res.Id, res.Name, res.DataType);
                }
                else
                {
                    throw new DuplicateEntityException(name, "Ya existe un Tipo de hallazgo con ese nombre");
                }




            }

            throw new EntityNotFoundException(id, "No se encontró un criterio de parametrización con ese ID");
        }
    }
}
