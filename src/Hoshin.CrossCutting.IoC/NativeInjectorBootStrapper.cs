using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.CRUDJob.AddJobUseCase;
using Hoshin.Core.Application.UseCases.CRUDJob.GetAllJobsUseCase;
using Hoshin.Core.Application.UseCases.CRUDJob.GetOneJobUseCase;
using Hoshin.Core.Application.UseCases.CRUDJob.UpdateJobUseCase;
using Hoshin.Core.Application.UseCases.CRUDPlant.AddPlantUseCase;
using Hoshin.Core.Application.UseCases.CRUDPlant.GetAllPlantsUseCase;
using Hoshin.Core.Application.UseCases.CRUDPlant.GetOnePlantUseCase;
using Hoshin.Core.Application.UseCases.CRUDPlant.UpdatePlantUseCase;
using Hoshin.Core.Application.UseCases.CRUDSector.AddSectorUseCase;
using Hoshin.Core.Application.UseCases.CRUDSector.GetAllSectorsUseCase;
using Hoshin.Core.Application.UseCases.CRUDSector.GetOneSectorUseCase;
using Hoshin.Core.Application.UseCases.CRUDSector.UpdateSectorUseCase;
using Hoshin.Core.Application.UseCases.LoginUser;
using Hoshin.Core.Application.UseCases.User.GetAllUser;
using Hoshin.Core.Application.UseCases.User.GetOneUser;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality;
using Hoshin.CrossCutting.ErrorHandler.Interfaces;
using Hoshin.CrossCutting.JWT;
using Hoshin.CrossCutting.JWT.Factory;
using Hoshin.CrossCutting.Logger.Implementations;
using Hoshin.CrossCutting.Logger.Interfaces;
using Hoshin.CrossCutting.Message.Implementations;
using Hoshin.CrossCutting.Message.Interfaces;
using Hoshin.CrossCutting.MicrosoftGraph.Services.Implementations;
using Hoshin.CrossCutting.MicrosoftGraph.Services.Interfaces;
using Hoshin.CrossCutting.Storage;
using Hoshin.CrossCutting.WorkflowCore.Finding.Steps;
using Hoshin.CrossCutting.WorkflowCore.GenericSteps;
using Hoshin.CrossCutting.WorkflowCore.Interfaces;
using Hoshin.CrossCutting.WorkflowCore.Repositories;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.ApproveFinding;
using Hoshin.Quality.Application.UseCases.CloseFinding;
using Hoshin.Quality.Application.UseCases.CreateFinding;
using Hoshin.Quality.Application.UseCases.UpdateApprovedFinding;
using Microsoft.Extensions.DependencyInjection;
using WorkflowCore.Interface;
using WorkflowCore.Services;
using Hoshin.Quality.Application.UseCases.ParametrizationCriteria.GetAllParametrizationCriteria;
using Hoshin.Quality.Application.UseCases.ParametrizationCriteria.GetOneParametrizationCriteria;
using Hoshin.Quality.Application.UseCases.ParametrizationCriteria.CreateParametrizationCriteria;
using Hoshin.Quality.Application.UseCases.ParametrizationCriteria.UpdateParametrizationCriteria;
using Hoshin.Quality.Application.UseCases.EditExpirationDateFinding;
using Hoshin.Quality.Application.UseCases.Finding.GetAllFinding;
using Hoshin.Quality.Application.UseCases.Finding.GetCountFinding;
using Hoshin.Quality.Application.UseCases.Finding.GetOneFinding;
using Hoshin.Quality.Application.UseCases.FindingsStates.CreateFindingsStateUseCase;
using Hoshin.Quality.Application.UseCases.FindingsStates.GetAllFindingsStates;
using Hoshin.Quality.Application.UseCases.FindingsStates.GetOneFindingsStates;
using Hoshin.Quality.Application.UseCases.FindingsStates.UpdateFindingsStates;
using Hoshin.Quality.Application.UseCases.FindingType.CreateFindingType;
using Hoshin.Quality.Application.UseCases.FindingType.DeleteFindingType;
using Hoshin.Quality.Application.UseCases.FindingType.GetAllActiveFindingType;
using Hoshin.Quality.Application.UseCases.FindingType.GetAllFindingType;
using Hoshin.Quality.Application.UseCases.FindingType.GetOneFindingType;
using Hoshin.Quality.Application.UseCases.FindingType.UpdateFindingType;
using Hoshin.Quality.Application.UseCases.ReassignFinding.ApproveReassignment;
using Hoshin.Core.Application.UseCases.Role.AddRole;
using Hoshin.Core.Application.UseCases.Role.CheckIfNameExists;
using Hoshin.Core.Application.UseCases.Role.CheckIfBasicExists;
using Hoshin.Core.Application.UseCases.Claim;
using Hoshin.Core.Application.UseCases.Role.GetRole;
using Hoshin.Core.Application.UseCases.Role.UpdateRole;
using Hoshin.Core.Application.UseCases.Role.GetAllRole;
using Hoshin.Core.Application.UseCases.Role.GetAllRolesActive;
using Hoshin.Quality.Application.UseCases.ReassignFinding.GetLastReassignment;
using Hoshin.Quality.Application.UseCases.ReassignFinding.RequestReassign;
using Hoshin.Core.Application.UseCases.User.AddUser;
using Hoshin.Core.Application.UseCases.User.UpdateUser;
using Hoshin.Infra.AzureStorage.Implementations;
using Hoshin.Core.Application.UseCases.AssignJobsSectorsPlant;
using Hoshin.Core.Application.UseCases.CRUDCompany.GetAllCompaniesUseCase;
using Hoshin.Core.Application.UseCases.CRUDCompany.GetOneCompanyUseCase;
using Hoshin.Core.Application.UseCases.CRUDCompany.AddCompanyUseCase;
using Hoshin.Core.Application.UseCases.CRUDCompany.UpdateCompanyUseCase;
using Hoshin.Core.Application.UseCases.AlertUser.UpdateAlertUserUseCase;
using Hoshin.Core.Application.UseCases.AlertUser.GetAllAlertUser;
using Hoshin.Core.Application.UseCases.Alert.GetAllAlert;
using Hoshin.Quality.Application.UseCases.AuditState.CreateAuditState;
using Hoshin.Quality.Application.UseCases.AuditState.GetAllAuditState;
using Hoshin.Quality.Application.UseCases.AuditState.GetOneAuditState;
using Hoshin.Quality.Application.UseCases.AuditState.UpdateAuditState;
using Hoshin.Quality.Application.Helpers;
using Hoshin.Quality.Application.UseCases.AspectStates.GetAllAspectStates;
using Hoshin.Quality.Application.UseCases.AspectStates.UpdateAspectStatus;
using Hoshin.Quality.Application.UseCases.AspectStates.CreateAspectState;
using Hoshin.Quality.Application.UseCases.AspectStates.GetOneAspectState;
using Hoshin.Quality.Application.UseCases.CRUDAuditTypes.GetOneAuditTypeUseCase;
using Hoshin.Quality.Application.UseCases.CRUDAuditTypes.GetAllAuditTypesUseCase;
using Hoshin.Quality.Application.UseCases.CRUDAuditTypes.AddAuditTypeUseCase;
using Hoshin.Quality.Application.UseCases.CRUDAuditTypes.UpdateAuditTypeUseCase;
using Hoshin.Quality.Application.UseCases.CRUDAuditTypes.GetAllActivesAuditTypesUseCase;

