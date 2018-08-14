using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Olahrago.ApiLayer.Misc
{
    public class Result
    {
        public bool Status
        {
            get; set;
        }
        public string Message { get; set; }
        public Object Data {get;set;}
    }
}
