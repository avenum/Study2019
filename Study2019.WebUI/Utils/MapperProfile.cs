using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study2019.WebUI.Utils
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Data.DTO.UserDTO, Models.RegUserModel>()
                .ForMember(x => x.Birtdate, x => x.MapFrom(y => y.BirtDate))
                .ReverseMap()
                .ForMember(x => x.BirtDate, x => x.MapFrom(y => y.Birtdate))
                .ForMember(x => x.RegDate, x => x.MapFrom(y => DateTime.Now))
                ;

            CreateMap<Data.DTO.UserDTO, Models.UserModel>()
                .ForMember(x => x.Birtdate, x => x.MapFrom(y => y.BirtDate))
                .ReverseMap()
                .ForMember(x => x.BirtDate, x => x.MapFrom(y => y.Birtdate))
                ;


        }
    }

}
