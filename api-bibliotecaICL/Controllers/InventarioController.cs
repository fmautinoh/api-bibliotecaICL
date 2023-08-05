using api_bibliotecaICL.Repositorio.IRepositorio;
using Api_Inventariobiblioteca.Repositorio.IRepositorio;
using APICL.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
