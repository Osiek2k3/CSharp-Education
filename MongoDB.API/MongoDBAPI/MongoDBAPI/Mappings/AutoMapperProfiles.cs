using AutoMapper;
using MongoDBAPI.Models;
using MongoDBAPI.Models.Dto;

namespace MongoDBAPI.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        { 
            CreateMap<BookModel,BookModelDto>().ReverseMap();
        }
    }
}
