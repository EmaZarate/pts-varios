using AutoMapper;
using Hoshin.CrossCutting.WorkflowCore.Audit.Data;
using Hoshin.CrossCutting.WorkflowCore.CorrectiveAction.Data;
using Hoshin.CrossCutting.WorkflowCore.Finding.Data;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.ApproveFinding;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.ApproveRejectAudit;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.ApproveRejectReportAudit;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CloseFinding;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CreateFinding;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDAudit;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CRUDCorrectiveAction;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.GenerateCorrectiveAction;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.ReassignFinding;
using Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.UpdateApprovedFinding;

namespace Hoshin.WebApi.Mapper
{
    public class DTOToWorkflowData : Profile
    {
        public DTOToWorkflowData()
        {
            CreateMap<CreateFindingDTO, FindingWorkflowData>();
            CreateMap<ApproveFindingDTO, FindingWorkflowData>();
            CreateMap<UpdateApprovedFindingDTO, FindingWorkflowData>();
            CreateMap<CloseFindingDTO, FindingWorkflowData>();
            CreateMap<ReassignmentsFindingHistoryDTO, FindingWorkflowData>()
                .ForMember(e => e.ResponsibleUserID, opt => opt.MapFrom(e => e.LastResponsibleUserID));
            CreateMap<AuditDTO, AuditWorkflowData>()
                 .ForMember(e => e.AuditStandardAspects, opt => opt.MapFrom(e => e.AuditStandardAspect));
            CreateMap<ApproveRejectAuditDTO, AuditWorkflowData>();
            CreateMap<ApproveRejectReportDTO, AuditWorkflowData>();
            CreateMap<CorrectiveActionDTO, CorrectiveActionWorkflowData>()
                 .ForMember(e =>  e.FindingID, opt => opt.MapFrom(e => e.RelatedFindingId));
            CreateMap<ActionPlanDTO, CorrectiveActionWorkflowData>();
        }
    }
}
