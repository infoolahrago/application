using ApiLayer.Misc;
using ApiLayer.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLayer.Business.Interface
{
    public interface IAccountLogic
    {
        Task<Result> CreateAccount(AccountDto detail);
    }
}
