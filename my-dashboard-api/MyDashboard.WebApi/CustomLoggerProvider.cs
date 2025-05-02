using MyDashboard.Service.Interface;

namespace MyDashboard.WebApi
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        private readonly ILoggerService _loggingService;
        public CustomLoggerProvider(ILoggerService loggingService) 
        {
            _loggingService = loggingService;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLogger(categoryName, _loggingService);
        }

        public void Dispose() { }
    }
}
