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

        public bool Login(string username, string password)
        {
            bool isVerified = false;
            var data = (from usr in Context.Account where usr.Username.Equals(username) select usr).FirstOrDefault();

            if (data != null)
            {
                isVerified = Encryption.VerifyMd5Hash(string.Concat(username, password), data.Password);
            }

            return isVerified;
        }

        public string GenerateToken(string username)
        {
            var token = new JwtSecurityToken(
            issuer: JwtAuthentication.Value.ValidIssuer,
            audience: JwtAuthentication.Value.ValidAudience,
            claims: new[]
            {
                // You can add more claims if you want
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            },
            expires: DateTime.UtcNow.AddDays(30),
            notBefore: DateTime.UtcNow,
            signingCredentials: JwtAuthentication.Value.SigningCredentials);

            var tokenData = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenData;
        }
    }
}
