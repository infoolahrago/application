using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Olahrago.ApiLayer.Misc.Interface
{
    public interface IEncryption
    {
        string EncryptPassword(string username, string password);

        bool VerifyEncryption(string username, string password);
    }
}
