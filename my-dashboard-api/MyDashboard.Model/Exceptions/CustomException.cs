namespace MyDashboard.Model.Exceptions
{
    public class CustomException : Exception
    {
        public int ErrorCode { get; set; }

        public CustomException(string message) : base(message)
        {
        }

        public CustomException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public CustomException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public CustomException(string message, Exception innerException, int errorCode) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}