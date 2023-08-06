using api_bibliotecaICL.Models.ModelDto;
using api_bibliotecaICL.Repositorio.IRepositorio;
using Api_Inventariobiblioteca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace api_bibliotecaICL.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly DatabaseContext _databaseContext;
        private string secretkey;

        public UsuarioRepositorio(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            secretkey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        public static string CalculateMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public async Task<LoginResponseDto> Login(UsuarioDto LgDto)
        {
            var contraseña = CalculateMD5Hash(LgDto.Pwsd); 
            var usuario = await _databaseContext.Usuarios.FirstOrDefaultAsync(u => u.Usu.ToLower() == LgDto.Usu.ToLower() && u.Pwsd == contraseña);

            if(usuario == null)
            {
                return new LoginResponseDto()
                {
                    Token = "",
                    Usuario = null
                };
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretkey);
            var tokenDescriptor = new SecurityTokenDescriptor { 
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
                    new Claim(ClaimTypes.Name,usuario.Usu.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDto loginResponseDto = new()
            {
                Token = tokenHandler.WriteToken(token),
                Usuario= usuario
            };
            return loginResponseDto;
        }
    }
}
