using System;
using BankingApp.Data.Entities;
using BankingApp.Data.UnitOfWork;
using BankingApp.DataTransfer;
using BankingApp.Services.Implementation;
using BankingApp.Services.Interfaces;
using Moq;
using Xunit;

namespace BankingApp.Services.Tests
{
    public class AuthenticateServiceTests
    {
        private readonly Mock<IUnitOfWork> _userServiceMock = new Mock<IUnitOfWork>();

        private readonly RegisterDto _correctRegisterDto = new RegisterDto
        {
            Login = "testUser1",
            Email = "testUser1@gmail.com",
            Password = "testUser1Pass",
            ConfirmPassword = "testUser1Pass"
        };

        private readonly RegisterDto _loginExistsRegisterDto = new RegisterDto
        {
            Login = "existingLogin",
            Email = "abc@gmail.com",
            Password = "ex",
            ConfirmPassword = "ex"
        };

        private readonly RegisterDto _emailExistsRegisterDto = new RegisterDto
        {
            Login = "abc",
            Email = "existingEmail@gmail.com",
            Password = "abc",
            ConfirmPassword = "abc"
        };

        private readonly RegisterDto _notConfirmedPasswordRegisterDto = new RegisterDto
        {
            Login = "aaa",
            Email = "aaa@gmail.com",
            Password = "aaa",
            ConfirmPassword = "bbb"
        };

        private readonly LoginDto _incorrectLoginLoginDto = new LoginDto
        {
            Login = "incorrectLogin",
            Password = "pass"
        };

        private readonly LoginDto _incorrectPasswordLoginDto = new LoginDto
        {
            Login = "testLogin1",
            Password = "incorrectPassword"
        };

        private readonly User _userToAdd = new User("login", "email@gmail.com", "password");

        private readonly User _correctUserFromDb = new User("correctLogin", "correctLogin@gmail.com", "correctPassword");

        public AuthenticateServiceTests()
        {
            _userServiceMock.Setup(u => u.UserRepository.UserLoginExists(_correctRegisterDto.Login))
                .Returns(false);

            _userServiceMock.Setup(u => u.UserRepository.UserLoginExists(_loginExistsRegisterDto.Login))
                .Returns(true);

            _userServiceMock.Setup(u => u.UserRepository.UserEmailExists(_emailExistsRegisterDto.Email))
                .Returns(true);

            _userServiceMock.Setup(u => u.UserRepository.Add(It.IsAny<User>()));

            _userServiceMock.Setup(u => u.UserRepository.GetByLogin(_incorrectLoginLoginDto.Login))
                .Returns((User) null);

            _userServiceMock.Setup(u => u.UserRepository.GetByLogin(_incorrectPasswordLoginDto.Login))
                .Returns(_correctUserFromDb);

            _userServiceMock.Setup(u => u.UserRepository.VerifyPassword(It.IsAny<Guid>(), _incorrectPasswordLoginDto.Password))
                .Returns(false);
        }

        #region RegisterTests

        [Fact]
        public void Register_WhenUserLoginExists_ReturnsErrorResultDto()
        {
            //Arrange
            var service = GetAuthenticateService();

            //Act
            var result = service.RegisterUser(_loginExistsRegisterDto);

            //Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void Register_WhenCorrectRegisterDto_ReturnsSuccessResultDto()
        {
            //Arrange
            var service = GetAuthenticateService();

            //Act
            var result = service.RegisterUser(_correctRegisterDto);

            //Assert
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void Register_WhenUserEmailExists_ReturnsErrorResultDto()
        {
            //Arrange
            var service = GetAuthenticateService();

            //Act
            var result = service.RegisterUser(_emailExistsRegisterDto);

            //Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void Register_WhenPasswordNotConfirmed_ReturnsErrorResultDto()
        {
            //Arrange
            var service = GetAuthenticateService();

            //Act
            var result = service.RegisterUser(_notConfirmedPasswordRegisterDto);

            //Assert
            Assert.False(result.IsSuccess);
        }
        #endregion

        #region GetIdentityTokenTests

        [Fact]
        public void GetIdentityToken_WhenIncorrectLogin_ReturnsSuccessResultDto()
        {
            //Arrange
            var service = GetAuthenticateService();

            //Act
            var result = service.GetIdentityToken(_incorrectLoginLoginDto);

            //Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void GetIdentityToken_WhenIncorrectPassword_ReturnsSuccessResultDto()
        {
            //Arrange
            var service = GetAuthenticateService();

            //Act
            var result = service.GetIdentityToken(_incorrectPasswordLoginDto);

            //Assert
            Assert.False(result.IsSuccess);
        }

        #endregion

        #region TestData

        private IAuthenticateService GetAuthenticateService() => new AuthenticateService(_userServiceMock.Object);

        #endregion
    }
}