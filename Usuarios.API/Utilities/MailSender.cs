using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace Usuarios.API.Utilities
{
    public class MailSender
    {
        public static void Principal(string mensaje, string asunto, string correo)
        {
            Execute(mensaje, asunto, correo).Wait();
        }

        static async Task Execute(string mensaje, string asunto, string correo)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("2538362017@mail.utec.edu.sv", "Biblioteca Comunitaria de Jardines de Colón");
            var subject = asunto;
            var to = new EmailAddress(correo, "Estudiante");
            var plainTextContent = mensaje;
            var htmlContent = mensaje;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
