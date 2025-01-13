

namespace PlantillaMicroservicioAdo.Modelo
{
    public class Libro
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Editorial { get; set; }
        public string Autor { get; set; }
        public DateTime FechaPublicacion { get; set; }
    }
}
