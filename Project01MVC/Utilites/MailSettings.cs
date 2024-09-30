using System.Net;
using System.Net.Mail;

namespace Mvc.PresentationLayer.Utilites
{
	public class Email
	{
        public string Subject { get; set; }
		public string Body { get; set; }
		public string Recipient { get; set; }
	}
	public static class MailSettings
	{
		public static void SendEmail(Email email)
		{
			// create SMTP clinet
			var client = new SmtpClient("smtp.gmail.com",587);
			client.EnableSsl = true;
			// create network credentials
			client.Credentials = new NetworkCredential("mahmoudabouali75@gmail.com", "nkpzparfazbkobhu");
			client.Send("mahmoudabouali75@gmail.com", email.Recipient, email.Subject, email.Body);
		}
	}
}
