using HackerApp.Client.Infrastructure.Invariance;
using JetBrains.Annotations;

namespace HackerApp.Infrastructure.ExceptionHandling
{
    [PublicAPI]
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            Guard.ObjectNotNull(() => app);
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            return app;
        }
    }
}