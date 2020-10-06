using AutoMapper;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.CrossCutting.WorkflowCore.Audit.Data;
using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data;
using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Mapper
{
    public class EntityToWorkflowDataMapper : Profile
    {
        public EntityToWorkflowDataMapper()
        {
            CreateMap<Findings, FindingWorkflowData>()
                .ForMember(x => x.FindingStateName, opt => opt.MapFrom(e => e.FindingState.Name))
                .ForMember(x => x.FindingStateColor, opt => opt.MapFrom(e => e.FindingState.Colour))
                .ForMember(x => x.FindingStateCode, opt => opt.MapFrom(e => e.FindingState.Code))
                .ForMember(x => x.FindingTypeName, opt => opt.MapFrom(e => e.FindingType.Name))
                .ForMember(x => x.SectorPlantTreatmentPlantName, opt => opt.MapFrom(e => e.EmitterUser.JobSectorPlant.SectorPlant.Plant.Name))
                .ForMember(x => x.SectorPlantTreatmentSectorName, opt => opt.MapFrom(e => e.EmitterUser.JobSectorPlant.SectorPlant.Sector.Name))
                .ForMember(x => x.SectorPlantTreatmentName, opt => opt.MapFrom(e => e.EmitterUser.JobSectorPlant.SectorPlant.Sector.Name +" - "+ e.EmitterUser.JobSectorPlant.SectorPlant.Plant.Name))
                .ForMember(x => x.SectorPlantLocationSectorName, opt => opt.MapFrom(e => e.SectorPlantLocation.Sector.Name))
                .ForMember(x => x.ResponsibleUserFullName, opt => opt.MapFrom(e => e.ResponsibleUser.Surname + ", " + e.ResponsibleUser.FirstName));
            CreateMap<Audits, AuditWorkflowData>()
                .ForMember(x => x.AuditTypeName, opt => opt.MapFrom(e => e.AuditType.Name))
                .ForMember(x => x.SectorName, opt => opt.MapFrom(e => e.SectorPlant.Sector.Name))
                .ForMember(x => x.AuditorFullName, opt => opt.MapFrom(e => e.Auditor.Surname + ", " + e.Auditor.FirstName))
                .ForMember(x => x.AuditStandards, opt => opt.MapFrom(e => e.AuditStandards));
            CreateMap<AuditStandardAspect, AuditStandardAspects>();
            CreateMap<CorrectiveActions, CorrectiveActionWorkflowData>()
                .ForMember(x => x.CorrectiveActionStateName, opt => opt.MapFrom(e => e.CorrectiveActionState.Name))
                .ForMember(x => x.SectorTreatmentName, opt => opt.MapFrom(e => e.SectorPlantTreatment.Sector.Name))
                .ForMember(x => x.ResponsibleUserFullName, opt => opt.MapFrom(e => e.ResponisbleUser.Surname + ", " + e.ResponisbleUser.FirstName))
                .ForMember(x => x.ReviewerUserFullName, opt => opt.MapFrom(e => e.ReviewerUser.Surname + ", " + e.ReviewerUser.FirstName));
            CreateMap<Tasks, TaskWorkflowData>()
                .ForMember(x => x.ResponsibleUserFullName, opt => opt.MapFrom(e => e.ResponsibleUser.Surname + ", " + e.ResponsibleUser.FirstName));
        }
    }
}
