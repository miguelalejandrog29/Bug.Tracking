namespace Bug.Tracking.Web.Data.Dto.Response
{
    public class ProjectResponse
    {
        public long Id { get; init; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        //Navigation Propertys
        public List<UserBugResponse> UserBugs { get; set; } = null!;
    }
}
