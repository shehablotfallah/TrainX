using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace TrainX.Models
{
    public partial class User 
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int SportId { get; set; }

        public virtual Sport? Sport { get; set; }
    }
}
