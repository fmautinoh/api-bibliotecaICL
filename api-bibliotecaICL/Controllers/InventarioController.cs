using api_bibliotecaICL.Models;
using api_bibliotecaICL.Repositorio.IRepositorio;
using Api_Inventariobiblioteca.Models.ModelDto;
using Api_Inventariobiblioteca.Repositorio.IRepositorio;
using APICL.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace api_bibliotecaICL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly IvInventarioRepositorio _vInvenrepo;
        private readonly IInventarioRepositorio _Invrepo;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;
        public InventarioController(IInventarioRepositorio inventarioRepositorio ,IvInventarioRepositorio vistarepo, IMapper mapper)
        {
            _vInvenrepo = vistarepo;
            _apiResponse = new APIResponse();
            _mapper = mapper;
            _Invrepo = inventarioRepositorio;
        }

        [HttpGet]
        [Route("/ListaInventario")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public async Task<ActionResult<APIResponse>> GetInventario()
        {
            try
            {
                IEnumerable<VInventario> Invlist = await _vInvenrepo.ListObjetos();
                _apiResponse.Alertmsg = "Listado Exitosamente";
                _apiResponse.Resultado = Invlist;
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
            }
            return _apiResponse;
        }
    }
}
