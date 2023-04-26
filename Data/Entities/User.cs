using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
	public class User : IdentityUser
	{
		[Required]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Length should not be less than 3 characters")]
		public string LastName { get; set; } = string.Empty;

		[Required]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Length should not be less than 3 characters")]
		public string FirstName { get; set; } = string.Empty;

	}
}
