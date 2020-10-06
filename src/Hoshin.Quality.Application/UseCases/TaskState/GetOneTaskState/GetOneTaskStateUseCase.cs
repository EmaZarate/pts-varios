using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;

namespace Hoshin.Quality.Application.UseCases.TaskState.GetOneTaskState
{
     public class GetOneTaskStateUseCase : IGetOneTaskStateUseCase

    {
        private ITaskStateRepository _stateTaskRepository;
        private readonly IMapper _mapper;

        public GetOneTaskStateUseCase(ITaskStateRepository stateTaskRepository, IMapper mapper)
        {
            _stateTaskRepository = stateTaskRepository;
            _mapper = mapper; 
        }

        public TaskStateOutput Execute( int id)
        {
            var paramC = _stateTaskRepository.Get(id);
            if (paramC != null)
            {
                return new TaskStateOutput(paramC.TaskStateID, paramC.Name, paramC.Color, paramC.Active, paramC.Code);
            }
            throw new EntityNotFoundException(id,"No se encontro un estado de Hallazgo con ese ID");
        }

    }

}
/* return _mapper.Map<Domain.TaskState.TaskState, TaskStateOutput>(_stateTaskRepository.Get(id));
*/