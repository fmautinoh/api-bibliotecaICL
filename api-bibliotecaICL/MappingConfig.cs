using api_bibliotecaICL.Models;
using api_bibliotecaICL.Models.ModelDto;
using Api_Inventariobiblioteca.Models.ModelDto;
using AutoMapper;
namespace api_bibliotecaICL
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Autore, AutorDto>().ReverseMap();
            CreateMap<Autore, AutorCreatedDto>().ReverseMap();
            CreateMap<LibroDto, VLibro>().ReverseMap();
            CreateMap<TipoAutorDto, TipoAutor>().ReverseMap();
            CreateMap<TipoLibroDto, TipoLibro>().ReverseMap();
            CreateMap<LibrosAutore, LibroAutorCreatedDto>().ReverseMap();




        }
    }
}
