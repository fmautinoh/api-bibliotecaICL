using api_bibliotecaICL.Models.ModelDto;

namespace api_bibliotecaICL.Repositorio.IRepositorio
{
    public interface IUsuarioRepositorio
    {
        Task<LoginResponseDto> Login(UsuarioDto LgDto);
    }
}
