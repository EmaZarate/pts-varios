using AutoMapper;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.Core.Domain.Job;
using Hoshin.Core.Domain.Role;
using Hoshin.Core.Domain.Sector;
using Hoshin.Core.Domain.Users;
using Hoshin.Quality.Domain.Aspect;
using Hoshin.Quality.Domain.Audit;
using Hoshin.Quality.Domain.AuditType;
using Hoshin.Quality.Domain.Finding;
using Hoshin.Quality.Domain.Evidence;
using Hoshin.Quality.Domain.FindingsState;
using Hoshin.Quality.Domain.FindingType;
using Hoshin.Quality.Domain.ParametrizationCriteria;
using System.Linq;
using Hoshin.Quality.Domain.CorrectiveAction;
using Hoshin.Quality.Domain.Task;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Mapper
{
    public class EntityToDomainMapper : Profile
    {
        public EntityToDomainMapper()
        {
            //Core.
            CreateMap<Users, User>()
                .ForMember(i => i.LastName, opt => opt.MapFrom(e => e.Surname))
                .ForMember(m => m.Roles, opt => opt.MapFrom(e => e.UserRoles.Select(x => x.Role.Name)));
                
            CreateMap<Roles, Role>();
            CreateMap<Entities.RoleClaim, Domain.Claim.Claim>();
            CreateMap<System.Security.Claims.Claim, Domain.Claim.Claim>()
                .ForMember(x => x.ClaimType, opt => opt.MapFrom(x => x.Type))
                .ForMember(x => x.ClaimValue, opt => opt.MapFrom(x => x.Value));
                
            CreateMap<Jobs, Job>()
                .ForMember(i => i.Id, opt => opt.MapFrom(e => e.JobID))
                .ForMember(i => i.Name, opt => opt.MapFrom(e => e.JobTitle));

            CreateMap<AlertUsers, Domain.AlertUser.AlertUser>()
                .ForMember(i => i.AlertType, opt => opt.MapFrom(e => e.Alert.AlertType))
                .ForMember(i => i.UsersID, opt => opt.MapFrom(e => e.Users.Id))
                .ForMember(i => i.AlertID, opt => opt.MapFrom(e => e.Alert.AlertID));

            //Quality.

            CreateMap<FindingTypes, FindingType>()
                .ForMember(i => i.Id, opt => opt.MapFrom(e => e.FindingTypeID))
                .ForMember(i => i.Parametrizations, opt => opt.MapFrom(e => e.ParametrizationsFindingTypes));
            CreateMap<FindingsStates, FindingsState>()
                .ForMember(i => i.Id, opt => opt.MapFrom(e => e.FindingStateID));
            CreateMap<ParametrizationsFindingTypes, FindingTypeParametrization>()
                .ForMember(i => i.IdParametrization, opt => opt.MapFrom(e => e.ParametrizationCriteriaID));
            CreateMap<ParametrizationCriterias, ParametrizationCriteria>()
                .ForMember(i => i.Id, opt => opt.MapFrom(e => e.ParametrizationCriteriaID));
            CreateMap<Plants, Domain.Plant.Plant>()
                .ForMember(e => e.Sectors, opt => opt.MapFrom(e => e.SectorsPlants));
            CreateMap<SectorsPlants, Domain.Sector.Sector>()
                .ForMember(e => e.Id, opt => opt.MapFrom(e => e.SectorID))
                .ForMember(e => e.Name, opt => opt.MapFrom(e => e.Sector.Name))
                .ForMember(e => e.Code, opt => opt.MapFrom(e => e.Sector.Code))
                .ForMember(e => e.ResposibleUserPlantSector, opt => opt.MapFrom(e=> e.ReferringJob))
                .ForMember(e => e.jobs, opt => opt.MapFrom(e => e.JobsSectorsPlants));
            CreateMap<JobsSectorsPlants, Job>()
                .ForMember(e => e.Active, opt => opt.MapFrom(e => e.Job.Active))
                .ForMember(e => e.Code, opt => opt.MapFrom(e => e.Job.Code))
                .ForMember(e => e.Id, opt => opt.MapFrom(e => e.Job.JobID))
                .ForMember(e => e.Name, opt => opt.MapFrom(e => e.Job.JobTitle));
            CreateMap<Findings, Finding>()
                .ForMember(i => i.Id, opt => opt.MapFrom(e => e.FindingID));
            CreateMap<FindingComments, FindingComment>();
            CreateMap<Sectors, Sector>()
                .ForMember(e => e.Id, opt => opt.MapFrom(e => e.SectorID));
            CreateMap<FindingsEvidences, Evidence>()
                .ForMember(e => e.FileName, opt => opt.MapFrom(e => e.Name));
            CreateMap<AuditsTypes, AuditType>()
                .ForMember(e => e.Id, opt => opt.MapFrom(e => e.AuditTypeID));
            CreateMap<Audits, Audit>()
            .ForMember(e => e.AuditStandardsID, opt => opt.Ignore())
            .ForMember(e => e.AuditReschedulingHistoriesID, opt => opt.Ignore());
            CreateMap<CorrectiveActions, CorrectiveAction>()
                .ForMember(e => e.DeleteEvidencesUrls, opt => opt.Ignore())
                .ForMember(e => e.NewEvidencesUrls, opt => opt.Ignore());
            CreateMap<CorrectiveActionEvidences, Evidence>()
               .ForMember(e => e.FileName, opt => opt.MapFrom(e => e.Name))
               .ForMember(e => e.EvidenceID, opt => opt.MapFrom(e => e.CorrectiveActionEvidenceID));
            CreateMap<Aspects, Aspect>();

            CreateMap<Tasks,Task>()
                .ForMember(e => e.DeleteEvidencesUrls, opt => opt.Ignore())
                .ForMember(e => e.NewEvidencesUrls, opt => opt.Ignore());
            CreateMap<TaskEvidences,Evidence>()
                   .ForMember(e => e.FileName, opt => opt.MapFrom(e => e.Name))
               .ForMember(e => e.EvidenceID, opt => opt.MapFrom(e => e.TaskEvidencesID));

        }
    }
}
