using FrontEnd.Pages.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontEnd.Pages
{
    public class ConferenceModel : PageModel
    {
        private readonly ILogger<ConferenceModel> _logger;
        protected readonly IApiClientService _apiClient;

        public ConferenceResponse conference { get; set; }
        public List<SessionResponse> Sessions { get; set; }
        public ConferenceModel(ILogger<ConferenceModel> logger, IApiClientService apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }
        public async Task<IActionResult> OnGet(int conference_id)
        {
            conference = await _apiClient.GetConferenceAsync(conference_id);

            if (conference == null)
                return RedirectToPage("/Index");
            Sessions = await _apiClient.GetSessionsByConference(conference.ID);
            if (Sessions is null || Sessions.Count == 0)
                Sessions = new List<SessionResponse>() { };


            return Page();
        }
    }
}