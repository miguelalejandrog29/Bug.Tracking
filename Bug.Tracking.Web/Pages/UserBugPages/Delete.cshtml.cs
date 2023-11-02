using Bug.Tracking.Web.Data.Dto.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bug.Tracking.Web.Pages.UserBugPages
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CreateModel> _logger;

        [BindProperty]
        public UserBugResponse UserBug { get; set; } = default!;

        public DeleteModel(IHttpClientFactory httpClientFactory, ILogger<CreateModel> logger)
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

            var userBug = await client.GetFromJsonAsync<UserBugResponse>($"bug/{id}");

            if (userBug == null)
            {
                return NotFound();
            }
            else
            {
                UserBug = userBug;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = _httpClientFactory.CreateClient("BugTrackingApi");

            var response = await client.DeleteAsync($"bug/{UserBug.Id}");

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
