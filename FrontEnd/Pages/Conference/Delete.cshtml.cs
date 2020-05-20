using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.Conference
{
    public class DeleteModel : PageModel
    {
        public IApiClientService ApiClient { get; }

        [BindProperty]
        public DataModels.Conference conference { get; set; }

        public DeleteModel(IApiClientService apiClientService)
        {
            ApiClient = apiClientService;
        }

        public void OnGet(int conference_id)
        {
            conference = ApiClient.GetConferenceAsync(conference_id).Result;

        }
        public IActionResult OnPost(int conference_id)
        {
            var result = ApiClient.DeleteConferenceAsync(conference_id).Result;
            if(result == false)
                return RedirectToPage("/Error");

            return RedirectToPage("/Index");


        }
    }
}