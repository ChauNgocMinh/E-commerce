using E_Commerce.Domain.Common;
using E_Commerce.Domain.Entities.Ranks;
using Microsoft.AspNetCore.Identity;
using System;

namespace E_Commerce.Domain.Entities.Users
{
    public class User : IdentityUser<Guid>
    {
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string DisplayName { get; set; }
        public string Avata { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid RankID { get; set; }
        public virtual ICollection<Rank> Ranks { get; set; }

    }
}
