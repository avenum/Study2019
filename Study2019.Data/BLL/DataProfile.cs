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
                .ForMember(x => x.ImageAvatarId, x => x.MapFrom(y => (y.Avatars.LastOrDefault() != null ? y.Avatars.Last().ImageId : (int?)null)))
                .ReverseMap()
                .ForMember(x => x.IsShared, x => x.MapFrom(y => y.SharedProfile))
                .ForMember(x => x.RegDate, x => x.Ignore())
                .ForMember(x => x.PasswordHash, x => x.Ignore())
                .ForMember(x => x.Salt, x => x.Ignore())
                ;

            CreateMap<DAL.Entities.Image, DTO.ImageDTO>()
                .ReverseMap()
                .ForMember(x => x.Created, x => x.MapFrom(y => DateTime.Now))
                ;

        }
    }
}
