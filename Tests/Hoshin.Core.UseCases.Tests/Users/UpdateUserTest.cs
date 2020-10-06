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
using System.Threading.Tasks;
using Hoshin.Core.Application.UseCases.User.UpdateUser;
using System.Security.Claims;

namespace Hoshin.Core.UseCases.Tests.Users
{
    public class UpdateUserTest
    {
        [Fact]
        public async void UpdateUserReturnUpdatedUserTest()
        {
            //Arrange
            User u = new User("1","userName","password", 1, 1, 1, "name", "surname", "address", "phoneNumber", "base64Profile", true);
            List<string> listRoles = new List<string>();
            listRoles.Add("rol1");
            listRoles.Add("rol2");

            ClaimsIdentity Identity = new ClaimsIdentity(new Claim[]
            {
                new Claim("id", "1"),
                new Claim("http://schemas.microsoft.com/identity/claims/tenantid", "test"),
                new Claim("http://schemas.microsoft.com/identity/claims/objectidentifier", Guid.NewGuid().ToString()),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "test"),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "test"),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn", "test"),
            }, "test");

            ClaimsPrincipal reqUser = new ClaimsPrincipal();
            reqUser.AddIdentity(Identity);

            UserOutput uO = new UserOutput() { Id = "2", Username = "user", Name = "Name", Surname = "surname", PlantID = 1, Plant = "Plant", SectorID = 1, Sector = "Sector", JobID = 1, Job = "job", PhoneNumber = "phone", Address = "address", base64Profile = "base64", Roles = listRoles };

            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            mockUserRepository.Setup(e => e.Update(It.IsAny<User>(), It.IsAny<List<string>>())).ReturnsAsync(u);
            mockMapper.Setup(e => e.Map<User, UserOutput>(It.IsAny<User>())).Returns(uO);

            var useCase = new UpdateUserUseCase(mockUserRepository.Object, mockMapper.Object);

            //Act
            var res = await useCase.Execute(reqUser, "1", "userName", "password", 1, 1, 1, "name", "surname", listRoles, "address", "phoneNumber", "base64Profile", true);

            //Assert
            Assert.IsType<UserOutput>(res);
        }
    }
}
