namespace Backend.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string name, string email, string motivo, string mensaje);
    }
}
