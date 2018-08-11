using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ApiLayer.Model.Dto;
using ApiLayer.Business;
using ApiLayer.Misc;
using ApiLayer.Business.Interface;
using ApiLayer.Business.Implementation;

namespace ApiLayer.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        private static Result ResultMessage = new Result();
        private static readonly IAccountLogic Account = new AccountLogic();

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<Result> Post([FromForm]AccountDto param)
        {
            ResultMessage = await Account.CreateAccount(param);
            
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
