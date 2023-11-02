using System.ComponentModel.DataAnnotations;

namespace Bug.Tracking.Api.Data.Dto.Request
{
    public class UserRequest
    {
        [Required(ErrorMessage = "{0} es requerido.")]
        //[MaxLength(150, ErrorMessage = "{0} debe ser un string con un máximo de {1} caracteres.")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "{0} es requerido.")]
        public required string Surname { get; set; }
    }
}
