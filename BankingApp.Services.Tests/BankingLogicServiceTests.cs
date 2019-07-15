﻿using System;
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

        #endregion

        #region TestData

        private IBankingLogicService GetBankingLogicService() => new BankingLogicService(_unitOfWorkMock.Object);

        private User GetTestSenderUser() => new User("testSenderUser1", "testSenderUser1@gmail.com", "testSenderUser1Password");
        private User GetTestReceiverUser() => new User("testReceiverUser1", "testReceiverUser1@gmail.com", "testReceiverUser1Password");

        #endregion
    }
}