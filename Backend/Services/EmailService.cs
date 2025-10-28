using Backend.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailService : IEmailService
{
    private readonly string _apiKey;
    private readonly string _fromEmail = "principioscar89@gmail.com"; 
    private readonly string _toEmail = "principioscar89@gmail.com";   
    private readonly string _fromName = "Software Prime - Portfolio";

    public EmailService(IConfiguration config)
    {
        _apiKey = config["SendGrid:ApiKey"]!;
    }

    public async Task<bool> SendEmailAsync(string name, string email, string motivo, string mensaje)
    {
        var client = new SendGridClient(_apiKey);

        var from = new EmailAddress(_fromEmail, _fromName);
        var to = new EmailAddress(_toEmail);

        var subject = $"📨 Nuevo mensaje desde el portfolio: {motivo}";

        var plainText =
                $@"Nombre: {name}
                Email: {email}
                Motivo: {motivo}
                Mensaje:
                {mensaje}";

        var htmlContent =
                $@"<strong>Nombre:</strong> {name}<br>
                <strong>Email:</strong> {email}<br>
                <strong>Motivo:</strong> {motivo}<br><br>
                <strong>Mensaje:</strong><br>
                {mensaje}";

        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainText, htmlContent);
        var response = await client.SendEmailAsync(msg);

        return response.StatusCode == System.Net.HttpStatusCode.Accepted;
    }
}
