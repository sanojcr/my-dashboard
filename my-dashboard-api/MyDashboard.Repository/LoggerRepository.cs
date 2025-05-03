using MyDashboard.Model.Entities.Entities;
using MyDashboard.Repository.Interface;

namespace MyDashboard.Repository;

public class LoggerRepository: ILoggerRepository
{
    private readonly MyDashboardlDbContext _dashboardlDbContext;

    public LoggerRepository(MyDashboardlDbContext dashboardlDbContext)
    {
        _dashboardlDbContext = dashboardlDbContext;
    }

    public bool AddLogToDatabase(ErrorLog error)
    {
        _dashboardlDbContext
           .ErrorLogs
           .Add(error);

        var result =  _dashboardlDbContext.SaveChanges();
        return result > 0;
    }
}
