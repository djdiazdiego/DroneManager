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

                //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer",
                //    BearerFormat = "JWT",
                //    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                //});
                //options.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Type = ReferenceType.SecurityScheme,
                //                Id = "Bearer",
                //            },
                //            Scheme = "Bearer",
                //            Name = "Bearer",
                //            In = ParameterLocation.Header,
                //        }, new List<string>()
                //    },
                //});

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
                // Specify the default API Version as 1.0
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
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

        ///// <summary>
        ///// Register authentication
        ///// </summary>
        ///// <param name="services"></param>
        ///// <param name="configuration"></param>
        //public static void AddAuthenticationExtension(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.AddAuthentication(x =>
        //    {
        //        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    }).AddJwtBearer();
        //}

    }
}
