using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Application.UseCases.Audit.CreateAudit;
using Hoshin.CrossCutting.WorkflowCore.Audit.Data;
using Hoshin.CrossCutting.WorkflowCore.Implementations;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Threading.Tasks;

namespace Hoshin.Quality.UseCases.Test.Audits
{
    public class CreateAuditsTest
    {
        //[Fact]
        //public void ExecuteWithNewAuditStateTest()
        //{
        //    //Arrange
        //    var newAudit = new AuditWorkflowData();
        //    newAudit.AuditID = 1;

        //    var mockWorkFlowCore = new Mock<CrossCutting.WorkflowCore.Interfaces.IWorkflowCore>();
        //    var mockMapper = new Mock<IMapper>();
        //    mockWorkFlowCore.Setup(e => e.StartFlow(It.IsAny<AuditWorkflowData>())).Returns(Task.FromResult("1"));

        //    CreateAuditUseCase useCase = new CreateAuditUseCase(mockWorkFlowCore.Object, mockMapper.Object);

        //    //Act
        //    var res = useCase.Execute(newAudit);

        //    //Assert
        //    Assert.IsType<AuditWorkflowData>(res);
        //}
    }
}
