using api_bibliotecaICL.Models.ModelDto;
using api_bibliotecaICL.Repositorio.IRepositorio;
using APICL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace api_bibliotecaICL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuariorepo;
        private APIResponse _apiResponse;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuariorepo = usuarioRepositorio;
            _apiResponse = new ();
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UsuarioDto model)
        {
            var loginresponse = await _usuariorepo.Login(model);
            if (loginresponse.Usuario == null || string.IsNullOrEmpty(loginresponse.Token))
            {
                _apiResponse.StatusCode= HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess=false;
                _apiResponse.ErrorMessage.Add("UserName o Password son Incorrectos");
                _apiResponse.Alertmsg = "UserName o Password son Incorrectos";
                return BadRequest(_apiResponse);
            }
            _apiResponse.IsSuccess = true;
            _apiResponse.Alertmsg = "Login Success";
            _apiResponse.StatusCode = HttpStatusCode.OK;
            _apiResponse.Resultado = loginresponse;
            return Ok(_apiResponse);
        }
    }
}
