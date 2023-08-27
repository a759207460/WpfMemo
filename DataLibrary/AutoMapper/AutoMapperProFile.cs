using AutoMapper;
using DataLibrary.Domain;
using DataLibrary.Dtos;

namespace DataLibrary.AutoMapper
{
    public class AutoMapperProFile : Profile
    {
        public AutoMapperProFile()
        {
            CreateMap<ToDo, ToDoDto>().ReverseMap();
            CreateMap<Menmo, MenmoDto>().ReverseMap();
        }
    }
}
