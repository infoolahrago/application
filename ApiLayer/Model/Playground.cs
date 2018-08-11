using System;
using System.Collections.Generic;

namespace Olahrago.ApiLayer.Model
{
    public partial class Playground
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public string Name { get; set; }
        public string Province { get; set; }
        public string Regency { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Location { get; set; }
    }
}
