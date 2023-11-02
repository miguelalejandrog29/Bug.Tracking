namespace Bug.Tracking.Web.Data.Dto.Response
{
    public class UserResponse
    {

        public long Id { get; init; }
        public required string Name { get; set; }
        public required string Surname { get; set; }

        //Navigation Propertys
        public List<UserBugResponse> UserBugs { get; set; } = null!;
    }
}
