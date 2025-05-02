using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using MyDashboard.Model;
using MyDashboard.Model.Exceptions;
using ValidationException = MyDashboard.Model.Exceptions.ValidationException;

namespace MyDashboard.WebApi
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, Constants.UNHANDLED_EXCEPTION_OCCURRED);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        public async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            int statusCode = StatusCodes.Status400BadRequest;
            string errorMessage = Constants.INTERNAL_SERVER_ERROR;
            httpContext.Response.ContentType = "application/json";

            if (ex is ValidationException)
            {
                var validationException = ex as ValidationException;
                statusCode = validationException!.ErrorCode;
                errorMessage = Constants.VALIDATION_ERROR;
                _logger.LogError($"{errorMessage} {validationException!.ErrorCode}: {validationException!.Message}", validationException);

            }
            else if (ex is RepositoryException)
            {
                var repositoryException = ex as RepositoryException;
                statusCode = repositoryException!.ErrorCode;
                errorMessage = Constants.VALIDATION_ERROR;
                _logger.LogError($"{errorMessage} {repositoryException!.ErrorCode}: {repositoryException!.Message}", repositoryException);
            }
            else if (ex is DatabaseException)
            {
                var databaseException = ex as DatabaseException;
                statusCode = databaseException!.ErrorCode;
                errorMessage = Constants.VALIDATION_ERROR;
                _logger.LogError($"{errorMessage} {databaseException!.ErrorCode}: {databaseException!.Message}", databaseException);
            }
            else
            {
                _logger.LogError($"{Constants.UNHANDLED_EXCEPTION}: {ex!.Message}", ex);
            }

            var response = new
            {
                StatusCode = statusCode,
                Message = errorMessage,
                Detail = ex.Message
            };

            var result = JsonSerializer.Serialize(response);
            await httpContext.Response.WriteAsync(result);
        }
    }
}