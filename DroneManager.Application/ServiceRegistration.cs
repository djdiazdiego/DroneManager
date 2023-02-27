using DroneManager.Core.Abstractions.Persistence;
using DroneManager.Core.Behaviours;
using DroneManager.Core.Data.Contexts;
using DroneManager.Core.Services;
using DroneManager.FileStorage;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DroneManager.Application
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceRegistration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddApplicationLayerServices(this WebApplicationBuilder builder)
        {
            //services.AddAuthenticationService(configuration);

            //services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));

            var applicationAssembly = Assembly.Load("DroneManager.Application");

            builder.Services.AddValidatorsFromAssembly(applicationAssembly);

            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(applicationAssembly);
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            builder.Services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<DbContextWriteBase>());

            builder.Services.AddAutoMapper(LoadMapperAssemblies());

            builder.Services.AddProjectServices();
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

        ///// <summary>
        ///// Add authentication service.
        ///// </summary>
        ///// <param name="services"></param>
        ///// <param name="configuration"></param>
        //private static void AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var configurationSection = configuration.GetSection(nameof(JWTSettings));
        //    var jwtSettings = configurationSection.Get<JWTSettings>();
        //    services.Configure<JWTSettings>(configurationSection);

        //    services.AddAuthentication(options =>
        //    {
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //    .AddJwtBearer(o =>
        //    {
        //        o.RequireHttpsMetadata = false;
        //        o.SaveToken = false;

        //        o.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidateLifetime = true,
        //            ClockSkew = TimeSpan.Zero,

        //            ValidIssuer = jwtSettings.Issuer,
        //            ValidAudience = jwtSettings.Audience,
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
        //        };

        //        o.Events = new JwtBearerEvents()
        //        {
        //            OnAuthenticationFailed = c =>
        //            {
        //                c.NoResult();
        //                c.Response.StatusCode = 500;
        //                c.Response.ContentType = "text/plain";
        //                return c.Response.WriteAsync(c.Exception.ToString());
        //            },
        //            OnChallenge = context =>
        //            {
        //                context.HandleResponse();
        //                context.Response.StatusCode = 401;
        //                context.Response.ContentType = "application/json";
        //                var result = JsonConvert.SerializeObject(new Response<string>("You are not Authorized"));
        //                return context.Response.WriteAsync(result);
        //            },
        //            OnForbidden = context =>
        //            {
        //                context.Response.StatusCode = 403;
        //                context.Response.ContentType = "application/json";
        //                var result = JsonConvert.SerializeObject(new Response<string>("You are not authorized to access this resource"));
        //                return context.Response.WriteAsync(result);
        //            },
        //        };
        //    });
        //}

    }
}
