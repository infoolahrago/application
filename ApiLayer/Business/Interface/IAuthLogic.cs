using Olahrago.ApiLayer.Misc;
using Olahrago.ApiLayer.Model.Dto;
using System.Collections.Generic;
using Olahrago.ApiLayer.Model;

namespace Olahrago.ApiLayer.Business.Interface
{
    public interface IAuthLogic
    {
        bool Login(string username, string password);

        string GenerateToken(string username);
    }
}
