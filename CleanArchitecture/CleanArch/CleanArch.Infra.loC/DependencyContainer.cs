using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using CleanArch.Application.Interfaces;
using CleanArch.Application.Services;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Data.Repository;
using CleanArch.Infra.Bus;
using CleanArch.Domain.Core.Bus;
using MediatR;
using CleanArch.Domain.Commands;
using CleanArch.Domain.CommandHandlers;
using CleanArch.Infra.Data.Context;

namespace CleanArch.Infra.loC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            services.AddScoped<IRequestHandler<CreateCourseCommand, bool>,CourseCommandHandler>();

            services.AddScoped<ICourseServices,CourseService>();

            services.AddScoped<ICourseRepository, CourseRepository>();

            services.AddScoped<UniversityDBContext>();
        }
    }
}
