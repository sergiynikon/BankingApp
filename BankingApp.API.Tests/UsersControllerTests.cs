using System;
using System.Collections.Generic;
using BankingApp.API.Controllers;
using BankingApp.Data.Entities;
using BankingApp.DataTransfer;
using BankingApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BankingApp.API.Tests
{
    public class UsersControllerTests
    {
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            var mock = new Mock<IUserService>();
            mock.Setup(service => service.GetAllUsers()).Returns(GetTestUserViewModelsDto());
            
            _controller = new UsersController(mock.Object);

        }

        #region Tests
        [Fact]
        public void GetAllUsers_ReturnsOkObjectResult()
        {
            //Act
            var result = _controller.GetAllUsers();

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAllUsers_ReturnsCorrectNumberOfUsers()
        {
            //Act
            var okResult = _controller.GetAllUsers() as OkObjectResult;
            var users = okResult.Value as List<UserViewModelDto>;

            //Assert
            Assert.NotNull(okResult);

            Assert.NotNull(users);

            Assert.Equal(2, users.Count);
        }
        #endregion

        #region TestData
        private IEnumerable<UserViewModelDto> GetTestUserViewModelsDto()
        {
            var testUserViewModelsDto = new List<UserViewModelDto>();
            foreach (var testUser in GetTestUsers())
            {
                testUserViewModelsDto.Add(UserViewModelDto.ConvertFromUser(testUser));
            }

            return testUserViewModelsDto;
        }

        private IEnumerable<User> GetTestUsers()
        {
            var testUsers = new List<User>
            {
                new User
                {
                    Id = new Guid("a0b63840-450d-4181-ab3a-910c75497ccc"),
                    Login = "admin",
                    Email = "admin@gmail.com",
                    Password = "abc123",
                    Balance = 10000,
                    ReceivedTransactions = new List<Transaction>(),
                    SentTransactions = new List<Transaction>(),
                    RowVersion = new byte[0]
                },
                new User
                {
                    Id = new Guid("933c9063-fe3a-4667-a0a6-a05842d21370"),
                    Login = "user1",
                    Email = "user1@gmail.com",
                    Password = "abc123",
                    Balance = 100,
                    ReceivedTransactions = new List<Transaction>(),
                    SentTransactions = new List<Transaction>(),
                    RowVersion = new byte[0]
                }
            };
            return testUsers;
        }
        #endregion
    }
}