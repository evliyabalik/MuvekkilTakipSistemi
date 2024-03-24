using System.Net;
using System.Net.Mail;

namespace MuvekkilTakipSistemi.Models
{


    public class MailModel
    {

        private static string? ErrorMessage { get; set; }

        public static bool SendMessage(string Email, string Konu, string Message)
        {
            try
            {
                var myMail = "destekmuvekkiltakip@gmail.com";
                var pass = "budbzbqukyryftuz";

                var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.Credentials = new NetworkCredential(myMail, pass);
                smtpClient.EnableSsl = true;

                var mailMessage = new MailMessage(
                    from: myMail,
                    to:Email,
                    subject:Konu,
                    Message
                    );
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }

        public static string GetErrorMessage()
        {
            return ErrorMessage;
        }
    }
}
