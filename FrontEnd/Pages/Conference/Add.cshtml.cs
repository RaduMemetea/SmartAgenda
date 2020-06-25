using DataModels;
using FrontEnd.Models;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace FrontEnd.Pages.Conference
{
    public class AddModel : PageModel
    {
        public IApiClientService ApiClient { get; }

        [BindProperty]
        public ConferenceResponse Conference { get; set; }

        [BindProperty]
        public string tagList { get; set; }

        public AddModel(IApiClientService apiClientService)
        {
            ApiClient = apiClientService;
        }

        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            Conference.ID = 0;

            Conference.ParseTagString = tagList;

            var result = ApiClient.CreateConferenceResponseAsync(Conference).Result;
            if (result.Item1 == false)
                return RedirectToPage("/Error");

            return RedirectToPage($"/Conference/Index", new { conference_id = result.Item2 });
        }

    }
}

