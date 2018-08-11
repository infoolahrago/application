using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Olahrago.ApiLayer.Model;
using Olahrago.ApiLayer.Model.Dto;
using Olahrago.ApiLayer.Misc;
using Olahrago.ApiLayer.Business.Interface;

namespace Olahrago.ApiLayer.Business.Implementation
{
    public class AccountLogic : IAccountLogic
    {
        private static Result ResultMessage = new Result();

        private static Message AppMessage;

        public async Task<Result> CreateAccount(AccountDto detail)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(detail.Email))
                {
                    ResultMessage.Message = AppMessage.GetMessage("email.empty");
                    return ResultMessage;
                }

                if (string.IsNullOrWhiteSpace(detail.Username))
                {
                    ResultMessage.Message = AppMessage.GetMessage("username.empty");
                    return ResultMessage;
                }

                using (var db = new OlahragoContext())
                {
                    Account account = new Account
                    {
                        Username = detail.Username,
                        Password = detail.Password,
                        AccountType = detail.AccountType
                    };

                    await db.Account.AddAsync(account);
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

        public bool CheckUsernameExist(string username)
        {
            using (var db = new OlahragoContext())
            {
                var checkUser = db.Account.Where(acc => acc.Username.Equals(username)).ToList();

                if (checkUser.Count > 0)
                {
                    ResultMessage.Status = true;
                }
            }

            return ResultMessage.Status;
        }

        public bool CheckEmailExist(string email)
        {
            using (var db = new OlahragoContext())
            {
                var checkEmail = db.Account.Where(acc => acc.Email.Equals(email)).ToList();

                if (checkEmail.Count > 0)
                {
                    ResultMessage.Status = true;
                }
            }

            return ResultMessage.Status;
        }
    }
}
