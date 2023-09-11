using System;
using System.Collections.Generic;

namespace TrainX.Models
{
    public partial class Video
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? PublishingDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public int CoachId { get; set; }
        public int SportId { get; set; }
        public string? UrlVideo { get; set; }

        public virtual Coach Coach { get; set; } = null!;
        public virtual Sport Sport { get; set; } = null!;
    }
}
