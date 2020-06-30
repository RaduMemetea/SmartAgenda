using DataModels;
using FrontEnd.Models;
using FrontEnd.Models.Identity;
using FrontEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace FrontEnd.Pages.Conference
{
    [Authorize]
    public class AddModel : PageModel
    {
        private readonly IApiIdentityService identityClient;

        public IApiClientService ApiClient { get; }

        [BindProperty]
        public ConferenceResponse Conference { get; set; }

        [BindProperty]
        public string tagList { get; set; }

        public AddModel(IApiClientService apiClientService, IApiIdentityService IdentityService)
        {
            ApiClient = apiClientService;
            identityClient = IdentityService;
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

            identityClient.AddUserOwnershipAsync(new UserOwnership { UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value, ConferenceId = result.Item2 });

            return RedirectToPage($"/Conference/Index", new { conference_id = result.Item2 });
        }

    }
}

