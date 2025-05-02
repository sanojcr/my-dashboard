using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDashboard.Model.Entities.Entities;

namespace MyDashboard.Repository.Interface
{
    public interface ILoggerRepository
    {
        void AddLogToDatabase(ErrorLog error);
    }
}
