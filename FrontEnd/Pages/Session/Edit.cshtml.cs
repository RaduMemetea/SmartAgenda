using DataModels;
using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Pages.Session
{
    public class EditModel : PageModel
    {


        [BindProperty]
        public SessionResponse SessionResponse { get; set; }
        public IApiClientService ApiClient { get; }
        public ConferenceResponse Conference { get; private set; }
        [BindProperty]
        public string HostsList { get; set; } = "";


        public EditModel(IApiClientService apiClient)
        {
            ApiClient = apiClient;
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

            Conference = ApiClient.GetConferenceAsync(SessionResponse.ConferenceID).Result;

            if (SessionResponse.Hosts != null && SessionResponse.Hosts.Any())
            {
                int i = 0;
                foreach (var person in SessionResponse.Hosts)
                    if (i == 0)// remove the case when the list beggins with ", {person}"
                    {
                        HostsList += $"{person.FullName}";
                        i = 1;
                    }
                    else
                        HostsList = $"{HostsList}, {person.FullName}";

            }


            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? session_id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (session_id == null)
            {
                return RedirectToPage("/Error");
            }

            var session = ApiClient.GetSessionResponseAsync(session_id.Value).Result;
            SessionResponse.ID = session.ID;
            SessionResponse.ConferenceID = session.ConferenceID;
            SessionResponse.Location.ID = 0; // remove the case when someone update a location and it changes N other entities
            SessionResponse.ParseHostsString = HostsList;

            var response = ApiClient.UpdateSessionResponseAsync(SessionResponse).Result;
            if (response.Item1 == false || response.Item2 <= 0)
                return RedirectToPage("/Error");

            return RedirectToPage("/Conference/Index", new { conference_id = SessionResponse.ConferenceID });
        }

      
    }
}