using Hoshin.Quality.Application.UseCases.Standard.CreateStandard;
using Hoshin.Quality.Application.UseCases.Standard.GetAllStandard;
using Hoshin.Quality.Application.UseCases.Standard.GetOneStandard;
using Hoshin.Quality.Application.UseCases.Standard.UpdateStandard;
using Hoshin.Quality.Application.UseCases.ApproveRejectAudit;

using Hoshin.Quality.Application.UseCases.Audit.CreateAudit;
using Hoshin.CrossCutting.WorkflowCore.Audit.Steps;
using Hoshin.Quality.Application.UseCases.Audit.GetOneAudit;
using Hoshin.Quality.Application.UseCases.Audit.GetAllAudit;
using Hoshin.Quality.Application.UseCases.Audit.UpdateAudit;
using Hoshin.Quality.Application.UseCases.Audit.PlanningAudit;

using Hoshin.Quality.Application.UseCases.AuditStandardAspect.GetAllAuditStandardAspect;
using Hoshin.Quality.Application.UseCases.Aspect.GetOneAspectUseCase;
using Hoshin.Quality.Application.UseCases.FindingType.GetAllForAuditFindingType;
using Hoshin.Quality.Application.UseCases.AddFindingToAspect;
using Hoshin.Quality.Application.UseCases.Finding.UpdateFinding;
using Hoshin.Quality.Application.UseCases.DeleteFindingFromAspect;
using Hoshin.Quality.Application.UseCases.AuditStandardAspect.SetWithoutFindings;
using Hoshin.Quality.Application.UseCases.AuditStandardAspect.SetNoAudited;
using Hoshin.Quality.Application.UseCases.Audit.EmitReportAudit;
using Hoshin.Quality.Application.UseCases.ApproveRejectReportAudit;
using Hoshin.Quality.Application.UseCases.Audit.DeleteAudit;
using Hoshin.Quality.Application.UseCases.Audit.GetCountAudit;
using Hoshin.Quality.Application.UseCases.CorrectiveActionStates.GetAllCorrectiveActionStates;
using Hoshin.Quality.Application.UseCases.CorrectiveActionStates.GetOneCorrectiveActionState;
using Hoshin.Quality.Application.UseCases.CorrectiveActionStates.CreateCorrectiveActionState;
using Hoshin.Quality.Application.UseCases.CorrectiveActionStates.UpdateCorrectiveActionState;
using Hoshin.Quality.Application.UseCases.CorrectiveAction.GetCountCorrectiveActions;
using Hoshin.Quality.Application.UseCases.CorrectiveAction.CreateCorrectiveAction;
using Hoshin.Quality.Application.UseCases.CorrectiveAction.UpdateCorrectiveAction;
using Hoshin.Quality.Application.UseCases.ExtendDueDateTask;
using Hoshin.Quality.Application.UseCases.Finding.GetAllApprovedInProgressFinding;
using Hoshin.Quality.Application.UseCases.TaskState.GetAll;
using Hoshin.Quality.Application.UseCases.TaskState.GetOneTaskState;
using Hoshin.Quality.Application.UseCases.TaskState.UpdateTaskState;
using Hoshin.Quality.Application.UseCases.TaskState.CreateTaskState;
using Hoshin.Quality.Application.UseCases.TaskState.CreateTaskStateUseCase;
using Hoshin.Quality.Application.UseCases.FishBone.GetAll;
using Hoshin.Quality.Application.UseCases.FishBone.GetAllActive;
using Hoshin.Quality.Application.UseCases.FishBone.UpdateFishBone;
using Hoshin.Quality.Application.UseCases.FishBone.GetOneFishBone;
using Hoshin.Quality.Application.UseCases.FishBone.CreateFishBone;
using Hoshin.Quality.Application.UseCases.CorrectiveAction.GetAllCorrectiveAction;
using Hoshin.Quality.Application.UseCases.EditCorrectiveActionFishbone;
using Hoshin.Quality.Application.UseCases.Tasks.GetOneTask;
using Hoshin.Quality.Application.UseCases.CorrectiveAction.GetOneCorrectiveAction;
using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Steps;
using Hoshin.Quality.Application.UseCases.OverduePlannigDate;
using Hoshin.Quality.Application.UseCases.Tasks.CreateTask;
using Hoshin.Quality.Application.UseCases.Tasks.UpdateTask;
using Hoshin.Quality.Application.UseCases.Tasks.DeleteTask;
using Hoshin.Quality.Application.UseCases.EditCorrectiveActionWorkgroup;
using Hoshin.Quality.Application.UseCases.CorrectiveActionEvidence.UpdateCorrectiveActionEvidence;
using Hoshin.Quality.Application.UseCases.GenerateCorrectiveAction;
using Hoshin.Quality.Application.UseCases.FinishedTasksCorrectiveAction;
using Hoshin.Quality.Application.UseCases.EvaluateCorrectiveAction;
using Hoshin.Quality.Application.UseCases.Tasks.GetAllTask;
using Hoshin.Quality.Application.UseCases.Tasks.GetAllTasksForAC;
using Hoshin.Quality.Application.UseCases.CorrectiveAction.DeleteCorrectiveAction;
using Hoshin.Quality.Application.UseCases.ParametrizationCorrectiveAction.GetAllParametrizationCorrectiveAction;
using Hoshin.Quality.Application.UseCases.ParametrizationCorrectiveAction.GetOneParametrizationCorrectiveAction;
using Hoshin.Quality.Application.UseCases.ParametrizationCorrectiveAction.CreateParametrizationCorrectiveAction;
using Hoshin.Quality.Application.UseCases.ParametrizationCorrectiveAction.UpdateParametrizationCorrectiveAction;
using Hoshin.Quality.Application.UseCases.Tasks.GetCountTask;
using Hoshin.Quality.Application.UseCases.ExtendDueDateCorrectiveAction;

