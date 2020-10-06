using AutoMapper;
using Hoshin.Core.Application.Exceptions.Common;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.Role;
using Hoshin.Core.Application.UseCases.Role.AddRole;
using Hoshin.Core.Domain.Role;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Hoshin.Core.UseCases.Tests.Roles
{
    public class AddRoleTest
    {
        [Fact]
        public async void AddNewRoleTest()
        {
            //Arrange
            var claims = new string[] { "Claim1", "Claim2" };
            var role = new Role() { Id ="id"};
            var mockRoleRepository = new Mock<IRoleRepository>();
            var mockMapper = new Mock<IMapper>();

            var roleOutput = Mock.Of<RoleOutput>();
            roleOutput.Id = "id";

            mockMapper.Setup(m => m.Map<Domain.Role.Role, RoleOutput>(It.IsAny<Role>())).Returns(roleOutput);
            mockRoleRepository.Setup(m => m.Add(It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(role);

            var useCase = new AddRoleUseCase(mockRoleRepository.Object, mockMapper.Object);

            //Act
            var result = await useCase.Execute("Test", claims.ToList(), true, true);

            //Assert
            Assert.NotNull(result.Id);
        }

        [Fact]
        public async void AddDuplicatedRoleTest()
        {
            //Arrange
            var claims = new string[] { "Claim1", "Claim2" };
            var mockRoleRepository = new Mock<IRoleRepository>();
            var mockMapper = new Mock<IMapper>();

            var roleOutput = Mock.Of<RoleOutput>();
            roleOutput.Id = "id";

            mockMapper.Setup(m => m.Map<Domain.Role.Role, RoleOutput>(It.IsAny<Role>())).Returns(roleOutput);
            mockRoleRepository.Setup(m => m.Add(It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync((Role)null);

            var useCase = new AddRoleUseCase(mockRoleRepository.Object, mockMapper.Object);

            //Act
            //Assert
            await Assert.ThrowsAsync<DuplicateEntityException>(() => useCase.Execute("Test", claims.ToList(), true, true));
        }
    }
}
