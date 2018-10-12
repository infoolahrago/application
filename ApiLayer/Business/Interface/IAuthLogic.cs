using Olahrago.ApiLayer.Misc;
using Olahrago.ApiLayer.Model.Dto;
using System.Collections.Generic;
using Olahrago.ApiLayer.Model;

namespace Olahrago.ApiLayer.Business.Interface
{
    public interface IAuthLogic
    {
        AccountDto Login(string username, string password);
    }
}
