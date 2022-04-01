using _3dMedia.Test.Executor.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace _3dMedia.Test.Executor.Extensions
{
    public static class CommonExtensions
    {
        public static IActionResult GetSuccessResponse(this object resContent)
        {
            var handler = new MessageHandler(data: resContent);

            return new OkObjectResult(handler);
        }

        public static IActionResult GetErrorResponse(this string errorMsg)
        {
            var handler = new MessageHandler(errorMessage: errorMsg, error: true);

            return new OkObjectResult(handler);
        }

        public static IActionResult GetServerErrorResponse(this Exception ex)
        {
            var handler = new MessageHandler(errorMessage: "Server Error Ocurred", data: $"Exception Message: { ex?.Message } \nStack Trace: { ex?.StackTrace }", error: true);
            return new OkObjectResult(handler);
        }
    }
}