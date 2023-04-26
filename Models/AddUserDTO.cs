using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class AddUserDTO
	{
		[Required]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Length should not be less than 3 characters")]
		public string LastName { get; set; } = string.Empty;

		[Required]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Length should not be less than 3 characters")]
		public string FirstName { get; set; } = string.Empty;

		[Required(ErrorMessage ="Enter valid email")]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessage = "Enter Phone")]
		[Phone]
		public string PhoneNumber { get; set; } = string.Empty;

		[Required]
		public string PasswordHash { get; set; } = string.Empty;

	}
}
