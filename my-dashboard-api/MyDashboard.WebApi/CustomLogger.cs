using MyDashboard.Model.Dtos;
using MyDashboard.Service;
using MyDashboard.Service.Interface;

namespace MyDashboard.WebApi
{
    public class CustomLogger: ILogger
    {
        private readonly string _categoryName;
        private readonly ILoggerService _loggerService;

        public CustomLogger(string categoryName, ILoggerService loggerService)
        {
            _categoryName = categoryName;
            _loggerService = loggerService;
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId,
            TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (IsEnabled(logLevel))
            {
                var log = new ErrorLogDto
                {
                    Timestamp = DateTime.UtcNow,
                    LogLevel = logLevel.ToString(),
                    Message = formatter(state, exception),
                    Exception = exception?.ToString(),
                    Source = _categoryName
                };

                _loggerService.AddLogToDatabase(log);
            }
        }
    }
}
