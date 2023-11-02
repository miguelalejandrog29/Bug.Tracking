namespace Bug.Tracking.Api.Data.Dto.Response
{
    public class UserBugResponse
    {
        public long Id { get; init; }
        public long ProjectId { get; set; }
        public long UserId { get; set; }
        public required string Description { get; set; }
        public required DateTime CreatedDate { get; set; }
        
    }
}
