using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.LoginUser;
using Hoshin.Core.Domain.Interfaces;
using Hoshin.CrossCutting.JWT;
using Hoshin.CrossCutting.MicrosoftGraph.DTO.User;
using Hoshin.CrossCutting.MicrosoftGraph.Services.Interfaces;
using Hoshin.CrossCutting.Storage;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hoshin.Core.UseCases.Tests.LoginUser
{
    public class LoginUserMicrosoftGraphTests
    {
        [Fact]
        public async void ExecuteWithValidAccessTokenTest()
        {
            //Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var mockJwtService = new Mock<IJwtService>();
            var mockStorage = new Mock<IStorage>();
            var mockUserService = new Mock<IUserService>();
            var mockUser = new Mock<Domain.Interfaces.IEntity>();

            mockUserService.Setup(b => b.GetAccessToken(It.IsNotNull<string>())).ReturnsAsync(new MicrosoftGraphAppAccessToken() { AccessToken = "ASD"});
            mockUserService.Setup(b => b.GetMe(It.IsNotNull<string>())).ReturnsAsync(new MicrosoftGraphUserData() { Mail = "ASD"});
            mockUserRepository.Setup(b => b.FindByEmail(It.IsNotNull<string>())).ReturnsAsync(new Domain.Users.User("asd", "asd", true));
            mockJwtService.Setup(b => b.GenerateJWT(It.IsNotNull<string>(), It.IsNotNull<string>(), It.IsNotNull<int>(), It.IsNotNull<int>(), It.IsNotNull<int>())).ReturnsAsync("jwt");

            var useCase = new LoginUserUseCase(mockUserRepository.Object, mockJwtService.Object, mockUserService.Object, mockStorage.Object);
            //Act
            var res = await useCase.Execute("token");

            //Assert
            Assert.IsType<UserOutput>(res);
        }
    }
}
