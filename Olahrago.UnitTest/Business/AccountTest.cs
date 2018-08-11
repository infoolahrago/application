using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ApiLayer.Business.Interface;

namespace Olahrago.UnitTest.Business
{
    public class AccountTest
    {
        private readonly IAccountLogic Account;

        [Fact]
        public void CreateAccount_WhenUsernameExist_ReturnStatusFalse()
        {

        }

        [Fact]
        public void CreateAccount_WhenEmailExist_ReturnStatusFalse()
        {

        }
    }
}
