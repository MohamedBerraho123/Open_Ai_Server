using AutoMapper;
using OpenAI_UIR.Dtos;
using OpenAI_UIR.Models;

namespace OpenAI_UIR.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Admin,AdminDto>().ReverseMap();
        }
    }
}
