using DataModels;
using FrontEnd.Models;
using FrontEnd.Models.Identity;
using FrontEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FrontEnd.Pages.Session
{
 
    [Authorize]
    public class AddModel : PageModel
    {

        [BindProperty]
        public SessionResponse SessionResponse { get; set; }
        public IApiClientService ApiClient { get; }
        public IApiIdentityService IdentityClient { get; }
        public ConferenceResponse Conference { get; private set; }
        [BindProperty]
        public string HostsList { get; set; }

        public AddModel(IApiClientService apiClient, IApiIdentityService apiIdentityService)
        {
            ApiClient = apiClient;
            IdentityClient = apiIdentityService;
        }

        public IActionResult OnGet(int? conference_id)
        {
            if (conference_id is null) return NotFound();

            if (IdentityClient.GetUserOwnershipAsync(new UserOwnership { UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value, ConferenceId = conference_id.Value }).Result == null)
                return NotFound();

            SessionResponse = new SessionResponse();

            Conference = ApiClient.GetConferenceAsync(conference_id.Value).Result;

            return Page();
        }


        public IActionResult OnPost(int conference_id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            SessionResponse.ID = 0;
            SessionResponse.ConferenceID = conference_id;
            SessionResponse.Location.ID = 0;
            SessionResponse.ParseHostsString = HostsList;
           
            var response = ApiClient.CreateSessionResponseAsync(SessionResponse);
            if (response.Result.Item1 == false || response.Result.Item2 <= 0)
                return RedirectToPage("/Error");

            return RedirectToPage("/Conference/Index", new { conference_id });
        }
    }
}
