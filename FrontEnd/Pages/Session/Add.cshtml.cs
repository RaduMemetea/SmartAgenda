using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

using FrontEnd.Models;
using FrontEnd.Services;

namespace FrontEnd.Pages.Session
{
    public class AddModel : PageModel
    {

        [BindProperty]
        public SessionResponse SessionResponse { get; set; }
        public IApiClientService ApiClient { get; }
        public int Conference_id { get; private set; }

        public AddModel(IApiClientService apiClient)
        {
            ApiClient = apiClient;
        }

        public IActionResult OnGet(int conference_id)
        {
            Conference_id = conference_id;
            return Page();
        }
       

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            SessionResponse.ID = 0;
            SessionResponse.ConferenceID = Conference_id;
            SessionResponse.Location.ID = SessionResponse.LocationID = 0;

            //var response = ApiClient.AddSessionToConference(conference_id, Session);
           // await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
