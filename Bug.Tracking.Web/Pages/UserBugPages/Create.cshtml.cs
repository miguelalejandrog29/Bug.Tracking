using Bug.Tracking.Web.Data.Dto.Request;
using Bug.Tracking.Web.Data.Dto.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bug.Tracking.Web.Pages.UserBugPages
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CreateModel> _logger;

        [BindProperty]
        public UserBugRequest UserBug { get; set; } = default!;

        public CreateModel(IHttpClientFactory httpClientFactory, ILogger<CreateModel> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("BugTrackingApi");

            var projectList = await client.GetFromJsonAsync<IList<ProjectResponse>>("project");

            if (projectList != null)
            {
                ViewData["ProjectId"] = new SelectList(projectList, "Id", "Name");
            }

            var userList = await client.GetFromJsonAsync<IList<UserResponse>>("user");

            if (userList != null)
            {
                ViewData["UserId"] = new SelectList(from s in userList
                                                    select new
                                                    {
                                                        Id = s.Id,
                                                        FullName = s.Name + " " + s.Surname
                                                    }, "Id", "FullName");
            }

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient("BugTrackingApi");

            if (!ModelState.IsValid || UserBug == null)
            {
                return Page();
            }

            var response = await client.PostAsJsonAsync<UserBugRequest>("bug", UserBug);

            if (!response.IsSuccessStatusCode)
            {
                var errorsJson = response.Content.ReadAsStringAsync().Result;

                _logger.LogError(errorsJson);

                ViewData["ErrorMessage"] = $"Ocurrió un problema con la API: {response.ReasonPhrase}";
                return await OnGetAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
