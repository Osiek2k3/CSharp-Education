using CleanArchitecture.Application.Contracts.Email;
using CleanArchitecture.Application.Contracts.Logging;
using CleanArchitecture.Application.Models.Email;
using CleanArchitecture.Infrastructure.EmailService;
using CleanArchitecture.Infrastructure.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            service.AddTransient<IEmailSender, EmailSender>();
            service.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            return service;
        }
    }
}
