using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Behaviours;
using DroneManager.Core.Services;
using DroneManager.FileStorage;
using DroneManager.Infrastructure.Contexts;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DroneManager.Application
{
    public static class ServiceRegistration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddApplicationLayerServices(this IServiceCollection services)
        {
            var applicationAssembly = Assembly.Load("DroneManager.Application");

            services.AddValidatorsFromAssembly(applicationAssembly);

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(applicationAssembly);
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<DbContextWrite>());

            services.AddAutoMapper(LoadMapperAssemblies());

            services.AddProjectServices();
        }

        private static void AddProjectServices(this IServiceCollection services)
        {
            services.AddSingleton<ISqlGuidGenerator, SequentialGuidGeneratorService>();
            services.AddTransient<IFileStorageService, FileStorageService>();
        }

        private static Assembly[] LoadMapperAssemblies()
        {
            var core = Assembly.Load("DroneManager.Core");
            var domain = Assembly.Load("DroneManager.Domain");
            var application = Assembly.Load("DroneManager.Application");

            return new Assembly[] { core, domain, application };
        }
    }
}
