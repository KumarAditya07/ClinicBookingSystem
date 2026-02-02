using ClinicBooking.API.Common.Response;
using ClinicBooking.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace ClinicBooking.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppException ex)
            {
                await HandleException(context, ex.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                await HandleException(context, "Something went wrong", HttpStatusCode.InternalServerError);
            }
        }


        public async Task HandleException(HttpContext context,string message,HttpStatusCode statusCode)
        {
            var response = new ApiResponse<string>
            {
                Success = false,
                Message = message,
                Data = null

            };

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));

        }


     }
    }

