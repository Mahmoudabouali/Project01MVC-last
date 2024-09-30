using System.ComponentModel.DataAnnotations;

namespace Mvc.PresentationLayer.ViewModels
{
	public class ResetPasswordViewModel
	{
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[DataType(DataType.Password)]
		[Compare(nameof(Password), ErrorMessage = "password & confirm password doesn't match")]
		public string ConfirmPassword { get; set; }
        public string Email { get; set; }
		public string Token { get; set; }
	}
}
