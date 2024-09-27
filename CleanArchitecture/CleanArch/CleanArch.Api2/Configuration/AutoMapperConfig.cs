using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using CleanArch.Application.AutoMapper;

namespace CleanArch.Api2.Configuration
{
    public static class AutoMapperConfig
    {
        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperConfiguration));
            AutoMapperConfiguration.RegisterMappings();
        }
    }
}