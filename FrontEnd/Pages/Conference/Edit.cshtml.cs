using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.Conference
{
    public class EditModel : PageModel
    {
        public IApiClientService ApiClient { get; }

        [BindProperty]
        public ConferenceResponse Conference { get; set; }

        public EditModel(IApiClientService apiClientService)
        {
            ApiClient = apiClientService;
        }

        public void OnGet(int conference_id)
        {
            Conference = ApiClient.GetConferenceAsync(conference_id).Result;
            Conference.Tags = Conference.Tags.ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            bool result = ApiClient.UpdateConferenceAsync(Conference).Result;
            if (result == false)
                return RedirectToPage("/Error");

            return RedirectToPage($"/Conference/Index", new { conference_id = Conference.ID });
        }
    }
}