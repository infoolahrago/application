using System;
using System.Collections.Generic;

namespace Olahrago.ApiLayer.Model
{
    public partial class PlaygroundFacilities
    {
        public long Id { get; set; }
        public long PlaygroundId { get; set; }
        public long FacilityId { get; set; }
    }
}
