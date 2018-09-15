using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Olahrago.ApiLayer.Model.Dto;
using Olahrago.ApiLayer.Misc;
using Olahrago.ApiLayer.Business.Interface;
using System.Threading.Tasks;
using Olahrago.ApiLayer.Misc.Interface;

namespace Olahrago.ApiLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : ControllerBase
    {
        public Result ResultMessage = new Result();
        public IAccountLogic Account { get; set; }
        public IMessage AppMessage { get; set; }
        public IAuthLogic Auth { get; set; }

        public AuthController(IAccountLogic account, IMessage message, IAuthLogic auth)
        {
            Account = account;
            AppMessage = message;
            Auth = auth;
        }

        // GET: api/Auth/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("login")]
        public Result Login([FromForm]string username, string password)
        {
            var checkUsername = Account.CheckUsernameExist(username);

            if (!checkUsername.Status)
            {
                ResultMessage.Status = false;
                ResultMessage.Message = AppMessage.GetMessageApp("wrong.username.or.password");
                return ResultMessage;
            }

            var login = Auth.Login(username, password);

            if (login)
            {
                var token = Auth.GenerateToken(username);

                ResultMessage.Status = true;
                ResultMessage.Message = AppMessage.GetMessageApp("login.success");
                ResultMessage.Data = token;
            }
            else
            {
                ResultMessage.Message = AppMessage.GetMessageApp("wrong.username.or.password");
            }

            return ResultMessage;
        }

        // POST: api/Auth
        [HttpPost]
        public Result Post([FromBody]string username, string password)
        {
            try
            {
                var checkUsername = Account.CheckUsernameExist(username);

                if (!checkUsername.Status)
                {
                    ResultMessage.Status = false;
                    ResultMessage.Message = AppMessage.GetMessageApp("wrong.username.or.password");
                    return ResultMessage;
                }

                var login = Auth.Login(username, password);

                if (login)
                {
                    ResultMessage.Status = true;
                    ResultMessage.Message = AppMessage.GetMessageApp("login.success");
                }
                else
                {
                    ResultMessage.Message = AppMessage.GetMessageApp("wrong.username.or.password");
                }
            }
            catch (Exception ex)
            {
                ResultMessage.Status = false;
                ResultMessage.Message = ex.Message;
            }

            return ResultMessage;
        }
        
        // PUT: api/Auth/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
