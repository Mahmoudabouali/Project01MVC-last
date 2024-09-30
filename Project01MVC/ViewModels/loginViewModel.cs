using System.ComponentModel.DataAnnotations;

namespace Mvc.PresentationLayer.ViewModels
{
	public class LoginViewModel
	{
		[Required]
		[EmailAddress(ErrorMessage = "invaild email")]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public bool RememberMe { get; set; }
	}
}
