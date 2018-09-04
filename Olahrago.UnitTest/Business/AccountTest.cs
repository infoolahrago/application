using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using Olahrago.ApiLayer.Business.Interface;
using Olahrago.ApiLayer.Misc;
using Olahrago.ApiLayer.Controllers;
using Olahrago.ApiLayer.Model.Dto;

namespace Olahrago.UnitTest.Business
{
    public class AccountTest
    {
        [Fact]
        public async void Post_WhenUsernameExist_ReturnTrue()
        {
            //Arrange
            var accountMock = new Mock<IAccountLogic>();
            
            accountMock.Setup(x => x.CheckUsernameExist(It.IsAny<string>())).Returns(() => new Result {
                Status = true
            });
            accountMock.Setup(x => x.CheckEmailExist(It.IsAny<string>())).Returns(() => new Result {
                Status = false
            });

            IAccountLogic account = accountMock.Object;
            var AccountCtrl = new AccountController(account);

            //Act
            var accountTest = AccountCtrl.Post(new AccountDto());
            var result = await accountTest.Should().BeOfType<Task<Result>>().Subject;

            //Assert
            result.Status.Should().BeTrue();
        }

        [Fact]
        public async void Post_WhenEmailExist_ReturnTrue()
        {
            //Arrange
            var accountMock = new Mock<IAccountLogic>();

            accountMock.Setup(x => x.CheckUsernameExist(It.IsAny<string>())).Returns(() => new Result
            {
                Status = false
            });
            accountMock.Setup(x => x.CheckEmailExist(It.IsAny<string>())).Returns(() => new Result
            {
                Status = true
            });

            IAccountLogic account = accountMock.Object;
            var AccountCtrl = new AccountController(account);

            //Act
            var accountTest = AccountCtrl.Post(new AccountDto());
            var result = await accountTest.Should().BeOfType<Task<Result>>().Subject;

            //Assert
            result.Status.Should().BeTrue();
        }

        [Fact]
        public async void Post_WhenCreateAccountSuccess_ReturnTrue()
        {
            //Arrange
            var accountMock = new Mock<IAccountLogic>();

            accountMock.Setup(x => x.CheckUsernameExist(It.IsAny<string>())).Returns(() => new Result
            {
                Status = false
            });
            accountMock.Setup(x => x.CheckEmailExist(It.IsAny<string>())).Returns(() => new Result
            {
                Status = false
            });
            accountMock.Setup(x => x.CreateAccount(It.IsAny<AccountDto>()));

            IAccountLogic account = accountMock.Object;
            var AccountCtrl = new AccountController(account);

            //Act
            var accountTest = AccountCtrl.Post(new AccountDto());
            var result = await accountTest.Should().BeOfType<Task<Result>>().Subject;

            //Assert
            result.Status.Should().BeTrue();
        }
    }
}
