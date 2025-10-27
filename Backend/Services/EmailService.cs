using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailService
{
    private readonly string _apiKey;
    private readonly string _fromEmail = "contacto@tudominio.com";
    private readonly string _fromName = "Mi Web";

    public EmailService()
    {
        _apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
    }

    public async Task<bool> SendEmailAsync(string name, string email, string motivo, string mensaje)
    {
        var client = new SendGridClient(_apiKey);
        var from = new EmailAddress(_fromEmail, _fromName);
        var to = new EmailAddress(email);
        var msg = MailHelper.CreateSingleEmail(from, to, motivo, mensaje, $"<p>{mensaje}</p>");

        var response = await client.SendEmailAsync(msg);
        return response.StatusCode == System.Net.HttpStatusCode.Accepted;
    }
}
