using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Olahrago.ApiLayer.Model;

namespace Olahrago.ApiLayer.Misc
{
    public class Message
    {
        public string GetMessage(string key)
        {
            string result = string.Empty;

            using (var db = new OlahragoContext())
            {
                result = db.ApplicationMessage.Where(am => am.Key.Equals(key)).FirstOrDefault().Value;
            }

            return result;
        }

        public string GetMessageApp(string key)
        {
            string result = string.Empty;



            return result;
        }
    }
}
