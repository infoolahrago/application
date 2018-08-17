using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Olahrago.ApiLayer.Business.Interface;
using Olahrago.ApiLayer.Business.Implementation;
using Olahrago.ApiLayer.Misc.Interface;
using Olahrago.ApiLayer.Misc;

namespace Olahrago.ApiLayer.Config
{
    public class ConstructorConfig
    {
        public void AddConstructor(IServiceCollection services)
        {
            //AccountLogic
            services.AddSingleton<IAccountLogic, AccountLogic>();
            services.AddSingleton<IMessage, Message>();
            services.AddSingleton<IEncryption, Encryption>();
        }
    }
}
