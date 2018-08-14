using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Olahrago.ApiLayer.Model.Dto;
using Olahrago.ApiLayer.Business;
using Olahrago.ApiLayer.Misc;
using Olahrago.ApiLayer.Misc.Interface;
using Olahrago.ApiLayer.Business.Interface;
using Olahrago.ApiLayer.Business.Implementation;

namespace ApiLayer.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        private static Result ResultMessage = new Result();
        private static readonly IAccountLogic Account = new AccountLogic();
        private static IMessage AppMessage { get; set; }

        // GET api/values
        [HttpGet]
        public Result Get()
        {
            ResultMessage.Data = Account.GetAccountList();

            return ResultMessage;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public Result Post([FromForm]AccountDto param)
        {
            try
            {
                var checkUsername = Account.CheckUsernameExist(param.Username);
                if (!checkUsername.Status)
                {
                    return checkUsername;
                }

                Account.CreateAccount(param);
            }
            catch (Exception ex)
            {
                ResultMessage.Message = ex.Message;
            }            
            
            return ResultMessage;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
