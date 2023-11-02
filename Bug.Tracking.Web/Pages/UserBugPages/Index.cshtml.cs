using Bug.Tracking.Web.Data.Dto.Response;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("BugTrackingApi");


            var response = await client.GetFromJsonAsync<IList<UserBugResponse>>("bug");

            if (response != null) UserBug = response;
        }
    }
}
