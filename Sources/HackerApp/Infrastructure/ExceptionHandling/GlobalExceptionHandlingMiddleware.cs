using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using HackerApp.Client.Infrastructure.HttpRequests.Dtos;
using JetBrains.Annotations;

namespace HackerApp.Infrastructure.ExceptionHandling
{
    [PublicAPI]
    public class GlobalExceptionHandlingMiddleware(RequestDelegate next)
    {
        [SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods", Justification = "Microsoft Interface")]
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
#pragma warning disable CA1031
            catch (Exception exception)
#pragma warning restore CA1031
            {
                var response = httpContext.Response;
                response.ContentType = MediaTypeNames.Application.Json;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorDto = CreateDtoFromException(exception);
                var serializedServerError = JsonSerializer.Serialize(errorDto);

                await response.WriteAsync(serializedServerError);
            }
        }

        private static Exception GetMostInnerException(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            return ex;
        }

        private ServerErrorDto CreateDtoFromException(Exception exception)
        {
            var mostInnerEx = GetMostInnerException(exception);

            return new ServerErrorDto(mostInnerEx.Message, mostInnerEx.GetType().Name, mostInnerEx.StackTrace ?? string.Empty);
        }
    }
}