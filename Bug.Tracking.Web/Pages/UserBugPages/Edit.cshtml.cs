using Bug.Tracking.Web.Data.Dto.Request;
using Bug.Tracking.Web.Data.Dto.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bug.Tracking.Web.Pages.UserBugPages
{
    public class EditModel : PageModel
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CreateModel> _logger;

        [BindProperty]
        public UserBugRequest UserBug { get; set; } = default!;

        public EditModel(IHttpClientFactory httpClientFactory, ILogger<CreateModel> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = _httpClientFactory.CreateClient("BugTrackingApi");

            var userBug = await client.GetFromJsonAsync<UserBugRequest>($"bug/{id}");

            if (userBug == null)
            {
                return NotFound();
            }

            UserBug = userBug;

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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _httpClientFactory.CreateClient("BugTrackingApi");

            var response = await client.PutAsJsonAsync<UserBugRequest>($"bug/{UserBug.Id}", UserBug);

            if (!response.IsSuccessStatusCode)
            {
                var errorsJson = response.Content.ReadAsStringAsync().Result;

                _logger.LogError(errorsJson);

                ViewData["ErrorMessage"] = $"Ocurrió un problema con la API: {response.ReasonPhrase}";
                return await OnGetAsync(UserBug.Id);
            }

            return RedirectToPage("./Index");
        }
    }
}
