using System.ComponentModel.DataAnnotations;

namespace Bug.Tracking.Web.Data.Dto.Request
{
    public class UserBugRequest
    {
        public long? Id { get; set; }
        [Required(ErrorMessage = "{0} es requerido.")]
        public long ProjectId { get; set; }
        [Required(ErrorMessage = "{0} es requerido.")]
        public long UserId { get; set; }
        [Required(ErrorMessage = "{0} es requerido.")]
        [MaxLength(100, ErrorMessage = "{0} debe ser un string con un máximo de {1} caracteres.")]
        public required string Description { get; set; }
    }
}
