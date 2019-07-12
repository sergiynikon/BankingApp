using BankingApp.API.Controllers;
using BankingApp.DataTransfer;
using BankingApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Moq;
using Xunit;

namespace BankingApp.API.Tests
{
    public class AuthenticateControllerTests
    {
        private readonly Mock<IAuthenticateService> _authenticateServiceMock = new Mock<IAuthenticateService>();

        private readonly LoginDto _validLoginModel = new LoginDto
        {
            Login = "TestUserLogin1", Password = "TestUserPassword1"
        };

        private readonly LoginDto _invalidLoginModel = new LoginDto
        {
            Login = "", Password = ""
        };

        private readonly RegisterDto _validRegisterDto = new RegisterDto
        {
            Login = "TestUserLogin1", Email = "TestUserLoin1@gmail.com", Password = "abc123", ConfirmPassword = "abc123"
        };

        private readonly RegisterDto _invalidRegisterDto = new RegisterDto
        {
            Login = "", Email = "", Password = "", ConfirmPassword = ""
        };

        private readonly ResultDto _validResultDto = ResultDto.Success();

        private readonly ResultDto _invalidResultDto = ResultDto.Error();

        public AuthenticateControllerTests()
        {
            _authenticateServiceMock.Setup(service => 
                    service.GetIdentityToken(It.IsAny<LoginDto>()))
                .Returns(_invalidResultDto);

            _authenticateServiceMock.Setup(service => 
                    service.GetIdentityToken(It.Is<LoginDto>(model => model.Equals(_validLoginModel))))
                .Returns(_validResultDto);

            _authenticateServiceMock.Setup(service => 
                    service.RegisterUser(It.IsAny<RegisterDto>()))
                .Returns(_invalidResultDto);

            _authenticateServiceMock.Setup(service =>
                    service.RegisterUser(It.Is<RegisterDto>(m => m.Equals(_validRegisterDto))))
                .Returns(_validResultDto);
        }

        #region LoginTests
        [Fact]
        public void Login_WhenValidLoginDto_ReturnsOk()
        {
            //Arrange
            var controller = GetController();

            //Act
            var result = controller.Login(_validLoginModel);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Login_WhenInvalidLoginDto_ReturnsBadRequest()
        {
            //Arrange
            var controller = GetController();

            //Act
            var result = controller.Login(_invalidLoginModel);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Login_WhenModelStateIsValid_ReturnsOk()
        {
            //Arrange
            var controller = GetController();

            //Act
            var result = controller.Login(_validLoginModel);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Login_WhenModelStateIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            var controller = GetController(addModelError: true);

            //Act
            var result = controller.Login(_validLoginModel);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Login_WhenNotAuthorized_NotReturnUnAuthorized()
        {
            //Arrange
            var controller = GetController();

            //Act
            var result = controller.Login(_validLoginModel);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region RegisterTests
        [Fact]
        public void Register_WhenModelStateIsValid_ReturnsOk()
        {
            //Arrange
            var controller = GetController();

            //Act
            var result = controller.Register(_validRegisterDto);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Register_WhenModelStateIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            var controller = GetController(addModelError: true);

            //Act
            var result = controller.Register(_validRegisterDto);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Register_WhenValidRegisterDto_ReturnsOk()
        {
            //Arrange
            var controller = GetController();

            //Act
            var result = controller.Register(_validRegisterDto);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Register_WhenInvalidRegisterDto_ReturnsBadRequest()
        {
            //Arrange
            var controller = GetController();

            //Act
            var result = controller.Register(_invalidRegisterDto);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region TestData
        private AuthenticateController GetController(bool addModelError = false)
        {
            var controller = new AuthenticateController(_authenticateServiceMock.Object);

            if (addModelError)
            {
                controller.ModelState.AddModelError("someKey", "someError");
            }

            return controller;
        }
        #endregion
    }
}
