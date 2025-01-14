using AutoMapper;
using Bogus;
using MediatR;
using PlantillaMicroservicioAdo.Aplicacion.Funcionalidad.Libros.Vm;
using PlantillaMicroservicioAdo.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaMicroservicioAdo.Aplicacion.Funcionalidad.Libros.Consulta
{
    internal class BuscarLibroGuidHandler : IRequestHandler<BuscarLibroGuidConsulta, LibroVm>
    {
        private readonly IMapper _mapper;
        public BuscarLibroGuidHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<LibroVm> Handle(BuscarLibroGuidConsulta request, CancellationToken cancellationToken)
        {
            var datosFalso = new Faker<Libro>()
                       .RuleFor(e => e.Id, f => Guid.NewGuid())
                       .RuleFor(e => e.Titulo, f => f.Commerce.ProductName())
                       .RuleFor(e => e.Editorial, f => f.Company.CompanyName())
                       .RuleFor(l => l.FechaPublicacion, f => f.Date.Past(10))
                       .RuleFor(e => e.Autor, f => f.Name.FullName());


            var datosFalsos = datosFalso.Generate(50);
            datosFalsos[0].Id = Guid.Empty;

            var registro = datosFalsos.FirstOrDefault(e => e.Id == request.Guid);


            if (registro is null)
            {
                throw new KeyNotFoundException($"No se encontró un registro con el ID: {request.Guid}");
            }

            var ejemploDto = _mapper.Map<LibroVm>(registro);


            return Task.FromResult(ejemploDto);

        }
    }
}