using ICorrectiveActionStatesHistoryRepository = Hoshin.Quality.Application.Repositories.ICorrectiveActionStatesHistoryRepository;
using Hoshin.Quality.Application.UseCases.CorrectiveAction.EditImpactCorrectiveAction;
using Hoshin.Quality.Application.UseCases.OverdueEvaluateDateCorrectiveAction;
using Hoshin.Quality.Application.UseCases.OverdueEvaluateDateUseCase;
using Hoshin.Quality.Application.UseCases.ReassignCorrectiveAction.RequestReassignAC;
using Hoshin.Core.Application.UseCases.User.ResetPasswordUser;

namespace Hoshin.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Cross Cutting - Logger
            services.AddTransient<ICustomLogger, CustomLogger>();
            services.AddTransient<ICustomLoggerProvider, CustomLoggerProvider>();

            //Message Layer
            services.AddTransient<IEmailSender, EmailSender>();

            //WorkFlowCore
            services.AddSingleton<IWorkflowRegistry, WorkflowRegistry>();
            services.AddSingleton<IWorkflowCore, WorkflowCore.Implementations.WorkflowCore>();
            //Steps
            services.AddTransient<SendEmail>();
            services.AddTransient<NewFinding>();
            services.AddTransient<UpdateApprovedFinding>();
            services.AddTransient<ApproveFinding>();
            services.AddTransient<RejectFinding>();
            services.AddTransient<CloseFinding>();
            services.AddTransient<ReassignFinding>();
            services.AddTransient<GenerateFindingReassignment>();
            services.AddTransient<RejectReassignFinding>();

            services.AddTransient<ScheduleAudit>();
            services.AddTransient<PlanAudit>();
            services.AddTransient<ApprovePlanAudit>();
            services.AddTransient<RejectPlanAudit>();
            services.AddTransient<RePlanAudit>();
            services.AddTransient<EmmitReportAudit>();
            services.AddTransient<RejectReportAudit>();
            services.AddTransient<ReEmmitReportAudit>();
            services.AddTransient<CloseAudit>();

            services.AddTransient<FinishedTasks>();
            services.AddTransient<GenerateCorrectiveAction>();
            services.AddTransient<GenerateTaskUsers>();
            services.AddTransient<NewCorrectiveAction>();
            services.AddTransient<ReviewedCorrectiveAction>();


            //Cross Cutting - JWT
            services.AddTransient<IJwtFactory, JwtFactory>();
            services.AddTransient<IJwtService, JwtService>();

            //Cross Cutting - Microsoft Graph
            services.AddTransient<IDriveService, DriveService>();
            services.AddTransient<IDriveItemService, DriveItemService>();
            services.AddTransient<IUserService, UserService>();

            //Cross Cutting - Storage
            services.AddSingleton<IStorage, Hoshin.CrossCutting.Storage.Storage>();

            //Cross Cutting - ErrorHandler
            services.AddScoped<IErrorHandler, Hoshin.CrossCutting.ErrorHandler.Implementations.ErrorHandler>();

            //Infra
            services.AddTransient<IAzureStorageRepository, AzureStorage>();

            //Repositories
            services.AddTransient<IAspectStatesRepository, AspectStatesRepository>();
            services.AddTransient<Core.Application.Repositories.IUserRepository, UserRepository>();
            services.AddTransient<Hoshin.CrossCutting.JWT.Repositories.IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IAlertUserRepository, AlertUserRepository>();
            services.AddTransient<IAlertRepository, AlertRepository>();
            services.AddTransient<IPlantRepository, PlantRepository>();
            services.AddTransient<ISectorRepository, SectorRepository>();
            services.AddTransient<Core.Application.Repositories.ISectorPlantRepository, SectorPlantRepository>();
            services.AddTransient<WorkflowCore.Repositories.ISectorPlantRepository, SectorPlantRepository>();
            services.AddTransient<IJobRepository, JobRepository>();
            services.AddTransient<IParametrizationCriteriaRepository, ParametrizationCriteriaRepository>();
            services.AddTransient<IFindingTypeRepository, FindingTypeRepository>();
            services.AddTransient<WorkflowCore.Repositories.IFindingRepository, FindingRepository>();
            services.AddTransient<Quality.Application.Repositories.IFindingRepository, FindingRepository>();
            services.AddTransient<WorkflowCore.Repositories.IFindingStateRepository, FindingStateRepository>();
            services.AddTransient<Quality.Application.Repositories.IFindingStateRepository, FindingStateRepository>();
            services.AddTransient<WorkflowCore.Repositories.IReassignmentsFindingHistoryRepository, ReassignmentsFindingHistoryRepository>();
            services.AddTransient<IFindingEvidenceRepository, FindingEvidenceRepository>();
            
            services.AddTransient<Quality.Application.Repositories.IReassignmentsFindingHistoryRepository, ReassignmentsFindingHistoryRepository>();
            services.AddTransient<IFindingStatesHistoryRepository, FindingStatesHistoryRepository>();
            services.AddTransient<IFindingStateHistoryRepository, FindingStatesHistoryRepository>();
            services.AddTransient<Quality.Application.Repositories.IAuditStateRepository, AuditStateRepository>();
            services.AddTransient<WorkflowCore.Repositories.IAuditStateRepository, AuditStateRepository>();
            services.AddTransient<WorkflowCore.Repositories.IUserWorkflowRepository, UserWorkflowRepository>();
            services.AddTransient<WorkflowCore.Repositories.ICorrectiveActionStatesHistoryRepository, CorrectiveActionStatesHistoryRepository>();
            services.AddTransient<ICorrectiveActionStatesHistoryRepository, CorrectiveActionStatesHistoryRepository>();

            services.AddTransient<IStandardRepository, StandardRepository>();
            services.AddTransient<Quality.Application.Repositories.IAuditRepository, AuditRepository>();
            services.AddTransient<IAuditTypeRepository, AuditTypeRepository>();
            services.AddTransient<WorkflowCore.Repositories.IAuditRepository, AuditRepository>();
            services.AddTransient<Quality.Application.Repositories.IAuditStandardAspectRepository, AuditStandardAspectRepository>();
            services.AddTransient<WorkflowCore.Repositories.IAuditStandardAspectRepository, AuditStandardAspectRepository>();
            services.AddTransient<IAspectRepository, AspectRepository>();
            services.AddTransient<Quality.Application.Repositories.ITaskStateRepository, TaskStateRepository>();
            services.AddTransient<WorkflowCore.Repositories.ITaskStateRepository, TaskStateRepository>();
            services.AddTransient<IFishBoneRepository, FishBoneRepository>();
            services.AddTransient<WorkflowCore.Repositories.ITaskRepository, TaskRepository>();

            services.AddTransient<WorkflowCore.Repositories.ICorrectiveActionStateRepository, CorrectiveActionStateRepository>();
            services.AddTransient<Quality.Application.Repositories.ICorrectiveActionStateRepository, CorrectiveActionStateRepository>();
            services.AddTransient<Quality.Application.Repositories.ICorrectiveActionRepository, CorrectiveActionRepository>();
            services.AddTransient<WorkflowCore.Repositories.ICorrectiveActionRepository, CorrectiveActionRepository>();
            services.AddTransient<Quality.Application.Repositories.ICorrectiveActionEvidenceRepository, CorrectiveActionEvidenceRepository>();
            services.AddTransient<WorkflowCore.Repositories.ICorrectiveActionEvidenceRepository, CorrectiveActionEvidenceRepository>();
            services.AddTransient<ICorrectiveActionFishboneRepository, CorrectiveActionFishboneRepository>();
            services.AddTransient<IUserCorrectiveActionsRepository, UserCorrectiveActionsRepository>();
            services.AddTransient<Quality.Application.Repositories.ITaskRepository, TaskRepository>();
            services.AddTransient<Quality.Application.Repositories.IParametrizationCorrectiveActionRepository, ParametrizationCorrectiveActionRepository>();
            services.AddTransient<WorkflowCore.Repositories.IParametrizationCorrectiveActionRepository, ParametrizationCorrectiveActionRepository>();
           

            services.AddTransient<ITasksEvidenceRepository, TaskEvidenceRespository>();
            


            //UseCases
            services.AddScoped<IGetAllAspectStatesUseCase, GetAllAspectStatesUseCase>();
            services.AddScoped<IGetOneAspectStateUseCase, GetOneAspectStateUseCase>();
            services.AddScoped<ICreateAspectStateUseCase, CreateAspectStateUseCase>();
            services.AddScoped<IUpdateAspectStatusUseCase, UpdateAspectStatusUseCase>();
            services.AddScoped<IUpdateAlertUserUseCase, UpdateAlertUserUseCase>();
            services.AddScoped<IGetAllAlertUserUseCase, GetAllAlertUserUseCase>();
            services.AddScoped<IGetAllAlertUseCase, GetAllAlertUseCase>();
            services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
            services.AddTransient<IGetOneUserUseCase, GetOneUserUseCase>();
            services.AddScoped<IGetAllUserUseCase, GetAllUserUseCase>();
            services.AddScoped<IGetAllCompaniesUseCase, GetAllCompaniesUseCase>();
            services.AddScoped<IGetOneCompanyUseCase, GetOneCompanyUseCase>();
            services.AddScoped<IAddCompanyUseCase, AddCompanyUseCase>();
            services.AddScoped<IUpdateCompanyUseCase, UpdateCompanyUseCase>();
            services.AddScoped<IGetAllPlantsUseCase, GetAllPlantsUseCase>();
            services.AddScoped<IGetOnePlantUseCase, GetOnePlantUseCase>();
            services.AddScoped<IAddPlantUseCase, AddPlantUseCase>();
            services.AddScoped<IUpdatePlantUseCase, UpdatePlantUseCase>();
            services.AddScoped<IGetAllSectorsUseCase, GetAllSectorsUseCase>();
            services.AddScoped<IGetOneSectorUseCase, GetOneSectorUseCase>();
            services.AddScoped<IAddSectorUseCase, AddSectorUseCase>();
            services.AddScoped<IUpdateSectorUseCase, UpdateSectorUseCase>();
            services.AddScoped<IGetAllJobsUseCase, GetAllJobsUseCase>();
            services.AddScoped<IGetOneJobUseCase, GetOneJobUseCase>();
            services.AddScoped<IAddJobUseCase, AddJobUseCase>();
            services.AddScoped<IUpdateJobUseCase, UpdateJobUseCase>();
            services.AddScoped<ICreateFindingTypeUseCase, CreateFindingTypeUseCase>();
            services.AddScoped<IGetAllFindingTypeUseCase, GetAllFindingTypeUseCase>();
            services.AddScoped<IGetAllActiveFindingTypeUseCase, GetAllActiveFindingTypeUseCase>();
            services.AddScoped<IGetAllForAuditFindingTypeUseCase, GetAllForAuditFindingTypeUseCase>();
            services.AddScoped<IGetOneFindingTypeUseCase, GetOneFindingTypeUseCase>();
            services.AddScoped<IUpdateFindingTypeUseCase, UpdateFindingTypeUseCase>();
            services.AddScoped<IDeleteFindingTypeUseCase, DeleteFindingTypeUseCase>();
            services.AddScoped<ICreateFindingsStateUseCase, CreateFindingsStateUseCase>();
            services.AddScoped<IGetAllFindingsStatesUseCase, GetAllFindingsStatesUseCase>();
            services.AddScoped<IGetOneFindingsStatesUseCase, GetOneFindingsStatesUseCase>();
            services.AddScoped<IUpdateFindingsStatesUseCase, UpdateFindingsStatesUseCase>();
            services.AddScoped<IGetAllFindingUseCase, GetAllFindingUseCase>();
            services.AddScoped<IUpdateFindingUseCase, UpdateFindingUseCase>();
            services.AddScoped<IDeleteFindingFromAspectUseCase, DeleteFindingFromAspectUseCase>();
            services.AddScoped<IGetCountFindingUseCase, GetCountFindingUseCase>();
            services.AddScoped<IGetOneFindingUseCase, GetOneFindingUseCase>();
            services.AddTransient<IRequestReassignUseCase, RequestReassignUseCase>();
            services.AddScoped<IGetAllFishBoneUseCase, GetAllFishBoneUseCase>();
            services.AddScoped<IGetAllActiveFishBoneUseCase, GetAllActiveFishBoneUseCase>();
            services.AddScoped<IUpdateFishBoneUseCase, UpdateFishBoneUseCase>();
            services.AddScoped<IGetOneFishBoneUseCase, GetOneFishBoneUseCase>();
            services.AddScoped<ICreateFishBoneUseCase, CreateFishBoneUseCase>();
            services.AddScoped<IGetAllTaskStatesUseCase, GetAllTaskStatesUseCase>();
            services.AddScoped<IGetOneTaskStateUseCase, GetOneTaskStateUseCase>();
            services.AddScoped<IUpdateTaskStatesUseCase, UpdateTaskStateUserCase>();
            services.AddScoped<ICreateTaskStateUseCase, CreateTaskStateUseCase>();
            services.AddTransient<IGetLastReassignment, GetLastReassignment>();
            services.AddTransient<IApproveorRejectReassignment, ApproveorRejectReassignment>();
            services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
            services.AddScoped<ICreateFindingUseCase, CreateFindingUseCase>();
            services.AddScoped<IApproveFindingUseCase, ApproveFindingUseCase>();
            services.AddScoped<IGetAllFindingUseCase, GetAllFindingUseCase>();
            services.AddScoped<IGetAllApprovedInProgressFindingUseCase, GetAllApprovedInProgressFinding>();
            services.AddScoped<IGetCountFindingUseCase, GetCountFindingUseCase>();
            services.AddScoped<IGetOneFindingUseCase, GetOneFindingUseCase>();
            services.AddScoped<IUpdateApprovedFindingUseCase, UpdateApprovedFindingUseCase>();
            services.AddScoped<ICloseFindingUseCase, CloseFindingUseCase>();
            services.AddScoped<IEditExpirationDateFindingUseCase, EditExpirationDateFindingUseCase>();
            services.AddScoped<IAddRoleUseCase, AddRoleUseCase>();
            services.AddScoped<ICheckIfNameExistsUseCase, CheckIfNameExistsUseCase>();
            services.AddScoped<ICheckIfBasicExistsUseCase, CheckIfBasicExistsUseCase>();
            services.AddScoped<IGetAllClaimsUseCase, GetAllClaimsUseCase>();
            services.AddScoped<IGetOneRoleUseCase, GetOneRoleUseCase>();
            services.AddScoped<IUpdateRoleUseCase, UpdateRoleUseCase>();
            services.AddScoped<IGetAllRolesUseCase, GetAllRolesUseCase>();
            services.AddScoped<IGetAllRolesActiveUseCase, GetAllRolesActiveUseCase>();
            services.AddScoped<IAddUserUseCase, AddUserUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            services.AddScoped<IResetPasswordUseCase, ResetPasswordUseCase>();
            services.AddScoped<IAssignJobsSectorsPlantUseCase, AssignJobsSectorsPlantUseCase>();
            services.AddScoped<IPlanningAuditUseCase, PlanningAuditUseCase>();

            services.AddScoped<IGetOneAuditTypeUseCase, GetOneAuditTypeUseCase>();
            services.AddScoped<IGetAllAuditTypesUseCase, GetAllAuditTypesUseCase>();
            services.AddScoped<IGetAllActivesAuditTypesUseCase, GetAllActivesAuditTypesUseCase>();
            services.AddScoped<IAddAuditTypeUseCase, AddAuditTypeUseCase>();
            services.AddScoped<IUpdateAuditTypeUseCase, UpdateAuditTypeUseCase>();
            services.AddScoped<ICreateAuditStateUseCase, CreateAuditStateUseCase>();
            services.AddScoped<IGetAllAuditStateUseCase, GetAllAuditStateUseCase>();
            services.AddScoped<IGetOneAuditStateUseCase, GetOneAuditStateUseCase>();
            services.AddScoped<IUpdateAuditStateUseCase, UpdateAuditStateUseCase>();
            services.AddScoped<IApproveRejectAuditUseCase, ApproveRejectAuditUseCase>();
            services.AddScoped<ICreateAuditUseCase, CreateAuditUseCase>();
            services.AddScoped<IGetAllAuditUseCase, GetAllAuditUseCase>();
            services.AddScoped<IGetOneAuditUseCase, GetOneAuditUseCase>();
            services.AddScoped<IUpdateAuditUseCase, UpdateAuditUseCase>();
            services.AddScoped<IGetAllAuditStandardAspectUseCase, GetAllAuditStandardAspectUseCase>();

            services.AddScoped<IGetOneAspectUseCase, GetOneAspectUseCase>();
            services.AddScoped<IAddFindingToAspectUseCase, AddFindingToAspectUseCase>();

            services.AddScoped<ISetWithoutFindingsAuditStandardAspectUseCase, SetWithoutFindingsAuditStandardAspectUseCase>();
            services.AddScoped<ISetNoAuditedAuditStandardAspectUseCase, SetNoAuditedAuditStandardAspectUseCase>();
            services.AddScoped<IEmitReportAuditUseCase, EmitReportAuditUseCase>();
            services.AddScoped<IApproveRejectReportAuditUseCase, ApproveRejectReportAuditUseCase>();
            services.AddScoped<IDeleteAuditUseCase, DeleteAuditUseCase>();
            services.AddScoped<IGetCountAuditUseCase, GetCountAuditUseCase>();

            services.AddScoped<IGetAllCorrectiveActionStatesUseCase, GetAllCorrectiveActionStatesUseCase>();
            services.AddScoped<IGetOneCorrectiveActionStateUseCase, GetOneCorrectiveActionStateUseCase>();
            services.AddScoped<ICreateCorrectiveActionStateUseCase, CreateCorrectiveActionStateUseCase>();
            services.AddScoped<IUpdateCorrectiveActionStateUseCase, UpdateCorrectiveActionStateUseCase>();
            services.AddScoped<IGetAllCorrectiveActionsUseCase, GetAllCorrectiveActionsUseCase>();
            services.AddScoped<ICreateCorrectiveActionUseCase, CreateCorrectiveActionUseCase>();
            services.AddScoped<IDeleteCorrectiveActionUseCase, DeleteCorrectiveActionUseCase>();
            services.AddScoped<IUpdateCorrectiveActionUseCase, UpdateCorrectiveActionUseCase>();
            services.AddScoped<IGetCountCorrectiveActionsUseCase, GetCountCorrectiveActionsUseCase>();
            services.AddScoped<IEditCorrectiveActionFishboneUseCase, EditCorrectiveActionFishboneUseCase>();
            services.AddScoped<IOverduePlannignDateUseCase, OverduePlannignDateUseCase>();
            services.AddScoped<IOverdueEvaluateDateUseCase, OverdueEvaluateDateUseCase>();
            services.AddScoped<IGetOneCorrectiveActionUseCase, GetOneCorrectiveActionUseCase>();

            services.AddScoped<IUpdateCorrectiveActionEvidenceUseCase, UpdateCorrectiveActionEvidenceUseCase>();
            services.AddScoped<IGenerateCorrectiveActionUseCase, GenerateCorrectiveActionUseCase>();

            services.AddScoped<ICreateTaskUseCase, CreateTaskUseCase>();
            services.AddScoped<IUpdateTaskUseCase, UpdateTaskUseCase>();
            services.AddScoped<IDeleteTaskUseCase, DeleteTaskUseCase>();
            services.AddScoped<IGetAllTaskUseCase, GetAllTaskUseCase>();
            services.AddScoped<IGetAllTasksForACUseCase, GetAllTasksForACUseCase>();
            services.AddScoped<IGetOneTaskUseCase, GetOneTaskUseCase>();
            services.AddScoped<IGetCountTaskUseCase, GetCountTaskUseCase>();
            services.AddScoped<IExtendDueDateTaskUseCase, ExtendDueDateTaskUseCase>();

            services.AddScoped<IFinishedTasksCorrectiveActionUseCase, FinishedTasksCorrectiveActionUseCase>();
            services.AddScoped<IEvaluateCorrectiveActionUseCase, EvaluateCorrectiveActionUseCase>();
            services.AddScoped<IGetAllParametrizationCorrectiveActionUseCase, GetAllParametrizationCorrectiveActionUseCase>();
            services.AddScoped<IGetOneParametrizationCorrectiveActionUseCase, GetOneParametrizationCorrectiveActionUseCase>();
            services.AddScoped<ICreateParametrizationCorrectiveActionUseCase, CreateParametrizationCorrectiveActionUseCase>();
            services.AddScoped<IUpdateParametrizationCorrectiveActionUseCase, UpdateParametrizationCorrectiveActionUseCase>();

            services.AddScoped<IGetAllParametrizationCriteriaUseCase, GetAllParametrizationCriteriaUseCase>();
            services.AddScoped<IGetOneParametrizationCriteriaUseCase, GetOneParametrizationCriteriaUseCase>();
            services.AddScoped<ICreateParametrizationCriteriaUseCase, CreateParametrizationCriteriaUseCase>();
            services.AddScoped<IUpdateParametrizationCriteriaUseCase, UpdateParametrizationCriteriaUseCase>();

            services.AddScoped<IEditCorrectiveActionWorkgroupUseCase, EditCorrectiveActionWorkgroupUseCase>();

            services.AddScoped<IExtendDueDateCorrectiveActionUseCase, ExtendDueDateCorrectiveActionUseCase>();
            services.AddScoped<IEditImpactCorrectiveActionUseCase, EditImpactCorrectiveActionUseCase>();
            services.AddScoped<IRequestReassignACUseCase, RequestReassignACUseCase>();


            //Helpers
            services.AddTransient<IUserLoggedHelper, UserLoggedHelper>();
            services.AddScoped<ICreateStandardUseCase, CreateStandardUseCase>();
            services.AddScoped<IGetAllStandardUseCase, GetAllStandardUseCase>();
            services.AddScoped<IGetOneStandardUseCase, GetOneStandardUseCase>();
            services.AddScoped<IUpdateStandardUseCase, UpdateStandardUseCase>();

        }
    }
}
