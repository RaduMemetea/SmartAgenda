using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrontEnd.Models;
using FrontEnd.Services;

namespace FrontEnd.Pages.Session
{
    public class EditModel : PageModel
    {


        [BindProperty]
        public SessionResponse SessionResponse { get; set; }
        public IApiClientService ApiClient { get; }
        public ConferenceResponse Conference { get; private set; }
        [BindProperty]
        public string chairsList { get; set; }


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

            SessionResponse = ApiClient.GetSessionAsync(session_id.Value).Result;

            Conference = ApiClient.GetConferenceAsync(SessionResponse.ConferenceID).Result;


            if (SessionResponse == null)
            {
                return NotFound();
            }
           
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

          

         

            return RedirectToPage("./Index");
        }

        private bool SessionResponseExists(int id)
        {
            // return _context.SessionResponse.Any(e => e.ID == id);
            return true;
        }
    }
}
