using WatchMovie.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;

namespace WatchMovie.Domain.Entities.Users
{
    public class User : IdentityUser<Guid>
    {
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsVip { get; set; } = false;
        public string DisplayName { get; set; }
        public string Avata { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
