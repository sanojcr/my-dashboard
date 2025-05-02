using Microsoft.AspNetCore.Http;

namespace MyDashboard.Model.Exceptions
{
    public class DatabaseException : CustomException
    {
        public DatabaseException(string message, int errorCode = StatusCodes.Status500InternalServerError) : base(message, errorCode)
        {
        }

        public DatabaseException(string message, Exception innerException, int errorCode = StatusCodes.Status500InternalServerError) : base(message, innerException, errorCode)
        {
        }
    }
}