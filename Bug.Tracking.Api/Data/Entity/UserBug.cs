using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bug.Tracking.Api.Data.Entity
{
    public class UserBug
    {
        [Key]
        public long Id { get; init; }
        [Required]
        [MaxLength(100)]
        [Column(TypeName = "TEXT")]
        public required string Description { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        //Foreign Keys        
        public long ProjectId { get; set; }
        public long UserId { get; set; }

        //Navigation Propertys
        [ForeignKey(nameof(ProjectId))]
        public Project? Project { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
    }
}
