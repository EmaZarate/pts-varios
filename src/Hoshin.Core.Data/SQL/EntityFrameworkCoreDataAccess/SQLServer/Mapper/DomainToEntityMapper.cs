using AutoMapper;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.Core.Domain;
using Hoshin.Core.Domain.Job;
using Hoshin.Core.Domain.Plant;
using Hoshin.Core.Domain.Sector;
using Hoshin.Core.Domain.Users;
using Hoshin.Quality.Domain.AuditType;
using Hoshin.Quality.Domain.CorrectiveAction;
using Hoshin.Quality.Domain.Finding;
using Hoshin.Quality.Domain.FindingType;
using Hoshin.Quality.Domain.ParametrizationCriteria;
using Hoshin.Quality.Domain.ReassignmentsFindingHistory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Mapper
{
    public class DomainToEntityMapper : Profile
    {
        public DomainToEntityMapper()
        {   
            CreateMap<ParametrizationCriteria, ParametrizationCriterias>()
                .ForMember(e => e.ParametrizationCriteriaID, opt => opt.MapFrom(e => e.Id));
            CreateMap<User, Users>()
                .ForMember(m => m.Surname, opt => opt.MapFrom(e => e.LastName));
            CreateMap<FindingType, FindingTypes>()
                .ForMember(e => e.FindingTypeID, opt => opt.MapFrom(e => e.Id))
                .ForMember(e => e.ParametrizationsFindingTypes, opt => opt.MapFrom(e => e.Parametrizations));
                //.ForMember(p => p.ParametrizationsFindingTypes, opt=> opt.MapFrom(pc => pc.Parametrizations));
                //.ForMember(p => p.ParametrizationsFindingTypes, opt => opt.Ignore());
            CreateMap<FindingTypeParametrization, ParametrizationsFindingTypes>()
                .ForMember(p => p.ParametrizationCriteriaID, opt => opt.MapFrom(e => e.IdParametrization))
                .ForMember(p => p.Value, opt => opt.MapFrom(e => e.Value));
            CreateMap<Finding, Findings>()
                .ForMember(e => e.FindingID, opt => opt.MapFrom(e => e.Id));
            CreateMap<Plant, Plants>();
            CreateMap<Sector, Sectors>()
                .ForMember(e => e.SectorID, opt => opt.MapFrom(e => e.Id));
            CreateMap<Job, Jobs>()
                .ForMember(e => e.JobID, opt => opt.MapFrom(e => e.Id))
                .ForMember(e => e.JobTitle, opt => opt.MapFrom(e => e.Name));
            CreateMap<AuditType, AuditsTypes>()
                .ForMember(e => e.AuditTypeID, opt => opt.MapFrom(e => e.Id));

            CreateMap<CorrectiveAction, CorrectiveActions>()
                .ForMember(p => p.Evidences, opt => opt.Ignore());
            CreateMap<SectorPlant, SectorsPlants>()
                .ForMember(p => p.JobsSectorsPlants, opt => opt.Ignore())
                .ForMember(p => p.FindingLocation, opt => opt.Ignore())
                .ForMember(p => p.FindingTreatment, opt => opt.Ignore())
                .ForMember(p => p.Audits, opt => opt.Ignore());


            CreateMap<Quality.Domain.CorrectiveActionFishbone.CorrectiveActionFishbone, CorrectiveActionFishbone>()
                .ForMember(m => m.CorrectiveActionFishboneCauses, opt => opt.MapFrom(m => m.Causes))
                .ReverseMap();
            CreateMap<Quality.Domain.CorrectiveActionFishbone.CorrectiveActionFishboneCause, CorrectiveActionFishboneCauses>()
                .ForMember(m => m.CorrectiveActionFishboneCauseWhys, opt => opt.MapFrom(m => m.Whys))
                .ReverseMap();
            CreateMap<Quality.Domain.CorrectiveActionFishbone.CorrectiveActionFishboneWhy, CorrectiveActionFishboneCauseWhys>()
                .ReverseMap();

            CreateMap<Quality.Domain.UserCorrectiveAction.UserCorrectiveAction, UserCorrectiveAction>()
                .ForMember(p => p.CorrectiveActions, opt => opt.Ignore());
        }
    }
}
