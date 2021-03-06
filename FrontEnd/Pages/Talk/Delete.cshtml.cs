﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FrontEnd.Data;
using FrontEnd.Models;
using FrontEnd.Services;
using FrontEnd.Models.Identity;

namespace FrontEnd.Pages.Talk
{
    public class DeleteModel : PageModel
    {

        [BindProperty]
        public TalksResponse Talk { get; set; }

        public IApiClientService ApiClient { get; }
        public IApiIdentityService IdentityClient { get; }

        public DeleteModel(IApiClientService apiClientService, IApiIdentityService apiIdentityService)
        {
            ApiClient = apiClientService;
            IdentityClient = apiIdentityService;
        }

        public async Task<IActionResult> OnGetAsync(int? talk_id)
        {
            if (talk_id == null)
            {
                return NotFound();
            }

            Talk = ApiClient.GetTalkResponseAsync(talk_id.Value).Result;

            if (Talk == null)
            {
                return NotFound();
            }
            if (IdentityClient.GetUserOwnershipAsync(new UserOwnership { UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value, ConferenceId = ApiClient.GetConferenceFromTalkID(Talk.ID).Result }).Result == null)
                return base.NotFound();


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? talk_id)
        {
            if (talk_id == null)
            {
                return NotFound();
            }

            var result = ApiClient.DeleteTalkAsync(talk_id.Value).Result;
            if (result == false)
                return RedirectToPage("/Error");


            return RedirectToPage("/Conference/Index", new { conference_id = ApiClient.GetConferenceFromTalkID(talk_id.Value).Result });

        }
    }
}
