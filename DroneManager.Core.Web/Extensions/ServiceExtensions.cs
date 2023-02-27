using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace DroneManager.Core.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddWebServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerService();
            builder.Services.AddControllerService();
            builder.Services.AddApiVersioningService();
        }

        /// <summary>
        /// Register swagger
        /// </summary>
        /// <param name="services"></param>
        private static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(t => t.FullName);

                options.UseInlineDefinitionsForEnums();
                options.DescribeAllParametersInCamelCase();
                options.UseAllOfToExtendReferenceSchemas();

                var versions = new[] { "v1" };

                foreach (var version in versions)
                {
                    options.SwaggerDoc(version, new OpenApiInfo
                    {
                        Title = "Drone Manager",
                        Version = version,
                        Description = "API Documentation",
                        Contact = new OpenApiContact
                        {
                            Name = "Dayron Jesús Díaz Diego",
                            Email = "dj.diazdiego@gmail.com"
                        }
                    });
                }

                var xmlFile = $"{Assembly.Load("DroneManager.Server").GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }

        /// <summary>
        /// Register api versioning
        /// </summary>
        /// <param name="services"></param>
        private static void AddApiVersioningService(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
            });
        }

        /// <summary>
        /// Register mvc builder
        /// </summary>
        /// <param name="services"></param>
        private static void AddControllerService(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });
        }
    }
}
