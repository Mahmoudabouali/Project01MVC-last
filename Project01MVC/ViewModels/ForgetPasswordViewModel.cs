using System.ComponentModel.DataAnnotations;

namespace Mvc.PresentationLayer.ViewModels
{
	public class ForgetPasswordViewModel
	{
		[EmailAddress]
		public string Email {  get; set; }
	}
}
