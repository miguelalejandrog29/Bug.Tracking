using Bug.Tracking.Web.Data.Dto.Filter;
using Bug.Tracking.Web.Data.Dto.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel;

namespace Bug.Tracking.Web.Pages.UserBugPages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IList<UserBugResponse> UserBug { get; set; } = default!;

        #region - Search -
        [BindProperty(SupportsGet = true)]
        public string? Description { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }
        [BindProperty(SupportsGet = true)]
        public long? ProjectId { get; set; }
        [BindProperty(SupportsGet = true)]
        [DisplayName("Usuario")]
        public long? UserId { get; set; }

        public SelectList? Projects { get; set; }
        public SelectList? Users { get; set; }
        #endregion

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("BugTrackingApi");
            var param = new Dictionary<string, string?>();
            var uri = "bug";

            if (!string.IsNullOrWhiteSpace(Description))
            {
                param.Add(nameof(UserBugFilter.Description), Description);
            }

            if (StartDate != null)
            {
                param.Add(nameof(UserBugFilter.StartDate), StartDate.Value.ToString("yyyy-MM-dd"));
            }

            if (EndDate != null)
            {
                param.Add(nameof(UserBugFilter.EndDate), EndDate.Value.ToString("yyyy-MM-dd"));
            }

            if (ProjectId != null && ProjectId != 0 )
            {
                param.Add(nameof(UserBugFilter.ProjectId), ProjectId.ToString());
            }

            if (UserId != null && UserId != 0)
            {
                param.Add(nameof(UserBugFilter.UserId), UserId.ToString());
            }

            if (param.Count != 0)
            {
                uri = QueryHelpers.AddQueryString("bug/search", param);
            }

            var response = await client.GetFromJsonAsync<IList<UserBugResponse>>(uri);

            if (response != null) UserBug = response;

            //Selects
            var projectList = await client.GetFromJsonAsync<IList<ProjectResponse>>("project");

            if (projectList != null)
            {
                Projects = new SelectList(projectList, "Id", "Name");
            }

            var userList = await client.GetFromJsonAsync<IList<UserResponse>>("user");

            if (userList != null)
            {
                Users = new SelectList(from s in userList
                                       select new
                                       {
                                           Id = s.Id,
                                           FullName = s.Name + " " + s.Surname
                                       }, "Id", "FullName");
            }
        }
    }
}
