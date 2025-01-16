using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlantillaMicroservicioAdo.Aplicacion.Funcionalidad.Libros.Comando.InicializarLibros;
using PlantillaMicroservicioAdo.Aplicacion.Funcionalidad.Libros.Consulta.BuscarLibroId;
using PlantillaMicroservicioAdo.Aplicacion.Funcionalidad.Libros.Vm;
using System.Net;

namespace PlantillaMicroservicioAdo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly IMediator _mediador;
        public LibroController(IMediator mediator)
        {
            _mediador = mediator;
        }
        [HttpGet("InicializarLibro", Name = "InicializarLibro")]
        [ProducesResponseType(typeof(bool), (int)(HttpStatusCode.OK))]
        public async Task<ActionResult<bool>> InicializarLibro()
        {
            var consulta = new InicializarLibrosComando();
            return await _mediador.Send(consulta);
        }
        [HttpGet("BuscarLibroId/{id}", Name = "BuscarLibroId")]
        [ProducesResponseType(typeof(LibroVm), (int)(HttpStatusCode.OK))]
        public async Task<ActionResult<LibroVm>> BuscarLibroId(int id)
        {
            var consulta = new BuscarLibroIdConsulta(id);
            return await _mediador.Send(consulta);
        }

    }
}
