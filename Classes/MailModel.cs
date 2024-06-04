using System.Net;
using System.Net.Mail;
namespace MuvekkilTakipSistemi.Classes
{
    public class MailModel
    {
        private static string? ErrorMessage { get; set; } //Hata mesajı için global bir değişken

        //Mesaj gönderme işlemlerini yapan metod
        public static bool SendMessage(string Email, string Konu, string Message)
        {
            try
            {
                var myMail = "destekmuvekkiltakip@gmail.com"; //Gönderici maili
                var pass = "budbzbqukyryftuz"; //Güvenlik gerekçesiyle şifre yerine mail servisinden bir uygulama kodu oluşturuldu
                var smtpClient = new SmtpClient("smtp.gmail.com", 587); //Mail serveri
                smtpClient.Credentials = new NetworkCredential(myMail, pass); //Bilgiler smtpclient içine girilir.
                smtpClient.EnableSsl = true; //SSL sertifikası doğrulama
                var mailMessage = new MailMessage( 
                    from: myMail,
                    to: Email,
                    subject: Konu,
                    Message
                    );//mail model
                smtpClient.Send(mailMessage); //mail gönder
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;//hata mesajı
                return false;
            }
        }
        public static string GetErrorMessage() //hata mesajı dönen metod
        {
            return ErrorMessage;
        }
    }
}
