using System;
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
    public class TransactionsControllerTests
    {
        private readonly Mock<ITransactionsService> _transactionsServiceMock = new Mock<ITransactionsService>();

        private readonly Guid _validUserId = Guid.Parse("a9f9d3ea-8120-483d-b1ec-45c117fab56f");
        private readonly Guid _invalidUserId = Guid.NewGuid();

        private readonly ResultDto _validResultDto = ResultDto.Success();
        private readonly ResultDto _invalidResultDto = ResultDto.Error();
        public TransactionsControllerTests()
        {
            _transactionsServiceMock.Setup(service =>
                    service.GetUserTransactions(GetTestUserId()))
                .Returns(_validResultDto);

            _transactionsServiceMock.Setup(service =>
                    service.GetUserTransactions(_validUserId))
                .Returns(_validResultDto);

            _transactionsServiceMock.Setup(service =>
                    service.GetUserTransactions(_invalidUserId))
                .Returns(_invalidResultDto);
        }

        #region GetUserTransactionsWithNoParametersTest
        [Fact]
        public void GetUserTransactionsWithNoParameters_ReturnsOk()
        {
            //Arrange
            var controller = GetUserController();

            //Act
            var result = controller.GetUserTransactions();

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetUserTransactions_WithValidUserId_ReturnsOk()
        {
            //Arrange
            var controller = GetUserController();

            //Act
            var result = controller.GetUserTransactions(_validUserId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetUserTransactions_WithInvalidUserId_ReturnsNotFound()
        {
            //Arrange
            var controller = GetUserController();

            //Act
            var result = controller.GetUserTransactions();

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
        #endregion

        #region TestData
        private Guid GetTestUserId()
        {
            return Guid.Parse("6410e9f4-0b44-4407-badc-cc5461a2da27");
        }

        private TransactionsController GetUserController()
        {
            var controller = new TransactionsController(_transactionsServiceMock.Object);

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