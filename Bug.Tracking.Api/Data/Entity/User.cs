using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bug.Tracking.Api.Data.Entity
{
    public class User
    {
        [Key]
        public long Id { get; init; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Surname { get; set; }

        //Navigation Propertys
        public List<UserBug> UserBugs { get; set; } = null!;
    }
}
