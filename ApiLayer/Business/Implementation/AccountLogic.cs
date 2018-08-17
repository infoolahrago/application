using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Olahrago.ApiLayer.Model;
using Olahrago.ApiLayer.Model.Dto;
using Olahrago.ApiLayer.Misc;
using Olahrago.ApiLayer.Misc.Interface;
using Olahrago.ApiLayer.Business.Interface;

namespace Olahrago.ApiLayer.Business.Implementation
{
    public class AccountLogic : IAccountLogic
    {
        private Result ResultMessage = new Result();

        public IMessage AppMessage { get; set; }

        public IEncryption Encryption { get; set; }

        private readonly OlahragoContext context = new OlahragoContext();

        public AccountLogic(IMessage message, IEncryption encryption)
        {
            AppMessage = message;
            Encryption = encryption;
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

        public async void CreateAccount(AccountDto detail)
        {
            string password = Encryption.EncryptPassword(detail.Username, detail.Password);

            Account account = new Account
            {
                Username = detail.Username,
                Password = password,
                AccountType = detail.AccountType,
                Email = detail.Email
            };

            await context.Account.AddAsync(account);
            await context.SaveChangesAsync();
        }

        public async void UpdateAccount(AccountDto detail)
        {
            var data = context.Account.Where(acc => acc.Id.Equals(detail.Id)).FirstOrDefault();

            if (data != null)
            {
                data.Status = detail.Status;

                await context.SaveChangesAsync();
            }
        }

        public async void DeleteAccount(int id)
        {
            var data = context.Account.Where(acc => acc.Id.Equals(id)).FirstOrDefault();

            if (data != null)
            {
                context.Account.Remove(data);

                await context.SaveChangesAsync();
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
