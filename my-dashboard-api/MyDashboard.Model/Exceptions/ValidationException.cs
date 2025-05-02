using Microsoft.AspNetCore.Http;

namespace MyDashboard.Model.Exceptions
{
    public class ValidationException : CustomException 
    {
        public ValidationException(string message, int errorCode = StatusCodes.Status400BadRequest) : base(message, errorCode)
        {
        }

        public ValidationException(string message, Exception innerException, int errorCode = StatusCodes.Status400BadRequest) : base(message, innerException, errorCode)
        {
        }
    }
}