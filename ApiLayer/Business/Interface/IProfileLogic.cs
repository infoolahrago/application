using System;
using System.Collections.Generic;
using Olahrago.ApiLayer.Model.Dto;

namespace Olahrago.ApiLayer.Business.Interface
{
    public interface IProfileLogic
    {
        IList<OwnerDto> GetListOwners();

        OwnerDto GetOwnerData(int id);

        void CreateOwner(OwnerDto detail);

        void UpdateOwner(OwnerDto detail);

        void DeleteOwner(int accountId);

        IList<UserDto> GetListUser();

        UserDto GetUserData(int id);

        void CreateUser(UserDto detail);

        void UpdateUser(UserDto detail);

        void DeleteUser(int id);
    }
}
