using api_bibliotecaICL.Models;
using api_bibliotecaICL.Models.ModelDto;
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

        [HttpGet]
        [Route("/ListaInventario/{idInv:int}")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public async Task<ActionResult<APIResponse>> GetAutorporID(int idInv)
        {
            try
            {
                if (idInv == 0)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;

                    return BadRequest(_apiResponse);
                }
                var inven = await _vInvenrepo.ListObjetos(c => c.InventarioId == idInv);
                if (inven == null)
                {
                    _apiResponse.Alertmsg = "Libro no encontrado no Encontrado";
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    _apiResponse.IsSuccess = false;

                    return NotFound(_apiResponse);
                }
                _apiResponse.Alertmsg = "Listado Exitosamente";
                _apiResponse.Resultado = inven;
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

        [HttpPost]
        [Route("/CreateInventario")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(500)]//Internal Error
        [ProducesResponseType(404)]//no found
        [ProducesResponseType(204)]//No content
        [ProducesResponseType(409)]//no found

        public async Task<ActionResult<APIResponse>> CreateInventario([FromBody] InventarioDto ModelInv)
        {
            try
            {
                if (ModelInv == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;

                    return BadRequest(_apiResponse);
                }


                if (!ModelState.IsValid)
                {
                    var message = "Campos Invalidos";
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;
                    _apiResponse.Resultado = ModelState;
                    _apiResponse.Alertmsg = message;
                    return BadRequest(_apiResponse);
                }

                InventarioLibro InventCrt = _mapper.Map<InventarioLibro>(ModelInv);
                await _Invrepo.Crear(InventCrt);
                _apiResponse.Alertmsg = "Stock agregado Exitosamente";
                _apiResponse.Resultado = InventCrt;
                _apiResponse.StatusCode = HttpStatusCode.Created;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
            }
            return _apiResponse;
        }

        [HttpPut]
        [Route("/updateInventario/{idInv:int}")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(500)]//Internal Error
        [ProducesResponseType(404)]//no found
        [ProducesResponseType(204)]//No content

        public async Task<IActionResult> UpdateInventario(int idInv, [FromBody] InventarioDto ModelInv)
        {
            if (idInv == 0)
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                return BadRequest(_apiResponse);
            }
            InventarioLibro mdInvUp = new()
            {
                InventarioId = idInv,
                LibroId = ModelInv.LibroId,
                Codigo = ModelInv.Codigo,
                EstadoId = ModelInv.EstadoId
            };

            await _Invrepo.Actualizar(mdInvUp);
            _apiResponse.Alertmsg = "Inventario Actualizado Correctamente Exitosamente";
            _apiResponse.StatusCode = HttpStatusCode.NoContent;
            return Ok(_apiResponse);
        }





    }
}
