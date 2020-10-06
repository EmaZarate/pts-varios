using AutoMapper;
using Hoshin.Core.Application.Exceptions.Common;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.Role;
using Hoshin.Core.Application.UseCases.Role.CheckIfBasicExists;
using Hoshin.Core.Domain.Role;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Hoshin.Core.UseCases.Tests.Roles
{
    public class GetBasicTest
    {
        [Fact]
        public async void CheckBasicRoleWithExistingBasicRole()
        {
            //Arrange
            var mockRoleRepository = new Mock<IRoleRepository>();
            mockRoleRepository.Setup(m => m.CheckIfBasicExists()).ReturnsAsync(true);

            var useCase = new CheckIfBasicExistsUseCase(mockRoleRepository.Object);
            //Act
            var res = await useCase.Execute();

            //Assert
            Assert.True(res);
        }

        [Fact]
        public async void CheckBasicRoleWithNotExistingBasicRole()
        {
            //Arrange
            var mockRoleRepository = new Mock<IRoleRepository>();
            mockRoleRepository.Setup(m => m.CheckIfBasicExists()).ReturnsAsync(false);

            var useCase = new CheckIfBasicExistsUseCase(mockRoleRepository.Object);
            //Act
            var res = await useCase.Execute();

            //Assert
            Assert.False(res);
        }
    }
}
