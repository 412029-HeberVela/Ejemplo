using System.ComponentModel.DataAnnotations;

namespace ParcialWebApi.DTOs
{
    public class CreateRequest
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(3)]
        public string Simbolo { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El valor debe ser mayor a 0")]
        public double ValorActual { get; set; }

        [Required]
        public int Categoria { get; set; }

        //Id lo genera la DB.
        //Estado se setea automáticamente como "H" al crear (alta lógica).
        //UltimaActualizacion se setea con DateTime.Now
    }
}
