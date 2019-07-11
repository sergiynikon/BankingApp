using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using BankingApp.API.Controllers;
using BankingApp.DataTransfer;
using BankingApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BankingApp.API.Tests
{
    public class UsersControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock = new Mock<IUserService>();
        public UsersControllerTests()
        {
            _userServiceMock.Setup(service => service.GetAllUsers()).Returns(GetTestUsers());
            _userServiceMock.Setup(service => service.GetUser(GetTestUserId())).Returns(GetTestUsers().First());
        }

        #region Tests
        [Fact]
        public void GetAllUsers_ReturnsOkObjectResult()
        {
            //Arrange
            var controller = GetUserController();

            //Act
            var result = controller.GetAllUsers();

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAllUsers_ReturnsCorrectNumberOfUsers()
        {
            //Arrange
            var controller = GetUserController();

            //Act
            var okResult = controller.GetAllUsers() as OkObjectResult;
            var users = okResult?.Value as List<UserViewModelDto>;

            //Assert
            Assert.NotNull(okResult);

            Assert.NotNull(users);

            Assert.Equal(3, users.Count);
        }

        [Fact]
        public void GetCurrentUser_ReturnsOkObjectResult()
        {
            //Arrange
            var controller = GetUserController();

            //Act
            var result = controller.GetCurrentUser();

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetCurrentUser_ReturnsUser()
        {
            //Arrange
            var controller = GetUserController();

            //Act
            var result = controller.GetCurrentUser();
            var okResult = result as OkObjectResult;
            var user = okResult?.Value as UserViewModelDto;

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(okResult);
            Assert.NotNull(user);
            Assert.Equal("testUser1", user.Login);
        }
        #endregion

        #region TestData
        private IEnumerable<UserViewModelDto> GetTestUsers()
        {
            var testUsers = new List<UserViewModelDto>
            {
                new UserViewModelDto
                {
                    Balance = 100000,
                    Email = "testUser1@email.com",
                    Login = "testUser1"
                },
                new UserViewModelDto
                {
                    Balance = 200000,
                    Email = "testUser2@email.com",
                    Login = "testUser2"
                },
                new UserViewModelDto
                {
                    Balance = 300000,
                    Email = "testUser3@email.com",
                    Login = "testUser3"
                }
            };

            return testUsers;
        }

        private Guid GetTestUserId()
        {
            return Guid.Parse("6410e9f4-0b44-4407-badc-cc5461a2da27");
        }

        private UsersController GetUserController()
        {
            var controller = new UsersController(_userServiceMock.Object);

            var userInClaimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, GetTestUserId().ToString())
            }));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userInClaimsPrincipal }
            };

            return controller;
        }
        #endregion
    }
}