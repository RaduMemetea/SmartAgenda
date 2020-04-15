using FrontEnd.Pages.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        protected readonly IApiClientService _apiClient;
        public List<ConferenceResponse> Conferences;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IApiClientService apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        public async Task<IActionResult> OnGet()
        {
            if (SearchTerm is null)
                Conferences = (List<ConferenceResponse>)await _apiClient.GetConferencesAsync();
            else
                Conferences = ((List<ConferenceResponse>)await _apiClient.GetConferencesAsync()).FindAll(x => x.Name.ToLowerInvariant().Contains(SearchTerm.ToLowerInvariant()));


            if (Conferences is null || Conferences.Count == 0)
                return RedirectToPage("/Error");

            return Page();

        }
    }
}
