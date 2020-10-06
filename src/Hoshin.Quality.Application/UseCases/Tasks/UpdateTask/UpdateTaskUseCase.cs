using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.User.GetOneUser;
using Hoshin.CrossCutting.Logger.Interfaces;
using Hoshin.CrossCutting.Message;
using Hoshin.CrossCutting.Message.Interfaces;
using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.FinishedTasksCorrectiveAction;
using Hoshin.Quality.Application.UseCases.TaskState.GetAll;
using Hoshin.Quality.Domain.AzureStorageBlobSettings;
using Hoshin.Quality.Domain.Evidence;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TaskModel = Hoshin.Quality.Domain.Task;

namespace Hoshin.Quality.Application.UseCases.Tasks.UpdateTask
{
    public class UpdateTaskUseCase : IUpdateTaskUseCase
    {
        private const int CORRECTIVE_ACTION_ENTITY_TYPE = 1;

        private readonly ITaskRepository _taskRepository;
        private readonly ITasksEvidenceRepository _tasksEvidenceRepository;
        private readonly IAzureStorageRepository _azureStorageRepository;
        private readonly IFinishedTasksCorrectiveActionUseCase _finishedTasksCorrectiveActionUseCase;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly EmailSettings _emailSettings;
        private readonly IUserRepository _userReopository;
        private readonly IGetAllTaskStatesUseCase _getAllTaskStates;
        private readonly AzureStorageBlobSettings _azureStorageSettings;
        private readonly ICorrectiveActionRepository _correctiveActionRepository;

        public UpdateTaskUseCase(
            ITasksEvidenceRepository tasksEvidenceRepository,
            IAzureStorageRepository azureStorageRepository,
            ITaskRepository taskRepository,
            IFinishedTasksCorrectiveActionUseCase finishedTasksCorrectiveActionUseCase,
            IMapper mapper,
            IEmailSender emailSender,
            ICustomLogger logger,
            IGetAllTaskStatesUseCase getAllTaskStates,
            IGetOneUserUseCase getOneUserUse,
            IUserRepository userReopository,
            IOptions<AzureStorageBlobSettings> azureStorageSettings,
            ICorrectiveActionRepository correctiveActionRepository,
            IOptions<EmailSettings> emailSettings
            )
        {

            _azureStorageRepository = azureStorageRepository;
            _tasksEvidenceRepository = tasksEvidenceRepository;
            _taskRepository = taskRepository;
            _mapper = mapper;
            _finishedTasksCorrectiveActionUseCase = finishedTasksCorrectiveActionUseCase;
            _emailSender = emailSender;
            _userReopository = userReopository;
            _getAllTaskStates = getAllTaskStates;
            _azureStorageSettings = azureStorageSettings.Value;
            _correctiveActionRepository = correctiveActionRepository;
            _emailSettings = emailSettings.Value;
        }

