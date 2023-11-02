using System.ComponentModel;

namespace Bug.Tracking.Web.Data.Dto.Response
{
    public class UserBugResponse
    {

        public long Id { get; init; }
        [DisplayName("Descripción")]
        public required string Description { get; set; }
        [DisplayName("Creado el")]
        public DateTime CreatedDate { get; set; }

        //Foreign Keys        
        public long ProjectId { get; set; }
        public long UserId { get; set; }

        //Navigation Propertys
        [DisplayName("Proyecto")]
        public ProjectResponse? Project { get; set; }
        [DisplayName("Usuario")]
        public UserResponse? User { get; set; }
    }
}
