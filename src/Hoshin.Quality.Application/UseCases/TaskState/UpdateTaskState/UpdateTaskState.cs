using AutoMapper;
using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;

namespace Hoshin.Quality.Application.UseCases.TaskState.UpdateTaskState
{
    public class UpdateTaskStateUserCase : IUpdateTaskStatesUseCase
    {
        private readonly ITaskStateRepository _taskStateRepository;
        private readonly IMapper _mapper;
        public UpdateTaskStateUserCase(ITaskStateRepository taskStateRepository )
        {
            _taskStateRepository = taskStateRepository;
         
        }
        
        public TaskStateOutput Execute(int id, string code, string name, string colour, bool active)
        {
            var taskState = _taskStateRepository.Get(id);
            if (taskState != null)
            {
                if (_taskStateRepository.CheckExistsTaskState(code, name, colour, id) == null)
                {
                    taskState.Name = name;
                    taskState.Code = code;
                    taskState.Active = active;
                    taskState.Color = colour;
                    var res = _taskStateRepository.Update(taskState);
                    return new TaskStateOutput(taskState.TaskStateID, taskState.Name, taskState.Color, taskState.Active, taskState.Code);

                }
                else
                {
                    throw new DuplicateEntityException(name, "Ya existe un estado de tarea con este nombre o código");
                }
            }
            throw new EntityNotFoundException(id, "No se encontró un estado de hallazgo con ese ID");
        }

      /*  public TaskStateOutput Execute(Domain.TaskState.TaskState taskState)
        {
            var existTaskState = _taskStateRepository.Get(taskState.TaskStateID);
            
            
            if (existTaskState != null)

            {
                var updateTaskState = _taskStateRepository.Update(taskState);
                return _mapper.Map<Domain.TaskState.TaskState, TaskStateOutput>(updateTaskState);
            }
            else
            {
                throw new DuplicateEntityException(taskState.Name, "Ya existe un estado de auditoria con este nombre, código o color", 436);
            }
        }*/
    }
}
