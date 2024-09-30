using System.ComponentModel.DataAnnotations;

namespace Mvc.PresentationLayer.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage ="First name is required")]
        public string FirstName { get; set; }
		[Required(ErrorMessage = "Last name is required")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "User name is required")]
		public string UserName { get; set; }
		[EmailAddress(ErrorMessage ="invaild email")]
		public string Email { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[DataType(DataType.Password)]
		[Compare(nameof(Password),ErrorMessage ="password & confirm password doesn't match")]
		public string ConfirmPassword { get; set; }
		public bool IsAgree { get; set; }

	}
}
