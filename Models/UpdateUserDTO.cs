using System.ComponentModel.DataAnnotations;


namespace Models
{
	public class UpdateUserDTO
	{
		
		[Required]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Length should not be less than 3 characters")]
		public string LastName { get; set; } = string.Empty;

		[Required]
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Length should not be less than 3 characters")]
		public string FirstName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Enter valid email")]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessage = "Enter Phone")]
		[Phone]
		public string PhoneNumber { get; set; } = string.Empty;

	}
}
