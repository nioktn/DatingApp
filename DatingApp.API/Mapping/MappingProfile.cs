using AutoMapper;
using DatingApp.API.Controllers.Resources;
using DatingApp.API.Models;

namespace DatingApp.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Note, NoteResource>();
            CreateMap<User, UserResource>();
        }
    }
}