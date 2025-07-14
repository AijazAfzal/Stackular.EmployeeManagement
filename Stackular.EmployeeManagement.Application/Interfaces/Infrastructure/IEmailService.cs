using Stackular.EmployeeManagement.Application.Models.Mail;

namespace Stackular.EmployeeManagement.Application.Interfaces.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
        bool SendEmail();
    }
}
