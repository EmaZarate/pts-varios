using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Hoshin.Core.Application.UseCases.User.GetAllUser;
using Hoshin.Core.Application.UseCases.User.GetOneUser;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Domain.Users;
using AutoMapper;
using Hoshin.Core.Application.Exceptions.User;

namespace Hoshin.Core.UseCases.Tests.Users
{
    public class GetOneUserTest
    {
        [Fact]
        public async void GetOneUserWhenExistsTest()
        {
            //Arrange
            User user = new User("1","username","firstname","lastname","msftgid","email");
            UserOutput userOutput = new UserOutput();

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(e => e.Get(It.IsAny<string>())).Returns(user);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(e => e.Map<User, UserOutput>(It.IsAny<User>())).Returns(userOutput);

            var useCase = new GetOneUserUseCase(mockUserRepository.Object, mockMapper.Object);

            //Act
            var res = await useCase.Execute("1");

            //Assert
            Assert.IsType<UserOutput>(res);
        }

        [Fact]
        public async void GetOneUserWhenNoExistsTest()
        {
            //Arrange
            User user = new User("1", "username", "firstname", "lastname", "msftgid", "email");
            UserOutput userOutput = new UserOutput();

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(e => e.Get(It.IsAny<string>())).Returns<User>(null);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(e => e.Map<User, UserOutput>(It.IsAny<User>())).Returns(userOutput);

            var useCase = new GetOneUserUseCase(mockUserRepository.Object, mockMapper.Object);

            //Act


            //Assert
            await Assert.ThrowsAsync<UserNotFoundByIdException>(() => useCase.Execute("1"));
        }
    }
}
