using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Application.UseCases.LoginUser;
using Hoshin.CrossCutting.JWT;
using Xunit;
using Moq;
using System.Threading.Tasks;
using Hoshin.CrossCutting.Storage;
using Hoshin.CrossCutting.MicrosoftGraph.Services.Interfaces;
using Hoshin.Core.Application.Exceptions.User;

namespace Hoshin.Core.UseCases.Tests.LoginUser
{
    public class LoginUserHoshinTests
    {
        [Fact]
        public async void ExecuteWithCorrectUserTest()
        {
            //Arrange
            Domain.Users.User usRet = new Domain.Users.User("hola", "password", true) { Id = "id" };

            var mockUserRepository = new Mock<IUserRepository>();
            var mockJwtService = new Mock<IJwtService>();
            var mockStorage = new Mock<IStorage>();
            var mockUserService = new Mock<IUserService>();
            

            mockUserRepository.Setup(b => b.CheckUser(It.IsAny<Domain.Users.User>())).ReturnsAsync(usRet);
            mockJwtService.Setup(b => b.GenerateJWT("hola", "id", 1, 1, 1)).Returns(Task.FromResult("jwt"));

            LoginUserUseCase useCase = new LoginUserUseCase(mockUserRepository.Object, mockJwtService.Object, mockUserService.Object ,mockStorage.Object);
            
            //Act
            var result = await useCase.Execute("hola", "password");

            //Assert
            Assert.IsType<UserOutput>(result);
        }
        [Fact]
        public async void ExecuteWithWrongUserTest()
        {
            //Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var mockStorage = new Mock<IStorage>();
            var mockUserService = new Mock<IUserService>();
            var mockJwtService = new Mock<IJwtService>();

            mockUserRepository.Setup(b => b.CheckUser(It.IsAny<Domain.Users.User>())).ReturnsAsync((Domain.Users.User)null);
            
            LoginUserUseCase useCase = new LoginUserUseCase(mockUserRepository.Object, mockJwtService.Object, mockUserService.Object, mockStorage.Object);

            //Act and
            //Assert
            await Assert.ThrowsAsync<UserNotFoundException>(() => useCase.Execute("Hola", "Password"));
        }
    }
}