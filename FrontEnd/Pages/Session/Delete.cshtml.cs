using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Authorization;
using FrontEnd.Models.Identity;

namespace FrontEnd.Pages.Session
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        public IApiClientService ApiClient { get; }
        public IApiIdentityService IdentityClient { get; }
        [BindProperty]
        public SessionResponse SessionResponse { get; set; }

        [BindProperty]
        public DataModels.Conference conference { get; set; }

        public DeleteModel(IApiClientService apiClientService, IApiIdentityService apiIdentityService)
        {
            ApiClient = apiClientService;
            IdentityClient = apiIdentityService;
        }

        public async Task<IActionResult> OnGetAsync(int? session_id)
        {
            if (session_id == null)
            {
                return NotFound();
            }

            SessionResponse = ApiClient.GetSessionResponseAsync(session_id.Value).Result;

            if (SessionResponse == null)
            {
                return NotFound();
            }

            conference = ApiClient.GetConferenceAsync(SessionResponse.ConferenceID).Result;

            if (conference == null)
            {
                return NotFound();
            }

            if (IdentityClient.GetUserOwnershipAsync(new UserOwnership { UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value, ConferenceId = conference.ID }).Result == null)
                return base.NotFound();


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? session_id)
        {
            if (session_id == null)
            {
                return NotFound();
            }

            conference = ApiClient.GetConferenceAsync(ApiClient.GetSessionResponseAsync(session_id.Value).Result.ConferenceID).Result;

            var result = ApiClient.DeleteSessionAsync(session_id.Value).Result;
            if (result == false)
                return RedirectToPage("/Error");

            
            return RedirectToPage("/Conference/Index", new { conference_id = conference.ID });

        }
    }
}
