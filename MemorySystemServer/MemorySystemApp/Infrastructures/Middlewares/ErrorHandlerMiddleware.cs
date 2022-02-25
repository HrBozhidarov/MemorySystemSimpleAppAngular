namespace MemorySystem.App.Infrastructures.Middlewares
{
    using System;
    using System.Threading.Tasks;

    using MemorySystem.Common;
    using MemorySystem.Controllers.Models.Output;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;

    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ErrorHandlerMiddleware> logger)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                // logger.LogError(
                //    ex,
                //    "Exception occured in {service} at: {time}",
                //    nameof(ErrorHandlerMiddleware),
                //    DateTimeOffset.Now);
                response.StatusCode = StatusCodeConstants.InternalServerError;

                await response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponseModel
                {
                    ErrorMessage = ex.Message,
                    StatusCode = StatusCodeConstants.InternalServerError,
                }));
            }
        }
    }
}
