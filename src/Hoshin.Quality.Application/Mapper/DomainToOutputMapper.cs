using AutoMapper;
using Hoshin.Core.Application.UseCases.User.GetAllUser;
using Hoshin.Core.Domain.Users;
using Hoshin.Quality.Application.UseCases.AspectStates;
using Hoshin.Quality.Application.UseCases.CorrectiveAction;
using Hoshin.Quality.Application.UseCases.CorrectiveActionStates;
using Hoshin.Quality.Application.UseCases.Finding;
using Hoshin.Quality.Application.UseCases.ReassignFinding;
using Hoshin.Quality.Application.UseCases.Tasks;
using Hoshin.Quality.Application.UseCases.TaskState;
using Hoshin.Quality.Domain.AspectStates;
using Hoshin.Quality.Domain.CorrectiveAction;
using Hoshin.Quality.Domain.CorrectiveActionState;
using Hoshin.Quality.Domain.Finding;
using Hoshin.Quality.Domain.ReassignmentsFindingHistory;
using Hoshin.Quality.Domain.Task;
using Hoshin.Quality.Domain.TaskState;
using System;

namespace Hoshin.Quality.Application.Mapper
{
    public class DomainToOutputMapper : Profile
    {
        public DomainToOutputMapper()
        {
            CreateMap<AspectStates, AspectStatesOutput>()
                .ForMember(e => e.ID, opt => opt.MapFrom(e => e.AspectStateID));
            CreateMap<Finding, FindingOutput>()
                .ForMember(e => e.PlantEmitterID, opt => opt.MapFrom(e => e.EmitterUser.PlantID))
                .ForMember(e => e.SectorEmitterID, opt => opt.MapFrom(e => e.EmitterUser.SectorID));
            CreateMap<FindingComment, FindingComment>()
                .ForMember(e => e.Date, opt => opt.MapFrom(e =>new DateTime(e.Date.Year, e.Date.Month, e.Date.Day)));
            CreateMap<ReassignmentsFindingHistory, ReassignmentsFindingHistoryOutput>()
                .ForMember(e => e.Id, opt => opt.MapFrom(e => e.FindingReassignmentHistoryID));
            CreateMap<CorrectiveActionState, CorrectiveActionStateOutput>()
                .ForMember(e => e.ID, opt => opt.MapFrom(e => e.CorrectiveActionStateID));
            CreateMap<CorrectiveAction, CorrectiveActionOutput>()
                .ForMember(e => e.CorrectiveActionFishbone, opt => opt.MapFrom(e => e.CorrectiveActionFishbones));
            CreateMap<Task, TaskOutput>()
                .ForMember(e => e.Firstname, opt => opt.MapFrom(e => e.ResponsibleUser.FirstName))
                .ForMember(e => e.Surname, opt => opt.MapFrom(e => e.ResponsibleUser.LastName));
            CreateMap<User, UserOutput>();
            CreateMap<TaskState, TaskStateOutput>();
        }
    }
}
