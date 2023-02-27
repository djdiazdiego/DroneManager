using DroneManager.Core.Web.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace DroneManager.Core.Web.Extensions
{
    public static class AppExtensions
    {
        /// <summary>
        /// Add swagger middleware
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Drone Manager v1");
                c.RoutePrefix = string.Empty;
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);

            });
        }

        /// <summary>
        /// Middeware for catch flow errors
        /// </summary>
        /// <param name="app"></param>
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app) =>
            app.UseMiddleware<ErrorHandlerMiddleware>();

    }
}
