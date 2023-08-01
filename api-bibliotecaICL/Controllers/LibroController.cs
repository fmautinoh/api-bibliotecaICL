using Api_Inventariobiblioteca.Models.ModelDto;
using Api_Inventariobiblioteca.Models;
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
    public class LibroController : ControllerBase
    {
        private readonly ILibroRepositorio _Librorrepo;
        private readonly Ivlibrorepositorio _vistalibrorepo;
        private readonly ILibroxAutorRepositorio _LibroAutorepo;
        private readonly IMapper _mapper;
        protected APIResponse _apiResponse;

        public LibroController(ILibroRepositorio libroRepositorio, Ivlibrorepositorio vistalibrorepo, ILibroxAutorRepositorio LibroAutorepo,  IMapper mapper)
        {
            _apiResponse = new APIResponse();
            _Librorrepo = libroRepositorio;
            _vistalibrorepo = vistalibrorepo;
            _LibroAutorepo = LibroAutorepo;
            _mapper = mapper;
        }

        //[HttpGet]
        //[Route("/ListaLibro")]
        //[ProducesResponseType(200)]//ok
        //[ProducesResponseType(400)]//badreq
        //[ProducesResponseType(404)]//no found
        //public async Task<ActionResult<APIResponse>> GetLibro()
        //{
        //    try
        //    {
        //        IEnumerable<LibroDto> autorlist = (IEnumerable<LibroDto>) await _vistalibrorepo.ListObjetos();
        //        _apiResponse.Alertmsg = "Listado Exitosamente";
        //        _apiResponse.Resultado = _mapper.Map<IEnumerable<LibroDto>>(autorlist);
        //        _apiResponse.StatusCode = HttpStatusCode.OK;
        //        return Ok(_apiResponse);
        //    }
        //    catch (Exception ex)
        //    {
        //        _apiResponse.IsSuccess = false;
        //        _apiResponse.ErrorMessage = new List<string> { ex.ToString() };
        //    }
        //    return _apiResponse;
        //}


        [HttpPost]
        [Route("/CreateLibro")]
        [ProducesResponseType(200)]//ok
        [ProducesResponseType(400)]//badreq
        [ProducesResponseType(500)]//Internal Error
        [ProducesResponseType(404)]//no found
        [ProducesResponseType(204)]//No content
        [ProducesResponseType(409)]//no found


        public async Task<ActionResult<APIResponse>> CrearLibro([FromBody] LibroCreatedDto ModelLibro)
        {
            try
            {
                if (ModelLibro == null)
                {
                    _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    _apiResponse.IsSuccess = false;

                    return BadRequest(_apiResponse);
                }

                var isexistente = await _Librorrepo.ListObjetos(c => c.NombreLib == ModelLibro.NombreLib);
                if (isexistente.Count != 0)
                {
                    var message = "Libro Existente";
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

                var arreglolenght = ModelLibro.Autor.Count;

                Libro modelcreate = new()
                {
                    NombreLib= ModelLibro.NombreLib,
                    TipoId= ModelLibro.TipoId,
                    Edicion= ModelLibro.Edicion,
                    Año = ModelLibro.Año,
                    Editorial= ModelLibro.Editorial
                };
                await _Librorrepo.Crear(modelcreate);
                Libro creado = await _Librorrepo.Listar(c => c.NombreLib == ModelLibro.NombreLib, tracked:false);
                foreach (var autorId in ModelLibro.Autor)
                {
                    // Crear el registro en la tabla LibrosAutores
                    LibrosAutore modelautorlibro = new ()
                    {
                        LibroId = creado.LibroId,
                        AutorId = autorId
                    };
                    LibrosAutore AutoreCrt = _mapper.Map<LibrosAutore>(modelautorlibro);
                    await _LibroAutorepo.Crear(modelautorlibro);
                }
                _apiResponse.Alertmsg = "Autor Creado Exitosamente";
                _apiResponse.Resultado = modelcreate;
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

    }
}
