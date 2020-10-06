using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.ReassignmentsFindingHistory;
using System.Linq;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class ReassignmentsFindingHistoryRepository : IReassignmentsFindingHistoryRepository, Hoshin.CrossCutting.WorkflowCore.Repositories.IReassignmentsFindingHistoryRepository
    {
        private readonly IServiceProvider _serviceProvider;
        public ReassignmentsFindingHistoryRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public ReassignmentsFindingHistory Add(ReassignmentsFindingHistory newRequestReassign)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                newRequestReassign.Date = DateTime.Today;
                FindingsReassignmentsHistory findingReassignmentsHistory = _mapper.Map<ReassignmentsFindingHistory, FindingsReassignmentsHistory>(newRequestReassign);

                _ctx.FindingsReassignmentsHistories.Add(findingReassignmentsHistory);

                var finding = _ctx.Findings.FirstOrDefault(x => x.FindingID == findingReassignmentsHistory.FindingID);

                FindingsStatesHistory findingsStateHistory = new FindingsStatesHistory();
                findingsStateHistory.FindingID = finding.FindingID;
                findingsStateHistory.Date = DateTime.Now;
                findingsStateHistory.FindingStateID = finding.FindingStateID;
                findingsStateHistory.CreatedByUserID = findingReassignmentsHistory.CreatedByUserID;
                _ctx.FindingsStatesHistories.Add(findingsStateHistory);

                finding.FindingStateID = 16;
                _ctx.Findings.Update(finding);


                    _ctx.FindingsReassignmentsHistories.Add(findingReassignmentsHistory);
                    _ctx.SaveChanges();

                    return _mapper.Map<FindingsReassignmentsHistory, ReassignmentsFindingHistory>(findingReassignmentsHistory);             
            }
        }

        public bool Add(int findingID, string reassignedUserId, string createByUserId, string state, string rejectComment)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                FindingsReassignmentsHistory f = new FindingsReassignmentsHistory()
                {
                    CreatedByUserID = createByUserId,
                    Date = DateTime.Now,
                    FindingID = findingID,
                    ReassignedUserID = reassignedUserId,
                    State = state,
                    CauseOfReject = rejectComment
                };

                _ctx.FindingsReassignmentsHistories.Add(f);
                _ctx.SaveChanges();

                return true;
            }
        }

        public ReassignmentsFindingHistory GetLast (int id_finding)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _mapper = scope.ServiceProvider.GetService(typeof(IMapper)) as IMapper;
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;

                var res = _ctx.FindingsReassignmentsHistories.Where(x => x.FindingID == id_finding).OrderByDescending(x => x.Date).FirstOrDefault();
                return _mapper.Map<FindingsReassignmentsHistory, ReassignmentsFindingHistory>(res); 
            }
        }


    }
}
