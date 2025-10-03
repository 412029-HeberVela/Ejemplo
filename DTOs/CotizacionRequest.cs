using System.ComponentModel.DataAnnotations;

namespace ParcialWebApi.DTOs
{
    public class CotizacionRequest
    {
        [Range(0.01, double.MaxValue, ErrorMessage = "El valor debe ser mayor a 0")]
        public double ValorActual { get; set; }
        public DateTime UltimaActualizacion { get; set; }
    }
}
