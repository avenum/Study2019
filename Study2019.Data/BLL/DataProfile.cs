using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study2019.Data.BLL
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<DAL.Entities.User, DTO.UserDTO>()
                .ForMember(x => x.SharedProfile, x => x.MapFrom(y => y.IsShared))
                .ReverseMap()
                .ForMember(x => x.IsShared, x => x.MapFrom(y => y.SharedProfile));

        }
    }
}
