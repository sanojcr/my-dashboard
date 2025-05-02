using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyDashboard.Model.Dtos;
using MyDashboard.Model.Entities.Entities;

namespace MyDashboard.Model
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<LoginDto, User>()
                .ForMember(d => d.PasswordHash,
                o => o.MapFrom(s => s.Password));
            CreateMap<RegisterDto, User>()
                .ForMember(d => d.PasswordHash,
                o => o.MapFrom(s => s.Password));
            CreateMap<ErrorLog, ErrorLogDto>();
            CreateMap<ErrorLogDto, ErrorLog>();
            CreateMap<RefreshToken, RefreshTokenDto>();
            CreateMap<RefreshTokenDto, RefreshToken>();
        }
    }
}
