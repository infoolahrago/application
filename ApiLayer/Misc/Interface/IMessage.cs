using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Olahrago.ApiLayer.Misc.Interface
{
    public interface IMessage
    {
        string GetMessage(string key);

        string GetMessageApp(string key);
    }
}
