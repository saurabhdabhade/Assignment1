using System.Net.Mail;
using System.Net;

namespace WebApplication1.Controllers
{
    public class EmailSender
    {
        string smtpServer = "smtp.gmail.com";
        int smtpPort = 587; // Replace with your SMTP port number
        string smtpUsername = "dabhades1999@gmail.com";
        string smtpPassword = "bwdsdbxxmzjthwez";
        bool enableSsl = true;

        public EmailSender() {}

        public void SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    smtpClient.EnableSsl = enableSsl;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(smtpUsername);
                    mailMessage.To.Add(toEmail);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;

                    smtpClient.Send(mailMessage);
                 }
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
    }
}
