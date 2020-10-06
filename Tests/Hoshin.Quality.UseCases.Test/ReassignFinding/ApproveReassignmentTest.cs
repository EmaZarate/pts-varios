using Hoshin.Quality.Application.Repositories;
using Moq;
using System;
using Xunit;
using Hoshin.Quality.Application.UseCases.ReassignFinding.RequestReassign;
using AutoMapper;
using Hoshin.Quality.Application.UseCases.ReassignFinding;
using Hoshin.Quality.Domain.ReassignmentsFindingHistory;

namespace Hoshin.Quality.UseCases.Test.ReassignFinding
{
    public class ApproveReassignmentTest
    {
        [Fact]
        public void ExecuteNewRequestReassing()
        {
            //Arrenge

            //var ReassingOutput = new ReassignmentsFindingHistoryOutput();
            //ReassingOutput.CreatedByUserID = 1;
            //ReassingOutput.Date = DateTime.Today;
            //ReassingOutput.FindingID = 1;
            //ReassingOutput.Id = 1;
            //ReassingOutput.ReassignedUserID = 2;
            //ReassingOutput.State = "Pendiente";

            //var reassingDomain = new ReassignmentsFindingHistory(1, 2, 3, "Pendiente");

            //var mockReassignmentFindingHistoryRepository = new Mock<IReassignmentsFindingHistoryRepository>();
            //var mockMapper = new Mock<IMapper>();

            //mockReassignmentFindingHistoryRepository.Setup(r => r.Update(It.IsNotNull<ReassignmentsFindingHistory>())).Returns(reassingDomain);
            //mockMapper.Setup(e => e.Map<ReassignmentsFindingHistory, ReassignmentsFindingHistoryOutput>(It.IsAny<ReassignmentsFindingHistory>())).Returns(ReassingOutput);
            //RequestReassignUseCase useCase = new RequestReassignUseCase(mockReassignmentFindingHistoryRepository.Object, mockMapper.Object);

            ////Action
            //var res = useCase.Execute(1, 2, 1, "State");

            ////Assert

            //Assert.IsType<ReassignmentsFindingHistoryOutput>(res);
        }
    }
}
