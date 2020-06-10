using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using FrontEnd.Models;
using FrontEnd.Services;

namespace FrontEnd.Pages.Session
{
    public class DeleteModel : PageModel
    {
        public IApiClientService ApiClient { get; }
      
        [BindProperty]
        public SessionResponse SessionResponse { get; set; }

        [BindProperty]
        public DataModels.Conference conference { get; set; }

        public DeleteModel(IApiClientService apiClientService)
        {
            ApiClient = apiClientService;
        }

        public async Task<IActionResult> OnGetAsync(int? session_id)
        {
            if (session_id == null)
            {
                return NotFound();
            }

            SessionResponse = ApiClient.GetSessionAsync(session_id.Value).Result;

            if (SessionResponse == null)
            {
                return NotFound();
            }

            conference = ApiClient.GetConferenceAsync(SessionResponse.ConferenceID).Result;

            if (conference == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? session_id)
        {
            if (session_id == null)
            {
                return NotFound();
            }

            conference = ApiClient.GetConferenceAsync(ApiClient.GetSessionAsync(session_id.Value).Result.ConferenceID).Result;

            var result = ApiClient.DeleteSessionAsync(session_id.Value).Result;
            if (result == false)
                return RedirectToPage("/Error");


            return RedirectToPage("/Conference/Index", new { conference_id = conference.ID });

        }
    }
}
