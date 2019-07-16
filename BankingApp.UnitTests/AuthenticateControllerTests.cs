using BankingApp.API.Controllers;
using BankingApp.DataTransfer;
using BankingApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BankingApp.UnitTests
{
    public class AuthenticateControllerTests
    {
        private static readonly string _token = "token";

        private readonly Mock<IAuthenticateService> _authenticateServiceMock = new Mock<IAuthenticateService>();

        private readonly LoginDto _invalidLoginModel = new LoginDto();

        private readonly LoginDto _correctLoginModel = new LoginDto
        {
            Login = "TestUserLogin1",
            Password = "TestUserPassword1"
        };

        private readonly LoginDto _incorrectLoginModel = new LoginDto
        {
            Login = "TestUserLogin2", Password = "TestUserPassword2"
        };

        private readonly RegisterDto _invalidRegisterDto = new RegisterDto();

        private readonly RegisterDto _correctRegisterDto = new RegisterDto
        {
            Login = "TestUserLogin1", Email = "TestUserLoin1@gmail.com", Password = "abc123", ConfirmPassword = "abc123"
        };

        private readonly RegisterDto _incorrectRegisterDto = new RegisterDto
        {
            Login = "TestUserLogin2", Email = "TestUserLogin2@gmail.com", Password = "abc12", ConfirmPassword = "abc12"
        };

        private readonly ResultDto _validResultDto = ResultDto.Success(
            new
            {
                encodedJwt = _token
            });

        private readonly ResultDto _invalidResultDto = ResultDto.Error();

        public AuthenticateControllerTests()
        {
            _authenticateServiceMock.Setup(service =>
                    service.GetIdentityToken(It.Is<LoginDto>(model => model.Equals(_correctLoginModel))))
                .Returns(_validResultDto);

            _authenticateServiceMock.Setup(service =>
                    service.GetIdentityToken(It.Is<LoginDto>(model => model.Equals(_incorrectLoginModel))))
                .Returns(_invalidResultDto);

            _authenticateServiceMock.Setup(service =>
                    service.RegisterUser(It.Is<RegisterDto>(m => m.Equals(_correctRegisterDto))))
                .Returns(_validResultDto);

            _authenticateServiceMock.Setup(service =>
                    service.RegisterUser(It.Is<RegisterDto>(m => m.Equals(_incorrectRegisterDto))))
                .Returns(_invalidResultDto);
        }

        #region LoginTests

        [Fact]
        public void Login_WhenModelStateIsValidAndModelIsCorrect_ReturnsOk()
        {
            //Arrange
            var controller = GetAuthenticateController();

            //Act
            var result = controller.Login(_correctLoginModel);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Login_WhenModelStateIsValidAndModelIsIncorrect_ReturnsBadRequest()
        {
            //Arrange
            var controller = GetAuthenticateController();

            //Act
            var result = controller.Login(_incorrectLoginModel);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Login_WhenCorrectLoginDto_ReturnsTokenInResultDto()
        {
            //Arrange
            var controller = GetAuthenticateController();

            //Act
            var result = controller.Login(_correctLoginModel);
            var okResult = result as OkObjectResult;
            var resultDto = okResult?.Value;

            //Assert
            Assert.Equal(_validResultDto, resultDto);
        }

        #endregion

        #region RegisterTests

        [Fact]
        public void Register_WhenModelStateIsValidAndModelIsCorrect_ReturnsOk()
        {
            //Arrange
            var controller = GetAuthenticateController();

            //Act
            var result = controller.Register(_correctRegisterDto);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Register_WhenModelStateIsValidAndModelIsIncorrect_ReturnsBadRequest()
        {
            //Arrange
            var controller = GetAuthenticateController();

            //Act
            var result = controller.Register(_incorrectRegisterDto);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion

        #region TestData
        private AuthenticateController GetAuthenticateController()
        {
            return new AuthenticateController(_authenticateServiceMock.Object);
        }

        #endregion
    }
}
