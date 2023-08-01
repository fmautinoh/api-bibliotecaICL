using Api_Inventariobiblioteca.Models;
using Api_Inventariobiblioteca.Models.ModelDto;
using Api_Inventariobiblioteca.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using APICL.Models;
using api_bibliotecaICL.Models;

namespace Api_Inventariobiblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IAutorRepositorio _autorrepo;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;

        public AutorController(IAutorRepositorio autorRepositorio, IMapper mapper)
        {
            _apiResponse = new APIResponse();
            _autorrepo = autorRepositorio;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("/ListaAutores")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public async Task<ActionResult<APIResponse>> GetAutor()
        {
            try
            {
                IEnumerable<Autore> autorlist = await _autorrepo.ListObjetos();
                _apiResponse.Alertmsg = "Listado Exitosamente";
                _apiResponse.Resultado = _mapper.Map<IEnumerable<AutorDto>>(autorlist);
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
        [Route("/ListaAutor/{idaut:int}")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(404)]//no found
        public async Task<ActionResult<APIResponse>> GetAutorporID(int idaut)
        {
            try
            {
                if (idaut == 0)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;

                    return BadRequest(_apiResponse);
                }
                var autor = await _autorrepo.ListObjetos(c => c.AutorId == idaut);
                if (autor == null)
                {
                    _apiResponse.Alertmsg = "Autor no Encontrado";
                    _apiResponse.StatusCode = HttpStatusCode.NotFound;
                    _apiResponse.IsSuccess = false;

                    return NotFound(_apiResponse);
                }
                _apiResponse.Alertmsg = "Listado Exitosamente";
                _apiResponse.Resultado = _mapper.Map<IEnumerable<AutorDto>>(autor);
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
        [Route("/CreateAutor")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(500)]//Internal Error
        [ProducesResponseType(404)]//no found
        [ProducesResponseType(204)]//No content
        [ProducesResponseType(409)]//no found


        public async Task<ActionResult<APIResponse>> CrearAutor([FromBody] AutorCreatedDto ModelAutor)
        {
            try
            {
                if (ModelAutor == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;

                    return BadRequest(_apiResponse);
                }

                var isexistente = await _autorrepo.ListObjetos(c => c.NombreAutor == ModelAutor.NombreAutor);
                if (isexistente.Count != 0)
                {
                    var message = "Autor Existente";
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;
                    _apiResponse.Alertmsg = message;
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

                Autore AutoreCrt = _mapper.Map<Autore>(ModelAutor);
                await _autorrepo.Crear(AutoreCrt);
                _apiResponse.Alertmsg = "Autor Creado Exitosamente";
                _apiResponse.Resultado = AutoreCrt;
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
        [Route("/updateAutor/{idaut:int}")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(500)]//Internal Error
        [ProducesResponseType(404)]//no found
        [ProducesResponseType(204)]//No content

        public async Task<IActionResult> UpdateLibro(int idaut, [FromBody] AutorCreatedDto ModelAutor)
        {
            if (idaut == 0)
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                _apiResponse.IsSuccess = false;
                return BadRequest(_apiResponse);
            }
            Autore mdAutorUp = new()
            {
                AutorId = idaut,
                NombreAutor=ModelAutor.NombreAutor,
                TipoAutorId= ModelAutor.TipoAutorId
            };

            await _autorrepo.Actualizar(mdAutorUp);
            _apiResponse.Alertmsg = "Autor Actualizado Correctamente Exitosamente";
            _apiResponse.StatusCode = HttpStatusCode.NoContent;
            return Ok(_apiResponse);
        }



    }
}
