using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiLayer.Model;
using ApiLayer.Model.Dto;
using ApiLayer.Misc;
using ApiLayer.Business.Interface;

namespace ApiLayer.Business.Implementation
{
    public class AccountLogic : IAccountLogic
    {
        private static Result ResultMessage = new Result();

        public async Task<Result> CreateAccount(AccountDto detail)
        {
            try
            {
                using (var db = new OlahragoContext())
                {
                    Account account = new Account();
                    var AccountData = account;
                    AccountData.Username = detail.Username;
                    AccountData.Password = detail.Password;
                    AccountData.AccountType = detail.AccountType;

                    await db.Account.AddAsync(AccountData);
                    await db.SaveChangesAsync();

                    ResultMessage.Status = true;
                }
            }
            catch (Exception ex)
            {
                ResultMessage.Status = false;
                ResultMessage.Message = ex.Message;
            }
            return ResultMessage;
        }
    }
}
