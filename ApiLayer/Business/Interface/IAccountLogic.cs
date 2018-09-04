using Olahrago.ApiLayer.Misc;
using Olahrago.ApiLayer.Model.Dto;
using System.Collections.Generic;
using Olahrago.ApiLayer.Model;

namespace Olahrago.ApiLayer.Business.Interface
{
    public interface IAccountLogic
    {
        void CreateAccount(AccountDto detail);

        IList<Account> GetAccountList();

        Account GetAccountData(int id);

        void UpdateAccount(AccountDto detail);

        void DeleteAccount(int id);

        Result CheckUsernameExist(string username);

        Result CheckEmailExist(string email);
    }
}
