using System;
using System.Collections.Generic;

namespace Olahrago.ApiLayer.Model
{
    public partial class PlaygroundImage
    {
        public long Id { get; set; }
        public long PlaygroundId { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
