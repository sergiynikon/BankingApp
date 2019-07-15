using System;
using BankingApp.Data.Entities;
using BankingApp.Data.UnitOfWork;
using BankingApp.DataTransfer;
using BankingApp.DataTransfer.Helpers;
using BankingApp.Services.Implementation;
using BankingApp.Services.Interfaces;
using Moq;
using Xunit;

namespace BankingApp.Services.Tests
{
    public class BankingLogicServiceTests
    {
        private static readonly double SenderUserInitialBalance = 10000000;

        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();

        private readonly Guid _testSenderUserId = Guid.NewGuid();
        private readonly Guid _testReceiverUserId = Guid.NewGuid();



        public BankingLogicServiceTests()
        {
            _unitOfWorkMock.Setup(u => u.UserRepository.GetById(It.IsAny<Guid>()))
                .Returns((User) null);

            _unitOfWorkMock.Setup(u => u.UserRepository.GetById(It.Is<Guid>(id => id == _testSenderUserId)))
                .Returns(GetTestSenderUser());

            _unitOfWorkMock.Setup(u => u.UserRepository.GetById(It.Is<Guid>(id => id == _testReceiverUserId)))
                .Returns(GetTestReceiverUser());

            _unitOfWorkMock.Setup(u => u.TransactionRepository.Add(It.IsAny<Transaction>()));
        }

        #region Tests

        [Theory]
        [InlineData(100)]
        [InlineData(Constants.MaxOperationAmount)]
        [InlineData(0.01)]
        public void Deposit_WithValidOperationModelDto_ReturnsSuccessResultDto(double amount)
        {
            //Arrange
            var service = GetBankingLogicService();

            //Act
            var result = service.Deposit(_testSenderUserId, new OperationModelDto
            {
                Amount = amount
            });

            //Assert
            Assert.True(result.IsSuccess);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Deposit_WithInvalidOperationModelDto_ReturnsErrorResultDto(double amount)
        {
            //Arrange
            var service = GetBankingLogicService();

            //Act
            var result = service.Deposit(_testSenderUserId, new OperationModelDto
            {
                Amount = amount
            });

            //Assert
            Assert.False(result.IsSuccess);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(Constants.MinOperationAmount)]
        [InlineData(Constants.MaxOperationAmount)]
        public void Deposit_WithValidOperationModelDto_ReturnsCorrectResultDto(double amount)
        {
            //Arrange
            var service = GetBankingLogicService();

            //Act
            var result = service.Deposit(_testSenderUserId, new OperationModelDto
            {
                Amount = amount
            });

            double resultAmount = (result.Result as OperationDetailsDto)?.Amount ?? 0;

            //Assert
            Assert.Equal(amount, resultAmount);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Withdraw_WithInvalidOperationModelDto_ReturnsErrorResultDto(double amount)
        {
            //Arrange
            var service = GetBankingLogicService();

            //Act
            var result = service.Withdraw(_testSenderUserId, new OperationModelDto
            {
                Amount = amount
            });

            //Assert
            Assert.False(result.IsSuccess);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        public void Withdraw_WithValidOperationModelDto_ReturnsCorrectResultDto(double amount)
        {
            //Arrange
            var service = GetBankingLogicService();

            //Act
            var result = service.Withdraw(_testSenderUserId, new OperationModelDto
            {
                Amount = amount
            });

            double resultAmount = (result.Result as OperationDetailsDto)?.Amount ?? 0;

            //Assert
            Assert.Equal(amount, resultAmount);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Transfer_WithInvalidOperationModelDto_ReturnsErrorResultDto(double amount)
        {
            //Arrange
            var service = GetBankingLogicService();

            //Act
            var result = service.Transfer(_testSenderUserId, new OperationModelDto
            {
                Amount = amount,
                ReceiverUserId = _testReceiverUserId
            });

            //Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void Transfer_WhenSenderUserBalanceLessThanAmount_ReturnsErrorResultDto()
        {
            //Arrange
            var service = GetBankingLogicService();
            
            //Act
            var result = service.Transfer(_testSenderUserId, new OperationModelDto
            {
                Amount = SenderUserInitialBalance + 0.01,
                ReceiverUserId = _testReceiverUserId
            });

            //Assert
            Assert.False(result.IsSuccess);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(Constants.MinOperationAmount)]
        [InlineData(Constants.MaxOperationAmount)]
        public void Transfer_WithValidOperationModelDto_ReturnsCorrectResultDto(double amount)
        {
            //Arrange
            var service = GetBankingLogicService();

            //Act
            var result = service.Transfer(_testSenderUserId, new OperationModelDto
            {
                Amount = amount,
                ReceiverUserId = _testReceiverUserId
            });

            double resultAmount = (result.Result as OperationDetailsDto)?.Amount ?? 0;

            //Assert
            Assert.Equal(amount, resultAmount);
        }

        [Fact]
        public void Transfer_WithSenderUserIdEqualsReceiverUserId_ReturnsErrorResultDto()
        {
            //Arrange
            var service = GetBankingLogicService();

            //Act
            var result = service.Transfer(_testSenderUserId, new OperationModelDto
            {
                Amount = 100,
                ReceiverUserId = _testSenderUserId
            });

            //Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void Transfer_WithNullReceiverUserId_ReturnsErrorResultDto()
        {
            //Arrange
            var service = GetBankingLogicService();

            //Act
            var result = service.Transfer(_testSenderUserId, new OperationModelDto
            {
                Amount = 100,
                ReceiverUserId = null
            });

            //Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void Transfer_WithIncorrectReceiverUserId_ReturnsErrorResultDto()
        {
            //Arrange
            var service = GetBankingLogicService();

            //Act
            var result = service.Transfer(_testSenderUserId, new OperationModelDto
            {
                Amount = 100,
                ReceiverUserId = Guid.NewGuid()
            });

            //Assert
            Assert.False(result.IsSuccess);
        }


        #endregion

        #region TestData

        private IBankingLogicService GetBankingLogicService() => new BankingLogicService(_unitOfWorkMock.Object);

        private User GetTestSenderUser()
        {
            var user = new User("testSenderUser1", "testSenderUser1@gmail.com", "testSenderUser1Password")
            {
                Balance = (long)SenderUserInitialBalance * 100
            };

            return user;
        }

        private User GetTestReceiverUser()
        {
            return new User("testReceiverUser1", "testReceiverUser1@gmail.com", "testReceiverUser1Password");
        }

        #endregion
    }
}