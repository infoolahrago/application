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
        private static Result ResultMessage = new Result();

        private static IMessage AppMessage = new Message();

        private static IEncryption Encryption = new Encryption();

        public IList<Account> GetAccountList()
        {
            try
            {
                using (var db = new OlahragoContext())
                {
                    var data = (from x in db.Account select x).ToList();

                    return data;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Account GetAccountData(int id)
        {
            try
            {
                using (var db = new OlahragoContext())
                {
                    var data = (from x in db.Account select x).Where(x => x.Id.Equals(id)).FirstOrDefault();

                    return data;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async void CreateAccount(AccountDto detail)
        {
            using (var db = new OlahragoContext())
            {
                string password = Encryption.EncryptPassword(detail.Username, detail.Password);

                Account account = new Account
                {
                    Username = detail.Username,
                    Password = password,
                    AccountType = detail.AccountType,
                    Email = detail.Email
                };

                await db.Account.AddAsync(account);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Result> UpdateAccount(AccountDto detail)
        {
            try
            {
                using (var db = new OlahragoContext())
                {
                    var data = db.Account.Where(acc => acc.Id.Equals(detail.Id)).FirstOrDefault();
                    
                    if (data!= null)
                    {
                        data.Status = detail.Status;
                    }

                    await db.SaveChangesAsync();

                    ResultMessage.Status = true;
                    ResultMessage.Message = AppMessage.GetMessageApp("account.updated.succesfully");
                }
            }
            catch (Exception ex)
            {
                ResultMessage.Status = false;
                ResultMessage.Message = ex.Message;
            }
            return ResultMessage;
        }

        public async Task<Result> DeleteAccount(AccountDto detail)
        {
            try
            {
                using (var db = new OlahragoContext())
                {
                    var data = db.Account.Where(acc => acc.Id.Equals(detail.Id)).FirstOrDefault();

                    if (data != null)
                    {
                        db.Account.Remove(data);
                    }

                    await db.SaveChangesAsync();

                    ResultMessage.Status = true;
                    ResultMessage.Message = AppMessage.GetMessageApp("account.deleted.succesfully");
                }
            }
            catch (Exception ex)
            {
                ResultMessage.Status = false;
                ResultMessage.Message = ex.Message;
            }
            return ResultMessage;
        }

        public Result CheckUsernameExist(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                ResultMessage.Message = AppMessage.GetMessageApp("username.empty");
                return ResultMessage;
            }

            using (var db = new OlahragoContext())
            {
                var checkUser = db.Account.Where(acc => acc.Username.Equals(username)).ToList();

                if (checkUser.Count > 0)
                {
                    ResultMessage.Status = true;
                    ResultMessage.Message = AppMessage.GetMessageApp("username.exist");
                }
            }

            return ResultMessage;
        }

        public Result CheckEmailExist(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                ResultMessage.Message = AppMessage.GetMessage("email.empty");
                return ResultMessage;
            }

            using (var db = new OlahragoContext())
            {
                var checkEmail = db.Account.Where(acc => acc.Email.Equals(email)).ToList();

                if (checkEmail.Count > 0)
                {
                    ResultMessage.Status = true;
                    ResultMessage.Message = AppMessage.GetMessage("email.exist");
                }
            }

            return ResultMessage;
        }
    }
}
