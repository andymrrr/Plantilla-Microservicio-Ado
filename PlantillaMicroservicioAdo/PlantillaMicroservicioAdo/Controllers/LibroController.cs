using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PlantillaMicroservicioAdo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LibroController(IMediator mediator)
        {
            _mediator = mediator;
        }

    }
}
