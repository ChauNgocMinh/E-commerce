using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Domain.Entities.Users
{
	public class User : IdentityUser
	{
		public string DisplayName { get; set; }
		public string Avata { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime DateOfBirth { get; set; }
	}
}
