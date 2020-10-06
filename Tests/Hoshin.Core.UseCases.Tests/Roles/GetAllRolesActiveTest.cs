using AutoMapper;
using Hoshin.Core.Application.Exceptions.Common;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.Role;
using Hoshin.Core.Application.UseCases.Role.GetAllRolesActive;
using Hoshin.Core.Domain.Role;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Core.UseCases.Tests.Roles
{
    public class GetAllRolesActiveTest
    {
        [Fact]
        public async void GetAllExistingRoleActive()
        {
            //Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRoleRepository = new Mock<IRoleRepository>();
            mockRoleRepository.Setup(m => m.GetAllRolesActive()).ReturnsAsync(Mock.Of<List<Role>>);
            mockMapper.Setup(m => m.Map<List<Role>, List<RoleOutput>>(It.IsAny<List<Role>>())).Returns(new List<RoleOutput>());

            var useCase = new GetAllRolesActiveUseCase(mockRoleRepository.Object, mockMapper.Object);
            //Act
            var res = await useCase.Execute();

            //Assert
            Assert.IsType<List<RoleOutput>>(res);
        }
    }
}
