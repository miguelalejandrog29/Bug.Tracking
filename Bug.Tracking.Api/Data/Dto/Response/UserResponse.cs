namespace Bug.Tracking.Api.Data.Dto.Response
{
    public class UserResponse
    {
        public long Id { get; init; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
    }
}
