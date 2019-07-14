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
    public class BankingControllerTests
    {
        private readonly Mock<IBankingLogicService> _bankingLogicServiceMock = new Mock<IBankingLogicService>();

        private readonly OperationModelDto _validModel = new OperationModelDto
        {
            ReceiverUserId = Guid.Parse("407e397c-7dc8-4c39-8b50-0e6623f4e38e"),
            Amount = 1000
        };

        private readonly OperationModelDto _invalidModel = new OperationModelDto
        {
            ReceiverUserId = null,
            Amount = 0
        };

        private readonly ResultDto _validResult = ResultDto.Success();
        private readonly ResultDto _invalidResult = ResultDto.Error();

        public BankingControllerTests()
        {
            _bankingLogicServiceMock.Setup(service => 
                    service.Deposit(It.Is<Guid>(id => id == GetTestUserId()), It.IsAny<OperationModelDto>()))
                .Returns(_invalidResult);

            _bankingLogicServiceMock.Setup(service =>
                    service.Deposit(It.IsAny<Guid>(), It.Is<OperationModelDto>(model => model.Equals(_validModel))))
                .Returns(_invalidResult);

            _bankingLogicServiceMock.Setup(service => 
                    service.Deposit(It.Is<Guid>(id => id.Equals(GetTestUserId())), It.Is<OperationModelDto>(model => model.Equals(_validModel))))
                .Returns(_validResult);

            _bankingLogicServiceMock.Setup(service =>
                    service.Withdraw(It.Is<Guid>(id => id == GetTestUserId()), It.IsAny<OperationModelDto>()))
                .Returns(_invalidResult);

            _bankingLogicServiceMock.Setup(service =>
                    service.Withdraw(It.IsAny<Guid>(), It.Is<OperationModelDto>(model => model.Equals(_validModel))))
                .Returns(_invalidResult);

            _bankingLogicServiceMock.Setup(service => 
                    service.Withdraw(It.Is<Guid>(id => id == GetTestUserId()), It.Is<OperationModelDto>(model => model.Equals(_validModel))))
                .Returns(_validResult);

            _bankingLogicServiceMock.Setup(service => 
                    service.Transfer(It.Is<Guid>(id => id == GetTestUserId()), It.IsAny<OperationModelDto>()))
                .Returns(_invalidResult);

            _bankingLogicServiceMock.Setup(service =>
                    service.Transfer(It.IsAny<Guid>(), It.Is<OperationModelDto>(model => model.Equals(_validModel))))
                .Returns(_invalidResult);

            _bankingLogicServiceMock.Setup(service => service.Transfer(
                    It.Is<Guid>(id => id == GetTestUserId()), It.Is<OperationModelDto>(model => model.Equals(_validModel))))
                .Returns(_validResult);
        }

        #region WithdrawTests
        [Fact]
        public void Deposit_WhenValidModelState_ReturnsOk()
        {
            //Arrange
            var controller = GetBankingLogicController();

            //Act
            var result = controller.Deposit(_validModel);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Deposit_WhenInvalidModelState_ReturnsOk()
        {
            //Arrange
            var controller = GetBankingLogicController();

            //Act
            var result = controller.Deposit(_invalidModel);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Withdraw_WhenValidModelState_ReturnsOk()
        {
            //Arrange
            var controller = GetBankingLogicController();

            //Act
            var result = controller.Withdraw(_validModel);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Withdraw_WhenInvalidModelState_ReturnsOk()
        {
            //Arrange
            var controller = GetBankingLogicController();

            //Act
            var result = controller.Withdraw(_invalidModel);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Transfer_WhenValidModelState_ReturnsOk()
        {
            //Arrange
            var controller = GetBankingLogicController();

            //Act
            var result = controller.Transfer(_validModel);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Transfer_WhenInvalidModelState_ReturnsOk()
        {
            //Arrange
            var controller = GetBankingLogicController();

            //Act
            var result = controller.Transfer(_invalidModel);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region TestData
        private Guid GetTestUserId()
        {
            return Guid.Parse("22b52dbd-75f6-4145-9050-6141618321ec");
        }

        private BankingLogicController GetBankingLogicController()
        {
            var controller = new BankingLogicController(_bankingLogicServiceMock.Object);

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