using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDashboard.Model.Dtos
{
    public class ErrorLogDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string Source { get; set; }
    }
}
