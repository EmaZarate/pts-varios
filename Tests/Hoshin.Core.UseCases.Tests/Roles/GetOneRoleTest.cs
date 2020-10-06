using AutoMapper;
using Hoshin.Core.Application.Exceptions.Common;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.Role;
using Hoshin.Core.Application.UseCases.Role.GetRole;
using Hoshin.Core.Domain.Role;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Core.UseCases.Tests.Roles
{
    public class GetOneRoleTest
    {
        [Fact]
        public async void GetOneExistingRole()
        {
            //Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRoleRepository = new Mock<IRoleRepository>();
            mockRoleRepository.Setup(m => m.GetOne(It.IsAny<string>())).ReturnsAsync(Mock.Of<Role>);
            mockMapper.Setup(m => m.Map<Role, RoleOutput>(It.IsAny<Role>())).Returns(new RoleOutput());

            var useCase = new GetOneRoleUseCase(mockRoleRepository.Object, mockMapper.Object);
            //Act
            var res =  await useCase.Execute("id");

            //Assert
            Assert.IsType<RoleOutput>(res);
        }

        [Fact]
        public async void GetOneNotExistingRole()
        {
            //Arrange
            var mockMapper = new Mock<IMapper>();
            var mockRoleRepository = new Mock<IRoleRepository>();
            mockRoleRepository.Setup(m => m.GetOne(It.IsAny<string>())).ReturnsAsync((Role)null);
            mockMapper.Setup(m => m.Map<Role, RoleOutput>(It.IsAny<Role>())).Returns(new RoleOutput());

            var useCase = new GetOneRoleUseCase(mockRoleRepository.Object, mockMapper.Object);
            //Act
            //Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(() => useCase.Execute("id"));
        }
    }
}
