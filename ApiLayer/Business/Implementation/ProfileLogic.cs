using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Olahrago.ApiLayer.Model;
using Olahrago.ApiLayer.Model.Dto;
using Olahrago.ApiLayer.Misc;
using Olahrago.ApiLayer.Misc.Interface;
using Olahrago.ApiLayer.Business.Interface;

namespace Olahrago.ApiLayer.Business.Implementation
{
    public class ProfileLogic : IProfileLogic
    {
        private Result ResultMessage = new Result();

        public IMessage AppMessage { get; set; }

        private readonly OlahragoContext context = new OlahragoContext();

        public ProfileLogic(IMessage message)
        {
            AppMessage = message;
        }

        public IList<OwnerDto> GetListOwners()
        {
            var data = (from own in context.Owner
                        join acc in context.Account on own.AccountId equals acc.Id into accs
                        from list in accs
                        select new OwnerDto
                        {
                            AccountId = own.AccountId,
                            Id = own.Id,
                            Name = own.Name,
                            Address = own.Address,
                            Email = own.Email,
                            Phone = own.Phone,
                            Account = list
                        }
                       ).ToList();

            return data;
        }

        public OwnerDto GetOwnerData(int id)
        {
            var data = (from own in context.Owner
                        join acc in context.Account on own.AccountId equals acc.Id into accs
                        from list in accs.DefaultIfEmpty()
                        where own.Id.Equals(id)
                        select new OwnerDto
                        {
                            AccountId = own.AccountId,
                            Id = own.Id,
                            Name = own.Name,
                            Address = own.Address,
                            Email = own.Email,
                            Phone = own.Phone,
                            Account = list
                        }
                       ).FirstOrDefault();
            return data;
        }

        public void CreateOwner(OwnerDto detail)
        {
            Owner data = new Owner
            {
                AccountId = detail.AccountId,
                Email = detail.Email,
                Name = detail.Name,
                Phone = detail.Phone,
                Address = detail.Address,
                IdentityType = detail.IdentityType,
                IdentityNumber = detail.IdentityNumber
            };

            context.Owner.Add(data);
            context.SaveChanges();
        }

        public void UpdateOwner(OwnerDto detail)
        {
            var data = (from own in context.Owner where own.Id.Equals(detail.Id) select own).FirstOrDefault();

            data.Phone = detail.Phone;
            data.Address = detail.Address;
            data.Name = detail.Name;

            context.SaveChanges();
        }

        public void DeleteOwner(int accountId)
        {
            var data = (from own in context.Owner where own.AccountId.Equals(accountId) select own).FirstOrDefault();

            context.Remove(data);
            context.SaveChanges();
        }

        public IList<UserDto> GetListUser()
        {
            var data = (from usr in context.User
                        join accs in context.Account on usr.AccountId equals accs.Id into jaccs
                        from list in jaccs.DefaultIfEmpty()
                        select new UserDto
                        {
                            Account = list,
                            AccountId = usr.AccountId,
                            Email = usr.Email,
                            Id = usr.Id,
                            Name = usr.Name,
                            Phone = usr.Phone
                        }
                        ).ToList();

            return data;
        }

        public UserDto GetUserData(int id)
        {
            var data = (from usr in context.User
                        join accs in context.Account on usr.AccountId equals accs.Id into jaccs
                        from list in jaccs.DefaultIfEmpty()
                        where usr.Id.Equals(id)
                        select new UserDto
                        {
                            Account = list,
                            AccountId = usr.AccountId,
                            Email = usr.Email,
                            Id = usr.Id,
                            Name = usr.Name,
                            Phone = usr.Phone
                        }
                        ).FirstOrDefault();
            return data;
        }

        public void CreateUser(UserDto detail)
        {
            var data = new UserDto
            {
                AccountId = detail.AccountId,
                Email = detail.Email,
                Name = detail.Name,
                Phone = detail.Phone
            };

            context.User.Add(data);
            context.SaveChanges();
        }

        public void UpdateUser(UserDto detail)
        {
            var data = (from usr in context.User where usr.Id.Equals(detail.Id) select usr).FirstOrDefault();

            if (data!= null)
            {
                data.Phone = detail.Phone;

                context.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            var data = (from usr in context.User where usr.Id.Equals(id) select usr).FirstOrDefault();

            if (data != null)
            {
                context.Remove(data);
                context.SaveChanges();
            }
        }
    }
}
