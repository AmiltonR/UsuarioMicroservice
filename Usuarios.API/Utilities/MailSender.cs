using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Net.Mail;

namespace Usuarios.API.Utilities
{
    public class MailSender
    {

        public static bool sendMail(string to, string asunto, string body)
        {
            bool msge = false;
            string from = "2502592017@mail.utec.edu.sv";
            string displayName = "Biblioteca comunitaria de Jardines de Colón";
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from, displayName);
                mail.To.Add(to);

                mail.Subject = asunto;
                mail.Body = body;
                mail.IsBodyHtml = true;


                SmtpClient client = new SmtpClient("smtp.office365.com", 587); //Aquí debes sustituir tu servidor SMTP y el puerto
                client.Credentials = new NetworkCredential(from, "08061998980608");
                client.EnableSsl = true;//En caso de que tu servidor de correo no utilice cifrado SSL,poner en false


                client.Send(mail);
                msge = true;

            }
            catch (Exception ex)
            {
            }

            return msge;
        }
    }
}
