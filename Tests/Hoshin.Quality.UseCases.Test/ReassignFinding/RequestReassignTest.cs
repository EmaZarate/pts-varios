using Hoshin.Quality.Application.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Hoshin.Quality.Application.UseCases.ReassignFinding.RequestReassign;
using AutoMapper;
using Hoshin.Quality.Application.UseCases.ReassignFinding;
using Hoshin.Quality.Domain.ReassignmentsFindingHistory;
using Microsoft.AspNetCore.Http;
using Hoshin.CrossCutting.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace Hoshin.Quality.UseCases.Test.ReassignFinding
{
    public class RequestReassignTest
    {
        [Fact]
        public void ExecuteNewRequestReassing()
        {
            //Arrenge

            var ReassingOutput = new ReassignmentsFindingHistoryOutput();
            ReassingOutput.CreatedByUserID = "1";
            ReassingOutput.Date = DateTime.Today;
            ReassingOutput.FindingID = 1;
            ReassingOutput.Id = 1;
            ReassingOutput.ReassignedUserID = "2";

            var reassingDomain = new ReassignmentsFindingHistory(1, "2", "3","Pendiente");

            var mockFindingRepository = new Mock<IFindingRepository>();
            var mockReassignmentFindingHistoryRepository = new Mock<IReassignmentsFindingHistoryRepository>();
            var mockWorkflowore = new Mock<Hoshin.CrossCutting.WorkflowCore.Interfaces.IWorkflowCore>();
            var mockHttpContext = new Mock<IHttpContextAccessor>();
            var mockMapper = new Mock<IMapper>();
            var mockHub = new Mock<IHubContext<FindingsHub>>();

            mockReassignmentFindingHistoryRepository.Setup(r => r.Add(It.IsNotNull<ReassignmentsFindingHistory>())).Returns(reassingDomain);

            mockMapper.Setup(e => e.Map<ReassignmentsFindingHistory, ReassignmentsFindingHistoryOutput>(It.IsAny<ReassignmentsFindingHistory>())).Returns(ReassingOutput);
            RequestReassignUseCase useCase = new RequestReassignUseCase(mockFindingRepository.Object, mockReassignmentFindingHistoryRepository.Object, mockWorkflowore.Object, mockMapper.Object, mockHttpContext.Object);

            //Action
            var res = useCase.Execute(1, "2", "1");

            //Assert

            Assert.IsType<ReassignmentsFindingHistoryOutput>(res);
        }
    }
}
