using Bug.Tracking.Api.Data.Dto.Request;
using Bug.Tracking.Api.Data.Dto.Response;
using Bug.Tracking.Api.Data.Entity;

namespace Bug.Tracking.Api.Data
{
    public static class MappingExtension
    {
        #region User

        public static UserResponse ToResponse(this User entity)
        {
            return new UserResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
            };
        }

        public static User ToEntity(this UserRequest request, long id = 0)
        {
            return new User
            {
                Id = id,
                Name = request.Name,
                Surname = request.Surname
            };
        }

        #endregion

        #region UserBug

        public static UserBugResponse ToResponse(this UserBug entity)
        {
            return new UserBugResponse
            {
                Id = entity.Id,
                ProjectId = entity.ProjectId,
                UserId = entity.UserId,
                Description = entity.Description,
                CreatedDate = entity.CreatedDate,
            };
        }

        public static UserBug ToEntity(this UserBugRequest request, long id = 0)
        {
            return new UserBug
            {
                Id = id,
                ProjectId = request.ProjectId,
                UserId = request.UserId,
                Description = request.Description
            };
        }

        #endregion
    }
}
