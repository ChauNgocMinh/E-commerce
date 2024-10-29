using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Response.UserResponse
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsVip { get; set; } = false;
        public string DisplayName { get; set; }
        public string Avata { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}
