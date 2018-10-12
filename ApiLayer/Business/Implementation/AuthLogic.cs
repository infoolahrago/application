using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Olahrago.ApiLayer.Misc;
using Olahrago.ApiLayer.Misc.Interface;
using Olahrago.ApiLayer.Model;
using Olahrago.ApiLayer.Business.Interface;
using Microsoft.Extensions.Options;
using Olahrago.ApiLayer.Model.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Olahrago.ApiLayer.Business.Implementation
{
    public class AuthLogic : IAuthLogic
    {
        private Result ResultMessage = new Result();

        public IMessage AppMessage;

        public IEncryption Encryption;

        private readonly OlahragoContext Context;

        private readonly IOptions<JwtAuth> JwtAuthentication;

        public AuthLogic(IMessage message, IEncryption encryption, OlahragoContext olahragoContext, IOptions<JwtAuth> jwtAuth)
        {
            AppMessage = message;
            Encryption = encryption;
            Context = olahragoContext;
            JwtAuthentication = jwtAuth;
        }

        public AccountDto Login(string username, string password)
        {
            bool isVerified = false;
            AccountDto account = new AccountDto();

            var data = (from usr in Context.Account where usr.Username.Equals(username) select usr).FirstOrDefault();

            if (data != null)
            {
                isVerified = Encryption.VerifyMd5Hash(string.Concat(username, password), data.Password);
            }

            if (isVerified)
            {
                account.AccountType = data.AccountType;
                account.Username = data.Username;
                account.Id = data.Id;
                account.JwtToken = GenerateToken(account);
            }

            return account;
        }

        private void SaveToken(AccountDto account, DateTime expiredDate)
        {
            try
            {
                Tokens tokens = new Tokens();

                tokens.AccountId = account.Id;
                tokens.ExpiredDate = expiredDate;
                tokens.Token = account.JwtToken;

                Context.Tokens.Add(tokens);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private Result CheckTokenExist(AccountDto account)
        {
            var data = (from tkn in Context.Tokens
                        where tkn.AccountId.Equals(account.Id) && DateTime.Now <= tkn.ExpiredDate
                        select tkn).FirstOrDefault();

            if (data != null)
            {
                ResultMessage.Data = data.Token;
                ResultMessage.Status = true;
            }
            else
            {
                ResultMessage.Status = false;
            }

            return ResultMessage;
        }

        private string GenerateToken(AccountDto account)
        {
            DateTime expired = DateTime.UtcNow.AddDays(3650);
            string tokenData = string.Empty;

            //check jwt token for each user
            var checkTokenExist = CheckTokenExist(account);
            if (checkTokenExist.Status)
            {
                tokenData = checkTokenExist.Data.ToString();
            }
            else
            {
                var token = new JwtSecurityToken(
                    issuer: JwtAuthentication.Value.ValidIssuer,
                    audience: JwtAuthentication.Value.ValidAudience,
                    claims: new[]
                    {
                    // You can add more claims if you want
                    new Claim(JwtRegisteredClaimNames.Sub, account.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    },
                    expires: expired,
                    notBefore: DateTime.UtcNow,
                    signingCredentials: JwtAuthentication.Value.SigningCredentials
                );

                tokenData = new JwtSecurityTokenHandler().WriteToken(token);
                account.JwtToken = tokenData;

                SaveToken(account, expired);
            }

            return tokenData;
        }
    }
}
