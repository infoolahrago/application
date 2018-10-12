using System;
using Microsoft.AspNetCore.Mvc;
using Olahrago.ApiLayer.Model.Dto;
using Olahrago.ApiLayer.Misc;
using Olahrago.ApiLayer.Business.Interface;
using System.Threading.Tasks;

namespace Olahrago.ApiLayer.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        public Result ResultMessage = new Result();
        public IAccountLogic Account { get; set; }

        public AccountController(IAccountLogic account)
        {
            Account = account;
        }

        // GET api/values
        [HttpGet]
        public async Task<Result> Get()
        {
            try
            {
                ResultMessage.Data = Account.GetAccountList();
                ResultMessage.Status = true;
            }
            catch (Exception ex)
            {
                ResultMessage.Message = ex.Message;
            }
            return ResultMessage;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Result Get(int id)
        {
            try
            {
                ResultMessage.Data = Account.GetAccountData(id);
                ResultMessage.Status = true;
            }
            catch (Exception ex)
            {
                ResultMessage.Message = ex.Message;
            }
            return ResultMessage;
        }

        // POST api/values
        [HttpPost]
        public Result Post([FromForm]AccountDto param)
        {
            try
            {
                var checkUsernameExist = Account.CheckUsernameExist(param.Username);
                var checkEmailExist = Account.CheckEmailExist(param.Email);

                if (checkUsernameExist.Status)
                {
                    return checkUsernameExist;
                }

                if (checkEmailExist.Status)
                {
                    return checkEmailExist;
                }

                Account.CreateAccount(param);
                ResultMessage.Status = true;
            }
            catch (Exception ex)
            {
                ResultMessage.Message = ex.Message;
            }

            return ResultMessage;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Result Put(int id, [FromForm]AccountDto param)
        {
            try
            {
                Account.UpdateAccount(param);
                ResultMessage.Status = true;
            }
            catch (Exception ex)
            {
                ResultMessage.Message = ex.Message;
            }
            return ResultMessage;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public Result Delete(int id)
        {
            try
            {
                Account.DeleteAccount(id);
                ResultMessage.Status = true;
            }
            catch (Exception ex)
            {
                ResultMessage.Message = ex.Message;
            }
            return ResultMessage;
        }
    }
}
