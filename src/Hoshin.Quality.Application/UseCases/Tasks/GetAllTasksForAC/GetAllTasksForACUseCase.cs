using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.Tasks.GetAllTasksForAC
{
    public class GetAllTasksForACUseCase: IGetAllTasksForACUseCase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        public GetAllTasksForACUseCase(ITaskRepository taskRepository, IMapper mapper)
        {
            this._taskRepository = taskRepository;
            this._mapper = mapper;
        }

        public List<TaskOutput> Execute()
        {
            return _mapper.Map<List<Domain.Task.Task>, List<TaskOutput>>(_taskRepository.GetAll());
        }
    }
}
