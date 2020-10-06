using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.TaskState.GetAll
{
    public class GetAllTaskStatesUseCase : IGetAllTaskStatesUseCase
    {
        private readonly ITaskStateRepository _TaskStateRepository;
        public GetAllTaskStatesUseCase(ITaskStateRepository TaskStateRepository) {
            _TaskStateRepository = TaskStateRepository;
        }
        public List<TaskStateOutput> Execute()
        {
            var listOutput = new List<TaskStateOutput>();
            var list = _TaskStateRepository.GetAll();
            foreach (var ts in list)
            {
                listOutput.Add(new TaskStateOutput(ts.TaskStateID,ts.Name,ts.Color,ts.Active,ts.Code));
            }

            return listOutput;
        }
    }
}
