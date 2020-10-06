using AutoMapper;
using Hoshin.Core.Domain.Job;
using Hoshin.Core.Domain.Plant;
using Hoshin.Core.Domain.Sector;
using Hoshin.Quality.Domain.Finding;
using Hoshin.Quality.Domain.FindingType;
using Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.AssignJobsSectorsPlant;
using Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.CRUDJobs;
using Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.CRUDPlants;
using Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.CRUDSectors;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CreateFinding;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.ApproveFinding;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.UpdateApprovedFinding;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDFindingTypes;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.EditExpirationDateFinding;
using Hoshin.Quality.Domain.Evidence;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDAuditTypes;
using Hoshin.Quality.Domain.AuditType;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDAudit;
using Hoshin.Quality.Domain.Audit;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.AuditStandardAspect;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDFinding;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDCorrectiveActionStates;
using Hoshin.Quality.Domain.CorrectiveActionState;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDCorrectiveAction;
using Hoshin.Quality.Domain.CorrectiveAction;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.EditCorrectiveActionFishbone;
using Hoshin.Quality.Domain.CorrectiveActionFishbone;
using Hoshin.Quality.Domain.Task;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDTask;
using Hoshin.Core.Domain.Users;
using Hoshin.WebApi.Controllers.Hoshin.Core.UseCases.CRUDUser;

namespace Hoshin.WebApi.Mapper
{
    public class DTOToDomain : Profile
    {
        public DTOToDomain()
        {
            CreateMap<Controllers.Hoshin.Quality.UseCases.CreateFinding.FindingEvidenceDTO, Evidence>();
            CreateMap<Controllers.Hoshin.Quality.UseCases.ApproveFinding.FindingEvidenceDTO, Evidence>();
            CreateMap<Controllers.Hoshin.Quality.UseCases.UpdateApprovedFinding.FindingEvidenceDTO, Evidence>();
            CreateMap<FindingTypesDTO, FindingType>();
            CreateMap<FindingDTO, Finding>()
                 .ForMember(x => x.Id, opt => opt.MapFrom(y => y.FindingID));
            CreateMap<ApproveFindingDTO, Finding>()
                .ForMember(e => e.Id, opt => opt.MapFrom(e => e.FindingID));
            CreateMap<AuditStandardAspectFindingDTO, Finding>();
            CreateMap<EditExpirationDateFindingDTO, Finding>();
            CreateMap<PlantDTO, Plant>();
            CreateMap<SectorDTO, Sector>()
                .ForMember(e => e.Id, opt => opt.MapFrom(e => e.SectorId));
            CreateMap<JobDTO, Job>()
                .ForMember(e => e.Id, opt => opt.MapFrom(e => e.JobId));
            CreateMap<JobsSectorDTO, Job>();
            CreateMap<AssignSectorsPlantDTO, Sector>()
                .ForMember(e => e.jobs, opt => opt.MapFrom(e => e.Jobs));
            CreateMap<AssignJobsSectorsPlantsDTO, Plant>()
                .ForMember(e => e.PlantID, opt => opt.MapFrom(e => e.Id));
            CreateMap<AuditStandardAspectDTO, Quality.Domain.AuditStandardAspect>();

            CreateMap<AuditDTO, Audit>()
            .ForMember(e => e.AuditStandardsID, opt => opt.MapFrom(e => e.AuditStandard))
            .ForMember(e => e.AuditState, opt => opt.Ignore())
            .ForMember(e => e.AuditStandards, opt => opt.Ignore());

            CreateMap<CorrectiveActionStateDTO, CorrectiveActionState>()
                .ForMember(e => e.CorrectiveActionStateID, opt => opt.MapFrom(e => e.ID));
            CreateMap<CorrectiveActionDTO, CorrectiveAction>()
                .ForMember(e => e.FindingID, opt => opt.MapFrom(e => e.RelatedFindingId));

            CreateMap<EditCorrectiveActionFishboneWhyDTO, CorrectiveActionFishboneWhy>();

            CreateMap<EditCorrectiveActionFishboneCauseDTO, CorrectiveActionFishboneCause>()
                .ForMember(e => e.X1, opt => opt.MapFrom(e => e.Coords[0].X1))
                .ForMember(e => e.X2, opt => opt.MapFrom(e => e.Coords[0].X2))
                .ForMember(e => e.Y1, opt => opt.MapFrom(e => e.Coords[0].Y1))
                .ForMember(e => e.Y2, opt => opt.MapFrom(e => e.Coords[0].Y2));

            CreateMap<EditCorrectiveActionFishboneDTO, CorrectiveActionFishbone>();

            CreateMap<TaskDTO, Task>()
              .ForMember(e => e.ResponsibleUser, opt => opt.Ignore())
              .ForMember(e => e.Result, opt => opt.Ignore())
              .ForMember(e => e.TaskState, opt => opt.Ignore())
              .ForMember(e => e.NewEvidencesUrls, opt => opt.Ignore())
              .ForMember(e => e.TaskEvidences, opt => opt.Ignore());
            CreateMap<UserDTO, User>()
                .ForMember(e => e.JobSectorPlant, opt => opt.Ignore());

        }
    }
}
