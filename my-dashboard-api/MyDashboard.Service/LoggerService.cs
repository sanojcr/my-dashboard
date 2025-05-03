using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyDashboard.Model.Dtos;
using MyDashboard.Model.Entities.Entities;
using MyDashboard.Repository;
using MyDashboard.Repository.Interface;
using MyDashboard.Service.Interface;

namespace MyDashboard.Service
{
    public class LoggerService: ILoggerService
    {
        private readonly ILoggerRepository _loggerRepository;
        private readonly IMapper _mapper;

        public LoggerService(ILoggerRepository loggerRepository, IMapper mapper)
        {
            _loggerRepository = loggerRepository;
            _mapper = mapper;
        }

        public bool AddLogToDatabase(ErrorLogDto error)
        {
            return _loggerRepository
                .AddLogToDatabase(_mapper.Map<ErrorLog>(error));
        }
    }
}
