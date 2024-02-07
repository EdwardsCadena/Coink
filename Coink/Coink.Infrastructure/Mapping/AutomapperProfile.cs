using AutoMapper;
using Coink.Core.DTOs;
using Coink.Core.Entities;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coink.Infrastructure.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            // Estas líneas crean mapas bidireccionales entre las entidades y sus correspondientes DTOs
            // //con esto puedo mapear un objeto users a un objeto Usersdtos y viceversa
            CreateMap<User, UserDTOs>();
            CreateMap<UserDTOs, User>();

            // Mapeos para Country
            CreateMap<Country, CountryDTOs>();
            CreateMap<CountryDTOs, Country>();

            // Mapeos para Department
            CreateMap<Department, DepartmentDTOs>();
            CreateMap<DepartmentDTOs, Department>();

            // Mapeos para Municipality
            CreateMap<Municipality, MunicipalityDTOs>();
            CreateMap<MunicipalityDTOs, Municipality>();
        }
    }
}
