namespace WebApi.BLL.Services.EmailService
{
    public interface IEmailService
    {
        Task SendMessageAsync(string to, string subject, string body, bool isHtnl = false);
    }
}
