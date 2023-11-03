namespace Bug.Tracking.Web.Data.Dto.Filter
{
    public class UserBugFilter
    {
        public long? ProjectId { get; set; }
        public long? UserId { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
