using Microsoft.AspNetCore.Http;

namespace MyDashboard.Model.Exceptions
{
    public class RepositoryException : CustomException
    {
        public RepositoryException(string message, int errorCode = StatusCodes.Status500InternalServerError) : base(message, errorCode)
        {
        }
        
        public RepositoryException(string message, Exception innerException, int errorCode = 5001) : base(message, innerException, errorCode)
        {
        }
    }

}