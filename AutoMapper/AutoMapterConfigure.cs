using AutoMapper;
using Practice1.Dtos.InputDto;
using Practice1.Models;

namespace Practice1.AutoMapper
{
    public class AutoMapterConfigure : Profile
    {
        public AutoMapterConfigure()
        {
            CreateMap<BookInputDto, Book>();
            CreateMap<BookUpdateInputDto, Book>();
        }

    }
}
