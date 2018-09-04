using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Olahrago.ApiLayer.Model;
using Olahrago.ApiLayer.Model.Dto;
using Olahrago.ApiLayer.Misc;
using Olahrago.ApiLayer.Misc.Interface;
using Olahrago.ApiLayer.Business.Interface;
using Microsoft.Extensions.Configuration;

namespace Olahrago.ApiLayer.Business.Implementation
{
    public class AccountLogic : IAccountLogic
    {
        private Result ResultMessage = new Result();

        public IMessage AppMessage;

        public IEncryption Encryption;

        private OlahragoContext context;

        public AccountLogic(IMessage message, IEncryption encryption, OlahragoContext olahragoContext)
        {
            AppMessage = message;
            Encryption = encryption;
            context = olahragoContext;
        }

        public IList<Account> GetAccountList()
        {
            var data = (from x in context.Account select x).ToList();

            return data;
        }

        public Account GetAccountData(int id)
        {
            var data = (from x in context.Account select x).Where(x => x.Id.Equals(id)).FirstOrDefault();

            return data;
        }

        public void CreateAccount(AccountDto detail)
        {
            string password = Encryption.EncryptPassword(detail.Username, detail.Password);

            Account account = new Account
            {
                Username = detail.Username,
                Password = password,
                AccountType = detail.AccountType,
                Email = detail.Email
            };

            context.Account.Add(account);
            context.SaveChanges();
        }

        public void UpdateAccount(AccountDto detail)
        {
            var data = context.Account.Where(acc => acc.Id.Equals(detail.Id)).FirstOrDefault();

            if (data != null)
            {
                data.Status = detail.Status;

                context.SaveChangesAsync();
            }
        }

        public void DeleteAccount(int id)
        {
            var data = context.Account.Where(acc => acc.Id.Equals(id)).FirstOrDefault();

            if (data != null)
            {
                context.Account.Remove(data);

                context.SaveChangesAsync();
            }
        }

        public Result CheckUsernameExist(string username)
        {
            var checkUser = context.Account.Where(acc => acc.Username.Equals(username)).ToList();

            if (checkUser.Count > 0)
            {
                ResultMessage.Status = true;
                ResultMessage.Message = AppMessage.GetMessageApp("username.exist");
            }

            return ResultMessage;
        }

        public Result CheckEmailExist(string email)
        {
            var checkEmail = context.Account.Where(acc => acc.Email.Equals(email)).ToList();

            if (checkEmail.Count > 0)
            {
                ResultMessage.Status = true;
                ResultMessage.Message = AppMessage.GetMessageApp("email.exist");
            }

            return ResultMessage;
        }
    }
}
