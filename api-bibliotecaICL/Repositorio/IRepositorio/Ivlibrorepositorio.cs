using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_bibliotecaICL.Models;
using Api_Inventariobiblioteca.Models;

namespace Api_Inventariobiblioteca.Repositorio.IRepositorio
{
    public interface Ivlibrorepositorio : IReaderRepositorio<VLibro>
    {
        // Add a new method to fetch books with multiple authors
        Task<List<VLibro>> ListLibrosConAutores();
    }
}
