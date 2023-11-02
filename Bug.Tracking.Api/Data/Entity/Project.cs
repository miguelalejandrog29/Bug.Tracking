using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bug.Tracking.Api.Data.Entity
{
    public class Project
    {
        [Key]
        public long Id { get; init; }
        [Required]
        public required string Name { get; set; }
        [Required]
        [Column(TypeName = "TEXT")]
        public required string Description { get; set; }

        //Navigation Propertys
        public List<UserBug> UserBugs { get; set; } = null!;
    }
}