        public async Task Execute(TaskModel.Task task, IFormFile[] Evidences, List<string> filesToDelete)
        {
            filesToDelete.RemoveAll(string.IsNullOrWhiteSpace);
            try
            {
                if (filesToDelete != null)
                {
                    await DeleteFiles(filesToDelete, task);
                }

                if (Evidences != null)
                {
                    await InsertFiles(Evidences, task);
                }
               
                _tasksEvidenceRepository.Update(task.TaskID, task.NewEvidencesUrls, task.DeleteEvidencesUrls);
                updateTaskProperties(task.Observation, task.TaskID, task.TaskStateID);
                if(task.EntityType == CORRECTIVE_ACTION_ENTITY_TYPE)
                {
                    await _finishedTasksCorrectiveActionUseCase.Execute(task.EntityID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private TaskOutput updateTaskProperties(string Observation, int taskID, int taskStateID)
        {
            var Task = _taskRepository.Get(taskID);
            if (Task != null)
            {
                Task.Observation = Observation;
                Task.TaskID = taskID;
                Task.TaskStateID = taskStateID;
                return _mapper.Map<TaskModel.Task, TaskOutput>(_taskRepository.UpdateTask(Task));
            }
            else
            {
                throw new Core.Application.Exceptions.Common.EntityNotFoundException(Convert.ToString(taskID), "No se encontro una tarea con ese ID");
            }
        }

        private async Task DeleteFiles(List<string> filesToDelete, Domain.Task.Task task)
        {
            task.DeleteEvidencesUrls = new List<string>();
            foreach (var fe in filesToDelete)
            {

                var fileToDelete = new Evidence();
                fileToDelete.FileName = fe;
                fileToDelete.Url = await _azureStorageRepository.DeleteFileAzureStorage(fileToDelete, _azureStorageSettings.ContainerTaskName);
                task.DeleteEvidencesUrls.Add(fileToDelete.Url);
            }
        }

        private async Task InsertFiles(IFormFile[] taskEvidence, Domain.Task.Task task)
        {
            task.NewEvidencesUrls = new List<string>();
            foreach (var fe in taskEvidence)
            {
                var fileToAdd = new Evidence();
                using (var memoryStream = new MemoryStream())
                {
                    await fe.CopyToAsync(memoryStream);
                    fileToAdd.Bytes = memoryStream.ToArray();
                }
                fileToAdd.FileName = fe.FileName;
                fileToAdd.IsInsert = true;
                fileToAdd.IsDelete = false;

                fileToAdd.Url = await _azureStorageRepository.InsertFileAzureStorage(fileToAdd, TypeData.Byte, _azureStorageSettings.ContainerTaskName);
                task.NewEvidencesUrls.Add(fileToAdd.Url);
            }
        }

        public TaskOutput Execute(TaskModel.Task task)
        {
            var taskEntity = _taskRepository.Get(task.TaskID);
            if (task != null)
            {
                taskEntity.Description = task.Description;
                taskEntity.ResponsibleUserID = task.ResponsibleUserID;
                taskEntity.ImplementationPlannedDate = task.ImplementationPlannedDate;
                taskEntity.RequireEvidence = task.RequireEvidence;
                return _mapper.Map<TaskModel.Task, TaskOutput>(_taskRepository.Update(task));
            }
            throw new EntityNotFoundException(taskEntity.TaskID, "No se encontró una tarea con ese ID");
        }

        public async Task Execute(TaskModel.Task task, string observation, DateTime overdureTime, int TaskID)
        {
            var getTask = _taskRepository.Get(TaskID);
            var users = _userReopository.GetAll();
            var idUser = "";
            foreach (var u in users)
            {
                var roles = u.Roles;
                foreach (var r in roles)
                {
                    if (r == "Aprobador de AC")
                    {
                        idUser = u.Id;
                    }
                }
            }
            var ResponsibleUser = _userReopository.Get(idUser);
            var email = ResponsibleUser.Email;

            List<string> EmailsToNotify = new List<string>();
            EmailsToNotify.Add(email);
            int taskStateID = 0;
            var url = $"{_emailSettings.Url}/quality/tasks/{ TaskID }/detail";
            var content = $"<html>" +
                                $"<body>" +
                                    $"<p>Estimado Usuario: </p>" +
                                    $"<p>Se solicita extensión fecha de vencimiento de una tarea de Acción Correctiva. </p>" +

                                    $"<p><b>Id tarea: </b>{getTask.TaskID}.</p>" +
                                    $"<p><b>Descripción de la tarea: </b>{getTask.Description}.</p>" +
                                    $"<p><b>Responsable asignado a la tarea: </b>{getTask.ResponsibleUser.FullName}.</p>" +
                                    $"<p><b>Fecha vencimiento de la tarea: </b>{getTask.ImplementationPlannedDate.ToString("dd/MM/yyyy")}.</p>" +
                                    $"<p><b>Estado: </b>{getTask.TaskState.Name}.</p>" +
                                    $"<p><b>Fecha vencimiento solicitada: </b>{task.overdureTime.Value.ToString("dd/MM/yyyy")}.</p>" +
                                    $"<p>Puede acceder desde aquí: <a href={url}>Ver tarea de Acción Correctiva</a>.</p>" +
                                    $"<p></p>" +
                                    $"<p>Saludos cordiales.</p>" +
                                $"</body>" +
                            $"</html>";
            await _emailSender.SendEmailAsync(EmailsToNotify.ToArray(), new List<string>().ToArray(), new List<string>().ToArray(), "Solicitud extensión de fecha de vencimiento de tarea de AC", content, true, System.Net.Mail.MailPriority.High);
            var allTaskStates = _getAllTaskStates.Execute();

            foreach (var s in allTaskStates)
            {
                if (s.Code == "EFV")
                {
                    taskStateID = s.TaskStateID;
                }
            }


            getTask.TaskStateID = taskStateID;
                 _taskRepository.Update(getTask);
        }

    }
}




      

     

